import {
  RebuildOperationIDKey,
  RebuildMarkKey
} from 'tessa/businessCalendar/businessCalendarHelper';
import { CardStoreRequest, CardService } from 'tessa/cards/service';
import {
  CardUIExtension,
  ICardUIExtensionContext,
  AdvancedCardDialogManager
} from 'tessa/ui/cards';
import {
  ButtonViewModel,
  AutoCompleteEntryViewModel,
  GridViewModel,
  GridRowAction
} from 'tessa/ui/cards/controls';
import { hasFlag, createTypedField, DotNetType } from 'tessa/platform';
import { CardPermissionFlags } from 'tessa/cards';
import {
  showNotEmpty,
  UIContext,
  showMessage,
  showError,
  LoadingOverlay,
  tryGetFromInfo
} from 'tessa/ui';
import { BusinessCalendarService, CalendarSettingsSection } from 'tessa/businessCalendar';
import { OperationService, OperationTypes } from 'tessa/platform/operations';

export class CalendarUIExtension extends CardUIExtension {
  public initialized(context: ICardUIExtensionContext): void {
    CalendarUIExtension.attachCommandToButton(
      context,
      'ValidateCalendar',
      CalendarUIExtension.validateCalendarButtonAction
    );
    CalendarUIExtension.attachCommandToButton(
      context,
      'RebuildCalendar',
      CalendarUIExtension.rebuildCalendarButtonAction
    );

    const calendarTypeControl = context.model.controls.get(
      'CalendarType'
    ) as AutoCompleteEntryViewModel;

    if (!calendarTypeControl) {
      return;
    }

    calendarTypeControl.changeFieldCommand.func = async () =>
      await this.openCalendarTypeInDialogAsync(context);

    // Для всех новых записей в именованных интервалах, которые добавляются с клиента - ставим, что они добавлены вручную.
    const namedRangesControl = context.model.controls.get('NamedRanges') as GridViewModel;
    if (!namedRangesControl) {
      return;
    }

    namedRangesControl.rowInvoked.add(args => {
      if (args.action == GridRowAction.Inserted) {
        args.row.set('IsManual', createTypedField(true, DotNetType.Boolean));
      }
    });
  }

  private static attachCommandToButton(
    context: ICardUIExtensionContext,
    buttonAlias: string,
    action: Function
  ) {
    const control = context.model.controls.get(buttonAlias);
    if (!control) {
      return;
    }

    const button = control as ButtonViewModel;
    if (
      !hasFlag(
        context.model.card.permissions.resolver.getCardPermissions(),
        CardPermissionFlags.AllowModify
      )
    ) {
      button.isReadOnly = true;
      return;
    }

    // tslint:disable-next-line:no-any
    button.onClick = action as any;
  }

  private static async validateCalendarButtonAction() {
    const context = UIContext.current;
    const editor = context.cardEditor;
    const cardID = editor!.cardModel!.card.id;

    const calendarID = editor?.cardModel?.card.sections
      .get(CalendarSettingsSection)
      ?.fields.get('CalendarID') as number | undefined;
    if (calendarID == undefined) {
      await showError('$UI_Common_Messages_CantValidateCalendarWithEmptyID');
      return;
    }

    const result = await BusinessCalendarService.validateCalendar(cardID);

    await showNotEmpty(result, '$UI_BusinessCalendar_ValidatedTitle');
  }

  private static async rebuildCalendarButtonAction() {
    const context = UIContext.current;
    const editor = context.cardEditor;

    if (editor && !editor.operationInProgress) {
      const calendarID = editor?.cardModel?.card.sections
        .get(CalendarSettingsSection)
        ?.fields.get('CalendarID') as number | undefined;
      if (calendarID == undefined) {
        await showError('$UI_Common_Messages_CantCalcCalendarWithEmptyID');
        return;
      }

      const isAlive = await OperationService.instance.isAlive(OperationTypes.CalendarRebuild);

      if (isAlive) {
        await showError('$UI_Common_Messages_CalendarRebuildOperationAlreadyRunning');
        return;
      }

      editor.setOperationInProgress(async () => {
        const storeRequest = new CardStoreRequest();
        if (editor.cardModel) {
          storeRequest.card = editor.cardModel.card.clone();
          storeRequest.info[RebuildMarkKey] = createTypedField(true, DotNetType.Boolean);
        }

        const storeResponse = await CardService.instance.store(storeRequest);
        const storeResult = storeResponse.validationResult.build();

        let operationID: string | null;

        if (
          storeResult.isSuccessful &&
          (operationID = tryGetFromInfo(storeResponse.info, RebuildOperationIDKey, null))
        ) {
          do {
            await new Promise(resolve => setTimeout(resolve, 500));
          } while (await OperationService.instance.isAlive(operationID));
        }

        await showNotEmpty(storeResult);
        await editor.refreshCard(editor.context);
        await showMessage('$UI_BusinessCalendar_CalendarIsRebuiltNotification');
      });
    }
  }

  private async openCalendarTypeInDialogAsync(context: ICardUIExtensionContext) {
    let calendarTypeID: guid;
    const settingsSection = context.card.sections.tryGet('CalendarSettings')!;
    if (!settingsSection || !(calendarTypeID = settingsSection.fields.tryGet('CalendarTypeID'))) {
      return;
    }

    const calendarTypeName: string = settingsSection.fields.get('CalendarTypeCaption');

    await LoadingOverlay.instance.show(async splashResolve => {
      await AdvancedCardDialogManager.instance.openCard({
        cardId: calendarTypeID,
        displayValue: calendarTypeName,
        context: context.uiContext,
        splashResolve,
        cardEditorModifierAction: async () => {
          const uiContext = UIContext.current;

          uiContext.cardEditor?.closed.add(async () => {
            const typeCard = uiContext.cardEditor!.cardModel!.card;
            if (uiContext.cardEditor?.isUpdatedServer) {
              const typeSection = typeCard.sections.get('CalendarTypes')!;

              settingsSection.fields.set(
                'CalendarTypeCaption',
                typeSection.fields.getField('Caption')!
              );
              settingsSection.fields.set(
                'CalendarTypeID',
                createTypedField(calendarTypeID, DotNetType.Guid)
              );
            }
          });
        }
      });
    });
  }
}
