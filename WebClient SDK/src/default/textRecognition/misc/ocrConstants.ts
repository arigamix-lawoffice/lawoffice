import { userKeyPrefix } from 'tessa/cards/cardHelper';

//#region card types

/** Идентификатор типа карточки "OcrSettings". */
export const OcrSettingsTypeId: guid = 'b3c9c077-8f51-47fc-ba36-0247f71d6b0f';

/** Название типа карточки "OcrSettings". */
export const OcrSettingsTypeName = 'OcrSettings';

/** Идентификатор типа карточки "OcrOperation". */
export const OcrOperationTypeId: guid = '8275fa2d-91d6-462f-8a1a-189c9b57720e';

/** Название типа карточки "OcrOperation". */
export const OcrOperationTypeName = 'OcrOperation';

/** Идентификатор типа диалога "OcrRequestDialog". */
export const OcrRequestDialogTypeID: guid = '51488d15-c19c-4855-8bec-7cc26936e9d6';

/** Название типа диалога "OcrRequestDialog". */
export const OcrRequestDialogTypeName = 'OcrRequestDialog';

//#endregion

//#region keys

/** Ключ, по которому выполняется определение действий, выполняемых в ходе OCR. */
export const OcrKey = `${userKeyPrefix}ocr`;

/** Ключ, по которому хранится идентификатор карточки операции OCR для удаления. */
export const OcrDeleteOperationIdKey = `${OcrKey}DeleteOperationID`;

/** Интервал (мс) полинга запроса состояния операции. */
export const OperationCheckIntervalMilliseconds = 1500;

/** Расширение файла с распознанным текстом. */
export const ContentFileExtension = 'pdf';

/** Расширение файла с метаинформацией по распознанному файлу. */
export const MetadataFileExtension = 'json';

/** Список расширений многостраничных файлов, поддерживаемых инструментом распознавания. */
export const MultipageFileExtensions: string[] = [ContentFileExtension, 'gif', 'tif', 'tiff'];

/** Список расширений файлов, поддерживаемых инструментом распознавания. */
export const SupportedFileExtensions: string[] = [
  ...MultipageFileExtensions,
  'bmp',
  'pnm',
  'png',
  'jpg',
  'jpeg',
  'jfif'
];

//#endregion
