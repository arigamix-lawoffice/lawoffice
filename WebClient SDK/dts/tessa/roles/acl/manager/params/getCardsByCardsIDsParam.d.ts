import { AclManagerGetCardsStrategyParamBase } from './aclManagerGetCardsStrategyParamBase';
import { IStorage, ArrayStorage } from 'tessa/platform/storage';
import { DotNetType, TypedField } from 'tessa/platform';
export declare class GetCardsByCardsIDsParam extends AclManagerGetCardsStrategyParamBase {
    constructor(storage: IStorage);
    static readonly cardIDsKey: string;
    static readonly cardTypeIDsKey: string;
    static readonly ignoreCardTypeCheckKey: string;
    /**  @inheritdoc */
    protected get strategyNameCore(): string;
    /**
     * Список идентификаторов карточек, для которых нужно пересчитать ACL.
     */
    get cardIDs(): ArrayStorage<TypedField<DotNetType.Guid, guid>>;
    /**
     * Список идентификаторов карточек, для которых нужно пересчитать ACL.
     */
    set cardIDs(value: ArrayStorage<TypedField<DotNetType.Guid, guid>>);
    /**
     * Списки типов карточек, которые соответствуют идентификаторам карточек из списка <see cref="CardIDs"/>.
     * Если не задан или не заполнен, типы карточек определяются автоматически.
     */
    get cardTypeIDs(): ArrayStorage<TypedField<DotNetType.Guid, guid>>;
    /**
     * Списки типов карточек, которые соответствуют идентификаторам карточек из списка <see cref="CardIDs"/>.
     * Если не задан или не заполнен, типы карточек определяются автоматически.
     */
    set cardTypeIDs(value: ArrayStorage<TypedField<DotNetType.Guid, guid>>);
    /**
     * Определяет, нужно ли проверять тип карточки при обработке.
     */
    get ignoreCardTypeCheck(): boolean;
    /**
     * Определяет, нужно ли проверять тип карточки при обработке.
     */
    set ignoreCardTypeCheck(value: boolean);
}
