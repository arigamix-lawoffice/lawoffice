#nullable enable

using System;
using System.Linq;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Localization;
using Tessa.Platform.Runtime;
using Tessa.Platform.Validation;
using Tessa.Tags;
using Tessa.UI;
using Tessa.UI.Cards;
using Tessa.UI.Tags;
using Tessa.UI.Tags.ViewModels;

namespace Tessa.Extensions.Default.Client.Tags
{
    /// <summary>
    /// Расширение на UI карточки, которое включает метки в карточке, если в <see cref="Card.Info"/> передаётся информация о метках карточки в объекте <see cref="TagsForCard"/>.
    /// </summary>
    public sealed class EnableTagsUIExtension : CardUIExtension
    {
        #region Fields

        private readonly IUIHost uiHost;
        private readonly IAdvancedCardDialogManager cardDialogManager;
        private readonly CreateDialogFormFuncAsync createDialogFormFuncAsync;
        private readonly ITagManager tagManager;
        private readonly ITagPermissionsTokenProvider clientTagPermissionsManager;
        private readonly ITagInfoModelFactory tagInfoModelFactory;
        private readonly ISession session;
        private readonly IUserSettings userSettings;

        #endregion

        #region Constructors

        public EnableTagsUIExtension(
            IUIHost uiHost,
            IAdvancedCardDialogManager cardDialogManager,
            CreateDialogFormFuncAsync createDialogFormFuncAsync,
            ITagManager tagManager,
            ITagPermissionsTokenProvider clientTagPermissionsManager,
            ITagInfoModelFactory tagInfoModelFactory,
            ISession session,
            IUserSettings userSettings)
        {
            this.uiHost = NotNullOrThrow(uiHost);
            this.cardDialogManager = NotNullOrThrow(cardDialogManager);
            this.createDialogFormFuncAsync = NotNullOrThrow(createDialogFormFuncAsync);
            this.tagManager = NotNullOrThrow(tagManager);
            this.clientTagPermissionsManager = NotNullOrThrow(clientTagPermissionsManager);
            this.tagInfoModelFactory = NotNullOrThrow(tagInfoModelFactory);
            this.session = NotNullOrThrow(session);
            this.userSettings = NotNullOrThrow(userSettings);
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override Task Initialized(ICardUIExtensionContext context)
        {
            if (context.Card is Card card && 
                context.UIContext.CardEditor is ICardEditorModel cardEditor)
            {
                /// В доп. информации карточки проверяем наличие ключа "TagsHelper.TagsDisabledKey".
                /// Далее удаляем ключ при его наличии, а для карточки запрещаем ставить метки.
                if (card.Info.TryGetValue(TagsHelper.TagsDisabledKey, out object? tagsDisabledObj))
                {
                    card.Info.Remove(TagsHelper.TagsDisabledKey);
                    if (tagsDisabledObj is bool tagsDisabled && tagsDisabled)
                    {
                        cardEditor.Toolbar.CanHaveTags = false;
                        return Task.CompletedTask;
                    }
                }

                var tagsForCard = TagsForCard.TryUnpack(context.Card.Info);

                if (tagsForCard is null)
                {
                    cardEditor.Toolbar.CanHaveTags = false;
                    return Task.CompletedTask;
                }

                cardEditor.Toolbar.CanHaveTags = true;
                cardEditor.Toolbar.TagsModel.Tags.Clear();
                cardEditor.Toolbar.TagsModel.MaxTagsDisplayed = this.userSettings.MaxTagsDisplayed;
                foreach (var tagInfo in tagsForCard.Tags)
                {
                    this.AddTagToToolbar(context.Card, cardEditor, tagInfo);
                }

                if (cardEditor.Toolbar.RightTagControl is AddTagButtonViewModel addTagButtonViewModel)
                {
                    addTagButtonViewModel.AddTagCommand = new DelegateCommand(async _ =>
                    {

                        if (cardEditor is null
                            || cardEditor.CardModel is null)
                        {
                            return;
                        }
                        var cardID = cardEditor.CardModel.Card.ID;

                        // Если в карточке были изменения, необходимо сохранить ее перед добавлением тега. 

                        if (cardEditor.CardModel.Card.StoreMode == CardStoreMode.Insert || 
                            await cardEditor.CardModel.HasChangesAsync())
                        {
                            if (await TessaDialog.ConfirmAsync(await LocalizationManager.GetStringAsync("Tags_SaveCardToAddTag")))
                            {
                                var saveResult = await cardEditor.CardModel.SaveAsync();
                                if (!saveResult.Success)
                                {
                                    return;
                                }
                            }
                            else
                            {
                                return;
                            }
                        }

                        var tagInfo = await TagUIExtensions.SelectOrCreateTagAsync(
                            this.createDialogFormFuncAsync,
                            this.uiHost,
                            this.cardDialogManager,
                            cardID);

                        if (!(tagInfo is not null &&
                            !cardEditor.Toolbar.TagsModel.Tags.Any(x => x.Id == tagInfo.ID && !x.IsDeleted)))
                        {
                            return;
                        }

                        var tag = new Tag()
                        {
                            SetAt = DateTime.UtcNow,
                            CardID = context.Card.ID,
                            TagID = tagInfo.ID,
                            UserID = this.session.User.ID
                        };
                        var tokenInfo = this.clientTagPermissionsManager.TryGetTagPermissionToken(context.Card);
                        var result = new ValidationResultBuilder();
                        await this.tagManager.AddTagAsync(tag, tokenInfo, result);
                        TessaDialog.ShowNotEmpty(result);
                        if (result.IsSuccessful())
                        {
                            this.AddTagToToolbar(cardEditor.CardModel.Card, cardEditor, tagInfo);
                        }
                    });
                }
            }
            return Task.CompletedTask;
        }

        #endregion

        #region Private Methods

        private void AddTagToToolbar(Card card, ICardEditorModel cardEditor, TagInfo tag)
        {
            // Проверяем, не был ли тег удален перед этим.
            // Если был, то восстанавливаем его, а не добавляем новый.
            var deletedTag = cardEditor.Toolbar.TagsModel.Tags.FirstOrDefault(x => x.Id == tag.ID);
            if (deletedTag is not null)
            {
                deletedTag.IsDeleted = false;
            }
            else
            {
                var tagInfoModel = this.tagInfoModelFactory.CreateModel(tag, card);
                var viewTagInfoModel = new CardTagViewModel(tagInfoModel);
                cardEditor.Toolbar.TagsModel.Tags.Add(viewTagInfoModel);
            }
        }

        #endregion
    }
}
