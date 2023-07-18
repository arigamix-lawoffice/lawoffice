import { KrGlobalTileContainer } from '../workflow/krProcess/krGlobalTileContainer';
import { KrTileInflater } from '../workflow/krProcess/krTileInflater';
import { TileExtension, ITileGlobalExtensionContext, TileGroups } from 'tessa/ui/tiles';

export class KrTilesExtension extends TileExtension {
  public initializingGlobal(context: ITileGlobalExtensionContext) {
    const tileInfos = KrGlobalTileContainer.instance.getTileInfos();
    if (tileInfos.length > 0) {
      const panel = context.workspace.rightPanel;
      const tiles = KrTileInflater.instance.inflate(
        panel.contextSource,
        tileInfos,
        TileGroups.Workflow
      );
      panel.tiles.push(...tiles);
    }
  }
}
