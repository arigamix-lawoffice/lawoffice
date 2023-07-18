namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess
{
    /// <summary>
    /// Описывает параметры запуска процесса.
    /// </summary>
    public interface IKrProcessLauncherSpecificParameters
    {
        /// <summary>
        /// Возвращает или задаёт значение флага, показывающего, следует ли создавать ошибку, если процесс не может быть выполнен из-за ограничений (параметр вторичного процесса "Сообщение при невозможности выполнения процесса").
        /// </summary>
        bool RaiseErrorWhenExecutionIsForbidden { get; set; }
    }
}