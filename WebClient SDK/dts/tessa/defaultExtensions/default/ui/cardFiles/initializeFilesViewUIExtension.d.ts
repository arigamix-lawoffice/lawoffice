import { FilesViewGeneratorBaseUIExtension } from './filesViewGeneratorBaseUIExtension';
import { ICardUIExtensionContext } from 'tessa/ui/cards';
export declare class InitializeFilesViewUIExtension extends FilesViewGeneratorBaseUIExtension {
    private _disposes;
    initializing(context: ICardUIExtensionContext): Promise<void>;
    initialized(context: ICardUIExtensionContext): Promise<void>;
    finalized(): void;
    private executeInitializingAction;
    private executeInitializedAction;
    private initializeDefaultGroupInFileControl;
    private initializeExtensionForTableForm;
    private attachViewToFileControlCore;
}
