import { DefaultFormMainViewModel } from './defaultFormMainViewModel';
import { ICardModel } from '../interfaces';
import { TaskHistoryViewModel } from 'tessa/ui/cards/tasks';
/**
 * Форма карточки с историей заданий.
 */
export declare class DefaultFormTabWithTaskHistoryViewModel extends DefaultFormMainViewModel {
    /**
     * Создаёт экземпляр класса с указанием информации,
     * необходимой для создания формы по умолчанию основной части карточки.
     * @param model Модель карточки в UI.
     */
    constructor(model: ICardModel);
    readonly taskHistory: TaskHistoryViewModel;
    protected initializeCore(): void;
}
