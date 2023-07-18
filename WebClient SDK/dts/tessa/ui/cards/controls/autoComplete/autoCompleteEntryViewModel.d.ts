import { AutoCompleteValueEventArgs } from './autoCompleteCommon';
import { AutoCompleteEntryDataSource, ControlButtonsContainer } from 'tessa/ui/cards/controls';
import { IAutoCompleteItem } from './autoCompleteItem';
import { IAutoCompletePopupItem } from './autoCompletePopupItem';
import { ICardModel, ControlKeyDownEventArgs } from '../../interfaces';
import { CardTypeEntryControl } from 'tessa/cards/types';
import { CardMetadataColumnSealed } from 'tessa/cards/metadata';
import { ITessaView } from 'tessa/views';
import { EventHandler } from 'tessa/platform/eventHandler';
import { ValidationResultBuilder } from 'tessa/platform/validation';
import { IStorage } from 'tessa/platform/storage';
import { Command } from 'tessa/platform';
import { ControlViewModelBase } from '../controlViewModelBase';
/**
 * Модель представления для контрола "Ссылка".
 */
export declare class AutoCompleteEntryViewModel extends ControlViewModelBase {
    constructor(control: CardTypeEntryControl, model: ICardModel);
    private readonly _buttonsContainer;
    private _cardModel;
    private _referenceSection;
    private _referenceColumn;
    private _refSection;
    private _view;
    private _viewComboBox;
    private _viewMapping;
    private _popupItems;
    private _selectedItem;
    private _hasSelectionAction;
    private _isLoading;
    private _comboIsLoading;
    private _comboIsLoadingTimer;
    private _hideSelectorButton;
    private _isClearFieldVisible;
    private _comboBoxMode;
    private _itemsSource;
    private _manualInput;
    isAllowOpenRefs: boolean;
    get hideSelectorButton(): boolean;
    set hideSelectorButton(value: boolean);
    get isClearFieldVisible(): boolean;
    set isClearFieldVisible(value: boolean);
    get comboBoxMode(): boolean;
    get buttonsContainer(): ControlButtonsContainer;
    get itemsSource(): AutoCompleteEntryDataSource;
    get item(): IAutoCompleteItem | null;
    get popupItems(): ReadonlyArray<IAutoCompletePopupItem> | null;
    get popupDisplayIndexes(): ReadonlyArray<number> | null;
    alwaysShowInDialog: boolean;
    get error(): string | null;
    get hasEmptyValue(): boolean;
    get selectedItem(): IAutoCompleteItem | null;
    set selectedItem(value: IAutoCompleteItem | null);
    get hasSelectionAction(): boolean;
    set hasSelectionAction(value: boolean);
    get manualInput(): boolean;
    get comboIsLoading(): boolean;
    get isLoading(): boolean;
    set isLoading(value: boolean);
    get view(): ITessaView | null;
    set view(value: ITessaView | null);
    get viewComboBox(): ITessaView | null;
    set viewComboBox(value: ITessaView | null);
    get refSection(): ReadonlyArray<string> | null;
    get referenceColumn(): CardMetadataColumnSealed;
    protected initializeCore(): void;
    setItem(item: IAutoCompletePopupItem | null): void;
    setItemFromViews: () => Promise<void>;
    deleteItem(): void;
    findItems(filter: string | null, withSearchDelay?: boolean): Promise<ReadonlyArray<IAutoCompletePopupItem>>;
    getRefInfo(): IStorage;
    openRefAction: () => Promise<void>;
    readonly changeFieldCommand: Command;
    readonly openCardCommand: Command;
    readonly valueSet: EventHandler<(args: AutoCompleteValueEventArgs<AutoCompleteEntryViewModel>) => void>;
    readonly valueDeleted: EventHandler<(args: AutoCompleteValueEventArgs<AutoCompleteEntryViewModel>) => void>;
    readonly keyDown: EventHandler<(args: ControlKeyDownEventArgs) => void>;
    onUnloading(validationResult: ValidationResultBuilder): void;
    initializeButtons(): void;
}
