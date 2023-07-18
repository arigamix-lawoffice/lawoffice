import { CardTypeSection } from 'tessa/cards/virtualScheme';
import { IViewControlDataProvider, ViewControlDataProviderResponse } from 'tessa/ui/cards/controls/viewControl/viewControlDataProvider';
export declare class VirtualSchemeDataProvider implements IViewControlDataProvider {
    private _cardTypeSections;
    constructor(_cardTypeSections: ReadonlyArray<CardTypeSection>);
    getDataAsync(): Promise<ViewControlDataProviderResponse>;
    private populateDataRows;
    private addFormalColumns;
    private addFieldDescriptions;
}
