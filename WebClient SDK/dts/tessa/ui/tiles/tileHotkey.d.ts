import { ITile } from './interfaces';
import { ItemHotkeyBase } from '../hotkeyStorageBase';
export declare class TileHotkey extends ItemHotkeyBase<ITile> {
    get tile(): ITile;
}
