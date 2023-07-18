using System;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess
{
    /// <summary>
    /// Запись для листа согласования, соответствующая файлу, изменённому в процессе выполнения задания.
    /// </summary>
    public sealed class KrHistoryFileItem
    {
        #region Properties

        /// <summary>
        /// Идентификатор файла.
        /// </summary>
        public Guid FileID { get; set; }

        /// <summary>
        /// Идентификатор версии файла.
        /// </summary>
        public Guid VersionRowID { get; set; }

        /// <summary>
        /// Имя файла.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Дата изменения файла, т.е. дата создания последней версии.
        /// </summary>
        public DateTime Modified { get; set; }

        #endregion
    }
}
