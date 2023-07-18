import { KrStageTypeFormatter, IKrStageTypeFormatterContext, StageTypeHandlerDescriptor } from 'tessa/workflow/krProcess';
export declare class TypedTaskStageTypeFormatter extends KrStageTypeFormatter {
    private _taskTypeCaption;
    private _taskDigest;
    descriptors(): StageTypeHandlerDescriptor[];
    format(context: IKrStageTypeFormatterContext): void;
}
