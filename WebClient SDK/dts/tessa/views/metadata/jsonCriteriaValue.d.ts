import { DotNetType, TypedField } from 'tessa/platform';
import { CriteriaValue } from 'tessa/views/metadata/criteriaValue';
import { SchemeType } from 'tessa/scheme';
export declare class JsonCriteriaValue {
    constructor(origMeta?: CriteriaValue, schemeType?: SchemeType | null);
    Text: TypedField<DotNetType.String, string | null> | null;
    Value: TypedField | null;
    private static getDataForTypedField;
}
