import { IKrStageTypeUIHandlerContext, KrStageTypeUIHandler, StageTypeHandlerDescriptor } from 'tessa/workflow/krProcess';
/**
 * UI обработчик типа этапа {@link resolutionDescriptor}.
 */
export declare class ResolutionStageUIHandler extends KrStageTypeUIHandler {
    constructor();
    private static readonly _krResolutionSettingsVirtual;
    private static readonly _krPerformersVirtual;
    private static readonly _controllerId;
    private static readonly _controllerName;
    private static readonly _planned;
    private static readonly _durationInDays;
    private static readonly _withControl;
    private static readonly _massCreation;
    private static readonly _majorPerformer;
    private static readonly _krPerformersVirtualSynthetic;
    private _settings?;
    private _performers?;
    private _controller?;
    private _subscribedTo;
    descriptors(): StageTypeHandlerDescriptor[];
    initialize(context: IKrStageTypeUIHandlerContext): Promise<void>;
    finalize(context: IKrStageTypeUIHandlerContext): Promise<void>;
    private readonly onSettingsFieldChanged;
    private readonly onPerformerStateChanged;
    private readonly onPerformersChanged;
    private performersChanged;
    private alivePerformer;
    private enableMassCreation;
}
