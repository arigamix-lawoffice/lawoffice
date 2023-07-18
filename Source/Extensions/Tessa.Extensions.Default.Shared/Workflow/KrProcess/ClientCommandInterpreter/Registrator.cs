using Tessa.Platform.Runtime;
using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess.ClientCommandInterpreter
{
    [Registrator(Type = SessionType.Client, Tag = RegistratorTag.DefaultForClientAndConsole)]
    public sealed class Registrator : RegistratorBase
    {
        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterType<IClientCommandInterpreter, ClientCommandInterpreter>(new ContainerControlledLifetimeManager())
                ;
        }
        

    }
}