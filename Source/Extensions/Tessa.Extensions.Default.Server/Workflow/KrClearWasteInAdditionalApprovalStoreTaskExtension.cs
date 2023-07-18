using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Workflow
{
    /// <summary>
    /// Расширение очищает поля с параметрами вариантов завершения у задачи доп. согласования,
    /// когда задача завершается без удаления.
    /// </summary>
    public sealed class KrClearWasteInAdditionalApprovalStoreTaskExtension :
        CardStoreTaskExtension
    {
        #region Base Overrides

        /// <inheritdoc/>
        public override Task StoreTaskBeforeRequest(ICardStoreTaskExtensionContext context)
        {
            Card card;
            StringDictionaryStorage<CardSection> sections;

            if (!context.IsCompletion
                || context.State != CardRowState.Modified
                || (card = context.Task.TryGetCard()) is null
                || (sections = card.TryGetSections()) is null)
            {
                return Task.CompletedTask;
            }

            if (sections.TryGetValue(KrConstants.KrAdditionalApprovalTaskInfo.Name, out var krAdditionalApprovalTaskInfo))
            {
                krAdditionalApprovalTaskInfo.SetChanged(KrConstants.KrAdditionalApprovalTaskInfo.Comment, false);
            }

            return Task.CompletedTask;
        }

        #endregion
    }
}
