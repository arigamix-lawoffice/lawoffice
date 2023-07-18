import { IViewControlDataProvider, ViewControlDataProviderRequest, ViewControlDataProviderResponse } from 'tessa/ui/cards/controls/viewControl';
import { ICardModel } from 'tessa/ui/cards';
export declare class CreateFileTemplateDataProvider implements IViewControlDataProvider {
    private readonly _fileTemplatesRows;
    constructor(cardModel: ICardModel);
    getDataAsync(request: ViewControlDataProviderRequest): Promise<ViewControlDataProviderResponse>;
    private static _addFieldDescriptions;
    private static _mapFileTemplateToViewRow;
    private static _buildParametersCollectionFromRequest;
    private _populateDataRows;
}
