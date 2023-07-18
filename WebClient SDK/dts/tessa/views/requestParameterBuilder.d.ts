import { CriteriaOperator } from './metadata/criteriaOperators';
import { RequestCriteria } from './metadata/requestCriteria';
import { RequestParameter } from './metadata/requestParameter';
import { ViewParameterMetadataSealed } from './metadata/viewParameterMetadata';
export declare class RequestParameterBuilder {
    constructor();
    constructor(descriptor: RequestParameter);
    private _descriptor;
    addCriteria(criteriaOperator: CriteriaOperator): RequestParameterBuilder;
    addCriteria(criteriaOperator: RequestCriteria): RequestParameterBuilder;
    addCriteria(criteriaOperator: CriteriaOperator, text: string, value: any): RequestParameterBuilder;
    addCriteria(criteriaOperator: CriteriaOperator, text1: string, value1: any, text2: string, value2: any): RequestParameterBuilder;
    private addCriteriaZero;
    private addCriteriaOne;
    private addCriteriaTwo;
    asRequestParameter(): RequestParameter;
    readOnly(readOnly?: boolean): RequestParameterBuilder;
    withMetadata(metadata: ViewParameterMetadataSealed): RequestParameterBuilder;
}
