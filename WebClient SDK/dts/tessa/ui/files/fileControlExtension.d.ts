import { IFileControlExtension, IFileControlExtensionContext } from './interfaces';
export declare class FileControlExtension implements IFileControlExtension {
    static readonly type = "FileControlExtension";
    shouldExecute(_context: IFileControlExtensionContext): boolean;
    initializing(_context: IFileControlExtensionContext): void;
    initialized(_context: IFileControlExtensionContext): void;
    finalized(_context: IFileControlExtensionContext): void;
    openingMenu(_context: IFileControlExtensionContext): void;
}
