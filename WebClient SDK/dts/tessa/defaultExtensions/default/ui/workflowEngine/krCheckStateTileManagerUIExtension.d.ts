import { WorkflowEngineTileManagerUIExtension, IWorkflowEngineTileManagerUIExtensionContext } from 'tessa/ui/workflow';
/**
 * Обработка расширения прав доступа к тайлам процесса, которое проверяет состояния документа.
 */
export declare class KrCheckStateTileManagerUIExtension extends WorkflowEngineTileManagerUIExtension {
    getExtensionId(): guid;
    modifyButtonRow(context: IWorkflowEngineTileManagerUIExtensionContext): Promise<void>;
}
