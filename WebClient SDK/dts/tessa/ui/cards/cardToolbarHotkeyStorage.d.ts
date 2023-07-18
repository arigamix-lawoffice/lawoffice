import { HotkeyStorageBase, ItemHotkeyBase, KeyboardModifiers } from '../hotkeyStorageBase';
import { CardToolbarAction } from './cardToolbarAction';
export declare class CardToolbarHotkey extends ItemHotkeyBase<CardToolbarAction> {
}
export declare class CardToolbarHotkeyStorage extends HotkeyStorageBase<CardToolbarAction, CardToolbarHotkey> {
    addItemHotkey(itemHotkey: CardToolbarHotkey): boolean;
    removeHotkey(item: CardToolbarAction): boolean;
    removeHotkey(hotkey: string): boolean;
    protected isItemEnabled(_item: CardToolbarHotkey): boolean;
    protected getItemCommand(item: CardToolbarHotkey, modifiers: KeyboardModifiers): Function;
}
