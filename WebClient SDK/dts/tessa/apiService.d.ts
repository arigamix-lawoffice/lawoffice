export interface TessaRequestInit extends RequestInit {
    credentials: 'same-origin';
    headers: Headers;
}
export declare class ApiService {
    private constructor();
    private static _instance;
    static get instance(): ApiService;
    private _servicePath;
    initialize(path: string): void;
    getDefaultOptions(): TessaRequestInit;
    setDefaultXHROptions(xhr: XMLHttpRequest): XMLHttpRequest;
    getURL(url?: string): string;
    getStatic(url: string, options?: RequestInit | null, getResult?: (resp: Response) => Promise<any>): Promise<any>;
    get(url: string, options?: RequestInit | null): Promise<any>;
    post(url: string, options?: RequestInit | null): Promise<any>;
    delete(url: string, options?: RequestInit | null): Promise<any>;
    fetch<T = Response>(url: string, options?: RequestInit | null, getResult?: (reponse: Response) => Promise<T>): Promise<T>;
}
