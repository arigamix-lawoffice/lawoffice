import { ICardEditorModel, ICardModel, CardEditorCreationContext, CardEditorOpeningContext } from 'tessa/ui/cards';
import { IUIContextActionOverridings, IUIContext } from '../uiContext';
import { IStorage } from 'tessa/platform/storage';
export interface DialogOptions {
    withDialogWallpaper?: boolean;
    withTabControlBackground?: boolean;
    openInFullscreen?: boolean;
    showOnlyFirstTab?: boolean;
    dialogAutoSize?: boolean;
    className?: string;
    dialogName?: string;
}
export interface CreateCardArg {
    cardId?: guid;
    cardTypeId?: guid;
    cardTypeName?: string;
    context?: IUIContext;
    displayValue?: string;
    info?: IStorage;
    cardModifierAction?: (context: CardEditorCreationContext) => void;
    cardModelModifierAction?: (context: CardEditorCreationContext) => void;
    cardEditorModifierAction?: (context: CardEditorCreationContext) => void;
    alwaysNewTab?: boolean;
    openToTheRightOfSelectedTab?: boolean;
    withUIExtensions?: boolean;
    saveCreationRequest?: boolean;
    creationModeDisplayText?: string;
    splashResolve?: () => void;
    dialogOptions?: DialogOptions;
}
interface ShowCardBaseArg {
    displayValue?: string;
    alwaysNewTab?: boolean;
    openToTheRightOfSelectedTab?: boolean;
    needDispatch?: boolean;
    prepareEditorAction?: (editor: ICardEditorModel) => boolean;
    beforeShowAction?: () => Promise<void>;
    dialogOptions?: DialogOptions;
}
export interface ShowCardArg extends ShowCardBaseArg {
    editor: ICardEditorModel;
}
export interface ShowCardModelArg extends ShowCardBaseArg {
    cardModel: ICardModel;
    dialogName?: string;
}
export interface OpenCardArg {
    cardId?: guid;
    cardTypeId?: guid;
    cardTypeName?: string;
    context?: IUIContext;
    displayValue?: string;
    info?: IStorage;
    cardEditorFactory?: () => ICardEditorModel;
    cardModifierAction?: (context: CardEditorOpeningContext) => void;
    cardModelModifierAction?: (context: CardEditorOpeningContext) => void;
    cardEditorModifierAction?: (context: CardEditorOpeningContext) => void;
    alwaysNewTab?: boolean;
    openToTheRightOfSelectedTab?: boolean;
    withUIExtensions?: boolean;
    needDispatch?: boolean;
    splashResolve?: () => void;
    dialogOptions?: DialogOptions;
    activateFirstAvailableWorkspaceOnLoadingError?: boolean;
}
/**
 * Получить название вкладки рабочей области для модели карточки.
 *
 * @param {ICardModel} model Модель карточки.
 * @param {string} displayValue Отображаемое значение по умолчанию.
 * @param {(string | undefined)} digest Дайджест, если он известен. Если значение не задано, то оно будет вычислено автоматически.
 * @returns {(Promise<string | null>)} Название рабочей области.
 */
export declare function getWorkspaceName(model: ICardModel, displayValue?: string, digest?: string | undefined): Promise<string | null>;
export declare function subscribeEditorToCardModelInitialized(editor: ICardEditorModel, displayValue?: string): void;
export declare function getAncestorOverridings(context: IUIContext | null | undefined, predicate: (ao: IUIContextActionOverridings) => boolean): IUIContextActionOverridings | null;
export {};
