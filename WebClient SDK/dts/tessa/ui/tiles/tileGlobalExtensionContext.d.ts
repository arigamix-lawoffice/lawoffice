import { ITileWorkspace } from './interfaces';
export interface ITileGlobalExtensionContext {
    readonly workspace: ITileWorkspace;
}
export declare class TileGlobalExtensionContext implements ITileGlobalExtensionContext {
    constructor(workspace: ITileWorkspace);
    readonly workspace: ITileWorkspace;
}
