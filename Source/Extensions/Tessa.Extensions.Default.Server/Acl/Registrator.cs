#nullable enable

using Tessa.Roles.Acl;
using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Server.Acl
{
    [Registrator]
    public sealed class Registrator : RegistratorBase
    {
        #region Base Overrides

        /// <inheritdoc/>
        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterType<IAclGenerationRuleCacheObjectFactory, KrAclGenerationRuleCacheObjectFactory>(new ContainerControlledLifetimeManager())
                .RegisterType<IAclGenerationRuleDataFactory, KrAclGenerationRuleDataFactory>(new ContainerControlledLifetimeManager())
                .RegisterType<IAclGenerationRuleExecutor, KrAclGenerationRuleExecutor>(new ContainerControlledLifetimeManager())
                ;
        }

        #endregion
    }
}
