import { FormTypeBase } from './formTypeBase';
import { ICardModel, IFormWithBlocksViewModel, IFormViewModelBase } from '../interfaces';
import { CardTypeControl, CardTypeForm } from 'tessa/cards/types';
/**
 * Тип формы, используемой в автоматическом UI карточки по умолчанию.
 */
export declare class DefaultFormType extends FormTypeBase {
    static get formClass(): string;
    protected createFormCore(form: CardTypeForm, parentControl: CardTypeControl | null, model: ICardModel): IFormWithBlocksViewModel;
    protected createMainFormCore(model: ICardModel): IFormViewModelBase;
}
