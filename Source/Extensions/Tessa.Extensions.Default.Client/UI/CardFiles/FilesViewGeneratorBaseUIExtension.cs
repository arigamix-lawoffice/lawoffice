using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared.Cards;
using Tessa.Extensions.Default.Shared.Views;
using Tessa.Files;
using Tessa.Localization;
using Tessa.Platform;
using Tessa.Platform.Collections;
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;
using Tessa.Scheme;
using Tessa.UI;
using Tessa.UI.Cards;
using Tessa.UI.Cards.Controls;
using Tessa.UI.Controls;
using Tessa.UI.Files;
using Tessa.UI.Files.Controls;
using Tessa.UI.Menu;
using Tessa.UI.Views.Content;
using Tessa.Views;
using Tessa.Views.Metadata;
using Tessa.Views.Metadata.Criteria;

namespace Tessa.Extensions.Default.Client.UI.CardFiles
{
    /// <summary>
    /// Базовый класс UI расширения для обработки расширений типа карточки "Список файлов в представлении".
    /// </summary>
    public abstract class FilesViewGeneratorBaseUIExtension : CardUIExtension
    {
        #region Constructors

        /// <summary>
        /// Создаёт новый экземпляр класса с указанием его зависимостей.
        /// </summary>
        /// <param name="session"><inheritdoc cref="ISession" path="/summary"/></param>
        /// <param name="extensionContainer"><inheritdoc cref="IExtensionContainer" path="/summary"/></param>
        /// <param name="viewService"><inheritdoc cref="IViewService" path="/summary"/></param>
        /// <param name="processNameResolver">Объект, выполняющий получение имён процессов, блокирующих добавление файла.</param>
        protected FilesViewGeneratorBaseUIExtension(
            ISession session,
            IExtensionContainer extensionContainer,
            IViewService viewService,
            IProcessNameResolver processNameResolver)
        {
            this.Session = session ?? throw new ArgumentNullException(nameof(session));
            this.ExtensionContainer = extensionContainer ?? throw new ArgumentNullException(nameof(extensionContainer));
            this.ViewService = viewService ?? throw new ArgumentNullException(nameof(viewService));
            this.ProcessNameResolver = processNameResolver ?? throw new ArgumentNullException(nameof(processNameResolver));
        }

        #endregion

        #region Static Helpers

        /// <summary>
        /// Метод для получения контрола файлов для контрола представление из <paramref name="info"/> по имени контрола.
        /// </summary>
        /// <param name="info">Объект с дополнительной информацией.</param>
        /// <param name="viewControlName">Имя контрола представление.</param>
        /// <returns>Возвращает файловый контрол для контрола представление или null, если контрол с заданны именем отсутствует в <paramref name="info"/>.</returns>
        public static ViewFileControl TryGetFileControl(ISerializableObject info, string viewControlName)
        {
            return info.TryGetValue(viewControlName, out var o)
                ? o as ViewFileControl
                : null;
        }

        #endregion

        #region Protected Properties

        ///<inheritdoc cref="IExtensionContainer"/>
        protected IExtensionContainer ExtensionContainer { get; }

        ///<inheritdoc cref="ISession"/>
        protected ISession Session { get; }

        ///<inheritdoc cref="IViewService"/>
        protected IViewService ViewService { get; }

