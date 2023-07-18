#nullable enable
using System.Collections;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared.Cards;
using Tessa.Extensions.Default.Shared.Views;
using Tessa.Platform;
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using Tessa.UI;
using Tessa.UI.Cards;
using Tessa.UI.Cards.Controls;
using Tessa.UI.Files;
using Tessa.UI.Views.Content;
using Tessa.Views;
using Unity;

namespace Tessa.Extensions.Default.Client.UI.CardFiles
{
    /// <summary>
    /// Реализация расширения типа карточки для преобразования представления в файловый контрол
    /// </summary>
    public sealed class InitializeFilesViewUIExtension : FilesViewGeneratorBaseUIExtension
    {
        #region Private fields

        private readonly ICardMetadata cardMetadata;

        private readonly IViewCardControlInitializationStrategy initializationStrategy;

        #endregion

        #region Constructor

        public InitializeFilesViewUIExtension(
            ISession session,
            IExtensionContainer extensionContainer,
            IViewService viewService,
            ICardMetadata cardMetadata,
            IProcessNameResolver processNameResolver,
            [Dependency(nameof(FilesViewCardControlInitializationStrategy))] IViewCardControlInitializationStrategy initializationStrategy)
            : base(session, extensionContainer, viewService, processNameResolver)
        {
            this.cardMetadata = NotNullOrThrow(cardMetadata);
            this.initializationStrategy = NotNullOrThrow(initializationStrategy);
        }

        #endregion

        #region Private methods

        private async Task ExecuteInitializingActionAsync(ITypeExtensionContext context)
        {
            var settings = NotNullOrThrow(context.Settings);
            var filesViewAlias = settings.TryGet<string>(DefaultCardTypeExtensionSettings.FilesViewAlias);

            var options = new FileControlCreationParams
            {
                CategoriesViewAlias = settings.TryGet<string>(DefaultCardTypeExtensionSettings.CategoriesViewAlias),
                PreviewControlName = settings.TryGet<string>(DefaultCardTypeExtensionSettings.PreviewControlName),
                IsCategoriesEnabled = settings.TryGet<bool>(DefaultCardTypeExtensionSettings.IsCategoriesEnabled),
                IsIgnoreExistingCategories =
                    settings.TryGet<bool>(DefaultCardTypeExtensionSettings.IsIgnoreExistingCategories),
                IsManualCategoriesCreationDisabled =
                    settings.TryGet<bool>(DefaultCardTypeExtensionSettings.IsManualCategoriesCreationDisabled),
                IsNullCategoryCreationDisabled =
                    settings.TryGet<bool>(DefaultCardTypeExtensionSettings.IsNullCategoryCreationDisabled),
                CategoriesViewMapping = settings.TryGet<IList>(DefaultCardTypeExtensionSettings.CategoriesViewMapping)
            };
            var uiContext = (ICardUIExtensionContext) NotNullOrThrow(context.ExternalContext);

            if (context.CardTask is null)
            {
                this.AddCardModelInitializers(uiContext.Model, filesViewAlias, options);
            }
            else
            {
                uiContext.Model.TaskInitializers.Add(async (taskCardModel, ct) =>
                {
                    if (taskCardModel.CardTask == context.CardTask)
                    {
                        this.AddCardModelInitializers(taskCardModel, filesViewAlias, options);
                    }
                });
            }
        }

