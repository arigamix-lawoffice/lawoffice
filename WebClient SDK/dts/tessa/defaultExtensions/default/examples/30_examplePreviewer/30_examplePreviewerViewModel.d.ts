import { IFileVersion } from 'tessa/files';
import { IPreviewerViewModel } from 'tessa/ui/cards/controls/previewer';
export declare class ExamplePreviewerViewModel implements IPreviewerViewModel {
    static get type(): string;
    readonly type: string;
    readonly showNameHeader: boolean;
    private _fileVersion;
    private _isLoading;
    private _text;
    load: (version: IFileVersion) => Promise<void>;
    unload: () => void;
    get fileVersion(): IFileVersion | null;
    get text(): string;
    get isLoading(): boolean;
}
