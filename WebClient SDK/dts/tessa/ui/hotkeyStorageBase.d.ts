export interface KeyboardModifiers {
    ctrl?: boolean;
    shift?: boolean;
    alt?: boolean;
    meta?: boolean;
}
export declare function getKeyboardModifiersKey(key: string, modifiers: KeyboardModifiers): string;
export declare class CommandHotkey {
    constructor(key: string, command: Function, modifiers?: KeyboardModifiers);
    readonly key: string;
    readonly modifiers: KeyboardModifiers;
    readonly modifiersKey: string;
    readonly command: Function;
}
export declare abstract class ItemHotkeyBase<T> {
    constructor(item: T, name: string, key: string, modifiers?: KeyboardModifiers);
    readonly item: T;
    readonly name: string;
    readonly key: string;
    readonly modifiers: KeyboardModifiers;
    readonly modifiersKey: string;
}
export declare abstract class HotkeyStorageBase<TItem, TItemHotkey extends ItemHotkeyBase<TItem>> {
    constructor();
    protected _hotkeys: Map<string, (TItemHotkey | CommandHotkey)[]>;
    protected _itemHotkeys: Map<TItem, [TItemHotkey]>;
    addItemHotkey(itemHotkey: TItemHotkey): boolean;
    addCommandHotkey(commandHotkey: CommandHotkey): boolean;
    removeHotkey(item: TItem): boolean;
    removeHotkey(hotkey: string): boolean;
    processHotkey(e: KeyboardEvent): boolean;
    clear(): void;
    protected isItemEnabled(_item: TItemHotkey): boolean;
    protected getItemCommand(_item: TItemHotkey, _modifiers: KeyboardModifiers): Function;
}
