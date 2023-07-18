#nullable enable

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Extensions.Default.Server.Workflow.KrPermissions;
using Tessa.Extensions.Default.Server.Workflow.KrProcess;
using Tessa.Extensions.Default.Shared.Workflow.KrPermissions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Data;
using Tessa.Platform.Runtime;
using Tessa.Platform.Validation;
using Tessa.Tags;

namespace Tessa.Extensions.Default.Shared.Tags
{
    /// <summary>
    /// <inheritdoc cref="ITagPermissionsManager" path="/summary"/> Серверная реализация для типового решения.
    /// </summary>
    public class KrTagPermissionsManager : TagPermissionsManager
    {
        #region Constructors

        public KrTagPermissionsManager(
            IDbScope dbScope,
            ISession session,
            IKrPermissionsManager permissionsManager,
            IKrTokenProvider krTokenProvider,
            ICardRepository cardRepository) : base(dbScope, session)
        {
            this.permissionsManager = NotNullOrThrow(permissionsManager);
            this.krTokenProvider = NotNullOrThrow(krTokenProvider);
            this.cardRepository = NotNullOrThrow(cardRepository);
        }

        #endregion

        #region Fields

        private readonly ICardRepository cardRepository;
        private readonly IKrPermissionsManager permissionsManager;
        private readonly IKrTokenProvider krTokenProvider;
        private static readonly KrPermissionFlagDescriptor[] readCardPermissions
            = new KrPermissionFlagDescriptor[] { KrPermissionFlagDescriptors.ReadCard };

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override async ValueTask<bool> CanAddTagAsync(
            Guid cardID,
            Guid tagID,
            Dictionary<string, object?>? tokenInfo,
            IValidationResultBuilder result,
            CancellationToken cancellationToken = default)
        {
            if (this.session.User.IsAdministrator())
            {
                return true;
            }

            var hasPermissionToStoreCard = await this.CheckPermissionToReadCardAsync(cardID, tokenInfo, result, cancellationToken);
            if (!hasPermissionToStoreCard)
            {
                result.AddError(this, "$Tags_Validators_AddToCardPermission_Denied");
                return false;
            }
            return await base.CanAddTagAsync(cardID, tagID, tokenInfo, result, cancellationToken);
        }

        /// <inheritdoc/>
        public override async ValueTask<bool> CanDeleteTagAsync(
            Guid cardID,
            Guid tagID,
            Dictionary<string, object?>? tokenInfo,
            IValidationResultBuilder result,
            CancellationToken cancellationToken = default)
        {
            if (this.session.User.IsAdministrator())
            {
                return true;
            }

            if (await this.IsTagSetByUserAsync(cardID, tagID, cancellationToken))
            {
                return true;
            }

            var hasPermissionToStoreCard = await this.CheckPermissionToReadCardAsync(cardID, tokenInfo, result, cancellationToken);
            if (!hasPermissionToStoreCard)
            {
                result.AddError(this, "$Tags_Validators_DeleteFromCardPermission_Denied");
                return false;
            }
            return await base.CanDeleteTagAsync(cardID, tagID, tokenInfo, result, cancellationToken);
        }

        #endregion

        #region Private Methods

        private async ValueTask<bool> CheckPermissionToReadCardAsync(
            Guid cardID,
            Dictionary<string, object?>? tokenInfo,
            IValidationResultBuilder result,
            CancellationToken cancellationToken = default)
        {
            var krToken = 
                tokenInfo is not null 
                    ? KrToken.TryGet(tokenInfo) 
                    : null; 

            // Если токен невалидный, проверим разрешение на сохранение карточки.
            var permContext = await this.permissionsManager.TryCreateContextAsync(
                new KrPermissionsCreateContextParams
                {
                    CardID = cardID,
                    ValidationResult = result,
                    PrevToken = krToken,
                },
                cancellationToken);

            if (permContext is null)
            {
                // Считаем, что карточка не относится к типовому решению, и не проводим проверку.
                return true;
            }

            var checkResult = await this.permissionsManager.CheckRequiredPermissionsAsync(permContext, readCardPermissions);
            return checkResult.Result;
        }

        #endregion
    }
}
