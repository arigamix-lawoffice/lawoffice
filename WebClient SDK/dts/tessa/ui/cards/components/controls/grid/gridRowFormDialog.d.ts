import * as React from 'react';
import { IGridFormContainer } from 'tessa/ui/cards/controls/grid';
export interface GridRowFormDialogProps {
    viewModel: IGridFormContainer;
}
export declare class GridRowFormDialog extends React.Component<GridRowFormDialogProps> {
    private _dialogRef;
    render(): JSX.Element | null;
    private handleCloseForm;
    private handleCloseFormWithSave;
    private handleCloseByContext;
    private handleCloseFormWithNoValidation;
}
