import { AdvancedDialogCommandHandler } from './advancedDialogCommandHandler';
import {
  IClientCommandHandlerContext,
  KrProcessInstance,
  launchProcess
} from 'tessa/workflow/krProcess';
import { showNotEmpty, tryGetFromInfo } from 'tessa/ui';
import {
  CardTaskCompletionOptionSettings,
  CardTaskDialogActionResult,
  systemKeyPrefix
} from 'tessa/cards';
import { IStorage } from 'tessa/platform/storage';
import { ICardEditorModel } from 'tessa/ui/cards';
import { createTypedField, DotNetType } from 'tessa/platform';

/**
 * Обработчик клиентской команды DefaultCommandTypes.ShowAdvancedDialog.
 */
export class KrAdvancedDialogCommandHandler extends AdvancedDialogCommandHandler {
  protected prepareDialogCommand(
    context: IClientCommandHandlerContext
  ): CardTaskCompletionOptionSettings | null {
    const parameters = context.command.parameters;
    if (!parameters.ProcessInstance) {
      return null;
    }
    const coSettingsObj = parameters.CompletionOptionSettings;
    if (!coSettingsObj) {
      return null;
    }
    return new CardTaskCompletionOptionSettings(coSettingsObj);
  }

  protected async completeDialogCore(
    actionResult: CardTaskDialogActionResult,
    context: IClientCommandHandlerContext,
    _cardEditor: ICardEditorModel,
    parentCardEditor: ICardEditorModel | null
  ): Promise<boolean> {
    const parameters = context.command.parameters;
    const instanceStorage = tryGetFromInfo<IStorage | null>(parameters, 'ProcessInstance', null);
    if (!instanceStorage) {
      return true;
    }

    const processInstance = new KrProcessInstance(instanceStorage);
    const requestInfo: IStorage = {};

    if (parentCardEditor && parentCardEditor.cardModel) {
      const card = parentCardEditor.cardModel.card;
      card.info[systemKeyPrefix + 'CardTaskDialogActionResult'] = actionResult.getStorage();
    } else {
      requestInfo[systemKeyPrefix + 'CardTaskDialogActionResult'] = actionResult.getStorage();
    }

    requestInfo[systemKeyPrefix + 'WebAdvancedDialogCommandSkipUIContextFlag'] = createTypedField(
      true,
      DotNetType.Boolean
    );

    const result = await launchProcess(processInstance, {
      cardEditor: parentCardEditor!,
      requestInfo: requestInfo
    });
    if (!result) {
      return false;
    }

    await showNotEmpty(result.validationResult.build());
    return (
      result.validationResult.isSuccessful &&
      !tryGetFromInfo(result.cardResponse.info, `${systemKeyPrefix}KeepTaskDialog`, false)
    );
  }
}
