using System.Threading.Tasks;
using Tessa.Cards.Extensions;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess
{
    public class KrDocTypeInvalidateSettingsCacheStoreExtension : CardStoreExtension
    {
        private readonly IKrTypesCache cache;
        public KrDocTypeInvalidateSettingsCacheStoreExtension(IKrTypesCache cache)
        {
            this.cache = cache;
        }

        public override Task AfterRequest(ICardStoreExtensionContext context)
        {
            if (!context.RequestIsSuccessful)
            {
                return Task.CompletedTask;
            }

            return this.cache.InvalidateAsync(false, true, context.CancellationToken);
        }
    }
}
