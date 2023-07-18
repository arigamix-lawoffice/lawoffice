import { FileCategory } from 'tessa/files';
import { IValidationResultBuilder } from 'tessa/platform/validation';
import { FileInfo } from './fileInfo';
import { IFileValidateCategoryContext } from './interfaces';
export declare class FileValidateCategoryContext implements IFileValidateCategoryContext {
    constructor(fileInfo: FileInfo, validationResult: IValidationResultBuilder, category: FileCategory | null);
    readonly fileInfo: FileInfo;
    readonly validationResult: IValidationResultBuilder;
    readonly category: FileCategory | null;
}
