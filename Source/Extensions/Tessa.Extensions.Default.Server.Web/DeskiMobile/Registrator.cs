using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Server.Web.DeskiMobile
{
    [Registrator]
    public sealed class Registrator : RegistratorBase
    {
        #region Base Overrides

        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterType<IDeskiMobileTokenManager, DeskiMobileTokenManager>(new ContainerControlledLifetimeManager())
                .RegisterType<IDeskiMobileLockingStrategy, DeskiMobileLockingStrategy>(new ContainerControlledLifetimeManager());
                
            DeskiMobileValidationKeys.Register();
        }

        #endregion
    }
}
