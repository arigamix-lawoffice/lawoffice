using System;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Platform.Conditions;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Cards
{
    /// <summary>
    /// Расширение на создание карточки виртуального файла
    /// Генерирует FileID и FileVersionID
    /// </summary>
    public sealed class KrVirtualFileNewGetExtension : CardNewGetExtension
    {
        #region Fields

        private readonly ICardMetadata cardMetadata;
        private readonly IConditionTypesProvider conditionTypesProvider;

        #endregion

        #region Constructors

        public KrVirtualFileNewGetExtension(
            ICardMetadata cardMetadata,
            IConditionTypesProvider conditionTypesProvider)
        {
            this.cardMetadata = NotNullOrThrow(cardMetadata);
            this.conditionTypesProvider = NotNullOrThrow(conditionTypesProvider);
        }

        #endregion

        #region Base Overrides

        public override async Task AfterRequest(ICardNewExtensionContext context)
        {
            if (!context.RequestIsSuccessful
                || (context.Method == CardNewMethod.Template
                    && !context.Request.Info.TryGet<bool>(CardHelper.CopyingCardKey)))
            {
                return;
            }

            var card = context.Response.Card;
            card.ID = Guid.NewGuid();

            var mainSection = card.Sections.GetOrAdd("KrVirtualFiles");
            mainSection.RawFields["FileID"] = card.ID;
            mainSection.RawFields["FileVersionID"] = Guid.NewGuid();

            var versionsSection = card.Sections.GetOrAddTable("KrVirtualFileVersions");
            foreach(var row in versionsSection.Rows)
            {
                row["FileVersionID"] = row.RowID;
            }

            await this.DeserializeConditionsAsync(card, context.CancellationToken);
        }

        public override async Task AfterRequest(ICardGetExtensionContext context)
        {
            if (!context.RequestIsSuccessful)
            {
                return;
            }

            var card = context.Response.Card;
            await this.DeserializeConditionsAsync(card, context.CancellationToken);
        }

        #endregion

        #region Private Methods

        private async ValueTask DeserializeConditionsAsync(Card card, CancellationToken cancellationToken = default)
        {
            await ConditionHelper.DeserializeConditionsToEntrySectionAsync(
                card,
                this.cardMetadata,
                this.conditionTypesProvider,
                "KrVirtualFiles",
                "Conditions",
                card.StoreMode == CardStoreMode.Insert,
                cancellationToken);
        }

        #endregion
    }
}
