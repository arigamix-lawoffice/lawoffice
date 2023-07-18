export declare enum DialogType {
    /**
     * Диалог открывается для выбора любых файлов
     */
    File = 0,
    /**
     * Диалог открывается для выбора изображений
     */
    Image = 1
}
export declare function getFileTypeFilterByDialogType(dialogType: DialogType): string | undefined;
