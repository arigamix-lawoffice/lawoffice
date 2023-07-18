import { JsonRequestParameter } from 'tessa/views/metadata/jsonRequestParameter';
import { DotNetType, TypedField } from 'tessa/platform';
import { IJsonExtensionMetadata } from 'tessa/views/workplaces/jsonExtensionMetadata';
import { ISearchQueryMetadata } from 'tessa/views/searchQueries/searchQueryMetadata';
import { IStorage } from 'tessa/platform/storage';
export interface IJsonSearchQueryMetadata {
    Alias: TypedField<DotNetType.String, string>;
    CreatedByUserID: TypedField<DotNetType.Guid, guid>;
    Extensions: IJsonExtensionMetadata[] | null;
    FormatVersion: TypedField<DotNetType.Int32, number>;
    ID: TypedField<DotNetType.Guid, guid>;
    IsPublic: TypedField<DotNetType.Boolean, boolean>;
    Items: Array<IJsonSearchQueryMetadata> | null;
    ModificationDateTime: TypedField<DotNetType.DateTime, string>;
    Parameters: Array<JsonRequestParameter> | null;
    ParametersByState: Map<string, Array<JsonRequestParameter>> | null;
    TemplateCompositionID: TypedField<DotNetType.Guid, guid>;
    ViewAlias: TypedField<DotNetType.String, string>;
}
export declare class JsonSearchQueryMetadata implements IJsonSearchQueryMetadata {
    constructor(origMeta?: ISearchQueryMetadata);
    Alias: TypedField<DotNetType.String, string>;
    CreatedByUserID: TypedField<DotNetType.Guid, guid>;
    Extensions: IJsonExtensionMetadata[] | null;
    FormatVersion: TypedField<DotNetType.Int32, number>;
    ID: TypedField<DotNetType.Guid, guid>;
    IsPublic: TypedField<DotNetType.Boolean, boolean>;
    Items: Array<IJsonSearchQueryMetadata> | null;
    ModificationDateTime: TypedField<DotNetType.DateTime, string>;
    Parameters: Array<JsonRequestParameter> | null;
    ParametersByState: Map<string, Array<JsonRequestParameter>> | null;
    TemplateCompositionID: TypedField<DotNetType.Guid, guid>;
    ViewAlias: TypedField<DotNetType.String, string>;
    static deserialize(storage: IStorage): JsonSearchQueryMetadata;
    convertBack(): ISearchQueryMetadata;
    private setTypedFields;
    private setObjectFields;
}
