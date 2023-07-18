import { StorageObject, IStorage } from 'tessa/platform/storage';
export declare class KrPermissionsFileSettings extends StorageObject {
    constructor(storage?: IStorage);
    static readonly fileIdKey: string;
    static readonly readAccessSettingKey: string;
    static readonly editAccessSettingKey: string;
    static readonly signAccessSettingKey: string;
    static readonly deleteAccessSettingKey: string;
    static readonly addAccessSettingKey: string;
    static readonly fileSizeKey: string;
    static readonly ownFileKey: string;
    get id(): guid;
    set id(value: guid);
    get readAcessSetting(): number | null | undefined;
    set readAcessSetting(value: number | null | undefined);
    get editAcessSetting(): number | null | undefined;
    set editAcessSetting(value: number | null | undefined);
    get signAcessSetting(): number | null | undefined;
    set signAcessSetting(value: number | null | undefined);
    get deleteAcessSetting(): number | null | undefined;
    set deleteAcessSetting(value: number | null | undefined);
    get addAcessSetting(): number | null | undefined;
    set addAcessSetting(value: number | null | undefined);
    get fileSize(): number | null | undefined;
    set fileSize(value: number | null | undefined);
    get ownFile(): boolean;
    set ownFile(value: boolean);
}
