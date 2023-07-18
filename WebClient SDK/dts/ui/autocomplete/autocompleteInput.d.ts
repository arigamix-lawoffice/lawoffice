import * as React from 'react';
import { MediaStyle } from '../mediaStyle';
declare class AutocompleteInput extends React.Component<IAutocompleteInputProps, IAutocompleteInputState> {
    container: HTMLElement | null;
    input: HTMLInputElement | null;
    caretPos: number;
    liRefs: Map<number, HTMLLIElement | null>;
    static defaultProps: {
        value: string;
        items: never[];
        disabled: boolean;
        isMobileMode: boolean;
        selectedItemIndex: number;
        isAutoFocusNeeded: boolean;
        selectableIndexes: never[];
    };
    constructor(props: IAutocompleteInputProps);
    componentDidMount(): void;
    componentDidUpdate(prevProps: IAutocompleteInputProps): void;
    setInputWidth(element: any, text: any): void;
    focus(opt?: FocusOptions): void;
    blur(): void;
    select(): void;
    isFocused(): boolean;
    getCaretPos(text: any, openDirection?: string | null): number;
    getSelectedItemDOMNode(index: number): HTMLLIElement | null | undefined;
    handleClick: (event: any) => void;
    handleChange: (event: any) => void;
    handleFocus: (event: any) => void;
    handleBlur: (event: any) => void;
    handleItemSelect: (event: any, index: any, item: any, isSelectable: any) => void;
    renderItems(): JSX.Element[] | null;
    render(): JSX.Element;
}
export interface IAutocompleteInputProps {
    value?: string | null;
    items?: {
        text: string;
    }[] | null;
    disabled?: boolean;
    isMobileMode?: boolean;
    isLoading?: boolean;
    selectedItemIndex?: number;
    isAutoFocusNeeded?: boolean;
    onChange: any;
    onFocus: any;
    onBlur: any;
    onItemSelect?: any;
    selectableIndexes?: any[];
    onInputClick?: any;
    hasButtons?: boolean;
    isLineBreak?: boolean;
    mediaStyle?: MediaStyle | null;
    style?: React.CSSProperties;
    title?: string;
    displayText?: string | null | undefined;
}
export interface IAutocompleteInputState {
    isFocused: boolean;
}
export default AutocompleteInput;
