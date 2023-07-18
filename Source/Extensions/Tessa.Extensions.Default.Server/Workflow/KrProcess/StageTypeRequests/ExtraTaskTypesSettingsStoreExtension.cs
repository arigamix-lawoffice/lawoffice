using System.Threading.Tasks;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.StageTypeRequests
{
    public sealed class ExtraTaskTypesSettingsStoreExtension : CardStoreExtension
    {
        private readonly IKrProcessContainer krProcessContainer;

        public ExtraTaskTypesSettingsStoreExtension(
            IKrProcessContainer krProcessContainer)
        {
            this.krProcessContainer = krProcessContainer;
        }

        /// <inheritdoc />
        public override Task AfterRequest(
            ICardStoreExtensionContext context)
        {
            if (context.ValidationResult.IsSuccessful()
                && context
                    .Request
                    .TryGetCard()
                    ?.TryGetSections()
                    ?.ContainsKey(KrConstants.KrSettingsRouteExtraTaskTypes.Name) == true)
            {
                this.krProcessContainer.ResetExtraTaskTypes();
            }

            return Task.CompletedTask;
        }
    }
}