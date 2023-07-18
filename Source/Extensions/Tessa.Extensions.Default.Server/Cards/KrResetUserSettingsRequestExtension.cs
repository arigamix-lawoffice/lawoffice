using System;
using System.Threading.Tasks;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Server.Workflow.KrPermissions;
using Tessa.Extensions.Default.Shared.Workflow.KrPermissions;
using Tessa.Extensions.Platform.Shared.Initialization;
using Tessa.Platform.Runtime;
using Tessa.Roles;

namespace Tessa.Extensions.Default.Server.Cards
{
    public sealed class KrResetUserSettingsRequestExtension : CardRequestExtension
    {
        #region Fields

        private readonly IRoleTypePermissionsManager permissionsManager;
        private readonly IKrPermissionsManager krPermissionsManager;

        #endregion

        #region Constructors

        public KrResetUserSettingsRequestExtension(
            IRoleTypePermissionsManager permissionsManager,
            IKrPermissionsManager krPermissionsManager)
        {
            this.permissionsManager = permissionsManager;
            this.krPermissionsManager = krPermissionsManager;
        }

        #endregion

        #region Base Overrides

        public override async Task BeforeRequest(ICardRequestExtensionContext context)
        {
            if (!context.Request.CardID.HasValue)
            {
                return;
            }

            Guid cardID = context.Request.CardID.Value;
            IUser currentUser = context.Session.User;

            if (currentUser.IsAdministrator()
                 || currentUser.ID == cardID
                 || !await this.permissionsManager.RoleTypeUseCustomPermissionsAsync(RoleHelper.PersonalRoleTypeID, context.CancellationToken))
            {
                return;
            }

            var checkCard = new Tessa.Cards.Card()
            {
                ID = cardID,
                TypeID = RoleHelper.PersonalRoleTypeID,
                Version = 1,
            };
            checkCard.Sections
                .GetOrAdd(InitializationExtensionHelper.PersonalRoleVirtualSection)
                .RawFields[InitializationExtensionHelper.PersonalRoleSettingsField] = string.Empty;

            var permissionsContext = await krPermissionsManager.TryCreateContextAsync(
                new KrPermissionsCreateContextParams
                {
                    Card = checkCard,
                    CardTypeID = RoleHelper.PersonalRoleTypeID,
                    AdditionalInfo = context.Info,
                    ExtensionContext = context,
                    ServerToken = context.Info.TryGetServerToken(),
                    ValidationResult = context.ValidationResult,
                    WithExtendedPermissions = true,
                    IsStore = true,
                }, cancellationToken: context.CancellationToken);

            if (permissionsContext != null)
            {
                await krPermissionsManager.CheckRequiredPermissionsAsync(permissionsContext, KrPermissionFlagDescriptors.EditCard);
            }
        }

        #endregion
    }
}
