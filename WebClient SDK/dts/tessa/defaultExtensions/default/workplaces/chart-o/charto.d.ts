import React from 'react';
import { ChartoSettings, DiagramType, DiagramDirection, LegendPosition, Palettes } from './chartoSettings';
import { IWorkplaceViewComponent } from 'tessa/ui/views';
import { BaseContentItem, ContentPlaceArea } from 'tessa/ui/views/content';
export declare class ChartoViewViewModel extends BaseContentItem {
    constructor(settings: ChartoSettings, viewComponent: IWorkplaceViewComponent, area?: ContentPlaceArea, order?: number);
    private _settings;
    private _dataReaction;
    private _loadingGuard;
    readonly models: ChartoItemViewModel[];
    get isLoading(): boolean;
    get DiagramType(): DiagramType;
    get DiagramDirection(): DiagramDirection;
    get YColumn(): string;
    get LegendPosition(): LegendPosition;
    get Caption(): string;
    get CaptionColumn(): string;
    get Palette(): Palettes;
    get LegendItemMinWidth(): number | undefined;
    get ColumnCount(): number;
    get LegendNotWrap(): boolean;
    get DoesntShowZeroValues(): boolean;
    get SelectedColor(): string;
    initialize(): void;
    dispose(): void;
    refresh(): Promise<void>;
}
export interface ChartoProps {
    viewModel: ChartoViewViewModel;
}
export declare class Charto extends React.Component<ChartoProps> {
    timeout: number | undefined;
    handleResize: () => void;
    componentDidMount(): void;
    componentWillUnmount(): void;
    render(): JSX.Element;
}
export declare class ChartoItemViewModel {
    readonly viewComponent: IWorkplaceViewComponent;
    readonly row: ReadonlyMap<string, any>;
    constructor(viewComponent: IWorkplaceViewComponent, row: ReadonlyMap<string, any>, settings: ChartoSettings);
    private _captionColumn;
    private _XColumn;
    private _YColumn;
    get CaptionColumn(): string;
    get XColumn(): string;
    set XColumn(value: string);
    get YColumn(): string;
    set YColumn(value: string);
}
export interface ChartoProps {
    viewModel: ChartoViewViewModel;
}
