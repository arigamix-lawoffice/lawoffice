import { ErrorInfo, RefObject } from 'react';
import { CardEditorOperationType } from './cardEditorOperationType';
import { CardEditorOpeningContext } from './cardEditorOpeningContext';
import { CardEditorCreationContext } from './cardEditorCreationContext';
import { CardModelFlags } from './cardModelFlags';
import { CardSavingRequest } from './cardSavingRequest';
import { ICardEditorModel, ICardModel, ICardEditorData, CardModelInitializingEventArgs, ICardEditorModelContext, CardEditorModelClosingArgs, CardEditorModelClosedArgs, CardEditorModelResizeArgs, CardEditorModelUIErrorOccurredArgs } from './interfaces';
import { ICardToolbarViewModel } from './cardToolbarViewModel';
import { UIButton } from '../uiButton';
import { Card, CardRow } from 'tessa/cards';
import { IStorage } from 'tessa/platform/storage';
import { ValidationResult } from 'tessa/platform/validation';
import { IUIContext } from 'tessa/ui/uiContext';
import { CreateModelFunc, CreateFormFunc, CreateFileSourceFunc, CreateFileContainerFunc } from 'tessa/ui/uiHelper';
import { ClassNameList } from 'tessa/ui';
import { EventHandler } from 'tessa/platform';
import { UIErrorPresenterViewModel } from './uiErrorPresenterViewModel';
export declare class CardEditorModel implements ICardEditorModel {
    constructor(createModelFunc?: CreateModelFunc | null, createFormFunc?: CreateFormFunc | null, createFileSourceFunc?: CreateFileSourceFunc | null, createFileContainerFunc?: CreateFileContainerFunc | null);
    private _extensionExecutor;
    private _extensionContext;
    private _operationLock;
    private _cardModel;
    private _createModelFunc;
    private _createFormFunc;
    private _createFileSourceFunc;
    private _createFileContainer;
    private _workspaceName;
    private _workspaceInfo;
    private _closed;
    private _isManualOperationInProgressDrop;
    private _currentOperationType;
    private _lastOperationType;
    private _lastOperationCompleted;
    private _lastOperationDuration;
    private _lastOperationRequestDuration;
    private _operationStatusText;
    private _operationInProgress;
    private _operationProgress;
    private _closePending;
    private _isUpdatedServer;
    private _isTaskBottomCardModeEnabled;
    private _progressTimer;
    private _progressTimerRequestGuard;
    private _toolbar;
    private _bottomToolbar;
    private _bottomDialogButtons;
    private _componentRef;
    private _statusBarIsVisible;
    readonly context: IUIContext;
    readonly className: ClassNameList;
    get cardModel(): ICardModel | null;
    set cardModel(value: ICardModel | null);
    readonly info: IStorage;
    readonly lastData: ICardEditorData;
    get currentOperationType(): CardEditorOperationType | null;
    get isTaskBottomCardModeEnabled(): boolean;
    set isTaskBottomCardModeEnabled(value: boolean);
    get lastOperationType(): CardEditorOperationType | null;
    set lastOperationType(value: CardEditorOperationType | null);
    get lastOperationCompleted(): Date | null;
    set lastOperationCompleted(value: Date | null);
    get lastOperationDuration(): number | null;
    set lastOperationDuration(value: number | null);
    get lastOperationRequestDuration(): number | null;
    set lastOperationRequestDuration(value: number | null);
    get operationStatusText(): string | null;
    set operationStatusText(value: string | null);
    get operationInProgress(): boolean;
    get operationProgress(): number | null;
    get isClosed(): boolean;
    get closePending(): boolean;
    set closePending(value: boolean);
    get workspaceName(): string;
    set workspaceName(value: string);
    get workspaceInfo(): string;
    set workspaceInfo(value: string);
    get localizedWorkspaceName(): string;
    get localizedWorkspaceInfo(): string;
    get isUpdatedServer(): boolean;
    set isUpdatedServer(value: boolean);
    withUIExtensions: boolean;
    dialogName: string | null;
    get toolbar(): ICardToolbarViewModel;
    set toolbar(value: ICardToolbarViewModel);
    get bottomToolbar(): ICardToolbarViewModel;
    set bottomToolbar(value: ICardToolbarViewModel);
    get bottomDialogButtons(): UIButton[];
    get statusBarIsVisible(): boolean;
    set statusBarIsVisible(value: boolean);
    createCard(args: {
        cardId?: guid;
        cardTypeId?: guid;
        cardTypeName?: string;
        context?: IUIContext;
        info?: IStorage;
        cardModifierAction?: (context: CardEditorCreationContext) => void;
        cardModelModifierAction?: (context: CardEditorCreationContext) => void;
    }): Promise<boolean>;
    openCard(args: {
        cardId?: guid;
        cardTypeId?: guid;
        cardTypeName?: string;
        context?: IUIContext;
        info?: IStorage;
        cardModifierAction?: (context: CardEditorOpeningContext) => void;
        cardModelModifierAction?: (context: CardEditorOpeningContext) => void;
    }): Promise<boolean>;
    refreshCard(context?: IUIContext, info?: IStorage): Promise<boolean>;
    saveCard(context?: IUIContext, info?: IStorage, request?: CardSavingRequest, resultClosure?: {
        validationResult?: ValidationResult;
    }): Promise<boolean>;
    deleteCard(args: {
        context?: IUIContext;
        info?: IStorage;
        withoutBackupOnly?: boolean;
    }): Promise<boolean>;
    getTemplatedCard(args: {
        cardId?: guid;
        cardTypeId?: guid;
        cardTypeName?: string;
        context?: IUIContext;
        info?: IStorage;
    }): Promise<Card | null>;
    tryGetComponentRef(): HTMLDivElement | null;
    setComponentRef(value: RefObject<HTMLDivElement> | null): void;
    close(force?: boolean): Promise<boolean>;
    createCardModel(card: Card, sectionRows: Map<string, CardRow>): Promise<ICardModel>;
    private createFullCardModel;
    private createCardModelCore;
    createFormWithExtensions(model: ICardModel, otherContext?: IUIContext | null): Promise<boolean>;
    initializeCardModel(model: ICardModel, info?: IStorage | null): Promise<void>;
    createAndInitializeModel(args: {
        card: Card;
        sectionRows?: Map<string, CardRow>;
        info?: IStorage;
        flags?: CardModelFlags;
        contextExecutor?: (action: (context: IUIContext) => void) => void;
        savingFunc?: (request?: CardSavingRequest) => Promise<ValidationResult>;
        otherContext?: IUIContext;
        initializingModelAction?: (model: ICardModel) => void;
        initializedModelAction?: (model: ICardModel) => void;
    }): Promise<ICardModel>;
    changeWorkspace(name: string, info?: string): void;
    private performOperation;
    private getOperationText;
    private getOperationTypeActionName;
    setOperationInProgress<T = void>(action: () => Promise<T>): Promise<T>;
    private tryCreateCard;
    private tryOpenCard;
    private trySaveCard;
    private tryDeleteCard;
    private tryGetTemplatedCard;
    private executeExtensions;
    getContext(): ICardEditorModelContext;
    setModelContext(modelContext: ICardEditorModelContext): void;
    notifyContextInitialized(): ValidationResult;
    private static canCloseWhenPending;
    private startCheckProgress;
    private stopCheckProgress;
    notifyUIErrorOccurred: (error: Error, errorInfo: ErrorInfo, close: () => void) => UIErrorPresenterViewModel | undefined;
    onModelSet: ((editor: ICardEditorModel) => void) | null;
    onClosing: ((editor: ICardEditorModel, force: boolean) => Promise<boolean>) | null;
    onClosed: ((editor: ICardEditorModel) => void) | null;
    onWorkspaceChanged: ((editor: ICardEditorModel, name: string, info?: string) => {
        name: string;
        info?: string;
    }) | null;
    readonly cardModelInitialized: EventHandler<(args: CardModelInitializingEventArgs) => Promise<void>>;
    readonly closing: EventHandler<(args: CardEditorModelClosingArgs) => void>;
    readonly closed: EventHandler<(args: CardEditorModelClosedArgs) => void>;
    readonly resize: EventHandler<(args: CardEditorModelResizeArgs) => void>;
    readonly uiErrorOccurred: EventHandler<(args: CardEditorModelUIErrorOccurredArgs) => void>;
}
