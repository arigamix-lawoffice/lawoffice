import { IStorage, StorageObject } from 'tessa/platform/storage';
interface CreateWithParams {
    processId: guid;
    cardId?: guid;
    processInfo?: IStorage;
    parentStageRowId?: guid;
    parentProcessTypeName?: string;
    parentProcessId?: guid;
    processHolderId?: guid;
    nestedOrder?: number;
    serializedProcess?: string;
    serializedProcessSignature?: string;
}
export declare class KrProcessInstance extends StorageObject {
    constructor(storage: IStorage);
    static createWithParams({ processId, cardId, processInfo, parentStageRowId, parentProcessTypeName, parentProcessId, processHolderId, nestedOrder, serializedProcess, serializedProcessSignature }: CreateWithParams): KrProcessInstance;
    processId(): guid;
    cardId(): guid | null;
    processInfo(): IStorage;
    processHolderId(): guid | null | undefined;
    parentProcessTypeName(): string | null | undefined;
    parentProcessId(): guid | null | undefined;
    parentStageRowId(): guid | null | undefined;
    nestedOrder(): number | null | undefined;
    serializedProcess(): string | null | undefined;
    serializedProcessSignature(): string | null | undefined;
}
export {};
