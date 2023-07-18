import { Card, CardRow } from 'tessa/cards';
import { GridColumnInfo } from 'tessa/ui/cards/controls/grid/gridColumnInfo';
export declare class GridCellFormatContext {
    readonly card: Card;
    readonly row: CardRow;
    readonly column: GridColumnInfo;
    readonly rawValue: any;
    readonly formattedValue: any;
    /**
     * @param {Card} card Карточка содержащаю строку name="row"
     * @param {CardRow} row Строка карточки, данные в которой используются для форматирования ячейки таблицы
     * @param {GridColumnInfo} column Колонка, которой принадлежит форматируемая ячейка таблицы
     * @param {object} rawValue Исходное значение до стандартного форматирования или null, если такое значение нельзя определить (например, при делегировании).
     * @param {object} formattedValue Отформатированное стандартными средствами значение, которое может использоваться для вывода в ячейку. При использовании сложного форматирования это всегда строка, но по умолчанию это могут быть данные того же типа, что были в строке.
     */
    constructor(card: Card, row: CardRow, column: GridColumnInfo, rawValue: any | null, formattedValue: any | null);
}
