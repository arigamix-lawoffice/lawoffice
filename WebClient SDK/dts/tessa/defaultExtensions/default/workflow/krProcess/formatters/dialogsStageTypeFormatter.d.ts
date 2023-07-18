import { KrStageTypeFormatter, IKrStageTypeFormatterContext, StageTypeHandlerDescriptor } from 'tessa/workflow/krProcess';
export declare class DialogsStageTypeFormatter extends KrStageTypeFormatter {
    private _kindCaption;
    private _taskDigest;
    descriptors(): StageTypeHandlerDescriptor[];
    format(context: IKrStageTypeFormatterContext): void;
}
