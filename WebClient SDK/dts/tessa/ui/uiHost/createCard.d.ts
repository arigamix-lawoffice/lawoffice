import { CreateCardArg } from './common';
import { ICardEditorModel } from 'tessa/ui/cards';
/**
 * Создаёт карточку и открывает её в отдельной вкладке.
 * @param args.cardId Идентификатор карточки или null, если открывается виртуальная карточка,
 * для которой идентификатор задаётся другим способом.
 * @param args.cardTypeId Идентификатор типа карточки или null, если идентификатор типа неизвестен.
 * @param args.cardTypeName Имя типа карточки или null, если имя типа неизвестно.
 * @param args.context Контекст, в пределах которого выполняется открытие, или null, если используется текущий контекст.
 * @param args.displayValue Отображаемое имя карточки, используемое при отсутствии Digest, или null,
 * если отображаемое имя вычисляется автоматически.
 * @param args.info Дополнительная информация для расширений или null, если дополнительная информация не требуется.
 * @param args.cardModifierAction Метод, который может изменить модель карточки перед созданием модели представления
 * (т.е. перед инициализацией UI). Выполняется только в случае успешного открытия карточки.
 * Метод может создать собственную модель представления, которая заменит стандартную.
 * Также метод может отменить открытие, при этом UI инициализирован не будет и открытие считается неудачным.
 * @param args.cardModelModifierAction Метод, который может изменить модель представления карточки
 * (например, настроить элементы управления), когда карточка была успешно открыта и UI инициализирован.
 * Метод может заменить созданную модель представления на модель, созданную другими средствами.
 * Также метод может отменить открытие, при этом UI использован не будет и открытие считается неудачным.
 * @param args.cardEditorModifierAction Метод, который может изменить модель представления для редактора карточки
 * (например, изменить заголовок вкладки), когда карточка была успешно открыта, UI инициализирован
 * и редактор подготовлен для отображения. Метод не может заменить созданную модель представления на модель,
 * созданную другими средствами. Метод может отменить открытие, при этом UI использован не будет
 * и открытие считается неудачным.
 * @param args.alwaysNewTab Признак того, что карточка всегда открывается в новой вкладке. Если значение равно false,
 * то вместо открытия карточки может быть выбрана вкладка, в которой уже открыта карточка с этим идентификатором.
 * Если значение равно true и карточка уже существует, то id вкладки будет заменен на новый. Из-за этого при рефреше страницы
 * с выбранной вкладкой будет ошибка, что карточка не найдена.
 * @param args.openToTheRightOfSelectedTab Если будет открыта новая вкладка (а не выбрана уже существующая),
 * то она будет открыта справа от текущей выбранной вкладки. В противном случае вкладка добавляется в конец.
 * @param args.withUIExtensions Признак того, что должны выполняться UI расширения при действиях с карточкой.
 * По умолчанию равно true.
 * @param args.saveCreationRequest Признак того, что запрос на создание карточки должен быть сохранён для последующего использования.
 * Если значение равно true, то после создания карточки её можно будет повторно создать
 * через плитку "Создать карточку" на правой боковой панели, а также через плитку "Сохранить, закрыть и создать"
 * на левой боковой панели. Если указано true, то на левой панели плитки не будет, а "Создать карточку"
 * на правой панели создаст предыдущую сохранённую для создания карточку, если таковая присутствует.
 * @param args.creationModeDisplayText Способ создания карточки, отображаемый в плитке "Создать карточку",
 * или null (или пустая строка), если в плитке не отображается дополнительный текст. Можно указать как строку локализации.
 * @param args.splashResolve Лямбда для закрытия экрана загрузки.
 */
export declare function createCard(args: CreateCardArg): Promise<ICardEditorModel | null>;