/// <reference types="react" />
import { IGridColumnInfo, IGridTableProps } from '../../interfaces';
export declare function useTableEventHandlers(props: IGridTableProps, columnInfo: IGridColumnInfo): {
    onBlur: React.FocusEventHandler;
    onFocus: React.FocusEventHandler;
    onKeyDown: React.KeyboardEventHandler;
};
