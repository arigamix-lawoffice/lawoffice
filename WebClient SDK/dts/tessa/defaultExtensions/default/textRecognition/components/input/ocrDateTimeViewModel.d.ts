import Moment from 'moment';
import { CardTypeEntryControl } from 'tessa/cards/types';
import { DateTimeTypeFormat } from 'ui/datePicker/dateTimeTypeFormat';
import { ICardModel } from 'tessa/ui/cards';
/** Модель представления с данными для контрола выбора выбора даты из календаря. */
export declare class OcrDateTimeViewModel {
    /**
     * Создает экземпляр класса {@link OcrDateTimeViewModel}.
     * @param {CardTypeEntryControl} control
     * Объект, описывающий расположение и свойства элемента
     * управления для привязки к полям строковой секции карточки.
     * @param {ICardModel} _model Модель карточки, доступная в UI.
     */
    constructor(control: CardTypeEntryControl, _model: ICardModel);
    /** Устанавливает минимальную границу даты, дни ДО указанного значения в свойстве будут недоступны. */
    readonly minDate: Moment.Moment | null;
    /** Устанавливает максимальную границу даты, дни ПОСЛЕ указанного значения в свойстве будут недоступны. */
    readonly maxDate: Moment.Moment | null;
    /** Дата, на которой открывается календарь. */
    readonly beginDate: Moment.Moment | null;
    /** Добавляет подсветку для дня в календаре, указанного в {@link beginDate}. */
    readonly highlightBeginDate: boolean;
    /** Признак, показывающий, что дата может быть не указана. */
    readonly dateNullable: boolean;
    /** Признак того, что часовой пояс сотрудника игнорируется и дата всегда указывается как UTC. */
    readonly ignoreTimezone: boolean;
    /** Настройки, определяющие формат отображаемой даты в контроле. */
    readonly dateTimeFormat: DateTimeTypeFormat;
    /** Инструмент для преобразования даты и времени к необходимому формату. */
    private readonly _dateTimeFormatter;
    /** Текущие дата/время, хранящиеся в системе, или `null`, если дата/время не выбраны. */
    storageDate: string | null;
    /** Текущие дата/время, отображаемые элементом управления, или `null`, если дата/время не выбраны. */
    get selectedDate(): Moment.Moment | null;
    set selectedDate(value: Moment.Moment | null);
    /**
     * Текущие дата/время, отображаемые элементом управления представленные
     * в виде строкового значения, или `null`, если дата/время не выбраны.
     */
    get selectedDayString(): string | null;
    /**
     * Текущие дата/время, отображаемые элементом управления представленные
     * в виде отформатированного строкового значения, или `null`, если дата/время не выбраны.
     */
    get selectedDayFormatted(): string | null;
    /** Признак того, что формат контрола поддерживает отображение даты. */
    get isDateFormat(): boolean;
}
