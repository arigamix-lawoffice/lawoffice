import { IFileSource } from './fileSource';
import { IFile } from './file';
import { IFileSignature } from './fileSignature';
import { VirtualFile } from './virtualFile';
import { VirtualFileVersion } from './virtualFileVersion';
import { FileType, FileCategory, FileCreationToken, FileVersionCreationToken, IFileVersion, CertData } from 'tessa/files';
import { EventHandler, Result } from 'tessa/platform';
import { IUserSession } from 'common/utility/userSession';
import { IStorage } from 'tessa/platform/storage';
import { ISignedData } from 'tessa/cards';
import { ValidationResult } from 'tessa/platform/validation';
export interface FileContainerChangingEventArgs {
    added: IFile | null;
    removed: IFile | null;
    cancel: boolean;
    fileContainer: FileContainer;
}
export interface FileContainerChangedEventArgs {
    added: IFile | null;
    removed: IFile | null;
    fileContainer: FileContainer;
}
export interface IFileContainerPermissions {
    canAdd: boolean;
    seal<T = FileContainerPermissionsSealed>(): T;
}
export interface FileContainerPermissionsSealed {
    readonly canAdd: boolean;
    seal<T = FileContainerPermissionsSealed>(): T;
}
export declare class FileContainerPermissions implements IFileContainerPermissions {
    constructor();
    canAdd: boolean;
    seal<T = FileContainerPermissionsSealed>(): T;
}
export declare class FileContainer {
    constructor(source: IFileSource, permissions?: FileContainerPermissions | null);
    private readonly _source;
    private _files;
    private readonly _permissions;
    private _isInitialized;
    get source(): IFileSource;
    get files(): ReadonlyArray<IFile>;
    get permissions(): FileContainerPermissionsSealed;
    get isInitialized(): boolean;
    init(files?: IFile[] | null): Promise<void>;
    dispose(): void;
    createFile(content: File, type?: FileType | null, category?: FileCategory | null, name?: string | null, user?: IUserSession | null, modifyFileTokenAction?: ((token: FileCreationToken) => void) | null, modifyVersionTokenAction?: ((token: FileVersionCreationToken) => void) | null): IFile;
    addFile(file: IFile, isVirtual?: boolean, notify?: boolean): Promise<void>;
    removeFile(file: IFile, notify?: boolean): Promise<void>;
    removeFile(fileId: guid, notify?: boolean): Promise<void>;
    getFileContent(file: IFile): Promise<Result<File>>;
    getFileContent(fileVersion: IFileVersion): Promise<Result<File>>;
    saveFileContent(file: IFile, info?: IStorage): Promise<Result<boolean>>;
    saveFileContent(fileVersion: IFileVersion, info?: IStorage): Promise<Result<boolean>>;
    getFileLink(file: IFile, info?: IStorage, withVersion?: boolean): string;
    getFileLink(fileVersion: IFileVersion, info?: IStorage): string;
    addSignature(version: IFileVersion, certificate: CertData, signedData: ISignedData, comment?: string, notify?: boolean): Promise<void>;
    removeSignature(version: IFileVersion, signatureToRemove: IFileSignature, notify?: boolean): Promise<void>;
    getClonedFiles(source?: IFileSource | null): IFile[];
    addVirtalFile(fileSource: IFileSource, file: VirtualFile, ...versions: VirtualFileVersion[]): Promise<IFile>;
    private addVirtualVersion;
    ensureAllContentModified(): Promise<ValidationResult>;
    resetDirtyFiles(): void;
    readonly containerFileChanging: EventHandler<(args: FileContainerChangingEventArgs) => void>;
    readonly containerFileChanged: EventHandler<(args: FileContainerChangedEventArgs) => void>;
    readonly initialized: EventHandler<() => void>;
}
