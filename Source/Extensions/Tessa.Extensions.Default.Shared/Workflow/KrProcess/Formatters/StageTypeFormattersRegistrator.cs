using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess.Formatters
{
    [Registrator]
    public class StageTypeFormattersRegistrator : RegistratorBase
    {
        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterType<IStageTypeFormatterContainer, StageTypeFormatterContainer>(
                    new ContainerControlledLifetimeManager())
                .RegisterType<ApprovalStageTypeFormatter>(new ContainerControlledLifetimeManager())
                .RegisterType<EditStageTypeFormatter>(new ContainerControlledLifetimeManager())
                .RegisterType<ChangeStateStageTypeFormatter>(new ContainerControlledLifetimeManager())
                .RegisterType<CreateCardStageTypeFormatter>(new ContainerControlledLifetimeManager())
                .RegisterType<ResolutionStageTypeFormatter>(new ContainerControlledLifetimeManager())
                .RegisterType<SigningStageTypeFormatter>(new ContainerControlledLifetimeManager())
                .RegisterType<ProcessManagementStageTypeFormatter>(new ContainerControlledLifetimeManager())
                .RegisterType<UniversalTaskStageTypeFormatter>(new ContainerControlledLifetimeManager())
                .RegisterType<NotificationStageTypeFormatter>(new ContainerControlledLifetimeManager())
                .RegisterType<AcquaintanceStageTypeFormatter>(new ContainerControlledLifetimeManager())
                .RegisterType<RegistrationStageTypeFormatter>(new ContainerControlledLifetimeManager())
                .RegisterType<HistoryManagementStageTypeFormatter>(new ContainerControlledLifetimeManager())
                .RegisterType<ForkStageTypeFormatter>(new ContainerControlledLifetimeManager())
                .RegisterType<ForkManagementStageTypeFormatter>(new ContainerControlledLifetimeManager())
                .RegisterType<TypedTaskStageTypeFormatter>(new ContainerControlledLifetimeManager())
                .RegisterType<AddFileFromTemplateStageTypeFormatter>(new ContainerControlledLifetimeManager())
                .RegisterType<DialogsStageTypeFormatter>(new ContainerControlledLifetimeManager())
                ;
        }

        public override void FinalizeRegistration()
        {
            this.UnityContainer
                .Resolve<IStageTypeFormatterContainer>()
                .RegisterFormatter<ApprovalStageTypeFormatter>(StageTypeDescriptors.ApprovalDescriptor)
                .RegisterFormatter<EditStageTypeFormatter>(StageTypeDescriptors.EditDescriptor)
                .RegisterFormatter<ChangeStateStageTypeFormatter>(StageTypeDescriptors.ChangesStateDescriptor)
                .RegisterFormatter<CreateCardStageTypeFormatter>(StageTypeDescriptors.CreateCardDescriptor)
                .RegisterFormatter<ResolutionStageTypeFormatter>(StageTypeDescriptors.ResolutionDescriptor)
                .RegisterFormatter<SigningStageTypeFormatter>(StageTypeDescriptors.SigningDescriptor)
                .RegisterFormatter<ProcessManagementStageTypeFormatter>(StageTypeDescriptors.ProcessManagementDescriptor)
                .RegisterFormatter<UniversalTaskStageTypeFormatter>(StageTypeDescriptors.UniversalTaskDescriptor)
                .RegisterFormatter<NotificationStageTypeFormatter>(StageTypeDescriptors.NotificationDescriptor)
                .RegisterFormatter<AcquaintanceStageTypeFormatter>(StageTypeDescriptors.AcquaintanceDescriptor)
                .RegisterFormatter<RegistrationStageTypeFormatter>(StageTypeDescriptors.RegistrationDescriptor)
                .RegisterFormatter<HistoryManagementStageTypeFormatter>(StageTypeDescriptors.HistoryManagementDescriptor)
                .RegisterFormatter<ForkStageTypeFormatter>(StageTypeDescriptors.ForkDescriptor)
                .RegisterFormatter<ForkManagementStageTypeFormatter>(StageTypeDescriptors.ForkManagementDescriptor)
                .RegisterFormatter<TypedTaskStageTypeFormatter>(StageTypeDescriptors.TypedTaskDescriptor)
                .RegisterFormatter<AddFileFromTemplateStageTypeFormatter>(StageTypeDescriptors.AddFromTemplateDescriptor)
                .RegisterFormatter<DialogsStageTypeFormatter>(StageTypeDescriptors.DialogDescriptor)
                ;
        }
    }
}