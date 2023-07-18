import { IUIContext } from 'tessa/ui';
export interface IFileTemplateDialogManager {
    /**
     * Создает диалог создания файлов по шаблону и запускает логику финальной стадии обработки результата диалога.
     * @param uiContext UI контекст.
     * @param finalStageLogicFuncAsync Функция выполняющая финальную стадию обработки результата диалога.
     */
    createAndProcessDialogAsync(uiContext: IUIContext, finalStageLogicFuncAsync?: (file: File) => Promise<void>): Promise<void>;
}
/**
 * Предоставляет логику для работы с диалогом создания файлов по шаблону.
 */
export declare class FileTemplateDialogManager implements IFileTemplateDialogManager {
    constructor();
    private static _instance;
    static get instance(): FileTemplateDialogManager;
    createAndProcessDialogAsync(uiContext: IUIContext, finalStageLogicFuncAsync?: (file: File) => Promise<void>): Promise<void>;
    private static createFileTemplateViewAction;
    private static createFileTemplateCardAction;
    private static fillFromStorage;
}
