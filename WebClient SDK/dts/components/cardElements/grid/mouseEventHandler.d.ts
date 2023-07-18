/// <reference types="react" />
export declare type MouseEventHandlerCtor = {
    onClick?: (e: React.MouseEvent) => void;
    onDoubleClick?: (e: React.MouseEvent) => void;
    onMouseDown?: (e: React.MouseEvent) => void;
};
export declare abstract class MouseEventHandler {
    constructor(args: MouseEventHandlerCtor);
    protected _onClick: (e: React.MouseEvent) => void;
    protected _onDoubleClick: (e: React.MouseEvent) => void;
    protected _onMouseDown: (e: React.MouseEvent) => void;
    protected _onClickWrapper: (e: React.MouseEvent) => void;
    get onClick(): (e: React.MouseEvent) => void;
    set onClick(value: (e: React.MouseEvent) => void);
    get onDoubleClick(): (e: React.MouseEvent) => void;
    set onDoubleClick(value: (e: React.MouseEvent) => void);
    get onMouseDown(): (e: React.MouseEvent) => void;
    set onMouseDown(value: (e: React.MouseEvent) => void);
    get onClickWrapper(): (e: React.MouseEvent) => void;
    protected rebuildClickWrapper(): void;
}
