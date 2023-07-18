import { IKrStageTypeUIHandlerContext, KrStageTypeUIHandler, StageTypeHandlerDescriptor } from 'tessa/workflow/krProcess';
/**
 * UI обработчик типа этапа {@link approvalDescriptor}.
 */
export declare class ApprovalUIHandler extends KrStageTypeUIHandler {
    /**
     * Идентификатор карточки вида задания "Рекомендательное согласование".
     */
    private static readonly _advisoryTaskKindId;
    private static readonly _advisoryField;
    private static readonly _notReturnEditField;
    private static readonly _kindIdField;
    private static readonly _kindCaptionField;
    private static readonly _returnWhenDisapprovedField;
    private _settings?;
    private _returnIfNotApprovedFlagControl?;
    private _returnAfterApprovalFlagControl?;
    descriptors(): StageTypeHandlerDescriptor[];
    initialize(context: IKrStageTypeUIHandlerContext): Promise<void>;
    finalize(): Promise<void>;
    private readonly onSettingsFieldChanged;
    private getKind;
    private advisoryConfigureFields;
    private notReturnEditConfigureFields;
}
