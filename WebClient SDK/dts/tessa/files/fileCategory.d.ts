export declare class FileCategory {
    constructor(id: guid | null, caption: string);
    readonly id: guid | null;
    readonly caption: string;
    static equals(a: FileCategory | null, b: FileCategory | null): boolean;
}
