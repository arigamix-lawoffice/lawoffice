using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Server.Cards.Helpers;
using Tessa.Extensions.Shared.Info;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Server.Cards.Law
{
    /// <summary>
    ///     Серверное расширение для получения виртуальной карточки дела.
    /// </summary>
    public sealed class LawCaseGetExtension : CardGetExtension
    {
        #region Fields

        private readonly ICardRepository cardRepository;
        private readonly LawCaseHelper lawCaseHelper;

        #endregion

        #region Constructors

        public LawCaseGetExtension(
            ICardRepository cardRepository,
            LawCaseHelper lawCaseHelper)
        {
            this.cardRepository = cardRepository ?? throw new ArgumentNullException(nameof(cardRepository));
            this.lawCaseHelper = lawCaseHelper ?? throw new ArgumentNullException(nameof(lawCaseHelper));
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc />
        public override async Task BeforeRequest(ICardGetExtensionContext context)
        {
            var nullableCardID = context.Request.CardID;

            if (!nullableCardID.HasValue)
            {
                ValidationSequence
                    .Begin(context.ValidationResult)
                    .SetObjectName(this)
                    .Error(CardValidationKeys.UnspecifiedCardID)
                    .End();

                return;
            }

            var caseID = nullableCardID.Value;
            var card = await this.TryCreateCardAsync(context.ValidationResult, context.CancellationToken);

            if (card is null)
            {
                return;
            }

            card.ID = caseID;

            // С несуществующей карточкой, у которой версия > 0, ничего нельзя сделать
            // (т.е. не будет диалога сохранения при закрытии, который был при любом закрытии).
            card.Version = 1;

            await this.lawCaseHelper.FillCaseCardFieldsAsync(card, caseID, context.ValidationResult, context.CancellationToken);

            context.Response = new CardGetResponse { Card = card };
            context.Response.SetForbidStoringHistory(true);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Попытка получения карточки.
        /// </summary>
        /// <param name="validationResult"><see cref="IValidationResultBuilder"/></param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns>Карточка. Если не удалось получить, то null.</returns>
        private async Task<Card?> TryCreateCardAsync(
            IValidationResultBuilder validationResult,
            CancellationToken cancellationToken = default)
        {
            var request = new CardNewRequest
            {
                CardTypeID = TypeInfo.LawCase.ID,
                Info = new Dictionary<string, object?> { { InfoMarks.IsGetRequest, true } }
            };

            var response = await this.cardRepository.NewAsync(request, cancellationToken);
            validationResult.Add(response.ValidationResult);

            return validationResult.IsSuccessful()
                ? response.Card
                : null;
        }

        #endregion
    }
}