import { WorkflowTileInfo } from './storage';
import { TileContextSource, ITile } from 'tessa/ui/tiles';
export declare class WorkflowTileInflater {
    private constructor();
    private static _instance;
    static get instance(): WorkflowTileInflater;
    inflate(contextSource: TileContextSource, tileInfos: ReadonlyArray<WorkflowTileInfo>, groups?: number | null): ITile[];
    private inflateTile;
    private static onClickAction;
    private static createInfo;
    private static tileGroupingEvaluation;
}
