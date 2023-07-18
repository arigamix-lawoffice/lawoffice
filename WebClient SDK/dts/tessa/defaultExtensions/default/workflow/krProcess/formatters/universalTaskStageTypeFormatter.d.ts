import { KrStageTypeFormatter, IKrStageTypeFormatterContext, StageTypeHandlerDescriptor } from 'tessa/workflow/krProcess';
export declare class UniversalTaskStageTypeFormatter extends KrStageTypeFormatter {
    private _digest;
    descriptors(): StageTypeHandlerDescriptor[];
    format(context: IKrStageTypeFormatterContext): void;
}
