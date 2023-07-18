import { IKrStageTypeUIHandlerContext, KrStageTypeUIHandler, StageTypeHandlerDescriptor } from 'tessa/workflow/krProcess';
/**
 * UI обработчик типа этапа {@link addFromTemplateDescriptor}.
 */
export declare class AddFromTemplateUIHandler extends KrStageTypeUIHandler {
    private static readonly _fileTemplateIdFieldName;
    descriptors(): StageTypeHandlerDescriptor[];
    validate(context: IKrStageTypeUIHandlerContext): Promise<void>;
}
