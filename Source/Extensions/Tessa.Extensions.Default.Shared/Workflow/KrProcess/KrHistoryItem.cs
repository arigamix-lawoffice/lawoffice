using System;
using System.Collections.Generic;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess
{
    /// <summary>
    /// Запись для листа согласования, соответствующая выполненному заданию.
    /// </summary>
    public sealed class KrHistoryItem
    {
        #region Properties

        /// <summary>
        /// Идентификатор задания.
        /// </summary>
        public Guid RowID { get; set; }

        /// <summary>
        /// Номер цикла согласования.
        /// </summary>
        public int Cycle { get; set; }

        /// <summary>
        /// Дата создания задания.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Запланированная дата выполнения задания.
        /// </summary>
        public DateTime Planned { get; set; }

        /// <summary>
        /// Фактическая дата выполнения задания.
        /// </summary>
        public DateTime? Completed { get; set; }

        /// <summary>
        /// Выбранный вариант завершения.
        /// </summary>
        public string OptionCaption { get; set; }

        /// <summary>
        /// Роль, на которую было назначено задание.
        /// </summary>
        public string RoleName { get; set; }
        
        /// <summary>
        /// Имя пользователя, который завершил задание.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Должность сотрудника на которого назначено задание.
        /// </summary>
        public string UserPosition { get; set; }

        /// <summary>
        /// Департамент сотрудника на которого назначено задание.
        /// </summary>
        public string UserDepartment { get; set; }

        /// <summary>
        /// Имя пользователя, который считается автором задания.
        /// </summary>
        public string AuthorName { get; set; }

        /// <summary>
        /// Описание результата выполненного задания. Строка никогда не равна <c>null</c>.
        /// </summary>
        public string Result { get; set; }

        /// <summary>
        /// Количество квантов календаря, которое потребовалось для завершения задания,
        /// или <c>null</c>, если кванты календаря не рассчитаны.
        /// </summary>
        public int? CompletedQuants { get; set; }

        /// <summary>
        /// Список информации по файлам, изменённым в процессе выполнения задания,
        /// или <c>null</c>, если файлы отсутствуют.
        /// </summary>
        public List<KrHistoryFileItem> Files { get; set; }

        /// <summary>
        /// Тип задания.
        /// </summary>
        public Guid TaskTypeID { get; set; }

        /// <summary>
        /// ID календаря.
        /// </summary>
        public Guid? CalendarID { get; set; }

        #endregion
    }
}
