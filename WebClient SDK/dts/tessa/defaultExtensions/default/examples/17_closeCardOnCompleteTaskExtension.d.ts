import { CardStoreExtension, ICardStoreExtensionContext } from 'tessa/cards/extensions';
import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
/**
 * Закрывает карточку выбранного типа после завершения определенного типа задания.
 *
 * Результат работы расширения:
 * В карточке "Автомобиль":
 * - Создайте задачу через тайл на левой панели - “Тестовое согласование”.
 * - Затем возьмите её в работу, выберите один из существующих вариантов. При выборе варианта
 * задача завершится и карточка закроется.
 */
export declare class CloseCardOnCompleteTaskStoreExtension extends CardStoreExtension {
    private _taskComplete;
    beforeRequest(context: ICardStoreExtensionContext): void;
    afterRequest(context: ICardStoreExtensionContext): void;
}
export declare class CloseCardOnCompleteTaskUIExtension extends CardUIExtension {
    reopening(context: ICardUIExtensionContext): void;
}
