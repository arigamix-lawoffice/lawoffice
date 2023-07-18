import React from 'react';
import { MenuAction } from 'tessa/ui/menuAction';
import { ClassNameList } from 'tessa/ui/classNameList';
export interface ITableBlockViewModel {
    readonly id: string;
    readonly parentBlockId: string | null;
    isToggled: boolean;
    count: number;
    text: string;
    visibility: boolean;
    toggle: () => void;
    onClick: (e: React.MouseEvent) => void;
    onDoubleClick: (e: React.MouseEvent) => void;
    onMouseDown: (e: React.MouseEvent) => void;
    getContextMenu(): ReadonlyArray<MenuAction>;
    style?: React.CSSProperties;
    className: ClassNameList;
}
export interface ITableBlockViewModelCreateOptions {
    id: string;
    caption: string;
    parentBlockId?: string | null;
    getContextMenu?: ((column: ITableBlockViewModel) => ReadonlyArray<MenuAction>) | null;
}
export declare class TableBlockViewModel implements ITableBlockViewModel {
    constructor(args: ITableBlockViewModelCreateOptions);
    protected _isToggled: boolean;
    protected _count: number;
    protected _text: string;
    protected _getContextMenu: ((block: ITableBlockViewModel) => ReadonlyArray<MenuAction>) | null;
    protected _visibility: boolean;
    protected _style?: React.CSSProperties;
    readonly id: string;
    readonly caption: string;
    readonly parentBlockId: string | null;
    get isToggled(): boolean;
    set isToggled(value: boolean);
    get count(): number;
    set count(value: number);
    get text(): string;
    set text(value: string);
    get visibility(): boolean;
    set visibility(value: boolean);
    onClick: (_e: React.MouseEvent) => void;
    onDoubleClick: (_e: React.MouseEvent) => void;
    onMouseDown: (_e: React.MouseEvent) => void;
    get style(): React.CSSProperties | undefined;
    set style(value: React.CSSProperties | undefined);
    readonly className: ClassNameList;
    toggle(): void;
    getContextMenu(): ReadonlyArray<MenuAction>;
}
