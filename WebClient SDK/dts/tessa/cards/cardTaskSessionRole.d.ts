import { CardStorageObject } from '.';
import { IStorage, IStorageValueFactory } from 'tessa/platform/storage';
/**
 * Запись с ID роли задания из CardTask.TaskAssignedRoles, к которой относится текущая сессия.
 * Также вместе с идентификатором записи из CardTask.TaskAssignedRoles возвращается ID функциональной роли, признак того,
 * что пользователь в текущей сессии является заместителем для данной записи и признак необходимости взятия задания в работу.
 * Объекты данного типа возвращаются в коллекции, чтобы понять, какими ФР обладает сотрудник из текущей сессии, и в каких из них он заместитель,
 * а так же для формирования списка ролей, которыми обладает текущая сессия, что необходимо для отображения информации в задании и при записи в историю заданий.
 */
export declare class CardTaskSessionRole extends CardStorageObject {
    constructor(storage?: IStorage);
    static readonly taskRoleRowIDKey: string;
    static readonly functionRoleIDKey: string;
    static readonly isDeputyKey: string;
    get taskRoleRowID(): guid | null;
    set taskRoleRowID(value: guid | null);
    get functionRoleID(): guid | null;
    set functionRoleID(value: guid | null);
    get isDeputy(): boolean;
    set isDeputy(value: boolean);
    isEmpty(): boolean;
}
export declare class CardTaskSessionFunctionRoleFactory implements IStorageValueFactory<CardTaskSessionRole> {
    getValue(storage: IStorage): CardTaskSessionRole;
    getValueAndStorage(): {
        value: CardTaskSessionRole;
        storage: IStorage;
    };
}
