import { CardSerializableObject } from 'tessa/cards/cardSerializableObject';
export interface CardMetadataFunctionRoleSealed {
    readonly id: guid | null;
    readonly name: string | null;
    readonly caption: string | null;
    /**
     * Признак того, что в функциональную роль включаются заместители.
     * */
    readonly canBeDeputy: boolean;
    /**
     * Признак того, что в функциональную роль включаются заместители.
     */
    readonly canTakeInProgress: boolean;
    /**
     * Признак того, что для функциональной роли по умолчанию скрываются задания
     */
    readonly hideTaskByDefault: boolean;
    /**
     * Признак того, что функциональная роль разрешает менять дайджест и плановую дату.
     */
    readonly canChangeTaskInfo: boolean;
    /**
     * Признак того, что функциональная роль разрешает менять список ролей задания.
     */
    readonly canChangeTaskRoles: boolean;
    seal<T = CardMetadataFunctionRoleSealed>(): T;
}
/**
 * Содержит информацию о функциональной роли задания.
 */
export declare class CardMetadataFunctionRole extends CardSerializableObject {
    constructor();
    id: guid | null;
    name: string | null;
    caption: string | null;
    /**
     * Признак того, что в функциональную роль включаются заместители.
     */
    canBeDeputy: boolean;
    /**
     * Признак того, что функциональная роль разрешает брать задание в работу.
     */
    canTakeInProgress: boolean;
    /**
     * Признак того, что для функциональной роли по умолчанию скрываются задания.
     */
    hideTaskByDefault: boolean;
    /**
     * Признак того, что функциональная роль разрешает изменять дайджест и плановую дату.
     */
    canChangeTaskInfo: boolean;
    /**
     * Признак того, что функциональная роль разрешает изменять список ролей задания.
     */
    canChangeTaskRoles: boolean;
    seal<T = CardMetadataFunctionRoleSealed>(): T;
}
