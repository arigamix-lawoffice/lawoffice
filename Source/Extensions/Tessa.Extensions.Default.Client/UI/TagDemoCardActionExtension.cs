#nullable enable

using System;
using System.Linq;
using System.Threading.Tasks;
using Tessa.Tags;
using Tessa.Platform.Runtime;
using Tessa.UI;
using Tessa.UI.Cards;
using Tessa.UI.Tags;
using Tessa.UI.Tags.ViewModels;
using Tessa.Localization;
using Tessa.Cards;

namespace Tessa.Extensions.Default.Client.UI
{
    /// <summary>
    /// Пример UI-расширения для работы тегов в карточке.
    /// </summary>
    public sealed class TagDemoCardActionExtension :
        CardUIExtension
    {
        #region Constructors

        public TagDemoCardActionExtension(
            IUIHost uiHost,
            IAdvancedCardDialogManager cardDialogManager,
            CreateDialogFormFuncAsync createDialogFormFuncAsync,
            ITagManager tagManager,
            ITagInfoModelFactory tagInfoModelFactory,
            ISession session)
        {
            this.uiHost = NotNullOrThrow(uiHost);
            this.cardDialogManager = NotNullOrThrow(cardDialogManager);
            this.createDialogFormFuncAsync = NotNullOrThrow(createDialogFormFuncAsync);
            this.tagManager = NotNullOrThrow(tagManager);
            this.tagInfoModelFactory = NotNullOrThrow(tagInfoModelFactory);
            this.session = NotNullOrThrow(session);
        }

        #endregion

        #region Fields

        private readonly IUIHost uiHost;
        private readonly IAdvancedCardDialogManager cardDialogManager;
        private readonly CreateDialogFormFuncAsync createDialogFormFuncAsync;
        private readonly ITagManager tagManager;
        private readonly ITagInfoModelFactory tagInfoModelFactory;
        private readonly ISession session;

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override async Task Initialized(ICardUIExtensionContext context)
        {
            if (context.Card is Card card &&
                context.UIContext.CardEditor is ICardEditorModel cardEditor)
            {
                cardEditor.Toolbar.CanHaveTags = true;
                cardEditor.Toolbar.TagsModel.Tags.Clear();
                var allTagsInfo = await this.tagManager.GetTagsAsync(context.Card.ID, context.ValidationResult, context.CancellationToken);

                if (!context.ValidationResult.IsSuccessful())
                {
                    return;
                }

                if (allTagsInfo is null)
                {
                    cardEditor.Toolbar.CanHaveTags = false;
                    return;
                }

                cardEditor.Toolbar.CanHaveTags = true;
                cardEditor.Toolbar.TagsModel.Tags.Clear();
                foreach (var tagInfo in allTagsInfo)
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

                        await this.tagManager.AddTagAsync(tag, null, context.ValidationResult);
                        if (context.ValidationResult.IsSuccessful())
                        {
                            this.AddTagToToolbar(cardEditor.CardModel.Card, cardEditor, tagInfo);
                        }
                    });
                }
            }
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
