import { TileLocalExtensionContext, ITileLocalExtensionContext } from './tileLocalExtensionContext';
import { ITilePanel, ITileWorkspace } from './interfaces';
export interface ITilePanelExtensionContext extends ITileLocalExtensionContext {
    readonly panel: ITilePanel;
}
export declare class TilePanelExtensionContext extends TileLocalExtensionContext {
    constructor(globalWorkspace: ITileWorkspace, workspace: ITileWorkspace, panel: ITilePanel);
    readonly panel: ITilePanel;
}
