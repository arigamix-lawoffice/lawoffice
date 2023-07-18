using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Tessa.Cards;
using Tessa.Extensions.Default.Client.UI.CardFiles;
using Tessa.Extensions.Default.Shared.Cards;
using Tessa.Extensions.Platform.Client.UI.TableViewExtension;
using Tessa.FileConverters;
using Tessa.Files;
using Tessa.Platform.Collections;
using Tessa.Platform.IO;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using Tessa.UI;
using Tessa.UI.Cards;
using Tessa.UI.Cards.Controls;
using Tessa.UI.Cards.Controls.AutoComplete;
using Tessa.UI.Cards.Tasks;
using Tessa.UI.Cards.Types.VirtualScheme;
using Tessa.UI.Controls.AutoCompleteCtrl;
using Tessa.UI.Controls.Helpers;
using Tessa.UI.Controls.TessaGrid;
using Tessa.UI.Files;
using Tessa.UI.Files.Controls;
using Tessa.UI.Menu;
using Tessa.UI.Notifications;
using Tessa.UI.Views.Content;
using Unity;

namespace Tessa.Extensions.Default.Client.UI
{
    public sealed class CarUIExtension :
        CardUIExtension
    {
        #region Constructors

        public CarUIExtension(
            ICardRepository cardRepository,
            ICardTypeService cardTypeService,
            IUIHost uiHost,
            IUnityContainer unityContainer,
            INotificationUIManager notificationUIManager,
            IAdvancedCardDialogManager cardDialogManager,
            CreateDialogFormFuncAsync createDialogFormFuncAsync,
            AutoCompleteDialogProvider autoCompleteDialogProvider,
            CustomFilesViewCardControlInitializationStrategy customInitializationStrategy)
        {
            this.cardRepository = cardRepository;
            this.cardTypeService = cardTypeService;
            this.uiHost = uiHost;
            this.unityContainer = unityContainer;
            this.notificationUIManager = notificationUIManager;
            this.cardDialogManager = cardDialogManager;
            this.createDialogFormFuncAsync = createDialogFormFuncAsync;
            this.autoCompleteDialogProvider = autoCompleteDialogProvider;
            this.customInitializationStrategy = customInitializationStrategy;
        }

        #endregion

        #region Fields

        private readonly ICardRepository cardRepository;
        private readonly ICardTypeService cardTypeService;
        private readonly IUIHost uiHost;
        private readonly IUnityContainer unityContainer;
        private readonly INotificationUIManager notificationUIManager;
        private readonly IAdvancedCardDialogManager cardDialogManager;
        private readonly CreateDialogFormFuncAsync createDialogFormFuncAsync;
        private readonly AutoCompleteDialogProvider autoCompleteDialogProvider;
        private readonly IViewCardControlInitializationStrategy customInitializationStrategy;

        /// <summary>
        /// Список расширений файлов, из которых можно выполнить преобразование в PDF,
        /// но для которых недоступен встроенный предпросмотр средствами системы <see cref="FileControlHelper.PreviewTypesByExtensions"/>.
        ///
        /// Каждое расширение указано с ведущей точкой и в нижнем регистре.
        /// </summary>
        private readonly HashSet<string> pdfConverterExtensions =
            new HashSet<string>(
                FileConverterFormat.GetRecommendedInputFormats(FileConverterFormat.Pdf)
                    .Select(x => "." + x.ToLowerInvariant())
                    .Except(FileControlHelper.PreviewTypesByExtensions.Keys.Select(x => x.ToLowerInvariant())),
                StringComparer.OrdinalIgnoreCase);

        private static readonly Guid virtualSchemeCardTypeID = new Guid("b2b4d2c2-8f92-4262-9951-fe1a64bf9b30");

        #endregion

        #region Command Actions

        private async void Get1CButtonActionAsync(object parameter) =>
            await Get1CHelper.RequestAndAddFileAsync(
                this.cardDialogManager,
                this.cardRepository,
                this.createDialogFormFuncAsync,
                this.notificationUIManager);

        private async void ShowDialogTypeFormActionAsync(object parameter)
        {
            var virtualSchemeDialog = await this.cardTypeService.GetCardTypeAsync(virtualSchemeCardTypeID);
            var virtualSchemeDialogClone = await virtualSchemeDialog.DeepCloneAsync();

            var presenter = new VirtualSchemePresenter(
                virtualSchemeDialogClone.CardTypeSections,
                new List<CardTypeSection>()
                {
                    new CardTypeSection(new Guid(), "Instances", "Fake Instances", Scheme.SchemeTableContentType.Entries)
                });
            var form = await presenter.CreateFormAsync(this.createDialogFormFuncAsync, this.unityContainer);

            if (form is null)
            {
                TessaDialog.ShowError("Dialog type \"VirtualScheme\" not found.");
                return;
            }

            await this.uiHost.ShowDialogAsync(
                "Demo Dialog type form",
                form,
                closeOnEscapeKey: true);
        }

        #endregion

        #region Base Overrides

        public override async Task Initializing(ICardUIExtensionContext context)
        {
            context.Model.GetFileViewExtensionInitializationStrategyHandlers()
                .Add(async (ctx, m, ct) =>
                    ctx.Settings.TryGet<string>(DefaultCardTypeExtensionSettings.FilesViewAlias) == "AllFilesControl"
                        ? this.customInitializationStrategy
                        : null);
        }

        public override async Task Initialized(ICardUIExtensionContext context)
        {
            // Добавляем тестовую кнопку в тулбар.
            // Кнопка отображается в карточке, открытой во вкладке и в диалоге.
            // Если необходимо отображать кнопку, например, только во вкладке, то надо добавить условие:
            // context.DialogName is null
            if (context.ToolbarActions is not null)
            {
                var title = "TestButton";
                context.ToolbarActions.Remove(title);
                context.ToolbarActions.Add(
                    new CardToolbarAction(
                        title,
                        "Test Button",
                        context.UIContext.CardEditor.Toolbar.CreateIcon("Thin285"),
                        new DelegateCommand(_ => TessaDialog.ShowMessage("Test button was pressed.")),
                        order: -1) // Всегда выводим первой.
                    );
            }

            var driverNameControl = context.Model.Controls.TryGet<AutoCompleteEntryViewModel>("DriverName2");
            if (driverNameControl is not null)
            {
                this.autoCompleteDialogProvider.ChangeAutoCompleteDialog(driverNameControl);
                driverNameControl.ValueSelected +=
                    async (sender, args) =>
                    {
                        await this.notificationUIManager.ShowTextOrMessageBoxAsync(
                            "Demo for event ValueSelected." + Environment.NewLine
                            + "Selected item: " + args.Item.DisplayText);
                    };

                driverNameControl.ValueDeleted +=
                    async (sender, args) =>
                    {
                        await this.notificationUIManager.ShowTextOrMessageBoxAsync(
                            "Demo for event ValueDeleted." + Environment.NewLine
                            + "Deleted item: " + args.Item.DisplayText);
                    };

                // Выполняем открытие по двойному клику в модальном диалоге, а не в вкладке основного окна.
                driverNameControl.OpenCardCommandClosure.Execute = async p =>
                {
                    if (p is AutoCompleteItem autoCompleteItem)
                    {
                        if (autoCompleteItem.Reference is Guid id
                            && id != Guid.Empty)
                        {
                            await this.cardDialogManager.OpenCardAsync(id);
                        }
                    }
                };
            }

            var ownersControl = context.Model.Controls.TryGet<AutoCompleteTableViewModel>("Owners2");

            if (ownersControl is not null)
            {
                ownersControl.ValueSelected +=
                    async (sender, args) =>
                    {
                        await this.notificationUIManager.ShowTextOrMessageBoxAsync(
                            "Demo for event ValueSelected." + Environment.NewLine
                            + "Selected item: " + args.Item.DisplayText + "[" + args.Row.RowID + "]");
                    };

                ownersControl.ValueDeleted +=
                    async (sender, args) =>
                    {
                        await this.notificationUIManager.ShowTextOrMessageBoxAsync(
                            "Demo for event ValueDeleted." + Environment.NewLine
                            + "Deleted item: " + args.Item.DisplayText + "[" + args.Row.RowID + "]");
                    };
            }

            // определяем обработчик нажатия на кнопку Get1CButton
            var get1CButton = context.Model.Controls.TryGet<ButtonViewModel>("Get1CButton");
            if (get1CButton is not null)
            {
                get1CButton.CommandClosure.Execute = this.Get1CButtonActionAsync;
                get1CButton.IsReadOnly = !context.Model.FileContainer.Permissions.CanAdd;
            }

            // определяем обработчик нажатия на кнопку ShowDialogTypeForm
            var showDialogTypeFormButton = context.Model.Controls.TryGet<ButtonViewModel>("ShowDialogTypeForm");
            if (showDialogTypeFormButton is not null)
            {
                showDialogTypeFormButton.CommandClosure.Execute = this.ShowDialogTypeFormActionAsync;
            }

            // добавляем валидацию (красную рамку) на текстовый контрол CarName
            var carNameControl = context.Model.Controls.TryGet("CarName");
            if (carNameControl is not null)
            {
                carNameControl.HasActiveValidation = true;
                carNameControl.ValidationFunc = c =>
                    ((TextBoxViewModelBase) c).Text == "42"
                        ? "Can't enter magic number here"
                        : null;
            }

            // скрываем файлы категории "Image" в одном контроле и показываем в другом
            IFileControl allFilesControl = FilesViewGeneratorBaseUIExtension.TryGetFileControl(context.Model.Info, "AllFilesControl");

            if (allFilesControl is not null)
            {
                foreach (IFile file in allFilesControl.Files.ToArray())
                {
                    // разрешены все файлы, кроме категории "Изображения"
                    if (file.Category is { Caption: "Image" })
                    {
                        allFilesControl.Files.Remove(file);
                    }
                }
            }

            IFileControl imagesFilesControl = FilesViewGeneratorBaseUIExtension.TryGetFileControl(context.Model.Info, "ImageFilesControl");
            if (imagesFilesControl is not null)
            {
                foreach (IFile file in imagesFilesControl.Files.ToArray())
                {
                    // разрешены только файлы с категорией "Изображения"
                    if (file.Category is not { Caption: "Image" })
                    {
                        imagesFilesControl.Files.Remove(file);
                    }
                }
            }

            // в карточке "Автомобиль" файлы на клиенте не будут отмечаться как большие, независимо от настроек сервера;
            // после сохранения их отметит как большие серверное расширение
            context.FileContainer.SetNewPhysicalFileAction(async (ctx, ct) => { ctx.Tags.Remove(FileTag.Large); });

            context.FileContainer.ContainerFileAdding += (s, e) =>
            {
                switch (e.Control.Name)
                {
                    case "AllFilesControl":
                        // разрешены все файлы, кроме категории "Изображения"
                        if (e.File.Category is { Caption: "Image" })
                        {
                            e.Cancel = true;
                        }

                        break;

                    case "ImageFilesControl":
                        // разрешены только файлы с категорией "Изображения"
                        if (e.File.Category is not { Caption: "Image" })
                        {
                            e.Cancel = true;
                        }

                        break;
                }
            };

            // запрещаем добавлять файлы с расширением .exe
            context.FileContainer.Files.ItemChecking += (s, e) =>
            {
                if (e.Action == ControllableItemAction.Add
                    && string.Equals(FileHelper.GetExtension(e.Item.Name), ".exe", StringComparison.OrdinalIgnoreCase))
                {
                    TessaDialog.ShowMessage("Can't add file: " + e.Item.Name);
                    e.Cancel = true;
                }
            };

            // если предпросмотр инициируется через контролы файлов на вкладке "Сравнение файлов", то независимо от глобальных настроек для файлов,
            // которые можно сконвертировать в PDF, но нельзя отобразить встроенным предпросмотром, - будем запрашивать конвертацию
            Func<IFilePreviewContext, CancellationToken, ValueTask> prevFilePreviewAction =
                context.FileContainer.TryGetFilePreviewAction();

            context.FileContainer.SetFilePreviewAction(
                async (ctx, ct) =>
                {

                    switch (ctx.FileControl.Name)
                    {
                        case "CompareFilesView1":
                        case "CompareFilesView2":
                            if (pdfConverterExtensions.Contains(FileHelper.GetExtension(ctx.File.Name) ?? string.Empty))
                            {
                                IFileContent previewContent = await ctx.AllocateAdditionalLocalContentAsync(cancellationToken: ct);
                                previewContent.RequestInfo.SetConverterFormat(FileConverterFormat.Pdf);

                                ctx.PreviewContent = previewContent;
                                ctx.LoadingText = "$UI_Controls_Preview_ConvertingFile";
                                ctx.LoadingExtraText = "$UI_Controls_Preview_ConvertingFile_FullText";
                                return;
                            }

                            break;
                    }

                    // если файловый контрол в другом месте или файл не преобразуется в PDF, то используем стандартный обработчик
                    if (prevFilePreviewAction is not null)
                    {
                        await prevFilePreviewAction(ctx, ct);
                    }
                });

            // в контроле-таблице "Список акций"
            var grid = context.Model.Controls.TryGet<GridViewModel>("ShareList");
            if (grid is not null)
            {
                // добавляем валидацию при сохранении редактируемой строки
                grid.RowValidating += (s, e) =>
                {
                    if (string.IsNullOrWhiteSpace(e.Row.Get<string>("Name")))
                    {
                        e.ValidationResult.AddError(this, "Share's name is empty (from RowValidating).");
                    }
                };

                // при клике по ячейке из первой колонки "Акция" в открытом окне будет поставлен фокус
                // на первый контрол типа "Строка", в котором также будет выделен весь текст
                grid.RowInvoked += (s, e) =>
                {
                    if (e.Action == GridRowAction.Opening && e.ColumnIndex == 0)
                    {
                        IControlViewModel textBox = e.RowModel.ControlBag.FirstOrDefault(x => x is TextBoxViewModel);
                        if (textBox is not null)
                        {
                            textBox.SelectAllWhenFocused(oneTime: true);
                            textBox.Focus();
                        }
                    }
                };

                // при открытии окна добавления/редактирования строки добавляем горячую клавишу
                grid.RowInitializing += (s, e) =>
                {
                    e.Window.InputBindings.Add(
                        new KeyBinding(
                            new DelegateCommand(p => TessaDialog.ShowMessage("F5 key is pressed")),
                            new KeyGesture(Key.F5)));
                };

                // контекстное меню таблицы, зависит от кликнутой ячейки
                grid.ContextMenuGenerators.Insert(0, async ctx =>
                {
                    string text = $"Name={ctx.Row.Model.Get<string>("Name")}, Count={ctx.Control.SelectedRows.Count}, Cell=\"{(ctx.Cell?.Value)}\"";

                    ctx.MenuActions.Add(
                        new MenuAction(
                            "Name",
                            text,
                            Icon.Empty,
                            new DelegateCommand(p => { TessaDialog.ShowMessage("Share name is " + ctx.Row.Model.Get<string>("Name")); })));

                    ctx.MenuActions.Add(
                        new MenuAction(
                            "EditRow",
                            "Edit row",
                            ctx.MenuContext.Icons.Get("Thin2"),
                            new DelegateCommand(p =>
                            {
                                var column = ctx.ColumnIndex >= 0 ? grid.Columns[ctx.ColumnIndex] : null;
                                var cellParam = new TessaGridCellParameter(ctx.Row, column);

                                if (grid.EditRowCommand.CanExecute(cellParam))
                                {
                                    grid.EditRowCommand.Execute(cellParam);
                                }
                            })));
                });

                // нажатие Ctrl+Enter показывает окно для выбранной строки в фокусе
                grid.KeyDownHandlers.Add((row, e) =>
                {
                    if (e.Key == Key.Enter && e.KeyboardDevice.Modifiers.Has(ModifierKeys.Control))
                    {
                        e.Handled = true;
                        TessaDialog.ShowMessage($"Share name is {row.Model.Get<string>("Name")}");
                    }
                });
            }

            var shareListView = context.Model.Controls.TryGet<CardTableViewControlViewModel>("ShareListView");

            if (shareListView is not null)
            {
                // добавляем валидацию при сохранении редактируемой строки
                shareListView.RowValidating += (s, e) =>
                {
                    if (string.IsNullOrWhiteSpace(e.Row.Get<string>("Name")))
                    {
                        e.ValidationResult.AddError(this, "Share's name is empty (from RowValidating).");
                    }
                };

                // при клике по ячейке из первой колонки "Акция" в открытом окне будет поставлен фокус
                // на первый контрол типа "Строка", в котором также будет выделен весь текст
                shareListView.RowInvoked += (s, e) =>
                {
                    if (e.Action == GridRowAction.Opening && e.Cell?.Column.ColumnName == "0")
                    {
                        IControlViewModel textBox = e.RowModel.ControlBag.FirstOrDefault(x => x is TextBoxViewModel);
                        if (textBox is not null)
                        {
                            textBox.SelectAllWhenFocused(oneTime: true);
                            textBox.Focus();
                        }
                    }
                };

                // при открытии окна добавления/редактирования строки добавляем горячую клавишу
                shareListView.RowInitializing += (s, e) =>
                {
                    e.Window.InputBindings.Add(
                        new KeyBinding(
                            new DelegateCommand(p => TessaDialog.ShowMessage("F5 key is pressed")),
                            new KeyGesture(Key.F5)));
                };

                // контекстное меню таблицы, зависит от кликнутой ячейки
                shareListView.RowContextMenuGenerators.Insert(0, async ctx =>
                {

                    string text = $"Name={ctx.RowViewModel.Data["0"]}, Count={ctx.ViewControlViewModel.SelectedRows.Count}, Cell=\"{(ctx.Cell?.Value)}\"";

                    ctx.MenuActions.Add(
                        new MenuAction(
                            "Name",
                            text,
                            Icon.Empty,
                            new DelegateCommand(p => { TessaDialog.ShowMessage("Share name is " + ctx.RowViewModel.Data["0"]); })));

                    ctx.MenuActions.Add(
                        new MenuAction(
                            "EditRow",
                            "Edit row",
                            ctx.MenuContext.Icons.Get("Thin2"),
                            new DelegateCommand(p =>
                            {
                                var cell = ctx.Cell;

                                if (shareListView.EditRowCommand.CanExecute(cell))
                                {
                                    shareListView.EditRowCommand.Execute(cell);
                                }
                            })));
                });

                // нажатие Ctrl+Enter показывает окно для выбранной строки в фокусе
                shareListView.KeyDownHandlers.Insert(0, (item, data, e) =>
                {
                    if (e.Key == Key.Enter && e.KeyboardDevice.Modifiers.Has(ModifierKeys.Control))
                    {
                        e.Handled = true;
                        if (data is ViewControlRowViewModel row)
                        {
                            TessaDialog.ShowMessage($"Share name is {row.Data["0"]}");
                        }
                    }
                });
            }

            // когда страница листается в левой области предпросмотра - она также листается в правой
            var preview1 = context.Model.Controls.TryGet<FilePreviewViewModel>("Preview1");
            var preview2 = context.Model.Controls.TryGet<FilePreviewViewModel>("Preview2");
            if (preview1 is not null && preview2 is not null)
            {
                preview1.FilePreview.TryLoadCustomPreviewFuncAsync = async (model, path, ct) =>
                {
                    string extension = System.IO.Path.GetExtension(path);
                    if (string.Equals(extension, ".txt", StringComparison.Ordinal))
                    {
                        string text = "Custom content preview:\n\n" + await System.IO.File.ReadAllTextAsync(path, ct);

                        // здесь можно указать контрол или модель представления, для которой в ресурсах определяется DataTemplate;
                        // также задайте функцию UnloadCustomPreviewFuncAsync, если контролу требуется очистка
                        return new System.Windows.Controls.TextBlock { Text = text, FontSize = 16, FontStyle = FontStyles.Italic, TextWrapping = TextWrapping.Wrap };
                    }

                    return null;
                };

                preview1.FilePreview.PagingControlPropertyChanged += (s, e) =>
                {
                    if (e.PropertyName == nameof(IFilePagingControlModel.CurrentPage))
                    {
                        int currentPage = ((IFilePagingControlModel) s).CurrentPage;
                        if (currentPage <= 0)
                        {
                            return;
                        }

                        IFilePagingControlModel otherControl = preview2.FilePreview.PagingControl;

                        if (otherControl is not null
                            && currentPage <= otherControl.TotalPages
                            && currentPage != otherControl.CurrentPage)
                        {
                            otherControl.BeginMove(currentPage);
                        }
                    }
                };
            }

            // Изменяем цвет только вариантов завершения
            await context.Model.ModifyTasksAsync(async (taskViewModel, _) =>
            {
                await taskViewModel.ModifyWorkspaceAsync((viewModel, _) =>
                {
                    foreach (var action in viewModel.Workspace.Actions)
                    {
                        if (action.Type == TaskActionType.AdditionalActions)
                        {
                            action.Background = Colors.Red;
                            continue;
                        }
                        if (action.CompletionOption is null)
                        {
                            continue;
                        }

                        action.Background = Colors.DarkBlue;
                    }

                    foreach (var action in viewModel.Workspace.AdditionalActions)
                    {
                        action.Background = Colors.SkyBlue;
                    }

                    return ValueTask.CompletedTask;
                });
            });
        }


        public override async Task Saving(ICardUIExtensionContext context)
        {
            // Добавим ошибку валидации, если найден файл с определённым именем.
            if (context.FileContainer.Files.Any(x => x.Name == "show error.txt"))
            {
                context.ValidationResult.AddError("File \"show error.txt\" was added, can't save the card.");
                return;
            }

            // Удаляем файл с определённым именем.
            IFile fileToRemove = context.FileContainer.Files.FirstOrDefault(x => x.Name == "remove me.txt");
            if (fileToRemove is not null)
            {
                bool removed = await context.FileContainer.Files.RemoveWithNotificationAsync(fileToRemove, context.CancellationToken);
                if (removed)
                {
                    // и вместо него добавляем другой файл с таким же контентом и категорией
                    await context.FileContainer
                        .BuildFile("file was removed.txt")
                        .SetContent(async ct => fileToRemove.Content)
                        .SetCategory(fileToRemove.Category)
                        .AddWithNotificationAsync(cancellationToken: context.CancellationToken);
                }
            }
        }

        #endregion
    }
}
