import { TileCommandEventType } from './tileCommandEventType';
import { KeyboardModifiers } from './keyboardModifiers';
import { ScopeContextInstance } from 'tessa/platform/scopes';
export interface ITileContext {
    readonly modifiers: KeyboardModifiers;
    readonly eventType: TileCommandEventType;
}
export declare class TileContext {
    constructor(modifiers: KeyboardModifiers, eventType: TileCommandEventType);
    private static _scopeContext;
    readonly modifiers: KeyboardModifiers;
    readonly eventType: TileCommandEventType;
    static get current(): ITileContext;
    static get hasCurrent(): boolean;
    static get unknown(): ITileContext;
    static create(context: ITileContext): ScopeContextInstance<ITileContext>;
}
