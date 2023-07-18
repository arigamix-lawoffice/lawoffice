import { IFileExtension, IFileExtensionContext } from './interfaces';
export declare class FileExtension implements IFileExtension {
    static readonly type = "FileExtension";
    shouldExecute(_context: IFileExtensionContext): boolean;
    openingMenu(_context: IFileExtensionContext): void;
}
