using System;
using System.Linq;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Data;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess
{
    /// <summary>
    /// Расширение на сохранение задания <see cref="DefaultTaskTypes.KrUniversalTaskTypeID"/>. Сохраняет вариант завершения задания, с которым оно было завершено, и комментарий в <see cref="CardInfoStorageObject.Info"/>. Завершает задание с вариантом завершения <see cref="DefaultCompletionOptions.Approve"/>.
    /// </summary>
    public sealed class KrUniversalTaskStoreExtension : CardStoreTaskExtension
    {
        #region Constants

        /// <summary>
        /// Имя ключа, по которому в <see cref="CardInfoStorageObject.Info"/> объекта настраиваемого задания содержится идентификатор варианта завершения или значение <see langword="null"/>, если задание не завершается. Тип значения: <see cref="Nullable{T}"/>, где T - <see cref="Guid"/>.
        /// </summary>
        public const string OptionIDKey = CardHelper.SystemKeyPrefix + "universalOptionID";

        /// <summary>
        /// Имя ключа, по которому в <see cref="CardInfoStorageObject.Info"/> объекта настраиваемого задания содержится отображаемое имя варианта завершения или значение <see langword="null"/>, если задание не завершается. Тип значения: <see cref="string"/>.
        /// </summary>
        public const string OptionCaptionKey = CardHelper.SystemKeyPrefix + "universalOptionCaption";

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override async Task StoreTaskBeforeRequest(ICardStoreTaskExtensionContext context)
        {
            if (!context.Task.OptionID.HasValue)
            {
                return;
            }

            var optionID = context.Task.OptionID.Value;
            if (optionID == DefaultCompletionOptions.Cancel)
            {
                // HandleInterrupt отзывает задание с вариантом Cancel, см. UniversalTaskStageTypeHandler
                return;
            }

            var taskCardSections = context.Task.Card.Sections;
            var optionRow = taskCardSections
                .TryGet(KrConstants.KrUniversalTaskOptions.Name)
                ?.TryGetRows()
                ?.FirstOrDefault(x => x.Get<Guid?>(KrConstants.KrUniversalTaskOptions.OptionID) == optionID);
            string optionCaption;

            if (optionRow is null)
            {
                if (context.Request.ServiceType != CardServiceType.Default)
                {
                    context.ValidationResult.AddError(
                        this,
                        "$KrProcess_UniversalTask_NotFoundCompletionOption",
                        optionID.ToString());
                    return;
                }

                if (!(await context.CardMetadata.GetEnumerationsAsync(context.CancellationToken))
                        .CompletionOptions.TryGetValue(optionID, out var completionOption))
                {
                    ValidationSequence
                        .Begin(context.ValidationResult)
                        .SetObjectName(this)
                        .Error(CardValidationKeys.UnknownTaskOption, context.Task.RowID, optionID)
                        .End();

                    return;
                }

                optionCaption = completionOption.Caption;
            }
            else
            {
                if (optionRow.TryGet<bool>(KrConstants.KrUniversalTaskOptionsSettingsVirtual.ShowComment))
                {
                    context.Task.Result = taskCardSections.TryGet(KrConstants.KrTask.Name)?.RawFields.TryGet<string>(KrConstants.KrTask.Comment);
                }

                context.Task.OptionID = DefaultCompletionOptions.Approve;
                optionCaption = optionRow.Get<string>(KrConstants.KrUniversalTaskOptions.Caption);
            }

            context.StoreContext.SetTaskAccessCheckIsIgnored(context.Task.RowID);
            context.Task.Info[OptionIDKey] = optionID;
            context.Task.Info[OptionCaptionKey] = optionCaption;
        }

        /// <inheritdoc/>
        public override async Task StoreTaskBeforeCommitTransaction(ICardStoreTaskExtensionContext context)
        {
            var taskInfo = context.Task.TryGetInfo();

            if (taskInfo is null
                || !taskInfo.TryGetValue(OptionIDKey, out var optionIDObj)
                || optionIDObj is not Guid optionID
                || !taskInfo.TryGetValue(OptionCaptionKey, out var optionCaptionObj)
                || optionCaptionObj is not string optionCaption)
            {
                return;
            }

            var executor = context.DbScope.Executor;

            await executor.ExecuteNonQueryAsync(
                context.DbScope.BuilderFactory
                    .Update("TaskHistory")
                        .C("OptionID").Equals().P("OptionID")
                        .C("OptionCaption").Equals().P("Caption")
                    .Where().C("RowID").Equals().P("TaskID")
                    .Build(),
                context.CancellationToken,
                executor.Parameter("TaskID", context.Task.RowID),
                executor.Parameter("Caption", optionCaption),
                executor.Parameter("OptionID", optionID));
        }

        #endregion
    }
}
