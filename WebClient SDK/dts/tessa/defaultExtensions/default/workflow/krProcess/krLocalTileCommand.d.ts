import { IKrTileCommand } from './interfaces';
import { IUIContext } from 'tessa/ui';
import { KrTileInfo } from 'tessa/workflow/krProcess';
import { ITile } from 'tessa/ui/tiles';
export declare class KrLocalTileCommand implements IKrTileCommand {
    private constructor();
    private static _instance;
    static get instance(): KrLocalTileCommand;
    onClickAction(context: IUIContext, _tile: ITile, tileInfo: KrTileInfo): Promise<void>;
}
