import { FileTag } from './fileTag';
import { FileVersionState, IFileVersion } from './fileVersion';
import { FileContentSource } from './fileContentSource';
import { IStorage } from 'tessa/platform/storage';
export interface IFileVersionCreationToken {
    id: guid | null;
    name: string;
    number: number;
    state: FileVersionState;
    created: string;
    createdById: guid;
    createdByName: string;
    size: number;
    linkId: guid | null;
    contentSource: FileContentSource;
    errorDate: string | null;
    errorMessage: string | null;
    options: IStorage;
    requestInfo: IStorage;
    tags: FileTag[];
    set(version: IFileVersion): any;
}
export declare class FileVersionCreationToken implements IFileVersionCreationToken {
    constructor();
    id: guid | null;
    name: string;
    number: number;
    state: FileVersionState;
    created: string;
    createdById: guid;
    createdByName: string;
    size: number;
    linkId: guid | null;
    contentSource: FileContentSource;
    errorDate: string | null;
    errorMessage: string | null;
    options: IStorage;
    requestInfo: IStorage;
    tags: FileTag[];
    set(version: IFileVersion): void;
}
