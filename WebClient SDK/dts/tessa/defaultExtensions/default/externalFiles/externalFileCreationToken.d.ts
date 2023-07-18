import { FileCreationToken, IFile } from 'tessa/files';
export declare class ExternalFileCreationToken extends FileCreationToken {
    description: string | Blob;
    set(file: IFile): void;
}
