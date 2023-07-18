import { SplitState, WorkplaceDisplayOption, WorkplaceResizeOption } from 'tessa/views/workplaces';
import { IViewContext } from './viewContext';
import { IReactRefsProvider, ReactRef } from '../reactRefProvider';
export declare class WorkplaceLayoutViewModel implements IReactRefsProvider {
    constructor(parent: WorkplaceLayoutViewModel | null | undefined, splitState: SplitState, splitPosition?: number | null, secondChildSize?: number | null);
    private _firstChild;
    private _secondChild;
    private _innerContent;
    private _splitState;
    private _splitPositionAdapted;
    private _secondChildSizeAdapted;
    private _sizeRatio;
    private _workplaceDisplayOption;
    private _workplaceResizeOption;
    private _minimumGridCellWidth;
    private _minimumGridCellHeight;
    private _resizerPosition;
    private _splitBreakpoint;
    private _caption;
    readonly parent: WorkplaceLayoutViewModel | null;
    get firstChild(): WorkplaceLayoutViewModel | null;
    set firstChild(value: WorkplaceLayoutViewModel | null);
    /**
     * Workplace display option in accordance with _WorkplaceDisplayOption_
     */
    get workplaceDisplayOption(): WorkplaceDisplayOption;
    set workplaceDisplayOption(value: WorkplaceDisplayOption);
    /**
     * Workplace resize option in accordance with _WorkplaceResizeOption_
     */
    get workplaceResizeOption(): WorkplaceResizeOption;
    set workplaceResizeOption(value: WorkplaceResizeOption);
    /**
     * The minimum height of the workplace layout child.
     */
    get minimumGridCellHeight(): number;
    set minimumGridCellHeight(value: number);
    /**
     * The minimum width of the workplace layout child.
     */
    get minimumGridCellWidth(): number;
    set minimumGridCellWidth(value: number);
    get secondChild(): WorkplaceLayoutViewModel | null;
    set secondChild(value: WorkplaceLayoutViewModel | null);
    get innerContent(): any | null;
    set innerContent(value: any | null);
    /**
     * Breakpoint value from which the workplace will be splited in accordance with Tessa Admin settings.
     */
    get splitBreakpoint(): number;
    set splitBreakpoint(value: number);
    /**
     * Split option within _SplitState_.
     */
    get splitState(): SplitState;
    /**
     * The size ratio of the second child to the first one.
     */
    get sizeRatio(): number;
    /**
     * Resizer position in pixels relative to the beginning of the first child.
     */
    get resizerPosition(): number;
    get caption(): string;
    set caption(value: string);
    selectionAction: (context: IViewContext | null) => void;
    get isSelectionListener(): boolean;
    getReactRefs(): ReactRef[];
    onResize(diff: number, actualSize: number, actualSizeChildFirst: number, actualSizeChildSecond: number, minimalCellSize: number): boolean;
    onResizeEnd(): void;
    private onViewResize;
    private onViewResizeEnd;
}
