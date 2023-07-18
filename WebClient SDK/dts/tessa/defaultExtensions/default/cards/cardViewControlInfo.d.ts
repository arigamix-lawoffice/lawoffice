import { ICloneable } from 'tessa/platform';
import { IStorage, StorageObject } from 'tessa/platform/storage';
export declare class CardViewControlInfo extends StorageObject implements ICloneable<CardViewControlInfo> {
    private static readonly ViewControlInfoKey;
    static readonly IDKey: string;
    static readonly DisplayTextKey: string;
    static readonly ControlNameKey: string;
    static readonly ViewAliasKey: string;
    static readonly ColPrefixKey: string;
    constructor(storage?: IStorage);
    get id(): guid;
    set id(value: guid);
    get displayText(): string;
    set displayText(value: string);
    get controlName(): string;
    set controlName(value: string);
    get viewAlias(): string;
    set viewAlias(value: string);
    get colPrefix(): string;
    set colPrefix(value: string);
    clone(): CardViewControlInfo;
    setInfo(requestInfo: IStorage): void;
    static tryGet(requestInfo: IStorage): CardViewControlInfo | null;
}
