import { CardTypeEntryControl } from 'tessa/cards/types';
import { ControlViewModelBase } from 'tessa/ui/cards/controls';
import { ICardModel } from 'tessa/ui/cards';
/** Модель-представление контрола ползунка для ввода вещественных значений. */
export declare class OcrSliderViewModel extends ControlViewModelBase {
    /** Хранилище с данными полей, среди которых имеется отслеживаемое поле. */
    private _fields;
    /** Название отслеживаемого поля. */
    private _fieldName;
    /** Минимальное значение. */
    readonly minValue: number;
    /** Максимальное значение. */
    readonly maxValue: number;
    /** Шаг смещения значения. */
    readonly step: number;
    /** Текущее значение. */
    get value(): number;
    set value(value: number);
    /**
     * Создает экземпляр класса {@link OcrSliderViewModel}.
     * @param {CardTypeEntryControl} control
     * Объект, описывающий расположение и свойства элемента
     * управления для привязки к полям строковой секции карточки.
     * @param {ICardModel} model Модель карточки, доступная в UI.
     */
    constructor(control: CardTypeEntryControl, model: ICardModel);
}
