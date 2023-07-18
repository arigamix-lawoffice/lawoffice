import {
  IKrStageTypeUIHandlerContext,
  KrStageTypeUIHandler,
  StageTypeHandlerDescriptor,
  addFromTemplateDescriptor
} from 'tessa/workflow/krProcess';
import { ValidationResult, ValidationResultType } from 'tessa/platform/validation';

import { plainColumnName } from 'tessa/workflow';

/**
 * UI обработчик типа этапа {@link addFromTemplateDescriptor}.
 */
export class AddFromTemplateUIHandler extends KrStageTypeUIHandler {
  //#endregion fields

  private static readonly _fileTemplateIdFieldName: string = plainColumnName(
    'KrAddFromTemplateSettingsVirtual',
    'FileTemplateID'
  );

  //#endregion

  //#region base overrides

  public descriptors(): StageTypeHandlerDescriptor[] {
    return [addFromTemplateDescriptor];
  }

  public async validate(context: IKrStageTypeUIHandlerContext): Promise<void> {
    if (!context.row.tryGet(AddFromTemplateUIHandler._fileTemplateIdFieldName)) {
      context.validationResult.add(
        ValidationResult.fromText(
          '$KrStages_AddFromTemplate_TemplateIsRequiredWarning',
          ValidationResultType.Warning
        )
      );
    }
  }

  //#endregion
}
