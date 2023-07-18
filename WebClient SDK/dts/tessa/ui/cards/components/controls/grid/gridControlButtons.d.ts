import * as React from 'react';
import { GridViewModel } from 'tessa/ui/cards/controls/grid';
export interface IGridControlButtonsProps {
    viewModel: GridViewModel;
}
interface IGridControlButtonsState {
    expandSearch: boolean;
}
export declare class GridControlButtons extends React.Component<IGridControlButtonsProps, IGridControlButtonsState> {
    constructor(props: IGridControlButtonsProps);
    render(): JSX.Element;
    private handleFocusChange;
}
export {};
