import { CardRow } from 'tessa/cards/cardRow';
export interface IAutoCompleteItem {
    readonly displayText: string;
    readonly columnValues: ReadonlyArray<any>;
    readonly reference: any;
}
export declare class AutoCompleteItem implements IAutoCompleteItem {
    constructor(displayText: string, columnValues: ReadonlyArray<any>, reference: any);
    readonly displayText: string;
    readonly columnValues: ReadonlyArray<any>;
    readonly reference: any;
}
export declare class RowAutoCompleteItem extends AutoCompleteItem {
    constructor(displayText: string, columnValues: ReadonlyArray<any>, reference: any, row: CardRow, order: number);
    readonly row: CardRow;
    readonly order: number;
}
