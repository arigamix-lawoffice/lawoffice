using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Caching;
using Tessa.Cards.Extensions;
using Tessa.Cards.Metadata;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Data;

namespace Tessa.Extensions.Default.Client.Workflow.KrProcess
{
    /// <summary>
    /// Клиентское расширение метаданных типового решения.
    /// Добавляет во все типы карточек типового решения данные из настроенного типа KrCard.
    /// </summary>
    public class KrCardMetadataExtension : KrCardMetadataExtensionBase
    {
        private readonly ICardCache clientCache;

        /// <inheritdoc />
        public KrCardMetadataExtension(ICardMetadata clientCardMetadata, ICardCache clientCache)
            : base(clientCardMetadata)
        {
            this.clientCache = clientCache;
        }

        /// <inheritdoc />
        public KrCardMetadataExtension(IDbScope dbScope)
            : base()
        {
            throw new InvalidOperationException("This constructor only for server-side.");
        }

        /// <inheritdoc />
        protected override Task ExtendKrTypesAsync(
            IList<CardType> krTypes,
            ICardMetadataExtensionContext context) =>
            Task.CompletedTask;

        /// <inheritdoc />
        protected override ValueTask<CardMetadataSectionCollection> GetAllSectionsAsync(ICardMetadataExtensionContext context) =>
            this.ClientCardMetadata.GetSectionsAsync(context.CancellationToken);

        /// <inheritdoc />
        protected override async Task<List<Guid>> GetCardTypeIDsAsync(CancellationToken cancellationToken = default)
        {
            if (!await this.clientCache.Cards.IsAllowedAsync(KrConstants.KrSettings.Name, cancellationToken).ConfigureAwait(false))
            {
                return null;
            }

            var krSettings = await this.clientCache.Cards.GetAsync(KrConstants.KrSettings.Name, cancellationToken).ConfigureAwait(false);
            if (!krSettings.IsSuccess)
            {
                return null;
            }

            return krSettings
                    .GetValue()
                    .Sections["KrSettingsCardTypes"]
                    .Rows
                    .Where(x => x.State == CardRowState.None
                            && x.Get<Guid>(KrConstants.KrSettingsCardTypes.CardTypeID) != DefaultCardTypes.KrCardTypeID)
                    .Select(x => x.Get<Guid>(KrConstants.KrSettingsCardTypes.CardTypeID))
                    .Distinct()
                    .ToList();
        }
    }
}
