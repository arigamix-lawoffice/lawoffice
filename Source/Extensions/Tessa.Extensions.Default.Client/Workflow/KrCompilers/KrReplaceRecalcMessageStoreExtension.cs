using System.Threading.Tasks;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared;
using Tessa.UI.Notifications;

namespace Tessa.Extensions.Default.Client.Workflow.KrCompilers
{
    public sealed class KrReplaceRecalcMessageStoreExtension: CardStoreExtension
    {
        private readonly INotificationUIManager notificationManager;

        public KrReplaceRecalcMessageStoreExtension(
            INotificationUIManager notificationManager)
        {
            this.notificationManager = notificationManager;
        }

        public override Task AfterRequest(
            ICardStoreExtensionContext context)
        {
            if (context.ValidationResult.Count == 1
                && context.ValidationResult.RemoveAll(DefaultValidationKeys.RecalcWithoutChanges) == 1)
            {
                _ = this.notificationManager.ShowTextOrMessageBoxAsync(DefaultValidationKeys.RecalcWithoutChanges.Message);
            }

            return Task.CompletedTask;
        }
    }
}