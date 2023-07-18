import { IStorage } from 'tessa/platform/storage';
export interface IExtensionSettingsMetadata {
    data: IStorage | null;
    seal<T = ExtensionSettingsMetadataSealed>(): T;
}
export interface ExtensionSettingsMetadataSealed {
    readonly data: IStorage | null;
    seal<T = ExtensionSettingsMetadataSealed>(): T;
}
export declare class ExtensionSettingsMetadata implements IExtensionSettingsMetadata {
    constructor();
    data: IStorage | null;
    seal<T = ExtensionSettingsMetadataSealed>(): T;
}
