import { ITilePanel, ITileWorkspace } from './interfaces';
import { TileContextSource } from './tileContextSource';
import { IStorage } from 'tessa/platform/storage';
export declare class TileWorkspace implements ITileWorkspace {
    constructor(leftPanel: ITilePanel, rightPanel: ITilePanel, info?: IStorage | null);
    readonly leftPanel: ITilePanel;
    readonly rightPanel: ITilePanel;
    readonly info: IStorage;
    clear(): void;
    clone(context?: TileContextSource): ITileWorkspace;
}
