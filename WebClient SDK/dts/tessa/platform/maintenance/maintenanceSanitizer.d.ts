export interface ISanitizer {
    sanitize(html: string): string;
}
export declare class MaintenanceSanitizer implements ISanitizer {
    private static ALLOWED_TAGS;
    private static ALLOWED_ATTR;
    static current: ISanitizer;
    private static _instance;
    static get instance(): MaintenanceSanitizer;
    sanitize(html: string): string;
}
