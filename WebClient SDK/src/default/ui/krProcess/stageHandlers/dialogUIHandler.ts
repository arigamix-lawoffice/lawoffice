import { DefaultCardTypes, plainColumnName } from 'tessa/workflow';
import { GridRowEventArgs, GridViewModel } from 'tessa/ui/cards/controls';
import { IBlockViewModel, IFormWithBlocksViewModel } from 'tessa/ui/cards';
import {
  IKrStageTypeUIHandlerContext,
  KrStageTypeUIHandler,
  StageTypeHandlerDescriptor,
  dialogDescriptor
} from 'tessa/workflow/krProcess';
import {
  IValidationResultBuilder,
  ValidationResult,
  ValidationResultBuilder,
  ValidationResultType
} from 'tessa/platform/validation';

import { BlockContentIndicator } from '../../blockContentIndicator';
import { CardFieldChangedEventArgs } from 'tessa/cards';
import { showNotEmpty } from 'tessa/ui';

/**
 * UI обработчик типа этапа {@link dialogDescriptor}.
 */
export class DialogUIHandler extends KrStageTypeUIHandler {
  //#region fields

  private static readonly _cardStoreModeId: string = plainColumnName(
    'KrDialogStageTypeSettingsVirtual',
    'CardStoreModeID'
  );

  private static readonly _openModeId: string = plainColumnName(
    'KrDialogStageTypeSettingsVirtual',
    'OpenModeID'
  );

  private static readonly _dialogTypeId: string = plainColumnName(
    'KrDialogStageTypeSettingsVirtual',
    'DialogTypeID'
  );

  private static readonly _dialogTypeName: string = plainColumnName(
    'KrDialogStageTypeSettingsVirtual',
    'DialogTypeName'
  );

  private static readonly _dialogTypeCaption: string = plainColumnName(
    'KrDialogStageTypeSettingsVirtual',
    'DialogTypeCaption'
  );

  private static readonly _templateId: string = plainColumnName(
    'KrDialogStageTypeSettingsVirtual',
    'TemplateID'
  );

  private static readonly _templateCaption: string = plainColumnName(
    'KrDialogStageTypeSettingsVirtual',
    'TemplateCaption'
  );

  private _blockContentIndicator?: BlockContentIndicator;

  //#endregion

  //#region base overrides

  public descriptors(): StageTypeHandlerDescriptor[] {
    return [dialogDescriptor];
  }

  public async validate(context: IKrStageTypeUIHandlerContext): Promise<void> {
    if (context.row.tryGet(DialogUIHandler._cardStoreModeId) == undefined) {
      context.validationResult.add(
        ValidationResult.fromText(
          '$KrStages_Dialog_CardStoreModeNotSpecified',
          ValidationResultType.Error
        )
      );
    }

    if (context.row.tryGet(DialogUIHandler._openModeId) == undefined) {
      context.validationResult.add(
        ValidationResult.fromText(
          '$KrStages_Dialog_CardOpenModeNotSpecified',
          ValidationResultType.Error
        )
      );
    }

    if (
      !context.row.tryGet(DialogUIHandler._dialogTypeId) &&
      !context.row.tryGet(DialogUIHandler._templateId)
    ) {
      context.validationResult.add(
        ValidationResult.fromText(
          '$KrStages_Dialog_TemplateAndTypeNotSpecified',
          ValidationResultType.Error
        )
      );
    }

    if (
      context.row.tryGet(DialogUIHandler._dialogTypeId) &&
      context.row.tryGet(DialogUIHandler._templateId)
    ) {
      context.validationResult.add(
        ValidationResult.fromText(
          '$KrStages_Dialog_TemplateAndTypeSelected',
          ValidationResultType.Error
        )
      );
    }
  }

