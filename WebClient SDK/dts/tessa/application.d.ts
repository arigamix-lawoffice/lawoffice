import { History } from 'history';
import { IExtensionExecutor } from 'tessa/extensions';
import { ValidationResult } from 'tessa/platform/validation';
import { ButtonProps, IUserSettings } from './ui';
export declare class Application {
    private constructor();
    private static _instance;
    static get instance(): Application;
    private _isInitialized;
    private _isAuthenticating;
    private _authenticatingMessage;
    private _isLoading;
    private _isLoadingProgress;
    private _loadingOverlay;
    private _history;
    private _listenerDisposer;
    private _executor;
    private _theme;
    private _wallpaper;
    private _userSettings;
    private _personalRoleSatelliteId;
    private _disposes;
    get isInitialized(): boolean;
    get isAuthenticating(): boolean;
    get authenticatingMessage(): ValidationResult | null;
    get isLoading(): boolean;
    set isLoading(value: boolean);
    get isLoadingProgress(): number | null;
    set isLoadingProgress(value: number | null);
    get loadingOverlay(): {
        text: string;
        button: ButtonProps | null;
    } | null;
    set loadingOverlay(value: {
        text: string;
        button: ButtonProps | null;
    } | null);
    get history(): History;
    get executor(): IExtensionExecutor;
    get theme(): string;
    set theme(value: string);
    get wallpaper(): string | null;
    set wallpaper(value: string | null);
    get userSettings(): IUserSettings;
    set userSettings(value: IUserSettings);
    get personalRoleSatelliteId(): guid | null;
    set personalRoleSatelliteId(value: guid | null);
    /**
     * @deprecated
     * Устаревшее свойство, которое будет удалено в будущих версиях.
     * Вместо этого используйте PageLifecycleSingleton.instance.showConfirmBeforeUnload
     */
    static get blockUnloadConfirm(): boolean;
    /**
     * @deprecated
     * Устаревшее свойство, которое будет удалено в будущих версиях.
     * Вместо этого используйте PageLifecycleSingleton.instance.showConfirmBeforeUnload
     */
    static set blockUnloadConfirm(block: boolean);
    static init(history: History): Promise<void>;
    static drop(): void;
    static dropBeforeLogout(): void;
    private static hidden;
    private static restore;
    private static hotkeyListener;
    private static historyListener;
    static loginSucceed(autoLogin?: boolean): Promise<boolean>;
    static logoutSucceed(disableAutoLogin?: boolean): void;
    private static defaultRedirect;
    private static loginRedirect;
    private static processRoute;
    private static canRedirectTo;
    static login(login: string, password: string, redirectTo?: string, getErrorMessage?: () => ValidationResult): Promise<void>;
    static winLogin(redirectTo?: string, getErrorMessage?: () => ValidationResult): Promise<void>;
    static samlLogin(redirectTo?: string): void;
    private static loginInternal;
    private static loginRequest;
    static logout(silent?: boolean, disableAutoLogin?: boolean): Promise<void>;
    private static logoutRequest;
    private static loadMeta;
    static cleanSWCache(onlyMeta?: boolean): Promise<boolean>;
    private static handleSWError;
}
