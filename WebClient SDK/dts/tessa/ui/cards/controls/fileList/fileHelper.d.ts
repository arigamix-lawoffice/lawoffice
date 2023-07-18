import { FileListVersionsDialogViewModel } from './fileListVersionsDialogViewModel';
import { FileCategory, FileType, CertData, IFileVersion, FileContainer } from 'tessa/files';
import { FileInfo, IFileControl } from 'tessa/ui/files';
import { ICAdESProvider } from 'tessa/cards';
import { FileViewModel } from './fileViewModel';
import { ValidationResult } from 'tessa/platform/validation';
export declare function addFiles(control: IFileControl, contents: ReadonlyArray<File>, fileTypes: FileType[]): Promise<void>;
export declare function showFileListVersionsDialog(viewModel: FileListVersionsDialogViewModel): Promise<any>;
export declare function validateFileContent(control: IFileControl, fileInfo: FileInfo): Promise<ValidationResult>;
export interface SelectCategoryResult {
    cancel: boolean;
    category: FileCategory | null;
}
export interface SelectCategoriesWithValidationResult {
    cancel: boolean;
    categories: (FileCategory | null)[];
    validatedFiles: FileInfo[];
}
export declare function selectFileCategoriesWithValidation(control: IFileControl, fileInfos: FileInfo[], autoSelectWhenFiltering: boolean, reselectOnValidationFailed: boolean): Promise<SelectCategoriesWithValidationResult>;
export declare function selectFileCategory(control: IFileControl, fileInfos: FileInfo[], autoSelectWhenFiltering: boolean): Promise<SelectCategoryResult>;
export interface SelectFileTypeResult {
    cancel: boolean;
    type: FileType | null;
}
export declare function selectFileType(control: IFileControl, fileTypes: FileType[], fileInfos: FileInfo[], categories: (FileCategory | null)[]): Promise<SelectFileTypeResult>;
export interface RenameResult {
    cancel: boolean;
    name: string;
}
export declare function renameFile(name: string): Promise<RenameResult>;
export declare function showFileSigns(fileContainer: FileContainer, edsProvider: ICAdESProvider, version: IFileVersion, showVerifyInfo?: boolean): Promise<void>;
export declare function checkSigns(edsProvider: ICAdESProvider, version: IFileVersion): Promise<void>;
export interface SelectFileCertResult {
    cancel: boolean;
    cert: CertData | null;
    comment: string | null;
}
export declare function selectFileCerts(edsProvider: ICAdESProvider, noCommentDialog?: boolean): Promise<SelectFileCertResult>;
export declare function setSign(edsProvider: ICAdESProvider, version: IFileVersion, cert: CertData): Promise<import("tessa/cards").ISignedData>;
export declare function getFileNameAndExtension(name: string): {
    name: string;
    ext: string;
};
export declare function getSelectedFilesPreviewMessage(files: ReadonlyArray<FileViewModel>): {
    main: string;
    additional: string;
};
export declare const isCanceledError: (error: unknown) => boolean;
