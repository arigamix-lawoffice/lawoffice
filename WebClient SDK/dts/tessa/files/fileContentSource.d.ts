export declare class FileContentSource {
    constructor(id: number, name: string);
    readonly id: number;
    readonly name: string;
    static readonly database: FileContentSource;
    static readonly fileSystem: FileContentSource;
}
