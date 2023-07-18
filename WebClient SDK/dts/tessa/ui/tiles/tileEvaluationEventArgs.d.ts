import { ITileSource, ITile } from './interfaces';
import { TileEvaluationAction } from './tileEvaluationAction';
export declare class TileEvaluationEventArgs {
    constructor(source: ITileSource);
    private _records;
    readonly tile: ITile;
    current: ITileSource;
    get currentTile(): ITile;
    get actionCount(): number;
    getIsCollapsed(tile: ITile): boolean | undefined;
    getIsCollapsedEffective(tile: ITile): boolean;
    getIsEnabled(tile: ITile): boolean | undefined;
    getIsEnabledEffective(tile: ITile): boolean;
    getIsHidden(tile: ITile): boolean | undefined;
    getIsHiddenEffective(tile: ITile): boolean;
    getIsVisible(tile: ITile): boolean | undefined;
    getIsVisibleEffective(tile: ITile): boolean;
    getInfo(tile: ITile): TileRecord;
    getInfoEffective(tile: ITile): TileRecord;
    setIsCollapsed(tile: ITile, isCollapsed: boolean): void;
    setIsEnabled(tile: ITile, isEnabled: boolean): void;
    setIsEnabledWithCollapsing(tile: ITile, isEnabled: boolean): void;
    setIsHidden(tile: ITile, isHidden: boolean): void;
    prepareActions(): TileEvaluationAction[];
    tryPrepareFirstAction(): TileEvaluationAction | null;
}
export interface TileRecord {
    isCollapsed?: boolean;
    isEnabled?: boolean;
    isHidden?: boolean;
}
