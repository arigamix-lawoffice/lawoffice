#nullable enable

using System;
using Tessa.Extensions.Default.Shared.Tags;
using Tessa.Tags;
using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Server.Tags
{
    [Registrator]
    public sealed class Registrator : RegistratorBase
    {
        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterType<ITagPermissionsManager, KrTagPermissionsManager>(new ContainerControlledLifetimeManager())
                .RegisterType<ITagPermissionsTokenProvider, KrTagPermissionsTokenProvider>(new ContainerControlledLifetimeManager())
                ;
        }
    }
}
