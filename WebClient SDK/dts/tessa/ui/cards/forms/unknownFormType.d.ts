import { FormTypeBase } from './formTypeBase';
import { ICardModel, IFormWithBlocksViewModel, IFormViewModelBase } from '../interfaces';
import { CardTypeForm, CardTypeControl } from 'tessa/cards/types';
/**
 * Тип формы, используемой в автоматическом UI карточки по умолчанию.
 */
export declare class UnknownFormType extends FormTypeBase {
    constructor(key: string);
    caption: string;
    static get formClass(): string;
    protected createFormCore(form: CardTypeForm, _parentControl: CardTypeControl | null, model: ICardModel): IFormWithBlocksViewModel;
    protected createMainFormCore(model: ICardModel): IFormViewModelBase;
}
