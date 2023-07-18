import { CardRow, CardRowState } from 'tessa/cards';
import { DefaultCardTypes, sectionName } from 'tessa/workflow';
import { DotNetType, Guid } from 'tessa/platform';
import { GridRowAction, GridRowEventArgs, GridViewModel } from 'tessa/ui/cards/controls';
import {
  IKrStageTypeUIHandlerContext,
  KrStageTypeUIHandler,
  StageTypeHandlerDescriptor,
  universalTaskDescriptor
} from 'tessa/workflow/krProcess';
import {
  IValidationResultBuilder,
  ValidationResultBuilder,
  ValidationResultType
} from 'tessa/platform/validation';

import { ArrayStorage } from 'tessa/platform/storage';
import { showNotEmpty } from 'tessa/ui';

/**
 * UI обработчик типа этапа {@link universalTaskDescriptor}.
 */
export class UniversalTaskStageTypeUIHandler extends KrStageTypeUIHandler {
  //#region fields

  private static readonly _krUniversalTaskOptionsSettingsVirtualSynthetic = sectionName(
    'KrUniversalTaskOptionsSettingsVirtual'
  );

  //#endregion

  //#region base overrides

  public descriptors(): StageTypeHandlerDescriptor[] {
    return [universalTaskDescriptor];
  }

  public async initialize(context: IKrStageTypeUIHandlerContext): Promise<void> {
    const grid = context.settingsForms
      .find(i => i.name === DefaultCardTypes.KrUniversalTaskStageTypeSettingsTypeName)
      ?.blocks.find(i => i.name === 'MainInfo')
      ?.controls.find(i => i.name === 'CompletionOptions') as GridViewModel;

    if (grid) {
      grid.rowInvoked.add(this.rowInvoked);
      grid.rowEditorClosing.add(this.rowClosing);
    }
  }

  public async finalize(context: IKrStageTypeUIHandlerContext): Promise<void> {
    const grid = context.settingsForms
      .find(i => i.name === DefaultCardTypes.KrUniversalTaskStageTypeSettingsTypeName)
      ?.blocks.find(i => i.name === 'MainInfo')
      ?.controls.find(i => i.name === 'CompletionOptions') as GridViewModel;

    if (grid) {
      grid.rowInvoked.remove(this.rowInvoked);
      grid.rowEditorClosing.remove(this.rowClosing);
    }
  }

  //#endregion

  //#region private methods

  private rowInvoked(args: GridRowEventArgs): void {
    if (args.action === GridRowAction.Inserted) {
      args.row.set('OptionID', Guid.newGuid(), DotNetType.Guid);
    }
  }

  private async rowClosing(args: GridRowEventArgs): Promise<void> {
    const row = args.row;
    let validationResult: IValidationResultBuilder | undefined;

    const optionId = row.get('OptionID');
    if (optionId) {
      const rows = args.cardModel.card.sections.get(
        UniversalTaskStageTypeUIHandler._krUniversalTaskOptionsSettingsVirtualSynthetic
      )?.rows;

      if (
        rows &&
        UniversalTaskStageTypeUIHandler.checkDuplicatesOptionId(rows, row.rowId, optionId)
      ) {
        validationResult ??= new ValidationResultBuilder();
        validationResult.add(
          ValidationResultType.Error,
          '$KrProcess_UniversalTask_CompletionOptionIDNotUnique'
        );
        args.cancel = true;
      }
    } else {
      validationResult ??= new ValidationResultBuilder();
      validationResult.add(
        ValidationResultType.Error,
        '$KrProcess_UniversalTask_CompletionOptionIDEmpty'
      );
      args.cancel = true;
    }

    if (!row.tryGet('Caption')) {
      validationResult ??= new ValidationResultBuilder();
      validationResult.add(
        ValidationResultType.Error,
        '$KrProcess_UniversalTask_CompletionOptionCaptionEmpty'
      );
      args.cancel = true;
    }

    if (validationResult) {
      await showNotEmpty(validationResult.build());
    }
  }

  private static checkDuplicatesOptionId(
    rows: ArrayStorage<CardRow>,
    rowId: guid,
    optionId: guid
  ): boolean {
    for (const row of rows) {
      if (Guid.equals(row.rowId, rowId) || row.state === CardRowState.Deleted) {
        continue;
      }

      const iOptionId = row.tryGet('OptionID');

      if (!iOptionId) {
        continue;
      }

      if (Guid.equals(optionId, iOptionId)) {
        return true;
      }
    }

    return false;
  }

  //#endregion
}
