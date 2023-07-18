import {
  CardSavingMode,
  CardSavingRequest,
  CardUIExtension,
  ICardModel,
  ICardUIExtensionContext
} from 'tessa/ui/cards';
import { ForumActionParameters, ForumDialogManager, ForumViewModel } from 'tessa/ui/cards/controls';
import {
  IUIContext,
  showConfirm,
  showConfirmWithCancel,
  showError,
  showMessage,
  tryGetFromInfo,
  UIContext
} from 'tessa/ui';
import { CardStoreMode } from 'tessa/cards';
import { createTypedField, DotNetType } from 'tessa/platform';
import { AddTopics, KrToken, SuperModeratorMode } from 'tessa/workflow';
import { IStorage } from 'tessa/platform/storage';
import { equalsCriteriaOperator, ViewParameterMetadata } from 'tessa/views/metadata';
import { ForumHelper } from 'tessa/forums';
import { RequestParameterBuilder } from 'tessa/views';
import { showView } from 'tessa/ui/uiHost';
import { DefaultFormMainViewModel } from 'tessa/ui/cards/forms';

export class TopicsUIExtension extends CardUIExtension {
  //#region fields

  private _dispose: (() => void) | null = null;

  //#endregion

  //#region CardUIExtension

  public async initialized(context: ICardUIExtensionContext): Promise<void> {
    if (!context.validationResult.isSuccessful) {
      return;
    }

    const allForumControls = context.model.controlsBag.filter(
      x => x instanceof ForumViewModel
    ) as ForumViewModel[];
    for (const forumControl of allForumControls) {
      if (!forumControl.isLicenseValid) {
        continue;
      }

      if (forumControl.block.form.isCollapsed) {
        continue;
      }

      if (context.model.inSpecialMode) {
        forumControl.isAddTopicEnabled = false;
        forumControl.isEnabledForumEmptyContextMenu = true;
      }

      forumControl.openParticipantsAction = this.getOpenParticipantsAction(context);

      forumControl.checkAddTopicPermissionAction = this.getCheckAddTopicPermissionsAction(
        context,
        forumControl
      );

      forumControl.checkSuperModeratorPermissionAction =
        this.getCheckSuperModeratorPermissionAction(context);

      const topicId = tryGetFromInfo<guid>(context.model.card.info, ForumHelper.TopicIDKey);
      const topicTypeId = tryGetFromInfo<guid>(context.model.card.info, ForumHelper.TopicTypeIDKey);
      if (topicId && topicTypeId && forumControl.topicTypeId === topicTypeId) {
        await forumControl.showTopic(topicId);
        delete context.model.card.info[ForumHelper.TopicIDKey];
        delete context.model.card.info[ForumHelper.TopicTypeIDKey];
        const mainFrom = context.model.mainForm as DefaultFormMainViewModel;
        mainFrom.selectedTab = forumControl.block.form;
        return;
      }
    }
  }

  public finalized(): void {
    if (this._dispose) {
      this._dispose();
      this._dispose = null;
    }
  }

  //#endregion

  //#region actions

  private getOpenParticipantsAction(context: ICardUIExtensionContext) {
    const openParticipantsAction: (
      params: ForumActionParameters,
      modifyOpenParticipantsAction: (params: ForumActionParameters) => void
    ) => Promise<void> = async (params, modifyOpenParticipants): Promise<void> => {
      modifyOpenParticipants && modifyOpenParticipants(params);

      const paramTopicMeta = new ViewParameterMetadata();
      paramTopicMeta.alias = 'TopicID';

      const paramTopicId = new RequestParameterBuilder()
        .withMetadata(paramTopicMeta)
        .addCriteria(equalsCriteriaOperator(), 'topicID', params.topicID)
        .asRequestParameter();

      const paramCardMeta = new ViewParameterMetadata();
      paramCardMeta.alias = 'CardID';
      const paramCardId = new RequestParameterBuilder()
        .withMetadata(paramCardMeta)
        .addCriteria(equalsCriteriaOperator(), 'cardID', context.card.id)
        .asRequestParameter();

      const paramParticipantMeta = new ViewParameterMetadata();
      paramParticipantMeta.alias = 'ParticipantTypeID';
      const paramParticipantType = new RequestParameterBuilder()
        .withMetadata(paramParticipantMeta)
        .addCriteria(equalsCriteriaOperator(), 'participantType', params.participantsTypeID)
        .asRequestParameter();

      await showView({
        viewAlias: 'TopicParticipants',
        displayValue: '$Workplaces_User_TopicParticipants',
        parameters: [paramTopicId, paramCardId, paramParticipantType],
        treeVisible: false
      });
    };

    return openParticipantsAction;
  }

