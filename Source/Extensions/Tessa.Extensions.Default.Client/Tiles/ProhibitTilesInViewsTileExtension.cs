using System;
using System.Linq;
using System.Threading.Tasks;
using Tessa.Platform;
using Tessa.UI.Tiles;
using Tessa.UI.Tiles.Extensions;
using Tessa.UI.Views;
using Tessa.Views;
using Tessa.Views.Metadata;

namespace Tessa.Extensions.Default.Client.Tiles
{
    /// <summary>
    /// Базовый класс для расширения, которое запрещает системные плитки "Удалить", "Экспорт" и "Показать структуру" для представлений.
    /// Если в системе появятся новые плитки, связанные с карточками в представлениях, то это расширение будет дополнено.
    /// Поэтому рекомендуется использовать классы-наследники для скрытия таких системных плиток в ваших представлениях.
    /// </summary>
    public abstract class ProhibitTilesInViewsTileExtension :
        TileExtension
    {
        #region Private Methods

        private void DeleteEvaluating(object sender, TileEvaluationEventArgs e) =>
            SetEnabledWithCollapsingInViewContext(e, this.CanDeleteCardAsync);

        private void ExportEvaluating(object sender, TileEvaluationEventArgs e) =>
            SetEnabledWithCollapsingInViewContext(e, this.CanExportCardAsync);

        private void ViewCardStorageEvaluating(object sender, TileEvaluationEventArgs e) =>
            SetEnabledWithCollapsingInViewContext(e, this.CanViewCardStorageAsync);

        private static async void SetEnabledWithCollapsingInViewContext(
            TileEvaluationEventArgs e,
            Func<ITessaView, IViewMetadata, ValueTask<bool>> canProcessViewFuncAsync)
        {
            IViewContext viewContext = e.CurrentTile.Context.ViewContext;

            var deferral = e.Defer();
            try
            {
                bool isEnabled = viewContext != null;
                if (isEnabled)
                {
                    ITessaView view = viewContext.View;
                    IViewMetadata viewMetadata;

                    bool canExecute = view != null
                        && (viewMetadata = await view.GetMetadataAsync()).References.Any(x => x.OpenOnDoubleClick && x.IsCard)
                        && viewContext.SelectedRow != null
                        && await canProcessViewFuncAsync(view, viewMetadata);

                    isEnabled &= canExecute;
                }

                e.SetIsEnabledWithCollapsing(e.CurrentTile, isEnabled);
            }
            catch (Exception ex)
            {
                deferral.SetException(ex);
            }
            finally
            {
                deferral.Dispose();
            }
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Метод возвращает <c>false</c>, если для заданного представления известно,
        /// что должна быть скрыта плитка удаления карточки из представления.
        ///
        /// Для неизвестных представлений должно возвращаться <c>true</c>, их также могут скрыть другие расширения.
        /// </summary>
        /// <param name="view">Представление, для которого выполняется проверка.</param>
        /// <param name="viewMetadata">Метаинформация представления.</param>
        /// <returns>
        /// <c>true</c>, если для заданного представления известно, что должна быть скрыта плитка удаления карточки из представления;
        /// <c>false</c> в противном случае.
        /// </returns>
        protected abstract ValueTask<bool> CanDeleteCardAsync(ITessaView view, IViewMetadata viewMetadata);

        /// <summary>
        /// Метод возвращает <c>false</c>, если для заданного представления известно,
        /// что должны быть скрыты плитки экспорта карточек из представления.
        ///
        /// Для неизвестных представлений должно возвращаться <c>true</c>, их также могут скрыть другие расширения.
        /// </summary>
        /// <param name="view">Представление, для которого выполняется проверка.</param>
        /// <param name="viewMetadata">Метаинформация представления.</param>
        /// <returns>
        /// <c>true</c>, если для заданного представления известно, что должны быть скрыты плитки экспорта карточек;
        /// <c>false</c> в противном случае.
        /// </returns>
        protected abstract ValueTask<bool> CanExportCardAsync(ITessaView view, IViewMetadata viewMetadata);

        /// <summary>
        /// Метод возвращает <c>false</c>, если для заданного представления известно,
        /// что должна быть скрыта плитка просмотра структуры карточки из представления.
        ///
        /// Для неизвестных представлений должно возвращаться <c>true</c>, их также могут скрыть другие расширения.
        /// </summary>
        /// <param name="view">Представление, для которого выполняется проверка.</param>
        /// <param name="viewMetadata">Метаинформация представления.</param>
        /// <returns>
        /// <c>true</c>, если для заданного представления известно, что должна быть скрыта плитка просмотра структуры карточки из представления;
        /// <c>false</c> в противном случае.
        /// </returns>
        protected abstract ValueTask<bool> CanViewCardStorageAsync(ITessaView view, IViewMetadata viewMetadata);

        #endregion

        #region Base Overrides

        public override Task InitializingLocal(ITileLocalExtensionContext context)
        {
            ITile others = context.Workspace.LeftPanel.Tiles.TryGet(TileNames.ViewsOther);
            if (others != null)
            {
                ITile delete = others.Tiles.TryGet(TileNames.DeleteCardFromView);
                if (delete != null)
                {
                    delete.Evaluating += this.DeleteEvaluating;
                }

                ITile export = others.Tiles.TryGet(TileNames.ExportCardFromView);
                if (export != null)
                {
                    export.Evaluating += this.ExportEvaluating;
                }

                ITile exportAll = others.Tiles.TryGet(TileNames.ExportAllCardsFromView);
                if (exportAll != null)
                {
                    exportAll.Evaluating += this.ExportEvaluating;
                }

                ITile viewStorage = others.Tiles.TryGet(TileNames.ViewStorageFromView);
                if (viewStorage != null)
                {
                    viewStorage.Evaluating += this.ViewCardStorageEvaluating;
                }
            }

            return Task.CompletedTask;
        }

        #endregion
    }
}