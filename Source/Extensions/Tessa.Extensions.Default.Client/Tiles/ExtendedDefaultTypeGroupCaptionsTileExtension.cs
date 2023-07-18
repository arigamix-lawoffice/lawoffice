using System.Threading.Tasks;
using Tessa.Extensions.Default.Shared;
using Tessa.UI.Tiles;
using Tessa.UI.Tiles.Extensions;

namespace Tessa.Extensions.Default.Client.Tiles
{
    public sealed class ExtendedDefaultTypeGroupCaptionsTileExtension : TileExtension
    {
        #region Base Overrides

        public override Task InitializingGlobal(ITileGlobalExtensionContext context)
        {
            context.Workspace.SetTypeGroupCaption(DefaultTileNames.Routes, "$KrTiles_Routes");
            return Task.CompletedTask;
        }

        #endregion
    }
}