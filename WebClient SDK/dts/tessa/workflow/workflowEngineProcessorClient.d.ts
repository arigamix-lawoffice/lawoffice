import { IWorkflowEngineProcessResult, WorkflowEngineProcessResult } from './workflowEngineProcessResult';
import { IWorkflowEngineProcessRequest } from './workflowEngineProcessStorageRequest';
import { IWorkflowEngineSignal } from './signals';
import { IStorage } from 'tessa/platform/storage';
import { CardRequest } from 'tessa/cards/service';
export interface IWorkflowEngineProcessorClient {
    processSignalAsync: (request: IWorkflowEngineProcessRequest, requestSignature?: string | null, additionalInfo?: IStorage | null, requestModifier?: ((request: CardRequest) => void) | null) => Promise<IWorkflowEngineProcessResult>;
    sendAsyncSignalAsync: (signal: IWorkflowEngineSignal, nodeInstanceId: guid, processInstanceId: guid, lockProcess: boolean, processDigest?: string | null, requestModifier?: ((request: CardRequest) => void) | null) => Promise<void>;
    sendSignalAsync: (processInstanceId: guid, signal: IWorkflowEngineSignal, nodeId?: guid | null, nodeInstanceId?: guid | null, requestModifier?: ((request: CardRequest) => void) | null) => Promise<IWorkflowEngineProcessResult>;
}
export declare class WorkflowEngineProcessorClient implements IWorkflowEngineProcessorClient {
    processSignalAsync(request: IWorkflowEngineProcessRequest, requestSignature?: string | null, additionalInfo?: IStorage | null, requestModifier?: ((request: CardRequest) => void) | null): Promise<IWorkflowEngineProcessResult>;
    sendAsyncSignalAsync(signal: IWorkflowEngineSignal, nodeInstanceId: guid, processInstanceId: guid, lockProcess: boolean, processDigest?: string | null, requestModifier?: ((request: CardRequest) => void) | null): Promise<void>;
    sendSignalAsync(processInstanceId: guid, signal: IWorkflowEngineSignal, nodeId?: guid | null, nodeInstanceId?: guid | null, requestModifier?: ((request: CardRequest) => void) | null): Promise<WorkflowEngineProcessResult>;
}
