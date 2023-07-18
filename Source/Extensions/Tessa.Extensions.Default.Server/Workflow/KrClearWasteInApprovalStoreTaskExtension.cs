using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Workflow
{
    /// <summary>
    /// Расширение очищает поля с параметрами вариантов завершения у задачи, когда она завершается без удаления.
    /// </summary>
    public sealed class KrClearWasteInApprovalStoreTaskExtension :
        CardStoreTaskExtension
    {
        #region Private Methods

        private static void MarkRowsAsDeleted(CardSection section)
        {
            var rows = section.TryGetRows();

            if (rows is not null && rows.Count > 0)
            {
                foreach (var row in rows)
                {
                    row.State = CardRowState.Deleted;
                }
            }
        }

        #endregion

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

            if (sections.TryGetValue(KrConstants.KrTask.Name, out var krTask))
            {
                krTask.SetChanged(KrConstants.KrTask.Comment, false);
            }

            if (sections.TryGetValue(KrConstants.KrCommentators.Name, out var krCommentators))
            {
                MarkRowsAsDeleted(krCommentators);
            }

            if (sections.TryGetValue(KrConstants.KrAdditionalApprovalUsers.Name, out var krAdditionalApprovalUsers))
            {
                MarkRowsAsDeleted(krAdditionalApprovalUsers);
            }

            if (sections.TryGetValue(KrConstants.KrAdditionalApproval.Name, out var krAdditionalApproval))
            {
                krAdditionalApproval.SetChanged(KrConstants.KrAdditionalApproval.Comment, false);
                krAdditionalApproval.SetChanged(KrConstants.KrAdditionalApproval.TimeLimitation, false);
                krAdditionalApproval.SetChanged(KrConstants.KrAdditionalApproval.FirstIsResponsible, false);
            }

            return Task.CompletedTask;
        }

        #endregion
    }
}
