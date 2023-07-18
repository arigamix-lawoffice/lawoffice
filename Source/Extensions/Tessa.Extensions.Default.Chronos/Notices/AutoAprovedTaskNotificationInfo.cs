using System;
using Tessa.Platform;

namespace Tessa.Extensions.Default.Chronos.Notices
{
    public class AutoAprovedTaskNotificationInfo : TaskNotificationStorage, ITaskNotificationInfo
    {
        #region Constructors

        public AutoAprovedTaskNotificationInfo()
            : base()
        {
            this.Init(nameof(this.ID), GuidBoxes.Empty);
            this.Init(nameof(this.Date), null);
            this.Init(nameof(this.Comment), null);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Идентификатор записи
        /// </summary>
        public Guid ID
        {
            get { return this.Get<Guid>(nameof(this.ID)); }
            set { this.Set(nameof(this.ID), value); }
        }

        /// <summary>
        /// Дата автоматического завершения
        /// </summary>
        public DateTime? Date
        {
            get { return this.Get<DateTime?>(nameof(this.Date)); }
            set { this.Set(nameof(this.Date), value); }
        }

        /// <summary>
        /// Комментарий, с которым было завершено задание
        /// </summary>
        public string Comment
        {
            get { return this.Get<string>(nameof(this.Comment)); }
            set { this.Set(nameof(this.Comment), value); }
        }

        #endregion
    }
}