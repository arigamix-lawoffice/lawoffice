import * as React from 'react';
import { IWorkplaceViewModel } from '../workplaceViewModel';
export interface WorkplaceTreeProps {
    workplace: IWorkplaceViewModel;
}
export declare class WorkplaceTree extends React.Component<WorkplaceTreeProps> {
    render(): JSX.Element;
}
