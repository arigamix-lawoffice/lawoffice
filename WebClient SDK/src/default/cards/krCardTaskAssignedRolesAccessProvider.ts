import {
  CardTaskAssignedRolesAccessProvider,
  ICardModel,
  CardSavingRequest,
  CardSavingMode
} from 'tessa/ui/cards';
import { IUIContext, UIContext, showConfirmWithCancel } from 'tessa/ui';
import { IStorage } from 'tessa/platform/storage';
import { CardStoreMode } from 'tessa/cards';
import { createTypedField, DotNetType } from 'tessa/platform';
import { KrToken } from 'tessa/workflow';

export class KrCardTaskAssignedRolesAccessProvider extends CardTaskAssignedRolesAccessProvider {
  //#region Fields

  private onceReLoaded = false;

  //#endregion

  //#region Private Methods

  public static async reopenMainCardWithMarkAsync(
    mainCardContext: IUIContext,
    mark: string,
    proceedConfirmation?: () => Promise<boolean>,
    proceedAndSaveCardConfirmation?: () => Promise<boolean | null>,
    continuationOnSuccessFunc?: () => Promise<boolean>,
    getInfo?: IStorage
  ): Promise<boolean> {
    const editor = mainCardContext.cardEditor;
    let model: ICardModel;

    if (editor == null || editor.operationInProgress || (model = editor.cardModel!) == null) {
      return true;
    }

    const cardIsNew = model.card.storeMode == CardStoreMode.Insert;
    const hasChanges = cardIsNew || (await model.hasChanges());
    let saveCardBeforeOpening: boolean | null;

    if (hasChanges && proceedAndSaveCardConfirmation != null) {
      saveCardBeforeOpening = await proceedAndSaveCardConfirmation();
    }
    //Если не указана функция подтверждения с вариантом отмены - сохраняем карточку
    //если есть подтверждение основного действия
    else if (proceedConfirmation != null && hasChanges) {
      saveCardBeforeOpening = await proceedConfirmation() ? true : null;
    }
    //Если в карточке не было изменений - не вызываем сохранения
    else if (proceedConfirmation != null) {
      saveCardBeforeOpening = await proceedConfirmation() ? false : null;
    }
    //Если не указана функция подтверждения и нет изменений - вызываем основное действие
    //без подтверждения и сохранения
    else {
      saveCardBeforeOpening = false;
    }

    if (getInfo == null) {
      getInfo = {};
    }

    if (!!mark) {
      getInfo[mark] = createTypedField(true, DotNetType.Boolean);
    }

    if (saveCardBeforeOpening === undefined) {
      return Promise.resolve(false);
    }

    if (saveCardBeforeOpening) {
      const token = KrToken.tryGet(editor.info);
      KrToken.remove(editor.info);

      if (
        !(await editor.saveCard(
          mainCardContext,
          {
            '.SaveWithPermissionsCalc': createTypedField(true, DotNetType.Boolean)
          },
          new CardSavingRequest(CardSavingMode.KeepPreviousCard)
        ))
      ) {
        return Promise.resolve(false);
      }

      token?.setInfo(getInfo);
    }

    const cardId = model.card.id;
    const cardType = model.cardType;

    const sendTaskSucceeded = await editor.openCard({
      cardId,
      cardTypeId: cardType.id!,
      cardTypeName: cardType.name!,
      context: mainCardContext,
      info: getInfo
    });

    if (sendTaskSucceeded) {
      editor.isUpdatedServer = true;
    } else if (cardIsNew || saveCardBeforeOpening) {
      // если карточка новая или была сохранена, а также не удалось выполнить mark-действие при открытии,
      // то у нас будет "висеть" карточка с некорректной версией;
      // её надо обновить, на этот раз без mark'и

      await editor.openCard({
        cardId,
        cardTypeId: cardType.id!,
        cardTypeName: cardType.name!,
        context: mainCardContext
      });
    }

    if (!sendTaskSucceeded || continuationOnSuccessFunc == null) {
      return sendTaskSucceeded;
    }

    const currentContextScope = UIContext.create(mainCardContext);
    try {
      return await continuationOnSuccessFunc();
    } finally {
      if (currentContextScope) {
        currentContextScope.dispose();
      }
    }
  }

  //#endregion

  //#region ICardTaskAssignedRolesAccessProvider Members

  /// <doc path='info[@type="ICardTaskAssignedRolesAccessProvider" and @item="CheckAccessAsync"]'/>
  public async checkAccessAsync(taskRowID: guid, mainCardUIcontext: IUIContext): Promise<boolean> {
    if (await super.checkAccessAsync(taskRowID, mainCardUIcontext)) {
      return true;
    }

    if (!this.onceReLoaded) {
      await KrCardTaskAssignedRolesAccessProvider.reopenMainCardWithMarkAsync(
        mainCardUIcontext,
        'kr_calculate_task_assigned_roles_permissions',
        undefined, //Не требуем подтверждения действия, если не было изменений
        async () => await showConfirmWithCancel('$KrMesages_EditModeConfirmation')
      );
      this.onceReLoaded = true;
    }

    return await super.checkAccessAsync(taskRowID, mainCardUIcontext);
  }

  //#endregion
}
