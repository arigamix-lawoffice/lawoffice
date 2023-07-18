import { ControlViewModelBase } from './controlViewModelBase';
import { ICardModel } from '../interfaces';
import { CardTypeEntryControl } from 'tessa/cards/types';
import { MediaStyle } from 'ui/mediaStyle';
/**
 * Модель представления для элемента управления типа "Флажок".
 */
export declare class CheckBoxViewModel extends ControlViewModelBase {
    constructor(control: CardTypeEntryControl, model: ICardModel);
    private _fields;
    private _fieldName;
    /**
     * Признак того, что флажок отмечен.
     */
    get isChecked(): boolean;
    set isChecked(value: boolean);
    getControlStyle(): MediaStyle | null;
}
