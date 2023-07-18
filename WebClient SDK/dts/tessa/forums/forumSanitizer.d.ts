import { ISanitizer } from 'tessa';
export declare class ForumSanitizer implements ISanitizer {
    static current: ISanitizer;
    private static _instance;
    static get instance(): ForumSanitizer;
    sanitize(html: string): string;
}
