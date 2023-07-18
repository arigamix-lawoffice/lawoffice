import { IFile, IFileVersion } from 'tessa/files';
import { Result } from 'tessa/platform';
import { IPreviewerViewModel } from 'tessa/ui/cards/controls/previewer/previewerViewModel';
import { IFileControlManager } from 'tessa/ui/files';
import { PreviewFileDialogViewModel, PreviewFileDialogOpts } from 'tessa/ui/tessaDialog';
export declare class PreviewManager implements IFileControlManager {
    constructor();
    private _filesAnglesAtom;
    private _preferPdfInfiniteMode;
    private _previewToolViewModel;
    private readonly _filesAngles;
    private readonly _previewPdfEnabled;
    private _currentLoadingVersionId;
    previewToolFactory: (version: IFileVersion) => {
        type: string;
        createViewModelFunc: () => IPreviewerViewModel;
    } | null;
    private _message;
    private _previewInDialog;
    get previewInDialog(): boolean;
    set previewInDialog(val: boolean);
    enabled: boolean;
    get previewPdfEnabled(): boolean;
    get message(): {
        main: string;
        additional?: string | null;
    } | null;
    set message(value: {
        main: string;
        additional?: string | null;
    } | null);
    get inProgress(): boolean;
    setEnabled(enabled?: boolean): void;
    showDefaultPreview(file: IFile): void;
    showPreviewInDialog(file: IFile, previewFileDialogOpts?: PreviewFileDialogOpts): Promise<PreviewFileDialogViewModel>;
    showPreview(file: IFile, previewFileDialogOpts?: PreviewFileDialogOpts): Promise<PreviewFileDialogViewModel | void>;
    reset(): void;
    resetPreview(): void;
    resetIfInPreview(file: IFile): void;
    resetIfInPreview(versionId: guid): void;
    handleFileContentLoading: (version: IFileVersion, callback: (result: Result<File>) => Promise<void>, handleParams?: {
        showLoadingMessage?: boolean | undefined;
        showErrorMessage?: boolean | undefined;
    } | undefined) => Promise<void>;
    /**
     * Углы поворота файлов в предпросмотре
     *
     * @readonly
     * @type {{ [key: string]: number }}
     */
    get filesAngles(): {
        [key: string]: {
            [key: string]: number;
        };
    };
    setFilePageAngle: (fileVersionId: string, pageIndex: number, angle: number) => void;
    getFilePageAngle: (fileVersionId: string, pageIndex: number) => number | null;
    get previewToolViewModel(): IPreviewerViewModel | null;
}
export declare const supportedPdfExtensions: () => string[];
export declare const supportedImageExtensions: string[];
export declare const supportedHtmlExtensions: string[];
export declare const supportedTxtExtensions: string[];
