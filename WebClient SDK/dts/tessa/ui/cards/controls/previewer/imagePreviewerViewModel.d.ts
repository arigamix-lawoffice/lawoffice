import { IPreviewerViewModel } from './previewerViewModel';
import { IFileVersion } from 'tessa/files';
import { HandleFileContentLoadingFuncType } from 'tessa/ui/files';
export declare class ImagePreviewerViewModel implements IPreviewerViewModel {
    static get type(): string;
    readonly type: string;
    readonly showNameHeader: boolean;
    private _showResetZoomBtn;
    private _maxScaleImagePreview;
    private readonly _getFileContentFunc;
    private _fileVersion;
    private _data;
    constructor(getFileContentFunc: HandleFileContentLoadingFuncType);
    get showResetZoomBtn(): boolean;
    set showResetZoomBtn(value: boolean);
    get maxScaleImagePreview(): number;
    set maxScaleImagePreview(value: number);
    get data(): string;
    get fileVersion(): IFileVersion | null;
    load: (version: IFileVersion) => Promise<void>;
    unload: () => void;
    private onFileLoadHandler;
}
