import type { History } from 'history';
import { InitializationResponse } from 'tessa/platform/initialization';
export interface IApplicationExtensionContext {
}
export declare class ApplicationExtensionContext implements IApplicationExtensionContext {
}
export interface IApplicationExtensionMetadataContext {
    response: InitializationResponse | null;
    readonly autoLogin: boolean;
}
export declare class ApplicationExtensionMetadataContext implements IApplicationExtensionMetadataContext {
    constructor(autoLogin?: boolean);
    response: InitializationResponse | null;
    readonly autoLogin: boolean;
}
export interface IApplicationExtensionRouteContext {
    readonly history: History;
    path: string;
    resolved: boolean;
}
