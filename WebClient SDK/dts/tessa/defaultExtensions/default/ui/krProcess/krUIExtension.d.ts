import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
/**
 * Скрывает результаты запроса комментария из задания согласования если комментарий не запрашивался.
 * Скрывает поле "Комментарий" для варианта завершения "Согласовать", если установлена соответствующая настройка.
 */
export declare class KrUIExtension extends CardUIExtension {
    private _commentIsHiddenForApproval;
    private static postponeMetadataInitializing;
    private static postponeContentInitializing;
    initialized(context: ICardUIExtensionContext): void;
    private commentIsHiddenForApproval;
    private modifyTaskAndAttachHandlers;
    private modifySigningTask;
    private modifyUniversalTask;
    private modifyUniversalTaskAction;
    private generateTaskAction;
}
