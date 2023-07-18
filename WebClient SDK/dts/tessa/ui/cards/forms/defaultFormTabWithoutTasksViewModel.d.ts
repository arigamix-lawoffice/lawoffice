import { DefaultFormMainViewModel } from './defaultFormMainViewModel';
import { ICardModel } from '../interfaces';
/**
 * Модель представления для формы карточки по умолчанию со вкладками.
 */
export declare class DefaultFormTabWithoutTasksViewModel extends DefaultFormMainViewModel {
    /**
     * Создаёт экземпляр класса с указанием информации,
     * необходимой для создания формы по умолчанию основной части карточки.
     * @param model Модель карточки в UI.
     */
    constructor(model: ICardModel);
}
