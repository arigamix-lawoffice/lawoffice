import { CardResponseBase } from './cardResponseBase';
import { CardFileVersion } from '../cardFileVersion';
import { IStorage, ArrayStorage } from 'tessa/platform/storage';
import { ICloneable } from 'tessa/platform';
export declare class CardGetFileVersionsResponse extends CardResponseBase implements ICloneable<CardGetFileVersionsResponse> {
    constructor(storage?: IStorage);
    static readonly fileVersionsKey: string;
    static readonly disallowCachingSystemKey: string;
    get fileVersions(): ArrayStorage<CardFileVersion>;
    set fileVersions(value: ArrayStorage<CardFileVersion>);
    get disallowCaching(): boolean;
    set disallowCaching(value: boolean);
    private static readonly _versionFactory;
    tryGetVersions(): ArrayStorage<CardFileVersion> | null | undefined;
    clone(): CardGetFileVersionsResponse;
}
