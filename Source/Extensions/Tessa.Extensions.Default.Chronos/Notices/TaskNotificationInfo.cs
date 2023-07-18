using System;
using Tessa.Platform;

namespace Tessa.Extensions.Default.Chronos.Notices
{
    /// <summary>
    /// Информация необходимая для построения уведомления по заданию.
    /// </summary>
    public class TaskNotificationInfo : TaskNotificationStorage, ITaskNotificationInfo
    {
        #region Constructors

        public TaskNotificationInfo()
            :base()
        {
            this.Init(nameof(this.CardNumber), null);
            this.Init(nameof(this.CardSubject), null);
            this.Init(nameof(this.InProgress), Int32Boxes.Zero);
            this.Init(nameof(this.TaskID), GuidBoxes.Empty);
            this.Init(nameof(this.Created), null);
            this.Init(nameof(this.Planned), null);
            this.Init(nameof(this.TaskInfo), null);
            this.Init(nameof(this.AuthorRole), null);
            this.Init(nameof(this.TypeCaption), null);
            this.Init(nameof(this.AutoApproveString), null);
            this.Init(nameof(this.AutoApproveDate), null);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Номер карточки.
        /// </summary>
        public string CardNumber
        {
            get { return this.Get<string>(nameof(this.CardNumber)); }
            set { this.Set(nameof(this.CardNumber), value); }
        }

	    /// <summary>
	    /// Тема карточки.
	    /// </summary>
        public string CardSubject
        {
            get { return this.Get<string>(nameof(this.CardSubject)); }
            set { this.Set(nameof(this.CardSubject), value); }
        }

        /// <summary>
        /// Признак, что задание взято в работу.
        /// </summary>
	    public int InProgress
        {
            get { return this.Get<int>(nameof(this.InProgress)); }
            set { this.Set(nameof(this.InProgress), value); }
        }

        /// <summary>
        /// ID задания.
        /// </summary>
        public Guid TaskID
        {
            get { return this.Get<Guid>(nameof(this.TaskID)); }
            set { this.Set(nameof(this.TaskID), value); }
        }

        /// <summary>
        /// Дата создания.
        /// </summary>
	    public DateTime? Created
        {
            get { return this.Get<DateTime?>(nameof(this.Created)); }
            set { this.Set(nameof(this.Created), value); }
        }

        /// <summary>
        /// Запланированная дата выполнения.
        /// </summary>
	    public DateTime? Planned
        {
            get { return this.Get<DateTime?>(nameof(this.Planned)); }
            set { this.Set(nameof(this.Planned), value); }
        }

        /// <summary>
        /// Инормация о задании.
        /// </summary>
	    public string TaskInfo
        {
            get { return this.Get<string>(nameof(this.TaskInfo)); }
            set { this.Set(nameof(this.TaskInfo), value); }
        }

        /// <summary>
        /// Автор задания.
        /// </summary>
	    public string AuthorRole
        {
            get { return this.Get<string>(nameof(this.AuthorRole)); }
            set { this.Set(nameof(this.AuthorRole), value); }
        }

        /// <summary>
        /// Тип задания.
        /// </summary>
        public string TypeCaption
        {
            get { return this.Get<string>(nameof(this.TypeCaption)); }
            set { this.Set(nameof(this.TypeCaption), value); }
        }

        /// <summary>
        /// Строка продупреждающая об автозаврешении.
        /// </summary>
        public string AutoApproveString
        {
            get { return this.Get<string>(nameof(this.AutoApproveString)); }
            set { this.Set(nameof(this.AutoApproveString), value); }
        }

        /// <summary>
        /// Планируемое время автозаврешении.
        /// </summary>
        public DateTime? AutoApproveDate
        {
            get { return this.Get<DateTime?>(nameof(this.AutoApproveDate)); }
            set { this.Set(nameof(this.AutoApproveDate), value); }
        }

        #endregion
    }
}
