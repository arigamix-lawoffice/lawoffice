import { IKrStageTypeUIHandlerContext, KrStageTypeUIHandler, StageTypeHandlerDescriptor } from 'tessa/workflow/krProcess';
/**
 * UI обработчик типа этапа {@link dialogDescriptor}.
 */
export declare class DialogUIHandler extends KrStageTypeUIHandler {
    private static readonly _cardStoreModeId;
    private static readonly _openModeId;
    private static readonly _dialogTypeId;
    private static readonly _dialogTypeName;
    private static readonly _dialogTypeCaption;
    private static readonly _templateId;
    private static readonly _templateCaption;
    private _blockContentIndicator?;
    descriptors(): StageTypeHandlerDescriptor[];
    validate(context: IKrStageTypeUIHandlerContext): Promise<void>;
    initialize(context: IKrStageTypeUIHandlerContext): Promise<void>;
    finalize(context: IKrStageTypeUIHandlerContext): Promise<void>;
    private static buttonSettings_RowClosing;
    private static onSettingsFieldChanged;
}
