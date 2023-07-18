import { ITile } from './interfaces';
export declare class TileEvaluationAction {
    constructor(tile: ITile, isCollapsed?: boolean, isEnabled?: boolean, isHidden?: boolean);
    readonly tile: ITile;
    readonly isCollapsed?: boolean;
    get isCollapsedEffective(): boolean;
    readonly isEnabled?: boolean;
    get isEnabledEffective(): boolean;
    readonly isHidden?: boolean;
    get isHiddenEffective(): boolean;
    apply(): void;
}
