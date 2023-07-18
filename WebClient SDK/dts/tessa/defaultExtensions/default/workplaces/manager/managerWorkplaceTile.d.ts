import * as React from 'react';
import { ManagerWorkplaceSettings } from './managerWorkplaceSettings';
import { IWorkplaceViewComponent } from 'tessa/ui/views';
export declare class ManagerWorkplaceTileViewModel {
    readonly viewComponent: IWorkplaceViewComponent;
    readonly row: ReadonlyMap<string, any>;
    constructor(viewComponent: IWorkplaceViewComponent, row: ReadonlyMap<string, any>);
    private _activeImage;
    private _hoverImage;
    private _inactiveImage;
    private _hover;
    caption: string;
    count: number;
    get activeImage(): string;
    set activeImage(value: string);
    get hoverImage(): string;
    set hoverImage(value: string);
    get inactiveImage(): string;
    set inactiveImage(value: string);
    get hover(): boolean;
    set hover(value: boolean);
    get isSelected(): boolean;
    static create(viewComponent: IWorkplaceViewComponent, row: ReadonlyMap<string, any>, settings: ManagerWorkplaceSettings): Promise<ManagerWorkplaceTileViewModel>;
    selectTile(): void;
}
interface ManagerWorkplaceTileProps {
    viewModel: ManagerWorkplaceTileViewModel;
}
export declare class ManagerWorkplaceTile extends React.Component<ManagerWorkplaceTileProps> {
    render(): JSX.Element;
    private handleMouseEnter;
    private handleMouseLeave;
    private handleClick;
}
export {};
