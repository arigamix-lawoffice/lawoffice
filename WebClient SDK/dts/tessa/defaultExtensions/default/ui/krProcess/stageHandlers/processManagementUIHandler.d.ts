import { IKrStageTypeUIHandlerContext, KrStageTypeUIHandler, StageTypeHandlerDescriptor } from 'tessa/workflow/krProcess';
/**
 * UI обработчик типа этапа {@link processManagementDescriptor}.
 */
export declare class ProcessManagementUIHandler extends KrStageTypeUIHandler {
    private static readonly _modeId;
    private _stageControl?;
    private _groupControl?;
    private _signalControl?;
    descriptors(): StageTypeHandlerDescriptor[];
    initialize(context: IKrStageTypeUIHandlerContext): Promise<void>;
    finalize(context: IKrStageTypeUIHandlerContext): Promise<void>;
    validate(context: IKrStageTypeUIHandlerContext): Promise<void>;
    private readonly modeChanged;
    private updateVisibility;
}
