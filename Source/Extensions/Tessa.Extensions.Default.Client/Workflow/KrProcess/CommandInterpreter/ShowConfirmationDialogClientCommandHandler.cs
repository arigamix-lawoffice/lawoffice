using System.Threading.Tasks;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess.ClientCommandInterpreter;
using Tessa.Platform.Validation;
using Tessa.UI;

namespace Tessa.Extensions.Default.Client.Workflow.KrProcess.CommandInterpreter
{
    /// <summary>
    /// Обработчик клиентской команды <see cref="DefaultCommandTypes.ShowConfirmationDialog"/>.
    /// </summary>
    public sealed class ShowConfirmationDialogClientCommandHandler: ClientCommandHandlerBase
    {
        #region Base Overrides

        /// <inheritdoc/>
        public override async Task Handle(
            IClientCommandHandlerContext context)
        {
            var command = context.Command;
            if (command.Parameters.TryGetValue("text", out var textObj)
                && textObj is string text
                && !string.IsNullOrWhiteSpace(text))
            {
                await TessaDialog.ShowNotEmptyAsync(ValidationResult.FromText(text));
            }
        }

        #endregion
    }
}