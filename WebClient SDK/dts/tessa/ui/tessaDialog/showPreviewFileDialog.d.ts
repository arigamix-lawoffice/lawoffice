import React from 'react';
import { DialogProps } from 'ui/dialog/dialog';
import { IFileControlManager } from 'tessa/ui/files';
import { IFile } from 'tessa/files';
export declare type PreviewFileDialogOpts = Partial<Omit<PreviewFileDialogViewModel, 'previewManager' | 'close' | 'file' | 'public' | 'setCloseRequest' | 'setProps'>>;
export declare class PreviewFileDialogViewModel {
    constructor(fileControlManager: IFileControlManager, file: IFile);
    private _previewManager;
    private _dialogStyle;
    private _dialogProps;
    private _closeRequest;
    file: IFile;
    reset: boolean;
    get previewManager(): IFileControlManager;
    set previewManager(previewManager: IFileControlManager);
    get dialogStyle(): React.CSSProperties;
    set dialogStyle(dialogStyle: React.CSSProperties);
    get dialogProps(): Omit<DialogProps, 'onCloseRequest'>;
    set dialogProps(props: Omit<DialogProps, 'onCloseRequest'>);
    onUnmountDialog: (() => void) | undefined;
    onMountDialog: (() => void) | undefined;
    setCloseRequest(request: () => void): void;
    close(): void;
    setProps(opts: PreviewFileDialogOpts): void;
}
export declare function showFilePreviewDialog(previewManager: PreviewFileDialogViewModel): Promise<PreviewFileDialogViewModel>;
