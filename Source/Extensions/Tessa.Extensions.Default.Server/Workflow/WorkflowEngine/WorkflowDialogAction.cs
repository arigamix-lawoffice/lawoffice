using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess.ClientCommandInterpreter;
using Tessa.Platform;
using Tessa.Workflow;
using Unity;

namespace Tessa.Extensions.Default.Server.Workflow.WorkflowEngine
{
    public sealed class WorkflowDialogAction : Tessa.Workflow.Actions.WorkflowDialogAction
    {
        #region Constructors
        public WorkflowDialogAction(
            ICardRepository cardRepository,
            [Dependency(CardRepositoryNames.Default)]ICardRepository cardRepositoryDef,
            ICardServerPermissionsProvider serverPermissionsProvider,
            ISignatureProvider signatureProvider,
            IWorkflowEngineCardsScope cardsScope,
            Func<ICardTaskCompletionOptionSettingsBuilder> ctcBuilderFactory,
            ICardFileManager cardFileManager)
            : base(
                  cardRepository,
                  cardRepositoryDef,
                  serverPermissionsProvider,
                  signatureProvider,
                  cardsScope,
                  ctcBuilderFactory,
                  cardFileManager)
        {
        }

        #endregion

        #region Base Overrides

        protected override Task StoreDialogWithoutTaskAsync(
            IWorkflowEngineContext context,
            Dictionary<string, object> storeInfo)
        {
            if (!(context.ResponseInfo.TryGetValue(KrProcessSharedExtensions.KrProcessClientCommandInfoMark, out var commandsObj)
                && commandsObj is IList commands))
            {
                commands = new List<object>();
                context.ResponseInfo[KrProcessSharedExtensions.KrProcessClientCommandInfoMark] = commands;
            }

            commands.Add(
                new KrProcessClientCommand(
                    DefaultCommandTypes.WeShowAdvancedDialog,
                    new Dictionary<string, object>
                    {
                        [KrConstants.Keys.CompletionOptionSettings] = storeInfo,
                    }).GetStorage());

            return Task.CompletedTask;
        }

        #endregion
    }
}
