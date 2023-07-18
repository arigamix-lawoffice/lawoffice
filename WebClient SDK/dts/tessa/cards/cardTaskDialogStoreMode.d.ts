/**
 * Перечисление режимов сохранения карточки диалога.
 */
export declare enum CardTaskDialogStoreMode {
    /**
     * Запись карточки диалога в объекта CardTask.info.
     */
    Info = 0,
    /**
     * Запись карточки диалога в CardTask.settings.
     */
    Settings = 1,
    /**
     * Сохранение карточки диалога как отдельной карточки.
     */
    Card = 2
}
