import { KrStageTypeFormatter, IKrStageTypeFormatterContext, StageTypeHandlerDescriptor } from 'tessa/workflow/krProcess';
export declare class KrResolutionStageTypeFormatter extends KrStageTypeFormatter {
    private _kindCaption;
    private _authorName;
    private _controllerName;
    private _planned;
    private _durationInDays;
    private _withControl;
    descriptors(): StageTypeHandlerDescriptor[];
    format(context: IKrStageTypeFormatterContext): void;
}
