using System.Threading.Tasks;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess.ClientCommandInterpreter
{
    /// <summary>
    /// Описывает обработчик клиентской команды.
    /// </summary>
    public interface IClientCommandHandler
    {
        /// <summary>
        /// Обработать клиентскую команду.
        /// </summary>
        /// <param name="context">Контекст клиентской команды.</param>
        /// <returns>Асинхронная задача.</returns>
        Task Handle(
            IClientCommandHandlerContext context);
    }
}