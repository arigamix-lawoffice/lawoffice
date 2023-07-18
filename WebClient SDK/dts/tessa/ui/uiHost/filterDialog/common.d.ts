import { CriteriaValue } from 'tessa/views/metadata/criteriaValue';
import { ViewParameterMetadataSealed } from 'tessa/views/metadata';
export interface IFilterDialogEditorViewModel {
    readonly meta: ViewParameterMetadataSealed;
    getValue(): CriteriaValue;
    focus(): any;
    blur(): any;
}
