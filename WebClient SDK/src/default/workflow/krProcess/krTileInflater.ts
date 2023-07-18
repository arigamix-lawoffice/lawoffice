import { IKrTileCommand } from './interfaces';
import { KrGlobalTileCommand } from './krGlobalTileCommand';
import { KrLocalTileCommand } from './krLocalTileCommand';
import {
  TileContextSource,
  ITile,
  Tile,
  TileEvaluationEventArgs,
  TileGroups
} from 'tessa/ui/tiles';
import { KrTileInfo } from 'tessa/workflow/krProcess';
import { IStorage } from 'tessa/platform/storage';
import { tryGetFromInfo, showNotEmpty } from 'tessa/ui';
import { getTessaIcon } from 'common/utility';
import { ValidationResult } from 'tessa/platform/validation';

export class KrTileInflater {
  //#region ctor

  private constructor() {}

  //#endregion

  //#region instance

  private static _instance: KrTileInflater;

  public static get instance(): KrTileInflater {
    if (!KrTileInflater._instance) {
      KrTileInflater._instance = new KrTileInflater();
    }
    return KrTileInflater._instance;
  }

  //#endregion

  //#region methods

  public inflate(
    contextSource: TileContextSource,
    tileInfos: ReadonlyArray<KrTileInfo>,
    groups: number | null = null
  ): ITile[] {
    const tiles: ITile[] = [];

    for (const tileInfo of tileInfos) {
      tiles.push(this.inflateTile(contextSource, tileInfo, groups));
    }

    return tiles;
  }

  private inflateTile(
    contextSource: TileContextSource,
    tileInfo: KrTileInfo,
    groups: number | null = null
  ): ITile {
    const icon = getTessaIcon(tileInfo.icon);

    const tileCollection: ITile[] = [];
    if (tileInfo.nestedTiles.length !== 0) {
      const nestedTiles: ITile[] = [];
      for (const nestedTileInfo of tileInfo.nestedTiles) {
        nestedTiles.push(this.inflateTile(contextSource, nestedTileInfo, groups));
      }

      tileCollection.push(...nestedTiles);

      // Тайл группы.
      const grouptile = new Tile({
        name: tileInfo.caption,
        caption: tileInfo.caption,
        icon: icon,
        contextSource,
        order: tileInfo.order,
        group: groups ?? TileGroups.NoGroup,
        tiles: tileCollection,
        sharedInfo: KrTileInflater.createInfo(tileInfo),
        toolTip: tileInfo.tooltip
      });
      if (tileInfo.actionGrouping) {
        grouptile.info['.actionsGrouping'] = true;
        grouptile.evaluating.add(KrTileInflater.tileGroupingEvaluation);
      }
      return grouptile;
    }

    const tile = new Tile({
      name: tileInfo.name,
      caption: tileInfo.caption,
      icon: icon,
      contextSource,
      command: KrTileInflater.onClickAction,
      order: tileInfo.order,
      group: groups ?? TileGroups.NoGroup,
      tiles: tileCollection,
      sharedInfo: KrTileInflater.createInfo(tileInfo),
      toolTip: tileInfo.tooltip
    });
    if (tileInfo.actionGrouping) {
      tile.info['.actionsGrouping'] = true;
      tile.evaluating.add(KrTileInflater.tileGroupingEvaluation);
    }
    return tile;
  }

  private static async onClickAction(tile: ITile) {
    const tileInfo = tryGetFromInfo<KrTileInfo | null>(tile.sharedInfo, 'TileInfo', null);
    if (tileInfo) {
      const context = tile.context;
      const command: IKrTileCommand = tileInfo.isGlobal
        ? KrGlobalTileCommand.instance
        : KrLocalTileCommand.instance;

      try {
        // В ТК вызывается как async void.
        // UIContext не будет сохраняться после первой асинхронной операции.
        // это нужно для синхронизации поведения, например в AdvancedDialogCommandHandler.
        command.onClickAction(context, tile, tileInfo);
      } catch (err) {
        await showNotEmpty(ValidationResult.fromError(err));
      }
    }
  }

  private static createInfo(tileInfo: KrTileInfo): IStorage {
    return {
      TileInfo: tileInfo
    };
  }

  private static tileGroupingEvaluation(e: TileEvaluationEventArgs) {
    e.setIsEnabledWithCollapsing(e.currentTile, true);
  }

  //#endregion
}
