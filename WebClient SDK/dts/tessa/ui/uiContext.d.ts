import { ICardEditorModel } from './cards/interfaces';
import { IViewContext } from './views/viewContext';
import { IStorage } from 'tessa/platform/storage';
import { ScopeContextInstance } from 'tessa/platform/scopes';
import { ITileWorkspace } from 'tessa/ui/tiles/interfaces';
import { CreateCardArg, OpenCardArg, ShowCardArg } from './uiHost/common';
export interface IUIContext {
    readonly cardEditor: ICardEditorModel | null;
    readonly viewContext: IViewContext | null;
    tiles: ITileWorkspace | null;
    readonly info: IStorage;
    readonly parent: IUIContext | null;
    actionOverridings: IUIContextActionOverridings | null;
    readonly isClosed: boolean;
}
export interface IUIContextActionOverridings {
    createCard?: (args: CreateCardArg) => Promise<ICardEditorModel | null>;
    openCard?: (args: OpenCardArg) => Promise<ICardEditorModel | null>;
    showCardEditor?: (args: ShowCardArg) => Promise<ICardEditorModel | null>;
}
export declare class UIContext implements IUIContext {
    constructor(args: {
        cardEditor?: ICardEditorModel;
        viewContext?: IViewContext | (() => IViewContext);
        info?: IStorage;
        actionOverridings?: IUIContextActionOverridings;
        parent?: IUIContext;
        parentCanBeClosed?: boolean;
        closeDialog?: (<T>(args: T) => void) | null;
    });
    private static _scopeContext;
    private _cardEditor;
    private _viewContext;
    private _viewContextFunc;
    private _info;
    private _parent;
    get cardEditor(): ICardEditorModel | null;
    get viewContext(): IViewContext | null;
    tiles: ITileWorkspace | null;
    get info(): IStorage;
    get parent(): IUIContext | null;
    static get current(): IUIContext;
    static get hasCurrent(): boolean;
    static get unknown(): IUIContext;
    actionOverridings: IUIContextActionOverridings | null;
    get isClosed(): boolean;
    closeDialog: (<T>(args: T) => void) | null;
    /**
     * Создаёт область операции, в которой заданный контекст будет являться текущим.
     * @param context Контекст, для которого создаётся область операции.
     * @param isCaptureContext Значение true, если текущий контекст должен быть установлен в качестве родительского, иначе - false. Значение по умолчанию: true.
     */
    static create(context: IUIContext | null, isCaptureContext?: boolean): ScopeContextInstance<IUIContext>;
    private static tryResolveAliveParent;
}
