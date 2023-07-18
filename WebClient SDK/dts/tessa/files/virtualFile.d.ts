import { IFileCreationToken } from './fileCreationToken';
import { IStorage } from 'tessa/platform/storage';
export declare class VirtualFile {
    readonly typeName: string;
    readonly id: guid;
    readonly name: string;
    private modifyFileTokenAction?;
    readonly requestInfo: IStorage;
    constructor(typeName: string, id: guid, name: string, modifyFileTokenAction?: ((token: IFileCreationToken) => void) | undefined, requestInfo?: IStorage);
    modify(token: IFileCreationToken): void;
}
