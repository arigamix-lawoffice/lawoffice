import { IKrStageTypeUIHandlerContext, KrStageTypeUIHandler, StageTypeHandlerDescriptor } from 'tessa/workflow/krProcess';
/**
 * UI обработчик типа этапа {@link signingDescriptor}.
 */
export declare class SigningUIHandler extends KrStageTypeUIHandler {
    private static readonly notReturnEdit;
    private _returnIfNotSignedFlagControl?;
    private _returnAfterSigningFlagControl?;
    descriptors(): StageTypeHandlerDescriptor[];
    initialize(context: IKrStageTypeUIHandlerContext): Promise<void>;
    finalize(context: IKrStageTypeUIHandlerContext): Promise<void>;
    private onSettingsFieldChanged;
    private notReturnEditConfigureFields;
}
