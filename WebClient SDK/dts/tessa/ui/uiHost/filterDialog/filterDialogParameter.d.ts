import * as React from 'react';
import { FilterDialogViewModel } from './filterDialogViewModel';
import { FilterDialogItemViewModel } from './filterDialogItemViewModel';
export interface FilterDialogParameterProps {
    viewModel: FilterDialogItemViewModel;
    dialog: FilterDialogViewModel;
    focusDialog: () => void;
}
export declare class FilterDialogParameter extends React.Component<FilterDialogParameterProps> {
    render(): JSX.Element;
    private renderCriterias;
    private handleAddNewCriteria;
    private handleClearAll;
}
