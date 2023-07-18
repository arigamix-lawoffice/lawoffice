import { ICardUIExtensionContext, CardUIExtension } from 'tessa/ui/cards';
/**
 * При сохранении выбранного типа карточки добавленые/измененные/подписанные файлы файлового
 * контрола задания переносятся в основную карточку.
 *
 * Дополнительная информация по данному расширению находится в документации kb_106.md.
 *
 * Результат работы расширения:
 * Для карточки "Автомобиль":
 * - добавьте файловый контрол для задачи "Тестовое согласование"
 * - создайте задачу через тайл на левой панели - “Тестовое согласование”.
 * - возьмите её в работу, добавьте файл в файловый контрол.
 * - при сохранении карточки файлы из файлового контрола задания переместятся в основную карточку.
 */
export declare class TaskEnableAttachFilesExampleUIExtension extends CardUIExtension {
    saving(context: ICardUIExtensionContext): Promise<void>;
}
