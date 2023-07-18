using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.Handlers;
using Tessa.Extensions.Default.Shared;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.StageTypeRequests
{
    public sealed class DialogCardExtension : CardRequestExtension
    {
        /// <inheritdoc />
        public override Task AfterRequest(
            ICardRequestExtensionContext context)
        {
            // Это случай основной карточки. С помощью ключа в ValidationResult передаем признак в респонс о том
            // что необходимо не закрывать окно диалога.
            var hasDialog = context.ValidationResult.RemoveAll(DefaultValidationKeys.CancelDialog);
            if (hasDialog != 0)
            {
                context.Response.SetKeepTaskDialog();
            }

            if (context.Info.TryGetValue(DialogStageTypeHandler.ChangedCardKey, out var changedCard))
            {
                context.Response.Info[DialogStageTypeHandler.ChangedCardKey] = changedCard;
            }

            if (context.Info.TryGetValue(DialogStageTypeHandler.ChangedCardFileContainerKey, out var changedCardFileContainer))
            {
                context.Response.Info[DialogStageTypeHandler.ChangedCardFileContainerKey] = changedCardFileContainer;
            }

            return Task.CompletedTask;
        }
    }
}
