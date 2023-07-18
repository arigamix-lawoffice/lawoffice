import { ITile, ITileSource } from './interfaces';
import { TileContextSource } from './tileContextSource';
import { TileEvaluationEventArgs } from './tileEvaluationEventArgs';
import { TileEvaluationResult } from './tileEvaluationResult';
import { IStorage } from 'tessa/platform/storage';
import { IUIContext } from 'tessa/ui/uiContext';
import { EventHandler } from 'tessa/platform';
export declare abstract class TileSource implements ITileSource {
    constructor(source: TileContextSource, tiles?: ITile[] | null, info?: IStorage | null, sharedInfo?: IStorage | null, evaluating?: ((e: TileEvaluationEventArgs) => void) | null);
    protected _evaluatingHandler: ((e: TileEvaluationEventArgs) => void) | null;
    get context(): IUIContext;
    set context(value: IUIContext);
    contextSource: TileContextSource;
    readonly tiles: ITile[];
    get sortedTiles(): ITile[];
    readonly info: IStorage;
    readonly sharedInfo: IStorage;
    readonly evaluating: EventHandler<(e: TileEvaluationEventArgs) => void>;
    private static tileSorting;
    private evaluateInvoke;
    protected childrenAreAvailableForEvaluation(_e: TileEvaluationEventArgs): boolean;
    evaluate(forceEvaluation?: boolean): TileEvaluationResult;
    evaluateCore(e: TileEvaluationEventArgs, forceEvaluation?: boolean): void;
    evaluateSelf(): TileEvaluationResult;
    evaluateSelfCore(e: TileEvaluationEventArgs): void;
    evaluateChildren(forceEvaluation?: boolean): TileEvaluationResult;
    evaluateChildrenCore(e: TileEvaluationEventArgs, forceEvaluation?: boolean): void;
    tryGetTile(id: guid): ITile | null;
    tryGetTile(name: string): ITile | null;
    enumerateTiles(): IterableIterator<ITile>;
    resetEvaluating(): void;
    dispose(): void;
}
