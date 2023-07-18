import { IFileVersion } from 'tessa/files';
import { IPreviewerViewModel } from './previewerViewModel';
import { HandleFileContentLoadingFuncType } from 'tessa/ui/files';
import { TextEncodingResolver } from './previewerTypes';
export declare class TxtPreviewerViewModel implements IPreviewerViewModel {
    constructor(getFileContentFunc: HandleFileContentLoadingFuncType, textEncodingResolver?: TextEncodingResolver);
    static get type(): string;
    /** Дефолтная реализация метода определения кодировки файла. */
    static defaultTextEncodingResolver: TextEncodingResolver;
    private readonly _getFileContentFunc;
    private readonly _textEncodingResolver;
    private _fileVersion;
    private _text;
    readonly type: string;
    readonly showNameHeader = true;
    get text(): string;
    get fileVersion(): IFileVersion | null;
    /** Флаг, показывающий необходимость определения кодировки контента файла при загрузке. */
    resolveFileEncodingOnLoading: boolean;
    load: (version: IFileVersion) => Promise<void>;
    unload: () => void;
    private onFileLoadHandler;
    private resolveEncoding;
}
