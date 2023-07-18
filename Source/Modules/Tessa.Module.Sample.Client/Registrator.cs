using Tessa.Extensions;
using Tessa.UI.Runtime;
using Unity;

namespace Tessa.Module.Sample.Client
{
    [Registrator]
    public sealed class Registrator : RegistratorBase
    {
        public override void FinalizeRegistration()
        {
            // типы могут быть не зарегистрированы в тестах или плагинах Chronos

            if (this.UnityContainer.IsRegistered<IApplicationResourceRegistrator>())
            {
                this.UnityContainer
                    .Resolve<IApplicationResourceRegistrator>()
                    .Register("Themes/Generic.xaml", typeof(Registrator).Assembly)
                    .Register("Resources/ViewModels.xaml", typeof(Registrator).Assembly)
                    ;
            }
        }
    }
}
