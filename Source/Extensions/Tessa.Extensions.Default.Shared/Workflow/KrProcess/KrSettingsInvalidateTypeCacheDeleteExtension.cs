using System;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Cards.Metadata;
using Tessa.Platform.Runtime;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess
{
    public sealed class KrSettingsInvalidateTypeCacheDeleteExtension :
        CardDeleteExtension
    {
        #region Constructors

        public KrSettingsInvalidateTypeCacheDeleteExtension(
            ICardCachedMetadata cardCachedMetadata,
            IKrTypesCache cache,
            ISession session)
        {
            // конструктор для вызова на клиенте

            this.cardCachedMetadata = cardCachedMetadata;
            this.cache = cache;
            this.session = session;
        }


        public KrSettingsInvalidateTypeCacheDeleteExtension(
            CardMetadataCache cardMetadataCache,
            IKrTypesCache cache,
            ISession session)
        {
            // конструктор для вызова на сервере

            this.cardMetadataCache = cardMetadataCache;
            this.cache = cache;
            this.session = session;
        }

        #endregion

        #region Fields

        private readonly ICardCachedMetadata cardCachedMetadata;

        private readonly CardMetadataCache cardMetadataCache;

        private readonly IKrTypesCache cache;

        private readonly ISession session;

        #endregion

        #region Private Methods

        private Task InvalidateMetadataCacheAsync(CancellationToken cancellationToken = default)
        {
            switch (this.session.Type)
            {
                case SessionType.Client:
                    return this.cardCachedMetadata.InvalidateAsync(cancellationToken);

                case SessionType.Server:
                    return this.cardMetadataCache.InvalidateGlobalAsync(cancellationToken);

                default:
                    throw new ArgumentOutOfRangeException("session.Type");
            }
        }

        #endregion

        #region Base Overrides

        public override async Task AfterRequest(ICardDeleteExtensionContext context)
        {
            if (!context.RequestIsSuccessful)
            {
                return;
            }

            await this.InvalidateMetadataCacheAsync(context.CancellationToken).ConfigureAwait(false);
            await this.cache.InvalidateAsync(true, true, context.CancellationToken).ConfigureAwait(false);
        }

        #endregion
    }
}
