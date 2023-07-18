import { TileContextSource, ITile } from 'tessa/ui/tiles';
import { KrTileInfo } from 'tessa/workflow/krProcess';
export declare class KrTileInflater {
    private constructor();
    private static _instance;
    static get instance(): KrTileInflater;
    inflate(contextSource: TileContextSource, tileInfos: ReadonlyArray<KrTileInfo>, groups?: number | null): ITile[];
    private inflateTile;
    private static onClickAction;
    private static createInfo;
    private static tileGroupingEvaluation;
}
