/// <reference types="react" />
import { IGridBlockViewModel } from 'components/cardElements/grid';
import { ClassNameList, MenuAction } from 'tessa/ui';
import { ITableBlockViewModel } from 'tessa/ui/views/content';
export declare class TableBlockAdapter implements IGridBlockViewModel {
    constructor(viewModel: ITableBlockViewModel);
    private _viewModel;
    get id(): string;
    get parentId(): string | null;
    get caption(): string;
    get count(): number;
    get showChildren(): boolean;
    set showChildren(value: boolean);
    get style(): React.CSSProperties | undefined;
    get className(): ClassNameList;
    onClick: (e: React.MouseEvent) => void;
    onDoubleClick: (e: React.MouseEvent) => void;
    onMouseDown: (e: React.MouseEvent) => void;
    getContextMenu: () => ReadonlyArray<MenuAction>;
}
