import { CardControlType, CardControlTypeFlags, CardControlTypeUsageMode } from 'tessa/cards';
import {
  CardTypeBlock,
  CardTypeControl,
  CardTypeEntryControl,
  CardTypeForm
} from 'tessa/cards/types';
import { ControlTypeBase } from 'tessa/ui/cards/controls';
import { ICardModel, IControlViewModel } from 'tessa/ui/cards';
import { OcrInputViewModel } from './ocrInputViewModel';

/** Тип контрола редактируемого поля. */
export class OcrInputType extends ControlTypeBase {
  protected createControlCore(
    control: CardTypeControl,
    _block: CardTypeBlock,
    _form: CardTypeForm,
    _parentControl: CardTypeControl | null,
    model: ICardModel
  ): IControlViewModel {
    return new OcrInputViewModel(control as CardTypeEntryControl, model);
  }
}

/** Экземпляр типа контрола редактируемого поля. */
export const OcrInputControlType = new CardControlType(
  '72f586f4-1aa3-4088-aa5b-95546241ecec',
  'OcrInput',
  CardControlTypeUsageMode.Entry,
  CardControlTypeFlags.UseEverywhere
);
