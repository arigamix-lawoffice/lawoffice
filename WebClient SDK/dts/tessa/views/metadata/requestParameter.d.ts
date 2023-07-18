import { RequestCriteria } from './requestCriteria';
import { ViewParameterMetadataSealed } from './viewParameterMetadata';
export declare class RequestParameter {
    constructor();
    private _metadata;
    criteriaValues: RequestCriteria[];
    get isHidden(): boolean;
    get metadata(): ViewParameterMetadataSealed | null;
    set metadata(value: ViewParameterMetadataSealed | null);
    name: string | null;
    readOnly: boolean;
    clone(): RequestParameter;
    cloneAsReadOnly(): RequestParameter;
}
