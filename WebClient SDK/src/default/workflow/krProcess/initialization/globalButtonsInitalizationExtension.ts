import { KrGlobalTileContainer } from '../krGlobalTileContainer';
import { ApplicationExtension, IApplicationExtensionMetadataContext } from 'tessa';
import { tryGetFromInfo } from 'tessa/ui';
import { KrTileInfo } from 'tessa/workflow/krProcess';

export class GlobalButtonsInitalizationExtension extends ApplicationExtension {
  public async afterMetadataReceived(context: IApplicationExtensionMetadataContext): Promise<void> {
    if (context.response) {
      const tiles = tryGetFromInfo(context.response.info, 'GlobalTilesInfoMark', []);
      const globalTiles: KrTileInfo[] = [];
      if (Array.isArray(tiles)) {
        for (const tile of tiles) {
          globalTiles.push(new KrTileInfo(tile));
        }
      } else {
        const names = Object.getOwnPropertyNames(tiles);
        for (const name of names) {
          globalTiles.push(new KrTileInfo(tiles[name]));
        }
      }

      KrGlobalTileContainer.instance.init(globalTiles);
    }
  }
}
