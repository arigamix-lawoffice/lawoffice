#nullable enable

using System;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Extensions.Default.Client.Views;
using Tessa.Platform;
using Tessa.UI;
using Tessa.UI.Tiles.Extensions;
using Tessa.UI.Views;

namespace Tessa.Extensions.Default.Client.Tiles
{
    /// <summary>
    /// Расширение, переопределяющее действие тайла <see cref="TileNames.FilterView"/> для замены диалога фильтрации представления.
    /// </summary>
    public sealed class FilterViewDialogOverrideTileExtension :
        TileExtension
    {
        #region Fields

        private readonly IAdvancedFilterViewDialogManager advancedFilterViewDialogManager;

        private readonly IFilterViewDialogDescriptorRegistry filterViewDialogDescriptorRegistry;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="advancedFilterViewDialogManager"><inheritdoc cref="IAdvancedFilterViewDialogManager" path="/summary"/></param>
        /// <param name="filterViewDialogDescriptorRegistry"><inheritdoc cref="IFilterViewDialogDescriptorRegistry" path="/summary"/></param>
        public FilterViewDialogOverrideTileExtension(
            IAdvancedFilterViewDialogManager advancedFilterViewDialogManager,
            IFilterViewDialogDescriptorRegistry filterViewDialogDescriptorRegistry)
        {
            this.advancedFilterViewDialogManager = NotNullOrThrow(advancedFilterViewDialogManager);
            this.filterViewDialogDescriptorRegistry = NotNullOrThrow(filterViewDialogDescriptorRegistry);
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc />
        public override Task InitializingLocal(ITileLocalExtensionContext context)
        {
            var otherTile = context.Workspace.LeftPanel.Tiles
                .TryGet(TileNames.ViewsOther);

            if (otherTile is null)
            {
                return Task.CompletedTask;
            }

            var filterViewTile = otherTile.Tiles
                .TryGet(TileNames.FilterView);

            if (filterViewTile is null)
            {
                return Task.CompletedTask;
            }

            var filterViewTileOriginalCommand = filterViewTile.Command;
            filterViewTile.Command = new DelegateCommand(async o =>
                await ExecuteInViewContextAsync(
                    async viewContext =>
                    {
                        if (this.filterViewDialogDescriptorRegistry.TryGet(viewContext.Id) is { } descriptor)
                        {
                            await this.advancedFilterViewDialogManager.OpenAsync(
                                descriptor,
                                viewContext.Parameters,
                                CancellationToken.None);
                        }
                        else
                        {
                            filterViewTileOriginalCommand.Execute(o);
                        }
                    },
                    filterViewTileOriginalCommand.CanExecute));

            return Task.CompletedTask;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Вызывает действие <paramref name="executeAsync"/> в текущем контексте <see cref="IViewContext"/> если доступен контекст, если определена <paramref name="canExecute"/> осуществляется проверка возможности выполнения операции.
        /// </summary>
        /// <param name="executeAsync">Делегат вызываемого в контексте действие.</param>
        /// <param name="canExecute">Делегат проверки возможности вызова действия.</param>
        private static async Task ExecuteInViewContextAsync(
            Func<IViewContext, Task> executeAsync,
            Func<IViewContext, bool>? canExecute = null)
        {
            var viewContext = UIContext.Current.ViewContext;
            if (viewContext is null)
            {
                return;
            }

            if (canExecute is not null
                && !canExecute(viewContext))
            {
                return;
            }

            await executeAsync(viewContext);
        }

        #endregion
    }
}
