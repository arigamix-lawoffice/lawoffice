import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
/**
 * Расширение UI для типа карточки "Шаблон бизнес-процесса".
 */
export declare class BusinessProcessUIExtension extends CardUIExtension {
    private _extensionsSectionRowsListener;
    private _disposes;
    initialized(context: ICardUIExtensionContext): Promise<void>;
    finalized(): void;
    private static updateAllButtonsSettings;
    private static updateButtonSettings;
    private static initGrid;
    private static formatButtonCell;
    private static initRow;
    private static invalidateRow;
    private static onTableRowOpening;
    private static compileAllButtons;
    private static onCompileButtonPressed;
    private static compile;
}
