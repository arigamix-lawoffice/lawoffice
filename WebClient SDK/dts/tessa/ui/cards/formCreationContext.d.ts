import { IControlViewModel } from './interfaces';
import { ScopeContextInstance } from 'tessa/platform/scopes';
import { FileListViewModel, FilePreviewViewModel } from './controls';
export interface IFormCreationContext {
    readonly controls: Map<string, IControlViewModel>;
    readonly fileControls: FileListViewModel[];
    readonly filePreviewControls: FilePreviewViewModel[];
    hasFileControl: boolean;
    registerControls(controls: IControlViewModel[]): any;
    registerFileControl(control: FileListViewModel): any;
    registerFilePreviewControl(control: FilePreviewViewModel): any;
}
export declare class FormCreationContext implements IFormCreationContext {
    constructor();
    private static _scopeContext;
    readonly controls: Map<string, IControlViewModel>;
    readonly fileControls: FileListViewModel[];
    readonly filePreviewControls: FilePreviewViewModel[];
    hasFileControl: boolean;
    static get current(): IFormCreationContext | null;
    static get hasCurrent(): boolean;
    registerControls(controls: IControlViewModel[]): void;
    registerFileControl(control: FileListViewModel): void;
    registerFilePreviewControl(control: FilePreviewViewModel): void;
    static create(context: IFormCreationContext): ScopeContextInstance<IFormCreationContext>;
}
