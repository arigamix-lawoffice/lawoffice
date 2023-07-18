using Tessa.Platform;
using Tessa.UI.Views.Extensions;
using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Client.Views
{
    [Registrator]
    public sealed class Registrator : RegistratorBase
    {
        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterType<LawCreateCaseViewExtension>(new ContainerControlledLifetimeManager())
                ;
        }

        public override void FinalizeRegistration()
        {
            this.UnityContainer
                .TryResolve<IWorkplaceExtensionRegistry>()
                .Register(typeof(LawCreateCaseViewExtension))
                .RegisterConfiguratorType(
                    typeof(LawCreateCaseViewExtension),
                    type => this.UnityContainer.Resolve<LawCreateCaseViewExtensionConfigurator>())
                ;
        }
    }
}