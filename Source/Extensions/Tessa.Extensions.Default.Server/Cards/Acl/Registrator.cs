using Tessa.Platform;
using Tessa.Roles.Acl.Extensions;
using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Server.Cards.Acl
{
    [Registrator]
    public sealed class Registrator : RegistratorBase
    {
        #region Base Overrides

        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterType<KrDocStatesAclExtension>(new ContainerControlledLifetimeManager())
                ;
        }

        public override void FinalizeRegistration()
        {
            this.UnityContainer
                .TryResolve<IAclGenerationRuleExtensionResolver>()?
                .Register<KrDocStatesAclExtension>(AclExtensionDescriptors.KrDocStates.ID)
                ;
        }

        #endregion
    }
}
