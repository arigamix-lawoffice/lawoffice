import { IFormType } from '../formTypeRegistry';
import { ICardModel, IFormWithBlocksViewModel, IFormViewModelBase } from '../interfaces';
import { CardTypeForm, CardTypeControl } from 'tessa/cards/types';
/**
 * Базовый класс для типа формы, используемой в автоматическом UI карточки по умолчанию.
 */
export declare abstract class FormTypeBase implements IFormType {
    protected abstract createFormCore(form: CardTypeForm, parentControl: CardTypeControl | null, model: ICardModel): IFormWithBlocksViewModel;
    createForm(form: CardTypeForm, parentControl: CardTypeControl | null, model: ICardModel, skipInitialization?: boolean): IFormWithBlocksViewModel;
    protected abstract createMainFormCore(model: ICardModel): IFormViewModelBase;
    createMainForm(model: ICardModel): IFormViewModelBase;
}
