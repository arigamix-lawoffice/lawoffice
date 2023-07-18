using System.Threading.Tasks;
using Tessa.Cards.Extensions;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess
{
    public sealed class KrDocTypeInvalidateSettingsCacheDeleteExtension : CardDeleteExtension
    {
        private readonly IKrTypesCache cache;

        public KrDocTypeInvalidateSettingsCacheDeleteExtension(IKrTypesCache cache)
        {
            this.cache = cache;
        }

        public override Task AfterRequest(ICardDeleteExtensionContext context)
        {
            if (!context.RequestIsSuccessful)
            {
                return Task.CompletedTask;
            }

            return this.cache.InvalidateAsync(cardTypes: false, docTypes: true, cancellationToken: context.CancellationToken);
        }
    }
}
