import { IFileVersionCreationToken } from './fileVersionCreationToken';
import { IStorage } from 'tessa/platform/storage';
export declare class VirtualFileVersion {
    readonly id: guid;
    readonly name: string;
    private modifyVersionTokenAction?;
    readonly requestInfo: IStorage;
    constructor(id: guid, name: string, modifyVersionTokenAction?: ((token: IFileVersionCreationToken) => void) | undefined, requestInfo?: IStorage);
    modify(token: IFileVersionCreationToken): void;
}
