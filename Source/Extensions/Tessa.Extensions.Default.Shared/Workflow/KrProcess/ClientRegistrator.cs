using Tessa.Cards;
using Tessa.Cards.Caching;
using Tessa.Cards.Extensions;
using Tessa.Platform;
using Tessa.Platform.Runtime;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess
{
    [Registrator(Type = SessionType.Client, Tag = RegistratorTag.DefaultForClientAndConsole)]
    public sealed class ClientRegistrator : RegistratorBase
    {
        /// <inheritdoc/>
        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterType<KrDocTypeInvalidateSettingsCacheDeleteExtension>(new ContainerControlledLifetimeManager())
                .RegisterType<KrDocTypeInvalidateSettingsCacheStoreExtension>(new ContainerControlledLifetimeManager())
                .RegisterType<KrSettingsInvalidateTypeCacheStoreExtension>(
                    new ContainerControlledLifetimeManager(),
                    new InjectionConstructor(
                        typeof(ICardCachedMetadata),
                        typeof(IKrTypesCache),
                        typeof(ISession)))
                .RegisterType<KrSettingsInvalidateTypeCacheDeleteExtension>(
                    new ContainerControlledLifetimeManager(),
                    new InjectionConstructor(
                        typeof(ICardCachedMetadata),
                        typeof(IKrTypesCache),
                        typeof(ISession)))
                .RegisterType<IKrTypesCache, KrTypesCache>(
                    new ContainerControlledLifetimeManager(),
                    new InjectionConstructor(
                        typeof(ICardRepository),
                        typeof(ICardMetadata),
                        typeof(ICardCache),
                        typeof(IUnityDisposableContainer)))
                .RegisterType<IKrProcessLauncher, KrProcessClientLauncher>(new ContainerControlledLifetimeManager())
                ;
        }

        /// <inheritdoc/>
        public override void RegisterExtensions(IExtensionContainer extensionContainer)
        {
            extensionContainer
                // Сброс кэша при изменении списка типов карточек в настройках процесса
                .RegisterExtension<ICardStoreExtension, KrSettingsInvalidateTypeCacheStoreExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 1)
                    .WithUnity(this.UnityContainer)
                    .WhenCardTypes(DefaultCardTypes.KrSettingsTypeID)
                    .WhenAnyStoreMethod())

                .RegisterExtension<ICardDeleteExtension, KrSettingsInvalidateTypeCacheDeleteExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 1)
                    .WithUnity(this.UnityContainer)
                    .WhenCardTypes(DefaultCardTypes.KrSettingsTypeID))

                // Сброс кэша при сохранении карточки типа документа
                .RegisterExtension<ICardStoreExtension, KrDocTypeInvalidateSettingsCacheStoreExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 1)
                    .WithUnity(this.UnityContainer)
                    .WhenCardTypes(DefaultCardTypes.KrDocTypeTypeID))

                // Сброс кэша при удалении карточки типа документа
                .RegisterExtension<ICardDeleteExtension, KrDocTypeInvalidateSettingsCacheDeleteExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 1)
                    .WithUnity(this.UnityContainer)
                    .WhenCardTypes(DefaultCardTypes.KrDocTypeTypeID))

                ;
        }
    }
}