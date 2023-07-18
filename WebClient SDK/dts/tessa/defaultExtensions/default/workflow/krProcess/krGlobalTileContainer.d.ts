import { KrTileInfo } from 'tessa/workflow/krProcess';
export declare class KrGlobalTileContainer {
    private constructor();
    private static _instance;
    static get instance(): KrGlobalTileContainer;
    private _infos;
    init(infos: KrTileInfo[]): void;
    getTileInfos(): ReadonlyArray<KrTileInfo>;
}
