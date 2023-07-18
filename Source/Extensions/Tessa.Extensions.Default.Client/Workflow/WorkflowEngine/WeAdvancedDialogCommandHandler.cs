#nullable enable

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Client.Workflow.KrProcess.CommandInterpreter;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess.ClientCommandInterpreter;
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;
using Tessa.UI;
using Tessa.UI.Cards;
using Tessa.Workflow;
using Tessa.Workflow.Actions;
using Tessa.Workflow.Helpful;

namespace Tessa.Extensions.Default.Client.Workflow.WorkflowEngine
{
    /// <summary>
    /// Обработчик клиентской команды <see cref="DefaultCommandTypes.WeShowAdvancedDialog"/>.
    /// </summary>
    public sealed class WeAdvancedDialogCommandHandler : AdvancedDialogCommandHandler
    {
        #region Fields

        private readonly IWorkflowEngineProcessorClient workflowEngineProcessor;

        #endregion

        #region Constructor

        public WeAdvancedDialogCommandHandler(
            Func<IAdvancedCardDialogManager> createAdvancedCardDialogManagerFunc,
            ISession session,
            ICardMetadata cardMetadata,
            Func<IUIHost> createUIHostFunc,
            Func<ICardEditorModel> createCardEditorFunc,
            IWorkflowEngineProcessorClient workflowEngineProcessor)
            : base(
                  createAdvancedCardDialogManagerFunc,
                  session,
                  cardMetadata) =>
            this.workflowEngineProcessor = NotNullOrThrow(workflowEngineProcessor);

        #endregion

        #region Base Overrides

        /// <inheritdoc />
        protected override CardTaskCompletionOptionSettings? PrepareDialogCommand(IClientCommandHandlerContext context)
        {
            var parameters = context.Command.Parameters;

            if (parameters.TryGetValue(KrConstants.Keys.CompletionOptionSettings, out var dialogSettingsObj)
                && dialogSettingsObj is Dictionary<string, object> dialogSettingsStorage
                && dialogSettingsStorage.TryGetValue(WorkflowDialogAction.DialogSettingsKey, out var coSettingsObj)
                && coSettingsObj is Dictionary<string, object?> coSettingsStorage)
            {
                return new CardTaskCompletionOptionSettings(coSettingsStorage);
            }

            return null;
        }

        /// <inheritdoc />
        protected override async ValueTask<bool> CompleteDialogCoreAsync(
            CardTaskDialogActionResult actionResult,
            IClientCommandHandlerContext context,
            ICardEditorModel cardEditor,
            ICardEditorModel? parentCardEditor = null)
        {
            var dialogSettings = context.Command.Parameters.TryGet<Dictionary<string, object>>(KrConstants.Keys.CompletionOptionSettings);
            var (request, requestSignature) = dialogSettings.GetProcessRequest();

            Dictionary<string, object?> responseInfo;
            switch (context.OuterContext)
            {
                case ICardStoreExtensionContext storeContext:
                    responseInfo = storeContext.Response!.Info;
                    break;

                case ICardGetExtensionContext getContext:
                    responseInfo = getContext.Response!.Info;
                    break;

                case ICardRequestExtensionContext requestContext:
                    responseInfo = requestContext.Response!.Info;
                    break;

                default:
                    return true;
            }

            var additionalInfo = new Dictionary<string, object?>(StringComparer.Ordinal);
            CardTaskDialogHelper.SetCardTaskDialogActionResult(additionalInfo, actionResult);

            if (responseInfo.TryGetValue(WorkflowEngineExtensions.WorkflowEngineProcessSerializedKey, out var processObj))
            {
                additionalInfo[WorkflowEngineExtensions.WorkflowEngineProcessSerializedKey] = processObj;
            }

            using var uiScope = parentCardEditor is null ? (IDisposable?) null : UIContext.Create(parentCardEditor.Context);

            var result = await this.workflowEngineProcessor.ProcessSignalAsync(
                request,
                requestSignature,
                additionalInfo,
                context.CancellationToken);

            TessaDialog.ShowNotEmpty(result.ValidationResult);
            if (result.ValidationResult.IsSuccessful
                && parentCardEditor is not null)
            {
                await parentCardEditor.RefreshCardAsync(parentCardEditor.Context, cancellationToken: context.CancellationToken);
            }

            return result.ValidationResult.IsSuccessful && !result.ResponseInfo.GetKeepTaskDialog();
        }

        #endregion

    }
}
