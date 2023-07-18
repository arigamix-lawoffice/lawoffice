using System.Collections.Generic;
using System.Threading;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess.ClientCommandInterpreter
{
    /// <summary>
    /// Представляет контекст клиентской команды.
    /// </summary>
    public sealed class ClientCommandHandlerContext : IClientCommandHandlerContext
    {
        /// <inheritdoc />
        public KrProcessClientCommand Command { get; set; }

        /// <inheritdoc />
        public object OuterContext { get; set; }

        /// <inheritdoc />
        public IDictionary<string, object> Info { get; set; }

        /// <inheritdoc />
        public CancellationToken CancellationToken { get; set; }
    }
}