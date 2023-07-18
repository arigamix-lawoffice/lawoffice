import { IWorkflowEngineSignal } from './signals';
import { IStorage, StorageObject } from 'tessa/platform/storage';
export interface IWorkflowEngineProcessRequest {
    processInstanceId: guid | null;
    processTemplateId: guid | null;
    processTemplateVersionId: guid | null;
    nodeIds: Array<guid>;
    nodeInstanceIds: Array<guid>;
    processFlag: WorkflowEngineProcessFlags;
    signalProcessingMode: WorkflowSignalProcessingMode;
}
export declare enum WorkflowSignalProcessingMode {
    Default = 0,
    Async = 1,
    AfterUploadingFiles = 2
}
export declare enum WorkflowEngineProcessFlags {
    None = 0,
    GenerateNextLinks = 1,
    LockProcess = 2,
    IsAsync = 4,
    IsDebug = 8,
    CreateNew = 16,
    SendToSubscribers = 32,
    NotInDb = 64,
    DefaultRuntime = 3,
    DefaultNew = 83,
    DefaultAsync = 5,
    DefaultDebug = 11
}
export declare class WorkflowEngineProcessStorageRequest extends StorageObject implements IWorkflowEngineProcessRequest {
    constructor(storage?: IStorage);
    get processInstanceId(): guid | null;
    set processInstanceId(value: guid | null);
    get processTemplateId(): guid | null;
    set processTemplateId(value: guid | null);
    get processTemplateVersionId(): guid | null;
    set processTemplateVersionId(value: guid | null);
    private _nodeIds;
    get nodeIds(): Array<guid>;
    set nodeIds(value: Array<guid>);
    private _nodeInstanceIds;
    get nodeInstanceIds(): Array<guid>;
    set nodeInstanceIds(value: Array<guid>);
    get signal(): IWorkflowEngineSignal;
    set signal(value: IWorkflowEngineSignal);
    get processFlag(): WorkflowEngineProcessFlags;
    set processFlag(value: WorkflowEngineProcessFlags);
    get signalProcessingMode(): WorkflowSignalProcessingMode;
    set signalProcessingMode(value: WorkflowSignalProcessingMode);
}
