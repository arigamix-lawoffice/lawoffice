import { IViewParameters } from './viewParameters';
import { ViewParameterMetadataSealed, RequestParameter } from 'tessa/views/metadata';
export interface IParametersSetNameProvider {
    readonly parametersSetName: string;
}
export declare class ViewParametersSwitcher implements IViewParameters {
    constructor(metadata: ViewParameterMetadataSealed[], parametersByStates: Map<string, IViewParameters>, nameProvider: IParametersSetNameProvider);
    private _atom;
    private _nameProvider;
    private _parametersByStates;
    private _setName;
    private _currentParameter;
    private _setNameReactionDisposer;
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
    private ensureParametersSetsIsActual;
    private updateSetName;
}
