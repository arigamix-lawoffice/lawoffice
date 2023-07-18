/**
 * Тип конвертера для преоброзования файлов в другие форматы
 */
export declare enum FileConverterType {
    /**
     * Не используется
     */
    None = 0,
    /**
     * Используется Libre/OpenOffice
     */
    LibreOffice = 1,
    /**
     * Используется сервис
     */
    OnlyOfficeService = 2,
    /**
     * Используется Document Builder
     */
    OnlyOfficeDocumentBuilder = 3
}
