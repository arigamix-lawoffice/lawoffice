import { LocalizationManager } from 'tessa/localization';
import {
  WorkflowEngineTileManagerUIExtension,
  IWorkflowEngineTileManagerUIExtensionContext
} from 'tessa/ui/workflow';

/**
 * Обработка расширения прав доступа к тайлам процесса, которое проверяет состояния документа.
 */
export class KrCheckStateTileManagerUIExtension extends WorkflowEngineTileManagerUIExtension {
  //region methods

  getExtensionId(): guid {
    return '9fce6311-1746-412a-9346-77394caebe91';
  }

  async modifyButtonRow(context: IWorkflowEngineTileManagerUIExtensionContext): Promise<void> {
    const contextRoleRows = context.allButtonRows.get('Extension_KrCheckStateTileExtension');

    if (contextRoleRows && contextRoleRows.length > 0) {
      const arrayValues: string[] = [];

      context.result += LocalizationManager.instance.localize(
        '$KrTileExtensions_CheckState_States'
      );
      contextRoleRows.map(x => {
        arrayValues.push(LocalizationManager.instance.localize(x.get('StateName')));
      });
      context.result += arrayValues.join('; ');
      context.result += '\n';
    }
  }

  //endregion
}
