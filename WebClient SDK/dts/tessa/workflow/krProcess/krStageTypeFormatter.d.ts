import { IKrStageTypeFormatterContext } from './krStageTypeFormatterContext';
import { StageTypeHandlerDescriptor } from './stageTypeHandlerDescriptor';
import { IExtension } from 'tessa/extensions';
import { IStorage } from 'tessa/platform/storage';
export interface IKrStageTypeFormatter extends IExtension {
    format(context: IKrStageTypeFormatterContext): any;
}
export declare class KrStageTypeFormatter implements IKrStageTypeFormatter {
    static readonly type = "KrStageTypeFormatter";
    private _performerName;
    private _krPerformersVirtual;
    shouldExecute(context: IKrStageTypeFormatterContext): boolean;
    descriptors(): StageTypeHandlerDescriptor[];
    format(context: IKrStageTypeFormatterContext): void;
    protected defaultDateFormatting(planned: any, timeLimit: any, context: IKrStageTypeFormatterContext): void;
    protected appendString(builder: {
        text: string;
    }, settings: IStorage, name: string, caption: string, localizable?: boolean, canBeWithoutValue?: boolean, limit?: number): void;
}
