import { ViewParameterMetadataSealed, RequestParameter } from 'tessa/views/metadata';
export interface IViewParameters {
    readonly metadata: ViewParameterMetadataSealed[];
    readonly parameters: ReadonlyArray<RequestParameter>;
    dispose(): any;
    addParameters(...params: RequestParameter[]): any;
    removeParameters(...params: RequestParameter[]): any;
    clear(): any;
    clone(): IViewParameters;
    cloneAsReadOnly(): IViewParameters;
    suspendChangesNotification(): () => void;
    getParametersSets(): Map<string, RequestParameter[]>;
}
export declare class ViewParameters implements IViewParameters {
    constructor(metadata: ViewParameterMetadataSealed[], parameters?: RequestParameter[]);
    private _atom;
    private _parameters;
    private _suspendChanges;
    readonly metadata: ViewParameterMetadataSealed[];
    get parameters(): ReadonlyArray<RequestParameter>;
    dispose(): void;
    addParameters(...params: RequestParameter[]): void;
    removeParameters(...params: RequestParameter[]): void;
    clear(): void;
    static empty(): IViewParameters;
    clone(): IViewParameters;
    cloneAsReadOnly(): IViewParameters;
    suspendChangesNotification(): () => void;
    getParametersSets(): Map<string, RequestParameter[]>;
}
