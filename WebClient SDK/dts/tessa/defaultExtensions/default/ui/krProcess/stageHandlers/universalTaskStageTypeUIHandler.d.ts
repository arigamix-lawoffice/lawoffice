import { IKrStageTypeUIHandlerContext, KrStageTypeUIHandler, StageTypeHandlerDescriptor } from 'tessa/workflow/krProcess';
/**
 * UI обработчик типа этапа {@link universalTaskDescriptor}.
 */
export declare class UniversalTaskStageTypeUIHandler extends KrStageTypeUIHandler {
    private static readonly _krUniversalTaskOptionsSettingsVirtualSynthetic;
    descriptors(): StageTypeHandlerDescriptor[];
    initialize(context: IKrStageTypeUIHandlerContext): Promise<void>;
    finalize(context: IKrStageTypeUIHandlerContext): Promise<void>;
    private rowInvoked;
    private rowClosing;
    private static checkDuplicatesOptionId;
}
