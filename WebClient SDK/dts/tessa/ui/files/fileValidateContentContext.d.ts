import { IValidationResultBuilder } from 'tessa/platform/validation';
import { FileInfo } from './fileInfo';
import { IFileValidateContentContext } from './interfaces';
export declare class FileValidateContentContext implements IFileValidateContentContext {
    constructor(fileInfo: FileInfo, validationResult: IValidationResultBuilder);
    readonly fileInfo: FileInfo;
    readonly validationResult: IValidationResultBuilder;
}
