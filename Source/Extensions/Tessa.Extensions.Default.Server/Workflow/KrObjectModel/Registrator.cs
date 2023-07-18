using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Server.Workflow.KrObjectModel
{
    [Registrator]
    public sealed class Registrator : RegistratorBase
    {
        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterType<IStageSettingsConverter, StageSettingsConverter>(new ContainerControlledLifetimeManager())
                .RegisterType<IObjectModelMapper, ObjectModelMapper>(new ContainerControlledLifetimeManager())
                
                ;

        }
    }
}