        private async Task ExecuteInitializedActionAsync(ITypeExtensionContext context)
        {
            var uiContext = (ICardUIExtensionContext) NotNullOrThrow(context.ExternalContext);

            if (context.CardTask is null)
            {
                var isAttached = await this.AttachViewToFileControlCoreAsync(
                    context,
                    uiContext.Model,
                    context.CancellationToken);
                if (!isAttached)
                {
                    var tables = uiContext.Model.ControlBag.OfType<GridViewModel>();
                    foreach (var table in tables)
                    {
                        table.RowInvoked += (s, e) =>
                        {
                            if (e.Action is GridRowAction.Inserted or GridRowAction.Opening)
                            {
                                this.InitializeExtensionForTableForm(context, e);
                            }
                        };
                    }
                }
            }
            else
            {
                await uiContext.Model.ModifyTasksAsync(async (task, model) =>
                {
                    if (task.TaskModel.CardTask == context.CardTask)
                    {
                        await task.ModifyWorkspaceAsync(async (t, subscribeToTaskModel) =>
                        {
                            var isAttached = await this.AttachViewToFileControlCoreAsync(
                                context,
                                task.TaskModel,
                                CancellationToken.None);
                            if (!isAttached)
                            {
                                var tables = task.TaskModel.ControlBag.OfType<GridViewModel>();
                                foreach (var table in tables)
                                {
                                    table.RowInvoked += (s, e) =>
                                    {
                                        if (e.Action is GridRowAction.Inserted or GridRowAction.Opening)
                                        {
                                            this.InitializeExtensionForTableForm(context, e);
                                        }
                                    };
                                }
                            }
                        });
                    }
                });
            }
        }

        private void InitializeExtensionForTableForm(ITypeExtensionContext context, RowEventArgs e)
        {
            var cardViewControlViewModels = e.RowModel.ControlBag.OfType<CardViewControlViewModel>();
            if (cardViewControlViewModels.Any())
            {
                DispatcherHelper.InvokeInUI(async () =>
                {
                    var isAttached = await this.AttachViewToFileControlCoreAsync(
                        context,
                        e.RowModel,
                        CancellationToken.None);
                    if (!isAttached)
                    {
                        var tables = e.RowModel.ControlBag.OfType<GridViewModel>();
                        foreach (var table in tables)
                        {
                            table.RowInvoked += (s2, e2) => { this.InitializeExtensionForTableForm(context, e2); };
                        }
                    }
                });
            }
        }

        private async ValueTask<bool> AttachViewToFileControlCoreAsync(
            ITypeExtensionContext extensionContext,
            ICardModel cardModel,
            CancellationToken cancellationToken = default)
        {
            IViewCardControlInitializationStrategy? strategy = null;
            foreach (var handlerAsync in cardModel.TryGetFileViewExtensionInitializationStrategyHandlers()
                     ?? Enumerable.Empty<TryGetControlInitializationStrategyAsync>())
            {
                strategy = await handlerAsync(extensionContext, cardModel, cancellationToken);
                if (strategy is not null)
                {
                    break;
                }
            }

            return await AttachViewToFileControlAsync(
                cardModel,
                extensionContext.Settings,
                CreateRowFunc,
                strategy ?? this.initializationStrategy,
                cancellationToken: cancellationToken);
        }
        
        private static ViewControlRowViewModel CreateRowFunc(TableRowCreationOptions options)
        {
            var fileViewModel = (IFileViewModel) options.Data[ColumnsConst.FileViewModel];
            options.Data.Remove(ColumnsConst.FileViewModel);

            return new TableFileRowViewModel(fileViewModel, options)
            {
                AutomationId = $"{fileViewModel.Model.ID}",
                AutomationName = fileViewModel.Model.Name,
            };
        }

        #endregion

        #region Base overrides

        public override async Task Initializing(ICardUIExtensionContext context)
        {
            ValidationResult result = await CardHelper
                .ExecuteTypeExtensionsAsync(
                    DefaultCardTypeExtensionTypes.InitializeFilesView,
                    context.Card,
                    this.cardMetadata,
                    this.ExecuteInitializingActionAsync,
                    context,
                    cancellationToken: context.CancellationToken);

            context.ValidationResult.Add(result);
        }

        public override async Task Initialized(ICardUIExtensionContext context)
        {
            // Вот здесь проходит основная инициализация. Она не запускается при открытии мелкой карточки.
            ValidationResult result = await CardHelper
                .ExecuteTypeExtensionsAsync(
                    DefaultCardTypeExtensionTypes.InitializeFilesView,
                    context.Card,
                    this.cardMetadata,
                    this.ExecuteInitializedActionAsync,
                    context);

            context.ValidationResult.Add(result);
        }

        #endregion
    }
}
