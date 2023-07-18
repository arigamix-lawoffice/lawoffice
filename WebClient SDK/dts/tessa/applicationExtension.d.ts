import { IApplicationExtensionContext, IApplicationExtensionMetadataContext, IApplicationExtensionRouteContext } from './applicationExtensionContext';
import { IExtension } from './extensions';
/**
 * Базовый класс для расширения, связанного с жизненным циклом приложения.
 */
export interface IApplicationExtension extends IExtension {
    /**
     * Данный метод вызывается единожды при первичной инициализации приложения
     * после подтверждения аутентификации.
     */
    initialize(context: IApplicationExtensionContext): void;
    /**
     * Данный метод вызывается единожды после получения метаданных
     * после подтверждения аутентификации.
     */
    afterMetadataReceived(context: IApplicationExtensionContext): Promise<void>;
    /**
     * Данный метод вызывается единожды при первичном утверждении маршрута
     * после подтверждения аутентификации.
     */
    routeResolve(context: IApplicationExtensionRouteContext): void;
    /**
     * Данный метод вызывается, когда вкладка теряет фокус или закрывается.
     */
    hidden(context: IApplicationExtensionRouteContext): void;
    /**
     * Данный метод вызывается, когда вкладка браузера получает фокус.
     */
    restore(context: IApplicationExtensionRouteContext): void;
    /**
     * Данный метод вызывается при закрытии вкладки браузера.
     *
     * Учтите, что состояние `Terminated` жизненного
     * цикла Page Lifecycle API на мобильных устройствах наступает только если пользователь обновляет
     * страницу или переходит в этой же вкладке по другому URL.
     * Если же пользователь закрывает вкладку/приложение, состояние не наступит.
     * Поэтому, если необходимо выполнить работу перед закрытием страницы, рекомендуем рассмотреть состояние hidden.
     * Однако данное состояние является идеальным вариантом для очистки связанных с приложением ресурсов,
     * которые потом нельзя восстановить.
     *
     * `FireFox не обрабатывает` никакие запросы в состоянии `Terminated` жизненного
     * цикла Page Lifecycle API, если открыта только одна вкладка браузера.
     * Если открыто две и более вкладок - запросы работают стабильно.
     * `(см. PageLifecycleState)`
     */
    finalize(context: IApplicationExtensionContext): void;
}
/**
 * Базовый класс для расширения, связанного с жизненным циклом приложения.
 */
export declare class ApplicationExtension implements IApplicationExtension {
    static readonly type = "ApplicationExtension";
    shouldExecute(_context: unknown): boolean;
    initialize(_context: IApplicationExtensionContext): void;
    afterMetadataReceived(_context: IApplicationExtensionMetadataContext): Promise<void>;
    routeResolve(_context: IApplicationExtensionRouteContext): void;
    hidden(_context: IApplicationExtensionContext): void;
    restore(_context: IApplicationExtensionContext): void;
    finalize(_context: IApplicationExtensionContext): void;
}
