export interface IComponentRegistry {
    register(key: string, componentFactory: Function): any;
    get(key: string): Function | undefined;
}
export declare class ComponentRegistry implements IComponentRegistry {
    private constructor();
    private static _instance;
    static get instance(): IComponentRegistry;
    private registry;
    register(key: string, componentFactory: Function): void;
    get(key: string): Function | undefined;
}
