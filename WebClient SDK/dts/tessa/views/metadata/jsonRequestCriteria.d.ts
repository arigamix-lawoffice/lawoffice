import { DotNetType, TypedField } from 'tessa/platform';
import { JsonCriteriaValue } from 'tessa/views/metadata/jsonCriteriaValue';
import { RequestCriteria } from 'tessa/views/metadata/requestCriteria';
import { SchemeType } from 'tessa/scheme';
export declare class JsonRequestCriteria {
    constructor(origMeta?: RequestCriteria, schemeType?: SchemeType | null);
    CriteriaName: TypedField<DotNetType.String, string | null>;
    Values: JsonCriteriaValue[] | null;
}
