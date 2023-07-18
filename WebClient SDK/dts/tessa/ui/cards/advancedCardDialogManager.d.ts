import { ICardEditorModel } from 'tessa/ui/cards/interfaces';
import { IUIContextActionOverridings } from 'tessa/ui/uiContext';
import { OpenCardArg, ShowCardArg, CreateCardArg, ShowCardModelArg } from 'tessa/ui/uiHost/common';
export declare const CardDefaultDialog = "DefaultDialog";
export declare class AdvancedCardDialogManager {
    private constructor();
    private static _instance;
    static get instance(): AdvancedCardDialogManager;
    openCard(args: OpenCardArg): Promise<void>;
    private openCardOverride;
    showCard(args: ShowCardArg): Promise<void>;
    showCardModel(args: ShowCardModelArg): Promise<void>;
    private showCardOverride;
    createCard(args: CreateCardArg): Promise<void>;
    private createCardOverride;
    private showCardCore;
    createUIContextActionOverridings(): IUIContextActionOverridings;
    private onDialogClosing;
    private invokeDialogClosingAction;
    private invokeDialogClosingBeforeSavingAction;
    /**
     * Создаёт карточку по шаблону и открывает её в диалоге.
     * При создании по шаблону используются и клиентские, и серверные расширения.
     *
     * @param {guid} templateId Идентификатор шаблона, по которому создаётся карточка.
     * @param {CreateCardArg} args Настройки создания карточки.
     * @returns {Promise<ICardEditorModel | null>} {@link ICardEditorModel} карточки, созданной по шаблону, или null, если произошла ошибка.
     */
    createFromTemplate(templateId: guid, args: CreateCardArg): Promise<ICardEditorModel | null>;
}
