import { IUIContext } from 'tessa/ui';
import { ITile } from 'tessa/ui/tiles';
import { KrTileInfo } from 'tessa/workflow/krProcess';

export interface IKrTileCommand {
  onClickAction(
    context: IUIContext,
    tile: ITile,
    tileInfo: KrTileInfo
  ): Promise<void>;
}