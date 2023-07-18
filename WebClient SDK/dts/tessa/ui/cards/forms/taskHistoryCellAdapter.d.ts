import { IGridCellViewModel, IGridRowViewModel } from 'components/cardElements/grid';
import React from 'react';
import { TaskHistoryTagViewModel } from '../tasks';
import { ClassNameList } from 'tessa/ui/classNameList';
export declare class TaskHistoryCellAdapter implements IGridCellViewModel {
    constructor(viewModel: TaskHistoryTagViewModel | string, parent: IGridRowViewModel, columnId: string, columnPropName: string);
    private _viewModel;
    private _parent;
    private _columnId;
    private _columnPropName;
    private _className;
    get columnId(): string;
    get columnPropName(): string;
    get parent(): IGridRowViewModel;
    get content(): string;
    get icon(): string | null;
    get tooltip(): string;
    get style(): React.CSSProperties | undefined;
    get className(): ClassNameList;
    onTagClick: (_e: React.SyntheticEvent) => void;
}
