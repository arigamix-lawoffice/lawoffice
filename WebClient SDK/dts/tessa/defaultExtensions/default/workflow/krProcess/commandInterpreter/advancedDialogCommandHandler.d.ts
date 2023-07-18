import { ClientCommandHandlerBase } from 'tessa/workflow/krProcess/clientCommandInterpreter';
import { IClientCommandHandlerContext } from 'tessa/workflow/krProcess';
import { CardTaskCompletionOptionSettings, CardTaskDialogActionResult } from 'tessa/cards';
import { ICardEditorModel } from 'tessa/ui/cards';
/**
 * Базовый класс обработчика клиентской команды отображения диалога.
 */
export declare abstract class AdvancedDialogCommandHandler extends ClientCommandHandlerBase {
    handle(context: IClientCommandHandlerContext): Promise<void>;
    /**
     * Метод для подготовки диалога для выполнения.
     *
     * @param {IClientCommandHandlerContext} context Контекст обработки клиентской команды.
     * @returns {CardTaskCompletionOptionSettings | null} Информация для формирования диалога, или значение null, если невозможно сформировать диалог.
     */
    protected abstract prepareDialogCommand(context: IClientCommandHandlerContext): CardTaskCompletionOptionSettings | null;
    /**
     * Метод выполнения диалога.
     *
     * @param {CardTaskDialogActionResult} actionResult Результат выполнения диалога.
     * @param {IClientCommandHandlerContext} context Контекст команды диалога.
     * @param {ICardEditorModel} cardEditor Редактор карточки диалога.
     * @param {(ICardEditorModel | null)} parentCardEditor Редактор карточки, для которой открывается диалог, если диалог открывается в рамках карточки.
     * @returns {Promise<boolean>} Значение true, если необходимо закрыть диалог, иначе false.
     */
    protected abstract completeDialogCore(actionResult: CardTaskDialogActionResult, context: IClientCommandHandlerContext, cardEditor: ICardEditorModel, parentCardEditor: ICardEditorModel | null): Promise<boolean>;
    /**
     * Отображает карточку в окне диалога.
     *
     * @param {CardTaskCompletionOptionSettings} coSettings Параметры диалога.
     * @param {IClientCommandHandlerContext} context Контекст обработки клиентской команды.
     */
    private showGlobalDialog;
    private prepareDialog;
    private getButtonAction;
    private completeDialog;
    /**
     * Задаёт контент указанных файлов в соответствующие CardFile.Info карточки файлов.
     *
     * @param {Card} dialogCard Карточка диалога.
     * @param {Readonly<IFile[]>} files Коллекция файлов.
     * @returns {Promise<ValidationResult>} Результат выполнения операции, агрегированный для всех файлов.
     */
    private static prepareFilesForStore;
    /**
     * Создаёт и открывает карточку в диалоге. Карточка создаётся в режиме по умолчанию или по шаблону.
     *
     * @param {CardTaskCompletionOptionSettings} dialogSettings {@link CardTaskCompletionOptionSettings}
     * @param {IStorage} info {@link IStorage}
     * @param {(context: CardEditorCreationContext) => void} cardEditorModifierAction
     * @param {(context: CardEditorCreationContext) => void} cardModifierAction
     */
    private static createNewCard;
}