        ///<inheritdoc cref="IProcessNameResolver"/>
        protected IProcessNameResolver ProcessNameResolver { get; }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Создает скрытый FileControl, через который представление
        /// будет взаимодействовать с файловым API. Для каждого алиаса представления
        /// должен быть создан свой файлконтрол. Создание происходит в CardUIExtension.Initializing.
        /// </summary>
        /// <param name="cardModel">Модель карточки.</param>
        /// <param name="viewControlName">Алиас контрола представления, которое будет адаптировано под отображение файлов.</param>
        /// <param name="creationParams">Параметры файлового контрола.</param>
        protected void AddCardModelInitializers(
            ICardModel cardModel,
            string viewControlName,
            FileControlCreationParams creationParams)
        {
            cardModel.ControlInitializers.Add(async (control, cm, r, ct) =>
            {
                if (control is CardViewControlViewModel viewControl)
                {
                    if (viewControl.Name != viewControlName)
                    {
                        return;
                    }

                    if (FormCreationContext.Current.FileControls.Any(x => x.Name == viewControl.Name))
                    {
                        TessaDialog.ShowError($"Multiple FileViewControlViewModel with Name='{viewControl.Name}' was found on the form.");
                        return;
                    }

                    var categoriesView = await this.ViewService.GetByNameAsync(creationParams.CategoriesViewAlias, ct);
                    if (categoriesView is null)
                    {
                        TessaDialog.ShowError(
                            $"Categories View:'{creationParams.CategoriesViewAlias}' isn't found'");
                        return;
                    }

                    var cardMetadata = cm.GeneralMetadata;
                    var fileContainer = cm.FileContainer;
                    var fileTypes =
                        CardHelper.GetCardFileTypes(
                            await CardHelper.GetFileCardTypesAsync(
                                cardMetadata,
                                this.Session.User.IsAdministrator(),
                                ct));


                    var fileControl = new ViewFileControl(
                        viewControl,
                        fileContainer,
                        this.ExtensionContainer,
                        cm.MenuContext,
                        fileTypes,
                        creationParams.IsCategoriesEnabled,
                        creationParams.IsManualCategoriesCreationDisabled,
                        creationParams.IsNullCategoryCreationDisabled,
                        false,
                        creationParams.IsIgnoreExistingCategories,
                        this.Session,
                        this.ProcessNameResolver,
                        previewControlName: creationParams.PreviewControlName,
                        name: viewControl.Name)
                    {
                        CategoryFilterAsync = async (context) =>
                        {
                            ITessaViewResult result = null;
                            IViewMetadata categoriesViewMetadata = await categoriesView.GetMetadataAsync(context.CancellationToken);

                            var request = new TessaViewRequest(categoriesViewMetadata);

                            var parameters =
                                await ViewMappingHelper.AddRequestParametersAsync(
                                    creationParams.CategoriesViewMapping,
                                    cardModel,
                                    this.Session,
                                    categoriesView,
                                    cancellationToken: context.CancellationToken);
                            if (parameters is not null)
                            {
                                request.Values.AddRange(parameters);
                            }

                            await cm.ExecuteInContextAsync(
                                async (c, ct3) => { result = await categoriesView.GetDataAsync(request, ct3).ConfigureAwait(false); },
                                context.CancellationToken).ConfigureAwait(false);

                            var rows = result?.Rows
                                ?? Array.Empty<object>();

                            // категории из представления в порядке, в котором их вернуло представление (кроме строчек null)
                            var viewCategories = rows
                                .Cast<IList<object>>()
                                .Where(x => x.Count > 0)
                                .Select(x => (IFileCategory) new FileCategory((Guid) x[0], (string) x[1]))
                                .ToArray();

                            // категории из представления плюс вручную добавленные или другие присутствующие в карточке категории, кроме null
                            var mainCategories = viewCategories
                                .Union(context.Categories)
                                .Where(x => x is not null)
                                .ToArray();

                            // добавляем наверх "Без категории" и возвращаем результирующий список
                            return new List<IFileCategory> { null }
                                .Union(mainCategories);
                        }
                    };

                    await fileControl.InitializeAsync(fileContainer.Files, cancellationToken: ct);

                    cm.Info[viewControl.Name] = fileControl;
                    FormCreationContext.Current.Register(fileControl);

                    control.Unloaded += async (s, e) =>
                    {
                        var deferral = e.Defer();
                        try
                        {
                            fileControl.StopTimer();
                            await fileControl.UnloadAsync(e.ValidationResult);
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
                }
            });
        }


        /// <summary>
        /// Связывает представление с файловым API через FileControl, созданный в InitializeFileControlAsync.
        /// </summary>
        /// <param name="cardModel">Модель карточки.</param>
        /// <param name="settings">Настройки расширения.</param>
        /// <param name="createRowFunc">Функция, создающая строку представления.</param>
        /// <param name="initializationStrategy">Стратегия инициализации представления.</param>
        /// <param name="viewModifierAction">Функция модификации контрола представления, например, задания дефолного столбца для сортировки.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Файловый контрол <see cref="ViewFileControl"/>.</returns>
        protected static async ValueTask<bool> AttachViewToFileControlAsync(
            ICardModel cardModel,
            ISerializableObject settings,
            Func<TableRowCreationOptions, ViewControlRowViewModel> createRowFunc,
            IViewCardControlInitializationStrategy initializationStrategy = null,
            Action<CardViewControlViewModel> viewModifierAction = null,
            CancellationToken cancellationToken = default)
        {
            var viewControlName = settings.TryGet<string>(DefaultCardTypeExtensionSettings.FilesViewAlias);

            if (!cardModel.Controls.TryGet(viewControlName, out var controlViewModel))
            {
                return false;
            }

            if (controlViewModel is not CardViewControlViewModel viewControlViewModel)
            {
                return false;
            }

            viewControlViewModel.CreateRowFunc = createRowFunc;
            var fileControl = TryGetFileControl(cardModel.Info, viewControlViewModel.Name);
            if (fileControl is null)
            {
                return false;
            }

            InitializeContextMenu(viewControlViewModel, fileControl);
            if (initializationStrategy is not null)
            {
                await viewControlViewModel.InitializeStrategyAsync(initializationStrategy, true, cancellationToken);
                viewModifierAction?.Invoke(viewControlViewModel);
                await viewControlViewModel.InitializeOnTabAsync();
            }

            AttachToFileControl(viewControlViewModel, fileControl);
            InitializeGrouping(viewControlViewModel, fileControl);
            InitializeFiltering(viewControlViewModel, fileControl);
            InitializeDragDrop(viewControlViewModel, cardModel);
            InitializeClickCommands(viewControlViewModel, fileControl);
            InitializeMenuButton(viewControlViewModel, fileControl);
            InitializeKeyDownHandlers(viewControlViewModel, fileControl);

            var defaultGroup = settings.TryGet<string>(DefaultCardTypeExtensionSettings.DefaultGroup);
            if (!string.IsNullOrEmpty(defaultGroup))
            {
                await fileControl.SelectGroupingAsync(fileControl.Groupings.TryGet(defaultGroup), cancellationToken);
            }
            return true;
        }

        #endregion

        #region Initialization

        /// <summary>
        /// Привязывает измнение коллекции файлов к представлению.
        /// </summary>
        /// <param name="viewModel">ViewModel контрола представления.</param>
        /// <param name="fileControl">ViewModel скрытого контрола с файлами.</param>
        private static void AttachToFileControl(CardViewControlViewModel viewModel, IFileControl fileControl)
        {
            fileControl.Items.CollectionChanged += (sender, e) =>
            {
                viewModel.DelayedViewRefresh.RunAfterDelay(150);
            };
        }

        /// <summary>
        /// Добавляет Drag&amp;Drop.
        /// </summary>
        /// <param name="viewModel">ViewModel контрола представления.</param>
        /// <param name="cardModel">Модель карточки.</param>
        private static void InitializeDragDrop(CardViewControlViewModel viewModel, ICardModel cardModel)
        {
            viewModel.AllowDrop = true;
            viewModel.DragDrop = new FilesDragDrop(cardModel, viewModel.Name);
        }

        /// <summary>
        /// Синхронизация группировки в файловом контроле и предсталвении.
        /// </summary>
        /// <param name="viewModel">ViewModel контрола представления.</param>
        /// <param name="fileControl">Контрол файлов.</param>
        private static void InitializeGrouping(CardViewControlViewModel viewModel, IFileControl fileControl)
        {
            foreach (var column in viewModel.Table.Columns.Cast<TableColumnViewModel>())
            {
                column.ContextMenuGenerators.Clear();
            }
            var groupCaptionColumn = viewModel.Table.Columns.Cast<TableColumnViewModel>().FirstOrDefault(x => x.ColumnName == ColumnsConst.GroupCaption);
            if (groupCaptionColumn is not null)
            {
                groupCaptionColumn.Visibility = false;
            }

            fileControl.PropertyChanged += async (o, e) =>
            {
                if (e.PropertyName == nameof(FileControl.SelectedGrouping))
                {
                    if (fileControl.SelectedGrouping is null)
                    {
                        viewModel.Table.GroupingColumn = null;
                        var categoryColumn = viewModel.Table.Columns.Cast<TableColumnViewModel>().First(x => x.ColumnName == ColumnsConst.CategoryCaption);
                        categoryColumn.Visibility = true;
                    }
                    else
                    {
                        if (fileControl.SelectedGrouping.Name == "Category")
                        {
                            var categoryColumn = viewModel.Table.Columns.Cast<TableColumnViewModel>().First(x => x.ColumnName == ColumnsConst.CategoryCaption);
                            categoryColumn.Visibility = false;
                        }
                        viewModel.Table.GroupingColumn = null;
                        viewModel.Table.GroupingColumn = viewModel.Table.Columns.Cast<TableColumnViewModel>().FirstOrDefault(x => x.ColumnName == ColumnsConst.GroupCaption).Metadata;
                    }

                    var groupCaptionColumnValue = viewModel.Table.Columns.Cast<TableColumnViewModel>().FirstOrDefault(x => x.ColumnName == ColumnsConst.GroupCaption);
                    if (groupCaptionColumnValue is not null)
                    {
                        groupCaptionColumnValue.Visibility = false;
                    }
                }
            };
        }

        /// <summary>
        /// Синхронизация фильтрации в файловом контроле и предсталвении.
        /// </summary>
        /// <param name="viewModel">ViewModel контрола представления.</param>
        /// <param name="fileControl">Контрол файлов.</param>
        private static void InitializeFiltering(CardViewControlViewModel viewModel, IFileControl fileControl)
        {
            fileControl.PropertyChanged += async (o, e) =>
            {
                // во время обновления представления фильтрация может быть сброшена в дата провайдере. Это событие обрабатывать не нужно.
                if (viewModel.IsDataLoading)
                {
                    return;
                }
                if (e.PropertyName == nameof(IFileControl.SelectedFiltering))
                {
                    var previousFilteringParameter = viewModel.Parameters.FirstOrDefault(x => x.Metadata.Alias == ColumnsConst.FilterParameter);
                    if (previousFilteringParameter is not null)
                    {
                        viewModel.Parameters.Remove(previousFilteringParameter);
                    }
                    if (fileControl.SelectedFiltering is FileGroupingFiltering filter)
                    {
                        var parameterMetadata = new ViewParameterMetadata
                        {
                            Caption = filter.Grouping.Caption,
                            Alias = ColumnsConst.FilterParameter,
                            SchemeType = SchemeType.String
                        };
                        var newFilteringParameter = new RequestParameterBuilder()
                                    .WithMetadata(parameterMetadata)
                                    .AddCriteria(new EqualsCriteriaOperator(), filter.Caption, filter.Caption)
                                    .AsRequestParameter();
                        viewModel.Parameters.Add(newFilteringParameter);
                    }

                    await viewModel.RefreshAsync();
                }
            };
        }

        /// <summary>
        /// Инициализирует обработчики нажатий на клавиатуру.
        /// </summary>
        /// <param name="viewModel">ViewModel контрола представления.</param>
        /// <param name="fileControl">ViewModel скрытого контрола с файлами.</param>
        private static void InitializeKeyDownHandlers(CardViewControlViewModel viewModel, IFileControl fileControl)
        {
            viewModel.KeyDownHandlers.Add(async (item, data, e) =>
            {
                if (data is TableFileRowViewModel row)
                {
                    if (row.GridViewModel.SelectedItems.Count == 1 &&
                        row.GridViewModel.SelectedItem == row)
                    {
                        if (e.Key == Key.Enter && Keyboard.Modifiers.HasFlag(ModifierKeys.Shift))
                        {
                            var file = row.FileViewModel.Model;
                            if (file is null)
                            {
                                TessaDialog.ShowMessage(string.Format(LocalizationManager.Localize("$UI_Common_FileNotFound"), ""));

                                return;
                            }

                            await FileControlHelper.OpenAsync(fileControl, new[] { file }, FileOpeningMode.ForEdit);
                        }
                    }
                }
            });
        }

        /// <summary>
        /// Биндинг событий, возникающих при нажатии кнопок мыши и выборе строки.
        /// </summary>
        /// <param name="viewModel">ViewModel контрола представления.</param>
        /// <param name="fileControl">ViewModel скрытого контрола с файлами.</param>
        private static void InitializeClickCommands(CardViewControlViewModel viewModel, ViewFileControl fileControl)
        {
            viewModel.Table.RowUnselected += async (o, e) =>
            {
                if (e.Row is null)
                {
                    return;
                }

                var gridViewModel = e.Row.GridViewModel;
                var row = (TableFileRowViewModel) e.Row;
                var fileViewModel = row.FileViewModel;
                var file = fileViewModel.Model.Versions.Last.File;

                //<- Обработка случая, когда строка выбрана, а файл еще прогружается.
                if (e.Row.IsSelected == fileViewModel.IsSelected)
                {
                    return;
                }

                if (fileControl.Manager.IsPreviewInProgress())
                {
                    fileViewModel.IsSelected = true;
                    e.Row.IsSelected = true;
                    return;
                }
                //->

                fileViewModel.IsSelected = false;

                // следующий код асинхронный, использовать аргументы "e" нельзя

                if (file.IsLocal && fileControl.Manager.IsInPreview(file.Content.GetLocalFilePath()))
                {
                    await fileControl.Manager.ResetPreviewAsync();
                }
                else if (gridViewModel.SelectedItems.Count == 1)
                {
                    await fileControl.Manager.ResetPreviewAsync();
                }

                if (Keyboard.Modifiers.Has(ModifierKeys.Control) || Keyboard.Modifiers.Has(ModifierKeys.Shift))
                {
                    var presenter = new FileControlPresenter(fileControl);
                    await presenter.ShowSelectedFilesMessageAsync(fileControl.SelectedItems.ToArray());
                }
            };

            viewModel.Table.RowSelected += async (o, e) =>
            {
                if (e.Row is null)
                {
                    return;
                }

                var gridViewModel = e.Row.GridViewModel;
                var row = (TableFileRowViewModel) e.Row;
                var fileViewModel = row.FileViewModel;

                //<- Обработка случая, когда строка выбрана, а файл еще прогружается.
                if (e.Row.IsSelected == fileViewModel.IsSelected)
                {
                    return;
                }

                if (fileControl.Manager.IsPreviewInProgress())
                {
                    fileViewModel.IsSelected = false;
                    e.Row.IsSelected = false;
                    return;
                }

                //->
                fileViewModel.IsSelected = true;

                // следующий код асинхронный, использовать аргументы "e" нельзя

                // если мы перевели фокус на это представление
                if (gridViewModel.SelectedItems.Count == 1)
                {
                    await fileControl.Manager.ClearSelectionAsync(fileControl);
                }

                if (Keyboard.Modifiers.HasNot(ModifierKeys.Control) && Keyboard.Modifiers.HasNot(ModifierKeys.Shift))
                {
                    fileControl.BeginShowPreview(fileViewModel);
                }

                if (Keyboard.Modifiers.Has(ModifierKeys.Control) || Keyboard.Modifiers.Has(ModifierKeys.Shift))
                {
                    var presenter = new FileControlPresenter(fileControl);
                    await presenter.ShowSelectedFilesMessageAsync(fileControl.SelectedItems.ToArray());
                }
            };

            viewModel.LeftButtonClickCommand = new DelegateCommand(async (o) =>
            {
                var clickInfo = (IViewClickInfo) o;
                var row = (TableFileRowViewModel) clickInfo.Row;
                var fileViewModel = row.FileViewModel;
                if (fileControl.Manager.IsPreviewInProgress())
                {
                    clickInfo.EventArgs.Handled = true;
                    return;
                }

                if (Keyboard.Modifiers.HasNot(ModifierKeys.Control) && Keyboard.Modifiers.HasNot(ModifierKeys.Shift) && row.IsSelected)
                {
                    fileControl.BeginShowPreview(fileViewModel);
                }
            });

            viewModel.DoubleClickCommand =
                new DelegateCommand(async o =>
                {
                    var clickInfo = (IViewClickInfo) o;
                    var row = (TableFileRowViewModel) clickInfo.Row;
                    var selectedFileID = row.FileID;
                    var file = fileControl.Files.FirstOrDefault(f => f.ID == selectedFileID);
                    if (file is null)
                    {
                        await TessaDialog.ShowMessageAsync(string.Format(LocalizationManager.Localize("$UI_Common_FileNotFound"), ""));
                        return;
                    }

                    if (Keyboard.Modifiers.HasFlag(ModifierKeys.Alt))
                    {
                        await FileControlHelper.OpenAsync(fileControl, new[] { file }, FileOpeningMode.ForEdit);
                    }
                    else
                    {
                        await FileControlHelper.OpenAsync(fileControl, new[] { file }, FileOpeningMode.ForRead);
                    }
                });

            viewModel.RightButtonClickCommand = new DelegateCommand(async o =>
            {
                var clickInfo = (IViewClickInfo) o;
                clickInfo.EventArgs.Handled = true;
                var presenter = new FileControlPresenter(fileControl);
                var row = (TableFileRowViewModel) clickInfo.Row;
                var fileID = row.FileID;
                var file = fileControl.Items.FirstOrDefault(f => f.Model.ID == fileID);
                await presenter.ShowFileMenuAsync(file, clickInfo.frameworkElement).ConfigureAwait(false);
            });
        }

        /// <summary>
        /// Создаем контекстное меню контрола.
        /// </summary>
        /// <param name="viewModel">ViewModel контрола представления.</param>
        /// <param name="fileControl">ViewModel скрытого контрола с файлами.</param>
        private static void InitializeContextMenu(CardViewControlViewModel viewModel, IFileControl fileControl)
        {
            viewModel.ContextMenuGenerators.Add(async context =>
            {
                (IMenuActionCollection actions, _, _) = await fileControl.GenerateControlMenuAsync();
                var toRemoveActions = new[] { FileMenuActionNames.Sortings };
                context.MenuActions.AddRange(actions.Where(x => !toRemoveActions.Contains(x.Name)));
            });
        }

        /// <summary>
        /// Добавление кнопки Меню в контрол представления.
        /// </summary>
        /// <param name="viewModel"> ViewModel контрола представления.</param>
        /// <param name="fileControl">ViewModel скрытого контрола с файлами.</param>
        private static void InitializeMenuButton(CardViewControlViewModel viewModel, IFileControl fileControl)
        {
            var refreshButtonIndex = viewModel.TopItems.Items.IndexOf(i => i is RefreshButton);
            if (refreshButtonIndex != -1)
            {
                viewModel.TopItems.Items.RemoveAt(refreshButtonIndex);
            }

            var addMenuButton = new ShowContextMenuButtonViewModel { FileControl = fileControl, ViewModel = viewModel };
            viewModel.TopItems.Items.Insert(0, addMenuButton);
        }

        #endregion

        #region Drag&Drop

        ///<inheritdoc/>
        private sealed class FilesDragDrop : DefaultDragDrop
        {
            private readonly ICardModel cardModel;
            private readonly string fileControlName;

            public FilesDragDrop(
                ICardModel cardModel,
                string fileControlName)
            {
                this.cardModel = cardModel ?? throw new ArgumentNullException(nameof(cardModel));
                this.fileControlName = fileControlName;
            }

            /// <inheritdoc />
            public override bool CanDrag => true;

            /// <inheritdoc />
            public override async ValueTask<object> StartDragAsync(object sender, MouseEventArgs e, CancellationToken cancellationToken = default)
            {
                var fileControl = TryGetFileControl(this.cardModel.Info, this.fileControlName);

                // когда drop реально начался, но предпросмотр занят загрузкой файла, то мы не запускаем drag&drop
                if (fileControl is null
                    || fileControl.Manager.IsPreviewInProgress(childrenOnly: true))
                {
                    return null;
                }

                fileControl.StopTimer();
                var operationFiles = fileControl.SelectedFiles.ToArray<IFileObject>();
                if (!await FileControlHelper.CheckCanDownloadFilesAndShowMessagesAsync(operationFiles))
                {
                    return null;
                }

                // При перетаскивании файлов записываем результаты валидации загрузки файлов в лог, но не выводим их пользователю.
                var (pathList, result) = await FileControlHelper.DownloadContentAsync(operationFiles, fileControl, cancellationToken);
                TessaLoggers.Validation.LogResultItems(result);
                if (pathList is null || pathList.Count == 0)
                {
                    return null;
                }

                // мы получили контент файлов и не упали - выполняем drag&drop
                var dataObj = new DataObject(DataFormats.FileDrop, pathList.ToArray());
                dataObj.SetData("DragSource", fileControl);

                return dataObj;
            }

            /// <inheritdoc />
            public override void OnMouseDown(object sender, MouseButtonEventArgs e)
            {
                if (e.ChangedButton == MouseButton.Left
                    && e.ClickCount == 2)
                {
                    var fileControl = TryGetFileControl(this.cardModel.Info, this.fileControlName);
                    fileControl?.StopTimer();
                }
            }

            /// <inheritdoc />
            public override async void OnDrop(object sender, DragEventArgs e)
            {
                var fileControl = TryGetFileControl(this.cardModel.Info, this.fileControlName);
                fileControl?.DropAction(sender, e);
            }

            /// <inheritdoc />
            public override async void DragOver(object sender, DragEventArgs e)
            {
                var fileControl = TryGetFileControl(this.cardModel.Info, this.fileControlName);
                fileControl?.DragOverAction(sender, e);
            }
        }

        #endregion
    }
}
