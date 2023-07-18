import { ICloneable } from 'tessa/platform/cloneable';
import { IStorage } from 'tessa/platform/storage/storage';
import { StorageObject } from 'tessa/platform/storage/storageObject';
import { IStorageValueFactory } from 'tessa/platform/storage/storageValueFactory';
import { DateFormat } from './dateFormat';
export interface IFormattingSettings {
    name: string;
    caption: string;
    dateFormat: DateFormat;
    dateSeparator: string;
    daysWithLeadingZero: boolean;
    monthsWithLeadingZero: boolean;
    hoursWithLeadingZero: boolean;
    time24Hour: boolean;
    timeSeparator: string;
    timeAmDesignator: string | null;
    timePmDesignator: string | null;
    numberGroupSeparator: string;
    numberDecimalSeparator: string;
}
export declare class FormattingSettingsStorageObject extends StorageObject implements IFormattingSettings, ICloneable<FormattingSettingsStorageObject> {
    constructor(storage?: IStorage);
    static nameKey: string;
    static captionKey: string;
    static dateFormatKey: string;
    static dateSeparatorKey: string;
    static daysWithLeadingZeroKey: string;
    static monthsWithLeadingZeroKey: string;
    static hoursWithLeadingZeroKey: string;
    static time24HourKey: string;
    static timeSeparatorKey: string;
    static timeAmDesignatorKey: string;
    static timePmDesignatorKey: string;
    static numberGroupSeparatorKey: string;
    static numberDecimalSeparatorKey: string;
    get name(): string;
    set name(value: string);
    get caption(): string;
    set caption(value: string);
    get dateFormat(): DateFormat;
    set dateFormat(value: DateFormat);
    get dateSeparator(): string;
    set dateSeparator(value: string);
    get daysWithLeadingZero(): boolean;
    set daysWithLeadingZero(value: boolean);
    get monthsWithLeadingZero(): boolean;
    set monthsWithLeadingZero(value: boolean);
    get hoursWithLeadingZero(): boolean;
    set hoursWithLeadingZero(value: boolean);
    get time24Hour(): boolean;
    set time24Hour(value: boolean);
    get timeSeparator(): string;
    set timeSeparator(value: string);
    get timeAmDesignator(): string | null;
    set timeAmDesignator(value: string | null);
    get timePmDesignator(): string | null;
    set timePmDesignator(value: string | null);
    get numberGroupSeparator(): string;
    set numberGroupSeparator(value: string);
    get numberDecimalSeparator(): string;
    set numberDecimalSeparator(value: string);
    clone(): FormattingSettingsStorageObject;
}
export declare class FormattingSettingsFactory implements IStorageValueFactory<FormattingSettingsStorageObject> {
    getValue(storage: IStorage): FormattingSettingsStorageObject;
    getValueAndStorage(): {
        value: FormattingSettingsStorageObject;
        storage: IStorage;
    };
}
export declare class FormattingSettings implements IFormattingSettings {
    private _numberGroupSeparatorInput?;
    private _datePattern?;
    private _timeWithoutSecondsPattern?;
    private _timePattern?;
    private _time24Pattern?;
    private _dateTimePattern?;
    private _dateTimeWithoutSecondsPattern?;
    private _dateInputPattern?;
    private _timeInputPattern?;
    private _dateTimeInputPattern?;
    private static readonly NotInitializedError;
    constructor(other?: IFormattingSettings);
    protected validateSettings(): void;
    init(): void;
    get numberGroupSeparatorInput(): string;
    get datePattern(): string;
    get timeWithoutSecondsPattern(): string;
    get timePattern(): string;
    get time24Pattern(): string;
    get dateTimePattern(): string;
    get dateTimeWithoutSecondsPattern(): string;
    get dateInputPattern(): string;
    get timeInputPattern(): string;
    get dateTimeInputPattern(): string;
    name: string;
    caption: string;
    dateFormat: DateFormat;
    dateSeparator: string;
    daysWithLeadingZero: boolean;
    monthsWithLeadingZero: boolean;
    hoursWithLeadingZero: boolean;
    time24Hour: boolean;
    timeSeparator: string;
    timeAmDesignator: string | null;
    timePmDesignator: string | null;
    numberGroupSeparator: string;
    numberDecimalSeparator: string;
}
export declare function createEnglish(init?: boolean): FormattingSettings;