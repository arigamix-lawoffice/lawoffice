import { ControlTypeBase } from './controlTypeBase';
import { ICardModel, IControlViewModel } from '../interfaces';
import { CardTypeForm, CardTypeBlock, CardTypeControl } from 'tessa/cards/types';
export declare class UnknownControlType extends ControlTypeBase {
    caption: string;
    constructor(key: string);
    protected createControlCore(control: CardTypeControl, _block: CardTypeBlock, _form: CardTypeForm, _parentControl: CardTypeControl | null, _model: ICardModel): IControlViewModel;
}
