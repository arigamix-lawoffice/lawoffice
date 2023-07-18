import { CardRow } from './cardRow';
import { ArrayStorage } from 'tessa/platform/storage';
import { EventHandler } from 'tessa/platform/eventHandler';
export declare class CardRowsListener {
    constructor();
    private _started;
    private _storage;
    rowInserted: EventHandler<(storage: ArrayStorage<CardRow>, row: CardRow) => void>;
    rowDeleted: EventHandler<(storage: ArrayStorage<CardRow>, row: CardRow) => void>;
    start(rowsStorage: ArrayStorage<CardRow>): void;
    stop(): void;
    private onCollectionChanged;
    private onRowInserted;
    private onRowDeleted;
}
