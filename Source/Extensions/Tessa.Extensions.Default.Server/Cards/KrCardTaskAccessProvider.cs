using System;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Server.Workflow.KrPermissions;
using Tessa.Extensions.Default.Shared.Workflow.KrPermissions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Cards
{
    /// <summary>
    /// Реализация <see cref="CardTaskAccessProvider"/> с проверкой прав доступа на редактирование списка связанных с заданием ролей.
    /// </summary>
    public class KrCardTaskAccessProvider : CardTaskAccessProvider
    {
        #region Fields

        private readonly IKrPermissionsManager permissionsManager;

        #endregion

        #region Constructors

        public KrCardTaskAccessProvider(IKrPermissionsManager permissionsManager)
        {
            this.permissionsManager = permissionsManager;
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc />
        protected override async ValueTask<bool> IsCantUpdateTaskAssignedRolesAsync(
            CardTask task,
            Card card,
            IValidationResultBuilder validationResult,
            Func<bool> checkFunction,
            ICardExtensionContext context = null,
            CancellationToken cancellationToken = default)
        {
            if (!checkFunction())
            {
                return false;
            }

            KrToken krToken = KrToken.TryGet(card.Info);
            KrToken serverToken = null;
            if (context != null)
            {
                serverToken = context.Info.TryGetServerToken();
            }

            var permissionsManagerContext = await permissionsManager.TryCreateContextAsync(
                new KrPermissionsCreateContextParams
                {
                    Card = card,
                    IsStore = true,
                    WithExtendedPermissions = true,
                    ValidationResult = validationResult,
                    AdditionalInfo = context?.Info,
                    PrevToken = krToken,
                    ServerToken = serverToken,
                    ExtensionContext = context,
                },
                cancellationToken: context == null ? cancellationToken : context.CancellationToken);

            if (permissionsManagerContext == null)
            {
                ValidationSequence
                    .Begin(validationResult)
                    .SetObjectName(typeof(KrCardTaskAccessProvider))
                    .ErrorText("$KrMessages_PermissionManagerContextRequired")
                    .End();
                return true;
            }

            var permissionsCheckResultOwn = await permissionsManager.CheckRequiredPermissionsAsync(
                permissionsManagerContext,
                KrPermissionFlagDescriptors.ModifyOwnTaskAssignedRoles);

            if (task.TaskSessionRoles.Count != 0 && permissionsCheckResultOwn.Result)
            {
                return false;
            }

            var permissionsCheckResultAll = await permissionsManager.CheckRequiredPermissionsAsync(
                permissionsManagerContext,
                KrPermissionFlagDescriptors.ModifyAllTaskAssignedRoles);

            return !permissionsCheckResultAll.Result;
        }

        #endregion
    }
}
