import { IExtension } from 'tessa/extensions';
import { IWorkflowEngineTileManagerUIExtensionContext } from './workflowEngineTileManagerUIExtensionContext';
/**
 * Расширение для модификации отображаемого значения в колонке "Настройки" в таблице кнопок бизнес-процесса.
 */
export interface IWorkflowEngineTileManagerUIExtension extends IExtension {
    /**
     * Метод для модификации значения в колонке "Настройки" для строки из таблицы кнопок бизнес-процесса.
     * @param context Контекст обработки.
     */
    modifyButtonRow(context: IWorkflowEngineTileManagerUIExtensionContext): Promise<void>;
}
/**
 * @inheritDoc
 */
export declare abstract class WorkflowEngineTileManagerUIExtension implements IWorkflowEngineTileManagerUIExtension {
    static readonly type = "WorkflowEngineTileManagerUIExtension";
    private _checkId;
    /**
     * Метод для получения id наследуемых расширений.
     * @returns id наследуемого расширения.
     */
    abstract getExtensionId(): guid;
    /**
     * Метод, проверяющий следует ли продолжить выполнение расширений.
     * @param context Контекст обработки.
     * @returns `true` продолжает выполнение, `false` прерывает.
     */
    shouldExecute(context: IWorkflowEngineTileManagerUIExtensionContext): boolean;
    modifyButtonRow(_context: IWorkflowEngineTileManagerUIExtensionContext): Promise<void>;
}
