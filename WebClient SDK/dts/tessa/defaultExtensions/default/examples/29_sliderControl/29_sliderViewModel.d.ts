import { ControlViewModelBase } from 'tessa/ui/cards/controls';
import { ICardModel } from 'tessa/ui/cards';
import { CardTypeEntryControl } from 'tessa/cards/types';
export declare class SliderViewModel extends ControlViewModelBase {
    constructor(control: CardTypeEntryControl, model: ICardModel);
    private _fields;
    private _fieldName;
    readonly minValue: number;
    readonly maxValue: number;
    readonly step: number;
    get value(): number;
    set value(value: number);
}
