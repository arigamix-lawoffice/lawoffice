using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Cards.Metadata;
using Tessa.Extensions.Default.Shared;
using Tessa.Platform.Collections;
using Tessa.Platform.Runtime;
using Tessa.Platform.Validation;
using Tessa.Scheme;
using Unity;

namespace Tessa.Extensions.Default.Server.Cards
{
    /// <summary>
    /// Расширение на создание виртуальной карточки состояния документа.
    /// </summary>
    public sealed class KrDocStateNewExtension :
        CardNewExtension
    {
        #region Constructors

        public KrDocStateNewExtension(
            [Dependency(CardRepositoryNames.DefaultWithoutTransactionAndLocking)]
            ICardRepository cardRepository,
            [OptionalDependency] ISchemeService schemeService = null)
        {
            this.cardRepository = cardRepository ?? throw new ArgumentNullException(nameof(cardRepository));
            this.schemeService = schemeService;
        }

        #endregion

        #region Fields

        private readonly ICardRepository cardRepository;

        private readonly ISchemeService schemeService;

        #endregion

        #region Private Methods

        private async Task<Card> TryCreateCardAsync(IValidationResultBuilder validationResult, CancellationToken cancellationToken = default)
        {
            var request = new CardNewRequest { CardTypeID = DefaultCardTypes.KrDocStateTypeID };

            CardNewResponse response = await this.cardRepository.NewAsync(request, cancellationToken);
            validationResult.Add(response.ValidationResult);

            return validationResult.IsSuccessful()
                ? response.Card
                : null;
        }

        #endregion

        #region Base Overrides

        public override async Task BeforeRequest(ICardNewExtensionContext context)
        {
            // пользователи без административных прав не должны открыть карточку, т.к. в системной информации они могут увидеть лишнее
            if (!context.Session.User.IsAdministrator())
            {
                ValidationSequence
                    .Begin(context.ValidationResult)
                    .SetObjectName(this)
                    .Error(ValidationKeys.UserIsNotAdmin)
                    .End();

                return;
            }

            if (this.schemeService is null)
            {
                // не зарегистрирован сервис схемы, остальное API карточек на месте,
                // но в этом расширении мы ничего не можем сделать
                return;
            }

            SchemeTable table = await this.schemeService.GetTableAsync("KrDocState", context.CancellationToken);
            if (table is null)
            {
                return;
            }

            Card card = await this.TryCreateCardAsync(context.ValidationResult, context.CancellationToken);
            if (card is null)
            {
                // сообщение об ошибке уже присутствует в context.ValidationResult
                return;
            }

            SealableObjectList<CardMetadataRecord> states = (await context.CardMetadata.GetEnumerationsAsync(context.CancellationToken))
                ["KrDocState"].Records;

            int stateID = states.Count > 0 ? states.Max(x => (int) x["ID"]) + 1 : 0;
            Guid cardID = DefaultExtensionHelper.GetKrDocStateCardID(stateID);

            Dictionary<string, object> fields = card.Sections["KrDocStateVirtual"].RawFields;
            fields["StateID"] = stateID;

            SchemePartition partition = table.Records.LastOrDefault()?.Partition;
            fields["PartitionID"] = partition?.ID;
            fields["PartitionName"] = partition?.Name;

            card.Info[DefaultExtensionHelper.StateIDKey] = stateID;

            card.ID = cardID;
            card.Version = 0;

            DateTime utcNow = DateTime.UtcNow;
            card.Created = utcNow;
            card.Modified = utcNow;

            IUser user = context.Session.User;
            card.CreatedByID = user.ID;
            card.ModifiedByID = user.ID;
            card.CreatedByName = user.Name;
            card.ModifiedByName = user.Name;

            context.Response = new CardNewResponse { Card = card };
        }

        #endregion
    }
}
