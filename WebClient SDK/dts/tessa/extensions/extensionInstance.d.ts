import { IExtension } from './extension';
import { ExtensionStage } from './extensionStage';
export declare class ExtensionInstance {
    constructor(factory: () => IExtension, stage: ExtensionStage, order: number, singleton: boolean);
    private readonly _factory;
    private _singletonInstance;
    readonly stage: ExtensionStage;
    readonly order: number;
    readonly singleton: boolean;
    getExtension(): IExtension;
}
