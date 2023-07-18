using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tessa.Cards.Workflow;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Events;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.Handlers
{
    /// <summary>
    /// Расширение, выполняющее обработку события завершения вложенного асинхронного процесса.
    /// </summary>
    public sealed class ForkEventExtension : KrEventExtension
    {
        #region Base Overrides

        /// <inheritdoc />
        public override async Task HandleEvent(
            IKrEventExtensionContext context)
        {
            if (context.ProcessInfo.ProcessTypeName != KrConstants.KrNestedProcessName
                || context.InitiationCause == KrProcessRunnerInitiationCause.StartProcess)
            {
                // Завершается не вложенный процесс
                // или
                // Если процесс запущен и сразу завершен,
                // то это все происходит прямо внутри HandleStageStart
                return;
            }

            var parameters = context.ProcessInfo?.ProcessParameters;
            if (parameters is null)
            {
                return;
            }

            var parentProcessTypeName = parameters.TryGet<string>(KrConstants.Keys.ParentProcessType);
            var parentProcessID = parameters.TryGet<Guid>(KrConstants.Keys.ParentProcessID);

            if (parentProcessTypeName is not null
                && parentProcessID != Guid.Empty)
            {
                var card = await context.MainCardAccessStrategy.GetCardAsync(cancellationToken: context.CancellationToken);

                if (card is null)
                {
                    return;
                }

                card
                    .GetWorkflowQueue()
                    .AddSignal(
                        parentProcessTypeName,
                        KrConstants.AsyncForkedProcessCompletedSingal,
                        processID: parentProcessID,
                        parameters: new Dictionary<string, object>(StringComparer.Ordinal)
                        {
                            [KrConstants.Keys.ProcessID] = context.ProcessInfo.ProcessID,
                            [KrConstants.Keys.ProcessInfoAtEnd] = context.WorkflowProcess.InfoStorage,
                        });
            }
        }

        #endregion
    }
}