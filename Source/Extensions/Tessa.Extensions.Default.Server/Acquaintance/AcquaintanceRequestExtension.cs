using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared.Acquaintance;
using Tessa.Extensions.Default.Shared.Workflow.KrPermissions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Acquaintance
{
    public sealed class AcquaintanceRequestExtension : CardRequestExtension
    {
        #region Fields

        private readonly IKrAcquaintanceManager acquaintanceManager;

        private readonly ICardRepository cardRepository;

        #endregion

        #region Constructors

        public AcquaintanceRequestExtension(
            IKrAcquaintanceManager acquaintanceManager,
            ICardRepository cardRepository)
        {
            this.acquaintanceManager = acquaintanceManager;
            this.cardRepository = cardRepository;
        }

        #endregion

        #region Base Overrides

        public override async Task AfterRequest(ICardRequestExtensionContext context)
        {
            Guid? cardID;
            if (!context.RequestIsSuccessful
                || !(cardID = context.Request.CardID).HasValue)
            {
                return;
            }

            AcquaintanceHelper.TryGetAcquaintanceInfo(
                context.Request.Info,
                out List<Guid> roleList,
                out string comment,
                out bool excludeDeputies,
                out string placeholderAliases,
                out Dictionary<string, object> info,
                out bool addSuccessMessage);

            if (roleList == null || roleList.Count == 0)
            {
                return;
            }

            // обязательно проверяем права на загрузку карточки (в этом случае запрос Get сам выдаст ошибку)
            // и права на выдачу заданий, самый простой способ - загружаем карточку на сервере с указанием CalculateResolutionPermissionsMark
            var getRequest = new CardGetRequest
            {
                CardID = cardID,
                GetMode = CardGetMode.ReadOnly,
                RestrictionFlags = CardGetRestrictionValues.MainCardFromSatellite,
            };

            getRequest.SetForbidStoringHistory(true);
            getRequest.Info[KrPermissionsHelper.CalculateResolutionPermissionsMark] = BooleanBoxes.True;

            CardGetResponse getResponse = await this.cardRepository.GetAsync(getRequest, context.CancellationToken);

            ValidationResult getResult = getResponse.ValidationResult.Build();
            context.ValidationResult.Add(getResult);
            if (!getResult.IsSuccessful)
            {
                return;
            }

            KrToken token = KrToken.TryGet(getResponse.Card.Info);
            if (token == null || !token.HasPermission(KrPermissionFlagDescriptors.CreateResolutions))
            {
                context.ValidationResult.AddError(
                    this,
                    KrPermissionsHelper.GetNotEnoughPermissionsErrorMessage(
                        KrPermissionFlagDescriptors.CreateResolutions));

                return;
            }

            var result = await this.acquaintanceManager.SendAsync(
                cardID.Value,
                roleList,
                excludeDeputies,
                comment,
                placeholderAliases,
                info,
                addSuccessMessage: addSuccessMessage,
                cancellationToken: context.CancellationToken);

            context.ValidationResult.Add(result);
        }

        #endregion
    }
}
