import { KrTileInfo } from 'tessa/workflow/krProcess';

export class KrGlobalTileContainer {

  //#region ctor

  private constructor() {}

  //#endregion

  //#region instance

  private static _instance: KrGlobalTileContainer;

  public static get instance(): KrGlobalTileContainer {
    if (!KrGlobalTileContainer._instance) {
      KrGlobalTileContainer._instance = new KrGlobalTileContainer();
    }
    return KrGlobalTileContainer._instance;
  }

  //#endregion

  //#region fields

  private _infos: KrTileInfo[];

  //#endregion

  //#region methods

  public init(infos: KrTileInfo[]) {
    this._infos = infos;
  }

  public getTileInfos(): ReadonlyArray<KrTileInfo> {
    return this._infos || [];
  }

  //#endregion

}