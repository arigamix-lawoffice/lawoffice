export interface IViewComponentRegistry {
    register(viewModel: any, componentFactory: Function): any;
    get(viewModel: any): Function;
}
export declare class ViewComponentRegistry implements IViewComponentRegistry {
    private constructor();
    private static _instance;
    static get instance(): IViewComponentRegistry;
    private registry;
    register(viewModel: any, componentFactory: Function): void;
    get(viewModel: any): Function;
}
