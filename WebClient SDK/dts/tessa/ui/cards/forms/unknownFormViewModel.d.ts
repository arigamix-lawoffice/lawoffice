import { FormViewModel } from './formViewModel';
import { ICardModel } from '../interfaces';
import { CardTypeForm } from 'tessa/cards/types';
/**
 * Модель представления для формы по умолчанию основной части карточки.
 */
export declare class UnknownFormViewModel extends FormViewModel {
    constructor(form: CardTypeForm, model: ICardModel);
}
