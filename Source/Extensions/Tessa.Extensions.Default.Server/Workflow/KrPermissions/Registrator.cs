using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.KrPermissions;
using Tessa.Platform;
using Tessa.Platform.Data;
using Tessa.Platform.Licensing;
using Tessa.Platform.ObjectLocking;
using Tessa.Views;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Server.Workflow.KrPermissions
{
    [Registrator]
    public sealed class Registrator : RegistratorBase
    {
        public override void RegisterUnity()
        {
            this.UnityContainer
                // Менеджер прав доступа
                .RegisterType<IKrPermissionsManager, KrPermissionsManager>(new ContainerControlledLifetimeManager())
                .RegisterType<IKrPermissionsMaskGenerator, KrPermissionsMaskGenerator>(new ContainerControlledLifetimeManager())

                // Серверное представление по флагам прав доступа
                .RegisterType<IViewInterceptor, KrPermissionsFlagsViewInterceptor>(
                    nameof(KrPermissionsFlagsViewInterceptor),
                    new ContainerControlledLifetimeManager())

                // Расширяет представление по правилам доступа
                .RegisterType<IViewInterceptor, KrPermissionsViewInterceptor>(
                    nameof(KrPermissionsViewInterceptor),
                    new ContainerControlledLifetimeManager())

                // Контейнер кеша правил доступа
                .RegisterType<IKrPermissionsCacheContainer, KrPermissionsCacheContainer>(new ContainerControlledLifetimeManager())

                .RegisterType<IKrPermissionsObjectLockingStrategy, KrPermissionsObjectLockingStrategy>(new ContainerControlledLifetimeManager())
                .RegisterFactory<IObjectTransactionLockingStrategy>(
                    nameof(KrPermissionsObjectLockingStrategy),
                    static c => new ObjectTransactionLockingStrategy(
                        c.Resolve<IKrPermissionsObjectLockingStrategy>()),
                    new ContainerControlledLifetimeManager())
                // Объект для получения блокировок на правила доступа
                .RegisterType<IKrPermissionsLockStrategy, KrPermissionsLockStrategy>(new ContainerControlledLifetimeManager())

                .RegisterSingleton<Files.IKrPermissionsFilesManager, Files.KrPermissionsFilesManager>()

                //Расширение на выдачу прав по заданиям типового процесса согласования
                .RegisterType<ITaskPermissionsExtension, KrProcessTasksPermissionsExtension>(new ContainerControlledLifetimeManager())

                .RegisterType<KrPermissionsNewGetExtension>(new ContainerControlledLifetimeManager())
                .RegisterType<KrPermissionsMaskDataGetExtension>(new ContainerControlledLifetimeManager())
                .RegisterType<KrGetUnavailableTypesForCreationGetExtension>(new ContainerControlledLifetimeManager())
                .RegisterType<KrWarnCannotCreateWhenCreatingTemplate>(new ContainerControlledLifetimeManager())
                .RegisterType<KrPermissionsExtensionMetadataExtension>(
                    new ContainerControlledLifetimeManager(),
                    new InjectionConstructor(
                        typeof(IDbScope),
                        typeof(ILicenseManager)))
                .RegisterType<KrPermissionsRulesStoreExtension>(new ContainerControlledLifetimeManager())
                .RegisterType<KrPermissionsStoreExtension>(new ContainerControlledLifetimeManager())
                .RegisterType<KrPermissionsDeleteExtension>(new ContainerControlledLifetimeManager())
                .RegisterType<KrAclReadAccessCardPermissionExtension>(new ContainerControlledLifetimeManager())
                ;
        }


        public override void InitializeExtensions(IExtensionContainer extensionContainer)
        {
            extensionContainer
                .RegisterType<ICardPermissionsExtension>(x => x
                    .MethodAsync<IKrPermissionsRecalcContext>(y => y.IsPermissionsRecalcRequired)
                    .MethodAsync<IKrPermissionsManagerContext>(y => y.ExtendPermissionsAsync),
                    x => x.Register(new AggregateFilterPolicy(
                        KrCardTypePermissionFilterPolicy.Instance)))
                .RegisterType<ITaskPermissionsExtension>(x => x
                    .MethodAsync<ITaskPermissionsExtensionContext>(y => y.ExtendPermissionsAsync),
                    x => x.Register(new AggregateFilterPolicy(
                        KrCardTypePermissionFilterPolicy.Instance,
                        KrCardTaskTypePermissionFilterPolicy.Instance)))
                .RegisterType<IKrPermissionsRuleExtension>(x => x
                    .MethodAsync<IKrPermissionsRuleExtensionContext>(y => y.CheckRuleAsync),
                    x => x.Register(new AggregateFilterPolicy(
                        KrCardTypePermissionFilterPolicy.Instance)));
        }

        public override void RegisterExtensions(IExtensionContainer extensionContainer)
        {
            extensionContainer
                //Расширение, обрабатывающее сохранение карточки с настройками доступа
                .RegisterExtension<ICardStoreExtension, KrPermissionsRulesStoreExtension>(x => x
                    .WithOrder(ExtensionStage.BeforePlatform, 1)
                    .WithUnity(this.UnityContainer)
                    .WhenAnyStoreMethod()
                    .WhenCardTypes(DefaultCardTypes.KrPermissionsTypeID))

                // Модификация типа карточки правил доступа
                .RegisterExtension<ICardMetadataExtension, KrPermissionsExtensionMetadataExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 1)
                    .WithUnity(this.UnityContainer))

                //Рассчитываем права на карточку и помещаем их в токен в инфо,
                //порядок = 11 чтобы отработать после заполнения основной карточки из сателлита (0)
                //или заполнения карточки значениями по умолчанию (10)
                //Должен выполняться до KrTileNewGetExtension
                .RegisterExtension<ICardGetExtension, KrPermissionsNewGetExtension>(x => x
                    .WithOrder(ExtensionStage.BeforePlatform, 11)
                    .WithUnity(this.UnityContainer)
                    .WhenMethod(CardGetMethod.Default, CardGetMethod.Export))
                .RegisterExtension<ICardGetExtension, KrPermissionsMaskDataGetExtension>(x => x
                    .WithOrder(ExtensionStage.BeforePlatform, 12)
                    .WithUnity(this.UnityContainer)
                    .WhenMethod(CardGetMethod.Default, CardGetMethod.Export))

                // экспорт карточки "Правило доступа", очищаем виртуальные поля
                .RegisterExtension<ICardGetExtension, KrPermissionsRulesExportExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 1)
                    .WithSingleton()
                    .WhenMethod(CardGetMethod.Export)
                    .WhenCardTypes(DefaultCardTypes.KrPermissionsTypeID))

                //Проверка возможности создания карточки
                //Должен выполняться до KrTileNewGetExtension
                .RegisterExtension<ICardNewExtension, KrPermissionsNewGetExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 11)
                    .WithUnity(this.UnityContainer)
                    .WhenAnyNewMethod())

                //Проверка прав при сохранении карточки
                .RegisterExtension<ICardStoreExtension, KrPermissionsStoreExtension>(x => x
                    .WithOrder(ExtensionStage.BeforePlatform, int.MinValue)
                    .WithUnity(this.UnityContainer))

                //Проверка прав при удалении карточки
                .RegisterExtension<ICardDeleteExtension, KrPermissionsDeleteExtension>(x => x
                    .WithOrder(ExtensionStage.BeforePlatform, 1)
                    .WithUnity(this.UnityContainer))

                //Проверка карточки каких типов не может создавать пользователь чтобы скрывать тайлы
                .RegisterExtension<ICardRequestExtension, KrGetUnavailableTypesForCreationGetExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 1)
                    .WithUnity(this.UnityContainer)
                    .WhenRequestTypes(DefaultRequestTypes.GetUnavailableTypes))

                //Предупреждение о невозможности создать карточку по шаблону при создании шаблона
                .RegisterExtension<ICardNewExtension, KrWarnCannotCreateWhenCreatingTemplate>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 1)
                    .WithUnity(this.UnityContainer)
                    .WhenCardTypes(CardHelper.TemplateTypeID))

                .RegisterExtension<ICardPermissionsExtension, KrRequestCalculateFullCardPermissionExtension>(x => x
                    .WithOrder(ExtensionStage.BeforePlatform, 0)
                    .WithSingleton())
                .RegisterExtension<ICardPermissionsExtension, KrAclReadAccessCardPermissionExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 0)
                    .WithUnity(this.UnityContainer))

                .RegisterExtension<ITaskPermissionsExtension, KrProcessTasksPermissionsExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 1)
                    .WithUnity(this.UnityContainer))
                    ;
        }
    }
}
