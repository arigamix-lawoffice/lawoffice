import { ApplicationExtension, IApplicationExtensionMetadataContext } from 'tessa';
import { FileControlExtension, FileExtension, FileExtensionContext, FileVersionExtension, FileVersionExtensionContext, IFileControl, IFileControlExtensionContext } from 'tessa/ui/files';
import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
export declare class DeskiExtension extends ApplicationExtension {
    private _lastMasterKeysUpdate;
    afterMetadataReceived(_context: IApplicationExtensionMetadataContext): Promise<void>;
}
export declare class DeskiFileExtension extends FileExtension {
    openingMenu(context: FileExtensionContext): void;
}
export declare class DeskiFileControlExtension extends FileControlExtension {
    initializing(context: IFileControlExtensionContext): void;
    openingMenu(context: IFileControlExtensionContext): void;
    static pasteFromClipboardAction(control: IFileControl): Promise<void>;
}
export declare class DeskiViewFileControlExtension extends CardUIExtension {
    initialized(context: ICardUIExtensionContext): Promise<void>;
    private executeInitializedAction;
    private initializeViewControl;
}
export declare class DeskiFileVersionExtension extends FileVersionExtension {
    static showOpenForReadWarningMessage: boolean;
    openingMenu(context: FileVersionExtensionContext): void;
}
export declare class DeskiUIExtension extends CardUIExtension {
    shouldExecute(): boolean;
    initialized(context: ICardUIExtensionContext): void;
    saving(context: ICardUIExtensionContext): Promise<void>;
    finalized(context: ICardUIExtensionContext): Promise<void>;
}
