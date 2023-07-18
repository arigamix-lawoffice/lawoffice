export declare const CACHE_NAME = "tessa-web-cache-v2";
export declare function isMetaRequest(request: Request): boolean;
export declare enum SWMessageType {
    InitSW = 1,
    AnotherTabFound = 2,
    UrlFromAnotherTab = 3,
    DontLoadApp = 4,
    LoadApp = 5,
    AnotherTabNotFound = 6
}
