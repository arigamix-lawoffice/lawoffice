import { DotNetType, TypedField } from 'tessa/platform';
import { IStorage } from 'tessa/platform/storage';
import { IExtensionMetadata } from 'tessa/views/workplaces/extensionMetadata';
export interface IJsonExtensionMetadata {
    TypeName: TypedField<DotNetType.String, string>;
    Order: TypedField<DotNetType.Int32, number>;
    Data: IStorage | null | undefined;
}
export declare class JsonExtensionMetadata implements IJsonExtensionMetadata {
    constructor(origMeta?: IExtensionMetadata);
    TypeName: TypedField<DotNetType.String, string>;
    Order: TypedField<DotNetType.Int32, number>;
    Data: IStorage | null | undefined;
}
