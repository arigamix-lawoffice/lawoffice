import { IStorage, StorageObject } from 'tessa/platform/storage';
export declare class KrPermissionsFileSizeLimitSettings extends StorageObject {
    private static readonly limitKey;
    private static readonly categoriesKey;
    private _categories;
    constructor(storage?: IStorage);
    /**
     * Ограничение на размер файла в байтах.
     */
    get limit(): number;
    set limit(value: number);
    /**
     * Список категорий, к которым относится настройка, или <c>null</c>, если она относится ко всем категориям.
     */
    get categories(): guid[];
    set categories(value: guid[]);
}
