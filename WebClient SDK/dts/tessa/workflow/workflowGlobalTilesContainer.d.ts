import { WorkflowTileInfo } from './storage';
export declare class WorkflowGlobalTilesContainer {
    private constructor();
    private static _instance;
    static get instance(): WorkflowGlobalTilesContainer;
    private _infos;
    init(infos: WorkflowTileInfo[]): void;
    getTileInfos(): ReadonlyArray<WorkflowTileInfo>;
}
