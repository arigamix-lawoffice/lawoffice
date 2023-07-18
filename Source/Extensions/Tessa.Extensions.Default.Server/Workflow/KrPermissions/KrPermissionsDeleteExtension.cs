using System;
using System.Threading.Tasks;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared.Workflow.KrPermissions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;

namespace Tessa.Extensions.Default.Server.Workflow.KrPermissions
{
    /// <summary>
    /// Расширение должно выполняться до того, как будут удалены.
    /// </summary>
    public sealed class KrPermissionsDeleteExtension :
        CardDeleteExtension
    {
        #region Constructors

        public KrPermissionsDeleteExtension(IKrPermissionsManager permissionsManager)
        {
            Check.ArgumentNotNull(permissionsManager, nameof(permissionsManager));

            this.permissionsManager = permissionsManager;
        }

        #endregion

        #region Fields

        private readonly IKrPermissionsManager permissionsManager;

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override async Task AfterBeginTransaction(ICardDeleteExtensionContext context)
        {
            Guid? cardID;

            if (context.CardType is null
                || !(cardID = context.Request.CardID).HasValue)
            {
                return;
            }

            await using (context.DbScope.Create())
            {
                var permContext = await this.permissionsManager.TryCreateContextAsync(
                    new KrPermissionsCreateContextParams
                    {
                        CardID = cardID,
                        CardTypeID = context.CardType.ID,
                        ValidationResult = context.ValidationResult,
                        AdditionalInfo = context.Info,
                        ExtensionContext = context,
                        ServerToken = context.Info.TryGetServerToken(),
                        PrevToken = KrToken.TryGet(context.Request.Info),
                    },
                    cancellationToken: context.CancellationToken);

                if (permContext is not null)
                {
                    await this.permissionsManager.CheckRequiredPermissionsAsync(
                        permContext,
                        KrPermissionFlagDescriptors.DeleteCard);
                }
            }
        }

        #endregion
    }
}
