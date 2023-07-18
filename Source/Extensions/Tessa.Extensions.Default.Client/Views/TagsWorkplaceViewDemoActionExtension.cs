#nullable enable

using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Tessa.Localization;
using Tessa.Platform.Collections;
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using Tessa.Tags;
using Tessa.UI;
using Tessa.UI.Cards;
using Tessa.UI.Menu;
using Tessa.UI.Tags;
using Tessa.UI.Tags.ViewModels;
using Tessa.UI.Views;
using Tessa.UI.Views.Content;
using Tessa.Views.Metadata;
using Tessa.Views.Workplaces;

namespace Tessa.Extensions.Default.Client.Views
{
    /// <summary>
    /// Пример расширения элемента рабочего места, которое добавляет кнопку для добавления тега к карточке из представления и пункт для добавление тега в контекстное меню для строки.
    /// </summary>
    public class TagsWorkplaceViewDemoActionExtension :
        IWorkplaceViewComponentExtension
    {
        #region Constants

        private const string Icon = "Thin20";

        #endregion

        #region Constructors

        public TagsWorkplaceViewDemoActionExtension(
            IUIHost uiHost,
            IAdvancedCardDialogManager advancedCardDialogManager,
            IIconContainer iconContainer,
            CreateDialogFormFuncAsync createDialogFormFuncAsync,
            ITagManager tagManager,
            ITagInfoModelFactory tagInfoModelFactory,
            ISession session)
        {
            this.uiHost = NotNullOrThrow(uiHost);
            this.advancedCardDialogManager = NotNullOrThrow(advancedCardDialogManager);
            this.iconContainer = NotNullOrThrow(iconContainer);
            this.createDialogFormFuncAsync = NotNullOrThrow(createDialogFormFuncAsync);
            this.tagManager = NotNullOrThrow(tagManager);
            this.tagInfoModelFactory = NotNullOrThrow(tagInfoModelFactory);
            this.session = NotNullOrThrow(session);
        }

        #endregion

        #region Fields

        private readonly IUIHost uiHost;

        private readonly IAdvancedCardDialogManager advancedCardDialogManager;

        private readonly IIconContainer iconContainer;

        private readonly CreateDialogFormFuncAsync createDialogFormFuncAsync;

        private readonly ITagManager tagManager;

        private readonly ITagInfoModelFactory tagInfoModelFactory;

        private readonly ISession session;

        private ColumnSettings? columnSettings;

        private TagsWorkplaceViewDemoActionExtensionViewModel? buttonModel;

        private IWorkplaceViewComponent? component;

        #endregion

        #region IWorkplaceViewComponentExtension Implementation

        /// <inheritdoc/>
        public void Clone(IWorkplaceViewComponent source, IWorkplaceViewComponent cloned, ICloneableContext context)
        {
        }

        /// <inheritdoc/>
        public void Initialize(IWorkplaceViewComponent model)
        {
            model.ContentFactories[nameof(TagsWorkplaceViewDemoActionExtension)] = (component) =>
            {
                this.buttonModel = new TagsWorkplaceViewDemoActionExtensionViewModel(
                    this.AddTagsAsync,
                    new IconViewModel("Thin20", this.iconContainer),
                    ContentPlaceAreas.ToolbarPlaces);
                this.component = component;
                this.component.Selection.SelectionChanged += this.ComponentSelectionChanged;
                this.component.PropertyChanged += this.ComponentPropertyChanged;
                this.columnSettings = ColumnSettings.GetOrCreate(this.component.DataNodeMetadata,
                    this.component.DataNodeMetadata.ScopeName);
                return this.buttonModel;
            };
            model.ContextMenuGenerators.Add(this.GetAddTagMenuAction());
        }

        /// <inheritdoc/>
        public void Initialized(IWorkplaceViewComponent model)
        {
        }

        #endregion

        #region Private Methods

        private async Task AddTagsAsync()
        {
            if (!this.canAddTags ||
                this.component is not IWorkplaceViewComponent component ||
                this.columnSettings is not ColumnSettings columnSettings)
            {
                return;
            }
            var viewMetadata = await component.View.GetMetadataAsync();
            var tagsPosition = columnSettings.GetTagsPosition() ?? viewMetadata.TagsPosition;

            if (tagsPosition == TagsPosition.None)
            {
                return;
            }
            var tableView = component.Content.OfType<TableView>().FirstOrDefault();

            if (tableView?.Grid is TableGridViewModel tableGridViewModel
                && viewMetadata.References.TryFirst(x => x.IsCard, out var reference)
                && viewMetadata.Columns.FindByName(reference.ColPrefix + "ID") is IViewColumnMetadata refColumn)
            {
                var rows = tableGridViewModel.SelectedItems.ToArray();
                var tagInfo = await TagUIExtensions.SelectOrCreateTagAsync(
                    this.createDialogFormFuncAsync,
                    this.uiHost,
                    this.advancedCardDialogManager,
                    rows.Length == 1 && rows[0].Data.TryGet<Guid?>(refColumn.Alias) is Guid cardIDFilter
                        ? cardIDFilter
                        : null,
                    default);

                if (tagInfo is null)
                {
                    return;
                }

                foreach (var row in rows)
                {
                    if (!row.Data.TryGetValue(refColumn.Alias, out var cardIDObj)
                        || cardIDObj is not Guid cardID)
                    {
                        continue;
                    }

                    var result = new ValidationResultBuilder();

                    await this.tagManager.AddTagAsync(
                        new Tag()
                        {
                            SetAt = DateTime.UtcNow,
                            CardID = cardID,
                            TagID = tagInfo.ID,
                            UserID = this.session.User.ID
                        },
                        null,
                        result);

                    await TessaDialog.ShowNotEmptyAsync(result);

                    if (!result.IsSuccessful())
                    {
                        return;
                    }

                    TagsViewModel? tagsViewModel = null;
                    switch (tagsPosition)
                    {
                        case TagsPosition.InColumn:
                            var cell = row.CellsByColumnName[TagsHelper.ColumnName];
                            if (cell.Column.ContainsTags)
                            {
                                tagsViewModel = cell.TagsModel;
                            }
                            break;
                        case TagsPosition.Top:
                            tagsViewModel = row.TopContent as TagsViewModel;
                            break;
                        case TagsPosition.Bottom:
                            tagsViewModel = row.BottomContent as TagsViewModel;
                            break;
                    }
                    if (tagsViewModel is null)
                    {
                        return;
                    }

                    // Проверяем, не был ли тег удален перед этим.
                    // Если был, то восстанавливаем его, а не добавляем новый.
                    var deletedTag = tagsViewModel.Tags.FirstOrDefault(x => x.Id == tagInfo.ID);
                    if (deletedTag is not null)
                    {
                        deletedTag.IsDeleted = false;
                    }
                    else
                    {
                        var tagModel = this.tagInfoModelFactory.CreateModel(
                            tagInfo, 
                            cardID
                        );
                        tagModel.AssociatedObject = row;
                        tagsViewModel.Tags.Add(new CardTagViewModel(tagModel));
                    }
                }
            }
        }

        private void ComponentPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (this.component is not null && this.component.SelectedRows.Any())
            {
                this.CanAddTags = true;
            }
            else
            {
                this.CanAddTags = false;
            }
        }

        private void ComponentSelectionChanged(object? sender, SelectionStateEventArgs e)
        {
            if (this.component is not null && this.component.SelectedRows.Any())
            {
                this.CanAddTags = true;
            }
            else
            {
                this.CanAddTags = false;
            }
        }

        private Func<ViewContextMenuContext, ValueTask> GetAddTagMenuAction()
        {
            return async c =>
            {
                c.MenuActions.Add(
                    new MenuAction(
                        "AddTag",
                        LocalizationManager.Localize("$Tags_ContextMenu_Add"),
                        c.MenuContext.Icons.Get("Thin20"),
                        new DelegateCommand(
                            async o =>
                            await c.MenuContext.UIContextExecutorAsync(async (ctx, ct) =>
                                await this.AddTagsAsync()))));
            };
        }

        #endregion

        #region Properties

        private bool canAddTags;

        public bool CanAddTags
        {
            get => this.canAddTags;
            set
            {
                if (this.canAddTags != value)
                {
                    this.canAddTags = value;
                    this.buttonModel!.IsEnabled = value;
                }
            }
        }

        #endregion
    }
}
