using System.Threading.Tasks;
using Tessa.Platform.Licensing;
using Tessa.UI;
using Tessa.UI.Controls.Forums;
using Tessa.UI.Tiles;
using Tessa.UI.Tiles.Extensions;

namespace Tessa.Extensions.Default.Client.Tiles
{
    internal class NotificationTileExtension : TileExtension
    {
        #region Fields

        private readonly INotificationButtonUIManager manager;

        private readonly ILicenseManager licenseManager;

        private readonly IUserSettings userSettings;

        #endregion

        #region Constructors

        public NotificationTileExtension(
            INotificationButtonUIManager manager,
            ILicenseManager licenseManager,
            IUserSettings userSettings)
        {
            this.manager = manager;
            this.licenseManager = licenseManager;
            this.userSettings = userSettings;
        }

        #endregion

        #region Base Overrides

        public override async Task InitializingGlobal(ITileGlobalExtensionContext context)
        {
            if (!LicensingHelper.CheckForumLicense(await this.licenseManager.GetLicenseAsync(context.CancellationToken), out _))
            {
                return;
            }

            context.Workspace.RightPanel.Tiles.Add(
                this.CreateShowMessageIndicatorTile(context));
        }

        #endregion

        #region Private Methods

        private void EnableShowNotificationButton(object sender, TileEvaluationEventArgs e)
        {
            e.SetIsEnabledWithCollapsing(e.CurrentTile, this.userSettings.EnableMessageIndicator);
        }

        private async Task ShowNotificationButton(object parameter)
        {
            await manager.ShowNotificationButtonAsync();
        }

        private Tile CreateShowMessageIndicatorTile(ITileGlobalExtensionContext context)
        {
            return new Tile(
                "MessageIndicator",
                TileHelper.SplitCaption("$Forum_Tiles_ShowMessageIndicator"),
                context.Icons.Get("Int1216"),
                context.Workspace.RightPanel,
                new DelegateCommand(async p => await this.ShowNotificationButton(p)),
                TileGroups.Settings,
                order: 0,
                evaluating: this.EnableShowNotificationButton);
        }

        #endregion
    }
}