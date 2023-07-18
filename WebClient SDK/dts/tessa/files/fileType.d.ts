export declare class FileType {
    constructor(id: guid, caption: string);
    readonly id: guid;
    readonly caption: string;
    static equals(a: FileType, b: FileType): boolean;
}
