import { ITile } from './interfaces';
import { TileHotkey } from './tileHotkey';
import { HotkeyStorageBase, KeyboardModifiers } from '../hotkeyStorageBase';
export declare class HotkeyStorage extends HotkeyStorageBase<ITile, TileHotkey> {
    addTileHotkey(tileHotkey: TileHotkey): boolean;
    addItemHotkey(tileHotkey: TileHotkey): boolean;
    removeHotkey(tile: ITile): boolean;
    removeHotkey(hotkey: string): boolean;
    protected isItemEnabled(item: TileHotkey): boolean;
    protected getItemCommand(item: TileHotkey, modifiers: KeyboardModifiers): Function;
}
