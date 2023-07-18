import { ITileWorkspace } from 'tessa/ui/tiles';
import { IExtensionExecutor } from 'tessa/extensions';
import { EventHandler } from 'tessa/platform';
export interface WorkspaceModelClosedArgs {
    workspace: WorkspaceModel;
    force?: boolean;
}
export interface WorkspaceModelHotkeyArgs {
    e: KeyboardEvent;
    processed: boolean;
}
export declare class WorkspaceModel {
    constructor(id: guid);
    protected _tileWorkspace: ITileWorkspace;
    protected _tileExecutor: IExtensionExecutor | null;
    readonly id: guid;
    readonly uiId: guid;
    get tileWorkspace(): ITileWorkspace;
    get tileExecutor(): IExtensionExecutor;
    get isCloseable(): boolean;
    get workspaceName(): string;
    get workspaceInfo(): string;
    get localizedWorkspaceName(): string;
    get localizedWorkspaceInfo(): string;
    activate(): void;
    deactivate(): void;
    close(_force?: boolean): Promise<boolean>;
    getRoute(): string;
    dispose(): void;
    readonly onClosed: EventHandler<(args: WorkspaceModelClosedArgs) => void>;
    readonly onHotkey: EventHandler<(args: WorkspaceModelHotkeyArgs) => void>;
}
