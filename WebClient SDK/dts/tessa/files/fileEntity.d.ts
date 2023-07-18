import { IFileSource } from './fileSource';
export interface IFileEntity {
    readonly id: guid;
    readonly source: IFileSource;
    equals(other?: IFileEntity | null): boolean;
}
export declare abstract class FileEntity implements IFileEntity {
    constructor(id: guid, source: IFileSource);
    protected _source: IFileSource;
    readonly id: guid;
    get source(): IFileSource;
    equals(other?: IFileEntity | null): boolean;
}
