using System;

namespace Tessa.Extensions.Default.Chronos.Workflow
{
    public sealed class KrAutoApproveAdditionalTaskRecord
    {
        #region Constructors

        public KrAutoApproveAdditionalTaskRecord(
            Guid taskID,
            string userName,
            string userPosition,
            bool isResponsible,
            string comment,
            bool isCompleted,
            Guid? optionID)
        {
            this.taskID = taskID;
            this.userName = userName;
            this.userPosition = userPosition;
            this.isResponsible = isResponsible;
            this.comment = comment;
            this.isCompleted = isCompleted;
            this.optionID = optionID;
        }

        #endregion

        #region Properties

        private readonly Guid taskID;
        /// <summary>
        /// Идентификатор задания
        /// </summary>
        public Guid TaskID
        {
            get { return this.taskID; }
        }

        private readonly string userName;
        /// <summary>
        /// Имя исполнителя
        /// </summary>
        public string UserName
        {
            get { return this.userName; }
        }

        private readonly string userPosition;
        /// <summary>
        /// Должность исполнителя
        /// </summary>
        public string UserPosition
        {
            get { return this.userPosition; }
        }

        private readonly bool isResponsible;
        /// <summary>
        /// Признак того, что задание ответсвенного исполниля
        /// </summary>
        public bool IsResponsible
        {
            get { return this.isResponsible; }
        }

        private readonly string comment;
        /// <summary>
        /// Комментарий исполниля
        /// </summary>
        public string Сomment
        {
            get { return this.comment; }
        }

        private readonly bool isCompleted;
        /// <summary>
        /// Признак того, что задание завершено
        /// </summary>
        public bool IsCompleted
        {
            get { return this.isCompleted; }
        }

        private readonly Guid? optionID;
        /// <summary>
        /// ID варианта завершения
        /// </summary>
        public Guid? OptionID
        {
            get { return this.optionID; }
        }

        #endregion
    }
}