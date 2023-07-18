import { OnlyOfficeApi } from './onlyOfficeApi';
/**
 * Синглтон-объект, представляющий API редактора документов и его хранилища.
 */
export declare class OnlyOfficeApiSingleton {
    private static _instance;
    private constructor();
    static get instance(): OnlyOfficeApi;
    static get isAvailable(): boolean;
    static init(api: OnlyOfficeApi): void;
}
