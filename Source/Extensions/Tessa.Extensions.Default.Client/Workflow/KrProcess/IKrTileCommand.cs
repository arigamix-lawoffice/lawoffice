using System.Threading.Tasks;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.UI;
using Tessa.UI.Tiles;

namespace Tessa.Extensions.Default.Client.Workflow.KrProcess
{
    /// <summary>
    /// Описывает действие выполняющееся по клику по тайлу использующемуся в подсистеме маршрутов.
    /// </summary>
    public interface IKrTileCommand
    {
        /// <summary>
        /// Выполняет действие по клику по тайлу использующегося в подсистеме маршрутов.
        /// </summary>
        /// <param name="context">Контекст операции с пользовательским интерфейсом.</param>
        /// <param name="tile">Плитка, по которой был выполнен клик.</param>
        /// <param name="tileInfo">Информация о плитке <paramref name="tile"/>.</param>
        /// <returns>Асинхронная задача.</returns>
        Task OnClickAsync(
            IUIContext context,
            ITile tile,
            KrTileInfo tileInfo);
    }
}