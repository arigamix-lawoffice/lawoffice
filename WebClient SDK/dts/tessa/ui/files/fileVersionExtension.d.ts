import { IFileVersionExtension, IFileVersionExtensionContext } from './interfaces';
export declare class FileVersionExtension implements IFileVersionExtension {
    static readonly type = "FileVersionExtension";
    shouldExecute(_context: IFileVersionExtensionContext): boolean;
    openingMenu(_context: IFileVersionExtensionContext): void;
}
