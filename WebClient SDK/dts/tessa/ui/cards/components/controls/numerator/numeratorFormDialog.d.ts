import * as React from 'react';
import { NumeratorDialogViewModel } from 'tessa/ui/cards/controls';
export interface NumeratorFormDialogProps {
    viewModel: NumeratorDialogViewModel;
    onClose: (result: boolean) => void;
}
export declare class NumeratorFormDialog extends React.Component<NumeratorFormDialogProps> {
    render(): JSX.Element;
    private handleValidate;
    private handleChangeFullNumber;
    private handleChangeNumber;
    private handleChangeSequence;
    private handleCloseForm;
    private handleCloseFormWithSave;
}
