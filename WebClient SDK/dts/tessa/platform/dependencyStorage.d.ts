import { Registry, IRegistry, IRegistryItem } from './registry';
export declare class DependencyStorageRegistry extends Registry<IRegistryItem> {
    private static _instance;
    static get instance(): IRegistry<IRegistryItem>;
}
