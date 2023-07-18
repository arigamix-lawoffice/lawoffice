import * as React from 'react';
import { IWorkplaceViewModel } from '../workplaceViewModel';
export interface WorkplaceContentProps {
    workplace: IWorkplaceViewModel;
}
export declare class WorkplaceContent extends React.Component<WorkplaceContentProps> {
    render(): JSX.Element;
}
