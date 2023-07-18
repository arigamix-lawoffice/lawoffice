import { IExtensionExecutor } from './extensionExecutor';
import { ExtensionStage } from './extensionStage';
export interface IExtensionContainer {
    registerBundle(info: ExtensionBundleInfo): any;
    registerExtension(args: {
        extension: any;
        stage?: ExtensionStage;
        order?: number;
        singleton?: boolean;
        type?: string;
    }): IExtensionContainer;
    resolveExecutor(type: string): IExtensionExecutor;
    resolveExecutor(extension: any): IExtensionExecutor;
    getExtensionsInfo(): Map<string, ExtensionBundleInfo>;
}
export interface ExtensionBundleInfo {
    name: string;
    buildTime: string;
}
export declare class ExtensionContainer implements IExtensionContainer {
    private constructor();
    private static _instance;
    static get instance(): IExtensionContainer;
    private _isSealed;
    private _extensionBundles;
    private _container;
    registerBundle(info: ExtensionBundleInfo): void;
    registerExtension(args: {
        extension: any;
        stage?: ExtensionStage;
        order?: number;
        singleton?: boolean;
        type?: string;
    }): IExtensionContainer;
    resolveExecutor(extension: any): IExtensionExecutor;
    initialize(): void;
    getExtensionsInfo(): Map<string, ExtensionBundleInfo>;
}
