import { runInAction } from 'mobx';
import { KrTileInflater } from '../../workflow/krProcess/krTileInflater';
import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
import { ITilePanel, Tile, TileGroups, ITile } from 'tessa/ui/tiles';
import { cardInSpecialMode, tryGetFromInfo } from 'tessa/ui';
import { KrTileInfo } from 'tessa/workflow/krProcess';
import { IStorage } from 'tessa/platform/storage';

export class KrTilesUIExtension extends CardUIExtension {

  public contextInitialized(context: ICardUIExtensionContext) {
    const card = context.card;
    let tilesPanel: ITilePanel;
    if (!card
      || cardInSpecialMode(context.model)
      || !context.uiContext.tiles
      || !(tilesPanel = context.uiContext.tiles.leftPanel)
    ) {
      return;
    }

    const tilesStorage = tryGetFromInfo<IStorage | null>(card.info, 'LocalTilesInfoMark', null);
    if (!tilesStorage) {
      return;
    }

    const localTiles: KrTileInfo[] = [];
    if (Array.isArray(tilesStorage)) {
      for (let tile of tilesStorage) {
        localTiles.push(new KrTileInfo(tile));
      }
    } else {
      const names = Object.getOwnPropertyNames(tilesStorage);
      for (let name of names) {
        localTiles.push(new KrTileInfo(tilesStorage[name]));
      }
    }

    // т.к. в этот момент tilesPanel.tiles уже observable, то нужно завернуть в runInAction
    runInAction(() => {
      KrTilesUIExtension.removeTilesWithTileInfo(tilesPanel.tiles);

      const tiles = KrTileInflater.instance.inflate(tilesPanel.contextSource, localTiles, TileGroups.Workflow);

      const actionGroupingTiles = tiles
        .filter(x => {
          const info = tryGetFromInfo<KrTileInfo | null>(x.sharedInfo, 'TileInfo', null);
          return info && info.actionGrouping
        })
        .map(x => x.clone());

      let groupTile = tilesPanel.tryGetTile('ActionsGrouping');
      if (groupTile) {
        KrTilesUIExtension.removeTilesWithTileInfo(groupTile.tiles);
      }

      if (actionGroupingTiles.length > 0) {
        if (!groupTile) {
          groupTile = new Tile({
            name: 'ActionsGrouping',
            caption: '$UI_Tiles_ActionsGrouping',
            icon: 'ta icon-thin-258',
            contextSource: tilesPanel.contextSource,
            group: TileGroups.Cards,
            order: 10
          });
          tilesPanel.tiles.push(groupTile);
        }

        groupTile.info['MinActionsGroupingCount'] = 1;
        groupTile.tiles.push(...actionGroupingTiles);
      }

      // TODO: может быть сюда надо пихать тайлы с фильтром !actionGrouping
      tilesPanel.tiles.push(...tiles);
    });
  }

  private static removeTilesWithTileInfo(tiles: ITile[]) {
    const removeIndices: number[] = [];
    tiles.forEach((x, i) => {
      if (tryGetFromInfo(x.sharedInfo, 'TileInfo', null)) {
        removeIndices.push(i);
      }
    });
    removeIndices.reverse();

    for (let i of removeIndices) {
      tiles.splice(i, 1);
    }
  }

}
