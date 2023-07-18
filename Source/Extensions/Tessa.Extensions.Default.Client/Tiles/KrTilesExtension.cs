using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using NLog;
using Tessa.Extensions.Default.Client.Workflow.KrProcess;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Collections;
using Tessa.Platform.Storage;
using Tessa.UI;
using Tessa.UI.Tiles.Extensions;

namespace Tessa.Extensions.Default.Client.Tiles
{
    public sealed class KrTilesExtension :
        TileExtension
    {
        private readonly IKrTileInflater tileInflater;

        private readonly IKrGlobalTileContainer tileContainer;

        private readonly Logger logger = LogManager.GetCurrentClassLogger();
        
        public KrTilesExtension(
            IKrTileInflater tileInflater,
            IKrGlobalTileContainer tileContainer)
        {
            this.tileInflater = tileInflater;
            this.tileContainer = tileContainer;
        }

        public override Task InitializingGlobal(
            ITileGlobalExtensionContext context)
        {
            var tileInfos = this.tileContainer.GetTileInfos();
            if (tileInfos?.Count > 0)
            {
                var panel = context.Workspace.RightPanel;

                var tiles = this.tileInflater.Inflate(panel, tileInfos);
                panel.Tiles.AddRange(tiles);
            }

            return Task.CompletedTask;
        }

        public override Task InitializingLocal(
            ITileLocalExtensionContext context)
        {
            var panel = context.Workspace.RightPanel;
            var keyGestureConverter = (TypeConverter) new KeyGestureConverter();
            
            foreach (var tile in panel.Tiles)
            {
                var tileInfo = tile.SharedInfo.TryGet<KrTileInfo>(KrConstants.Ui.TileInfo);
                if (tileInfo != null
                    && !string.IsNullOrWhiteSpace(tileInfo.ButtonHotkey))
                {
                    try
                    {
                        var gesture = (KeyGesture)keyGestureConverter.ConvertFromString(tileInfo.ButtonHotkey);
                        tile.Context.AddInputBinding(tile, gesture);
                    }
                    catch (ArgumentException)
                    {
                        this.logger.Warn($"Invalid hotkey {tileInfo.ButtonHotkey} in KrSecondaryProcess {tileInfo.Name} (ID = {tileInfo.ID}).");
                    }
                }
            }

            return Task.CompletedTask;
        }
    }
}