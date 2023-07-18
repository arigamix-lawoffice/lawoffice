using Tessa.Roles;
using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Shared.Workflow.KrPermissions
{
    [Registrator]
    public sealed class Registrator : RegistratorBase
    {
        public override void RegisterUnity()
        {
            this.UnityContainer
                // Переопределяем менеджер проверки принадлежности карточки роли к кастомной подсистеме прав доступа
                .RegisterType<IRoleTypePermissionsManager, KrRoleTypePermissionsManager>(new ContainerControlledLifetimeManager())
                ;
        }
    }
}
