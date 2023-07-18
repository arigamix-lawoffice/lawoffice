import { DateTimeTypeFormat } from 'ui/datePicker/dateTimeTypeFormat';
import { IControlViewModel } from 'tessa/ui/cards';
import { OcrValidationResult } from './ocrValidationResult';
import { OcrValidator } from './ocrValidator';
/** Валидатор значения даты и времени. */
export declare class OcrDateTimeValidator extends OcrValidator {
    /** Признак того, что часовой пояс сотрудника игнорируется и дата всегда указывается как UTC. */
    private readonly _ignoreTimezone;
    /** Настройки, определяющие формат отображаемой даты в контроле. */
    private readonly _dateTimeFormat;
    /** Инструмент для преобразования даты и времени к необходимому формату. */
    private readonly _dateTimeFormatter;
    /**
     * Создает экземпляр класса {@link OcrDateTimeValidator}.
     * @param {IControlViewModel} _control Модель представления проверяемого контрола.
     * @param {DateTimeTypeFormat} dateTimeFormat Настройки, определяющие формат отображаемой даты в контроле.
     * @param {IControlViewModel} ignoreTimezone Признак того, что часовой пояс сотрудника игнорируется и дата всегда указывается как UTC.
     */
    constructor(_control: IControlViewModel, dateTimeFormat: DateTimeTypeFormat, ignoreTimezone: boolean);
    protected get patterns(): ReadonlyArray<RegExp>;
    protected get error(): string;
    protected compileValue(match: RegExpExecArray): string;
    private formatDate;
    validate(value: string): OcrValidationResult;
}
