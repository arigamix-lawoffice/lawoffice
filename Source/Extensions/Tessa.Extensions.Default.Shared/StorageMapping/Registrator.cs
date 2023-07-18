using Tessa.Extensions.Default.Shared.StorageMapping.StorageMappingHandlers;
using Tessa.Platform;
using Tessa.Platform.Storage.Mapping;
using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Shared.StorageMapping
{
    [Registrator(Tag = RegistratorTag.ConsoleClient | RegistratorTag.Server)]
    public sealed class Registrator : RegistratorBase
    {
        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterType<KrSecondaryProcessStorageMappingHandler>(new ContainerControlledLifetimeManager())
                .RegisterType<KrStageTemplateStorageMappingHandler>(new ContainerControlledLifetimeManager())
                .RegisterType<KrStageCommonMethodStorageMappingHandler>(new ContainerControlledLifetimeManager())
                .RegisterType<KrStageGroupStorageMappingHandler>(new ContainerControlledLifetimeManager())
                ;
        }

        public override void FinalizeRegistration()
        {
            // Регистрация хэндлеров маппингов для выгружаемого во внешние файлы контента карточек.
            this.UnityContainer
                .TryResolve<IStorageMappingResolver>()?
                .RegisterKrStorageMappingHandlers()
                ;
        }
    }
}
