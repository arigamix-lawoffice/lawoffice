#nullable enable

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Workflow;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow
{
    /// <summary>
    /// Объект, обеспечивающий взаимодействие с Workflow API.
    /// </summary>
    public interface IWorkflowAPIBridge
    {
        /// <summary>
        /// Отправляет задание.
        /// </summary>
        /// <param name="taskTypeID">Идентификатор типа задания.</param>
        /// <param name="digest">Дайджест задания.</param>
        /// <param name="roleID">Роль, на которую отправляется задание или значение <see langword="null"/>, если он будет задан позже.</param>
        /// <param name="roleName">Имя роли, на которую отправляется задание.</param>
        /// <param name="taskParameters">Параметры задания.</param>
        /// <param name="taskRowID">Опционально идентификатор задания.</param>
        /// <param name="modifyTaskAction">Действие, выполняемое перед отправкой задания.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Информация по заданию или значение по умолчанию для типа, если при создании здания произошла ошибка.</returns>
        Task<IWorkflowTaskInfo?> SendTaskAsync(
            Guid taskTypeID,
            string? digest,
            Guid? roleID,
            string? roleName = null,
            Dictionary<string, object?>? taskParameters = null,
            Guid? taskRowID = null,
            Func<CardTask, CancellationToken, ValueTask>? modifyTaskAction = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Добавляет задание с указанным идентификатором в список активных.
        /// </summary>
        /// <param name="taskID">Идентификатор задания.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить выполнения асинхронной задачи.</param>
        /// <returns>Асинхронная задача.</returns>
        Task AddActiveTaskAsync(
            Guid taskID,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Удаляет задание с указанным идентификатором из списка активных.
        /// </summary>
        /// <param name="taskID">Идентификатор задания.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить выполнения асинхронной задачи.</param>
        /// <returns>Значение true, если задание успешно удалено из списка активных, иначе - false.</returns>
        ValueTask<bool> TryRemoveActiveTaskAsync(
            Guid taskID,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Уделяет задание с указанным идентификатором из списка активных.
        /// В случае неудачи будет выброшено исключение
        /// </summary>
        /// <param name="taskID">Идентификатор задания.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить выполнения асинхронной задачи.</param>
        /// <returns>Асинхронная задача.</returns>
        ValueTask RemoveActiveTaskAsync(
            Guid taskID,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Возвращает список идентификаторов активных заданий.
        /// </summary>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить выполнения асинхронной задачи.</param>
        /// <returns>Список идентификаторов активных заданий.</returns>
        ValueTask<IReadOnlyList<Guid>> GetActiveTasksAsync(
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Инициализирует счётчик с заданным номером с указанием начального значения.
        /// </summary>
        /// <param name="counterNumber">Номер счётчика, уникальный в пределах подпроцесса.</param>
        /// <param name="initialValue">Начальное значение счётчика, которое должно быть больше нуля.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить выполнения асинхронной задачи.</param>
        /// <returns>Асинхронная задача.</returns>
        ValueTask InitCounterAsync(
            int counterNumber,
            int initialValue,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Уменьшает текущее значение счётчика на заданное значение <paramref name="decrementValue" />.
        /// Если текущее значение становится не больше нуля, то счётчик удаляется.
        /// </summary>
        /// <param name="counterNumber">Номер счётчика, уникальный в пределах подпроцесса.</param>
        /// <param name="decrementValue">Значение, на которое уменьшается текущее значение счётчика. Если значение меньше нуля, то текущее значение счётчика будет увеличено.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить выполнения асинхронной задачи.</param>
        /// <returns>Состояние счётчика после выполнения метода.</returns>
        ValueTask<WorkflowCounterState> DecrementCounterAsync(
            int counterNumber,
            int decrementValue = 1,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Удаляет счётчик с заданным номером, уникальным для подпроцесса.
        /// </summary>
        /// <param name="counterNumber">Номер счётчика, уникальный в пределах подпроцесса.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Значение true, если счётчик был найден и удалён; false в противном случае.</returns>
        ValueTask<bool> RemoveCounterAsync(
            int counterNumber,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Возвращает список процессов, ожидающих завершения.
        /// </summary>
        IList<IWorkflowProcessInfo> ProcessesAwaitingRemoval { get; }

        /// <summary>
        /// Возвращает запрос на следующее сохранение.
        /// </summary>
        CardStoreRequest NextRequest { get; }

        /// <summary>
        /// Возвращает флаг, показывающий, необходимо ли выполнить следующее сохранение.
        /// </summary>
        bool NextRequestPending { get; }

        /// <summary>
        /// Уведомляет WorkflowAPI о необходимости выполнения следующего сохранения.
        /// </summary>
        void NotifyNextRequestPending();
    }
}
