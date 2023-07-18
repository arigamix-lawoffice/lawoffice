import { CardSection, CardTypeSection, CardTypeSectionColumn } from 'tessa/cards';
import { IBlockViewModel } from '../interfaces';
import { PresenterBase } from './presenterBase';
import type { VirtualSchemePresenter } from './virtualSchemePresenter';
export declare class PhysicalColumnPresenter extends PresenterBase {
    private _virtualSchemePresenter;
    private _propertiesSection;
    private _propertiesBlock;
    private _physicalColumn;
    private _section;
    private _complexColumn;
    constructor(_virtualSchemePresenter: VirtualSchemePresenter, _propertiesSection: CardSection, _propertiesBlock: IBlockViewModel, _physicalColumn: CardTypeSectionColumn, _section: CardTypeSection, _complexColumn?: CardTypeSectionColumn | null);
    private _disposes;
    attach(): void;
    detach(): void;
    private editorPropertyChanged;
    private isColumnNameValid;
    private getOffSet;
}
