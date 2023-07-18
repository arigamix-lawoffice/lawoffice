import { AdvancedDialogCommandHandler } from '../krProcess/commandInterpreter/advancedDialogCommandHandler';
import {
  CardTaskCompletionOptionSettings,
  CardTaskDialogActionResult,
  systemKeyPrefix
} from 'tessa/cards';
import { IClientCommandHandlerContext } from 'tessa/workflow/krProcess';
import { ICardEditorModel } from 'tessa/ui/cards';
import { IStorage } from 'tessa/platform/storage';
import { showNotEmpty, tryGetFromInfo } from 'tessa/ui';
import { WorkflowEngineProcessorClient, WorkflowEngineProcessStorageRequest } from 'tessa/workflow';
import { createTypedField, DotNetType } from 'tessa/platform';

/**
 * Обработчик клиентской команды DefaultCommandTypes.WeShowAdvancedDialog.
 */
export class WeAdvancedDialogCommandHandler extends AdvancedDialogCommandHandler {
  protected prepareDialogCommand(
    context: IClientCommandHandlerContext
  ): CardTaskCompletionOptionSettings | null {
    const parameters = context.command.parameters;
    const dialogSettingsStorage: IStorage = parameters.CompletionOptionSettings;
    const coSettings = tryGetFromInfo<IStorage | null>(
      dialogSettingsStorage,
      systemKeyPrefix + 'DialogSettings',
      null
    );
    if (coSettings) {
      return new CardTaskCompletionOptionSettings(coSettings);
    }
    return null;
  }

  protected async completeDialogCore(
    actionResult: CardTaskDialogActionResult,
    context: IClientCommandHandlerContext,
    _dialogCardEditor: ICardEditorModel | null,
    parentCardEditor: ICardEditorModel | null
  ): Promise<boolean> {
    const dialogSettings = context.command.parameters.CompletionOptionSettings;
    const { request, requestSignature } = this.getProcessRequest(dialogSettings);

    if (!context.outerContext || !context.outerContext.response) {
      return true;
    }

    const responseInfo = context.outerContext.response.info || {};
    const additionalInfo: IStorage = {};
    additionalInfo[systemKeyPrefix + 'CardTaskDialogActionResult'] = actionResult.getStorage();

    const processObj = tryGetFromInfo<IStorage | null>(
      responseInfo,
      systemKeyPrefix + 'WorkflowEngineProcessSerializedKey',
      null
    );
    if (processObj) {
      additionalInfo[systemKeyPrefix + 'WorkflowEngineProcessSerializedKey'] = processObj;
    }

    // ???
    // using var uiScope = parentCardEditor is null ? (IDisposable)null : UIContext.Create(parentCardEditor.Context);

    const workflowEngineProcessor = new WorkflowEngineProcessorClient();
    const result = await workflowEngineProcessor.processSignalAsync(
      request!,
      requestSignature,
      additionalInfo,
      request => {
        const requestInfo = request.info;
        requestInfo[systemKeyPrefix + 'WebAdvancedDialogCommandSkipUIContextFlag'] =
          createTypedField(true, DotNetType.Boolean);
      }
    );

    await showNotEmpty(result.validationResult);
    if (result.validationResult.isSuccessful && parentCardEditor) {
      await parentCardEditor.refreshCard(parentCardEditor.context);
    }

    return !!(
      result.validationResult.isSuccessful &&
      result.responseInfo &&
      !tryGetFromInfo(result.responseInfo, `${systemKeyPrefix}KeepTaskDialog`, false)
    );
  }

  private getProcessRequest(info: IStorage) {
    let request: WorkflowEngineProcessStorageRequest | null = null;
    const requestFromInfo = tryGetFromInfo<IStorage>(info, systemKeyPrefix + 'ProcessRequest');
    if (requestFromInfo) {
      request = new WorkflowEngineProcessStorageRequest(requestFromInfo);
    }
    const requestSignature = tryGetFromInfo<string | null>(
      info,
      systemKeyPrefix + 'ProcessRequestSignature',
      null
    );
    return { request, requestSignature };
  }
}
