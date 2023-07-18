import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
/** Расширение, реализующее функционал постраничного сравнения файлов. */
export declare class OcrCompareFilesUIExtension extends CardUIExtension {
    private _commonDisposers;
    private _syncPageEventDisposers;
    shouldExecute(context: ICardUIExtensionContext): boolean;
    initialized(context: ICardUIExtensionContext): Promise<void>;
    finalized(): void;
    private static modifyPreviewer;
    private static modifyPreviewerForRecognitionMode;
    private static onRowCheckHandler;
    private static colorizeRowsFunc;
    private setPageSyncEvents;
    private static generateContextMenu;
    private static dispose;
}
