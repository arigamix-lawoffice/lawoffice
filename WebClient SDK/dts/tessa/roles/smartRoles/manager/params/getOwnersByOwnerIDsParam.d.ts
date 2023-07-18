import { IStorage, ArrayStorage } from 'tessa/platform/storage';
import { DotNetType, TypedField } from 'tessa/platform';
import { SmartRoleGetOwnersStrategyParamBase } from './smartRoleGetOwnersStrategyParamBase';
/**
 * Реализация параметра для получения владельцев умных ролей по их идентификаторам.
 */
export declare class GetOwnersByOwnerIDsParam extends SmartRoleGetOwnersStrategyParamBase {
    constructor(storage: IStorage);
    static readonly ownerIDsKey: string;
    /**  @inheritdoc */
    protected get strategyNameCore(): string;
    /**
     * Список идентификаторов владельцев умных ролей, для которых нужно пересчитать умную роль.
     */
    get ownerIDs(): ArrayStorage<TypedField<DotNetType.Guid, guid>>;
    /**
     * Список идентификаторов владельцев умных ролей, для которых нужно пересчитать умную роль.
     */
    set ownerIDs(value: ArrayStorage<TypedField<DotNetType.Guid, guid>>);
}
