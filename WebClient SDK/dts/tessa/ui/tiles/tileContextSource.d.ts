import { HotkeyStorage } from './hotkeyStorage';
import { IUIContext } from 'tessa/ui/uiContext';
export declare class TileContextSource {
    constructor(context?: IUIContext);
    private _context;
    get context(): IUIContext;
    set context(value: IUIContext);
    readonly hotkeyStorage: HotkeyStorage;
}
