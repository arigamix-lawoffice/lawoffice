import { ITileWorkspace } from './interfaces';
export interface ITileLocalExtensionContext {
    readonly globalWorkspace: ITileWorkspace;
    readonly workspace: ITileWorkspace;
}
export declare class TileLocalExtensionContext implements ITileLocalExtensionContext {
    constructor(globalWorkspace: ITileWorkspace, workspace: ITileWorkspace);
    readonly globalWorkspace: ITileWorkspace;
    readonly workspace: ITileWorkspace;
}
