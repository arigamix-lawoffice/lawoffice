import { ITile } from './interfaces';
import { TileSource } from './tileSource';
import { TileContextSource } from './tileContextSource';
import { TileCommandBehavior } from './tileCommandBehavior';
import { TileEvaluationEventArgs } from './tileEvaluationEventArgs';
import { ITileContext } from './tileContext';
import { IStorage } from 'tessa/platform/storage';
export interface TileCommand {
    (tile: ITile): void | Promise<void>;
}
export declare class Tile extends TileSource implements ITile {
    constructor(args: {
        name: string;
        caption: string;
        icon: string;
        contextSource: TileContextSource;
        command?: TileCommand | null;
        group?: number;
        order?: number;
        commandBehavior?: TileCommandBehavior;
        isCollapsed?: boolean;
        isEnabled?: boolean;
        isHidden?: boolean;
        tiles?: ITile[] | null;
        info?: IStorage | null;
        sharedInfo?: IStorage | null;
        evaluating?: ((e: TileEvaluationEventArgs) => void) | null;
        id?: guid | null;
        beforeGroupAction?: (tile: ITile) => Promise<void>;
        toolTip?: string;
    });
    private _caption;
    private _icon;
    private _command;
    private _group;
    private _order;
    private _commandBehavior;
    private _isCollapsed;
    private _isEnabled;
    private _isHidden;
    private _hotkey;
    private _toolTip;
    readonly id: guid;
    readonly name: string;
    get caption(): string;
    set caption(value: string);
    get icon(): string;
    set icon(value: string);
    get command(): TileCommand | null;
    set command(value: TileCommand | null);
    get group(): number;
    set group(value: number);
    get order(): number;
    set order(value: number);
    get commandBehavior(): TileCommandBehavior;
    set commandBehavior(value: TileCommandBehavior);
    get isCollapsed(): boolean;
    set isCollapsed(value: boolean);
    get isEnabled(): boolean;
    set isEnabled(value: boolean);
    get isHidden(): boolean;
    set isHidden(value: boolean);
    get isVisible(): boolean;
    get isHasVisibleTiles(): boolean;
    get hotkey(): string;
    set hotkey(value: string);
    beforeGroupAction: ((tile: ITile) => Promise<void>) | null;
    get toolTip(): string;
    set toolTip(value: string);
    protected childrenAreAvailableForEvaluation(e: TileEvaluationEventArgs): boolean;
    tileAction: (context?: ITileContext | undefined) => Promise<void>;
    clone(context?: TileContextSource): ITile;
}
