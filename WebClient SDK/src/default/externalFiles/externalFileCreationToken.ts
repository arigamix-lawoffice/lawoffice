import { ExternalFile } from './externalFile';
import { FileCreationToken, IFile } from 'tessa/files';

export class ExternalFileCreationToken extends FileCreationToken {
  public description: string | Blob;

  public set(file: IFile) {
    super.set(file);

    this.description = file instanceof ExternalFile ? file.description : '';
  }
}
