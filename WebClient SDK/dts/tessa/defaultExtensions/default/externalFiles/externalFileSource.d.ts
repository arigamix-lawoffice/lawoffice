import { FileSource, IFileCreationToken, IFile, IFileVersion } from 'tessa/files';
import { Result, ResultWithInfo } from 'tessa/platform';
import { IStorage } from 'tessa/platform/storage';
export declare class ExternalFileSource extends FileSource {
    protected createFileCore(token: IFileCreationToken): IFile;
    protected getFileCreationTokenCore(): IFileCreationToken;
    protected getContentCore(fileOrFileVersion: IFile | IFileVersion, _info?: IStorage): Promise<ResultWithInfo<File>>;
    protected saveContentCore(fileOrFileVersion: IFile | IFileVersion, info?: IStorage): Promise<Result<boolean>>;
}
