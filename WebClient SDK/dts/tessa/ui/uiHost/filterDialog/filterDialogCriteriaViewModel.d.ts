import { IFilterDialogEditorViewModel } from './common';
import { RequestParameter } from 'tessa/views/metadata/requestParameter';
import { CriteriaOperator } from 'tessa/views/metadata/criteriaOperators';
import { ISelectFromViewContext } from 'tessa/ui/views/selectFromViewContext';
import { FilterDialogViewModel } from './filterDialogViewModel';
export declare class FilterDialogCriteriaViewModel {
    constructor(criteriaOperators: CriteriaOperator[], parameter: RequestParameter, deleteCriteriaAction: (criteria: FilterDialogCriteriaViewModel) => void, selectionAction: (criteria: FilterDialogCriteriaViewModel, context: ISelectFromViewContext) => Promise<void>, dialog: FilterDialogViewModel);
    private _dialog;
    private _criteriaOperator;
    private _values;
    private _selectionAction;
    private _deleteCriteriaAction;
    readonly criteriaOperators: ReadonlyArray<CriteriaOperator>;
    readonly parameter: RequestParameter;
    get criteriaOperator(): CriteriaOperator;
    set criteriaOperator(value: CriteriaOperator);
    get readOnly(): boolean;
    get values(): ReadonlyArray<IFilterDialogEditorViewModel>;
    get hasAnyNotEmptyValue(): boolean;
    changeValue(index: number, text: string, value: string): void;
    private rebuildEditors;
    private getEditor;
}
