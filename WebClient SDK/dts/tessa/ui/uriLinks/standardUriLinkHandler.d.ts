import { IUriLinkHandler } from './interfaces';
import { UriLinkHandlerEventType } from './uriLinkHandlerEventType';
export declare class StandardUriLinkHandler implements IUriLinkHandler {
    openAsync(uriString: string, _: UriLinkHandlerEventType): Promise<void>;
}
