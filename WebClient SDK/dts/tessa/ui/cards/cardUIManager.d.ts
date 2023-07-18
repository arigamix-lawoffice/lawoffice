import { ICardModel } from './interfaces';
import { IStorage, MapStorage } from 'tessa/platform/storage';
import { ICardEditorModel, CardEditorCreationContext } from 'tessa/ui/cards';
import { Card, CardRow } from 'tessa/cards';
import { CardNewResponse, CardNewRequest } from 'tessa/cards/service';
import { CreateCardArg } from 'tessa/ui/uiHost/common';
/**
 * Создаёт карточку по шаблону и открывает её.
 *
 * @param {Card} templateCard Карточка шаблона, по которой требуется создать карточку.
 * @param {(IStorage | null)} templateInfo Дополнительная информация, помещаемая в запрос на создание карточки по шаблону, или null, если дополнительная информация отсутствует.
 * @param {(CreateCardArg | null)} args Настройки создания карточки или значение null, если используются настройки по умолчанию.
 *
 * Игнорируется значение свойства ShowCardArg.alwaysNewTab.
 * @returns {(Promise<ICardEditorModel | null>)} {@link ICardEditorModel} созданной карточки по шаблону или значение null, если произошла ошибка.
 */
export declare function tryCreateFromTemplate(templateCard: Card, templateInfo?: IStorage | null, args?: CreateCardArg | null): Promise<ICardEditorModel | null>;
/**
 * Обрабатывает запрос с созданной карточкой, полученный в результате создания карточки по шаблону, и открывает редактор такой карточки в отдельной вкладке.
 *
 * @param {CardNewRequest} newRequest Запрос на создание карточки по шаблону.
 * @param {CardNewResponse} newResponse Результат создания карточки по шаблону.
 * @param {(CreateCardArg | null)} args Настройки создания карточки или значение null, если используются настройки по умолчанию.
 * Игнорируется значение свойства ShowCardArg.alwaysNewTab.
 * @returns {(Promise<ICardEditorModel | null>)} {@link ICardEditorModel} созданной карточки по шаблону или значение null, если произошла ошибка.
 */
export declare function tryCreateFromTemplateResponse(newRequest: CardNewRequest, newResponse: CardNewResponse, args?: CreateCardArg | null): Promise<ICardEditorModel | null>;
/**
 * Обрабатывает результат создания карточки по шаблону, отображая результат валидации и возвращая редактор карточки, который можно отобразить в новой вкладке.
 *
 * @param {CardNewRequest} request Запрос на создание карточки по шаблону.
 * @param {CardNewResponse} response Результат запроса на создание карточки по шаблону.
 * @param {(context: CardEditorCreationContext) => void} modifyCardAction Метод, который может изменить модель карточки перед созданием модели представления (т.е. перед инициализацией UI).
 * Выполняется только в случае успешного создания карточки.
 * Метод может создать собственную модель представления, которая заменит стандартную.
 * Также метод может отменить создание, при этом UI инициализирован не будет и создание считается неудачным.
 * @param {(context: CardEditorCreationContext) => void} cardModelModifierAction Метод, который может изменить модель представления карточки (например, настроить элементы управления), когда карточка была успешно создана, и UI был инициализирован.
 * Метод может заменить созданную модель представления на модель, созданную другими средствами.
 * Также метод может отменить создание, при этом UI использован не будет и создание считается неудачным.
 * @returns {(Promise<ICardEditorModel | null>)} Редактор карточки, в котором открыта карточка, созданная по шаблону, или null, если при открытии карточки возникли ошибки или открытие карточки было отменено.
 */
export declare function processTemplateResponse(request: CardNewRequest, response: CardNewResponse, modifyCardAction?: (context: CardEditorCreationContext) => void, cardModelModifierAction?: (context: CardEditorCreationContext) => void): Promise<ICardEditorModel | null>;
export declare function tryEditCardInTemplate(templateCard: Card, templateSectionRows: MapStorage<CardRow>): Promise<ICardEditorModel | null>;
export declare function tryOpenTemplateFromCard(cardInTemplateModel: ICardModel): Promise<ICardEditorModel | null>;
