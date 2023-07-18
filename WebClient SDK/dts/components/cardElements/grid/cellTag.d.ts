import { MouseEventHandler, MouseEventHandlerCtor } from './mouseEventHandler';
export declare type CellTagCtor = {
    icon: string;
    toolTip?: string;
    visible?: boolean;
} & MouseEventHandlerCtor;
export declare class CellTag extends MouseEventHandler {
    constructor(args: CellTagCtor);
    protected _icon: string;
    protected _toolTip: string;
    protected _visible: boolean;
    get icon(): string;
    set icon(value: string);
    get toolTip(): string;
    set toolTip(value: string);
    get visible(): boolean;
    set visible(value: boolean);
}
