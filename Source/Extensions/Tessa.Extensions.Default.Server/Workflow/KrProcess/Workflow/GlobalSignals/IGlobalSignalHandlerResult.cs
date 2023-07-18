namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.GlobalSignals
{
    /// <summary>
    /// Описывает результат обработки глобального сигнала.
    /// </summary>
    public interface IGlobalSignalHandlerResult
    {
        /// <summary>
        /// Возвращает значение, показывающее, что сигнал был обработан.
        /// </summary>
        bool Handled { get; }

        /// <summary>
        /// Возвращает значение, показывающее, необходимо ли после обработки сигнала продолжать текущее выполнение runner-a.
        /// Значение <c>false</c>, если текущее выполнение должно быть прервано.
        /// Если запланировано еще одно сохранение карточки, приводящее к запуску runner-a, то runner будет запущен с текущим KrScope.
        /// </summary>
        bool ContinueCurrentRun { get; }

        /// <summary>
        /// Возвращает значение, показывающее, необходимо ли начать выполнение этапов из следующей группы этапов, несмотря на то, что в текущей группе этапов остались не обработанные этапы.
        /// </summary>
        bool ForceStartGroup { get; }
    }
}