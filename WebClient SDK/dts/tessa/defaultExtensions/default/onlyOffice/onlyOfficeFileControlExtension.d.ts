import { FileControlExtension, IFileControl, IFileControlExtensionContext } from 'tessa/ui/files';
import { FileContainer } from 'tessa/files';
import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
/**
 * Представляет собой расширение, которое добавляет возможность создания файлов по шаблону.
 */
export declare class OnlyOfficeFileControlExtension extends FileControlExtension {
    initializing(context: IFileControlExtensionContext): void;
    openingMenu(context: IFileControlExtensionContext): void;
    static createEmptyWordFileAction(control: IFileControl, container: FileContainer, newFileNameCaption: string): Promise<void>;
    static createEmptyExcelFileAction(control: IFileControl, container: FileContainer, newFileNameCaption: string): Promise<void>;
    static createEmptyPowerPointFileAction(control: IFileControl, container: FileContainer, newFileNameCaption: string): Promise<void>;
    shouldExecute(): boolean;
}
export declare class OnlyOfficeViewFileControlExtension extends CardUIExtension {
    initialized(context: ICardUIExtensionContext): Promise<void>;
    private executeInitializedAction;
    private initializeViewControl;
}
