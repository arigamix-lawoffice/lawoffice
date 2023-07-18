import { CardStorageObject } from 'tessa/cards';
import { IStorage, IStorageValueFactory } from 'tessa/platform/storage';
import { SortingColumn } from '../sortingColumn';
export declare class PlainSortingColumnFactory implements IStorageValueFactory<PlainSortingColumn> {
    getValue(storage: IStorage): PlainSortingColumn;
    getValueAndStorage(): {
        value: PlainSortingColumn;
        storage: IStorage;
    };
}
export declare class PlainSortingColumn extends CardStorageObject {
    static CreateFromSortingColumn(column: SortingColumn): PlainSortingColumn;
    constructor(storage?: IStorage);
    static readonly aliasKey: string;
    static readonly descendingKey: string;
    get alias(): string | null;
    set alias(value: string | null);
    get descending(): boolean;
    set descending(value: boolean);
}
