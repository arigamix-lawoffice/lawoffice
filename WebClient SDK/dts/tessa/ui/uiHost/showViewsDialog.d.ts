import { SelectAction } from '../views';
import { SelectedValue } from 'tessa/views';
import { RequestParameter } from 'tessa/views/metadata/requestParameter';
export declare function showViewsDialog(refSection: ReadonlyArray<string> | null, onValueSelected: (value: SelectedValue) => Promise<void>, parameters?: RequestParameter[] | null, selectAction?: SelectAction | null): Promise<void>;
export declare function showSelectionDialog(selectedValues: SelectedValue[]): Promise<SelectedValue | null>;
