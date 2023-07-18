import { WorkspaceModel } from './workspaceModel';
import { ICardEditorModel } from 'tessa/ui/cards/interfaces';
export declare class CardWorkspace extends WorkspaceModel {
    constructor(id: guid, editor: ICardEditorModel);
    private _editor;
    get editor(): ICardEditorModel;
    get isCloseable(): boolean;
    get workspaceName(): string;
    get workspaceInfo(): string;
    dispose(): void;
    getRoute(): string;
    close(force?: boolean): Promise<boolean>;
    private onModelSet;
    private onCardEditorModelClosing;
    private onCardEditorModelClosed;
}
