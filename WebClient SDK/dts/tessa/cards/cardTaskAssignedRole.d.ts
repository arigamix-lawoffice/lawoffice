import { CardStorageObject } from './cardStorageObject';
import { IStorage, IStorageValueFactory } from 'tessa/platform/storage';
import { EventHandler } from 'tessa/platform';
import { CardTaskAssignedRoleState } from './cardTaskAssignedRoleState';
/**
 * Запись функциональной роли, которая связаны с <see cref="CardTask"/>.
 */
export declare class CardTaskAssignedRole extends CardStorageObject {
    constructor(storage?: IStorage);
    static readonly rowIDKey: string;
    static readonly taskRoleIDKey: string;
    static readonly taskRoleCaptionKey: string;
    static readonly roleIDKey: string;
    static readonly roleNameKey: string;
    static readonly roleTypeIDKey: string;
    static readonly positionKey: string;
    static readonly showInTaskDetailsKey: string;
    static readonly parentRowIDKey: string;
    static readonly masterKey: string;
    static readonly systemStateKey: string;
    get rowId(): guid;
    set rowId(value: guid);
    get taskRoleId(): guid;
    set taskRoleId(value: guid);
    get taskRoleCaption(): string;
    set taskRoleCaption(value: string);
    get roleId(): guid;
    set roleId(value: guid);
    get roleName(): string;
    set roleName(value: string);
    get roleTypeId(): guid;
    set roleTypeId(value: guid);
    get position(): string;
    set position(value: string);
    get showInTaskDetails(): boolean;
    set showInTaskDetails(value: boolean);
    get parentRowId(): guid | null;
    set parentRowId(value: guid | null);
    get master(): boolean;
    set master(value: boolean);
    get state(): CardTaskAssignedRoleState;
    set state(value: CardTaskAssignedRoleState);
    readonly stateChanged: EventHandler<(args: CardTaskAssignedRoleStateEventArgs) => void>;
    isEmpty(): boolean;
}
export interface CardTaskAssignedRoleStateEventArgs {
    readonly assignedRole: CardTaskAssignedRole;
    readonly oldState: CardTaskAssignedRoleState;
    readonly newState: CardTaskAssignedRoleState;
}
export declare class CardTaskAssignedRolesFactory implements IStorageValueFactory<CardTaskAssignedRole> {
    getValue(storage: IStorage): CardTaskAssignedRole;
    getValueAndStorage(): {
        value: CardTaskAssignedRole;
        storage: IStorage;
    };
}
