import * as React from 'react';
import { MenuAction } from 'tessa/ui';
import { DropdownProps } from 'ui/dropdown/dropdown';
export interface MenuActionComponentProps extends Omit<DropdownProps, 'onOutsideClick'> {
    itemClassName?: string;
    actions: MenuAction[] | ReadonlyArray<MenuAction>;
    onClose: () => void;
    onMenuAction?: (action: (e: React.MouseEvent) => void) => void;
    setStyledComponents?: (props: any) => string;
}
export interface MenuActionComponentState {
    prevPropisOpened: boolean;
    actionStack: MenuAction[];
}
export declare class MenuActionComponent extends React.Component<MenuActionComponentProps, MenuActionComponentState> {
    constructor(props: MenuActionComponentProps);
    static getDerivedStateFromProps(props: MenuActionComponentProps, state: MenuActionComponentState): MenuActionComponentState | null;
    render(): JSX.Element | null;
    private renderMainView;
    private renderActionView;
    private renderAction;
    private handleBackAction;
    private handleClose;
    private createMenuAction;
}
export declare function openMenuActionComponent(args: {
    actions: MenuAction[] | ReadonlyArray<MenuAction>;
    className?: string;
    itemClassName?: string;
    openPosition?: 'left' | 'right';
    openDirection?: 'left' | 'right';
    up?: boolean;
    autoDirection?: boolean;
    leftOffset?: number;
    topOffset?: number;
    rootElement?: any;
    contextMenu?: boolean;
    onMenuAction?: (action: (e: React.MouseEvent) => void) => void;
    setStyledComponents?: (props: any) => string;
}): Promise<void>;
