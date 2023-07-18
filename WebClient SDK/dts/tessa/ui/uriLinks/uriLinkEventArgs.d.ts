/**
 * Аргументы события открытия ссылки.
 */
export declare class UriLinkEventArgs {
    /**
     * Строковое представление ссылки.
     */
    uriString: string;
    /**
     * Признак того, что событие обработано.
     */
    handled: boolean;
    /**
     * Признак того, что следует отменить обработку ссылки глобальным обработчиком.
     */
    cancel: boolean;
    constructor(uriString: string);
}
