export interface ISessionService {
    closeSession(): Promise<boolean>;
    closeSessionAsAdmin(sessionID: guid): Promise<boolean>;
}
export declare class SessionService implements ISessionService {
    private static _instance;
    static get instance(): SessionService;
    closeSessionAsAdmin(sessionID: guid): Promise<any>;
    closeSession(): Promise<any>;
}
