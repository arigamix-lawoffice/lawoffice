import { ControlTypeBase } from '../controlTypeBase';
import { ICardModel, IControlViewModel } from 'tessa/ui/cards';
import { CardTypeForm, CardTypeBlock, CardTypeControl } from 'tessa/cards/types';
export declare class HtmlViewerType extends ControlTypeBase {
    protected createControlCore(control: CardTypeControl, _block: CardTypeBlock, _form: CardTypeForm, _parentControl: CardTypeControl | null, model: ICardModel): IControlViewModel;
}
