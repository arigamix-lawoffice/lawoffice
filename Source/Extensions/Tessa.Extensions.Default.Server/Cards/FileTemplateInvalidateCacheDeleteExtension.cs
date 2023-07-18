using System.Threading.Tasks;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Server.Files.VirtualFiles;

namespace Tessa.Extensions.Default.Server.Cards
{
    public sealed class FileTemplateInvalidateCacheDeleteExtension : CardDeleteExtension
    {
        #region Fields

        private readonly IKrVirtualFileCache virtualFileCache;

        private bool wasInTransaction;

        #endregion

        #region Constructors

        public FileTemplateInvalidateCacheDeleteExtension(IKrVirtualFileCache virtualFileCache)
        {
            this.virtualFileCache = virtualFileCache;
        }

        #endregion

        #region Base Overrides

        public override Task BeforeCommitTransaction(ICardDeleteExtensionContext context)
        {
            this.wasInTransaction = true;
            return Task.CompletedTask;
        }

        public override async Task AfterRequest(ICardDeleteExtensionContext context)
        {
            if (!context.RequestIsSuccessful || !this.wasInTransaction)
            {
                return;
            }

            await this.virtualFileCache.InvalidateAsync(context.CancellationToken);
        }

        #endregion
    }
}
