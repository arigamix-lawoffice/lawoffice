import { ICardEditorModel } from './interfaces';
import { CardToolbarItem } from './cardToolbarItem';
import { IUIContext } from '../uiContext';
import { CardToolbarHotkeyStorage } from './cardToolbarHotkeyStorage';
import { KeyboardModifiers } from '../tiles';
import { CardTagViewModel } from '../tags';
export interface ICardToolbarViewModel {
    readonly componentId: string;
    readonly context: IUIContext;
    rightToLeft: boolean;
    readonly isVisible: boolean;
    readonly items: ReadonlyArray<CardToolbarItem>;
    readonly hotkeys: CardToolbarHotkeyStorage;
    backgroundColor: string | null;
    canHaveTags: boolean;
    readonly tags: Array<CardTagViewModel>;
    clickAddTag: (() => Promise<void>) | null;
    addItem(item: CardToolbarItem): any;
    removeItem(item: CardToolbarItem): any;
    clearItems(): any;
    addItemIfNotExists(item: CardToolbarItem, hotkey?: {
        name: string;
        key: string;
        modifiers?: KeyboardModifiers;
    }): any;
    removeItemIfExists(name: string): any;
    dispose(): any;
    executeClickAddTag(): Promise<void>;
}
export declare class CardToolbarViewModel implements ICardToolbarViewModel {
    constructor(cardEditor: ICardEditorModel, isBottom?: boolean);
    private _componentId;
    private _cardEditor;
    private _items;
    private _backgroundColor;
    private _canHaveTags;
    get componentId(): string;
    get context(): IUIContext;
    rightToLeft: boolean;
    get isVisible(): boolean;
    get items(): ReadonlyArray<CardToolbarItem>;
    readonly hotkeys: CardToolbarHotkeyStorage;
    readonly isBottom: boolean;
    get backgroundColor(): string | null;
    set backgroundColor(value: string | null);
    get canHaveTags(): boolean;
    set canHaveTags(value: boolean);
    readonly tags: Array<CardTagViewModel>;
    clickAddTag: (() => Promise<void>) | null;
    addItem(item: CardToolbarItem): void;
    removeItem(item: CardToolbarItem): void;
    clearItems(): void;
    addItemIfNotExists(item: CardToolbarItem, hotkey?: {
        name: string;
        key: string;
        modifiers?: KeyboardModifiers;
    }): boolean;
    removeItemIfExists(name: string): boolean;
    dispose(): void;
    executeClickAddTag(): Promise<void>;
}
