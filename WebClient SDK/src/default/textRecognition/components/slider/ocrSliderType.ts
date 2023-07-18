import { CardControlType, CardControlTypeFlags, CardControlTypeUsageMode } from 'tessa/cards';
import {
  CardTypeBlock,
  CardTypeControl,
  CardTypeEntryControl,
  CardTypeForm
} from 'tessa/cards/types';
import { ControlTypeBase } from 'tessa/ui/cards/controls';
import { ICardModel, IControlViewModel } from 'tessa/ui/cards';
import { OcrSliderViewModel } from './ocrSliderViewModel';

/** Тип контрола ползунка для ввода вещественных значений. */
export class OcrSliderType extends ControlTypeBase {
  protected createControlCore(
    control: CardTypeControl,
    _block: CardTypeBlock,
    _form: CardTypeForm,
    _parentControl: CardTypeControl | null,
    model: ICardModel
  ): IControlViewModel {
    return new OcrSliderViewModel(control as CardTypeEntryControl, model);
  }
}

/** Экземпляр типа контрола ползунка для ввода вещественных значений. */
export const OcrSliderControlType = new CardControlType(
  '56f90200-63a0-4557-a7c1-053b30a7ecda',
  'OcrSlider',
  CardControlTypeUsageMode.Entry,
  CardControlTypeFlags.UseEverywhere
);
