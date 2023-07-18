using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Localization;
using Tessa.Platform;
using Tessa.Platform.Collections;
using Tessa.Properties.Resharper;
using Tessa.UI;
using Tessa.UI.Menu;
using Tessa.UI.Views;
using Tessa.UI.Views.Content;
using Tessa.UI.Views.Workplaces.Tree;
using Tessa.Views;

namespace Tessa.Extensions.Default.Client.Views
{
    /// <summary>
    ///     Расширение демонстрирующее возможность добавления элементов контекстного меню
    /// </summary>
    /// <remarks>
    /// У расширения есть конфигуратор <see cref="ViewsContextMenuExtensionConfigurator"/>.
    /// </remarks>
    public sealed class ViewsContextMenuExtension : IWorkplaceViewComponentExtension
    {
        /// <summary>
        ///     The clear filter caption.
        /// </summary>
        private const string ClearFilterCaption = "$Views_Table_ClearFilter_ToolTip";

        /// <summary>
        ///     The clear filter icon name.
        /// </summary>
        private const string ClearFilterIconName = "Thin65";

        /// <summary>
        ///     The clear filter name.
        /// </summary>
        private const string ClearFilterName = "ClearFilter";

        /// <summary>
        ///     The filter icon name.
        /// </summary>
        private const string FilterIconName = "Thin100";

        /// <summary>
        ///     The filter name.
        /// </summary>
        private const string FilterName = "Filter";

        /// <summary>
        ///     The filter separator name.
        /// </summary>
        private const string FilterSeparatorName = "FilterSeparator";

        /// <summary>
        ///     The open card icon name.
        /// </summary>
        private const string OpenCardIconName = "Thin13";

        /// <summary>
        ///     The open card separator name.
        /// </summary>
        private const string OpenCardSeparatorName = "OpenCardSeparator";

        /// <summary>
        ///     The refresh icon name.
        /// </summary>
        private const string RefreshIconName = "Thin412";

        /// <summary>
        ///     The refresh name.
        /// </summary>
        private const string RefreshName = "Refresh";

        /// <summary>
        ///     The ui tiles filter.
        /// </summary>
        private const string UITilesFilter = "$UI_Tiles_Filter";

        /// <summary>
        ///     The ui tiles opencard.
        /// </summary>
        private const string UITilesOpenCard = "$UI_Tiles_OpenCard";

        /// <summary>
        ///     The ui tiles refresh.
        /// </summary>
        private const string UITilesRefresh = "$UI_Tiles_Refresh";

        /// <summary>
        ///     Gets or sets the host getter.
        /// </summary>
        private readonly Func<IUIHost> hostGetter;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewsContextMenuExtension"/> class.
        ///     Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
        /// </summary>
        /// <param name="hostGetter">
        /// Фабрика получения <see cref="IUIHost"/>
        /// </param>
        public ViewsContextMenuExtension([NotNull] Func<IUIHost> hostGetter)
        {
            Contract.Requires(hostGetter != null);
            this.hostGetter = hostGetter;
        }

        /// <summary>
        /// Вызывается при клонировании модели <paramref name="source"/> в контексте <paramref name="context"/>
        /// </summary>
        /// <param name="source">
        /// Исходная модель
        /// </param>
        /// <param name="cloned">
        /// Клонированная модель
        /// </param>
        /// <param name="context">
        /// Контекст клонирования
        /// </param>
        public void Clone(IWorkplaceViewComponent source, IWorkplaceViewComponent cloned, ICloneableContext context)
        {
        }

        /// <summary>
        /// Вызывается после создания модели <paramref name="model"/>
        /// </summary>
        /// <param name="model">
        /// Инициализируемая модель
        /// </param>
        public void Initialize(IWorkplaceViewComponent model)
        {
            model.ContextMenuGenerators.AddRange(
                GetRefreshMenuAction(),
                GetFilterMenuAction(),
                GetClearFilterMenuAction(),
                this.GetOpenCardMenuAction());
        }

        /// <summary>
        /// Вызывается после создания модели <paramref name="model"/> перед отображении в UI
        /// </summary>
        /// <param name="model">
        /// Модель
        /// </param>
        public void Initialized(IWorkplaceViewComponent model)
        {
        }

        /// <summary>
        ///     Возвращает действие создающее элемент меню
        /// </summary>
        /// <returns>
        ///     Действие создающее элемент меню
        /// </returns>
        private static Func<ViewContextMenuContext, ValueTask> GetClearFilterMenuAction()
        {
            return c =>
                {
                    if (c.ViewContext.Parameters.All(p => p.ReadOnly))
                    {
                        return new ValueTask();
                    }

                    c.MenuActions.Add(
                        new MenuAction(
                            ClearFilterName,
                            LocalizationManager.Localize(ClearFilterCaption),
                            c.MenuContext.Icons.Get(ClearFilterIconName),
                            new DelegateCommand(
                                async o => await c.ViewContext.ClearFilterAsync(c.ViewContext.Parameters),
                                o => c.ViewContext.CanClearFilter(c.ViewContext.Parameters))));

                    return new ValueTask();
                };
        }

