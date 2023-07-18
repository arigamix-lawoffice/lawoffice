using Tessa.Extensions.Default.Shared.Workflow.KrProcess;

namespace Tessa.Test.Default.Shared.Kr
{
    /// <summary>
    /// Описывает методы выполняющие настройку типа карточки или документа.
    /// </summary>
    /// <typeparam name="T">Тип конфигуратора реализующий данный интерфейс.</typeparam>
    public interface ITypeConfigurator<T>
        where T : ITypeConfigurator<T>
    {
        /// <summary>
        /// Использовать согласование.
        /// </summary>
        /// <param name="useApproving">Значение <see langword="true"/>, если должны использоваться маршруты, иначе - <see langword="false"/>.</param>
        /// <param name="hideRouteTab">Значение <see langword="true"/>, если должна быть скрыт вкладка "Маршрут", иначе - <see langword="false"/>.</param>
        /// <param name="useRoutesInWorkflowEngine">Значение <see langword="true"/>, если должны использоваться маршруты в бизнес-процессах, иначе - <see langword="false"/>.</param>
        /// <returns>Объект <typeparamref name="T"/> для создания цепочки.</returns>
        T UseApproving(
            bool useApproving = true,
            bool hideRouteTab = default,
            bool useRoutesInWorkflowEngine = default);

        /// <summary>
        /// Использовать автоматическое согласование.
        /// </summary>
        /// <param name="useAutoApprove">Значение <see langword="true"/>, если должны автоматически согласовываться просроченные задания согласования, иначе - <see langword="false"/>.</param>
        /// <param name="exceededDays">Завершать при превышении срока более чем (рабочих дней).</param>
        /// <param name="notifyBefore">Уведомлять за N дней до завершения.</param>
        /// <param name="autoApproveComment">Комментарий при завершении.</param>
        /// <returns>Объект <typeparamref name="T"/> для создания цепочки.</returns>
        T UseAutoApprove(
            bool useAutoApprove = true,
            double exceededDays = default,
            double notifyBefore = default,
            string autoApproveComment = default);

        /// <summary>
        /// Использовать регистрацию.
        /// </summary>
        /// <param name="value">Значение <see langword="true"/>, если должна использоваться регистрация, иначе - <see langword="false"/>.</param>
        /// <returns>Объект <typeparamref name="T"/> для создания цепочки.</returns>
        T UseRegistration(
            bool value = true);

        /// <summary>
        /// Настраивает автоматическое выделение основного номера (проектного, если используется регистрация).
        /// </summary>
        /// <param name="autoAssignment">Режим выделения номера.</param>
        /// <param name="sequence">Последовательность из которой выделяется номер.</param>
        /// <param name="format">Формат полного номера.</param>
        /// <param name="allowManualAssignment">Значение <see langword="true"/>, если разрешено выделять номер вручную, иначе - <see langword="false"/>.</param>
        /// <param name="releaseNumber">Значение <see langword="true"/>, если номер должен быть освобождён при окончательном удалении карточки, иначе - <see langword="false"/>.</param>
        /// <returns>Объект <typeparamref name="T"/> для создания цепочки.</returns>
        T ConfigureDocNumberRegular(
            KrDocNumberRegularAutoAssignmentID autoAssignment,
            string sequence = default,
            string format = default,
            bool allowManualAssignment = default,
            bool releaseNumber = default);

        /// <summary>
        /// Настраивает автоматическое выделение регистрационного номера.
        /// </summary>
        /// <param name="autoAssignment">Режим выделения номера.</param>
        /// <param name="sequence">Последовательность из которой выделяется номер.</param>
        /// <param name="format">Формат полного номера.</param>
        /// <param name="allowManualAssignment">Значение <see langword="true"/>, если разрешено выделять номер вручную, иначе - <see langword="false"/>.</param>
        /// <param name="releaseNumber">Значение <see langword="true"/>, если номер должен быть освобождён при окончательном удалении карточки, иначе - <see langword="false"/>.</param>
        /// <returns>Объект <typeparamref name="T"/> для создания цепочки.</returns>
        T ConfigureDocNumberRegistration(
            KrDocNumberRegistrationAutoAssignmentID autoAssignment,
            string sequence = default,
            string format = default,
            bool allowManualAssignment = default,
            bool releaseNumber = default);

        /// <summary>
        /// Отключить проверку даты для подзадач.
        /// </summary>
        /// <param name="value">Значение <see langword="true"/>, если необходимо отключить проверку даты для подзадач, иначе - <see langword="false"/>.</param>
        /// <returns>Объект <typeparamref name="T"/> для создания цепочки.</returns>
        T DisableChildResolutionDateCheck(
            bool value = true);

        /// <summary>
        /// Использовать типовой процесс отправки задач.
        /// </summary>
        /// <param name="value">Значение <see langword="true"/>, если необходимо отключить проверку даты для подзадач, иначе - <see langword="false"/>.</param>
        /// <returns>Объект <typeparamref name="T"/> для создания цепочки.</returns>
        T UseResolutions(
            bool value = true);

        /// <summary>
        /// Использовать систему обсуждений.
        /// </summary>
        /// <param name="useForum">Значение <see langword="true"/>, если должна использоваться система обсуждений, иначе - <see langword="false"/>.</param>
        /// <param name="useDefaultForumTab">Значение <see langword="true"/>, если должна использоваться стандартная вкладка "Обсуждения", иначе - <see langword="false"/>.</param>
        /// <returns>Объект <typeparamref name="T"/> для создания цепочки.</returns>
        T UseForum(
            bool useForum = true,
            bool useDefaultForumTab = default);
    }
}