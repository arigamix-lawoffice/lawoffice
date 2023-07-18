import { AttachmentStoreMode, AttachmentType } from './enums';
import { ValidationStorageObject } from 'tessa/platform/validation';
import { IStorage, IStorageValueFactory } from 'tessa/platform/storage';
export declare class ItemModel extends ValidationStorageObject {
    constructor(storage?: IStorage);
    static readonly messageIdKey: string;
    static readonly idKey: string;
    static readonly versionIdKey: string;
    static readonly uriKey: string;
    static readonly dataBase64Key: string;
    static readonly fileNameKey: string;
    static readonly captionKey: string;
    static readonly typeKey: string;
    static readonly storeModeKey: string;
    static readonly showInToolbarKey: string;
    static readonly fileSizeKey: string;
    static readonly originalFileIdKey: string;
    get messageId(): guid | null;
    set messageId(value: guid | null);
    get id(): guid | null;
    set id(value: guid | null);
    get originalFileId(): guid | null;
    set originalFileId(value: guid | null);
    get versionId(): guid | null;
    get uri(): string | null;
    set uri(value: string | null);
    get dataBase64(): string | null;
    set dataBase64(value: string | null);
    get fileName(): string | null;
    set fileName(value: string | null);
    get caption(): string | null;
    set caption(value: string | null);
    get type(): AttachmentType;
    set type(value: AttachmentType);
    get storeMode(): AttachmentStoreMode;
    set storeMode(value: AttachmentStoreMode);
    get showInToolbar(): boolean;
    set showInToolbar(value: boolean);
    get fileSize(): number;
    set fileSize(value: number | undefined);
}
export declare class ItemModelFactory implements IStorageValueFactory<ItemModel> {
    getValue(storage: IStorage): ItemModel;
    getValueAndStorage(): {
        value: ItemModel;
        storage: IStorage;
    };
}
