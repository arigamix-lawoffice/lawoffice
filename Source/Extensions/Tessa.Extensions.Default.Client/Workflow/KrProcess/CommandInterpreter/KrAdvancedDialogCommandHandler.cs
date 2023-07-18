#nullable enable

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess.ClientCommandInterpreter;
using Tessa.Platform.Runtime;
using Tessa.UI;
using Tessa.UI.Cards;

namespace Tessa.Extensions.Default.Client.Workflow.KrProcess.CommandInterpreter
{
    /// <summary>
    /// Обработчик клиентской команды <see cref="DefaultCommandTypes.ShowAdvancedDialog"/>.
    /// </summary>
    public sealed class KrAdvancedDialogCommandHandler : AdvancedDialogCommandHandler
    {
        #region Fields

        private readonly IKrProcessLauncher launcher;

        #endregion

        #region Constructor

        public KrAdvancedDialogCommandHandler(
            IKrProcessLauncher launcher,
            Func<IAdvancedCardDialogManager> createAdvancedCardDialogManagerFunc,
            ISession session,
            ICardMetadata cardMetadata)
            : base(
                  createAdvancedCardDialogManagerFunc,
                  session,
                  cardMetadata) =>
            this.launcher = NotNullOrThrow(launcher);

        #endregion

        #region Base Overrides

        /// <inheritdoc />
        protected override CardTaskCompletionOptionSettings? PrepareDialogCommand(IClientCommandHandlerContext context)
        {
            var parameters = context.Command.Parameters;

            if (!parameters.ContainsKey(KrConstants.Keys.ProcessInstance))
            {
                return null;
            }

            if (!parameters.TryGetValue(KrConstants.Keys.CompletionOptionSettings, out var coSettingsObj)
                || coSettingsObj is not Dictionary<string, object?> coSettingsStorage)
            {
                return null;
            }

            return new CardTaskCompletionOptionSettings(coSettingsStorage);
        }

        /// <inheritdoc />
        protected override async ValueTask<bool> CompleteDialogCoreAsync(
            CardTaskDialogActionResult actionResult,
            IClientCommandHandlerContext context,
            ICardEditorModel cardEditor,
            ICardEditorModel? parentCardEditor = null)
        {
            var parameters = context.Command.Parameters;

            if (!parameters.TryGetValue(KrConstants.Keys.ProcessInstance, out var instanceStorageObj)
                || instanceStorageObj is not Dictionary<string, object> instanceStorage)
            {
                return true;
            }

            var processInstance = new KrProcessInstance(instanceStorage);
            Dictionary<string, object?>? requestInfo;

            if (parentCardEditor is not null)
            {
                requestInfo = null;
                var card = parentCardEditor.CardModel.Card;
                CardTaskDialogHelper.SetCardTaskDialogActionResult(card.Info, actionResult);
            }
            else
            {
                requestInfo = new Dictionary<string, object?>(StringComparer.Ordinal);
                CardTaskDialogHelper.SetCardTaskDialogActionResult(requestInfo, actionResult);
            }

            var result = await this.launcher.LaunchAsync(
                processInstance,
                specificParameters: new KrProcessUILauncher.SpecificParameters
                {
                    CardEditor = parentCardEditor,
                    RequestInfo = requestInfo,
                });

            await TessaDialog.ShowNotEmptyAsync(result.ValidationResult);

            return result.ValidationResult.IsSuccessful() && result.CardResponse?.GetKeepTaskDialog() != true;
        }

        #endregion
    }
}
