using System.Threading.Tasks;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Events
{
    /// <summary>
    /// Расширение для события подсистемы маршрутов.
    /// </summary>
    public interface IKrEventExtension : IExtension
    {
        /// <summary>
        /// Обработка события KrProcess
        /// </summary>
        /// <param name="context">Контекст расширений.</param>
        /// <returns>Асинхронная задача.</returns>
        Task HandleEvent(IKrEventExtensionContext context);
    }
}