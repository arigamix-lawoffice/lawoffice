/// <reference types="react" />
import { IUIContext } from './uiContext';
export declare class MenuAction {
    constructor(name: string, caption?: string | null, icon?: string | null, action?: ((e: React.MouseEvent) => void) | null, children?: MenuAction[] | null, isCollapsed?: boolean, isSelected?: boolean, contextExecutor?: ((action: (context: IUIContext) => void) => void) | null, tooltip?: string, className?: string);
    private _contextExecutor;
    private _children;
    readonly name: string;
    caption: string | null;
    icon: string | null;
    action: ((e?: React.MouseEvent) => void) | null;
    get children(): MenuAction[];
    lazyChildren: (() => MenuAction[]) | null;
    isCollapsed: boolean;
    isSelected: boolean;
    tooltip?: string;
    className?: string;
    get hasContextExecutor(): boolean;
    executeAction(e?: React.MouseEvent): void;
    setContextExecutor(executeInContext: ((action: (context: IUIContext) => void) => void) | null, overwrite?: boolean, deep?: boolean): void;
    clone(): MenuAction;
    static create(args: {
        name: string;
        caption?: string | null;
        icon?: string | null;
        action?: ((e: React.MouseEvent) => void) | null;
        children?: MenuAction[] | null;
        lazyChildren?: (() => MenuAction[]) | null;
        isCollapsed?: boolean;
        isSelected?: boolean;
        contextExecutor?: ((action: (context: IUIContext) => void) => void) | null;
        tooltip?: string;
        className?: string;
    }): MenuAction;
}
export declare class SeparatorMenuAction extends MenuAction {
    constructor(isCollapsed?: boolean, name?: string);
}
