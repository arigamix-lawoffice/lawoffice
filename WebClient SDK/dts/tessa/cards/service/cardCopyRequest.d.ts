import { CardInfoStorageObject } from 'tessa/cards/cardInfoStorageObject';
import { IStorage, ArrayStorage } from 'tessa/platform/storage';
import { Card } from 'tessa/cards/card';
import { CardFile } from 'tessa/cards/cardFile';
export declare class CardCopyRequest extends CardInfoStorageObject {
    constructor(storage?: IStorage);
    static readonly exportedCardKey: string;
    static readonly sourceFileListKey: string;
    static readonly sourceCardIdKey: string;
    static readonly sourceCardTypeIdKey: string;
    static readonly sourceCardTypeNameKey: string;
    get exportedCard(): Card;
    set exportedCard(value: Card);
    get sourceFileList(): ArrayStorage<CardFile>;
    set sourceFileList(value: ArrayStorage<CardFile>);
    get sourceCardId(): guid;
    set sourceCardId(value: guid);
    get sourceCardTypeId(): guid;
    set sourceCardTypeId(value: guid);
    get sourceCardTypeName(): string;
    set sourceCardTypeName(value: string);
    private static readonly _fileFactory;
    tryGetExportedCard(): Card | null | undefined;
    tryGetSourceFileList(): ArrayStorage<CardFile> | null | undefined;
}
