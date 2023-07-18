using System;
using System.Threading.Tasks;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Scope;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.Handlers
{
    /// <summary>
    /// Обработчик этапа <see cref="StageTypeDescriptors.HistoryManagementDescriptor"/>.
    /// </summary>
    public class HistoryManagementStageTypeHandler : StageTypeHandlerBase
    {
        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="HistoryManagementStageTypeHandler"/>.
        /// </summary>
        /// <param name="krScope">Объект предоставляющий методы для работы с текущим контекстом расширений типового расширения и использования разделяемых объектов карточек.</param>
        public HistoryManagementStageTypeHandler(
            IKrScope krScope)
        {
            this.KrScope = krScope ?? throw new ArgumentNullException(nameof(krScope));
        }

        #endregion

        #region Protected Properties

        /// <summary>
        /// Возвращает или задаёт объект предоставляющий методы для работы с текущим контекстом расширений типового расширения и использования разделяемых объектов карточек.
        /// </summary>
        protected IKrScope KrScope { get; set; }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override async Task<StageHandlerResult> HandleStageStartAsync(IStageTypeHandlerContext context)
        {
            if (!context.MainCardID.HasValue)
            {
                context.ValidationResult.AddError(this, "$KrStages_HistoryManagement_GlobalContextError");
                return StageHandlerResult.SkipResult;
            }

            var taskHistoryGroupID =
                context.Stage.SettingsStorage.TryGet<Guid?>(
                    KrConstants.KrHistoryManagementStageSettingsVirtual.TaskHistoryGroupTypeID);

            if (taskHistoryGroupID.HasValue)
            {
                var parentTaskHistoryGroupID =
                    context.Stage.SettingsStorage.TryGet<Guid?>(
                        KrConstants.KrHistoryManagementStageSettingsVirtual.ParentTaskHistoryGroupTypeID);
                var newIteration =
                    context.Stage.SettingsStorage.TryGet<bool?>(
                        KrConstants.KrHistoryManagementStageSettingsVirtual.NewIteration) ?? default;

                var group = await context.TaskHistoryResolver.ResolveTaskHistoryGroupAsync(
                    taskHistoryGroupID.Value,
                    parentTaskHistoryGroupID,
                    newIteration,
                    cancellationToken: context.CancellationToken);

                if (group is null)
                {
                    return StageHandlerResult.EmptyResult;
                }

                await this.KrScope.SetCurrentHistoryGroupAsync(
                    context.MainCardID.Value,
                    group.RowID,
                    cancellationToken: context.CancellationToken);
            }
            else
            {
                await this.KrScope.SetCurrentHistoryGroupAsync(
                    context.MainCardID.Value,
                    null,
                    cancellationToken: context.CancellationToken);
            }

            return StageHandlerResult.CompleteResult;
        }

        #endregion
    }
}