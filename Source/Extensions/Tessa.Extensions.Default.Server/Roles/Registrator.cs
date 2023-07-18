using Tessa.Roles.NestedRoles;
using Tessa.Roles.SmartRoles;
using Tessa.Views;
using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Server.Roles
{
    [Registrator]
    public sealed class Registrator : RegistratorBase
    {
        public override void FinalizeRegistration()
        {
            this.UnityContainer
                .RegisterType<INestedRoleContextSelector, NestedRoleContextSelector>(new ContainerControlledLifetimeManager())
                .RegisterType<IViewInterceptor, TaskAssignedRoleUsersInterceptor>(nameof(TaskAssignedRoleUsersInterceptor), new ContainerControlledLifetimeManager())
                .RegisterType<ISmartRoleGeneratorDataFactory, KrSmartRoleGeneratorDataFactory>(new ContainerControlledLifetimeManager())
                .RegisterType<ISmartRoleGeneratorCacheObjectFactory, KrSmartRoleGeneratorCacheObjectFactory>(new ContainerControlledLifetimeManager())
                ;
        }
    }
}
