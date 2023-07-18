using System;
using System.Collections.Generic;
using Unity.Lifetime;
using Unity;

namespace Tessa.Extensions.Server.Cards.Helpers
{
    [Registrator]
    public sealed class Registrator : RegistratorBase
    {
        /// <inheritdoc/>
        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterType<LawCaseHelper>(new ContainerControlledLifetimeManager())
                ;
        }
    }
}
