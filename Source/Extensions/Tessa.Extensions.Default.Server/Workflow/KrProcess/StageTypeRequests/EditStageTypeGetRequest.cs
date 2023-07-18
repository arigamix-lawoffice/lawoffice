using System.Linq;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.StageTypeRequests
{
    public sealed class EditStageTypeGetRequest : CardGetExtension
    {
        public override Task AfterRequest(
            ICardGetExtensionContext context)
        {
            Card card;
            if (!context.ValidationResult.IsSuccessful()
                || (card = context.Response.TryGetCard()) is null
                || !card.TryGetKrApprovalCommonInfoSection(out var aci)
                || !aci.Fields.TryGetValue(KrConstants.KrApprovalCommonInfo.AuthorComment, out var authCommObj)
                || !(authCommObj is string authComm)
                || string.IsNullOrWhiteSpace(authComm))
            {
                return Task.CompletedTask;
            }

            var cardTaskSections = card
                .TryGetTasks()
                ?.FirstOrDefault(p => p.TypeID == DefaultTaskTypes.KrEditTypeID)
                ?.TryGetCard()
                ?.TryGetSections();

            if (cardTaskSections != null
                && cardTaskSections.TryGetValue(KrConstants.KrTaskCommentVirtual.Name, out var commSec))
            {
                commSec.RawFields[KrConstants.KrTaskCommentVirtual.Comment] = authComm;
            }

            return Task.CompletedTask;
        }
    }
}