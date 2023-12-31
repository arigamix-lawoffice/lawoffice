import * as React from 'react';
import { IUIContext } from './uiContext';
import { Visibility } from 'tessa/platform';
import { RaisedButton } from 'ui';
import { MenuAction } from './menuAction';
declare type FunctionOrValue<T> = T | (() => T | null) | null;
export declare class UIButton {
    constructor(caption: string | null, buttonAction?: ((btn: UIButton, e?: React.MouseEvent) => void) | null, icon?: FunctionOrValue<string>, isEnabled?: boolean | (() => boolean) | null, visibility?: Visibility | (() => Visibility) | null, className?: FunctionOrValue<string>, style?: FunctionOrValue<React.CSSProperties>, contextExecutor?: ((action: (context: IUIContext) => void) => void) | null, child?: UIButton[] | null, name?: string | null, tooltip?: string | null, getChild?: () => MenuAction[] | UIButton[] | null);
    private _ref;
    private _caption;
    private _captionPosition;
    private _title;
    private _buttonAction;
    private _icon;
    private _isEnabled;
    private _visibility;
    private _className;
    private _style;
    private _dropDownClassName;
    private _getChild;
    private _closeRequest;
    private _isIsEnabledFunc;
    private _isVisibilityFunc;
    private _isClassNameFunc;
    private _isIconFunc;
    private _isStyleFunc;
    private _isDropDownClassNameFunc;
    private _reactComponent;
    private _contextExecutor?;
    private _asyncGuard;
    private readonly _child;
    readonly name: string;
    get getChildFunc(): (() => MenuAction[] | UIButton[] | null) | null;
    set getChildFunc(value: (() => MenuAction[] | UIButton[] | null) | null);
    get caption(): string | null;
    set caption(value: string | null);
    get captionPosition(): 'after' | 'before' | null;
    set captionPosition(value: 'after' | 'before' | null);
    get icon(): string | null;
    set icon(icon: FunctionOrValue<string>);
    get isEnabled(): boolean;
    get visibility(): Visibility;
    get buttonAction(): ((btn: UIButton, e?: React.MouseEvent) => void) | null;
    set buttonAction(action: ((btn: UIButton, e?: React.MouseEvent) => void) | null);
    get className(): string | null;
    get style(): React.CSSProperties | null;
    get dropDownClassName(): string | null;
    get reactComponent(): ((props: UIButtonComponentProps) => JSX.Element) | null;
    get child(): UIButton[];
    get tooltip(): string | null;
    set tooltip(value: string | null);
    tryGetReactComponentRef(): React.RefObject<RaisedButton> | null;
    bindReactComponentRef(ref: React.RefObject<RaisedButton>): void;
    unbindReactComponentRef(): void;
    private handleButtonAction;
    onClick: (e?: React.MouseEvent<Element, MouseEvent> | undefined) => void;
    setCloseRequest(request: (result?: any) => void): void;
    close(result?: any): void;
    setIsEnabled(isEnabled?: boolean | (() => boolean) | null): void;
    setIcon(icon?: FunctionOrValue<string>): void;
    setVisibility(visibility?: Visibility | (() => Visibility) | null): void;
    setClassName(className?: FunctionOrValue<string>): void;
    setStyle(style?: FunctionOrValue<React.CSSProperties>): void;
    setDropDownClassName(className?: FunctionOrValue<string>): void;
    setReactComponent(component: (props: UIButtonComponentProps) => JSX.Element): void;
    setContextExecutor(contextExecutor: ((action: (context: IUIContext) => void) => void) | null, overwrite?: boolean): void;
    static create(args: {
        caption?: string | null;
        buttonAction?: (btn: UIButton, e?: React.MouseEvent) => void;
        icon?: FunctionOrValue<string>;
        isEnabled?: boolean | (() => boolean) | null;
        visibility?: Visibility | (() => Visibility) | null;
        className?: FunctionOrValue<string>;
        style?: FunctionOrValue<React.CSSProperties>;
        contextExecutor?: ((action: (context: IUIContext) => void) => void) | null;
        child?: UIButton[] | null;
        getChild?: () => MenuAction[] | UIButton[] | null;
        name?: string | null;
        tooltip?: string | null;
    }): UIButton;
}
export interface UIButtonComponentProps {
    viewModel: UIButton;
    className?: string;
    style?: React.CSSProperties;
    dropDownClassName?: string;
    dropDownOpenPosition?: 'left' | 'right';
    dropDownOpenDirection?: 'left' | 'right';
    dropDownOpenUp?: boolean;
}
export interface UIButtonComponentState {
    isDropDownOpen: boolean;
}
export declare class UIButtonComponent extends React.Component<UIButtonComponentProps, UIButtonComponentState> {
    constructor(props: UIButtonComponentProps);
    componentDidMount(): void;
    componentWillUnmount(): void;
    private _dropDownRef;
    render(): JSX.Element;
    private getActions;
    private convertToMenuAction;
    private handleButtonClick;
    private handleOpenDropDown;
    private handleCloseDropDown;
}
export {};
