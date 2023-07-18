import { ControlTypeBase } from './../controlTypeBase';
import { ICardModel, IControlViewModel } from './../../interfaces';
import { CardTypeBlock, CardTypeControl, CardTypeForm } from 'tessa/cards/types';
export declare class ViewControlType extends ControlTypeBase {
    protected createControlCore(control: CardTypeControl, _block: CardTypeBlock, _form: CardTypeForm, _parentControl: CardTypeControl | null, model: ICardModel): IControlViewModel;
    protected controlInitialization(viewModel: IControlViewModel, model: ICardModel, skipInitialization: boolean): void;
}
