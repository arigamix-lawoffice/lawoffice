using System;
using System.Threading.Tasks;
using Tessa.Extensions.Default.Client.Notifications;
using Tessa.Platform;
using Tessa.UI;
using Tessa.UI.Tiles;
using Tessa.UI.Tiles.Extensions;

namespace Tessa.Extensions.Default.Client.Tiles
{
    /// <summary>
    /// Плитка для проверки уведомлений по заданиям вручную.
    /// </summary>
    public sealed class CheckTasksTileExtension :
        TileExtension
    {
        #region Constructors

        public CheckTasksTileExtension(IKrNotificationManager notificationManager) =>
            this.notificationManager = notificationManager ?? throw new ArgumentNullException(nameof(notificationManager));

        #endregion

        #region Fields

        private readonly IKrNotificationManager notificationManager;

        #endregion

        #region Private Methods

        private async void CheckTasksActionAsync(object parameter) =>
            await this.notificationManager.CheckTasksAsync(manualCheck: true);


        private async void CheckTasksEvaluating(object sender, TileEvaluationEventArgs e)
        {
            var deferral = e.Defer();
            try
            {
                e.SetIsEnabledWithCollapsing(
                    e.CurrentTile,
                    await this.notificationManager.CanCheckTasksAsync());
            }
            catch (Exception ex)
            {
                deferral.SetException(ex);
            }
            finally
            {
                deferral.Dispose();
            }
        }

        #endregion

        #region Base Overrides

        public override Task InitializingGlobal(ITileGlobalExtensionContext context)
        {
            ITilePanel panel = context.Workspace.RightPanel;

            panel.Tiles.Add(
                new Tile(
                    TileNames.CheckTasks,
                    "$UI_Tiles_CheckTasks",
                    context.Icons.Get("Thin412"),
                    panel,
                    new DelegateCommand(this.CheckTasksActionAsync),
                    TileGroups.Top,
                    order: 100,
                    size: TileSize.Half,
                    toolTip: TileHelper.GetToolTip("$UI_Tiles_CheckTasks_ToolTip", KrTileKeys.CheckTasks),
                    evaluating: this.CheckTasksEvaluating));

            return Task.CompletedTask;
        }


        public override Task InitializingLocal(ITileLocalExtensionContext context)
        {
            ITilePanel panel = context.Workspace.RightPanel;

            ITile checkTasks = panel.Tiles.TryGet(TileNames.CheckTasks);
            if (checkTasks != null)
            {
                checkTasks.Context.AddInputBinding(checkTasks, KrTileKeys.CheckTasks);
            }

            return Task.CompletedTask;
        }

        #endregion
    }
}
