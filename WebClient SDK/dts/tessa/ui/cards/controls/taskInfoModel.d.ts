import { CardTask } from 'tessa/cards';
import { CardMetadataFunctionRoleSealed } from 'tessa/cards/metadata';
export declare class TaskInfoModel {
    constructor(cardTask: CardTask, metadataFunctionRoles: CardMetadataFunctionRoleSealed[]);
    readonly cardTask: CardTask;
    readonly performerRolesCount: number;
    private readonly metadataFunctionRoles;
    private readonly performerRole;
    get roleName(): string | null;
    get roleTypeId(): guid | undefined;
    get roleId(): guid | undefined;
    private getPerformerRoleAndCount;
}
