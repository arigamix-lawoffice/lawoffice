using Tessa.Extensions.Default.Client.Workflow.WorkflowEngine;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess.ClientCommandInterpreter;
using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Client.Workflow.KrProcess.CommandInterpreter
{
    [Registrator]
    public sealed class Registrator : RegistratorBase
    {
        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterType<IClientCommandHandler, ShowConfirmationDialogClientCommandHandler>( new ContainerControlledLifetimeManager())
                .RegisterType<IClientCommandHandler, RefreshAndNotifyClientCommandHandler>(new ContainerControlledLifetimeManager())
                .RegisterType<IClientCommandHandler, OpenCardClientCommandHandler>(new ContainerControlledLifetimeManager())
                .RegisterType<IClientCommandHandler, CreateCardViaTemplateCommandHandler>(new ContainerControlledLifetimeManager())
                .RegisterType<IClientCommandHandler, CreateCardViaDocTypeCommandHandler>(new ContainerControlledLifetimeManager())
                .RegisterType<IClientCommandHandler, KrAdvancedDialogCommandHandler>(new ContainerControlledLifetimeManager())

                ;
        }

        public override void FinalizeRegistration()
        {
            this.UnityContainer
                .Resolve<IClientCommandInterpreter>()
                .RegisterHandler<ShowConfirmationDialogClientCommandHandler>(DefaultCommandTypes.ShowConfirmationDialog)
                .RegisterHandler<RefreshAndNotifyClientCommandHandler>(DefaultCommandTypes.RefreshAndNotify)
                .RegisterHandler<OpenCardClientCommandHandler>(DefaultCommandTypes.OpenCard)
                .RegisterHandler<CreateCardViaTemplateCommandHandler>(DefaultCommandTypes.CreateCardViaTemplate)
                .RegisterHandler<CreateCardViaDocTypeCommandHandler>(DefaultCommandTypes.CreateCardViaDocType)
                .RegisterHandler<KrAdvancedDialogCommandHandler>(DefaultCommandTypes.ShowAdvancedDialog)
                .RegisterHandler<WeAdvancedDialogCommandHandler>(DefaultCommandTypes.WeShowAdvancedDialog)

                ;
        }
    }
}