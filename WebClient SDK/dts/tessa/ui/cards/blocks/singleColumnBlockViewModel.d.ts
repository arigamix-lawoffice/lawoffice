import { BlockViewModelBase } from './blockViewModelBase';
import { ICardModel, IControlViewModel } from '../interfaces';
import { CardTypeBlock } from 'tessa/cards/types';
import { Visibility } from 'tessa/platform/visibility';
/**
 * Модель представления для блока, выстраивающего элементы управления в одну колонку.
 */
export declare class SingleColumnBlockViewModel extends BlockViewModelBase {
    /**
     * Создаёт экземпляр класса с указанием метаинформации по блоку
     * и списка моделей представления элемента управления внутри блока.
     * @param block Метаинформация по блоку.
     * @param caption Заголовок блока.
     * @param captionVisibility Видимость заголовка блока.
     * @param model Модель карточки, доступная в UI.
     * @param controls Коллекция моделей представления элемента управления внутри блока.
     * @param controlInterval Вертикальный интервал между элементами управления в блоке.
     */
    constructor(block: CardTypeBlock, caption: string, captionVisibility: Visibility, model: ICardModel, controls: IControlViewModel[], controlInterval: number, leftCaptions: boolean, collapsed: boolean, doNotCollapseWithTopBlock: boolean, columnIndex?: number, rowIndex?: number, columnSpan?: number, rowSpan?: number, stretchVertically?: boolean);
    /**
     * Количество колонок.
     */
    get columnsCount(): number;
}
