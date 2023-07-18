import { IAutoCompletePopupItem } from './autoCompletePopupItem';
import { RowAutoCompleteItem } from './autoCompleteItem';
import { AutoCompleteDataSource } from './autoCompleteDataSource';
import { AutoCompleteTableDataSourceContext } from './autoCompleteTableDataSourceContext';
import { SelectedValue } from 'tessa/views';
import { CardRow } from 'tessa/cards';
export declare class AutoCompleteTableDataSource extends AutoCompleteDataSource {
    constructor(dataSourceContext: AutoCompleteTableDataSourceContext);
    private _cardModel;
    private _rows;
    private _orderColumnName;
    private _referenceColumnNames;
    private _rowsTransformer;
    private _rowTransformer;
    readonly dataSourceContext: AutoCompleteTableDataSourceContext;
    getItems(): ReadonlyArray<RowAutoCompleteItem>;
    setItem(item: IAutoCompletePopupItem | null): void;
    setItemFromViews(value: SelectedValue): void;
    deleteItem(row: CardRow): void;
    findItems(filter: string | null): Promise<IAutoCompletePopupItem[]>;
    private getRowsTransformer;
    private getRowTransformer;
    private rowVisibilityFilter;
    private createItem;
    private addNewItem;
    private getAutocCompleteItemByRow;
}
