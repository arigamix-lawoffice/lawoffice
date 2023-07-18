import { IGridCellViewModel, IGridRowViewModel } from 'components/cardElements/grid';
import React from 'react';
import { ClassNameList, MenuAction } from 'tessa/ui';
import { DefaultFormTabWithTaskHistoryViewModel } from '.';
import { TaskHistoryItemViewModel } from '../tasks';
export declare class TaskHistoryRowAdapter implements IGridRowViewModel {
    constructor(viewModel: TaskHistoryItemViewModel, form: DefaultFormTabWithTaskHistoryViewModel, index: number);
    private _viewModel;
    private _form;
    private _cells;
    private _index;
    private _isToggled;
    get id(): string;
    get index(): number;
    get parentId(): string | null;
    get showChildren(): boolean;
    set showChildren(value: boolean);
    get showOverflow(): boolean;
    set showOverflow(value: boolean);
    get isSelected(): boolean;
    set isSelected(value: boolean);
    get isLastSelected(): boolean;
    get cells(): IGridCellViewModel[];
    get toolTip(): string | undefined;
    get style(): React.CSSProperties | undefined;
    get className(): ClassNameList;
    getContextMenu: (_e: React.MouseEvent, columnId?: string | undefined) => readonly MenuAction[];
}
