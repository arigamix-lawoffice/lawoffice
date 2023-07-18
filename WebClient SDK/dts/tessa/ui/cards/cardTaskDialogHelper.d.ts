import { IStorage } from 'tessa/platform/storage';
import { CardTask, CardTaskCompletionOptionSettings } from 'tessa/cards';
/**
 * Возвращает параметры диалога для указанного варианта завершения.
 *
 * @param {(IStorage | null)} settings Задание, содержащее параметры.
 * @param {guid} completionOptionId Идентификатор варианта завершения.
 * @returns {(CardTaskCompletionOptionSettings | null)} Параметры варианта завершения диалога или значение null, если параметры не удалось получить.
 */
export declare const getCompletionOptionSettings: (settings: IStorage | null, completionOptionId: guid) => CardTaskCompletionOptionSettings | null;
/**
 * Возвращает коллекцию, содержащую информацию по всем параметрам диалога, содержащимся в заданном задании.
 *
 * @param task Задание.
 * @returns Коллекция, содержащая информацию по всем параметрам диалога.
 */
export declare const getAllCompletionOptionSettings: (task: CardTask) => CardTaskCompletionOptionSettings[];
