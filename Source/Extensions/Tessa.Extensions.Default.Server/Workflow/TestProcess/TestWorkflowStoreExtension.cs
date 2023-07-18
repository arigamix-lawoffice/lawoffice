using System;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.ComponentModel;
using Tessa.Cards.Extensions;
using Tessa.Cards.Workflow;
using Tessa.Extensions.Default.Server.Workflow.KrProcess;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.TestProcess;
using Unity;

namespace Tessa.Extensions.Default.Server.Workflow.TestProcess
{
    public sealed class TestWorkflowStoreExtension :
        KrWorkflowStoreExtension
    {
        #region Constructors

        public TestWorkflowStoreExtension(
            IKrTokenProvider krTokenProvider,
            [Dependency(CardRepositoryNames.Default)] ICardRepository cardRepositoryToCreateNextRequest,
            ICardRepository cardRepositoryToStoreNextRequest,
            ICardRepository cardRepositoryToCreateTasks,
            ICardTaskHistoryManager taskHistoryManager,
            ICardGetStrategy cardGetStrategy,
            IWorkflowQueueProcessor workflowQueueProcessor)
            : base(
                krTokenProvider,
                cardRepositoryToCreateNextRequest,
                cardRepositoryToStoreNextRequest,
                cardRepositoryToCreateTasks,
                taskHistoryManager,
                cardGetStrategy,
                workflowQueueProcessor)
        {
        }

        #endregion

        #region Base Overrides

        protected override async ValueTask<bool> TaskIsAllowedAsync(CardTask task, ICardStoreExtensionContext context)
        {
            Guid taskTypeID = task.TypeID;
            return taskTypeID == DefaultTaskTypes.TestTask1TypeID
                || taskTypeID == DefaultTaskTypes.TestTask2TypeID;
        }

        protected override async ValueTask<bool> CanHandleQueueItemAsync(WorkflowQueueItem queueItem, ICardStoreExtensionContext context) =>
            TestProcessHelper.MainSubProcess == queueItem.Signal.ProcessTypeName;

        protected override ValueTask<bool> CanStartProcessAsync(Guid? processID, string processName, ICardStoreExtensionContext context)
        {
            switch (processName)
            {
                case TestProcessHelper.ProcessName:
                    return new ValueTask<bool>(true);

                default:
                    return new ValueTask<bool>(false);
            }
        }

        protected override Task StartProcessAsync(
            Guid? processID,
            string processName,
            IWorkflowWorker workflowWorker,
            CancellationToken cancellationToken = default)
        {
            switch (processName)
            {
                case TestProcessHelper.ProcessName:
                    return workflowWorker.StartProcessAsync(
                        TestProcessHelper.MainSubProcess,
                        newProcessID: processID,
                        cancellationToken: cancellationToken);

                default:
                    throw new ArgumentOutOfRangeException(nameof(processName), processName, null);
            }
        }

        protected override async ValueTask<IWorkflowWorker> CreateWorkerAsync(
            IWorkflowManager workflowManager,
            CancellationToken cancellationToken = default) =>
            new TestWorkflowWorker(workflowManager, this.CardRepositoryToCreateTasks);

        #endregion
    }
}
