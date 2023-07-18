import { ControlViewModelBase } from './controlViewModelBase';
import { CardTypeTabControl, CardTypeControl } from 'tessa/cards/types';
import { ICardModel, IFormWithBlocksViewModel, IControlState, IBlockViewModel } from '../interfaces';
import { ValidationResultBuilder } from 'tessa/platform/validation';
/**
 * Модель представления для элемента управления вкладками.
 */
export declare class TabControlViewModel extends ControlViewModelBase {
    constructor(control: CardTypeTabControl, parentControl: CardTypeControl | null, model: ICardModel);
    private _tabs;
    private _selectedTab;
    cardModel: ICardModel;
    get tabs(): ReadonlyArray<IFormWithBlocksViewModel>;
    get selectedTab(): IFormWithBlocksViewModel | null;
    set selectedTab(value: IFormWithBlocksViewModel | null);
    get isEmpty(): boolean;
    get visibleTabs(): ReadonlyArray<IFormWithBlocksViewModel>;
    addTab(tab: IFormWithBlocksViewModel): void;
    removeTab(tab: IFormWithBlocksViewModel): boolean;
    getState(): IControlState;
    setState(state: IControlState): boolean;
    getNestedVisibleBlocks(): Iterable<IBlockViewModel>;
    onUnloading(validationResult: ValidationResultBuilder): void;
}
/**
 * Состояние контрола с табами.
 */
export declare class TabControlState implements IControlState {
    constructor(control: TabControlViewModel);
    readonly selectedTabIndex: number;
    apply(control: TabControlViewModel): boolean;
}
