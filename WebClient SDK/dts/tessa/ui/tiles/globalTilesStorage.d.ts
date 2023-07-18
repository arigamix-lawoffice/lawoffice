import { TileWorkspace } from './tileWorkspace';
export declare class GlobalTilesStorage {
    private constructor();
    private static _instance;
    static get instance(): GlobalTilesStorage;
    readonly workspace: TileWorkspace;
}
