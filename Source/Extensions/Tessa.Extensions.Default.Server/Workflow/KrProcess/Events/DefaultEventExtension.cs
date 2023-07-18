using System.Threading.Tasks;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Events
{
    /// <summary>
    /// Расширение для события подсистемы маршрутов по умолчанию.
    /// </summary>
    public class DefaultEventExtension : KrEventExtension
    {
        /// <inheritdoc/>
        public override Task HandleEvent(IKrEventExtensionContext context) => Task.CompletedTask;
    }
}