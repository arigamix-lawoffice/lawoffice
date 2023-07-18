using System;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess
{
    /// <summary>
    /// Запись для листа согласования, соответствующая текущему заданию.
    /// </summary>
    public sealed class KrTaskItem
    {
        #region Properties

        /// <summary>
        /// Роль, на которую назначено задание.
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// Пользователь, взявший задание в работу,
        /// или пустая строка, если задание ещё не взято в работу.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Дата создание задания.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Дата взятия задания в работу
        /// или <c>null</c>, если задание ещё не взято в работу.
        /// </summary>
        public DateTime? InProgress { get; set; }

        /// <summary>
        /// Количество квантов календаря с момента создания задания,
        /// или <c>null</c>, если кванты календаря не рассчитаны.
        /// </summary>
        public int? CreatedQuants { get; set; }

        /// <summary>
        /// Количество квантов календаря, в течение которых задание находится в работе,
        /// или <c>null</c>, если задание ещё не взято в работу или кванты календаря не рассчитаны.
        /// </summary>
        public int? InProgressQuants { get; set; }

        /// <summary>
        /// Количество квантов календаря, оставшихся до запланированного завершения задания,
        /// или <c>null</c>, если кванты календаря не рассчитаны.
        /// </summary>
        public int? RemainingQuants { get; set; }

        #endregion
    }
}
