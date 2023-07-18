import { CardTypeForm } from 'tessa/cards/types';
import { DefaultFormViewModel } from './defaultFormViewModel';
import { DefaultFormSettings } from './defaultFormSettings';
import { ICardModel, IBlockViewModel } from '../interfaces';
/**
 * Модель представления для формы по умолчанию, которая создаётся
 * для редактирования строки коллекционной, древовидной секции или отображения таска.
 */
export declare class DefaultFormSimpleViewModel extends DefaultFormViewModel {
    /**
     * Создаёт экземпляр класса с указанием метаинформации по форме, модели карточки
     * и списка моделей представления блоков внутри формы.
     * @param form Метаинформация по форме.
     * @param model Модель карточки в UI.
     * @param blocks Коллекция моделей представления блоков внутри формы.
     * @param formSettings Настройки для формы по умолчанию.
     */
    constructor(form: CardTypeForm, model: ICardModel, blocks: IBlockViewModel[], formSettings: DefaultFormSettings);
    private _modelInitializing;
    protected initializeCore(): void;
}
