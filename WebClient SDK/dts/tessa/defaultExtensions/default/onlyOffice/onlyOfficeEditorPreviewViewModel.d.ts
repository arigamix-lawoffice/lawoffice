import { IFileVersion } from 'tessa/files';
import { OnlyOfficeApi } from './onlyOfficeApi';
import { IPreviewerViewModel } from 'tessa/ui/cards/controls/previewer';
import { EventHandler } from 'tessa/platform';
/**
 * Представляет собой модель редактора, используемого для предпросмотра файлов.
 */
export declare class OnlyOfficeEditorPreviewViewModel implements IPreviewerViewModel {
    static get type(): string;
    readonly showNameHeader: boolean;
    readonly type: string;
    readonly resizerMouseDown: EventHandler<() => void>;
    readonly resizerMouseUp: EventHandler<() => void>;
    private _fileVersion;
    private readonly _api;
    private readonly _errorHandler;
    private _closeEditorCallback;
    private _editor;
    constructor(api: OnlyOfficeApi, errorHandler: (e: Error) => void);
    load: (version: IFileVersion) => void;
    setUpEditorSafety: (placeholder: string) => Promise<void>;
    unload: () => void;
    private openEditorFunc;
    get fileVersion(): IFileVersion | null;
}
