import { KrStageTypeFormatter, IKrStageTypeFormatterContext, StageTypeHandlerDescriptor } from 'tessa/workflow/krProcess';
export declare class KrSigningStageTypeFormatter extends KrStageTypeFormatter {
    private _isParallel;
    descriptors(): StageTypeHandlerDescriptor[];
    format(context: IKrStageTypeFormatterContext): void;
}
