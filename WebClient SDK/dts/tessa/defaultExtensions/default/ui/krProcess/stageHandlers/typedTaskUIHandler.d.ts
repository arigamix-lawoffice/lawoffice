import { IKrStageTypeUIHandlerContext, KrStageTypeUIHandler, StageTypeHandlerDescriptor } from 'tessa/workflow/krProcess';
/**
 * UI обработчик типа этапа {@link typedTaskDescriptor}.
 */
export declare class TypedTaskUIHandler extends KrStageTypeUIHandler {
    descriptors(): StageTypeHandlerDescriptor[];
    validate(context: IKrStageTypeUIHandlerContext): Promise<void>;
}
