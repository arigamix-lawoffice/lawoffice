import { IFilterDialogEditorViewModel } from './common';
import { ViewParameterMetadataSealed } from 'tessa/views/metadata';
import { CriteriaValue } from 'tessa/views/metadata/criteriaValue';
import { IAutoCompleteItem, IAutoCompletePopupItem } from 'tessa/ui/cards/controls/autoComplete';
import { SelectAction } from 'tessa/ui/views/selectFromViewContext';
import type { Autocomplete } from 'ui';
import { ControlButtonsContainer } from 'tessa/ui/cards/controls/controlButtonsContainer';
export declare class FilterDialogAutoCompleteViewModel implements IFilterDialogEditorViewModel {
    constructor(meta: ViewParameterMetadataSealed);
    private readonly _buttonsContainer;
    private _reactComponentRef;
    private _dataSource;
    private _popupItems;
    private _selectedItem;
    private _comboIsLoading;
    private _comboIsLoadingTimer;
    readonly meta: ViewParameterMetadataSealed;
    get item(): IAutoCompleteItem | null;
    set item(value: IAutoCompleteItem | null);
    get popupItems(): ReadonlyArray<IAutoCompletePopupItem>;
    get selectedItem(): IAutoCompleteItem | null;
    set selectedItem(value: IAutoCompleteItem | null);
    get autoCompleteMode(): boolean;
    get comboBoxMode(): boolean;
    get buttonsContainer(): ControlButtonsContainer;
    readonly hideSelectorButton: boolean;
    get popupDisplayIndexes(): number[] | null;
    get comboIsLoading(): boolean;
    selectAction: SelectAction | null;
    deleteAction: () => void | null;
    get maxResultsCount(): number;
    tryGetReactComponentRef(): Autocomplete | null;
    changeItem(text: string, value: string): void;
    getValue(): CriteriaValue;
    setItem(item: IAutoCompletePopupItem | null): void;
    setItemFromViews(): Promise<void>;
    deleteItem(): void;
    findItems(filter: string | null): Promise<ReadonlyArray<IAutoCompletePopupItem>>;
    getDefaultCaption(popupItem: IAutoCompletePopupItem): string;
    bindReactComponentRef: (ref: React.RefObject<Autocomplete>) => void;
    unbindReactComponentRef: () => void;
    focus(opt?: FocusOptions): void;
    blur(): void;
    initializeButtons(): void;
}
