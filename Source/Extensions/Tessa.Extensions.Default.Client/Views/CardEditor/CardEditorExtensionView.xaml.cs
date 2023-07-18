using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Tessa.Cards;
using Tessa.Localization;
using Tessa.Platform;
using Tessa.Platform.Validation;
using Tessa.UI;
using Tessa.UI.Cards;
using Tessa.UI.Controls;
using Tessa.UI.Views;
using Tessa.UI.Views.Content;
using Tessa.Views;
using Tessa.Views.Metadata;

namespace Tessa.Extensions.Default.Client.Views.CardEditor
{
    public partial class CardEditorExtensionView :
        IContentItem,
        IWeakEventListener
    {
        #region Constants

        private readonly static Thickness TopPanelShownContentMargin = new Thickness(0, 40, 0, 0);
        private readonly static Thickness TopPanelHiddenContentMargin = new Thickness(0, 0, 0, 0);

        #endregion

        #region Dependency Properties

        public static readonly DependencyProperty TopPanelVisibilityProperty = DependencyProperty.Register(
            "TopPanelVisibility",
            typeof(Visibility),
            typeof(CardEditorExtensionView),
            new PropertyMetadata(null));

        public static readonly DependencyProperty ContentMarginProperty = DependencyProperty.Register(
            "ContentMargin",
            typeof(Thickness),
            typeof(CardEditorExtensionView),
            new PropertyMetadata(null));

        #endregion

        #region Static Properties

        public static RoutedCommand ShowErrorCommand { get; }

        public static RoutedCommand ShowAppropriateCardCommand { get; }

        #endregion

        #region Static Methods

        private static async void OnExecutedShowError(object sender, ExecutedRoutedEventArgs e)
        {
            await ((CardEditorExtensionView) sender).ShowErrorAsync();
        }

        private static async void OnExecutedShowAppropriateCard(object sender, ExecutedRoutedEventArgs e)
        {
            await ((CardEditorExtensionView) sender).ShowAppropriateCardAsync();
        }

        #endregion

        #region Fields

        private Func<ICardEditorModel> createEditorFunc;

        private ValidationResult saveCardError;

        private readonly IWorkplaceViewComponent component;

        private readonly IUIContext uiContext;

        private ICardEditorModel editor;

        private ClosedCardViewModel closedCardViewModel = new ClosedCardViewModel();

        #endregion

        #region Constructors

        static CardEditorExtensionView()
        {
            ShowErrorCommand = new RoutedCommand(
                nameof(ShowErrorCommand),
                typeof(CardEditorExtensionView));

            ShowAppropriateCardCommand = new RoutedCommand(
                nameof(ShowAppropriateCardCommand),
                typeof(CardEditorExtensionView));

            CommandManager.RegisterClassCommandBinding(typeof(CardEditorExtensionView),
                new CommandBinding(ShowErrorCommand, OnExecutedShowError));

            CommandManager.RegisterClassCommandBinding(typeof(CardEditorExtensionView),
                new CommandBinding(ShowAppropriateCardCommand, OnExecutedShowAppropriateCard));
        }

        public CardEditorExtensionView(
            IWorkplaceViewComponent component,
            Func<ICardEditorModel> createEditorFunc)
        {
            this.InitializeComponent();
            this.component = component;
            this.uiContext = new UIContext(component);
            this.createEditorFunc = createEditorFunc;

            this.component.CanUnload += async (sender, e) =>
            {
                if (this.editor?.CardModel?.Card is null
                    || !this.editor.CardModel.Card.HasChanges())
                {
                    return;
                }

                var deferral = e.Defer();
                try
                {
                    var eventArgs = new DeferredCancelEventArgs();
                    var dialogResult = await CardUIHelper.OnWorkspaceClosingAsync(this.editor, eventArgs, null);
                    if (dialogResult)
                    {
                        var result = await this.editor.SaveCardWithResultAsync(this.editor.Context);
                        if (!result.IsSuccessful)
                        {
                            e.Cancel = true;
                            return;
                        }
                    }

                    if (eventArgs.Cancel)
                    {
                        e.Cancel = true;
                    }
                }
                catch (Exception ex)
                {
                    deferral.SetException(ex);
                }
                finally
                {
                    deferral.Dispose();
                }
            };

            this.component.Unloading += async (sender, e) =>
            {
                var deferral = e.Defer();
                try
                {
                    if (this.editor is { } editorToClose)
                    {
                        this.editor = null;
                        editorToClose.PropertyChanged -= IsUpdatedServer_PropertyChanged;
                        await editorToClose.OperationCompleted;
                        await editorToClose.CloseAsync(force: true);
                    }
                }
                catch (Exception ex)
                {
                    deferral.SetException(ex);
                }
                finally
                {
                    deferral.Dispose();
                }
            };

            PropertyChangedEventManager.AddHandler(this.component, this.OnComponentIsDataLoadingChanged, nameof(IWorkplaceViewComponent.IsDataLoading));
            SelectionStateChangedEventManager.AddListener(this.component.Selection, this);
        }

        #endregion

        #region Public Methods

        public async Task ShowErrorAsync()
        {
            await TessaDialog.ShowNotEmptyAsync(this.saveCardError, logResult: false);
        }

        public async Task ShowAppropriateCardAsync()
        {
            await this.editor.OperationCompleted;
            var hasChanges = await this.editor.CardModel.HasChangesAsync();
            if (!hasChanges || hasChanges && await TessaDialog.ConfirmAsync("$UI_Misc_UnsavedDataWillBeLost"))
            {
                await this.UpdateCardAsync();
                this.saveCardError = null;
                this.HideTopPanel();
            }
        }

        #endregion

        #region Private Methods

        private void ShowTopPanel()
        {
            DispatcherHelper.InvokeInUI(() =>
            {
                this.SetValue(TopPanelVisibilityProperty, Visibility.Visible);
                this.SetValue(ContentMarginProperty, TopPanelShownContentMargin);
            });
        }

        private void HideTopPanel()
        {
            DispatcherHelper.InvokeInUI(() =>
            {
                this.SetValue(TopPanelVisibilityProperty, Visibility.Collapsed);
                this.SetValue(ContentMarginProperty, TopPanelHiddenContentMargin);
            });
        }

        private async void Editor_Closed(object sender, DeferredEventArgs e)
        {
            if (sender is ICardEditorModel editor)
            {
                DelegateCommand reopenCommand =
                    editor.LastOperationType != CardEditorOperationType.Delete ? new DelegateCommand(this.OpenCardEditorHandlerAsync) : null;
                var closedCardEditorModel = new ClosedCardViewModel() { ReopenCommand = reopenCommand };
                this.DataContext = closedCardEditorModel;
            }
        }

        private async void OpenCardEditorHandlerAsync(object _)
        {
            await this.OpenCardEditorAsync();
        }

        private async Task OpenCardEditorAsync()
        {
            this.editor = createEditorFunc();
            this.editor.Info[CardUIHelper.ToolbarHiddenActions] = new[]
                { TileNames.SaveAndCloseCard, TileNames.Cancel };
            this.editor.Closed += Editor_Closed;
            this.editor.DialogName = CardUIHelper.DefaultDialogName;
            this.DataContext = this.editor;
            await this.UpdateCardAsync();
        }

        private async void OnComponentIsDataLoadingChanged(object sender, PropertyChangedEventArgs e)
        {
            this.HideTopPanel();
            if (this.editor is null)
            {
                await this.OpenCardEditorAsync();
            }
            else
            {
                // Апдейт компонента будет выполняться после того, как загружены данные.
                if (component.IsDataLoading)
                {
                    return;
                }

                if (this.editor.CardModel is not null &&
                    await this.editor.CardModel.HasChangesAsync())
                {
                    await editor.OperationCompleted;
                    await DispatcherHelper.InvokeInUIAsync(async () =>
                    {
                        if (await CardUIHelper.OnWorkspaceClosingNoCancelAsync(editor, new DeferredCancelEventArgs(), null))
                        {
                            var result = await this.editor.SaveCardWithResultAsync(this.editor.Context);
                            if (!result.IsSuccessful)
                            {
                                this.saveCardError = result;
                                this.ShowTopPanel();
                                this.editor.IsUpdatedServer = false;
                                this.editor.PropertyChanged += IsUpdatedServer_PropertyChanged;
                            }
                            else
                            {
                                await this.UpdateCardAsync();
                                this.saveCardError = null;
                            }
                        }
                        else
                        {
                            await this.UpdateCardAsync();
                        }
                    });
                }
                else
                {
                    await this.UpdateCardAsync();
                }
            }
        }

        private async void IsUpdatedServer_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(this.editor.IsUpdatedServer) && this.editor.IsUpdatedServer)
            {
                this.HideTopPanel();
                await this.UpdateCardAsync();
                this.saveCardError = null;
            }
        }

        private async Task UpdateCardAsync()
        {
            this.editor.PropertyChanged -= IsUpdatedServer_PropertyChanged;

            if (this.component.Data is null || this.component.IsDataLoading)
            {
                return;
            }

            var view = this.component.View;
            IDictionary<string, object> row = this.component.SelectedRow ?? this.component.Data?.FirstOrDefault();
            if (view is null || row is null)
            {
                this.editor = null;
                this.DataContext = this.closedCardViewModel;
                return;
            }

            var viewMetadata = await view.GetMetadataAsync();

            IViewReferenceMetadata reference = viewMetadata.References.FirstOrDefault(x => x.IsCard && x.OpenOnDoubleClick);

            if (reference is null)
            {
                return;
            }

            object idValue = row.GetCardID(reference);

            if (idValue is null)
            {
                return;
            }

            Guid? cardId = null;


            switch (idValue)
            {
                case Guid g:
                    cardId = g;
                    break;

                case string s:
                    if (Guid.TryParse(s, out Guid id))
                    {
                        cardId = id;
                    }

                    break;
            }

            if (!cardId.HasValue)
            {
                await this.editor.SetCardModelAsync(null);
                this.editor.Toolbar.Actions.Clear();
                this.editor.BottomToolbar.Actions.Clear();
                return;
            }

            await this.editor.OpenCardAsync(cardId.Value, null, null, this.uiContext, cardModelModifierActionAsync: async (context) =>
            {
                if (context.CardModel?.MainForm is IFormWithTabsViewModel formWithTabsViewModel)
                {
                    formWithTabsViewModel.TabsAreCollapsed = true;
                }
            });
        }

        #endregion

        #region Properties

        /// <summary>
        /// Функция, возвращаемая в методе <see cref="GetTemplate"/>.
        /// </summary>
        public Func<IPlaceArea, DataTemplate> DataTemplateFunc { get; init; } // = null

        #endregion

        #region IContentItem Members

        /// <inheritdoc />
        public IEnumerable<IPlaceArea> PlaceAreas { get; init; } = ContentPlaceAreas.ContentPlaces;

        /// <inheritdoc />
        public int Order { get; init; } // = 0

        /// <inheritdoc />
        public DataTemplate GetTemplate(IPlaceArea area) => this.DataTemplateFunc?.Invoke(area);

        #endregion

        #region IWeakEventListener Members

        /// <inheritdoc />
        public bool ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
        {
            if (managerType == typeof(SelectionStateChangedEventManager))
            {
                _ = this.UpdateCardAsync();
                return true;
            }

            return false;
        }

        #endregion
    }
}
