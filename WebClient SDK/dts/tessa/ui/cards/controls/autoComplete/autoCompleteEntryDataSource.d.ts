import { IAutoCompletePopupItem } from './autoCompletePopupItem';
import { IAutoCompleteItem } from './autoCompleteItem';
import { AutoCompleteDataSource } from './autoCompleteDataSource';
import { AutoCompleteEntryDataSourceContext } from './autoCompleteEntryDataSourceContext';
import { SelectedValue } from 'tessa/views';
export declare class AutoCompleteEntryDataSource extends AutoCompleteDataSource {
    constructor(dataSourceContext: AutoCompleteEntryDataSourceContext);
    private _item;
    private readonly _viewComboBox;
    private readonly _viewComboBoxAlias;
    private _popupComplexColumnsDisplayIndexesComboBox;
    private _displayIndexes;
    readonly dataSourceContext: AutoCompleteEntryDataSourceContext;
    get popupComplexColumnsDisplayIndexesComboBox(): ReadonlyArray<number> | null;
    get displayIndexes(): ReadonlyArray<number> | null;
    isItemExists(): boolean;
    getItem(): IAutoCompleteItem | null;
    setItem(item: IAutoCompletePopupItem | null): void;
    setItemFromViews(value: SelectedValue): void;
    deleteItem(): void;
    findItems(filter: string | null, comboBox?: boolean): Promise<IAutoCompletePopupItem[]>;
    private createItem;
    private addNewItem;
    private getFieldValue;
    private isAutoCompleteItemChanged;
    private generatePopupItemsComboBox;
}
