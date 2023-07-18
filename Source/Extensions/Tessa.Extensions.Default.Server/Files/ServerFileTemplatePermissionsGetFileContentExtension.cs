using System;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Server.Workflow.KrPermissions;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Files
{
    public sealed class ServerFileTemplatePermissionsGetFileContentExtension :
        CardGetFileContentExtension
    {
        #region Fields

        private readonly IKrPermissionsManager permissionsManager;

        #endregion

        #region Constructors

        public ServerFileTemplatePermissionsGetFileContentExtension(IKrPermissionsManager permissionsManager)
        {
            this.permissionsManager = permissionsManager;
        }

        #endregion

        #region Base Overrides

        public override Task BeforeRequest(ICardGetFileContentExtensionContext context)
        {
            Guid? cardID;

            if (context.Request.ServiceType == CardServiceType.Default
                || context.Request.VersionRowID != CardHelper.ReplacePlaceholdersVersionRowID
                || !(cardID = context.Request.TryGetInfo()?.TryGet<Guid?>(CardHelper.PlaceholderCurrentCardIDInfo)).HasValue)
            {
                return Task.CompletedTask;
            }

            return KrFileAccessHelper.CheckAccessAsync(
                context.Request,
                context,
                cardID.Value,
                permissionsManager,
                context.CancellationToken);
        }

        #endregion
    }
}
