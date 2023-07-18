using Tessa.Platform;
using Tessa.UI.Views.Extensions;
using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Client.Views.CardEditor
{
    [Registrator]
    public class Registrator :
        RegistratorBase
    {
        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterType<CardEditorExtension>(new ContainerControlledLifetimeManager())
                .RegisterType<CardEditorExtensionConfigurator>(new ContainerControlledLifetimeManager())
                ;
        }


        public override void FinalizeRegistration()
        {
            this.UnityContainer
                .TryResolve<IWorkplaceExtensionRegistry>()
                ?
                .Register(typeof(CardEditorExtension))
                .RegisterConfiguratorType(
                    typeof(CardEditorExtension),
                    type => this.UnityContainer.Resolve<CardEditorExtensionConfigurator>())
                ;
        }
    }
}