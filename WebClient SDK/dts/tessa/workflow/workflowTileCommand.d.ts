import { WorkflowTileInfo } from './storage/workflowTileInfo';
import { IUIContext } from 'tessa/ui';
import { ITile } from 'tessa/ui/tiles';
export interface IWorkflowTileCommand {
    onClickAction(context: IUIContext, tile: ITile, tileInfo: WorkflowTileInfo): Promise<void>;
}
