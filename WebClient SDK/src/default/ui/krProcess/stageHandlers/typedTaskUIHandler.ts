import {
  IKrStageTypeUIHandlerContext,
  KrStageTypeUIHandler,
  StageTypeHandlerDescriptor,
  typedTaskDescriptor
} from 'tessa/workflow/krProcess';
import { ValidationResult, ValidationResultType } from 'tessa/platform/validation';

import { plainColumnName } from 'tessa/workflow';

/**
 * UI обработчик типа этапа {@link typedTaskDescriptor}.
 */
export class TypedTaskUIHandler extends KrStageTypeUIHandler {
  //#region base overrides

  public descriptors(): StageTypeHandlerDescriptor[] {
    return [typedTaskDescriptor];
  }

  public async validate(context: IKrStageTypeUIHandlerContext): Promise<void> {
    if (!context.row.tryGet(plainColumnName('KrTypedTaskSettingsVirtual', 'TaskTypeID'))) {
      context.validationResult.add(
        ValidationResult.fromText('$KrStages_TypedTask_TaskType', ValidationResultType.Error)
      );
    }
  }

  //#endregion
}
