using System;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.ComponentModel;
using Tessa.Extensions.Default.Server.Workflow.KrPermissions;
using Tessa.Extensions.Default.Server.Workflow.KrProcess;
using Tessa.Extensions.Default.Shared.Notices;
using Tessa.Extensions.Default.Shared.Workflow.KrPermissions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Data;
using Tessa.Platform.Runtime;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Notices
{
    ///<inheritdoc/>
    public sealed class KrNotificationSubscriptionPermissionManagerServer : KrNotificationSubscriptionPermissionManager
    {
        #region Fields

        private readonly IDbScope dbScope;
        private readonly IKrTokenProvider krTokenProvider;
        private readonly IKrPermissionsManager permissionsManager;
        private readonly IKrPermissionsCacheContainer permissionsCacheContainer;

        private static readonly KrPermissionFlagDescriptor[] notificationPermissions
            = new KrPermissionFlagDescriptor[] { KrPermissionFlagDescriptors.SubscribeForNotifications };

        #endregion

        #region Constructors

        public KrNotificationSubscriptionPermissionManagerServer(
            ISession session,
            IKrTypesCache typesCache,
            IDbScope dbScope,
            IKrTokenProvider krTokenProvider,
            IKrPermissionsManager permissionsManager,
            IKrPermissionsCacheContainer permissionsCacheContainer)
            : base(session, typesCache)
        {
            this.dbScope = dbScope;
            this.krTokenProvider = krTokenProvider;
            this.permissionsManager = permissionsManager;
            this.permissionsCacheContainer = permissionsCacheContainer;
        }

        #endregion

        #region Base Overrides

        ///<inheritdoc/>
        public override async ValueTask<bool> CheckAccessAsync(
            Guid cardID,
            IValidationResultBuilder validationResult = null,
            CancellationToken cancellationToken = default)
        {
            var permContext = await this.permissionsManager.TryCreateContextAsync(
                new KrPermissionsCreateContextParams
                {
                    CardID = cardID,
                    ValidationResult = validationResult,
                },
                cancellationToken);

            if (permContext is null)
            {
                return false;
            }

            return
                await this.permissionsManager.CheckRequiredPermissionsAsync(
                    permContext,
                    notificationPermissions);
        }

        ///<inheritdoc/>
        public override async ValueTask<bool> CheckAccessAsync(
            Card card,
            IValidationResultBuilder validationResult = null,
            CancellationToken cancellationToken = default)
        {
            var permContext = await this.permissionsManager.TryCreateContextAsync(
                new KrPermissionsCreateContextParams
                {
                    Card = card,
                    CardTypeID = await this.GetCardTypeIDAsync(card.ID, cancellationToken),
                    ValidationResult = validationResult,
                    PrevToken = KrToken.TryGet(card.Info),
                },
                cancellationToken);

            if (permContext != null)
            {
                return
                    await this.permissionsManager.CheckRequiredPermissionsAsync(
                        permContext,
                        notificationPermissions);
            }
            return await base.CheckAccessAsync(card, validationResult, cancellationToken);
        }

        ///<inheritdoc/>
        public override async ValueTask SetAccessAsync(Card card, CancellationToken cancellationToken = default)
        {
            this.krTokenProvider
                .CreateToken(
                    card.ID,
                    CardComponentHelper.DoNotCheckVersion,
                    await this.permissionsCacheContainer.GetVersionAsync(cancellationToken),
                    notificationPermissions)
                .Set(card.Info);
        }

        #endregion

        #region Private Methods

        private async Task<Guid> GetCardTypeIDAsync(Guid cardID, CancellationToken cancellationToken = default)
        {
            await using (this.dbScope.Create())
            {
                var query = this.dbScope.BuilderFactory
                    .Select()
                    .C("TypeID")
                    .From("Instances").NoLock()
                    .Where().C("ID").Equals().P("CardID")
                    .Build();
                return await this.dbScope.Db.SetCommand(
                        query,
                        this.dbScope.Db.Parameter("CardID", cardID))
                    .LogCommand()
                    .ExecuteAsync<Guid>(cancellationToken);
            }
        }

        #endregion
    }
}