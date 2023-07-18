import { IUserSession } from 'common/utility/userSession';
export declare const TessaClientApplicationAlias = "arigamix";
export declare const TessaProtocol = "arigamix";
export declare function getWebLink(action?: string, parameters?: Map<string, string>): string;
export declare function getClientLink(session: IUserSession, action: string, parameters: Map<string, string>): string;
