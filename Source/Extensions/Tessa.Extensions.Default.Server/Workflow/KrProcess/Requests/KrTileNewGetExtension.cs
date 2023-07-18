using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.KrPermissions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;
using Tessa.Platform.Data;
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using Tessa.Roles;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Requests
{
    /// <summary>
    /// Вычисляем видимость плиток типового решения на сервере
    /// при создании карточек, добавленных в типовое решение.
    /// </summary>
    /// <remarks>
    /// Разрешения <see cref="Card.Permissions"/> рассчитываются в расширениях с порядком
    /// <see cref="ExtensionStage.BeforePlatform"/> и <see cref="ExtensionStage.Platform"/>.
    /// </remarks>
    public sealed class KrTileNewGetExtension :
        CardNewGetExtension
    {
        #region Constructors

        public KrTileNewGetExtension(IKrTypesCache krTypesCache,
            ISession session)
        {
            this.krTypesCache = krTypesCache;
            this.session = session;
        }

        #endregion

        #region Fields

        private readonly IKrTypesCache krTypesCache;

        private readonly ISession session;

        #endregion

        #region private

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Card TryGetCard(
            CardResponseBase response)
        {
            switch (response)
            {
                case CardGetResponse cardGetResponse:
                    return cardGetResponse.Card;
                case CardNewResponse cardNewResponse:
                    return cardNewResponse.Card;
            }

            return null;
        }

        private async Task AfterRequestInternalAsync(
            CardType cardType,
            Dictionary<string, object> requestInfo,
            CardResponseBase response,
            IValidationResultBuilder validationResult,
            IDbScope dbScope,
            bool cardIsNew,
            CancellationToken cancellationToken = default)
        {
            Card card;
            IKrType krType;

            // карточка PersonalRoleSatellite может загружаться в т.ч. в TessaAdmin при старте приложения,
            // поэтому фильтруем тип карточки перед тем, как выполнение дойдёт до KrWorkflowHelper.TryGetKrType

            if (!validationResult.IsSuccessful()
                || cardType == null
                || (cardType.Flags & (CardTypeFlags.Administrative | CardTypeFlags.Singleton)) != 0
                || RoleHelper.IsRole(cardType.ID)
                || cardType.ID == RoleHelper.PersonalRoleSatelliteTypeID
                || (card = TryGetCard(response)) == null
                || (krType = await KrProcessSharedHelper.TryGetKrTypeAsync(this.krTypesCache, card, cardType.ID, cancellationToken: cancellationToken)) == null)
            {
                return;
            }

            int state = (int)(await KrProcessSharedHelper.GetKrStateAsync(card, cancellationToken: cancellationToken) ?? KrState.Draft);

            bool isUserAdministrator = this.session.User.IsAdministrator();
            bool canShowHiddenStages =
                !cardIsNew
                && isUserAdministrator
                && !krType.UseRoutesInWorkflowEngine
                && card.HasHiddenStages();

            // если уже выполняется расчёт пермишенов кнопкой "Редактировать" CalculatePermissionsMark
            // или пермишены были вычислены ранее кнопкой "Редактировать" PermissionsCalculatedMark, то не показываем кнопку "Редактировать"

            bool canEdit =
                !cardIsNew
                && requestInfo?.TryGet<bool>(KrPermissionsHelper.CalculatePermissionsMark) != true
                && requestInfo?.TryGet<bool>(KrPermissionsHelper.PermissionsCalculatedMark) != true
                && !card.Info.TryGet<bool>(KrPermissionsHelper.PermissionsCalculatedMark)
                && KrToken.TryGet(card.Info)?.HasPermission(KrPermissionFlagDescriptors.FullCardPermissionsGroup) != true;

            bool useTasks = krType.UseResolutions && cardType.Flags.Has(CardTypeFlags.AllowTasks);

            ICardPermissionResolver permissionsResolver = card.Permissions.CreateResolver();
            bool canEditNumber = permissionsResolver.GetCardPermissions().Has(CardPermissionFlags.AllowEditNumber);
            bool canReplaceNumber = state == (int)KrState.Registered
                ? krType.AllowManualRegistrationDocNumberAssignment
                : krType.AllowManualRegularDocNumberAssignment;

            bool canShowSkippedStages = !cardIsNew
                && !isUserAdministrator
                && !krType.UseRoutesInWorkflowEngine
                && card.HasSkipStages();

            card.SetTileIsVisible(
                DefaultTileNames.KrShowHiddenStages,
                canShowHiddenStages);

            card.SetTileIsVisible(
                DefaultTileNames.KrEditMode,
                canEdit);

            card.SetTileIsVisible(
                DefaultTileNames.WfCreateResolution,
                useTasks);

            card.SetTileIsVisible(
                ButtonNames.EditNumber,
                canEditNumber);

            card.SetTileIsVisible(
                ButtonNames.ReplaceNumber,
                canEditNumber && canReplaceNumber);

            card.SetTileIsVisible(
                DefaultTileNames.KrShowSkippedStages,
                canShowSkippedStages);
        }

        #endregion

        #region Base Overrides

        public override Task AfterRequest(ICardNewExtensionContext context) =>
            this.AfterRequestInternalAsync(
                context.CardType,
                context.Request.TryGetInfo(),
                context.Response,
                context.ValidationResult,
                context.DbScope,
                true,
                context.CancellationToken);

        public override Task AfterRequest(ICardGetExtensionContext context) =>
            this.AfterRequestInternalAsync(
                context.CardType,
                context.Request.TryGetInfo(),
                context.Response,
                context.ValidationResult,
                context.DbScope,
                false,
                context.CancellationToken);

        #endregion
    }
}

