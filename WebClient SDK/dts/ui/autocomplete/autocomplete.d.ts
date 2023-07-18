import * as React from 'react';
import { MediaStyle } from 'ui';
import { IAutoCompletePopupItem } from 'tessa/ui/cards/controls';
import { UIButton } from 'tessa/ui/uiButton';
declare class Autocomplete extends React.Component<AutocompleteProps, AutocompleteState> {
    private _input;
    debounceItemsRequest: any;
    inputCaretPos: number;
    input: any;
    dialogInput: any;
    dialogAutocomplete: HTMLDivElement | null;
    autocomplete: HTMLDivElement | null;
    dotsButton: any;
    get isMobileMode(): boolean;
    static defaultProps: {
        items: never[];
        isAutocompleteMode: boolean;
        isComboBoxMode: boolean;
        isTable: boolean;
        alwaysShowInDialog: boolean;
        isSelectMode: boolean;
    };
    constructor(props: AutocompleteProps);
    shouldComponentUpdate(nextProps: AutocompleteProps, nextState: AutocompleteState): boolean;
    static getDerivedStateFromProps(props: AutocompleteProps, state: AutocompleteState): AutocompleteState | null;
    componentDidMount(): void;
    componentDidUpdate(): void;
    componentWillUnmount(): void;
    getCurrentItems(items: {
        text: string;
    }[] | undefined | null): {
        text: string;
    }[];
    getDropdownItems(text?: string | null): void;
    addToCurrentItems(index?: number | null, keyCode?: number | null): boolean;
    deleteFromCurrentItems(index?: number | null): void;
    deleteAllCurrentItems(): void;
    openDropdown(type: string): void;
    closeDropdown(): void;
    openDialog(): void;
    closeDialog: () => void;
    getCaretPos(openDirection: string): number;
    getDropdownRootElement(): any;
    getContextRootElement(): HTMLLIElement | null | undefined;
    getNextFocusedDropdownItemIndex(keyCode: number, currentIndex: number, itemsLength: number): number;
    getCurrentInput(): any;
    getCurrentContainer(): HTMLDivElement | null;
    focus(opt?: FocusOptions): void;
    blur(): void;
    select(): void;
    checkUniqueItem(text: string, items: AutocompleteDropDownItemsCollection['items']): boolean;
    getSelectableItemsIndex(): number[];
    isNotReference(currentItems: any[] | undefined | null, index: number): boolean;
    setCurrentRowId(index: number, item: IAutoCompletePopupItem[] | null): void;
    isContextMenuOpen(isDeleteAllowed: boolean, isReferenceAllowed: boolean): boolean;
    /**
     * Обработчик нажатия клавиш.
     * @param {object} event Событие нажатия клавиши.
     */
    handleKeyDown: (event: React.KeyboardEvent) => void;
    handleClick: (event: React.SyntheticEvent) => void;
    /**
     * Обработчик нажатия кнопки combox.
     */
    handleComboBoxButtonClick: () => void;
    handleDotsButtonClick: () => void;
    handleClearButtonClick: () => void;
    /**
     * Обработчик ввода.
     * @param {string} text Введенный текст.
     */
    handleInputChange: (text: string | null | undefined) => void;
    /**
     * Обработчик получения фокуса.
     */
    handleInputFocus: () => void;
    /**
     * Обработчик потери фокуса.
     */
    handleInputBlur: () => void;
    /**
     * Обработчик выбора объекта из выпадающей подсказки.
     * @param {object} index Индекс выбранного объекта.
     */
    handleDropdownItemSelect: (index: number) => void;
    /**
     * Обработчик получения фокуса объекта из выпадающей подсказки.
     * @param {object} index Индекс выбранного объекта.
     */
    handleDropdownItemFocus: (index: number) => void;
    /**
     * Обработчик выбора объекта из текущей коллекции.
     * @param {object} index Индекс выбранного объекта.
     * @param {object} item Выбранный объект
     */
    handleCurrentItemSelect: (index: number, item: IAutoCompletePopupItem[]) => void;
    /**
     * Обработчик клика за границей выпадющих элементов.
     */
    handleOutsideDropdownClick: () => void;
    handleTabOutsideDropdownClick: () => void;
    handleOutsideContextClick: () => void;
    handleDivBlur(): void;
    handleStringZeroWidthTrim(event: React.ClipboardEvent<HTMLDivElement>): void;
    render(): JSX.Element;
    renderDialog(): JSX.Element | null;
    renderButtons(isDialog?: boolean): JSX.Element | null;
}
export interface AutocompleteDropDownItemsCollection {
    additionalLabelText: string | null;
    items: {
        text: string | null | undefined;
        fields: any[];
    }[];
}
export interface AutocompleteProps {
    items?: {
        text: string;
    }[] | null;
    dropdownItems: AutocompleteDropDownItemsCollection | null;
    disabled?: boolean;
    className?: string;
    dialogProps?: {
        other: any;
        className: string;
    };
    isAutocompleteMode?: boolean;
    isComboBoxMode?: boolean;
    isTable?: boolean;
    onAddItem: any;
    onDeleteItem: any;
    onDropdownItemsRequest: any;
    onTextChange?: any;
    onReferenceItem?: any;
    alwaysShowInDialog?: boolean;
    onDotsModeToggle?: any;
    hideSelectorButton?: boolean;
    isClearFieldVisible?: boolean;
    onItemSelect?: any;
    isSelectMode?: boolean;
    isLineBreak?: boolean;
    isManualInput?: boolean;
    comboIsLoading?: boolean;
    isLoading?: boolean;
    onKeyDown?: any;
    onFocus?: any;
    onBlur?: any;
    mediaStyle?: MediaStyle | null;
    style?: React.CSSProperties;
    title?: string;
    bindReactComponentRef?: (ref: React.RefObject<Autocomplete>) => void;
    buttons?: {
        defaultButtons?: UIButton[];
        dialogButtons?: UIButton[];
    };
    unbindReactComponentRef?: () => void;
}
export interface AutocompleteState {
    mode: string | null;
    text?: string | null;
    currentItems?: {
        text: string;
    }[] | null;
    isDropdownOpened?: string;
    isDialogOpened: boolean;
    isLoading?: boolean;
    focusedDropdownItemIndex: number;
    selectedCurrentItemIndex: number;
    selectedCurrentItemIndexForSelectMode: number;
    prevPropItems: {
        text: string;
    }[] | null;
    displayText: string | null | undefined;
}
export default Autocomplete;
