using Tessa.Extensions.Default.Server.Workflow.KrProcess.Events;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;
using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.Handlers
{
    [Registrator]
    public sealed class Registrator : RegistratorBase
    {
        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterType<IStageTasksRevoker, StageTasksRevoker>(new ContainerControlledLifetimeManager())
                .RegisterType<AddFromTemplateStageTypeHandler>(new ContainerControlledLifetimeManager())
                .RegisterType<ApprovalStageTypeHandler>(new ContainerControlledLifetimeManager())
                .RegisterType<ChangeStateStageTypeHandler>(new ContainerControlledLifetimeManager())
                .RegisterType<CreateCardStageTypeHandler>(new ContainerControlledLifetimeManager())
                .RegisterType<DeregistrationStageTypeHandler>(new ContainerControlledLifetimeManager())
                .RegisterType<EditStageTypeHandler>(new ContainerControlledLifetimeManager())
                .RegisterType<PartialGroupRecalcStageTypeHandler>(new ContainerControlledLifetimeManager())
                .RegisterType<RegistrationStageTypeHandler>(new ContainerControlledLifetimeManager())
                .RegisterType<ResolutionStageTypeHandler>(new ContainerControlledLifetimeManager())
                .RegisterType<SigningStageTypeHandler>(new ContainerControlledLifetimeManager())
                .RegisterType<ProcessManagementStageTypeHandler>(new ContainerControlledLifetimeManager())
                .RegisterType<UniversalTaskStageTypeHandler>(new ContainerControlledLifetimeManager())
                .RegisterType<ScriptStageTypeHandler>(new ContainerControlledLifetimeManager())
                .RegisterType<NotificationStageTypeHandler>(new ContainerControlledLifetimeManager())
                .RegisterType<AcquaintanceStageTypeHandler>(new ContainerControlledLifetimeManager())
                .RegisterType<HistoryManagementStageTypeHandler>(new ContainerControlledLifetimeManager())
                .RegisterType<ForkStageTypeHandler>(new ContainerControlledLifetimeManager())
                .RegisterType<ForkManagementStageTypeHandler>(new ContainerControlledLifetimeManager())
                .RegisterType<TypedTaskStageTypeHandler>(new ContainerControlledLifetimeManager())
                .RegisterType<DialogStageTypeHandler>(new ContainerControlledLifetimeManager())

                .RegisterType<ForkEventExtension>(new ContainerControlledLifetimeManager())
                ;
        }

        /// <inheritdoc />
        public override void RegisterExtensions(
            IExtensionContainer extensionContainer)
        {
            extensionContainer
                .RegisterExtension<IKrEventExtension, ForkEventExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform)
                    .WithSingleton()
                    .WhenEventType(DefaultEventTypes.AsyncProcessCompleted));

        }

        public override void FinalizeRegistration()
        {
            this.UnityContainer
                .TryResolve<IKrProcessContainer>()
                ?
                .RegisterHandler<AddFromTemplateStageTypeHandler>(StageTypeDescriptors.AddFromTemplateDescriptor)
                .RegisterHandler<ApprovalStageTypeHandler>(StageTypeDescriptors.ApprovalDescriptor)
                .RegisterHandler<ChangeStateStageTypeHandler>(StageTypeDescriptors.ChangesStateDescriptor)
                .RegisterHandler<CreateCardStageTypeHandler>(StageTypeDescriptors.CreateCardDescriptor)
                .RegisterHandler<DeregistrationStageTypeHandler>(StageTypeDescriptors.DeregistrationDescriptor)
                .RegisterHandler<EditStageTypeHandler>(StageTypeDescriptors.EditDescriptor)
                .RegisterHandler<PartialGroupRecalcStageTypeHandler>(StageTypeDescriptors.PartialGroupRecalcDescriptor)
                .RegisterHandler<ResolutionStageTypeHandler>(StageTypeDescriptors.ResolutionDescriptor)
                .RegisterHandler<RegistrationStageTypeHandler>(StageTypeDescriptors.RegistrationDescriptor)
                .RegisterHandler<SigningStageTypeHandler>(StageTypeDescriptors.SigningDescriptor)
                .RegisterHandler<ProcessManagementStageTypeHandler>(StageTypeDescriptors.ProcessManagementDescriptor)
                .RegisterHandler<UniversalTaskStageTypeHandler>(StageTypeDescriptors.UniversalTaskDescriptor)
                .RegisterHandler<ScriptStageTypeHandler>(StageTypeDescriptors.ScriptDescriptor)
                .RegisterHandler<NotificationStageTypeHandler>(StageTypeDescriptors.NotificationDescriptor)
                .RegisterHandler<AcquaintanceStageTypeHandler>(StageTypeDescriptors.AcquaintanceDescriptor)
                .RegisterHandler<HistoryManagementStageTypeHandler>(StageTypeDescriptors.HistoryManagementDescriptor)
                .RegisterHandler<ForkStageTypeHandler>(StageTypeDescriptors.ForkDescriptor)
                .RegisterHandler<ForkManagementStageTypeHandler>(StageTypeDescriptors.ForkManagementDescriptor)
                .RegisterHandler<TypedTaskStageTypeHandler>(StageTypeDescriptors.TypedTaskDescriptor)
                .RegisterHandler<DialogStageTypeHandler>(StageTypeDescriptors.DialogDescriptor)

                .RegisterTaskType(KrConstants.KrTaskTypeIDList)
                ;
        }
    }
}