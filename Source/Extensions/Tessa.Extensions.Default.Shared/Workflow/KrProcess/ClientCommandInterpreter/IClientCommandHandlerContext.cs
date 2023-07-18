using System.Collections.Generic;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess.ClientCommandInterpreter
{
    /// <summary>
    /// Описывает контекст клиентской команды.
    /// </summary>
    public interface IClientCommandHandlerContext
        : IExtensionContext
    {
        /// <summary>
        /// Возвращает или задаёт текущую клиентскую команду.
        /// </summary>
        KrProcessClientCommand Command { get; set; }

        /// <summary>
        /// Возвращает или задаёт внешний контекст, в котором запущено выполнение. Может быть <c>null</c>.
        /// </summary>
        object OuterContext { get; set; }
        
        /// <summary>
        /// Возвращает или задаёт дополнительную информацию.
        /// </summary>
        IDictionary<string, object> Info { get; set; }
        
    }
}