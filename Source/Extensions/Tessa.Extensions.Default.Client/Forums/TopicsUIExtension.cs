using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared.Workflow.KrPermissions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Forums;
using Tessa.Forums.ForumCached;
using Tessa.Forums.Notifications;
using Tessa.Platform;
using Tessa.Platform.Collections;
using Tessa.UI;
using Tessa.UI.Cards;
using Tessa.UI.Controls.Forums;
using Tessa.UI.Controls.Forums.Controls;
using Tessa.UI.Windows;
using Tessa.Views.Parser.SyntaxTree.Workplace;

namespace Tessa.Extensions.Default.Client.Forums
{
    /// <summary>
    /// Класс для управления вкладкой обсуждения
    /// </summary>
    public sealed class TopicsUIExtension : CardUIExtension
    {
        #region Constructors

        public TopicsUIExtension(
            IForumDialogManager forumDialogManager,
            IDocumentTabManager documentTabManager,
            IWorkplaceInterpreter interpreter)
        {
            this.documentTabManager = documentTabManager;
            this.interpreter = interpreter;
            this.forumDialogManager = forumDialogManager;
        }

        #endregion

        #region Base overrides

        public override async Task Initialized(ICardUIExtensionContext context)
        {
            if (!context.ValidationResult.IsSuccessful())
            {
                return;
            }

            var cardID = context.Card.ID;
            var uiContext = context.UIContext;
            var forms = context.Model.Forms;

            // проверить что карточка входит в типое решине и на ней включено "использовать систему форумов"
            foreach (ForumControlViewModel forumControl in GetAllForumControls(context))
            {
                // обновляем всю модель при выборе вкладки
                if (forumControl.Content is ForumLicenseNotExistViewModel)
                {
                    return;
                }

                if (forumControl.Block.Form.IsCollapsed
                    || !forms.Contains(forumControl.Block.Form))
                {
                    continue;
                }

                if (context.Model.InSpecialMode())
                {
                    forumControl.EnableSpecialMode();
                }

                forumControl.OpenParticipants(
                    (openParticipantsContext, modifyParamAction) => this.OpenParticipantsView(cardID, openParticipantsContext, modifyParamAction));
                forumControl.CheckAddTopicPermission(
                    ct => this.CheckAddTopicPermissionsAsync(forumControl, uiContext, ct));
                forumControl.CheckSuperModeratorPermissionAsync(
                    ct => CheckSuperModeratorPermissionsAsync(uiContext, ct));

                // Если в инфо есть topicID значит мы пришли из OpenTopicOnDoubleClickExtension. и Это ID выбронного топика
                if (context.Model.Card.Info.TryGetValue(ForumHelper.TopicIDKey, out object t) && t is Guid topicID &&
                    context.Model.Card.Info.TryGetValue(ForumHelper.TopicTypeIDKey, out object type) && type is Guid typeID)
                {
                    if (forumControl.ForumViewModel.TopicTypeID == typeID)
                    {
                        IForumData data = forumControl.ForumViewModel.ForumControlDependencies.ForumClientCachedDataManager.GetForumData();
                        await forumControl.SelectTopicAsync(
                            topicID,
                            null,
                            data.ReadTopicStatusList.TryFirst(
                                top => top.TopicID == topicID, out UserStatusModel userStat)
                                ? userStat.LastReadMessageTime
                                : null,
                            cancellationToken: context.CancellationToken);
                        context.Model.Card.Info.Remove(ForumHelper.TopicIDKey);

                        var mainForm = context.Model.MainFormWithTabs;
                        mainForm.SelectedTab = forumControl.Block.Form;
                    }
                }
            }
        }

        #endregion

        #region Fields

        private readonly IForumDialogManager forumDialogManager;
        private readonly IDocumentTabManager documentTabManager;
        private readonly IWorkplaceInterpreter interpreter;

        #endregion

        #region Private

        /// <summary>
        /// Возвращает все топики в алиаcе которого есть название Forum
        /// </summary>
        private static List<ForumControlViewModel> GetAllForumControls(ICardUIExtensionContext context) =>
            context.Model.ControlBag.OfType<ForumControlViewModel>().ToList();


        private async Task CheckAddTopicPermissionsAsync(ForumControlViewModel forumControlViewModel, IUIContext uiContext, CancellationToken cancellationToken = default)
        {
            await OpenMarkedCardAsync(
                uiContext,
                KrPermissionsHelper.CalculateAddTopicPermissions,
                null, //Не требуем подтверждения действия, если не было изменений
                cardIsNew => cardIsNew
                    ? TessaDialog.Confirm("$KrTiles_EditModeConfirmation") ? true : null
                    : TessaDialog.ConfirmWithCancel("$KrTiles_EditModeConfirmation"),
                () => this.AddTopicShowDialogAsync(forumControlViewModel, cancellationToken),
                cancellationToken: cancellationToken);
        }

        private static async Task CheckSuperModeratorPermissionsAsync(IUIContext uiContext, CancellationToken cancellationToken = default)
        {
            await OpenMarkedCardAsync(
                uiContext,
                KrPermissionsHelper.CalculateSuperModeratorPermissions,
                null, //Не требуем подтверждения действия, если не было изменений
                cardIsNew => cardIsNew
                    ? TessaDialog.Confirm("$KrTiles_EditModeConfirmation") ? true : null
                    : TessaDialog.ConfirmWithCancel("$KrTiles_EditModeConfirmation"),
                async () => await SuperModeratorPermissionsMessageAsync(),
                cancellationToken: cancellationToken);
        }

