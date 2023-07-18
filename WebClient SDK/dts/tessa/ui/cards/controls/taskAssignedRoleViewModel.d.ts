import { SupportUnloadingViewModel } from '../supportUnloadingViewModel';
import { CardTaskAssignedRole } from 'tessa/cards';
export declare class TaskAssignedRoleViewModel extends SupportUnloadingViewModel {
    constructor(model: CardTaskAssignedRole, getTaskRoleName: (id: guid) => string, openTaskRolesDialog: () => Promise<void>);
    model: CardTaskAssignedRole;
    taskRoleName: string;
    roleName: string;
    openTaskRolesDialog: () => Promise<void>;
}
