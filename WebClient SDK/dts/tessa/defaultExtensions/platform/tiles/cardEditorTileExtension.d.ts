import { TileExtension, ITileGlobalExtensionContext, TileEvaluationEventArgs, ITileLocalExtensionContext } from 'tessa/ui/tiles';
/**
 * Плитки для редактирования карточки.
 */
export declare class CardEditorTileExtension extends TileExtension {
    initializingGlobal(context: ITileGlobalExtensionContext): void;
    initializingLocal(context: ITileLocalExtensionContext): void;
    static enableOnCard(e: TileEvaluationEventArgs): void;
    static enableOnUpdateCard(e: TileEvaluationEventArgs): void;
    static enableOnCardAndHasCreationInfo(e: TileEvaluationEventArgs): void;
    static enableOnCardAndAdministrator(e: TileEvaluationEventArgs): void;
    static enableOnUpdateCardAndStoreChangesetsAndAdministrator(e: TileEvaluationEventArgs): void;
    static enableOnUpdateCardAndNotHiddenAndAdministrator(e: TileEvaluationEventArgs): void;
    static enableOnUpdateAndNotManagementCard(e: TileEvaluationEventArgs): void;
    static enableOnUpdateAndAllowDeleteCard(e: TileEvaluationEventArgs): void;
    static enableOnUpdateCardAndNotHiddenAndNotSingleton(e: TileEvaluationEventArgs): void;
    static copyActionEvaluating(e: TileEvaluationEventArgs): void;
    private static saveCloseAndCreateAction;
    private static refreshAction;
    private static cardActionHistoryAction;
    private static cardStructureAction;
    private static copyLinkAction;
    private static deleteCardAction;
    private static deleteCardActionCore;
    private static createTemplateAction;
    static createCopyAction(): Promise<void>;
}
