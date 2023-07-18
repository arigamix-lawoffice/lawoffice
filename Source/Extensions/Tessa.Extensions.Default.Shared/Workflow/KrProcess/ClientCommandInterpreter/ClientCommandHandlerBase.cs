using System.Threading.Tasks;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess.ClientCommandInterpreter
{
    /// <summary>
    /// Предоставляет базовую реалиацию обработчика клиентской команды.
    /// </summary>
    public abstract class ClientCommandHandlerBase : IClientCommandHandler
    {
        /// <inheritdoc />
        public virtual Task Handle(
            IClientCommandHandlerContext context)
            => Task.CompletedTask;
    }
}