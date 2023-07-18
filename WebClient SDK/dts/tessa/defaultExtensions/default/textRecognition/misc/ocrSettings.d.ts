import { Card, CardRow } from 'tessa/cards';
import { OcrPatternTypes } from '../misc/ocrTypes';
export declare class OcrSettings {
    private static _instance;
    readonly isEnabled: boolean;
    readonly mapping: Mapping;
    readonly patterns: Map<OcrPatternTypes, RegExp[]>;
    static get instance(): OcrSettings;
    private constructor();
}
declare class Item {
    protected _row: CardRow;
    protected _prefix: string;
    get id(): guid;
    get name(): guid;
    constructor(row: CardRow, prefix: string);
    get(key: string): any;
}
declare class Collection<T extends Item> {
    private readonly _elements;
    constructor(ctor: new (card: Card, row: CardRow, sectionPrefix: string) => T, card: Card, sectionPrefix: string, parentRowId?: guid);
    getById(id: guid): T | undefined;
    getByName(name: string): T | undefined;
    [Symbol.iterator](): IterableIterator<T>;
}
declare class Field extends Item {
    constructor(_card: Card, row: CardRow);
}
declare class Section extends Item {
    readonly fields: Collection<Field>;
    constructor(card: Card, row: CardRow);
}
declare class Type extends Item {
    readonly sections: Collection<Section>;
    constructor(card: Card, row: CardRow);
}
declare class Mapping {
    readonly types: Collection<Type>;
    constructor(card: Card);
}
export {};
