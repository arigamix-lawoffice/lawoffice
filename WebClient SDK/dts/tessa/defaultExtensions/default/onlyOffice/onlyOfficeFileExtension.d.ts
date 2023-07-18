import { FileExtension, IFileExtensionContext } from 'tessa/ui/files';
/**
 * Представляет собой расширение, которое добавляет возможность открытия файла в редакторе OnlyOffice.
 */
export declare class OnlyOfficeFileExtension extends FileExtension {
    openingMenu(context: IFileExtensionContext): void;
    shouldExecute(): boolean;
}
