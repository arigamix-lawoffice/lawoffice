import React from 'react';
import { ControlProps } from './../controlProps';
import { ViewControlViewModel } from 'tessa/ui/cards/controls';
export declare class ViewControl extends React.Component<ControlProps<ViewControlViewModel>> {
    private _contentRef;
    componentDidMount(): void;
    render(): JSX.Element | null;
    private removeParamHandler;
    private openFilterHandler;
    private renderParameterBalloons;
    private static renderItems;
}