  public async initialize(context: IKrStageTypeUIHandlerContext): Promise<void> {
    context.row.fieldChanged.add(DialogUIHandler.onSettingsFieldChanged);

    let form: IFormWithBlocksViewModel | undefined;

    if (
      !(form = context.settingsForms.find(
        i => i.name === DefaultCardTypes.KrDialogStageTypeSettingsTypeName
      ))
    ) {
      return;
    }

    let mainInfoBlock: IBlockViewModel | undefined;
    let grid: GridViewModel | undefined;

    if (
      (mainInfoBlock = form.blocks.find(i => i.name === 'MainInfo')) &&
      (grid = mainInfoBlock.controls.find(i => i.name === 'ButtonSettings') as GridViewModel)
    ) {
      grid.rowEditorClosing.add(DialogUIHandler.buttonSettings_RowClosing);
    }

    const control = form.blocks.find(i => i.name === 'KrDialogScriptsBlock');
    if (control) {
      const sectionMeta = context.cardModel.cardMetadata.getSectionByName('KrStagesVirtual');

      if (sectionMeta) {
        const fieldIDs = new Map<string, string>();

        for (const column of sectionMeta.columns) {
          if (column.id && column.name) {
            fieldIDs.set(column.id, column.name);
          }
        }

        this._blockContentIndicator = new BlockContentIndicator(control, context.row, fieldIDs);
      }
    }
  }

  public async finalize(context: IKrStageTypeUIHandlerContext): Promise<void> {
    context.row.fieldChanged.remove(DialogUIHandler.onSettingsFieldChanged);

    let form: IFormWithBlocksViewModel | undefined;
    let block: IBlockViewModel | undefined;
    let grid: GridViewModel | undefined;

    if (
      (form = context.settingsForms.find(
        i => i.name === DefaultCardTypes.KrDialogStageTypeSettingsTypeName
      )) &&
      (block = form.blocks.find(i => i.name === 'MainInfo')) &&
      (grid = block.controls.find(i => i.name === 'ButtonSettings') as GridViewModel)
    ) {
      grid.rowEditorClosing.remove(DialogUIHandler.buttonSettings_RowClosing);
    }

    if (this._blockContentIndicator) {
      this._blockContentIndicator.dispose();
      this._blockContentIndicator = undefined;
    }
  }

  //#endregion

  //#region private methods

  private static buttonSettings_RowClosing(e: GridRowEventArgs): void {
    const row = e.row;
    let validationResult: IValidationResultBuilder | undefined;

    if (row.tryGet('TypeID') == undefined) {
      validationResult ??= new ValidationResultBuilder();
      validationResult.add(ValidationResult.fromError('$KrStages_Dialog_ButtonTypeIDNotSpecified'));
      e.cancel = true;
    }

    if (!row.tryGet('Caption')) {
      validationResult ??= new ValidationResultBuilder();
      validationResult.add(
        ValidationResult.fromError('$KrStages_Dialog_ButtonCaptionNotSpecified')
      );
      e.cancel = true;
    }

    if (!row.tryGet('Name')) {
      validationResult ??= new ValidationResultBuilder();
      validationResult.add(ValidationResult.fromError('$KrStages_Dialog_ButtonAliasNotSpecified'));
      e.cancel = true;
    }

    if (validationResult) {
      showNotEmpty(validationResult.build());
    }
  }

  private static onSettingsFieldChanged(e: CardFieldChangedEventArgs): void {
    const settings = e.storage;

    if (e.fieldName === DialogUIHandler._dialogTypeId) {
      if (e.fieldValue) {
        settings.set(DialogUIHandler._templateId, null);
        settings.set(DialogUIHandler._templateCaption, null);
      }
    } else if (e.fieldName === DialogUIHandler._templateId) {
      if (e.fieldValue) {
        settings.set(DialogUIHandler._dialogTypeId, null);
        settings.set(DialogUIHandler._dialogTypeName, null);
        settings.set(DialogUIHandler._dialogTypeCaption, null);
      }
    }
  }

  //#endregion
}
