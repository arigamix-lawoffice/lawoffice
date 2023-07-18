import { WorkflowQueueItem } from './workflowQueueItem';
import { CardInfoStorageObject } from '../cardInfoStorageObject';
import { ICloneable } from 'tessa/platform';
import { IStorage, ArrayStorage } from 'tessa/platform/storage';
export declare class WorkflowQueue extends CardInfoStorageObject implements ICloneable<WorkflowQueue> {
    constructor(storage?: IStorage);
    static readonly itemsKey: string;
    get isEmpty(): boolean;
    get items(): ArrayStorage<WorkflowQueueItem>;
    set items(value: ArrayStorage<WorkflowQueueItem>);
    private static readonly _itemFactory;
    ensureCacheResolved(): void;
    clean(): void;
    tryGetItems(): ArrayStorage<WorkflowQueueItem> | null | undefined;
    clone(): WorkflowQueue;
    addSignal(args: {
        processTypeName: string;
        name?: string;
        number?: number;
        parameters?: IStorage;
        signalTypeId?: guid;
        signalTypeName?: string;
        queueEventTypeId?: guid;
        queueEventTypeName?: string;
        processId?: guid;
    }): WorkflowQueueItem;
}
