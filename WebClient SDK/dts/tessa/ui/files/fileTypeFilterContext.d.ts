import { FileType, FileCategory } from 'tessa/files';
import { FileInfo } from './fileInfo';
import { IFileTypeFilterContext } from './interfaces';
export declare class FileTypeFilterContext implements IFileTypeFilterContext {
    constructor(types: FileType[], fileInfos: FileInfo[], categories: (FileCategory | null)[]);
    readonly types: FileType[];
    readonly fileInfos: FileInfo[];
    readonly categories: (FileCategory | null)[];
}
