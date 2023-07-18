import type { CardSection, CardTypeSection } from 'tessa/cards';
import type { IBlockViewModel } from '../interfaces';
import { PresenterBase } from './presenterBase';
import type { VirtualSchemePresenter } from './virtualSchemePresenter';
export declare class SectionPresenter extends PresenterBase {
    private _virtualSchemePresenter;
    private _propertiesSection;
    private _propertiesBlock;
    private _section;
    private _cardTypeSections;
    private _availableSections;
    constructor(_virtualSchemePresenter: VirtualSchemePresenter, _propertiesSection: CardSection, _propertiesBlock: IBlockViewModel, _section: CardTypeSection, _cardTypeSections: readonly CardTypeSection[], _availableSections: readonly CardTypeSection[]);
    private _disposes;
    attach(): void;
    detach(): void;
    private editorPropertyChanged;
    private isSectionNameValid;
    private changeReferencedSectionsNames;
}
