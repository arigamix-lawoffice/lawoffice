import { BlockViewModelBase } from './blockViewModelBase';
import { ICardModel, IControlViewModel } from '../interfaces';
import { CardTypeBlock } from 'tessa/cards/types';
import { Visibility } from 'tessa/platform/visibility';
/**
 * Модель представления для блока, выстраивающего элементы управления по горизонтали (слева направо или справа налево),
 * причём элементы управления, не уместившиеся на строке, переносятся на следующую строку.
 */
export declare class HorizontalBlockViewModel extends BlockViewModelBase {
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
     * @param rightToLeftAlignment Признак того, что вывод элементов управления выполняется справа налево, а не слева направо.
     * @param leftCaptions Признак того, что заголовки элементов управления выводятся слева, а не сверху.
     */
    constructor(block: CardTypeBlock, caption: string, captionVisibility: Visibility, model: ICardModel, controls: IControlViewModel[], verticalInterval: number, horizontalInterval: number, rightToLeftAlignment: boolean, leftCaptions: boolean, collapsed: boolean, doNotCollapseWithTopBlock: boolean, columnIndex?: number, rowIndex?: number, columnSpan?: number, rowSpan?: number, stretchVertically?: boolean);
    protected _rightToLeftAlignment: boolean;
    /**
     * Признак того, что вывод элементов управления выполняется справа налево, а не слева направо.
     */
    get rightToLeftAlignment(): boolean;
    set rightToLeftAlignment(value: boolean);
}
