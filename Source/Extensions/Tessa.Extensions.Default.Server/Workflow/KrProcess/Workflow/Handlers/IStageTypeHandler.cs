using System.Threading.Tasks;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.Handlers
{
    /// <summary>
    /// Описывает обработчик этапа.
    /// </summary>
    public interface IStageTypeHandler
    {
        /// <summary>
        /// Метод, вызываемый перед вызовом скрипта инициализации этапа.
        /// </summary>
        /// <param name="context">Контекст обработчика этапа.</param>
        /// <returns>Асинхронная задача.</returns>
        Task BeforeInitializationAsync(
            IStageTypeHandlerContext context);

        /// <summary>
        /// Метод, вызываемый после вызова скрипта постобработки этапа.
        /// </summary>
        /// <param name="context">Контекст обработчика этапа.</param>
        /// <returns>Асинхронная задача.</returns>
        Task AfterPostprocessingAsync(
            IStageTypeHandlerContext context);
        
        /// <summary>
        /// Обработка старта этапа. 
        /// </summary>
        /// <param name="context">Контекст обработчика этапа.</param>
        /// <returns>Результат выполнения этапа.</returns>
        Task<StageHandlerResult> HandleStageStartAsync(
            IStageTypeHandlerContext context);

        /// <summary>
        /// Обработка завершения задания.
        /// </summary>
        /// <param name="context">Контекст обработчика этапа.</param>
        /// <returns>Результат выполнения этапа.</returns>
        Task<StageHandlerResult> HandleTaskCompletionAsync(
            IStageTypeHandlerContext context);

        /// <summary>
        /// Обработка возврата задания на роль.
        /// </summary>
        /// <param name="context">Контекст обработчика этапа.</param>
        /// <returns>Результат выполнения этапа.</returns>
        Task<StageHandlerResult> HandleTaskReinstateAsync(
            IStageTypeHandlerContext context);

        /// <summary>
        /// Обработка сигнала.
        /// </summary>
        /// <param name="context">Контекст обработчика этапа.</param>
        /// <returns>Результат выполнения этапа.</returns>
        Task<StageHandlerResult> HandleSignalAsync(
            IStageTypeHandlerContext context);

        /// <summary>
        /// Обработка восстановления процесса.
        /// </summary>
        /// <param name="context">Контекст обработчика этапа.</param>
        /// <returns>Результат выполнения этапа.</returns>
        Task<StageHandlerResult> HandleResurrectionAsync(
            IStageTypeHandlerContext context);
        
        /// <summary>
        /// Обработка отмены этапа.
        /// Данный метод должен утилизировать все используемые этапом ресурсы.
        /// </summary>
        /// <param name="context">Контекст обработчика этапа.</param>
        /// <returns>Значение <see langword="true"/>, если выполнение этапа завершено и выполнение процесса может быть продолжено, иначе <see langword="false"/>.</returns>
        Task<bool> HandleStageInterruptAsync(
            IStageTypeHandlerContext context);
    }
}