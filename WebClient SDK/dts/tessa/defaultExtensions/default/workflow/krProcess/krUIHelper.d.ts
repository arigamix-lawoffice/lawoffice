import { KrTypesCache, IKrType } from 'tessa/workflow';
import { Card, CardRow } from 'tessa/cards';
import { ValidationResultBuilder } from 'tessa/platform/validation';
export declare const CompiledCardTypes: string[];
export declare function sendCompileRequest(compileFlag: string): Promise<void>;
/**
 * Возвращает эффективные настройки для типа карточки или типа документа {@link IKrType} по карточке card, которая загружена со всеми секциями, или null, если настройки нельзя получить.
 *
 * @param {KrTypesCache} krTypesCache Кэш типов карточек.
 * @param {Card} card Карточка, загруженная со всеми секциями.
 * @param {guid} cardTypeId Идентификатор типа карточки.
 * @param {ValidationResultBuilder | null} validationResult Объект, в который записываются сообщения об ошибках, или null, если сообщения никуда не записываются.
 * @returns {IKrType | null} Эффективные настройки для типа карточки или типа документа или null, если настройки нельзя получить.
 */
export declare function tryGetKrType(krTypesCache: KrTypesCache, card: Card, cardTypeId: guid, validationResult?: ValidationResultBuilder | null): IKrType | null;
/**
 * Возвращает значение, показывающее, может ли указанный тип карточки содержать шаблоны этапов.
 *
 * @param {guid} typeId Идентификатор типа карточки.
 * @returns {boolean} Значение true, если указанный тип карточки может содержать шаблоны этапов, иначе - false.
 */
export declare function designTimeCard(typeId: guid): boolean;
/**
 * Возвращает значение, показывающее, является ли указанный тип карточки типом карточки в котором выполняется маршрут.
 *
 * @param {guid} typeId Идентификатор типа карточки.
 * @returns {boolean} Значение true, если указанный тип карточки может содержать выполняющийся маршрут, иначе - false.
 */
export declare function runtimeCard(typeId: guid): boolean;
/**
 * Возвращает значение, показывающее, возможен ли пропуск указанного этапа.
 *
 * @param {CardRow} row Строка этапа, для которого выполняется проверка.
 * @returns {boolean} Значение, показывающее, возможен ли пропуск указанного этапа.
 */
export declare function canBeSkipped(row: CardRow): boolean;
/**
 * Выполняет пропуск этапа.
 *
 * @param {CardRow} row Строка этапа, пропуск которого выполняется.
 * @returns {boolean} Значение true, если этап был пропущен, иначе - false.
 */
export declare function skipStage(row: CardRow): boolean;
