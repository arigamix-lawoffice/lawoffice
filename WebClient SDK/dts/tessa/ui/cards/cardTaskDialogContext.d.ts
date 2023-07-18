import { ICardEditorModel } from './interfaces';
import { CardTaskCompletionOptionSettings } from 'tessa/cards';
import { IValidationResultBuilder } from 'tessa/platform/validation';
/**
 * Объект, предоставляющий информацию по открытому диалогу.
 */
export declare class CardTaskDialogContext {
    readonly mainCardEditor: ICardEditorModel;
    readonly mainCardID: guid;
    readonly taskId: guid;
    readonly taskTypeId: guid;
    readonly dialogSettings: CardTaskCompletionOptionSettings;
    readonly validationResult: IValidationResultBuilder;
    readonly onButtonPressedAction: CardTaskDialogOnButtonPressedFunc | null;
    /**
     * Инициализирует новый экземпляр класса {@link CardTaskDialogContext}.
     * @param {ICardEditorModel} mainCardEditor Редактируемое представление карточки, в которой открыт диалог.
     * @param {guid} mainCardID Идентификатор карточки, в которой открыт диалог.
     * @param {guid} taskId Идентификатор задания диалога.
     * @param {guid} taskTypeId Идентификатор типа задания диалога.
     * @param {CardTaskCompletionOptionSettings} dialogSettings {@link CardTaskCompletionOptionSettings}
     * @param {IValidationResultBuilder} validationResult {@link IValidationResultBuilder}
     * @param {(CardTaskDialogOnButtonPressedFunc | null)} onButtonPressedAction Функция, выполняемая при нажатии на кнопку в диалоге вместо стандартного действия или значение null, если выполняется стандартное действие.
     */
    constructor(mainCardEditor: ICardEditorModel, mainCardID: guid, taskId: guid, taskTypeId: guid, dialogSettings: CardTaskCompletionOptionSettings, validationResult: IValidationResultBuilder, onButtonPressedAction: CardTaskDialogOnButtonPressedFunc | null);
}
/**
 * Функция, выполняемая при нажатии на кнопку в диалоге вместо стандартного действия.
 */
export declare type CardTaskDialogOnButtonPressedFunc = (dialogCardEditor: ICardEditorModel, settings: CardTaskCompletionOptionSettings, buttonName: string | null, cancel: boolean) => Promise<void>;
