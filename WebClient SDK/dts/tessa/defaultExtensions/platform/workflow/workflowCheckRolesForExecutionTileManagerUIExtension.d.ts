import { WorkflowEngineTileManagerUIExtension, IWorkflowEngineTileManagerUIExtensionContext } from 'tessa/ui/workflow';
/**
 * Обработка расширения прав доступа к тайлам процесса, которое проверяет доступ по ролям на выполнение.
 */
export declare class WorkflowCheckRolesForExecutionTileManagerUIExtension extends WorkflowEngineTileManagerUIExtension {
    getExtensionId(): guid;
    modifyButtonRow(context: IWorkflowEngineTileManagerUIExtensionContext): Promise<void>;
}
