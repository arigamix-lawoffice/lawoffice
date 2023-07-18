import { IWorkflowTileCommand } from './workflowTileCommand';
import { WorkflowTileInfo } from './storage/workflowTileInfo';
import { IUIContext } from 'tessa/ui';
import { ITile } from 'tessa/ui/tiles';
export declare class WorkflowLocalTileCommand implements IWorkflowTileCommand {
    private constructor();
    private static _instance;
    static get instance(): WorkflowLocalTileCommand;
    onClickAction(context: IUIContext, _tile: ITile, tileInfo: WorkflowTileInfo): Promise<void>;
}
