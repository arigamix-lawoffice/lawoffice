import { IValidationResultBuilder } from 'tessa/platform/validation';
export declare class AccessSettings {
    static readonly AllowEdit = 0;
    static readonly DisallowEdit = 1;
    static readonly DisallowRowAdding = 2;
    static readonly DisallowRowDeleting = 3;
    static readonly MaskData = 4;
}
export declare class MandatoryValidationType {
    static readonly Always = 0;
    static readonly OnTaskCompletion = 1;
    static readonly WhenOneFieldFilled = 2;
}
export declare class ControlType {
    static readonly Tab = 0;
    static readonly Block = 1;
    static readonly Control = 2;
}
export declare class FileAccessSettings {
    static readonly FileNotAvailable = 0;
    static readonly ContentNotAvailable = 1;
    static readonly OnlyLastVersion = 2;
    static readonly OnlyLastAndOwnVersions = 3;
    static readonly AllVersions = 4;
    static readonly InfoKey = ".FileAccessSettings";
}
export declare enum FileEditAccessSettings {
    Disallowed = 0,
    Allowed = 1
}
/** <summary>
 * Тип действия, производимого над файлом, которое привело к ошибке.
 */
export declare enum KrPermissionsErrorAction {
    /**
     * Добавление нового файла.
     */
    AddFile = 0,
    /**
     * Изменение файла.
     */
    EditFile = 1,
    /**
     * Замена файла.
     */
    ReplaceFile = 2,
    /**
     * Изменение категории файла.
     */
    ChangeCategory = 3
}
/**
 * Тип ошибки, возникшей при проверке файла.
 */
export declare enum KrPermissionsErrorType {
    /**
     * Действие запрещено.
     */
    NotAllowed = 0,
    /**
     * Ошибка размера файла.
     */
    FileTooBig = 1
}
/**
 * Идентификатор категории файла, используемый при проверке доступа к файлу в ситуации, когда у файла не задана категория.
 */
export declare const noCategoryFilesCategoryId: string;
/**
 * Метод для добавления ошибки доступа к файлу.
 * @param validationResultBuilder Билдер результата валидации, куда записывается ошибка.
 * @param validationObject Объект валидации, указываемый в ошибке.
 * @param errorAction Тип действия работы с файлом, который привёл к ошибке.
 * @param errorType Тип ошибки.
 * @param fileName Имя файла.
 * @param fileExtension Расширение файла или <c>null</c>, если требуется определить расширение файла по его имени.
 * @param replacedFileName Имя заменяемого файла. Используется, если <paramref name="errorAction"/> имеет значение <see cref="KrPermissionsErrorAction.ReplaceFile"/>.
 * @param categoryCaption Текст категории файла или <c>null</c>, если файл без категории.
 * @param sizeLimit Ограничение на размер файла в байтах. Используется, если <paramref name="errorType"/> имеет значение <see cref="KrPermissionsErrorType.FileTooBig"/>.
 */
export declare function addFileValidationError(validationResultBuilder: IValidationResultBuilder, errorAction: KrPermissionsErrorAction, errorType: KrPermissionsErrorType, fileName: string, fileExtension?: string | null, replacedFileName?: string | null, categoryCaption?: string | null, sizeLimit?: number | null): void;
