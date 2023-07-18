using System.Threading.Tasks;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Localization;
using Tessa.UI;
using Tessa.UI.Cards;
using Tessa.UI.Tiles;

namespace Tessa.Extensions.Default.Client.Workflow.KrProcess
{
    /// <summary>
    /// Команда выполняющаяся при клике по локальному тайлу подсистемы маршрутов.
    /// </summary>
    public sealed class KrLocalTileCommand : IKrTileCommand
    {
        private readonly IKrProcessLauncher launcher;

        public KrLocalTileCommand(
            IKrProcessLauncher launcher)
        {
            this.launcher = launcher;
        }

        /// <inheritdoc />
        public async Task OnClickAsync(
            IUIContext context,
            ITile tile,
            KrTileInfo tileInfo)
        {
            var cardEditor = context?.CardEditor;
            if (tileInfo.ID == default
                || cardEditor == null)
            {
                return;
            }

            if (tileInfo.AskConfirmation
                && !TessaDialog.Confirm(LocalizationManager.Format(tileInfo.ConfirmationMessage)))
            {
                return;
            }

            using (TessaSplash.Create("$KrButton_DefaultTileSplash"))
            using (cardEditor.SetOperationInProgress(blocking: true))
            {
                // Двойное сохранение, если в карточке есть изменения. Сперва делается основное сохранение, затем запуск процесса.
                if (await cardEditor.CardModel.HasChangesAsync())
                {
                    if (!await cardEditor.SaveCardAsync(
                            context,
                            request: new CardSavingRequest(CardSavingMode.RefreshOnSuccess)))
                    {
                        return;
                    }
                }

                var process = KrProcessBuilder
                    .CreateProcess()
                    .SetProcess(tileInfo.ID)
                    .SetCard(context.CardEditor.CardModel.Card.ID)
                    .Build();

                await this.launcher.LaunchWithCardEditorAsync(process, cardEditor, true);
            }
        }
    }
}