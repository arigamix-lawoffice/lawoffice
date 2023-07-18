import { CardTaskHistoryState } from './cardTaskHistoryState';
import { CardInfoStorageObject } from './cardInfoStorageObject';
import { IStorage, IStorageValueFactory } from 'tessa/platform/storage';
export declare class CardTaskHistoryGroup extends CardInfoStorageObject {
    constructor(storage: IStorage);
    static readonly rowIdKey: string;
    static readonly parentRowIdKey: string;
    static readonly typeIdKey: string;
    static readonly captionKey: string;
    static readonly systemStateKey: string;
    get rowId(): guid;
    set rowId(value: guid);
    get parentRowId(): guid | null;
    set parentRowId(value: guid | null);
    get typeId(): guid;
    set typeId(value: guid);
    get caption(): string;
    set caption(value: string);
    get state(): CardTaskHistoryState;
    set state(value: CardTaskHistoryState);
    hasChanges(): boolean;
    removeChanges(): boolean;
    isEmpty(): boolean;
}
export declare class CardTaskHistoryGroupFactory implements IStorageValueFactory<CardTaskHistoryGroup> {
    getValue(storage: IStorage): CardTaskHistoryGroup;
    getValueAndStorage(): {
        value: CardTaskHistoryGroup;
        storage: IStorage;
    };
}
