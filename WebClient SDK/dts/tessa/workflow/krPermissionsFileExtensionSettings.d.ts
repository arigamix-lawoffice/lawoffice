import { ArrayStorage, IStorage, StorageObject } from 'tessa/platform/storage';
import { KrPermissionsFileSizeLimitSettings } from './krPermissionsFileSizeLimitSettings';
export declare class KrPermissionsFileExtensionSettings extends StorageObject {
    private static readonly flagKey;
    private static readonly allowedCategoriesKey;
    private static readonly disallowedCategoriesKey;
    private static readonly signAllowedCategoriesKey;
    private static readonly signDisallowedCategoriesKey;
    private static readonly sizeLimitSettingsKey;
    private _allowedCategories;
    private _disallowedCategories;
    private _signAllowedCategories;
    private _signDisallowedCategories;
    private static readonly sizeLimitSettingsFactory;
    constructor(extension: string, storage?: IStorage);
    /**
     * Расширение, для которого применяется настройка.
     */
    readonly extension: string;
    /**
     * Флаг доступа новых файлов.
     */
    get flag(): number;
    set flag(value: number);
    /**
     * Определяет, что добавление файлов для данного расширения файлов разрешено для всех категорий, кроме DisallowedCategories.
     */
    get addAllowed(): boolean;
    /**
     * Определяет, что добавление файлов для данного расширения файлов запрещено для всех категорий, кроме AllowedCategories.
     */
    get addDisallowed(): boolean;
    /**
     * Определяет, что подписание файлов для данного расширения файлов разрешено для всех категорий, кроме SignDisallowedCategories.
     */
    get signAllowed(): boolean;
    /**
     * Определяет, что подписание файлов для данного расширения файлов запрещено для всех категорий, кроме SignAllowedCategories.
     */
    get signDisallowed(): boolean;
    /**
     * Список категорий, доступных для использования. Может иметь значение null, тогда доступны все категории,
     * если isAllowed имеет значение true.
     */
    get allowedCategories(): guid[];
    set allowedCategories(value: guid[]);
    /**
     * Список категорий, использование которых недоступно. Может иметь значение null, тогда недоступны все категории,
     * если isDisallowed имеет значение true.
     */
    get disallowedCategories(): guid[];
    set disallowedCategories(value: guid[]);
    /**
     * Список категорий, доступных для использования при подписании файлов. Может иметь значение null, тогда доступны все категории,
     * если SignAllowed имеет значение true.
     */
    get signAllowedCategories(): guid[];
    set signAllowedCategories(value: guid[]);
    /**
     * Список категорий, недоступных  для использования при подписании файлов. Может иметь значение null, тогда недоступны все категории,
     * если SignDisallowed имеет значение true.
     */
    get signDisallowedCategories(): guid[];
    set signDisallowedCategories(value: guid[]);
    /**
     * Список настроек с ограничениями размера файлов. Создаёт пустой список, если он задан.
     */
    get sizeLimitSettings(): ArrayStorage<KrPermissionsFileSizeLimitSettings>;
    set fileSettings(value: ArrayStorage<KrPermissionsFileSizeLimitSettings>);
    /**
     * Метод для получения списка настроек с ограничениями размера файлов.
     * @returns Список настроек с ограничениями размера файлов или null, если список не задан.
     */
    tryGetSizeLimitSettings(): ArrayStorage<KrPermissionsFileSizeLimitSettings> | null | undefined;
}
