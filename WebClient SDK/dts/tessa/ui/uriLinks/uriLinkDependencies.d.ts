import { IUIContext } from '../uiContext';
import { IUriLinkDependencies, IUriLinkHandler } from './interfaces';
export declare class UriLinkDependencies implements IUriLinkDependencies {
    uriLinkHandler: IUriLinkHandler;
    uiContextExecutor: (action: (context: IUIContext) => void) => void;
    constructor(uriLinkHandler: IUriLinkHandler, uiContextExecutor: (action: (context: IUIContext) => void) => void);
}
