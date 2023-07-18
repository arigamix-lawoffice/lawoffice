import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
/**
 * Расширение для модификации UI карточки и заданий в соответствии с бизнес-процессами Workflow.
 */
export declare class WfCardUIExtension extends CardUIExtension {
    private _disposes;
    initialized(context: ICardUIExtensionContext): void;
    reopening(context: ICardUIExtensionContext): void;
    finalized(): void;
    private static createTaskInfo;
    private static modifyResolutionTask;
    static taskCardNavigateAction: (taskRowId?: string | undefined) => Promise<void>;
    private static beginTaskCardNavigateAction;
    private static postponeMetadataInitializing;
    private static postponeContentInitializing;
}
