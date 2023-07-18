import { KrStageTypeFormatter, IKrStageTypeFormatterContext, StageTypeHandlerDescriptor } from 'tessa/workflow/krProcess';
export declare class KrEditStageTypeFormatter extends KrStageTypeFormatter {
    private _changeState;
    descriptors(): StageTypeHandlerDescriptor[];
    format(context: IKrStageTypeFormatterContext): void;
}
