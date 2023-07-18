using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Extensions.Default.Server.Workflow.KrObjectModel;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Scope;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Serialization;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.KrPermissions;
using Tessa.Extensions.Default.Shared.Workflow.WorkflowEngine;
using Tessa.Platform;
using Tessa.Platform.Data;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using Tessa.Scheme;
using static Tessa.Extensions.Default.Shared.Workflow.KrProcess.KrConstants;

namespace Tessa.Extensions.Default.Server.Workflow.KrPermissions
{
    /// <summary>
    ///  Расширение на проверку прав доступа по заданию отправленному из подсистемы маршрутов. Выдаёт права на редактирование карточки и приложенных файлов.
    /// </summary>
    public sealed class KrProcessTasksPermissionsExtension : ITaskPermissionsExtension
    {
        #region Nested Types

        private sealed class PermissionsRegex
        {
            #region Constructors

            public PermissionsRegex(string canEditCardField, string canEditFilesField)
            {
                this.CanEditCardField = canEditCardField;
                this.CanEditCardRegex = new Regex($"\"{Regex.Escape(canEditCardField)}\":\\s*true", RegexOptions.Compiled);

                this.CanEditFilesField = canEditFilesField;
                this.CanEditFilesRegex = new Regex($"\"{Regex.Escape(canEditFilesField)}\":\\s*true", RegexOptions.Compiled);
            }

            #endregion

            #region Properties

            public string CanEditCardField { get; }

            public Regex CanEditCardRegex { get; }

            public string CanEditFilesField { get; }

            public Regex CanEditFilesRegex { get; }

            #endregion
        }

        #endregion

        #region Fields

        private readonly IKrProcessContainer processContainer;
        private readonly IKrScope krScope;
        private readonly IKrStageSerializer serializer;

        private static readonly Dictionary<Guid, PermissionsRegex> permissionsByTaskTypes =
            new()
            {
                {
                    DefaultTaskTypes.KrApproveTypeID,
                    new PermissionsRegex(KrApprovalSettingsVirtual.CanEditCard, KrApprovalSettingsVirtual.CanEditFiles)
                },
                {
                    DefaultTaskTypes.KrSigningTypeID,
                    new PermissionsRegex(KrSigningStageSettingsVirtual.CanEditCard, KrSigningStageSettingsVirtual.CanEditFiles)
                },
                {
                    DefaultTaskTypes.KrRegistrationTypeID,
                    new PermissionsRegex(KrRegistrationStageSettingsVirtual.CanEditCard, KrRegistrationStageSettingsVirtual.CanEditFiles)
                },
                {
                    DefaultTaskTypes.KrUniversalTaskTypeID,
                    new PermissionsRegex(KrUniversalTaskSettingsVirtual.CanEditCard, KrUniversalTaskSettingsVirtual.CanEditFiles)
                },
            };

        private static readonly PermissionsRegex wePermissionsRegex =
            new(
                WorkflowConstants.NamesKeys.CanEditCard,
                WorkflowConstants.NamesKeys.CanEditAnyFiles);

        #endregion

        #region Constructors

        public KrProcessTasksPermissionsExtension(
            IKrProcessContainer processContainer,
            IKrScope krScope,
            IKrStageSerializer serializer)
        {
            Check.ArgumentNotNull(processContainer, nameof(processContainer));
            Check.ArgumentNotNull(krScope, nameof(krScope));
            Check.ArgumentNotNull(serializer, nameof(serializer));

            this.processContainer = processContainer;
            this.krScope = krScope;
            this.serializer = serializer;
        }

        #endregion

        #region ITaskPermissionsExtension Members

