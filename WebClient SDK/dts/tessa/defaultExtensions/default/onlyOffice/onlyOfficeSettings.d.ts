/**
 * Настройки интеграции с OnlyOffice.
 */
export interface OnlyOfficeSettings {
    readonly apiScriptUrl: string;
    readonly previewEnabled: boolean;
    readonly excludedPreviewFormats: string[];
}
