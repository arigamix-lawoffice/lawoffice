using Tessa.Platform.Runtime;
using Unity;

namespace Tessa.Extensions.Shared.Services
{
    [Registrator(Type = SessionType.Client, Tag = RegistratorTag.DefaultForClientAndConsole)]
    public sealed class Registrator : RegistratorBase
    {
        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterSingleton<IService, ServiceClient>()
                ;
        }
    }
}
