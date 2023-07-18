import { FileCategory } from './fileCategory';
import { FileType } from './fileType';
import { FilePermissions } from './filePermissions';
import { FileTag } from './fileTag';
import { IStorage } from 'tessa/platform/storage';
import { IFile } from 'tessa/files';
export interface IFileCreationToken {
    id: guid | null;
    name: string;
    category: FileCategory | null;
    type: FileType;
    isLocal: boolean;
    permissions: FilePermissions;
    options: IStorage;
    requestInfo: IStorage;
    size: number;
    lastVersionTags: FileTag[];
    newVersionTags: FileTag[];
    created: string | null;
    createdById: guid | null;
    createdByName: string | null;
    modified: string | null;
    modifiedById: guid | null;
    modifiedByName: string | null;
    set(file: IFile): any;
}
export declare class FileCreationToken implements IFileCreationToken {
    constructor();
    id: guid | null;
    name: string;
    category: FileCategory | null;
    type: FileType;
    isLocal: boolean;
    permissions: FilePermissions;
    options: IStorage;
    requestInfo: IStorage;
    size: number;
    lastVersionTags: FileTag[];
    newVersionTags: FileTag[];
    created: string | null;
    createdById: guid | null;
    createdByName: string | null;
    modified: string | null;
    modifiedById: guid | null;
    modifiedByName: string | null;
    set(file: IFile): void;
}
