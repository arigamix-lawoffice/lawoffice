import * as React from 'react';
import { IFilterableGrid } from 'tessa/ui/cards/controls/grid';
import { Visibility } from 'tessa/platform';
export interface IGridFilterInput {
    filterableGrid: IFilterableGrid;
    visibility: Visibility;
    expand?: boolean;
    onFocusChange?: (isFocused: boolean) => void;
}
interface IGridFilterInputState {
    showClearButton: boolean;
}
export declare class GridInputFilter extends React.Component<IGridFilterInput, IGridFilterInputState> {
    constructor(props: IGridFilterInput);
    searchRef: HTMLInputElement | null;
    timeout: number;
    render(): JSX.Element;
    private onFocus;
    private onBlur;
    private onClear;
    private onInputFilterChange;
    filterItems: () => void;
}
export {};
