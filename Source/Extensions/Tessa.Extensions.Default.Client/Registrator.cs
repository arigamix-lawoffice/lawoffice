using Tessa.Platform;
using Tessa.UI.Runtime;

namespace Tessa.Extensions.Default.Client
{
    [Registrator]
    public sealed class Registrator : RegistratorBase
    {
        public override void FinalizeRegistration()
        {
            // типы могут быть не зарегистрированы в тестах или плагинах Chronos

            var resourceRegistrator = this.UnityContainer.TryResolve<IApplicationResourceRegistrator>();
            if (resourceRegistrator != null)
            {
                resourceRegistrator
                    .Register("Themes/Generic.xaml", typeof(Registrator).Assembly)
                    .Register("Resources/ViewModels.xaml", typeof(Registrator).Assembly)
                    ;
            }
        }
    }
}
