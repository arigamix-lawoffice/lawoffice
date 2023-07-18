using System.Collections.Generic;
using System.Threading.Tasks;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess
{
    public sealed class KrGetDocTypesCardExtension : CardRequestExtension
    {
        private readonly IKrTypesCache typesCache;
        public KrGetDocTypesCardExtension(IKrTypesCache typesCache)
        {
            this.typesCache = typesCache;
        }

        public override async Task AfterRequest(ICardRequestExtensionContext context)
        {
            IReadOnlyList<KrDocType> docTypes = await typesCache.GetDocTypesAsync(context.CancellationToken);

            foreach (KrDocType doctype in docTypes)
            {
                context.Response.Info.Add("KrDocTypes_" + doctype.ID, doctype.GetStorage());
            }
        }
    }
}
