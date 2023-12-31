import { Card, CardFile, CardTask } from 'tessa/cards';
import { CardInfoStorageObject } from 'tessa/cards/cardInfoStorageObject';
import { IStorage } from 'tessa/platform/storage';
import { ICloneable } from 'tessa/platform';
import { ArgumentNullError } from 'tessa/platform/errors';
export declare class CardRequest extends CardInfoStorageObject implements ICloneable<CardRequest> {
    constructor(storage?: IStorage);
    static readonly requestTypeKey: string;
    static readonly cardIdKey: string;
    static readonly cardTypeIdKey: string;
    static readonly cardTypeNameKey: string;
    static readonly fileIdKey: string;
    static readonly fileTypeIdKey: string;
    static readonly fileTypeNameKey: string;
    static readonly fileVersionIdKey: string;
    static readonly taskIdKey: string;
    static readonly taskTypeIdKey: string;
    static readonly taskTypeNameKey: string;
    get requestType(): guid;
    set requestType(value: guid);
    get cardId(): guid | null;
    set cardId(value: guid | null);
    get cardTypeId(): guid | null;
    set cardTypeId(value: guid | null);
    get cardTypeName(): string | null;
    set cardTypeName(value: string | null);
    get fileId(): guid | null;
    set fileId(value: guid | null);
    get fileTypeId(): guid | null;
    set fileTypeId(value: guid | null);
    get fileTypeName(): string | null;
    set fileTypeName(value: string | null);
    get fileVersionId(): guid | null;
    set fileVersionId(value: guid | null);
    get taskId(): guid | null;
    set taskId(value: guid | null);
    get taskTypeId(): guid | null;
    set taskTypeId(value: guid | null);
    get taskTypeName(): string | null;
    set taskTypeName(value: string | null);
    clean(): void;
    clone(): CardRequest;
    setCardInfo(card: Card): ArgumentNullError | undefined;
    setFileInfo(file: CardFile): ArgumentNullError | undefined;
    setTaskInfo(task: CardTask): ArgumentNullError | undefined;
}