        private async Task<bool> AddTopicShowDialogAsync(ForumControlViewModel forumControlViewModel, CancellationToken cancellationToken = default)
        {
            var card = UIContext.Current.CardEditor?.CardModel?.Card;
            if (card is null)
            {
                return false;
            }

            var token = KrToken.TryGet(card.Info);
            if (token?.HasPermission(KrPermissionFlagDescriptors.AddTopics) == true)
            {
                await this.forumDialogManager.AddTopicShowDialogAsync(card.ID, model => forumControlViewModel.ForumViewModel.ModifyAddingTopic(model), cancellationToken);
                return true;
            }

            TessaDialog.ShowError("$Forum_Permission_NoPermissionToAddTopic");
            return false;
        }

        private static async Task<bool> SuperModeratorPermissionsMessageAsync()
        {
            var card = UIContext.Current.CardEditor?.CardModel?.Card;
            if (card is null)
            {
                return false;
            }

            var token = KrToken.TryGet(card.Info);
            if (token?.HasPermission(KrPermissionFlagDescriptors.SuperModeratorMode) == true)
            {
                TessaDialog.ShowMessage("$Forum_Permission_SuperModeratorModeOn");
                return false;
            }

            TessaDialog.ShowError("$Forum_Permission_NoRequiredPermissions");
            return false;
        }

        private static async Task OpenMarkedCardAsync(IUIContext context,
            string mark,
            Func<bool> proceedConfirmation,
            Func<bool, bool?> proceedAndSaveCardConfirmation,
            Func<Task<bool>> continuationOnSuccessFunc = null,
            Dictionary<string, object> getInfo = null,
            CancellationToken cancellationToken = default)
        {
            ICardEditorModel editor = context.CardEditor;
            ICardModel model;

            if (editor is null || editor.OperationInProgress || (model = editor.CardModel) is null)
            {
                return;
            }

            bool cardIsNew = model.Card.StoreMode == CardStoreMode.Insert;
            bool hasChanges = cardIsNew || await model.HasChangesAsync(cancellationToken: cancellationToken);
            bool? saveCardBeforeOpening;

            if (hasChanges && proceedAndSaveCardConfirmation is not null)
            {
                saveCardBeforeOpening = proceedAndSaveCardConfirmation(cardIsNew);
            }
            //Если не указана функция подтверждения с вариантом отмены - сохраняем карточку
            //если есть подтверждение основного действия
            else if (proceedConfirmation is not null && hasChanges)
            {
                saveCardBeforeOpening = proceedConfirmation() ? true : null;
            }
            //Если в карточке не было изменений - не вызываем сохранения
            else if (proceedConfirmation is not null)
            {
                saveCardBeforeOpening = proceedConfirmation() ? false : null;
            }
            //Если не указана функция подтверждения и нет изменений - вызываем основное действие
            //без подтверждения и сохранения
            else
            {
                saveCardBeforeOpening = false;
            }

            getInfo ??= new Dictionary<string, object>(StringComparer.Ordinal);

            if (!string.IsNullOrWhiteSpace(mark))
            {
                getInfo[mark] = BooleanBoxes.True;
            }

            if (!saveCardBeforeOpening.HasValue)
            {
                return;
            }

            if (saveCardBeforeOpening.Value)
            {
                KrToken token = KrToken.TryGet(editor.Info);
                KrToken.Remove(editor.Info);

                bool res = await editor.SaveCardAsync(
                    context,
                    new Dictionary<string, object>
                    {
                        { KrPermissionsHelper.SaveWithPermissionsCalcFlag, true }
                    },
                    new CardSavingRequest(CardSavingMode.KeepPreviousCard),
                    cancellationToken);
                if (!res)
                {
                    return;
                }

                token?.Set(getInfo);
            }

            Guid cardID = model.Card.ID;
            CardType cardType = model.CardType;

            bool sendTaskSucceeded = await editor.OpenCardAsync(
                cardID,
                cardType.ID,
                cardType.Name,
                context,
                getInfo,
                cancellationToken: cancellationToken);

            if (sendTaskSucceeded)
            {
                editor.IsUpdatedServer = true;
            }
            else if (cardIsNew || saveCardBeforeOpening.Value)
            {
                // если карточка новая или была сохранена, а также не удалось выполнить mark-действие при открытии,
                // то у нас будет "висеть" карточка с некорректной версией;
                // её надо обновить, на этот раз без mark'и

                await editor.OpenCardAsync(
                    cardID,
                    cardType.ID,
                    cardType.Name,
                    context,
                    cancellationToken: cancellationToken);
            }

            if (continuationOnSuccessFunc is null)
            {
                return;
            }

            await using (UIContext.Create(context))
            {
                await continuationOnSuccessFunc();
            }
        }

        private async ValueTask OpenParticipantsView(
            Guid cardID,
            OpenParticipantsContext openParticipantsContext,
            Func<OpenParticipantsContext, ValueTask> modifyParamAction = null)
        {
            if (modifyParamAction is not null)
            {
                await modifyParamAction(openParticipantsContext);
            }

            await TopicParticipantsWorkplaceTab.OpenParticipantsViewTabAsync(
                this.interpreter,
                this.documentTabManager,
                cardID,
                openParticipantsContext);
        }

        #endregion
    }
}
