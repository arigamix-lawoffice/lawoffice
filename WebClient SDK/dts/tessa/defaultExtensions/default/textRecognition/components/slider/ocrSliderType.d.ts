import { CardControlType } from 'tessa/cards';
import { CardTypeBlock, CardTypeControl, CardTypeForm } from 'tessa/cards/types';
import { ControlTypeBase } from 'tessa/ui/cards/controls';
import { ICardModel, IControlViewModel } from 'tessa/ui/cards';
/** Тип контрола ползунка для ввода вещественных значений. */
export declare class OcrSliderType extends ControlTypeBase {
    protected createControlCore(control: CardTypeControl, _block: CardTypeBlock, _form: CardTypeForm, _parentControl: CardTypeControl | null, model: ICardModel): IControlViewModel;
}
/** Экземпляр типа контрола ползунка для ввода вещественных значений. */
export declare const OcrSliderControlType: CardControlType;
