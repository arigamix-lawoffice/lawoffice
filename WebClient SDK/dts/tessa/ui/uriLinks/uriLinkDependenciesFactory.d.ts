import { IUIContext } from '../uiContext';
import { IUriLinkDependenciesFactory, IUriLinkHandler } from './interfaces';
import { UriLinkDependencies } from './uriLinkDependencies';
export declare class UriLinkDependenciesFactory implements IUriLinkDependenciesFactory {
    private constructor();
    private static _instance;
    static get instance(): UriLinkDependenciesFactory;
    private _customUriLinkHandler?;
    private _standardUriLinkHandler;
    create(uiContextExecutor?: (action: (context: IUIContext) => void) => void): UriLinkDependencies;
    setCustomUriLinkHandler(customUriLinkHandler?: IUriLinkHandler): void;
    private executeInEmptyContext;
}
