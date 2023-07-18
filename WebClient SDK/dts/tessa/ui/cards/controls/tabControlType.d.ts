import { ControlTypeBase } from './controlTypeBase';
import { ICardModel, IControlViewModel } from '../interfaces';
import { CardTypeForm, CardTypeBlock, CardTypeControl } from 'tessa/cards/types';
export declare class TabControlType extends ControlTypeBase {
    protected createControlCore(control: CardTypeControl, _block: CardTypeBlock, _form: CardTypeForm, parentControl: CardTypeControl | null, model: ICardModel): IControlViewModel;
}
