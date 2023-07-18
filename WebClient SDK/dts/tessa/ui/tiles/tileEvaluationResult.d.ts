import { TileEvaluationAction } from './tileEvaluationAction';
import { TileEvaluationEventArgs } from './tileEvaluationEventArgs';
export declare class TileEvaluationResult {
    constructor(actions: TileEvaluationAction[], eventArgs: TileEvaluationEventArgs);
    readonly actions: ReadonlyArray<TileEvaluationAction>;
    readonly eventArgs: TileEvaluationEventArgs;
    apply(): void;
}
