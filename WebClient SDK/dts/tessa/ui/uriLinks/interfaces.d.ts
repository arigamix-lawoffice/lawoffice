import { IUIContext } from '../uiContext';
import { UriLinkDependencies } from './uriLinkDependencies';
import { UriLinkHandlerEventType } from './uriLinkHandlerEventType';
/**
 * Глобальный обработчик ссылок.
 */
export interface IUriLinkHandler {
    /**
     * Выполняет обработку открываемой ссылки.
     *
     * @param {string} uriString cтроковое представление ссылки.
     * @param {UriLinkHandlerEventType} eventType тип события которое открывает ссылку.
     */
    openAsync(uriString: string, eventType: UriLinkHandlerEventType): Promise<void>;
}
/**
 * Фабрика зависимостей для обработки ссылок.
 */
export interface IUriLinkDependenciesFactory {
    /**
     * Устанавливает кастомный глобальный обработчик ссылок, который может быть использован при создании зависимостей.
     *
     * @param {?IUriLinkHandler} customUriLinkHandler кастомный обработчик ссылок. Или undefined, чтобы фабрика использовала стандартный обработчик.
     */
    setCustomUriLinkHandler(customUriLinkHandler?: IUriLinkHandler): void;
    /**
     * Создает зависимости для обработки ссылок.
     *
     * @param uiContextExecutor @see {@link IUriLinkDependencies.uiContextExecutor}.
     */
    create(uiContextExecutor: (action: (context: IUIContext) => void) => void): UriLinkDependencies;
}
/**
 * Зависимости для обработки ссылок.
 */
export interface IUriLinkDependencies {
    /**
     * Кастомный обработчик ссылок.
     */
    uriLinkHandler: IUriLinkHandler;
    /**
     * Выполняет заданный action в контексте IUIContext.
     */
    uiContextExecutor: (action: (context: IUIContext) => void) => void;
}
