using System;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Cards.Metadata;
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess
{
    public sealed class KrSettingsInvalidateTypeCacheStoreExtension :
        CardStoreExtension
    {
        #region Constructors

        public KrSettingsInvalidateTypeCacheStoreExtension(
            ICardCachedMetadata cardCachedMetadata,
            IKrTypesCache cache,
            ISession session)
        {
            // конструктор для вызова на клиенте

            this.cardCachedMetadata = cardCachedMetadata;
            this.cache = cache;
            this.session = session;
        }


        public KrSettingsInvalidateTypeCacheStoreExtension(
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

        public override async Task AfterRequest(ICardStoreExtensionContext context)
        {
            StringDictionaryStorage<CardSection> sections;
            if (!context.RequestIsSuccessful
                || (sections = context.Request.Card.TryGetSections()) == null)
            {
                return;
            }

            if (sections.TryGetValue("KrSettingsCardTypes", out CardSection cardTypesSection))
            {
                ListStorage<CardRow> rows = cardTypesSection.TryGetRows();
                if (rows != null && rows.Count > 0)
                {
                    await this.InvalidateMetadataCacheAsync(context.CancellationToken).ConfigureAwait(false);
                    await this.cache.InvalidateAsync(true, true, context.CancellationToken).ConfigureAwait(false);
                    return;
                }
            }

            if (sections.TryGetValue("KrSettings", out CardSection settings)
                && (settings.RawFields.ContainsKey("PermissionsExtensionTypeID")
                || settings.RawFields.ContainsKey("AllowManualInputAndAutoCreatePartners")))
            {
                //менялся тип, расширяющий безопасность
                await this.InvalidateMetadataCacheAsync(context.CancellationToken).ConfigureAwait(false);
            }
        }

        #endregion
    }
}
