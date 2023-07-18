/// <reference types="react" />
import { IUIContext } from 'tessa/ui/uiContext';
export declare enum TabPanelButtonSide {
    Left = 0,
    Right = 1
}
export declare enum TabPanelButtonMobileRule {
    ContextMenu = 0,
    Hidden = 1
}
export declare enum TabPanelButtonSelectionMode {
    NormalMode = 0,
    SelectionMode = 1,
    All = 2
}
export declare class TabPanelButton {
    name: string;
    caption?: string | undefined;
    icon?: string | undefined;
    buttonAction?: ((btn: TabPanelButton, e?: import("react").MouseEvent<Element, MouseEvent> | undefined) => void) | undefined;
    child?: TabPanelButton[] | undefined;
    side: TabPanelButtonSide;
    selectionMode: TabPanelButtonSelectionMode;
    mobileRule: TabPanelButtonMobileRule;
    contextExecutor?: ((action: (context: IUIContext) => void) => void) | null | undefined;
    constructor(name: string, caption?: string | undefined, icon?: string | undefined, buttonAction?: ((btn: TabPanelButton, e?: import("react").MouseEvent<Element, MouseEvent> | undefined) => void) | undefined, child?: TabPanelButton[] | undefined, side?: TabPanelButtonSide, selectionMode?: TabPanelButtonSelectionMode, mobileRule?: TabPanelButtonMobileRule, contextExecutor?: ((action: (context: IUIContext) => void) => void) | null | undefined);
    private _asyncGuard;
    onClick: (e?: import("react").MouseEvent<Element, MouseEvent> | undefined) => void;
    private handleButtonAction;
    static create(args: {
        name: string;
        caption?: string;
        icon?: string;
        buttonAction?: (btn: TabPanelButton, e?: React.MouseEvent) => void;
        child?: TabPanelButton[];
        side?: TabPanelButtonSide;
        selectionMode?: TabPanelButtonSelectionMode;
        mobileRule?: TabPanelButtonMobileRule;
        contextExecutor?: ((action: (context: IUIContext) => void) => void) | null;
    }): TabPanelButton;
}
