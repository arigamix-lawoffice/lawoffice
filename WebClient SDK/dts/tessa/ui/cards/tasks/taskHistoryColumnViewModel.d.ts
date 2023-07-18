import { GridColumnViewModel } from 'tessa/ui/cards/controls/grid/gridColumnViewModel';
import { TaskHistoryItemViewModel } from 'tessa/ui/cards/tasks/taskHistoryItemViewModel';
import { DotNetType } from 'tessa/platform';
export declare class TaskHistoryColumnViewModel extends GridColumnViewModel {
    constructor(header: string, propName: string, netType: DotNetType, getRawValue?: (row: TaskHistoryItemViewModel) => any);
    readonly propName: any;
    private _getRawValue;
    _netType: DotNetType;
    get netType(): DotNetType;
    getValue(row: TaskHistoryItemViewModel): any;
    getRawValue(row: TaskHistoryItemViewModel): any;
}
