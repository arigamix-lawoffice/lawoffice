import { CardTypeSection, CardTypeSectionColumn } from 'tessa/cards';
import { IViewControlDataProvider, ViewControlDataProviderResponse } from '../controls';
export declare class ReferenceColumnsViewDataProvider implements IViewControlDataProvider {
    constructor(arg?: {
        complexColumn: CardTypeSectionColumn;
        referencedSection: CardTypeSection;
    });
    private _complexColumn;
    private _referencedSection;
    getDataAsync(): Promise<ViewControlDataProviderResponse>;
    private populateDataRows;
    private addFieldDescriptions;
}
