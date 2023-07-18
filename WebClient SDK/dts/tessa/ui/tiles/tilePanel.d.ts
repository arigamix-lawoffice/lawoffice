import { ITilePanel, ITile } from './interfaces';
import { TileSource } from './tileSource';
import { TileContextSource } from './tileContextSource';
import { TileDirection } from './tileDirection';
import { IStorage } from 'tessa/platform/storage';
export declare class TilePanel extends TileSource implements ITilePanel {
    constructor(contextSource: TileContextSource, direction: TileDirection, tiles?: ITile[] | null, info?: IStorage | null, sharedInfo?: IStorage | null);
    private _currentTile;
    private _tilesStack;
    private _waitingForAsyncId;
    private _backTile;
    readonly direction: TileDirection;
    get currentTile(): ITile | null;
    get backTile(): ITile;
    scrollTop: number;
    groupTileAction: (tile: ITile, scrollTop?: number | undefined) => Promise<void>;
    backTileAction: () => void;
    reset: () => void;
    clone(context?: TileContextSource): ITilePanel;
    dispose(): void;
}
