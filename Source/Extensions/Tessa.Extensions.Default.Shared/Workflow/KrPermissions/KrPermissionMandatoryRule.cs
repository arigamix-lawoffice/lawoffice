using System;
using System.Collections.Generic;

namespace Tessa.Extensions.Default.Shared.Workflow.KrPermissions
{
    /// <summary>
    /// Расширенные настройки правила обязательности полей.
    /// </summary>
    public sealed class KrPermissionMandatoryRule
    {
        #region Fields

        private ICollection<Guid> columnIDs;
        private ICollection<Guid> taskTypes;
        private ICollection<Guid> completionOptions;

        #endregion

        #region Constructors

        public KrPermissionMandatoryRule(
            Guid sectionID,
            string text,
            int validationType)
        {
            this.SectionID = sectionID;
            this.Text = text;
            this.ValidationType = validationType;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Идентификатор секции.
        /// </summary>
        public Guid SectionID { get; }

        /// <summary>
        /// Определяет, проверяется ли обязательность полей секции.
        /// </summary>
        public bool HasColumns => this.columnIDs?.Count > 0;

        /// <summary>
        /// Список полей, обязательность которых проверяется.
        /// </summary>
        public ICollection<Guid> ColumnIDs => this.columnIDs ??= new List<Guid>();

        /// <summary>
        /// Текст ошибки обязательности.
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// Тип проверки обязательности.
        /// </summary>
        public int ValidationType { get; }

        /// <summary>
        /// Определяет, относится ли данное правило к заданиям.
        /// </summary>
        public bool HasTaskTypes => this.taskTypes?.Count > 0;

        /// <summary>
        /// Список типов заданий, к которым относятся данные настройки.
        /// </summary>
        public ICollection<Guid> TaskTypes => this.taskTypes ??= new List<Guid>();

        /// <summary>
        /// Определяет, к каким вариантам завершения относятся данные настройки.
        /// </summary>
        public bool HasCompletionOptions => this.taskTypes?.Count > 0;

        /// <summary>
        /// Список вариантов завершения, к которым относятся данные настройки.
        /// </summary>
        public ICollection<Guid> CompletionOptions => this.completionOptions ??= new List<Guid>();

        #endregion
    }
}
