import React from 'react';
import { IRowCounter } from '../../rowCounter';
export interface RowCounterProps {
    viewModel: IRowCounter;
}
export declare class RowCounter extends React.Component<RowCounterProps> {
    render(): JSX.Element | null;
}
