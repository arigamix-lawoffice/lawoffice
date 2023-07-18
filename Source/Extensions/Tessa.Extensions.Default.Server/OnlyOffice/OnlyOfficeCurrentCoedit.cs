using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tessa.Extensions.Default.Server.OnlyOffice
{
    public class OnlyOfficeCurrentCoedit
    {
        /// <summary>
        /// Идентификатор версии файла
        /// </summary>
        public Guid SourceFileVersionID { get; set; }
        /// <summary>
        /// Ключ для совместного редактирования
        /// </summary>
        public string CoeditKey { get; set; }
        /// <summary>
        /// Время последнего обращения
        /// </summary>
        public DateTime LastAccessTime { get; set; }
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name { get; set; }
    }
}
