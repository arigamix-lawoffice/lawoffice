import { SliderViewModel } from './29_sliderViewModel';
import { ControlTypeBase } from 'tessa/ui/cards/controls';
import { ICardModel, IControlViewModel } from 'tessa/ui/cards';
import {
  CardTypeForm,
  CardTypeBlock,
  CardTypeControl,
  CardTypeEntryControl
} from 'tessa/cards/types';

export class SliderType extends ControlTypeBase {
  protected createControlCore(
    control: CardTypeControl,
    _block: CardTypeBlock,
    _form: CardTypeForm,
    _parentControl: CardTypeControl | null,
    model: ICardModel
  ): IControlViewModel {
    return new SliderViewModel(control as CardTypeEntryControl, model);
  }
}
