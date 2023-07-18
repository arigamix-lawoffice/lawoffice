import { CardRow } from 'tessa/cards';
import { CardUIExtension, ICardEditorModel, ICardUIExtensionContext } from 'tessa/ui/cards';
import { DotNetType, Guid } from 'tessa/platform';
import { IUIContext, showNotEmpty, showViewModelDialog } from 'tessa/ui';
import { OcrOperationTypeId } from '../misc/ocrConstants';
import { OcrProgressDialog } from '../components/dialog/ocrProgressDialog';
import { OcrProgressDialogOptions } from '../components/dialog/ocrProgressDialogOptions';
import { OcrProgressDialogViewModel } from '../components/dialog/ocrProgressDialogViewModel';
import { OcrRequestStates } from '../misc/ocrTypes';
import { OperationCheckIntervalMilliseconds } from '../misc/ocrConstants';
import { userSession } from 'common/utility';

/** Расширение, выполняющее отслеживание прогресса операции по распознаванию текста в файле. */
export class OcrMonitoringUIExtension extends CardUIExtension {
  //#region fields

  private _disposer: VoidFunction | null = null;

  //#endregion

  //#region base overrides

  public shouldExecute(context: ICardUIExtensionContext): boolean {
    return Guid.equals(context.card.typeId, OcrOperationTypeId);
  }

  public async contextInitialized(context: ICardUIExtensionContext): Promise<void> {
    const uiContext = context.uiContext;
    const editor = uiContext.cardEditor;
    if (!editor || editor.operationInProgress) {
      return;
    }

    const requests = editor.cardModel?.card.sections.get('OcrRequests')?.rows;
    const executableRequest =
      // находим первый активный запрос на распознавание файла
      requests?.find(r => r.get('StateID') === OcrRequestStates.Active) ??
      // либо, если такого нет, то первый созданный
      requests?.find(r => r.get('StateID') === OcrRequestStates.Created);

    if (executableRequest) {
      // создаем диалог и начинаем отслеживать прогресс
      this.monitoring(uiContext, editor, executableRequest);
    }
  }

  public finalized(_context: ICardUIExtensionContext): void {
    this._disposer?.();
    this._disposer = null;
  }

  //#endregion

  //#region private

  private async monitoring(
    uiContext: IUIContext,
    editor: ICardEditorModel,
    executableRequest: CardRow
  ): Promise<void> {
    // создаем вью-модель для отслеживания прогресса операции распознавания
    const progressViewModel = new OcrProgressDialogViewModel(
      executableRequest.rowId,
      OperationCheckIntervalMilliseconds,
      undefined,
      Guid.equals(executableRequest.get('CreatedByID'), userSession.UserID)
    );
    // запускаем отслеживание операции
    this._disposer = progressViewModel.monitorStart();
    // отображаем диалог пользователю
    const dialogOptionResult = await showViewModelDialog<OcrProgressDialogOptions>(
      progressViewModel,
      OcrProgressDialog
    );
    // пользователь продолжил выполнение операции в фоне, поэтому закрываем карточку диалога
    if (dialogOptionResult === OcrProgressDialogOptions.ContinueInBackground) {
      await editor.close();
    }
    // пользователь выполнил запрос на отмену операции
    else if (dialogOptionResult === OcrProgressDialogOptions.Cancel) {
      executableRequest.set('StateID', OcrRequestStates.Interrupted, DotNetType.Int32);
      await editor.saveCard(uiContext);
    }
    // операция была завершена системой
    else {
      // если была ошибка, то отображаем ее пользователю
      if (progressViewModel.validationResult) {
        await showNotEmpty(progressViewModel.validationResult);
      }
      // выполняем обновление карточки
      await editor.refreshCard(uiContext);
    }
  }

  //#endregion
}
