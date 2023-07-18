import { Duration } from 'moment';
/**
 * Проверка, клик/тап был мимо набора элементов
 * @param {Event} event       событие клика/тапа
 * @param {Node[]} ...elements элементы для проверки
 */
export declare function checkIfClickedOutside(event: Event, ...elements: (Node | null)[]): boolean;
/**
 * Капитализирует строку
 * @param  {string} str
 * @return {string}
 */
export declare function capitalizeFirstLetter(str: string): string;
/**
 * Получить FormData из объекта, где все вложенные проперти будут частью FormData.
 *
 * @export
 * @param {Object} object Объект.
 * @param {FormData} [form] Форма.
 * @param {string} [namespace] Неймспейс.
 * @returns {FormData} Форма.
 */
export declare function getFormDataRecursive(object: Object, form?: FormData, namespace?: string): FormData;
export declare const ScriptRegex: RegExp;
export declare const ALinkRegex: RegExp;
export declare function errorHandler(error: any): any;
export declare function downloadFileThroughATagWithBlob(blob: Blob, fileName: string | null): void;
/**
 *
 * @param objectUrl  like 'data:application/octet-stream;base64,...' where ... - file base64 string
 * @param fileName
 */
export declare function downloadFileThroughATagWithUrl(objectUrl: string, fileName: string | null): void;
/**
 * Конвертирует указанный интервал времени Duration в такты, представляющие значение структуры TimeSpan в .NET.
 */
export declare function durationToDotNetTimeSpanTicks(d: Duration): number;
