import { KrStageTypeFormatter, IKrStageTypeFormatterContext, StageTypeHandlerDescriptor } from 'tessa/workflow/krProcess';
export declare class NotificationStageTypeFormatter extends KrStageTypeFormatter {
    private _excludeDeputies;
    private _excludeSubscribers;
    private _optionalRecipients;
    descriptors(): StageTypeHandlerDescriptor[];
    format(context: IKrStageTypeFormatterContext): void;
    private getDisplaySettings;
}
