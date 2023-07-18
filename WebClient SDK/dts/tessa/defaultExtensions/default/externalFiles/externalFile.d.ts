import { TessaFile, FileCategory, FileType, IFileSource, FilePermissionsSealed, IFile } from 'tessa/files';
export declare class ExternalFile extends TessaFile {
    constructor(id: guid, name: string, category: FileCategory | null, type: FileType, source: IFileSource, permissions: FilePermissionsSealed | null, modified: string | null, modifiedById: guid | null, modifiedByName: string | null, created: string | null, createdById: guid | null, createdByName: string | null, isLocal: boolean | undefined, origin: IFile | null | undefined, description: string | Blob);
    description: string | Blob;
}
