export interface IRegistry<TValue extends IRegistryItem<TKey>, TKey = guid> {
    register(item: TValue): any;
    unregister(id: TKey): any;
    get(id: TKey): TValue | undefined;
    tryGet(id: TKey): TValue | null;
    getAll(): TValue[];
    isDefined(id: TKey): boolean;
    isDefined(item: TValue): boolean;
}
export interface IRegistryItem<TKey = guid> {
    id: TKey;
}
export declare abstract class Registry<TValue extends IRegistryItem<TKey>, TKey = guid> implements IRegistry<TValue, TKey> {
    protected items: Map<TKey, TValue>;
    register(item: TValue): void;
    unregister(id: TKey): void;
    get(id: TKey): TValue | undefined;
    tryGet(id: TKey): TValue | null;
    getAll(): TValue[];
    isDefined(id: TKey): boolean;
    isDefined(item: TValue): boolean;
}
