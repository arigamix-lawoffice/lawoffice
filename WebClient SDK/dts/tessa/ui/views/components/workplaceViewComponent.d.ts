import * as React from 'react';
import { IWorkplaceViewComponent } from 'tessa/ui/views';
export interface WorkplaceViewComponentProps {
    viewModel: IWorkplaceViewComponent;
}
export declare class WorkplaceViewComponent extends React.Component<WorkplaceViewComponentProps> {
    render(): JSX.Element;
}
export interface WorkplaceViewComponentInternalProps {
    viewModel: IWorkplaceViewComponent;
    windowElement: HTMLElement;
}
