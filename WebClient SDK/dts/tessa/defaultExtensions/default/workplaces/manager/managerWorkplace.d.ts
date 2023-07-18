import * as React from 'react';
import { ManagerWorkplaceSettings } from './managerWorkplaceSettings';
import { ManagerWorkplaceTileViewModel } from './managerWorkplaceTile';
import { BaseContentItem, ContentPlaceArea } from 'tessa/ui/views/content';
import { IWorkplaceViewComponent } from 'tessa/ui/views';
export declare class ManagerWorkplaceViewModel extends BaseContentItem {
    constructor(settings: ManagerWorkplaceSettings, viewComponent: IWorkplaceViewComponent, area?: ContentPlaceArea, order?: number);
    private _settings;
    private _dataReaction;
    private _loadingGuard;
    readonly models: ManagerWorkplaceTileViewModel[];
    get isLoading(): boolean;
    initialize(): void;
    dispose(): void;
    refresh(): Promise<void>;
}
export interface ManagerWorkplaceProps {
    viewModel: ManagerWorkplaceViewModel;
}
export declare class ManagerWorkplace extends React.Component<ManagerWorkplaceProps> {
    render(): JSX.Element;
}
