using System.Threading.Tasks;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Events
{
    /// <summary>
    /// Базовый абстрактный класс расширения для события подсистемы маршрутов.
    /// </summary>
    public abstract class KrEventExtension : IKrEventExtension
    {
        /// <inheritdoc />
        public virtual Task HandleEvent(IKrEventExtensionContext context) => Task.CompletedTask;
    }
}