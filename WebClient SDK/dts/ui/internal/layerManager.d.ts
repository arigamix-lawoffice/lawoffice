declare class DialogOverlayHelper {
    currentZIndex: number;
    setZIndex(index: number): void;
}
export declare class LayerManager {
    zIndex: number;
    dialogs: {
        order: number;
        dialog: HTMLElement | null;
        dialogType?: string;
    }[];
    helper: DialogOverlayHelper;
    constructor();
    private calculateZIndex;
    getIndex(): number;
    getNextIndex(): number;
    pushDialog(dialog: HTMLElement | null, dialogType?: string): void;
    removeDialog(dialog: HTMLElement | null): void;
    isTopDialog(dialog: HTMLElement | null): boolean;
    focusCurrentDialog(): void;
    isAnyDialogsExists(): boolean;
    isLastDialogLoadingOverlay(): boolean | undefined;
}
declare const LayerManagerRef: LayerManager;
export default LayerManagerRef;
