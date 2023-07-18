import { IKrStageTypeUIHandlerContext, KrStageTypeUIHandler, StageTypeHandlerDescriptor } from 'tessa/workflow/krProcess';
/**
 * UI обработчик типа этапа {@link createCardDescriptor}.
 */
export declare class CreateCardUIHandler extends KrStageTypeUIHandler {
    private static readonly _templateId;
    private static readonly _templateCaption;
    private static readonly _typeId;
    private static readonly _typeCaption;
    private static readonly _modeId;
    descriptors(): StageTypeHandlerDescriptor[];
    validate(context: IKrStageTypeUIHandlerContext): Promise<void>;
    initialize(context: IKrStageTypeUIHandlerContext): Promise<void>;
    finalize(context: IKrStageTypeUIHandlerContext): Promise<void>;
    private static onSettingsFieldChanged;
}
