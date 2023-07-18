import { WorkflowQueueSignal } from './workflowQueueSignal';
import { CardInfoStorageObject } from '../cardInfoStorageObject';
import { ICloneable } from 'tessa/platform';
import { IStorage, IStorageValueFactory } from 'tessa/platform/storage';
export declare class WorkflowQueueItem extends CardInfoStorageObject implements ICloneable<WorkflowQueueItem> {
    constructor(storage?: IStorage);
    static readonly idKey: string;
    static readonly handledKey: string;
    static readonly signalKey: string;
    static readonly queueEventTypeIdKey: string;
    static readonly queueEventTypeNameKey: string;
    get id(): guid;
    set id(value: guid);
    get handled(): boolean;
    set handled(value: boolean);
    get queueEventTypeId(): guid;
    set queueEventTypeId(value: guid);
    get queueEventTypeName(): string;
    set queueEventTypeName(value: string);
    get signal(): WorkflowQueueSignal;
    set signal(value: WorkflowQueueSignal);
    ensureCacheResolved(): void;
    isEmpty(): boolean;
    clean(): void;
    tryGetSignal(): WorkflowQueueSignal | null | undefined;
    clone(): WorkflowQueueItem;
}
export declare class WorkflowQueueItemFactory implements IStorageValueFactory<WorkflowQueueItem> {
    getValue(storage: IStorage): WorkflowQueueItem;
    getValueAndStorage(): {
        value: WorkflowQueueItem;
        storage: IStorage;
    };
}
