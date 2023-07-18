/**
 * Выполняет запрос к серверу и отправляет на логин, если токен неправильный или закончилась сессия.
 */
export declare const fetchWithLogoutCheck: (path: any, options?: RequestInit) => Promise<Response>;
/**
 * Выполняет запрос к серверу и отправляет на логин, если токен неправильный или закончилась сессия,
 * и выполняет стандартную обработку.
 */
export declare const fetchWithLogoutCheckAndDefaultThen: (path: any, options?: RequestInit | undefined, getResult?: ((resp: Response) => Promise<any>) | undefined) => Promise<any>;
/**
 * Выполняет стандартную обработку ответа от сервера.
 */
export declare const processDefaultThen: (promise: Promise<Response>, getResult?: ((resp: Response) => Promise<any>) | undefined) => Promise<any>;
/**
 * Отправляет POST-запрос на указанный url с указанным body.
 * @returns boolean. true, если успешно отправлено или поставлено в очередь отправки; в противном случае - false.
 */
export declare const postOnPageClose: (url: string, body?: BodyInit | undefined) => boolean;
/**
 * Отправляет GET-запрос на указанный url.
 * @returns boolean. true, если успешно отправлено; в противном случае - false.
 */
export declare const getOnPageClose: (url: string) => boolean;
