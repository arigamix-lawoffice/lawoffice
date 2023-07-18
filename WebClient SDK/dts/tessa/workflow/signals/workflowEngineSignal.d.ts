import { IStorage, StorageObject } from 'tessa/platform/storage';
export interface IWorkflowEngineSignal {
    type: string;
    ignoreSignalProcessingMode: boolean;
    hash: IStorage;
    getStorage: () => IStorage;
}
export declare class WorkflowEngineSignal extends StorageObject implements IWorkflowEngineSignal {
    constructor(storage?: IStorage);
    get type(): string;
    set type(value: string);
    get hash(): IStorage;
    set hash(value: IStorage);
    get ignoreSignalProcessingMode(): boolean;
    set ignoreSignalProcessingMode(value: boolean);
    static createWithParams(type: string, hash?: IStorage | null): WorkflowEngineSignal;
    static createDefaultSignal(hash?: IStorage | null): WorkflowEngineSignal;
    static createExitSignal(): WorkflowEngineSignal;
}
