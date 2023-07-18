import { IParametersMappingContext, ViewControlViewModel } from '..';
import { ViewComponentBase } from 'tessa/ui/views';
import { SchemeDbType } from 'tessa/platform';
import { IGridRowTagViewModel } from 'components/cardElements/grid/interfaces';
export declare class ViewControlViewComponent extends ViewComponentBase<ViewControlViewComponent> {
    constructor();
    viewControl: ViewControlViewModel;
    selectRowOnContextMenu: boolean;
    initialize(): void;
    dispose(): void;
    protected initializeMasterLinks(): void;
    protected initializeSelection(): void;
    protected updatePageCountVisibility(): void;
    protected updateParameters(): void;
    protected getViewData(): Promise<{
        columns: ReadonlyMap<string, SchemeDbType>;
        rows: ReadonlyArray<ReadonlyMap<string, any>>;
        rowCount: number;
        tags: Map<guid, IGridRowTagViewModel[]>;
    } | null>;
    private getViewMapping;
    private getFilterFunc;
    private mapCardId;
    getParametersMappingContext(): IParametersMappingContext;
}