  private getCheckAddTopicPermissionsAction(
    context: ICardUIExtensionContext,
    control: ForumViewModel
  ) {
    return async (): Promise<void> => {
      await TopicsUIExtension.openMarkedCard(
        context.uiContext,
        'kr_calculate_addtopic_permissions',
        null, // Не требуем подтверждения действия, если не было изменений
        async cardIsNew =>
          cardIsNew
            ? (await showConfirm('$KrTiles_EditModeConfirmation'))
              ? true
              : null
            : await showConfirmWithCancel('$KrTiles_EditModeConfirmation'),
        async () => await this.addTopicShowDialog(context, control)
      );
    };
  }

  private getCheckSuperModeratorPermissionAction(context: ICardUIExtensionContext) {
    return async (): Promise<void> => {
      await TopicsUIExtension.openMarkedCard(
        context.uiContext,
        'kr_calculate_supermoderator_permissions',
        null, // Не требуем подтверждения действия, если не было изменений
        async cardIsNew =>
          cardIsNew
            ? (await showConfirm('$KrTiles_EditModeConfirmation'))
              ? true
              : null
            : await showConfirmWithCancel('$KrTiles_EditModeConfirmation'),
        async () => await this.superModeratorPermissionsMessage()
      );
    };
  }

  private addTopicShowDialog = async (
    context: ICardUIExtensionContext,
    control: ForumViewModel
  ) => {
    const card = UIContext.current.cardEditor?.cardModel?.card;
    if (!card) {
      return false;
    }

    const token = KrToken.tryGet(card.info);
    if (token?.hasPermission(AddTopics) == true) {
      await ForumDialogManager.instance.addTopicShowDialog(context.card.id, model =>
        control.modifyAddingTopicAction(model)
      );
      return true;
    } else {
      await showError('$Forum_Permission_NoPermissionToAddTopic');
      return false;
    }
  };

  private superModeratorPermissionsMessage = async () => {
    const card = UIContext.current.cardEditor?.cardModel?.card;
    if (!card) {
      return false;
    }

    const token = KrToken.tryGet(card.info);
    if (token?.hasPermission(SuperModeratorMode) === true) {
      await showMessage('$Forum_Permission_SuperModeratorModeOn');
      return false;
    }

    await showError('$Forum_Permission_NoRequiredPermissions');
    return false;
  };

  private static async openMarkedCard(
    context: IUIContext,
    mark: string | null,
    proceedConfirmation: (() => Promise<boolean>) | null,
    proceedAndSaveCardConfirmation: ((cardIsNew: boolean) => Promise<boolean | null>) | null,
    continuationOnSuccessFunc: (() => Promise<boolean>) | null = null,
    getInfo: IStorage | null = null
  ): Promise<void> {
    const editor = context.cardEditor;
    let model: ICardModel;

    if (!editor || editor.operationInProgress || !(model = editor.cardModel!)) {
      return;
    }

    const cardIsNew = model.card.storeMode === CardStoreMode.Insert;
    const hasChanges = cardIsNew || (await model.hasChanges());
    let saveCardBeforeOpening: boolean | null;

    if (hasChanges && proceedAndSaveCardConfirmation) {
      saveCardBeforeOpening = await proceedAndSaveCardConfirmation(cardIsNew);
      // Если не указана функция подтверждения с вариантом отмены - сохраняем карточку
      // если есть подтверждение основного действия
    } else if (hasChanges && proceedConfirmation) {
      saveCardBeforeOpening = (await proceedConfirmation()) ? true : null;
      // Если в карточке не было изменений - не вызываем сохранения
    } else if (proceedConfirmation) {
      saveCardBeforeOpening = (await proceedConfirmation()) ? false : null;
      // Если не указана функция подтверждения и нет изменений - вызываем основное действие
      // без подтверждения и сохранения
    } else {
      saveCardBeforeOpening = false;
    }

    if (saveCardBeforeOpening === null) {
      return;
    }

    if (!getInfo) {
      getInfo = {};
    }

    if (mark) {
      getInfo[mark] = createTypedField(true, DotNetType.Boolean);
    }

    if (saveCardBeforeOpening) {
      const token = KrToken.tryGet(editor.info);
      KrToken.remove(editor.info);

      const saveSuccess = await editor.saveCard(
        context,
        {
          '.SaveWithPermissionsCalc': createTypedField(true, DotNetType.Boolean)
        },
        new CardSavingRequest(CardSavingMode.KeepPreviousCard)
      );

      if (!saveSuccess) {
        return;
      }

      if (token) {
        token.setInfo(getInfo);
      }
    }

    const cardId = model.card.id;
    const cardType = model.cardType;

    const sendTaskSucceeded = await editor.openCard({
      cardId,
      cardTypeId: cardType.id!,
      cardTypeName: cardType.name!,
      context,
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
        context
      });
    }

    if (!continuationOnSuccessFunc) {
      return;
    }

    const contextInstance = UIContext.create(context);
    try {
      await continuationOnSuccessFunc();
    } finally {
      contextInstance.dispose();
    }
  }

  //#endregion
}
