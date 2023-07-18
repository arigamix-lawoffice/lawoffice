import { Card, CardRow } from 'tessa/cards';
import { GridColumnInfo } from 'tessa/ui/cards/controls';
export declare class CardTableViewRowData implements ReadonlyMap<string, any> {
    readonly card: Card;
    readonly cardRow: CardRow;
    readonly columnInfos: Map<string, GridColumnInfo>;
    readonly orderColumnName: string | null;
    readonly flagColumnName: string;
    readonly sectionName: string;
    constructor(card: Card, cardRow: CardRow, columnInfos: Map<string, GridColumnInfo>, orderColumnName: string | null, flagColumnName: string, sectionName: string);
    get rowId(): guid;
    get order(): number;
    get flag(): boolean;
    set flag(value: boolean);
    readonly [Symbol.toStringTag]: 'Map';
    forEach(callbackfn: (value: any, key: string, map: ReadonlyMap<string, any>) => void, thisArg?: any): void;
    get(key: string): any | undefined;
    has(key: string): boolean;
    get size(): number;
    [Symbol.iterator](): IterableIterator<[string, any]>;
    entries(): IterableIterator<[string, any]>;
    keys(): IterableIterator<string>;
    values(): IterableIterator<any>;
}
