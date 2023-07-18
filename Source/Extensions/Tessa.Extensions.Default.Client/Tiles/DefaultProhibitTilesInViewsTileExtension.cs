using System.Threading.Tasks;
using Tessa.Extensions.Default.Shared;
using Tessa.Views;
using Tessa.Views.Metadata;

namespace Tessa.Extensions.Default.Client.Tiles
{
    /// <summary>
    /// Запрещает системные плитки "Удалить", "Экспорт" и "Показать структуру" для типовых представлений,
    /// перечисленных в <see cref="DefaultViewAliases"/>.
    /// </summary>
    public sealed class DefaultProhibitTilesInViewsTileExtension :
        ProhibitTilesInViewsTileExtension
    {
        #region Base Overrides

        protected override ValueTask<bool> CanDeleteCardAsync(ITessaView view, IViewMetadata viewMetadata) =>
            new ValueTask<bool>(DefaultViewAliases.CanDeleteCard(viewMetadata.Alias));

        protected override ValueTask<bool> CanExportCardAsync(ITessaView view, IViewMetadata viewMetadata) =>
            new ValueTask<bool>(DefaultViewAliases.CanExportCard(viewMetadata.Alias));

        protected override ValueTask<bool> CanViewCardStorageAsync(ITessaView view, IViewMetadata viewMetadata) =>
            new ValueTask<bool>(DefaultViewAliases.CanViewCardStorage(viewMetadata.Alias));

        #endregion
    }
}