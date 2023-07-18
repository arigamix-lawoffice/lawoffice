import { BlockViewModelBase } from './blockViewModelBase';
import { ICardModel, IControlViewModel } from '../interfaces';
import { CardTypeBlock } from 'tessa/cards/types';
import { Visibility } from 'tessa/platform/visibility';
/**
 * Модель представления для блока, состоящего из единственного элемента управления.
 * Такой элемент управления может растягиваться по высоте, занимая всё свободное место,
 * если он встраивается в форму в режиме сетки (по кнопке "Расположение блоков").
 */
export declare class SingleControlBlockViewModel extends BlockViewModelBase {
    /**
     * Создаёт экземпляр класса с указанием метаинформации по блоку
     * и списка моделей представления элемента управления внутри блока.
     * @param block Метаинформация по блоку.
     * @param caption Заголовок блока.
     * @param captionVisibility Видимость заголовка блока.
     * @param model Модель карточки, доступная в UI.
     * @param controls Коллекция моделей представления элемента управления внутри блока.
     * @param leftCaptions Признак того, что заголовки элементов управления выводятся слева, а не сверху.
     */
    constructor(block: CardTypeBlock, caption: string, captionVisibility: Visibility, model: ICardModel, controls: IControlViewModel[], leftCaptions: boolean, collapsed: boolean, doNotCollapseWithTopBlock: boolean, columnIndex?: number, rowIndex?: number, columnSpan?: number, rowSpan?: number, stretchVertically?: boolean);
}
