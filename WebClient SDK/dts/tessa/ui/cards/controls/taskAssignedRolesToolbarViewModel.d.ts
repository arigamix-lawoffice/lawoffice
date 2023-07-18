import { CommandFunc, Command } from 'tessa/platform';
import { BaseViewControlItem, ViewControlViewModel } from './viewControl';
import { UIButton } from 'tessa/ui';
export declare class TaskAssignedRolesToolbarViewModel extends BaseViewControlItem {
    constructor(viewComponent: ViewControlViewModel, addTaskAssignedRoleActionAsync: CommandFunc, deleteTaskAssignedRoleActionAsync: CommandFunc, setMasterTaskAssignedRoleActionAsync: CommandFunc, setShowInTaskDetailsActionAsync: CommandFunc, canExecuteSetMaster: (selectedRow?: ReadonlyMap<string, unknown> | null) => boolean);
    static readonly AddTaskAssignedRoleToolTip = "$UI_Controls_TaskAssignedRolesToolbar_Add_ToolTip";
    static readonly DeleteTaskAssignedRoleToolTip = "$UI_Controls_TaskAssignedRolesToolbar_Delete_ToolTip";
    static readonly SetMasterTaskAssignedRoleToolTip = "$UI_Controls_TaskAssignedRolesToolbar_SetMaster_ToolTip";
    static readonly SetShowInTaskDetailsToolTip = "$UI_Controls_TaskAssignedRolesToolbar_SetShowInTaskDetails_ToolTip";
    private readonly _addTaskAssignedRoleButtonCommand;
    get addTaskAssignedRoleButtonCommand(): Command;
    private readonly _deleteTaskAssignedRoleButtonCommand;
    get deleteTaskAssignedRoleButtonCommand(): Command;
    private readonly _setMasterTaskAssignedRoleButtonCommand;
    get setMasterTaskAssignedRoleButtonCommand(): Command;
    private readonly _setShowInTaskDetailsButtonCommand;
    get setShowInTaskDetailsButtonCommand(): Command;
    buttons: UIButton[];
}
