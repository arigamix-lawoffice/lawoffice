
using System.Threading.Tasks;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers.UserAPI
{
    /// <summary>
    /// Интерфейс скриптов элемента процесса.
    /// Под элементом процесса понимается группа, шаблон и этап в равной степени.
    /// </summary>
    public interface IKrProcessItemScript
    {
        /// <summary>
        /// Выполняет скрипт инициализации.
        /// </summary>
        /// <returns>Асинхронная задача.</returns>
        Task RunBeforeAsync();

        /// <summary>
        /// Скрипт инициализации.
        /// </summary>
        /// <returns>Асинхронная задача.</returns>
        Task BeforeAsync();

        /// <summary>
        /// Выполняет условие. 
        /// </summary>
        /// <returns>
        /// Признак того, что единица исполнения подтверждена.
        /// </returns>
        ValueTask<bool> RunConditionAsync();

        /// <summary>
        /// Условие выполнения.
        /// </summary>
        /// <returns>Признак того, что единица исполнения подтверждена.</returns>
        ValueTask<bool> ConditionAsync();

        /// <summary>
        /// Выполняет скрипт постобработки.
        /// </summary>
        /// <returns>Асинхронная задача.</returns>
        Task RunAfterAsync();

        /// <summary>
        /// Скрипт постобработки.
        /// </summary>
        /// <returns>Асинхронная задача.</returns>
        Task AfterAsync();
    }
}