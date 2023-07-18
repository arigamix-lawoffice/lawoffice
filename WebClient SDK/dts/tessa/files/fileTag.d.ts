export declare class FileTag {
    readonly key: string;
    constructor(key: string);
    equals(other?: FileTag | null): boolean;
    static parse(tags?: string | null): string[];
    static parseAndBox(tags?: string | null): FileTag[];
    static aggregate(...tagKeys: string[]): string;
}
