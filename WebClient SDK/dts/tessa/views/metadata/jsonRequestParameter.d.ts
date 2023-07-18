import { DotNetType, TypedField } from 'tessa/platform';
import { JsonRequestCriteria } from 'tessa/views/metadata/jsonRequestCriteria';
import { RequestParameter } from 'tessa/views/metadata/requestParameter';
export declare class JsonRequestParameter {
    constructor(origMeta?: RequestParameter);
    CriteriaValues: JsonRequestCriteria[];
    Name: TypedField<DotNetType.String, string | null>;
}
