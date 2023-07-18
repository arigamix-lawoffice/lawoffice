#nullable enable

using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using LinqToDB;
using Tessa.Cards.Extensions;
using Tessa.Cards.Workflow;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Extensions.Default.Shared.Workflow.Wf;
using Tessa.Extensions.Default.Shared.Workflow.WorkflowEngine;
using Tessa.Platform;
using Tessa.Platform.Data;
using Tessa.Platform.Storage;
using Tessa.Workflow;
using Tessa.Workflow.Helpful;
using Tessa.Workflow.Signals;
using WorkflowSignalTypes = Tessa.Workflow.Signals.WorkflowSignalTypes;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess
{
    /// <summary>
    /// Расширение на сохранение задания. Уведомляет подсистему маршрутов или Workflow Engine о завершении следующих типов заданий: <see cref="DefaultTaskTypes.WfResolutionTypeID"/>, <see cref="DefaultTaskTypes.WfResolutionChildTypeID"/>, <see cref="DefaultTaskTypes.WfResolutionControlTypeID"/>.
    /// </summary>
    public sealed class KrResolutionStoreExtensions : CardStoreTaskExtension
    {
        #region Constants

        private const string KrResolutionRevokingChildren = nameof(KrResolutionRevokingChildren);

        #endregion

        #region Fields

        private readonly IWorkflowEngineProcessor workflowProcessor;

        #endregion

        #region Constructor

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="KrResolutionStoreExtensions"/>.
        /// </summary>
        /// <param name="workflowProcessor"><inheritdoc cref="IWorkflowEngineProcessor" path="/summary"/></param>
        public KrResolutionStoreExtensions(
            IWorkflowEngineProcessor workflowProcessor) =>
            this.workflowProcessor = NotNullOrThrow(workflowProcessor);

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override async Task StoreTaskBeforeCommitTransaction(ICardStoreTaskExtensionContext context)
        {
            if (!context.IsCompletion
                || context.CompletionOption?.ID != DefaultCompletionOptions.Complete
                && context.CompletionOption?.ID != DefaultCompletionOptions.Revoke
                || !context.ValidationResult.IsSuccessful())
            {
                return;
            }

            var parentContext = WorkflowScopeContext.Current.StoreContext;
            if (parentContext is not null
                && parentContext.Info.TryGet<bool>(KrResolutionRevokingChildren))
            {
                return;
            }

            if (context.Task.Card
                .Sections[WfHelper.ResolutionSection]
                .Fields.Get<bool>(WfHelper.ResolutionRevokeChildrenField))
            {
                context.StoreContext.Info[KrResolutionRevokingChildren] = BooleanBoxes.True;
            }

            Guid parentTaskID;
            string? processKind;
            Guid? processID;

            await using (context.DbScope!.Create())
            {
                var db = context.DbScope.Db;
                var factory = context.DbScope.BuilderFactory;

                // 1. Получение ИД первого задания и параметров процесса.
                await using (var rootTaskReader = await db
                    .SetCommand(factory
                        .With("ParentTaskHistory", static e => e
                                    .Select()
                                        .C("t", "ParentRowID")
                                    .From("TaskHistory", "t").NoLock()
                                    .Where()
                                        .C("t", "TypeID").NotEquals().P("ProjectTypeID")
                                        .And().C("t", "RowID").Equals().P("RowID")
                                    .UnionAll()
                                    .Select()
                                        .C("t", "ParentRowID")
                                    .From("TaskHistory", "t").NoLock()
                                    .InnerJoin("ParentTaskHistory", "p")
                                        .On().C("p", "RowID").Equals().C("t", "RowID")
                                    .Where()
                                        .C("t", "TypeID").NotEquals().P("ProjectTypeID"),
                                columnNames: new[] { "RowID" },
                                recursive: true)
                            .Select().Top(1)
                                .C("t", "Settings", "RowID")
                            .From("TaskHistory", "t").NoLock()
                            .InnerJoin("ParentTaskHistory", "p").NoLock()
                                .On().C("p", "RowID").Equals().C("t", "RowID")
                            .Where()
                                .C("t", "TypeID").Equals().P("ProjectTypeID")
                            .Limit(1)
                        .Build(),
                        db.Parameter("RowID", context.Task.RowID, DataType.Guid),
                        db.Parameter("ProjectTypeID", DefaultTaskTypes.WfResolutionProjectTypeID, DataType.Guid))
                    .LogCommand()
                    .ExecuteReaderAsync(CommandBehavior.SequentialAccess, context.CancellationToken))
                {
                    if (!await rootTaskReader.ReadAsync(context.CancellationToken))
                    {
                        return;
                    }

                    var settingsJson = rootTaskReader.GetNullableString(0);

                    if (string.IsNullOrEmpty(settingsJson))
                    {
                        return;
                    }

                    var settings = StorageHelper.DeserializeFromTypedJson(settingsJson);

                    if (settings is null
                        || !(processID = settings.TryGet<Guid?>(KrConstants.TaskHistorySettingsKeys.ProcessID)).HasValue
                        || (processKind = settings.TryGet<string>(KrConstants.TaskHistorySettingsKeys.ProcessKind)) is null)
                    {
                        return;
                    }

                    parentTaskID = rootTaskReader.GetGuid(1);
                }

                // 2. Проверка наличия незавершённых дочерних заданий.
                var isChildTasksCompleted =
                    context.CompletionOption.ID == DefaultCompletionOptions.Revoke
                    || !await db
                    .SetCommand(
                        factory
                            .With("ChildTaskHistory", static e => e
                                .Select()
                                    .C("t", "RowID")
                                .From("TaskHistory", "t").NoLock()
                                .Where()
                                    .C("t", "ParentRowID").Equals().P("RootRowID")
                                .UnionAll()
                                .Select()
                                    .C("t", "RowID")
                                .From("TaskHistory", "t").NoLock()
                                .InnerJoin("ChildTaskHistory", "p")
                                    .On().C("p", "RowID").Equals().C("t", "ParentRowID"),
                                columnNames: new[] { "RowID" },
                                recursive: true)
                            .Select().Top(1)
                                .V(true)
                            .From("TaskHistory", "t").NoLock()
                            .InnerJoin("ChildTaskHistory", "p")
                                .On().C("t", "RowID").Equals().C("p", "RowID")
                            .InnerJoin("WfSatelliteTaskHistory", "s")
                                .On().C("t", "RowID").Equals().C("s", "RowID")
                            .Where()
                                .C("t", "Completed").IsNull()
                                .Or().C("s", "Controlled").Equals().V(false)
                            .Limit(1)
                            .Build(),
                        db.Parameter("RootRowID", parentTaskID, DataType.Guid))
                    .LogCommand()
                    .ExecuteAsync<bool>(context.CancellationToken);

                if (!isChildTasksCompleted)
                {
                    return;
                }
            }

            switch (processKind)
            {
                case KrConstants.KrProcessName:
                case KrConstants.KrSecondaryProcessName:
                case KrConstants.KrNestedProcessName:
                    context.Request.Card
                        .GetWorkflowQueue()
                        .AddSignal(name: KrConstants.KrPerformSignal, processID: processID, processTypeName: processKind);
                    break;
                case WorkflowEngineHelper.WorkflowEngineProcessName:
                    {
                        var signal = new WorkflowEngineTaskSignal(
                            WorkflowSignalTypes.CompleteTask,
                            new List<object> { parentTaskID })
                        {
                            OptionID = context.CompletionOption.ID
                        };
                        signal.Hash[WorkflowConstants.NamesKeys.CompletedTaskRowID] = context.Task.RowID;

                        var result =
                            await this.workflowProcessor.SendSignalToAllSubscribersAsync(
                                processID.Value,
                                signal,
                                WorkflowEngineProcessFlags.DefaultRuntime,
                                storeCard: context.Request.Card,
                                cancellationToken: context.CancellationToken);
                        context.ValidationResult.Add(result.ValidationResult);

                        break;
                    }
            }
        }

        #endregion
    }
}
