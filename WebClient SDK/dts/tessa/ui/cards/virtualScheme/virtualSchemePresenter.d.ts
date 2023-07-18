import { CardTypeSection, CardTypeSectionColumn } from 'tessa/cards';
import { IFormViewModelBase } from '../interfaces';
import { PresenterBase } from './presenterBase';
export declare class VirtualSchemePresenter extends PresenterBase {
    constructor(cardTypeSctions: Array<CardTypeSection>, generalSections: Array<CardTypeSection>);
    private _cardTypeSctions;
    private _availableSections;
    private _form;
    private _virtualSchemeView;
    private _referenceTablesView;
    private _propertiesBlock;
    private _referenceColumnsBlock;
    private _propertiesSection;
    private initialize;
    private rowSelected;
    createForm(): Promise<IFormViewModelBase | null>;
    refreshVirtualSchemeView(section?: CardTypeSection | null, column?: CardTypeSectionColumn | null): Promise<void>;
    private generateRowContextMenu;
    private addTable;
    private deleteTable;
    private deletePhisicalColumn;
    private deleteComplexColumn;
    private deleteReferenceColumn;
    private addReferenceColumn;
    private addComplexColumn;
    private addPhysicalColumn;
    private getNewSectionName;
    private getNewColumnName;
}
