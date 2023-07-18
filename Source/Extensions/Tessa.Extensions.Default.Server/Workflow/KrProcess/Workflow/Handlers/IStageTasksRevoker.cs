using System.Threading.Tasks;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.Handlers
{
    /// <summary>
    /// Описывает объект выполняющий отзыв заданий этапа.
    /// </summary>
    public interface IStageTasksRevoker
    {
        /// <summary>
        /// Отзывает все задания этапа.
        /// </summary>
        /// <param name="context">Контекст <see cref="IStageTasksRevoker"/>.</param>
        /// <returns>Значение <see langword="true"/>, если заданий больше нет и прерывание этапа завершено, иначе - <see langword="false"/>.</returns>
        Task<bool> RevokeAllStageTasksAsync(IStageTasksRevokerContext context);

        /// <summary>
        /// Отзывает задание идентификатор которого указан в свойстве <see cref="IStageTasksRevokerContext.TaskID"/>.
        /// </summary>
        /// <param name="context">Контекст <see cref="IStageTasksRevoker"/>.</param>
        /// <returns>Значение <see langword="true"/>, если задания с указанным идентификатором в карточке нет, иначе - <see langword="false"/>.</returns>
        Task<bool> RevokeTaskAsync(IStageTasksRevokerContext context);

        /// <summary>
        /// Отзывает все задания идентификаторы которых указаны в свойстве <see cref="IStageTasksRevokerContext.TaskIDs"/>.
        /// </summary>
        /// <param name="context">Контекст <see cref="IStageTasksRevoker"/>.</param>
        /// <returns>Число отозванных заданий.</returns>
        Task<int> RevokeTasksAsync(IStageTasksRevokerContext context);
    }
}