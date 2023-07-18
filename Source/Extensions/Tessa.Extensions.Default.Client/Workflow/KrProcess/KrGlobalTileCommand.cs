using System.Threading.Tasks;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Localization;
using Tessa.UI;
using Tessa.UI.Tiles;

namespace Tessa.Extensions.Default.Client.Workflow.KrProcess
{
    /// <summary>
    /// Команда выполняющаяся при клике по глобальному тайлу подсистемы маршрутов.
    /// </summary>
    public sealed class KrGlobalTileCommand : IKrTileCommand
    {
        private readonly IKrProcessLauncher launcher;


        public KrGlobalTileCommand(
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
            if (tileInfo.ID == default)
            {
                return;
            }

            if (tileInfo.AskConfirmation
                && !TessaDialog.Confirm(LocalizationManager.Format(tileInfo.ConfirmationMessage)))
            {
                return;
            }

            using (TessaSplash.Create("$KrButton_DefaultTileSplash"))
            {
                var process = KrProcessBuilder
                    .CreateProcess()
                    .SetProcess(tileInfo.ID)
                    .Build();

                var specificParameters = new KrProcessUILauncher.SpecificParameters
                {
                    RaiseErrorWhenExecutionIsForbidden = true
                };

                var result = await this.launcher.LaunchAsync(process, specificParameters: specificParameters);

                TessaDialog.ShowNotEmpty(result.ValidationResult);
            }
        }
    }
}