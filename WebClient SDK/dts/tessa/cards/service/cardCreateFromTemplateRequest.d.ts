import { CardInfoStorageObject } from 'tessa/cards/cardInfoStorageObject';
import { ArrayStorage, IStorage } from 'tessa/platform/storage';
import { Card } from 'tessa/cards/card';
import { CardFile } from '../cardFile';
export declare class CardCreateFromTemplateRequest extends CardInfoStorageObject {
    constructor(storage?: IStorage);
    static readonly sourceCardKey: string;
    static readonly templateCardIDKey: string;
    static readonly filesKey: string;
    get sourceCard(): Card;
    set sourceCard(value: Card);
    get files(): ArrayStorage<CardFile>;
    set files(value: ArrayStorage<CardFile>);
    get templateCardId(): guid;
    set templateCardId(value: guid);
    tryGetTemplateCard(): Card | null | undefined;
    tryGetFiles(): ArrayStorage<CardFile> | null | undefined;
    private static readonly _fileFactory;
}
