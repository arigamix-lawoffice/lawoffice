using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tessa.Extensions.Default.Chronos.FileConverters
{
    /// <summary>
    /// Тип конвертера для преобразования файлов в другие форматы
    /// </summary>
    public enum FileConverterType
    {
        /// <summary>
        /// Не используется
        /// </summary>
        None = 0,
        /// <summary>
        /// Используется Libre/OpenOffice
        /// </summary>
        LibreOffice = 1,
        /// <summary>
        /// Используется сервис
        /// </summary>
        OnlyOfficeService = 2,
        /// <summary>
        /// Используется Document Builder
        /// </summary>
        OnlyOfficeDocumentBuilder = 3
    }
}
