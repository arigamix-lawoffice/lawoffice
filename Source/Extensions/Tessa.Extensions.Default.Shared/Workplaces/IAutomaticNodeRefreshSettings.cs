#nullable enable

namespace Tessa.Extensions.Default.Shared.Workplaces
{
    /// <summary>
    /// Описание интерфейса настроек автоматического обновления узлов рабочего места.
    /// </summary>
    public interface IAutomaticNodeRefreshSettings
    {
        /// <summary>
        /// Интервал автоматического обновления в секундах.
        /// </summary>
        int RefreshInterval { get; set; }

        /// <summary>
        /// Признак необходимости обновления табличных данных.
        /// </summary>
        bool WithContentDataRefreshing { get; set; }
    }
}
