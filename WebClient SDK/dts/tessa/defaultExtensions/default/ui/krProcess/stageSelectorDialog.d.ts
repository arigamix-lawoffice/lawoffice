import * as React from 'react';
import { StageGroup } from './stageGroup';
import { StageType } from './stageType';
import { StageSelectorViewModel } from './stageSelectorViewModel';
export declare const stageSelectorColumnName = "name";
export interface StageSelectorDialogProps {
    viewModel: StageSelectorViewModel;
    onClose: (args: {
        cancel: boolean;
        group: StageGroup | null;
        type: StageType | null;
    }) => void;
}
export declare class StageSelectorDialog extends React.Component<StageSelectorDialogProps> {
    constructor(props: StageSelectorDialogProps);
    private _layouts;
    private _groupColumns;
    private _typeColumns;
    private get groupRows();
    private get typeRows();
    render(): JSX.Element;
    private handleCloseForm;
    private handleCloseFormWithResult;
}
