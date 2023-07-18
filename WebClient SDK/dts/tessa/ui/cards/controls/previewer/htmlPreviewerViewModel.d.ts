import { IPreviewerViewModel } from './previewerViewModel';
import { HandleFileContentLoadingFuncType } from 'tessa/ui/files';
import { IFileVersion } from 'tessa/files';
import { EventHandler } from 'tessa/platform';
export declare class HtmlPreviewerViewModel implements IPreviewerViewModel {
    static get type(): string;
    readonly type: string;
    readonly showNameHeader: boolean;
    readonly resizerMouseDown: EventHandler<() => void>;
    private readonly _getFileContentFunc;
    private _uriLinkDependencies;
    private _fileVersion;
    private _safeHtml;
    constructor(getFileContentFunc: HandleFileContentLoadingFuncType);
    get safeHtml(): string;
    get fileVersion(): IFileVersion | null;
    load: (version: IFileVersion) => Promise<void>;
    unload: () => void;
    private onFileLoadHandler;
    handleGenericLinkClickAsync: (href: string) => Promise<void>;
}
