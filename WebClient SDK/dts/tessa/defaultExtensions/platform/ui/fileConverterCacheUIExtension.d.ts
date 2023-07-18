import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
export declare class FileConverterCacheUIExtension extends CardUIExtension {
    shouldExecute(context: ICardUIExtensionContext): boolean;
    initialized(context: ICardUIExtensionContext): void;
    /**
     * Количество дней, которые к файлам не должно было быть обращений, чтобы их можно было удалять.
     * Используется только для кнопки "Удалить старые файлы".
     */
    private FilesToRemoveWithoutAccessDays;
    /**
     * Самая поздняя разрешённая дата, когда выполнялось обращение к файлу в кэше.
     * Все файлы, к которым обращались раньше это даты, будут удалены из кэша.
     * Укажите такую дату по этому ключу в запросе на сохранение карточки файловых конвертеров.
     * Значение <c>null</c> указывает, что из кэша будут удалены все файлы.
     */
    private OldestPreviewRequestTimeKey;
    private removeOldFilesAction;
    private removeAllFilesAction;
    private removeFilesFromButtonAsync;
}
