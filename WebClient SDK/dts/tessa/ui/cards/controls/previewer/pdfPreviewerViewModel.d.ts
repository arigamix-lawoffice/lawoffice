import { HandleFileContentLoadingFuncType, ScaleOption } from 'tessa/ui/files';
import { IFileVersion } from 'tessa/files';
import { IPreviewerViewModel } from './previewerViewModel';
import { IToolbarGroup } from 'ui/toolbar/interfaces';
import { LocalizationManager } from 'tessa/localization';
import { OcrRecognizedCollection } from 'tessa/platform/textRecognition/entities/ocrRecognizedCollection';
import { OcrRecognizedLayout } from 'tessa/platform/textRecognition/entities/ocrRecognizedLayout';
import { UIButton } from 'tessa/ui';
/**
 * Представляет собой модель представления средства предпросмотра файлов через конвертацию в PDF.
 */
export declare class PdfPreviewerViewModel implements IPreviewerViewModel {
    static get type(): string;
    private static readonly scaleStep;
    readonly type: string;
    readonly showNameHeader: boolean;
    readonly buttons: UIButton[];
    readonly groups: IToolbarGroup[];
    private _fileContent;
    private _fileVersion;
    readonly localizationManager: LocalizationManager;
    private _inputPageValue;
    private _pageIndex;
    private _totalPages;
    private _scaleType;
    private _scaleValue;
    private _documentRotate;
    private readonly _getFileContentFunc;
    private readonly _setFilePageAngleFunc;
    private readonly _getFilePageAngleFunc;
    readonly handleError: (error: string) => void;
    /** Непрерывный режим отображения. */
    private _infiniteMode;
    /** Признак, показывающий состояние режима распознавания. */
    private _recognitionMode;
    /** Способ отображения распознанных элементов. */
    private _recognizedLayout;
    /** Коллекция распознанных элементов. */
    private _recognizedCollection;
    constructor(getFileContentFunc: HandleFileContentLoadingFuncType, setFilePageAngleFunc: (fileVersionId: guid, pageIndex: number, angle: number) => void, getFilePageAngleFunc: (fileVersionId: guid, pageIndex: number) => number | null, handleError: (error: string) => void, preferPdfInfiniteMode: boolean);
    private initPropsToDefault;
    private initDefaultButtons;
    load: (fileVersion: IFileVersion) => Promise<void>;
    unload: () => void;
    changeScale: (up: boolean) => void;
    changeRotate: (value: number) => void;
    goToFirstPage: () => void;
    goToLastPage: () => void;
    goToPrevPage: () => void;
    goToNextPage: () => void;
    rotateCurrentPageLeft: () => void;
    rotateCurrentPageRight: () => void;
    upScaleDocument: () => void;
    downScaleDocument: () => void;
    setEnteredPageAsCurrent: () => void;
    get fileContent(): File | null;
    get fileVersion(): IFileVersion | null;
    validateAndSetInputPage(str: string): void;
    set inputPageValue(value: number);
    get inputPageValue(): number;
    get totalPages(): number;
    set totalPages(value: number);
    get currentPageRotateAngle(): number;
    get documentRotateAngle(): number;
    set pageIndex(value: number);
    get pageIndex(): number;
    set scaleType(type: ScaleOption);
    get scaleType(): ScaleOption;
    set scaleValue(value: number);
    get scaleValue(): number;
    /** Непрерывный режим отображения. */
    get infiniteMode(): boolean;
    set infiniteMode(value: boolean);
    /** Режим распознавания. */
    get recognitionMode(): boolean;
    set recognitionMode(value: boolean);
    /** Способ отображения распознанных элементов. */
    get recognizedLayout(): OcrRecognizedLayout;
    set recognizedLayout(value: OcrRecognizedLayout);
    /** Коллекция распознанных элементов. */
    get recognizedCollection(): OcrRecognizedCollection | null;
}
