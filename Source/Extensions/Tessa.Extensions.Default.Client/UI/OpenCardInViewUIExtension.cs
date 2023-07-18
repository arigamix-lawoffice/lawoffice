using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared.Cards;
using Tessa.Localization;
using Tessa.Platform;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using Tessa.UI;
using Tessa.UI.Cards;
using Tessa.UI.Cards.Controls;
using Tessa.UI.Controls;
using Tessa.Views.Metadata;

namespace Tessa.Extensions.Default.Client.UI
{
    /// <summary>
    /// Реализация расширения типа карточки для добавления возможности
    /// открывать карточки из представления.
    /// </summary>
    public sealed class OpenCardInViewUIExtension : CardUIExtension
    {
        #region Fields

        private readonly ICardMetadata cardMetadata;
        private readonly IUIHost uiHost;
        private readonly IAdvancedCardDialogManager cardDialogManager;

        #endregion

        #region Constructors

        public OpenCardInViewUIExtension(
            ICardMetadata cardMetadata,
            IUIHost uiHost,
            IAdvancedCardDialogManager cardDialogManager)
        {
            this.cardMetadata = cardMetadata ?? throw new ArgumentNullException(nameof(cardMetadata));
            this.uiHost = uiHost ?? throw new ArgumentNullException(nameof(uiHost));
            this.cardDialogManager = cardDialogManager ?? throw new ArgumentNullException(nameof(cardDialogManager));
        }

        #endregion

        #region Type extension methods

