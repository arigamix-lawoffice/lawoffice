import { saveAs } from 'file-saver';
import { ExternalFileCreationToken } from './externalFileCreationToken';
import { ExternalFile } from './externalFile';
import { FileSource, IFileCreationToken, IFile, IFileVersion, FileVersion } from 'tessa/files';
import { Guid, Result, ResultWithInfo } from 'tessa/platform';
import { IStorage } from 'tessa/platform/storage';
import {
  ValidationResult,
  ValidationResultBuilder,
  ValidationResultType
} from 'tessa/platform/validation';

export class ExternalFileSource extends FileSource {
  protected createFileCore(token: IFileCreationToken): IFile {
    // здесь возможно исключение, связанное с параметрами метода
    if (!(token instanceof ExternalFileCreationToken)) {
      throw new Error('Unexpected token type.');
    }

    // все свойства токена проверяются в конструкторе
    return new ExternalFile(
      token.id || Guid.newGuid(),
      token.name,
      token.category,
      token.type,
      this,
      token.permissions.clone(),
      token.modified,
      token.modifiedById,
      token.modifiedByName,
      token.created,
      token.createdById,
      token.createdByName,
      token.isLocal,
      null,
      token.description
    );
  }

  protected getFileCreationTokenCore(): IFileCreationToken {
    return new ExternalFileCreationToken();
  }

  protected async getContentCore(
    fileOrFileVersion: IFile | IFileVersion,
    _info?: IStorage
  ): Promise<ResultWithInfo<File>> {
    const file =
      fileOrFileVersion instanceof FileVersion ? fileOrFileVersion.file : fileOrFileVersion;

    const builder = new ValidationResultBuilder();
    if (!(file instanceof ExternalFile)) {
      builder.add(ValidationResult.fromText('Unexpected file type.', ValidationResultType.Error));
    }
    const description = (file as ExternalFile).description;

    return {
      data: new File([description], fileOrFileVersion.name),
      info: {},
      validationResult: builder.build()
    };
  }

  protected async saveContentCore(
    fileOrFileVersion: IFile | IFileVersion,
    info?: IStorage
  ): Promise<Result<boolean>> {
    const result = await this.getContentCore(fileOrFileVersion, info);
    if (!result.data) {
      return {
        data: false,
        validationResult: result.validationResult
      };
    }

    await saveAs(result.data, fileOrFileVersion.name);

    return {
      data: true,
      validationResult: result.validationResult
    };
  }
}
