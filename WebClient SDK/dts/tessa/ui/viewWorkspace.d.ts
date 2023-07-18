import { WorkspaceModel } from './workspaceModel';
import { IWorkplaceViewModel } from 'tessa/ui/views';
export declare class ViewWorkspace extends WorkspaceModel {
    constructor(workplace: IWorkplaceViewModel);
    readonly workplace: IWorkplaceViewModel;
    get isCloseable(): boolean;
    get workspaceName(): string;
    get workspaceInfo(): string;
    activate(): Promise<void>;
    deactivate(): Promise<void>;
    close(force?: boolean): Promise<boolean>;
    getRoute(): string;
}
