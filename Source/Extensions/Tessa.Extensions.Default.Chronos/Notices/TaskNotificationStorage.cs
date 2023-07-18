using System;
using System.Collections.Generic;
using Tessa.Platform;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Chronos.Notices
{
    public abstract class TaskNotificationStorage : StorageObject, ITaskNotificationInfo
    {
        #region Constructors

        public TaskNotificationStorage()
            : base(new Dictionary<string, object>(StringComparer.Ordinal))
        {
            this.Init(nameof(this.CardID), GuidBoxes.Empty);
            this.Init(nameof(this.UserID), GuidBoxes.Empty);
            this.Init(nameof(this.UserName), null);
            this.Init(nameof(this.LinkText), null);
            this.Init(nameof(this.Link), null);
            this.Init(nameof(this.WebLink), null);
        }

        #endregion

        #region ITaskNotificationInfo Members

        /// <summary>
        /// ID карточки.
        /// </summary>
        public Guid CardID
        {
            get { return this.Get<Guid>(nameof(this.CardID)); }
            set { this.Set(nameof(this.CardID), value); }
        }

        /// <summary>
        /// Исполнитель.
        /// </summary>
        public Guid UserID
        {
            get { return this.Get<Guid>(nameof(this.UserID)); }
            set { this.Set(nameof(this.UserID), value); }
        }

        /// <summary>
        /// Исполнитель.
        /// </summary>
        public string UserName
        {
            get { return this.Get<string>(nameof(this.UserName)); }
            set { this.Set(nameof(this.UserName), value); }
        }

        /// <summary>
        /// Текст для ссылки.
        /// </summary>
        public string LinkText
        {
            get { return this.Get<string>(nameof(this.LinkText)); }
            set { this.Set(nameof(this.LinkText), value); }
        }

        /// <summary>
        /// Ссылка.
        /// </summary>
        public string Link
        {
            get { return this.Get<string>(nameof(this.Link)); }
            set { this.Set(nameof(this.Link), value); }
        }

        /// <summary>
        /// Ссылка на веб-клиент.
        /// </summary>
        public string WebLink
        {
            get { return this.Get<string>(nameof(this.WebLink)); }
            set { this.Set(nameof(this.WebLink), value); }
        }

        #endregion
    }
}
