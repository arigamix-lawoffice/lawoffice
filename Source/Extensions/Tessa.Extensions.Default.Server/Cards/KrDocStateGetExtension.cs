using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared;
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using Tessa.Scheme;
using Unity;

namespace Tessa.Extensions.Default.Server.Cards
{
    /// <summary>
    /// Расширение на получение виртуальной карточки состояния документа.
    /// </summary>
    public sealed class KrDocStateGetExtension :
        CardGetExtension
    {
        #region Constructors

        public KrDocStateGetExtension(
            [Dependency(CardRepositoryNames.DefaultWithoutTransactionAndLocking)]
            ICardRepository cardRepository,
            IConfigurationInfoProvider configurationInfoProvider,
            [OptionalDependency] ISchemeService schemeService = null)
        {
            this.cardRepository = NotNullOrThrow(cardRepository);
            this.configurationInfoProvider = NotNullOrThrow(configurationInfoProvider);
            this.schemeService = schemeService;
        }

        #endregion

        #region Fields

        private readonly ICardRepository cardRepository;

        private readonly IConfigurationInfoProvider configurationInfoProvider;
        
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

        public override async Task BeforeRequest(ICardGetExtensionContext context)
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

            int? stateID = context.Request.TryGetInfo()?.TryGet<int?>(DefaultExtensionHelper.StateIDKey);
            if (!stateID.HasValue)
            {
                ValidationSequence
                    .Begin(context.ValidationResult)
                    .SetObjectName(this)
                    .Error(CardValidationKeys.UnspecifiedCardID)
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

            Guid cardID = DefaultExtensionHelper.GetKrDocStateCardID(stateID.Value);

            SchemeRecord record = table.Records.FirstOrDefault(x => (short) x["ID"] == stateID.Value);
            if (record is null)
            {
                context.ValidationResult.AddInstanceNotFoundError(
                    this,
                    this.configurationInfoProvider.GetFlags(),
                    cardID);

                return;
            }

            Card card = await this.TryCreateCardAsync(context.ValidationResult, context.CancellationToken);
            if (card is null)
            {
                // сообщение об ошибке уже присутствует в context.ValidationResult
                return;
            }

            Dictionary<string, object> fields = card.Sections["KrDocStateVirtual"].RawFields;
            fields["StateID"] = stateID.Value;
            fields["Name"] = (string) record["Name"];

            SchemePartition partition = record.Partition;
            fields["PartitionID"] = partition?.ID;
            fields["PartitionName"] = partition?.Name;

            card.ID = cardID;
            card.Version = 1; // с несуществующей карточкой, у которой версия > 0, ничего нельзя сделать

            DateTime utcNow = DateTime.UtcNow;
            card.Created = utcNow;
            card.Modified = utcNow;

            IUser user = context.Session.User;
            card.CreatedByID = user.ID;
            card.ModifiedByID = user.ID;
            card.CreatedByName = user.Name;
            card.ModifiedByName = user.Name;

            card.Info[DefaultExtensionHelper.StateIDKey] = stateID.Value;

            context.Response = new CardGetResponse { Card = card };
        }

        #endregion
    }
}
