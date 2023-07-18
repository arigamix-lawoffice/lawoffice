import { FileExtension, IFileExtensionContext } from 'tessa/ui/files';
export declare class CarFileExtension extends FileExtension {
    shouldExecute(): boolean;
    openingMenu(context: IFileExtensionContext): void;
}
