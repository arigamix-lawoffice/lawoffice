import { IFile } from './file';
import { FileEntity, IFileEntity } from './fileEntity';
import { IFileSignature } from './fileSignature';
import { FileContentSource } from './fileContentSource';
import { FileSignatureLoadingMode } from './fileSignatureLoadingMode';
import { FileTag } from './fileTag';
import { IStorage } from 'tessa/platform/storage';
import { ValidationResult } from 'tessa/platform/validation';
export interface IFileVersion extends IFileEntity {
    name: string;
    readonly state: FileVersionState;
    readonly size: number;
    readonly content: File | null;
    readonly options: IStorage;
    readonly requestInfo: IStorage;
    readonly info: IStorage;
    readonly number: number;
    readonly created: string;
    readonly createdById: guid;
    readonly createdByName: guid;
    readonly linkId: guid | null;
    readonly errorDate: string | null;
    readonly errorMessage: string | null;
    readonly file: IFile;
    readonly contentSource: FileContentSource;
    readonly signatures: IFileSignature[];
    readonly tags: FileTag[];
    signaturesAdded: IFileSignature[];
    signaturesAreComprehensive: boolean;
    signaturesHasComprehensiveData: boolean;
    isDirty: boolean;
    readonly contentModified: string | null;
    ensureContentDownloaded(): Promise<ValidationResult>;
    ensureContentModified(): Promise<ValidationResult>;
    ensureSignaturesLoaded(loadingMode?: FileSignatureLoadingMode): Promise<ValidationResult>;
    setContent(content: File | null): any;
    getExtension(): string;
    clone(file: IFile): IFileVersion;
    invalidateContent(): Promise<ValidationResult>;
}
export declare enum FileVersionState {
    Created = 0,
    Uploading = 1,
    Success = 2,
    Error = 3
}
export declare class FileVersion extends FileEntity implements IFileVersion {
    constructor(id: guid, name: string, num: number, state: FileVersionState, created: string, createdById: guid, createdByName: string, content: File | null, contentSource: FileContentSource, size: number, errorDate: string | null, errorMessage: string | null, file: IFile, linkId: guid | null);
    private _name;
    private _content;
    private _size;
    private _isDirty;
    private _contentModified;
    get name(): string;
    set name(value: string);
    readonly state: FileVersionState;
    get size(): number;
    set size(value: number);
    get content(): File | null;
    readonly options: IStorage;
    readonly requestInfo: IStorage;
    readonly info: IStorage;
    readonly number: number;
    readonly created: string;
    readonly createdById: guid;
    readonly createdByName: guid;
    readonly linkId: guid | null;
    readonly errorDate: string | null;
    readonly errorMessage: string | null;
    readonly file: IFile;
    readonly contentSource: FileContentSource;
    readonly signatures: IFileSignature[];
    readonly tags: FileTag[];
    signaturesAdded: IFileSignature[];
    signaturesAreComprehensive: boolean;
    signaturesHasComprehensiveData: boolean;
    get isDirty(): boolean;
    set isDirty(value: boolean);
    get contentModified(): string | null;
    ensureContentDownloaded(): Promise<ValidationResult>;
    ensureContentModified(): Promise<ValidationResult>;
    ensureSignaturesLoaded(loadingMode?: FileSignatureLoadingMode): Promise<ValidationResult>;
    setContent(content: File | null): void;
    getExtension(): string;
    clone(file: IFile): IFileVersion;
    invalidateContent(): Promise<ValidationResult>;
    private tryGetLocalContent;
}
