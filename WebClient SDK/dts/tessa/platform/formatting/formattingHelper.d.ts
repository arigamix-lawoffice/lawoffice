import { MomentBuiltinFormat } from 'moment';
import { DotNetType } from 'tessa/platform/dotNetType';
import { SchemeDbType } from 'tessa/platform/schemeDbType';
import { TypedField } from 'tessa/platform/typedField';
import { DateTimeTypeFormat } from 'ui/datePicker/dateTimeTypeFormat';
export declare function formatToString(displayFormat: string, ...values: any[]): string;
export declare function formatToString(displayFormat: string, ...values: TypedField[]): string;
export declare function getDisplayText(text: string): string;
export declare function formatValue(value: any): any;
export declare function formatTypedValue(value: any, valueType: DotNetType): any;
export declare function formatDate(value: string | null | undefined, treatValueAsUtc?: boolean): string;
export declare function formatDateTime(value: string | null | undefined, treatValueAsUtc?: boolean): string;
export declare function formatDateTimeWithoutSeconds(value: string | null | undefined, treatValueAsUtc?: boolean): string;
export declare const isTimeSpanRegExp: RegExp;
export declare function formatTime(value: string | null | undefined, treatValueAsUtc?: boolean): string;
export declare function format24Time(value: string | null | undefined, treatValueAsUtc?: boolean): string;
export declare function formatDateTimeBySchemeDbType(dbType: SchemeDbType, value: string, treatValueAsUtc?: boolean): string;
export declare function formatDateTimeByTypeFormat(dateTimeTypeFormat: DateTimeTypeFormat, value: string, treatValueAsUtc?: boolean): string;
export declare function formatDateAsIs(value: string, opt?: {
    inFormat?: string | MomentBuiltinFormat | (string | MomentBuiltinFormat)[];
    outFormat?: string;
}): string;
export declare function formatBoolean(value: boolean): string;
export declare function formatDecimal(value: number | string, separateGroups?: boolean): string;
export declare function formatSize(size: number, unitSize: number): string;
/**
 * Вернуть строку локализации разницы дней, c учетом отрицательных значений
 * @param {number} quants количество квантов разницы
 * @param {string}
 */
export declare function formatDateDiff(quants: number, localize: (s: string) => string): string;
export declare function formatDateDiffCore(quants: number, negativeQuants: boolean, localize: (s: string) => string): string;
export declare function formatDecimalFixed(value: string | number | undefined, digitsAfterSeparator?: number, separateGroups?: boolean): string;
export declare function fixDecimalInput(value: string | undefined): string;
export declare function appendHalfUnitValue(result: string, halfUnitValue: number, singleUnity: string, sevaralUnits: string, manyUnits: string): string;
/**
 * Возвращает словоформу, подходящую к количеству некоторых единиц.
 * @param units Количество единиц, к которым необходимо вернуть словоформу.
 * @param singleUnit Словоформа, подходящая для значения, равного 1.
 * @param severalUnits Словоформа, подходящая для значения равного 2.
 * @param manyUnits Словоформа, подходящая для значения, равного 10.
 * @param plusHalfUnit Признак того, что словоформу следует подбирать для числа, к которому добавили 0.5.
 */
export declare function getUnitString(units: number, singleUnit: string, severalUnits: string, manyUnits: string, plusHalfUnit?: boolean): string;
export declare function compareDates(a: string | Date, b: string | Date, comparer: (a: number, b: number) => number): number;
