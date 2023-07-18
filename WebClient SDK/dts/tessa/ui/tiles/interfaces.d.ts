import { TileContextSource } from './tileContextSource';
import { TileCommandBehavior } from './tileCommandBehavior';
import { TileDirection } from './tileDirection';
import { TileEvaluationEventArgs } from './tileEvaluationEventArgs';
import { TileEvaluationResult } from './tileEvaluationResult';
import { ITileContext } from './tileContext';
import { IUIContext } from 'tessa/ui/uiContext';
import { IStorage } from 'tessa/platform/storage';
import { EventHandler } from 'tessa/platform';
export interface ITileWorkspace {
    readonly leftPanel: ITilePanel;
    readonly rightPanel: ITilePanel;
    readonly info: IStorage;
    clear(): any;
    clone(context?: TileContextSource): ITileWorkspace;
}
export interface ITilePanel extends ITileSource {
    readonly direction: TileDirection;
    readonly currentTile: ITile | null;
    readonly backTile: ITile;
    readonly scrollTop: number;
    groupTileAction(tile: ITile, scrollTop?: number): any;
    backTileAction(): any;
    reset(): any;
    clone(context?: TileContextSource): ITilePanel;
}
export interface ITile extends ITileSource {
    readonly id: guid;
    readonly name: string;
    caption: string;
    icon: string;
    command: Function | null;
    group: number;
    order: number;
    commandBehavior: TileCommandBehavior;
    isCollapsed: boolean;
    isEnabled: boolean;
    isHidden: boolean;
    readonly isVisible: boolean;
    readonly isHasVisibleTiles: boolean;
    hotkey: string;
    beforeGroupAction: ((tile: ITile) => Promise<void>) | null;
    toolTip: string;
    tileAction(context?: ITileContext, additionalCommand?: Function): any;
    clone(context?: TileContextSource): ITile;
}
export interface ITileSource {
    context: IUIContext;
    contextSource: TileContextSource;
    readonly tiles: ITile[];
    readonly sortedTiles: ITile[];
    readonly info: IStorage;
    readonly sharedInfo: IStorage;
    readonly evaluating: EventHandler<(e: TileEvaluationEventArgs) => void>;
    evaluate(forceEvaluation?: boolean): TileEvaluationResult;
    evaluateCore(e: TileEvaluationEventArgs, forceEvaluation?: boolean): any;
    evaluateSelf(): TileEvaluationResult;
    evaluateSelfCore(e: TileEvaluationEventArgs): any;
    evaluateChildren(forceEvaluation?: boolean): TileEvaluationResult;
    evaluateChildrenCore(e: TileEvaluationEventArgs, forceEvaluation?: boolean): any;
    tryGetTile(id: guid): ITile | null;
    tryGetTile(name: string): ITile | null;
    enumerateTiles(): IterableIterator<ITile>;
    resetEvaluating(): any;
    dispose(): any;
}
