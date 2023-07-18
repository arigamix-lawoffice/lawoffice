import { ITableRowViewModel, TableTagViewModel } from 'tessa/ui/views/content';
export declare class SelectorTagViewModel extends TableTagViewModel {
    constructor(selected: boolean, isReadOnly: boolean, row: ITableRowViewModel, selectionChanged: (row: ITableRowViewModel, selected: boolean) => void);
    private _isSelected;
    private _isReadOnly;
    private _row;
    private _selectionChanged;
    get isSelected(): boolean;
    set isSelected(value: boolean);
    get isReadOnly(): boolean;
    set isReadOnly(value: boolean);
    private updateIcon;
    private clickCommand;
}
