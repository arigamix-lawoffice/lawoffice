using Tessa.Extensions.Default.Shared.Acquaintance;
using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Client.Acquaintance
{
    /// <summary>
    /// Регистрация расширений, управляющих плитками.
    /// </summary>
    [Registrator]
    public sealed class Registrator : RegistratorBase
    {
        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterType<IKrAcquaintanceManager, KrAcquaintanceManager>(new ContainerControlledLifetimeManager())
                ;
        }
    }
}
