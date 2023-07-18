/** Идентификатор типа карточки "OcrSettings". */
export declare const OcrSettingsTypeId: guid;
/** Название типа карточки "OcrSettings". */
export declare const OcrSettingsTypeName = "OcrSettings";
/** Идентификатор типа карточки "OcrOperation". */
export declare const OcrOperationTypeId: guid;
/** Название типа карточки "OcrOperation". */
export declare const OcrOperationTypeName = "OcrOperation";
/** Идентификатор типа диалога "OcrRequestDialog". */
export declare const OcrRequestDialogTypeID: guid;
/** Название типа диалога "OcrRequestDialog". */
export declare const OcrRequestDialogTypeName = "OcrRequestDialog";
/** Ключ, по которому выполняется определение действий, выполняемых в ходе OCR. */
export declare const OcrKey: string;
/** Ключ, по которому хранится идентификатор карточки операции OCR для удаления. */
export declare const OcrDeleteOperationIdKey: string;
/** Интервал (мс) полинга запроса состояния операции. */
export declare const OperationCheckIntervalMilliseconds = 1500;
/** Расширение файла с распознанным текстом. */
export declare const ContentFileExtension = "pdf";
/** Расширение файла с метаинформацией по распознанному файлу. */
export declare const MetadataFileExtension = "json";
/** Список расширений многостраничных файлов, поддерживаемых инструментом распознавания. */
export declare const MultipageFileExtensions: string[];
/** Список расширений файлов, поддерживаемых инструментом распознавания. */
export declare const SupportedFileExtensions: string[];
