import { IWorkflowEngineSignal } from './signals';
import { ValidationResult } from 'tessa/platform/validation';
import { IStorage } from 'tessa/platform/storage';
export interface IWorkflowEngineProcessResult {
    readonly validationResult: ValidationResult;
    readonly stopPending: boolean;
    readonly endSignals: Array<IWorkflowEngineSignal>;
    readonly responseInfo: IStorage | null;
}
export declare class WorkflowEngineProcessResult implements IWorkflowEngineProcessResult {
    constructor(validationResult: ValidationResult, responseInfo?: IStorage | null);
    readonly validationResult: ValidationResult;
    readonly responseInfo: IStorage | null;
    readonly stopPending: boolean;
    readonly endSignals: Array<IWorkflowEngineSignal>;
    static empty(): IWorkflowEngineProcessResult;
}
