using System.Threading.Tasks;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess.ClientCommandInterpreter;

namespace Tessa.Extensions.Default.Client.Workflow.KrProcess.Requests
{
    public sealed class KrClientCommandCustomExtension: CardRequestExtension
    {
        private readonly IClientCommandInterpreter interpreter;

        public KrClientCommandCustomExtension(
            IClientCommandInterpreter interpreter)
        {
            this.interpreter = interpreter;
        }

        /// <inheritdoc />
        public override async Task AfterRequest(
            ICardRequestExtensionContext context)
        {
            if (context.RequestIsSuccessful
                && context.Response.TryGetKrProcessClientCommands(out var clientCommands))
            {
                await this.interpreter.InterpretAsync(clientCommands, context, context.CancellationToken);
            }
        }
    }
}