import { UIButton } from 'tessa/ui/uiButton';
import { PreviewManager } from 'tessa/ui/cards/previewManager';
import { ClassNameList } from 'tessa/ui/classNameList';
import { CardFilePreviewPosition } from 'tessa/cards';
import { ICardModel } from '../interfaces';
export interface IDefaultPreviewArea {
    readonly cardModel: ICardModel;
    readonly buttons: UIButton[];
    readonly className: ClassNameList;
    isCollapsed: boolean;
    alwaysHidden: boolean;
    initialize(): void;
    dispose(): void;
}
export declare class DefaultPreviewAreaViewModel implements IDefaultPreviewArea {
    constructor(model: ICardModel);
    protected _initialized: boolean;
    protected _alwaysHidden: boolean;
    private static _sideAtom;
    private static _widthAtom;
    readonly cardModel: ICardModel;
    readonly previewManager: PreviewManager;
    readonly buttons: UIButton[];
    readonly className: ClassNameList;
    get alwaysHidden(): boolean;
    set alwaysHidden(value: boolean);
    get isCollapsed(): boolean;
    set isCollapsed(value: boolean);
    private getVisibilityPreviewButton;
    private initializePreviewButtons;
    initialize(): void;
    protected initializeCore(): void;
    dispose(): void;
    static setFilePreviewPosition(value?: CardFilePreviewPosition): void;
    static get filePreviewPosition(): CardFilePreviewPosition;
    static setFilePreviewWidth(value: number): void;
    static get filePreviewWidth(): number;
}
export interface DefaultPreviewAreaViewModelSettings {
    previewSide?: CardFilePreviewPosition;
    previewWidth?: number;
}
