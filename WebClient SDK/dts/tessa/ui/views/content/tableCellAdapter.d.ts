import { GridCellContext, IGridCellViewModel, IGridRowViewModel } from 'components/cardElements/grid';
import { CSSProperties } from 'react';
import { ITableCellViewModel, TableTagViewModel } from 'tessa/ui/views/content';
import { ClassNameList } from 'tessa/ui/classNameList';
export declare class TableCellAdapter implements IGridCellViewModel {
    constructor(viewModel: ITableCellViewModel, parent: IGridRowViewModel, columnId: string);
    private _viewModel;
    private _parent;
    private _columnId;
    get columnId(): string;
    get parent(): IGridRowViewModel;
    get content(): (ctx: GridCellContext) => unknown;
    get style(): CSSProperties;
    get tooltip(): string;
    get leftTags(): TableTagViewModel[];
    get rightTags(): TableTagViewModel[];
    get isSelected(): boolean;
    set isSelected(value: boolean);
    get className(): ClassNameList;
    onClick: (e: React.MouseEvent) => void;
    onDoubleClick: (e: React.MouseEvent) => void;
    onMouseDown: (e: React.MouseEvent) => void;
}
