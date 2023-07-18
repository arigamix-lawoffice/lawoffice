using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess
{
    /// <summary>
    /// Расширение для инициализации карточки при создании на основании другой карточки.
    /// </summary>
    public sealed class KrCreateBasedOnNewExtension :
        CardNewExtension
    {
        #region Constructors

        public KrCreateBasedOnNewExtension(
            ICardRepository extendedRepository,
            IKrCreateBasedOnHandler createBasedOnHandler)
        {
            this.extendedRepository = extendedRepository;
            this.createBasedOnHandler = createBasedOnHandler;
        }

        #endregion

        #region Fields

        private readonly ICardRepository extendedRepository;

        private readonly IKrCreateBasedOnHandler createBasedOnHandler;

        #endregion

        #region Base Overrides

        public override async Task AfterRequest(ICardNewExtensionContext context)
        {
            Card newCard;
            Dictionary<string, object> info;

            if (context.CardType == null
                || context.CardType.InstanceType != CardInstanceType.Card
                || context.CardType.Flags.Has(CardTypeFlags.Singleton)
                || !context.RequestIsSuccessful
                || (info = context.Request.TryGetInfo()) == null
                || !context.ValidationResult.IsSuccessful()
                || (newCard = context.Response.TryGetCard()) == null)
            {
                return;
            }

            Card baseCard;
            if (info.TryGet<object>(KrCreateBasedOnHelper.CardKey) is Dictionary<string, object> baseCardStorage)
            {
                baseCard = new Card(baseCardStorage);
            }
            else
            {
                var baseCardID = info.TryGet<Guid?>(KrCreateBasedOnHelper.CardIDKey);
                if (!baseCardID.HasValue)
                {
                    return;
                }

                var getRequest = new CardGetRequest
                {
                    CardID = baseCardID,
                    GetMode = CardGetMode.ReadOnly,
                    RestrictionFlags =
                        CardGetRestrictionFlags.RestrictTaskCalendar
                        | CardGetRestrictionFlags.RestrictTaskHistory,
                };

                var baseTokenStorage = info.TryGet<Dictionary<string, object>>(KrCreateBasedOnHelper.TokenKey);
                KrToken baseToken = baseTokenStorage != null ? KrToken.TryGet(baseTokenStorage) : null;

                if (baseToken != null)
                {
                    baseToken.Set(getRequest.Info);
                }

                CardGetResponse getResponse = await this.extendedRepository.GetAsync(getRequest, context.CancellationToken);
                context.ValidationResult.Add(getResponse.ValidationResult);

                if (!getResponse.ValidationResult.IsSuccessful())
                {
                    return;
                }

                baseCard = getResponse.Card;
            }

            context.Info[KrCreateBasedOnHelper.CardInfoKey] = baseCard;

            ValidationResult infoResult = await this.createBasedOnHandler.CopyInfoAsync(baseCard, newCard, context.CancellationToken);
            context.ValidationResult.Add(infoResult);

            if (!infoResult.IsSuccessful)
            {
                return;
            }

            bool copyFiles = info.TryGet<bool>(KrCreateBasedOnHelper.CopyFilesKey);
            if (copyFiles)
            {
                // файлы копируются как псевдосоздание по шаблону, и у карточки newCard к этому моменту
                // должна быть корректная структура, в т.ч. должен быть задан непустой идентификатор карточки,
                // причём сам идентификатор при копировании не используется, но он влияет на валидацию структуры

                // поэтому если идентификатор пустой, то мы его создаём и по завершении копирования сбрасываем

                ValidationResult result = await this.createBasedOnHandler.CopyFilesAsync(baseCard, newCard, cancellationToken: context.CancellationToken);
                context.ValidationResult.Add(result);
            }
        }

        #endregion
    }
}
