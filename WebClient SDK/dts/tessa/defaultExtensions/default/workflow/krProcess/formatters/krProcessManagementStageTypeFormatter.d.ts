import { KrStageTypeFormatter, IKrStageTypeFormatterContext, StageTypeHandlerDescriptor } from 'tessa/workflow/krProcess';
export declare class KrProcessManagementStageTypeFormatter extends KrStageTypeFormatter {
    private _managePrimaryProcess;
    private _modeId;
    private _modeName;
    private _stageName;
    private _groupName;
    private _groupRowName;
    private _signal;
    descriptors(): StageTypeHandlerDescriptor[];
    format(context: IKrStageTypeFormatterContext): void;
}
