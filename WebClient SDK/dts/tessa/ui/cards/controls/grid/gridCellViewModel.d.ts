/// <reference types="react" />
import { GridColumnInfo } from './gridColumnInfo';
import { Card, CardRow } from 'tessa/cards';
import { GridCellFormatContext } from './gridCellFormatContext';
import { ClassNameList } from 'tessa/ui/classNameList';
export declare class GridCellViewModel {
    constructor(row: CardRow, card: Card, columnInfo: GridColumnInfo);
    private _row;
    private _card;
    private _columnInfo;
    private _style;
    private _gridCellFormatFunc;
    private _cacheFunction;
    get value(): any;
    get toolTip(): string;
    get columnInfo(): GridColumnInfo;
    get style(): React.CSSProperties;
    set style(value: React.CSSProperties);
    readonly className: ClassNameList;
    get gridCellFormatFunc(): ((context: GridCellFormatContext) => unknown) | null;
    set gridCellFormatFunc(value: ((context: GridCellFormatContext) => unknown) | null);
    private getStyle;
    private valueWithCache;
    private getFormattedValue;
}
