export interface ISanitizer {
    sanitize(html: string): string;
}
export declare class TessaSanitizer implements ISanitizer {
    sanitize(html: string): string;
}
