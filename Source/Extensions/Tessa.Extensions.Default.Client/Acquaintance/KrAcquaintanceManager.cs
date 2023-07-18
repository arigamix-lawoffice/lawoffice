using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Acquaintance;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Client.Acquaintance
{
    public sealed class KrAcquaintanceManager : IKrAcquaintanceManager
    {
        #region Fields

        private readonly ICardRepository cardRepository;

        #endregion

        #region Constructors

        public KrAcquaintanceManager(ICardRepository cardRepository)
        {
            this.cardRepository = cardRepository;
        }

        #endregion

        #region IKrAcquaintanceManager Implementation

        public async Task<ValidationResult> SendAsync(
            Guid mainCardID,
            IReadOnlyList<Guid> roleIDList,
            bool excludeDeputies = false,
            string comment = null,
            string placeholderAliases = null,
            Dictionary<string, object> info = null,
            Guid? notificationCardID = null,
            Guid? senderID = null,
            bool addSuccessMessage = false,
            CancellationToken cancellationToken = default)
        {
            // Запрос на отправку данных для массового ознакомления
            var request = new CardRequest
            {
                CardID = mainCardID,
                RequestType = DefaultRequestTypes.Acquaintance,
            };

            AcquaintanceHelper.SetAcquaintanceInfo(
                request,
                roleIDList,
                comment,
                excludeDeputies,
                placeholderAliases,
                info,
                addSuccessMessage);

            CardResponse response = await cardRepository.RequestAsync(request, cancellationToken).ConfigureAwait(false);
            return response.ValidationResult.Build();
        }

        #endregion
    }
}
