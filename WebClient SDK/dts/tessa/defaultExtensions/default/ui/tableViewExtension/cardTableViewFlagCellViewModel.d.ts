import { Card, CardRow } from 'tessa/cards';
import { GridColumnInfo } from 'tessa/ui/cards/controls/grid/gridColumnInfo';
import { ITableCellViewModelCreateOptions, TableCellViewModel } from 'tessa/ui/views/content';
export declare class CardTableViewFlagCellViewModel extends TableCellViewModel {
    constructor(args: ITableCellViewModelCreateOptions);
    private _rowData;
    private _disposes;
    readonly card: Card;
    readonly cardRow: CardRow;
    readonly columnInfo: GridColumnInfo;
    get value(): any;
    get disabled(): boolean;
    dispose(): void;
    private handleContainerClick;
    private handleCheckboxClick;
    private getCheckBoxContent;
}
