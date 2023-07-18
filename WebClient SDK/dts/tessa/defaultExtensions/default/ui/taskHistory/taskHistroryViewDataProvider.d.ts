import { IViewControlDataProvider, ViewControlDataProviderResponse, ViewControlDataProviderRequest } from 'tessa/ui/cards/controls';
export declare class TaskHistroryViewDataProvider implements IViewControlDataProvider {
    constructor(dataProvider: IViewControlDataProvider);
    private _defaultDataProvider;
    private _cachedResponse;
    getDataAsync(request: ViewControlDataProviderRequest): Promise<ViewControlDataProviderResponse>;
}