        /// <summary>
        /// Помощник применения функционала данного расширения.
        /// </summary>
        /// <param name="context">Контекст расширения типа.</param>
        /// <returns>Ассинхронная задача.</returns>
        private async Task ExecuteInitializedAsync(ITypeExtensionContext context)
        {
            var extensionContext = (ICardUIExtensionContext) context.ExternalContext;
            if (context.CardTask is null)
            {
                // действия для карточки
                await this.AttachDoubleClickHandlerAsync(
                    extensionContext.UIContext,
                    extensionContext.Model,
                    context.Settings,
                    cancellationToken: context.CancellationToken);
            }
            else
            {
                // действия для задания
                await extensionContext.Model.ModifyTasksAsync(async (task, model) =>
                {
                    if (task.TaskModel.CardTask == context.CardTask)
                    {
                        await task.ModifyWorkspaceAsync(async (t, subscribeToTaskModel) =>
                        {
                            await this.AttachDoubleClickHandlerAsync(
                                extensionContext.UIContext,
                                task.TaskModel,
                                context.Settings);
                        });
                    }
                });
            }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Помощник подключения функционала открытия карточки по двойному клику.
        /// </summary>
        /// <param name="uiContext">Контекст операции с пользовательским интерфейсом.</param>
        /// <param name="cardModel">Модель карточки.</param>
        /// <param name="settings">Настройки расширения.</param>
        /// <param name="cancellationToken">Объект для отмены асинхронной операции.</param>
        /// <returns>Асинхронная задача с результатом успешности присоединения обработчика.</returns>
        private async Task<bool> AttachDoubleClickHandlerAsync(
            IUIContext uiContext,
            ICardModel cardModel,
            ISerializableObject settings,
            CancellationToken cancellationToken = default)
        {
            // получаем имя представления
            var viewControlName = settings.TryGet<string>(DefaultCardTypeExtensionSettings.ViewControlAlias);

            // пытаемся получить контрол по имени
            var viewModel = cardModel.Controls.TryGet<CardViewControlViewModel>(viewControlName);
            if (viewModel is null)
            {
                return false;
            }
            
            // определяем колонку с идентификатором карточки
            var prefixReference = settings.TryGet<string>(DefaultCardTypeExtensionSettings.ViewReferencePrefix)?.Trim();
            var mapping = TryGetCardReferenceMapping(viewModel, prefixReference);
            var displayInDialog = settings.TryGet<bool>(DefaultCardTypeExtensionSettings.IsOpenCardInDialog);
            var dialogName = settings.TryGet<string>(DefaultCardTypeExtensionSettings.CardDialogName);

            // присоединяем наш обработчик
            viewModel.DoubleClickCommand = new DelegateCommand(async p =>
            {
                if (!string.IsNullOrEmpty(prefixReference) && mapping is null)
                {
                    string message = LocalizationManager.Format(
                        "$UI_Cards_TypesEditor_Exception_RefSectionInView",
                        prefixReference,
                        viewModel.CardTypeControl.ToString(),
                        viewModel.Block.CardTypeBlock.ToString(),
                        viewModel.Block.Form.CardTypeForm.ToString(),
                        viewModel.CardModel.CardType.ToString());
                    throw new InvalidOperationException(message);
                }
                await this.DoubleClickHandlerAsync(
                    uiContext,
                    (IViewClickInfo) p,
                    mapping,
                    displayInDialog,
                    dialogName);
            });

            return true;
        }

        /// <summary>
        /// Обработчик двойного клика мыши.
        /// </summary>
        /// <param name="uiContext">Контекст операции с пользовательским интерфейсом.</param>
        /// <param name="clickInfo">Информация о текущей выделенной в представлении строке.</param>
        /// <param name="mapping">Названия колонок с идентификатором и именем.</param>
        /// <param name="displayInDialog">Флаг необходимости открывать карточку в диалоговом режиме.</param>
        /// <param name="dialogName">Служебное имя диалога.</param>
        /// <returns>Задача.</returns>
        private async Task DoubleClickHandlerAsync(
            IUIContext uiContext,
            IViewClickInfo clickInfo,
            ViewMapping mapping,
            bool displayInDialog,
            string dialogName)
        {
            var row = clickInfo.Row;
            if (mapping is null ||
                row.Data[mapping.EntityID] is not Guid cardID ||
                cardID == Guid.Empty)
            {
                return;
            }
            
            string cardName = null;
            if (!string.IsNullOrWhiteSpace(mapping.EntityName))
            {
                cardName = row.Data[mapping.EntityName] as string;
            }
            var caption = cardName;
            if (string.IsNullOrWhiteSpace(caption))
            {
                caption = cardID.ToString();
            }

            var viewControlInfo = new CardViewControlInfo
            {
                ID = cardID,
                DisplayText = caption,
                ControlName = mapping.ControlName,
                ViewAlias = mapping.ViewName,
                ColPrefix = mapping.ColPrefix
            };
            var info = new Dictionary<string, object>(StringComparer.Ordinal);
            viewControlInfo.Set(info);

            using ISplash splash = TessaSplash.Create(TessaSplashMessage.OpeningCard);
            using var _ = UIContext.Create(uiContext);
            try
            {
                // теперь проверяем режим открытия
                if (displayInDialog)
                {
                    // открывать в диалоге
                    await this.cardDialogManager.OpenCardAsync(
                        cardID,
                        dialogName: dialogName,
                        options: new OpenCardOptions
                        {
                            DisplayValue = caption,
                            Info = info,
                            UIContext = uiContext,
                            Splash = splash,
                        });
                }
                else
                {
                    // открывать во вкладке или диалоге, если карточка уже открыта в диалоге
                    await this.uiHost.OpenCardAsync(
                        cardID,
                        options: new OpenCardOptions
                        {
                            DisplayValue = caption,
                            Info = info,
                            UIContext = uiContext,
                            Splash = splash,
                        });
                }
            }
            catch (NotSupportedException)
            {
                // используется FakeUIHost, например, мы открыты в TessaAdmin, игнорируем ошибку
            }
        }

        /// <summary>
        /// Помощник получения маппинга колонок представления.
        /// </summary>
        /// <param name="viewModel">Модель контрола представления.</param>
        /// <param name="prefixReference">Префикс целевых колонок представления.</param>
        /// <returns>Возвращает маппинг или <c>null</c> - если маппинг не найден.</returns>
        private static ViewMapping TryGetCardReferenceMapping(
            CardViewControlViewModel viewModel,
            string prefixReference)
        {
            var viewMetadata = viewModel.ViewMetadata;
            if (viewMetadata is null)
            {
                return null;
            }

            if (!string.IsNullOrEmpty(prefixReference))
            {
                // пытаемся найти нужную ссылку
                var reference = viewMetadata.References.FindByName(prefixReference);
                if (reference is null)
                {
                    return null;
                }
                var mapping = TryGetCardIDNameMapping(viewMetadata, reference.ColPrefix, reference.DisplayValueColumn);
                return TryAttachControlInfo(mapping, viewModel);
            }
            
            foreach (var reference in viewMetadata.References)
            {
                if (!reference.IsCard || !reference.OpenOnDoubleClick)
                {
                    continue;
                }
                var mapping = TryGetCardIDNameMapping(viewMetadata, reference.ColPrefix, reference.DisplayValueColumn);
                if (mapping is not null)
                {
                    return TryAttachControlInfo(mapping, viewModel);
                }
            }

            return null;
        }

        /// <summary>
        /// Помощник получения маппинга колонок представления.
        /// </summary>
        /// <param name="viewMetadata">Метаданные представления.</param>
        /// <param name="prefixReference">Префикс целевых колонок представления.</param>
        /// <param name="displayValueColumn">Имя колонки с отображаемым знанчением. Может быть <c>null</c>.</param>
        /// <returns>Возвращает маппинг или <c>null</c> - если маппинг не найден.</returns>
        private static ViewMapping TryGetCardIDNameMapping(
            IViewMetadata viewMetadata,
            string prefixReference,
            string displayValueColumn = null)
        {
            if (viewMetadata is null ||
                string.IsNullOrEmpty(prefixReference))
            {
                return null;
            }
                      
            var column = viewMetadata.Columns.FindByName(prefixReference + "ID") ??
                viewMetadata.Columns.FindByName(prefixReference + "RowID");
            if (column is null)
            {
                return null;
            }

            var mapping = new ViewMapping()
            {
                ColPrefix = prefixReference,
                EntityID = column.Alias
            };

            if (string.IsNullOrEmpty(displayValueColumn))
            {
                displayValueColumn = viewMetadata.Columns
                    .Where(c => c.Alias.StartsWith(prefixReference, StringComparison.OrdinalIgnoreCase) &&
                                c != column)
                    .Select(c => c.Alias)
                    .FirstOrDefault();
            }
            mapping.EntityName = displayValueColumn;

            return mapping;
        }

        /// <summary>
        /// Помощник присоединения данных о контроле к маппингу колонок представления.
        /// </summary>
        /// <param name="mapping">Маппинг колонок представления.</param>
        /// <param name="viewModel">Модель представления.</param>
        /// <returns>Маппинг колонок представления с присоединённой информацией.</returns>
        private static ViewMapping TryAttachControlInfo(
            ViewMapping mapping,
            CardViewControlViewModel viewModel)
        {
            if (mapping is not null)
            {
                mapping.ControlName = viewModel.CardTypeControl.Name;
                mapping.ViewName = viewModel.Alias;
            }
            return mapping;
        }

        #endregion

        #region Base overrides

        /// <inheritdoc/>
        public override async Task Initialized(ICardUIExtensionContext context)
        {
            ValidationResult result = await CardHelper
                .ExecuteTypeExtensionsAsync(
                    DefaultCardTypeExtensionTypes.OpenCardInView,
                    context.Card,
                    this.cardMetadata,
                    ExecuteInitializedAsync,
                    context,
                    cancellationToken: context.CancellationToken);

            context.ValidationResult.Add(result);
        }

        #endregion

        #region Internal Classes

        /// <summary>
        /// Класс для маппинга колонок в представлении.
        /// </summary>
        class ViewMapping
        {
            /// <summary>
            /// Имя элемента управления.
            /// </summary>
            internal string ControlName;
            /// <summary>
            /// Имя представления.
            /// </summary>
            internal string ViewName;
            /// <summary>
            /// Префикс референса.
            /// </summary>
            internal string ColPrefix;
            /// <summary>
            /// Название колонки с идентификатором.
            /// </summary>
            internal string EntityID;
            /// <summary>
            /// Название колонки с именем.
            /// </summary>
            internal string EntityName;
        }

        #endregion
    }
}
