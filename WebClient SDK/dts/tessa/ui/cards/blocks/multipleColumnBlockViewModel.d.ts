import { BlockViewModelBase } from './blockViewModelBase';
import { ICardModel, IControlViewModel } from '../interfaces';
import { CardTypeBlock } from 'tessa/cards/types';
import { Visibility } from 'tessa/platform/visibility';
/**
 * Модель представления для блока, выстраивающего элементы управления в несколько колонок.
 */
export declare class MultipleColumnBlockViewModel extends BlockViewModelBase {
    /**
     * Создаёт экземпляр класса с указанием метаинформации по блоку
     * и списка моделей представления элемента управления внутри блока.
     * @param block Метаинформация по блоку.
     * @param caption Заголовок блока.
     * @param captionVisibility Видимость заголовка блока.
     * @param model Модель карточки, доступная в UI.
     * @param controls Коллекция моделей представления элемента управления внутри блока.
     * @param verticalInterval Вертикальный интервал между элементами управления в блоке.
     * @param horizontalInterval Горизонтальный интервал между элементами управления в блоке.
     * @param columnsCount Количество колонок.
     * @param leftCaptions Заголовки слева.
     */
    constructor(block: CardTypeBlock, caption: string, captionVisibility: Visibility, model: ICardModel, controls: IControlViewModel[], verticalInterval: number, horizontalInterval: number, columnsCount: number, leftCaptions: boolean, collapsed: boolean, doNotCollapseWithTopBlock: boolean, columnIndex?: number, rowIndex?: number, columnSpan?: number, rowSpan?: number, stretchVertically?: boolean);
    protected _columnsCount: number;
    /**
     * Количество колонок.
     */
    get columnsCount(): number;
    set columnsCount(value: number);
}
