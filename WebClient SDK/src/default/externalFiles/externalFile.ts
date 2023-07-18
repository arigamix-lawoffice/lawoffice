import {
  TessaFile,
  FileCategory,
  FileType,
  IFileSource,
  FilePermissionsSealed,
  IFile
} from 'tessa/files';

export class ExternalFile extends TessaFile {
  constructor(
    id: guid,
    name: string,
    category: FileCategory | null,
    type: FileType,
    source: IFileSource,
    permissions: FilePermissionsSealed | null,
    modified: string | null,
    modifiedById: guid | null,
    modifiedByName: string | null,
    created: string | null,
    createdById: guid | null,
    createdByName: string | null,
    isLocal = true,
    origin: IFile | null = null,
    description: string | Blob
  ) {
    super(
      id,
      name,
      category,
      type,
      source,
      permissions,
      modified,
      modifiedById,
      modifiedByName,
      created,
      createdById,
      createdByName,
      isLocal,
      origin
    );
    this.description = description;
  }

  public description: string | Blob;
}
