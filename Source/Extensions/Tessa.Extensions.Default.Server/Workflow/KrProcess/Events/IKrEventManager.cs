using System.Threading.Tasks;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Events
{
    /// <summary>
    /// Описывает объект предоставляющий методы для отправки событий маршрутов документов.
    /// </summary>
    public interface IKrEventManager
    {
        /// <summary>
        /// Создаёт событие маршрута документа.
        /// </summary>
        /// <param name="context">Контекст <see cref="IKrEventManager"/>.</param>
        /// <returns>Асинхронная задача.</returns>
        Task RaiseAsync(IKrEventExtensionContext context);
    }
}