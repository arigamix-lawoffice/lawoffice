import { FileExtension, IFileExtensionContext } from 'tessa/ui/files';
/** Расширение, добавляющее пункт "Распознавание текста" в контекстное меню файла. */
export declare class OcrMenuFileExtension extends FileExtension {
    shouldExecute(_context: IFileExtensionContext): boolean;
    openingMenu(context: IFileExtensionContext): void;
}
