import { IExtensionSettingsMetadata, ExtensionSettingsMetadataSealed } from './extensionSettingsMetadata';
export interface IExtensionMetadata {
    hidden: boolean;
    order: number;
    settings: IExtensionSettingsMetadata | null;
    typeName: string;
    seal<T = ExtensionMetadataSealed>(): T;
}
export interface ExtensionMetadataSealed {
    readonly hidden: boolean;
    readonly order: number;
    readonly settings: ExtensionSettingsMetadataSealed | null;
    readonly typeName: string;
    seal<T = ExtensionMetadataSealed>(): T;
}
export declare class ExtensionMetadata implements IExtensionMetadata {
    constructor(typeName?: string, settings?: IExtensionSettingsMetadata | ExtensionSettingsMetadataSealed | null);
    hidden: boolean;
    order: number;
    settings: IExtensionSettingsMetadata | null;
    typeName: string;
    seal<T = ExtensionMetadataSealed>(): T;
}
