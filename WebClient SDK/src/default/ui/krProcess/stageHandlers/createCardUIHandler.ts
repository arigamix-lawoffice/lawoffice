import {
  IKrStageTypeUIHandlerContext,
  KrStageTypeUIHandler,
  StageTypeHandlerDescriptor,
  createCardDescriptor
} from 'tessa/workflow/krProcess';
import { ValidationResult, ValidationResultType } from 'tessa/platform/validation';

import { CardFieldChangedEventArgs } from 'tessa/cards';
import { plainColumnName } from 'tessa/workflow';

/**
 * UI обработчик типа этапа {@link createCardDescriptor}.
 */
export class CreateCardUIHandler extends KrStageTypeUIHandler {
  //#region fields

  private static readonly _templateId: string = plainColumnName(
    'KrCreateCardStageSettingsVirtual',
    'TemplateID'
  );

  private static readonly _templateCaption: string = plainColumnName(
    'KrCreateCardStageSettingsVirtual',
    'TemplateCaption'
  );

  private static readonly _typeId: string = plainColumnName(
    'KrCreateCardStageSettingsVirtual',
    'TypeID'
  );

  private static readonly _typeCaption: string = plainColumnName(
    'KrCreateCardStageSettingsVirtual',
    'TypeCaption'
  );

  private static readonly _modeId: string = plainColumnName(
    'KrCreateCardStageSettingsVirtual',
    'ModeID'
  );

  //#endregion

  //#region base overrides

  public descriptors(): StageTypeHandlerDescriptor[] {
    return [createCardDescriptor];
  }

  public async validate(context: IKrStageTypeUIHandlerContext): Promise<void> {
    const template = context.row.tryGet(CreateCardUIHandler._templateId);
    const type = context.row.tryGet(CreateCardUIHandler._typeId);

    if (!template && !type) {
      context.validationResult.add(
        ValidationResult.fromText(
          '$KrStages_CreateCard_TemplateAndTypeNotSpecified',
          ValidationResultType.Error
        )
      );
    } else if (template && type) {
      context.validationResult.add(
        ValidationResult.fromText(
          '$KrStages_CreateCard_TemplateAndTypeSelected',
          ValidationResultType.Error
        )
      );
    }

    if (context.row.tryGet(CreateCardUIHandler._modeId) == undefined) {
      context.validationResult.add(
        ValidationResult.fromText('$KrStages_CreateCard_ModeRequired', ValidationResultType.Error)
      );
    }
  }

  public async initialize(context: IKrStageTypeUIHandlerContext): Promise<void> {
    context.row.fieldChanged.add(CreateCardUIHandler.onSettingsFieldChanged);
  }

  public async finalize(context: IKrStageTypeUIHandlerContext): Promise<void> {
    context.row.fieldChanged.remove(CreateCardUIHandler.onSettingsFieldChanged);
  }

  //#endregion

  //#region private methods

  private static onSettingsFieldChanged(e: CardFieldChangedEventArgs): void {
    if (e.fieldName === CreateCardUIHandler._typeId) {
      if (e.fieldValue) {
        e.storage.set(CreateCardUIHandler._templateId, null);
        e.storage.set(CreateCardUIHandler._templateCaption, null);
      }
    } else if (e.fieldName === CreateCardUIHandler._templateId) {
      if (e.fieldValue) {
        e.storage.set(CreateCardUIHandler._typeId, null);
        e.storage.set(CreateCardUIHandler._typeCaption, null);
      }
    }
  }

  //#endregion
}
