import { IWorkflowTileCommand } from './workflowTileCommand';
import { WorkflowTileInfo } from './storage/workflowTileInfo';
import { IUIContext } from 'tessa/ui';
import { ITile } from 'tessa/ui/tiles';
export declare class WorkflowGlobalTileCommand implements IWorkflowTileCommand {
    private constructor();
    private static _instance;
    static get instance(): WorkflowGlobalTileCommand;
    onClickAction(_context: IUIContext, _tile: ITile, tileInfo: WorkflowTileInfo): Promise<void>;
}
