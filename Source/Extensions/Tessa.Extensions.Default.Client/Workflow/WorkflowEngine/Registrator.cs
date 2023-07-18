using Tessa.Extensions.Default.Shared.Workflow.WorkflowEngine;
using Tessa.UI.Workflow;
using Tessa.UI.WorkflowViewer.Actions;
using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Client.Workflow.WorkflowEngine
{
    [Registrator]
    public sealed class Registrator : RegistratorBase
    {
        /// <inheritdoc/>
        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterType<IWorkflowActionUIHandler, KrChangeStateActionUIHandler>(nameof(KrChangeStateActionUIHandler), new ContainerControlledLifetimeManager())
                .RegisterType<IWorkflowActionUIHandler, WorkflowCreateCardActionUIHandler>(nameof(WorkflowCreateCardActionUIHandler), new ContainerControlledLifetimeManager())
                .RegisterType<IWorkflowActionUIHandler, KrAcquaintanceActionUIHandler>(nameof(KrAcquaintanceActionUIHandler), new ContainerControlledLifetimeManager())
                .RegisterType<IWorkflowEngineTileManagerUIExtension, KrCheckStateTileManagerUIExtension>(nameof(KrCheckStateTileManagerUIExtension), new ContainerControlledLifetimeManager())
                .RegisterInstance<IWorkflowActionUIHandler>(
                    nameof(KrDescriptors.RegistrationDescriptor),
                    new WorkflowActionUIHandlerBase(KrDescriptors.RegistrationDescriptor),
                    new ContainerControlledLifetimeManager())
                .RegisterInstance<IWorkflowActionUIHandler>(
                    nameof(KrDescriptors.DeregistrationDescriptor),
                    new WorkflowActionUIHandlerBase(KrDescriptors.DeregistrationDescriptor),
                    new ContainerControlledLifetimeManager())
                .RegisterType<IWorkflowActionUIHandler, KrTaskRegistrationActionUIHandler>(nameof(KrTaskRegistrationActionUIHandler), new ContainerControlledLifetimeManager())
                .RegisterType<IWorkflowActionUIHandler, KrApprovalActionUIHandler>(nameof(KrApprovalActionUIHandler), new ContainerControlledLifetimeManager())
                .RegisterType<IWorkflowActionUIHandler, KrSigningActionUIHandler>(nameof(KrSigningActionUIHandler), new ContainerControlledLifetimeManager())
                .RegisterType<IWorkflowActionUIHandler, KrAmendingActionUIHandler>(nameof(KrAmendingActionUIHandler), new ContainerControlledLifetimeManager())
                .RegisterType<IWorkflowActionUIHandler, KrUniversalTaskActionUIHandler>(nameof(KrUniversalTaskActionUIHandler), new ContainerControlledLifetimeManager())
                .RegisterType<IWorkflowActionUIHandler, KrResolutionActionUIHandler>(nameof(KrResolutionActionUIHandler), new ContainerControlledLifetimeManager())
                .RegisterType<IWorkflowActionUIHandler, KrRouteInitializationActionUIHandler>(nameof(KrRouteInitializationActionUIHandler), new ContainerControlledLifetimeManager())
                ;
        }
    }
}
