using System.Collections.Generic;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;

namespace Tessa.Extensions.Default.Client.Workflow.KrProcess
{
    /// <summary>
    /// Контейнер, содержащий информацию о тайлах глобальных вторичных процессов.
    /// </summary>
    public interface IKrGlobalTileContainer
    {
        /// <summary>
        /// Инициализирует контейнер заданной коллекцией.
        /// </summary>
        /// <param name="globalTiles">Коллекция, содержащая информацию о тайлах.</param>
        void Init(IReadOnlyList<KrTileInfo> globalTiles);

        /// <summary>
        /// Возвращает сохранённую в контейнере информацию о тайлах.
        /// </summary>
        /// <returns>Коллекция, содержащая информацию о тайлах или значение <see langword="null"/>, если контейнер не инициализирован.</returns>
        IReadOnlyList<KrTileInfo> GetTileInfos();
    }
}
