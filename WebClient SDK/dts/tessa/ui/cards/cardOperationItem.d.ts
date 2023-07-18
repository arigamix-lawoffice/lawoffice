export declare class CardOperationItem<T = guid> {
    constructor(cardId: T, displayValue: string);
    readonly cardId: T;
    readonly displayValue: string;
    typeId: guid | null;
}
