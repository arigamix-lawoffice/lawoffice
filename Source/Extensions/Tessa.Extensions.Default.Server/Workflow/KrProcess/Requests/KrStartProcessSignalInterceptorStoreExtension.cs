using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Cards.Workflow;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Data;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Requests
{
    /// <summary>
    /// Расширение на сохранение карточки обрабатывающее сигналы типа <see cref="KrConstants.KrStartProcessSignal"/> и <see cref="KrConstants.KrStartProcessUnlessStartedGlobalSignal"/>, расположенные в очереди действий Workflow, осуществляя планирование запуска основного процесса <see cref="KrConstants.KrProcessName"/>.
    /// </summary>
    public sealed class KrStartProcessSignalInterceptorStoreExtension :
        CardStoreExtension
    {
        #region Base Overrides

        /// <inheritdoc/>
        public override async Task BeforeRequest(
            ICardStoreExtensionContext context)
        {
            Card card;
            WorkflowQueue queue;
            if (!context.ValidationResult.IsSuccessful()
                || (card = context.Request.TryGetCard()) is null
                || (queue = card.TryGetWorkflowQueue()) is null
                || queue.IsEmpty)
            {
                return;
            }

            foreach (var item in queue.Items)
            {
                var signal = item.TryGetSignal();

                if (signal is null
                    || signal.ProcessTypeName != KrConstants.KrProcessName)
                {
                    continue;
                }

                switch (signal.Name)
                {
                    case KrConstants.KrStartProcessSignal:
                        item.Handled = true;
                        StartProcess(context.Request, signal.Parameters);
                        break;
                    case KrConstants.KrStartProcessUnlessStartedGlobalSignal:
                        item.Handled = true;
                        if (!await ProcessStartedAsync(context.DbScope, card.ID, context.CancellationToken))
                        {
                            StartProcess(context.Request, signal.Parameters);
                        }
                        break;
                }
            }
        }

        #endregion

        #region Private Methods

        private static void StartProcess(
            CardStoreRequest request,
            Dictionary<string, object> parameters)
        {
            request.SetStartingProcessName(KrConstants.KrProcessName);
            request.SetStartingKrProcessParameters(parameters);
            request.RemoveSecondaryProcess();
        }

        private static async Task<bool> ProcessStartedAsync(
            IDbScope dbScope,
            Guid cardID,
            CancellationToken cancellationToken = default)
        {
            await using (dbScope.Create())
            {
                var db = dbScope.Db;
                return await db
                    .SetCommand(dbScope.BuilderFactory
                        .Select()
                        .V(true)
                        .From("WorkflowProcesses", "wp").NoLock()
                        .InnerJoin(KrConstants.KrApprovalCommonInfo.Name, "aci").NoLock()
                            .On().C("wp", "ID").Equals().C("aci", "ID")
                        .Where().C("aci", KrConstants.KrApprovalCommonInfo.MainCardID).Equals().P("CardID")
                        .Build(),
                        db.Parameter("CardID", cardID))
                    .LogCommand()
                    .ExecuteAsync<bool>(cancellationToken);
            }
        }

        #endregion
    }
}