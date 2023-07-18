using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Client.DocLoad
{
    /// <summary>
    /// Регистрация зависимостей для потокового сканирования.
    /// </summary>
    [Registrator]
    public sealed class Registrator : RegistratorBase
    {
        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterType<IPrintDialogProvider, PrintDialogProvider>(new ContainerControlledLifetimeManager())
                ;
        }
    }
}