        /// <inheritdoc/>
        public async Task ExtendPermissionsAsync(ITaskPermissionsExtensionContext context)
        {
            var task = context.Task;
            var taskType = context.TaskType;
            var taskTypeID = taskType.ID;

            bool supportedTaskType = await this.processContainer.IsTaskTypeRegisteredAsync(taskTypeID, context.CancellationToken);
            if (!supportedTaskType)
            {
                return;
            }

            var descriptor = context.Descriptor;

            descriptor.Set(KrPermissionFlagDescriptors.ReadCard, true);
            descriptor.Set(KrPermissionFlagDescriptors.SignFiles, true);

            var autoStartTask = task.StoredState == CardTaskState.Created
                && (taskType.Flags.Has(CardTypeFlags.AutoStartTasks)
                    || task.Flags.Has(CardTaskFlags.AutoStart));

            // Задания процесса согласования дают права только, если пользователь исполнитель и задание в работе/отложено или автоматически берётся в работу.
            if (!task.IsCanPerform
                || task.StoredState != CardTaskState.InProgress
                && task.StoredState != CardTaskState.Postponed
                && taskTypeID != DefaultTaskTypes.KrRequestCommentTypeID
                && !autoStartTask)
            {
                return;
            }

            //Задание редактирования, как исполнитель, задание в работе или отложено
            if (taskTypeID == DefaultTaskTypes.KrEditTypeID ||
                taskTypeID == DefaultTaskTypes.KrEditInterjectTypeID)
            {
                descriptor.Set(KrPermissionFlagDescriptors.FullCardPermissionsGroup, true, true);
                return;
            }

            // для всех типов задач в маршрутах, кроме перечисленных выше, даём права на новые файлы, когда они в работе
            descriptor
                .Set(KrPermissionFlagDescriptors.AddFiles, true)
                .Set(KrPermissionFlagDescriptors.EditOwnFiles, true);

            // Задание согласования, как исполнитель, задание в работе или отложено
            if (!permissionsByTaskTypes.ContainsKey(taskTypeID))
            {
                return;
            }

            bool isEditCard;
            bool isEditFiles;

            if (await WorkflowHelper.IsWorkflowEngineTaskAsync(task, context.Card, context.DbScope, context.CancellationToken))
            {
                (isEditCard, isEditFiles) = await GetWorkflowPermissionsValueAsync(context);
            }
            else
            {
                bool isSuccess;
                (isSuccess, isEditCard, isEditFiles) = await this.GetKrRoutePermissionsValueAsync(context);

                if (!isSuccess)
                {
                    return;
                }
            }

            if (isEditCard)
            {
                descriptor.Set(KrPermissionFlagDescriptors.EditCard, true);
            }
            if (isEditFiles)
            {
                descriptor.Set(KrPermissionFlagDescriptors.EditFiles, true);
            }
        }

        #endregion

        #region Private methods

        private static async Task<(bool isEditCard, bool isEditFiles)> GetWorkflowPermissionsValueAsync(
            ITaskPermissionsExtensionContext context)
        {
            var task = context.Task;
            bool isEditCard = default;
            bool isEditFiles = default;

            if (context.Mode == KrPermissionsCheckMode.WithCard)
            {
                var settings = task.Settings;
                if (settings.Any())
                {
                    isEditCard = settings.TryGet<bool>(wePermissionsRegex.CanEditCardField);
                    isEditFiles = settings.TryGet<bool>(wePermissionsRegex.CanEditFilesField);
                }
            }
            else
            {
                await using (context.DbScope.Create())
                {
                    var db = context.DbScope.Db;
                    var settings = await db
                        .SetCommand(
                            context.DbScope.BuilderFactory
                                .Select().C("Settings")
                                .From(Names.Tasks).NoLock()
                                .Where().C(Names.Tasks_RowID).Equals().P("TaskID")
                                .Build(),
                            db.Parameter("TaskID", task.RowID))
                        .LogCommand()
                        .ExecuteStringAsync(context.CancellationToken);

                    if (!string.IsNullOrEmpty(settings))
                    {
                        isEditCard = wePermissionsRegex.CanEditCardRegex.IsMatch(settings);
                        isEditFiles = wePermissionsRegex.CanEditFilesRegex.IsMatch(settings);
                    }
                }
            }

            return (isEditCard, isEditFiles);
        }

        private async Task<(bool isSuccess, bool isEditCard, bool isEditFiles)> GetKrRoutePermissionsValueAsync(
            ITaskPermissionsExtensionContext context)
        {
            var task = context.Task;
            var taskTypeID = context.TaskType.ID;

            bool isEditCard = default;
            bool isEditFiles = default;

            if (permissionsByTaskTypes.TryGetValue(taskTypeID, out PermissionsRegex permissions))
            {
                // Задание также может выдавать право редактирования всех файлов и карточки - нужно залезть в карточку и глянуть, что там в этапе указано.
                if (context.Mode == KrPermissionsCheckMode.WithCard)
                {
                    var card = context.Card;

                    //Проверка на загрузке карточки - можно глянуть в саму карточку в контексте
                    var stage = card.Sections[KrStages.Virtual].Rows.FirstOrDefault(x =>
                        x.RowID == card.Sections[KrApprovalCommonInfo.Virtual].Fields.Get<Guid?>(KrProcessCommonInfo.CurrentApprovalStageRowID));

                    if (stage is null)
                    {
                        stage = await this.GetStageRowFromNotMainProcessAsync(
                            task.RowID,
                            context.DbScope,
                            context.ValidationResult,
                            context.CancellationToken);

                        if (stage is null)
                        {
                            return default;
                        }
                    }

                    if (stage.Get<bool>(permissions.CanEditCardField))
                    {
                        isEditCard = true;
                    }

                    if (stage.Get<bool>(permissions.CanEditFilesField))
                    {
                        isEditFiles = true;
                    }
                }
                else
                {
                    await using (context.DbScope.Create())
                    {
                        DbManager db = context.DbScope.Db;

                        string settings = await db
                            .SetCommand(
                                context.DbScope.BuilderFactory
                                    .Select().C("ks", "Settings")
                                    .From("KrStages", "ks").NoLock()
                                    .InnerJoin("KrApprovalCommonInfo", "kaci").NoLock()
                                    .On().C("kaci", "CurrentApprovalStageRowID").Equals().C("ks", "RowID")
                                    .Where().C("kaci", "MainCardID").Equals().P("MainCardID")
                                    .Build(),
                                db.Parameter("MainCardID", context.CardID))
                            .LogCommand()
                            .ExecuteStringAsync(context.CancellationToken);

                        if (!string.IsNullOrEmpty(settings))
                        {
                            isEditCard = permissions.CanEditCardRegex.IsMatch(settings);
                            isEditFiles = permissions.CanEditFilesRegex.IsMatch(settings);
                        }
                    }
                }
            }

            return (true, isEditCard, isEditFiles);
        }

