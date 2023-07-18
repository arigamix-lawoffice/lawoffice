import { ShowCardArg } from './common';
import { ICardEditorModel } from 'tessa/ui/cards';
/**
 * Отображает карточку в новой вкладке.
 * @param args.editor Редактор карточки. Предполагается, что модель представления карточки уже задана и инициализирована.
 * @param args.displayValue Отображаемое имя карточки, используемое при отсутствии Digest, или null,
 * если отображаемое имя вычисляется автоматически.
 * @param args.alwaysNewTab Признак того, что карточка всегда открывается в новой вкладке. Если значение равно false,
 * то вместо открытия карточки может быть выбрана вкладка, в которой уже открыта карточка с этим идентификатором.
 * Если значение равно true и карточка уже существует, то id вкладки будет заменен на новый. Из-за этого при обновлении страницы
 * с выбранной вкладкой будет ошибка, что карточка не найдена.
 * @param args.openToTheRightOfSelectedTab Если будет открыта новая вкладка (а не выбрана уже существующая),
 * то она будет открыта справа от текущей выбранной вкладки. В противном случае вкладка добавляется в конец.
 * @param args.needDispatch Если значение равно true, то будет изменен url путь.
 * @param args.prepareEditorAction Метод, который подготавливает редактор карточки непосредственно перед отображением,
 * или null, если подготовительные действия не требуется.
 * @param args.prepareEditorAction Метод, который вызывается сразу после prepareEditorAction и перед отображением,
 * или null, если подготовительные действия не требуется. Выполняется асинхронно.
 */
export declare function showCard(args: ShowCardArg): Promise<ICardEditorModel | null>;
export declare function showCardCore(args: {
    editor: ICardEditorModel;
    alwaysNewTab?: boolean;
    openToTheRightOfSelectedTab?: boolean;
    needDispatch?: boolean;
    closeIfExists?: boolean;
}): Promise<ICardEditorModel>;
