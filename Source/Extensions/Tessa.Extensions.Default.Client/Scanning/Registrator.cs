using Tessa.Extensions.Platform.Client.Scanning;
using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Client.Scanning
{
    /// <summary>
    /// Любой кастомный <see cref="IScanProvider"/> может быть зарегистрирован через дефолтный атрибут <c>[Registrator]</c>.
    /// </summary>
    [Registrator(ExtensionStage.BeforePlatform)]
    public sealed class Registrator : RegistratorBase
    {
        public override void RegisterUnity()
        {
            // при каждом запросе IScanProvider должен возвращаться новый экземпляр класса

            this.UnityContainer
                .RegisterType<IScanProvider, ServiceScanProvider>(new PerResolveLifetimeManager())
                ;
        }
    }
}
