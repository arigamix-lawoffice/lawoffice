#region Usings

using System.Collections.Generic;
using Tessa.Properties.Resharper;

#endregion

namespace Tessa.Extensions.Default.Shared.Workplaces
{
    /// <summary>
    ///     Описание интерфейса поддерживаемых настроек фильтрации
    /// </summary>
    public interface ITreeItemFilteringSettings
    {
        #region Public properties

        /// <summary>
        ///     Список параметров
        /// </summary>
        [NotNull]
        List<string> Parameters { get; set; }

        /// <summary>
        ///     Список секций
        /// </summary>
        [NotNull]
        List<string> RefSections { get; set; }

        #endregion
    }
}