        private async Task<CardRow> GetStageRowFromNotMainProcessAsync(
            Guid taskRowID,
            IDbScope dbScope,
            IValidationResultBuilder validationResult,
            CancellationToken cancellationToken = default)
        {
            await using (dbScope.Create())
            {
                var db = dbScope.Db;

                db.SetCommand(
                    dbScope.BuilderFactory
                        .Select()
                        .C("wp", "TypeName")
                        .C("wt", "ProcessRowID")
                        .C("wp", "Params")
                        .From("WorkflowTasks", "wt").NoLock()
                        .InnerJoin("WorkflowProcesses", "wp").NoLock()
                        .On().C("wt", "ProcessRowID").Equals().C("wp", "RowID")
                        .Where().C("wt", "RowID").Equals().P("TaskRowID")
                        .Build(),
                    db.Parameter("TaskRowID", taskRowID))
                    .LogCommand();

                string processTypeName;
                Guid? nestedProcessID = default;
                Guid processHolderID;
                await using (var reader = await db.ExecuteReaderAsync(CommandBehavior.SequentialAccess, cancellationToken))
                {
                    if (!await reader.ReadAsync(cancellationToken))
                    {
                        return default;
                    }

                    processTypeName = reader.GetString(0);

                    switch (processTypeName)
                    {
                        case KrSecondaryProcessName:
                            processHolderID = reader.GetGuid(1);
                            break;

                        case KrNestedProcessName:
                            var processParamsJson = await reader.GetSequentialNullableStringAsync(2, cancellationToken);
                            var processParams = StorageHelper.DeserializeFromTypedJson(processParamsJson);
                            nestedProcessID = processParams.TryGet<Guid>(Keys.NestedProcessID);
                            processHolderID = processParams.TryGet<Guid>(Keys.ProcessHolderID);
                            break;

                        default:
                            return default;
                    }
                }

                await using var krScopeContext = KrScopeContext.Create();
                var satellite = await this.krScope.GetSecondaryKrSatelliteAsync(processHolderID, cancellationToken);

                validationResult.Add(krScopeContext.Value.ValidationResult);

                if (!krScopeContext.Value.ValidationResult.IsSuccessful())
                {
                    return default;
                }

                if (!satellite.Sections.ContainsKey(KrStages.Virtual))
                {
                    satellite.Sections.Add(KrStages.Virtual);
                }

                await this.serializer.DeserializeSectionsAsync(
                    satellite,
                    satellite,
                    cancellationToken: cancellationToken);

                var krSecondaryProcessCommonInfoFields = satellite.Sections[KrSecondaryProcessCommonInfo.Name].Fields;
                Guid? currentStageRowID;

                switch (processTypeName)
                {
                    case KrSecondaryProcessName:
                        currentStageRowID = krSecondaryProcessCommonInfoFields.Get<Guid?>(KrProcessCommonInfo.CurrentApprovalStageRowID);
                        break;
                    case KrNestedProcessName:
                        var nestedWorkflowProcesses = krSecondaryProcessCommonInfoFields.Get<string>(KrProcessCommonInfo.NestedWorkflowProcesses);
                        if (string.IsNullOrWhiteSpace(nestedWorkflowProcesses))
                        {
                            return default;
                        }
                        else
                        {
                            currentStageRowID = this.serializer.Deserialize<List<object>>(nestedWorkflowProcesses)
                                .Select(p => new NestedProcessCommonInfo((Dictionary<string, object>) p))
                                .FirstOrDefault(i => i.NestedProcessID == nestedProcessID)?.CurrentStageRowID;
                        }
                        break;
                    default:
                        return default;
                }

                if (!currentStageRowID.HasValue)
                {
                    return default;
                }

                return satellite.Sections[KrStages.Virtual].Rows.FirstOrDefault(x =>
                    x.RowID == currentStageRowID.Value);
            }
        }

        #endregion
    }
}