        /// <summary>
        ///     Возвращает действие создающее элемент меню
        /// </summary>
        /// <returns>
        ///     Действие создающее элемент меню
        /// </returns>
        private static Func<ViewContextMenuContext, ValueTask> GetFilterMenuAction()
        {
            return c =>
                {
                    if (c.ViewContext.Parameters.Metadata.All(p => p.Hidden))
                    {
                        return new ValueTask();
                    }

                    c.MenuActions.Add(new MenuSeparatorAction(FilterSeparatorName));
                    c.MenuActions.Add(
                        new MenuAction(
                            FilterName,
                            LocalizationManager.Localize(UITilesFilter),
                            c.MenuContext.Icons.Get(FilterIconName),
                            new DelegateCommand(async o => await c.ViewContext.FilterViewAsync(), o => c.ViewContext.CanFilterView())));

                    return new ValueTask();
                };
        }

        /// <summary>
        ///     Возвращает действие создающее элемент меню
        /// </summary>
        /// <returns>
        ///     Действие создающее элемент меню
        /// </returns>
        private static Func<ViewContextMenuContext, ValueTask> GetRefreshMenuAction()
        {
            return
                c =>
                    {
                        c.MenuActions.Add(
                            new MenuAction(
                                RefreshName,
                                LocalizationManager.Localize(UITilesRefresh),
                                c.MenuContext.Icons.Get(RefreshIconName),
                                new DelegateCommand(
                                    async o => await c.ViewContext.RefreshViewAsync(),
                                    o => c.ViewContext.CanRefreshView())));

                        return new ValueTask();
                    };
        }

        /// <summary>
        ///     Возвращает действие создающее элемент меню
        /// </summary>
        /// <returns>
        ///     Действие создающее элемент меню
        /// </returns>
        private Func<ViewContextMenuContext, ValueTask> GetOpenCardMenuAction()
        {
            return async c =>
                {
                    if (c.ViewContext.RefSection is not null)
                    {
                        return;
                    }

                    var viewContext = c.ViewContext;
                    var view = viewContext.View;
                    IDictionary<string, object> selectedObject = viewContext.SelectedRow;
                    if (selectedObject == null || view == null)
                    {
                        return;
                    }

                    var metadata = await view.GetMetadataAsync(c.CancellationToken);
                    if (metadata == null)
                    {
                        return;
                    }

                    var cardRefs = (from r in metadata.References where r.IsCard select r).ToArray();
                    if (cardRefs.Length == 0)
                    {
                        return;
                    }

                    bool separatorAddided = false;
                    var addedIDs = new List<Guid>();
                    foreach (var cardRef in cardRefs)
                    {
                        var cardId = selectedObject.GetValueID(cardRef.ColPrefix);
                        if (cardId == null || cardId is DBNull)
                        {
                            return;
                        }

                        object displayValueObj = !string.IsNullOrWhiteSpace(cardRef.DisplayValueColumn)
                                                     ? selectedObject[cardRef.DisplayValueColumn.ToUpperInvariant()]
                                                     : selectedObject.GetFirstStringValueByPrefix(cardRef.ColPrefix);

                        string displayValue = displayValueObj != null ? displayValueObj.ToString() : string.Empty;

                        Guid id;
                        if (cardId is Guid guid)
                        {
                            id = guid;
                        }
                        else
                        {
                            if (!Guid.TryParse(cardId.ToString(), out id))
                            {
                                continue;
                            }
                        }

                        if (!addedIDs.Contains(id))
                        {
                            addedIDs.Add(id);

                            if (!separatorAddided)
                            {
                                c.MenuActions.Add(new MenuSeparatorAction(OpenCardSeparatorName));
                                separatorAddided = true;
                            }

                            c.MenuActions.Add(
                                new MenuAction(
                                    "OpenCard_" + id,
                                    string.Format("{0}: {1}", LocalizationManager.Localize(UITilesOpenCard), LocalizationManager.Format(displayValue)),
                                    c.MenuContext.Icons.Get(OpenCardIconName),
                                    new DelegateCommand(
                                        async o =>
                                        await c.MenuContext.UIContextExecutorAsync(async (ctx, ct) =>
                                            await this.OpenCardInUIHostAsync(id, displayValue, ctx, ct)))));
                        }
                    }
                };
        }

        /// <summary>
        /// Вызывает отображение карточки в UI
        /// </summary>
        /// <param name="id">
        /// Идентификатор карточки
        /// </param>
        /// <param name="displayValue">
        /// Отображаемая строка
        /// </param>
        /// <param name="context">
        /// Контекст
        /// </param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        private async Task OpenCardInUIHostAsync(
            Guid id,
            string displayValue,
            IUIContext context,
            CancellationToken cancellationToken = default)
        {
            try
            {
                using ISplash splash = TessaSplash.Create(TessaSplashMessage.OpeningCard);

                IUIHost uiHost = this.hostGetter();
                await uiHost.OpenCardAsync(
                    id,
                    options: new OpenCardOptions
                    {
                        DisplayValue = displayValue,
                        UIContext = context,
                        Splash = splash,
                    },
                    cancellationToken: cancellationToken);
            }
            catch (NotSupportedException)
            {
                // используется FakeUIHost, например, мы открыты в TessaAdmin, игнорируем ошибку
            }
        }
    }
}