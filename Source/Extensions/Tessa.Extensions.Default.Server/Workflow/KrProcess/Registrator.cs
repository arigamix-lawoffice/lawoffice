using Tessa.Cards;
using Tessa.Cards.Caching;
using Tessa.Cards.Extensions;
using Tessa.Cards.Metadata;
using Tessa.Extensions.Default.Server.Cards;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Serialization;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Settings;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;
using Tessa.Platform.Data;
using Tessa.Platform.Runtime;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess
{
    [Registrator]
    public sealed class Registrator : RegistratorBase
    {
        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterType<IKrStageSerializer, KrStageSerializer>(new ContainerControlledLifetimeManager())
                ;

            this.UnityContainer
                .RegisterType<KrCardMetadataExtension>(
                    // Расширение выполняется только при старте,
                    // производительность не снижает.
                    new PerResolveLifetimeManager(),
                    new InjectionConstructor(
                        typeof(IDbScope),
                        typeof(IKrProcessContainer),
                        typeof(IKrStageSerializer)))
                .RegisterType<KrSecondaryProcessMetadataExtension>(new ContainerControlledLifetimeManager())
                ;

            this.UnityContainer
                .RegisterType<IKrTokenProvider, KrTokenProvider>(new ContainerControlledLifetimeManager())
                .RegisterType<ICardServerPermissionsProvider, KrCardServerPermissionsProvider>(new ContainerControlledLifetimeManager())
                .RegisterType<ICardTaskAccessProvider, KrCardTaskAccessProvider>(new ContainerControlledLifetimeManager())
                .RegisterType<IKrTypesCache, KrTypesCache>(
                    new ContainerControlledLifetimeManager(),
                    new InjectionConstructor(
                        typeof(IDbScope),
                        typeof(ICardMetadata),
                        typeof(ICardCache),
                        typeof(IUnityDisposableContainer)))
                ;

            this.UnityContainer
                .RegisterType<KrSetDefaultSettingsValuesGetExtension>(new ContainerControlledLifetimeManager())
                .RegisterType<KrSetTemplateDocTypeNewExtension>(new ContainerControlledLifetimeManager())
                .RegisterType<KrDocTypeInvalidateSettingsCacheStoreExtension>(new ContainerControlledLifetimeManager())
                .RegisterType<KrDocTypeInvalidateSettingsCacheDeleteExtension>(new ContainerControlledLifetimeManager())
                .RegisterType<KrGetDocTypesCardExtension>(new ContainerControlledLifetimeManager())
                .RegisterType<KrCreateBasedOnNewExtension>(new ContainerControlledLifetimeManager())
                .RegisterType<KrResolutionStoreExtensions>(new ContainerControlledLifetimeManager())
                .RegisterType<KrUniversalTaskSettingsStoreExtension>(new ContainerControlledLifetimeManager())
                .RegisterType<KrAcquaintanceSettingsStoreExtension>(new ContainerControlledLifetimeManager())
                .RegisterType<KrStrictSecurityCardNewGetExtension>(new ContainerControlledLifetimeManager())
                .RegisterType<KrStrictSecurityCardStoreExtension>(new ContainerControlledLifetimeManager())
                .RegisterType<KrStrictSecurityCardDeleteExtension>(new ContainerControlledLifetimeManager())
                .RegisterType<KrStagesImportExtension>(new ContainerControlledLifetimeManager())
                ;

            this.UnityContainer
                .RegisterType<PartnersCardMetadataExtension>(
                    new ContainerControlledLifetimeManager(),
                    new InjectionConstructor(typeof(IDbScope)))
                ;

            this.UnityContainer
                .RegisterType<KrSettingsInvalidateTypeCacheStoreExtension>(
                    new ContainerControlledLifetimeManager(),
                    new InjectionConstructor(
                        typeof(CardMetadataCache),
                        typeof(IKrTypesCache),
                        typeof(ISession)))
                ;

            this.UnityContainer
                .RegisterType<KrSettingsInvalidateTypeCacheDeleteExtension>(
                    new ContainerControlledLifetimeManager(),
                    new InjectionConstructor(
                        typeof(CardMetadataCache),
                        typeof(IKrTypesCache),
                        typeof(ISession)))
                ;
        }

        public override void RegisterExtensions(IExtensionContainer extensionContainer)
        {
            extensionContainer
                .RegisterExtension<ICardMetadataExtension, KrSecondaryProcessMetadataExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 0)
                    .WithUnity(this.UnityContainer))
                // Формирование метадаты карточек типового решения.
                .RegisterExtension<ICardMetadataExtension, KrCardMetadataExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 1)
                    .WithUnity(this.UnityContainer))

                // Отключение ручного ввода и авто-создания контрагентов, если в настройках это выключено
                .RegisterExtension<ICardMetadataExtension, PartnersCardMetadataExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform)
                    .WithUnity(this.UnityContainer))
                ;

            extensionContainer
                // Сохраняет карточку-сателлит при импорте
                .RegisterExtension<ICardStoreExtension, FixTaskHistoryCardStoreExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 2)
                    .WithSingleton()
                    .WhenMethod(CardStoreMethod.Default))

                // Заполнение виртуальной секции результатов комментарования
                .RegisterExtension<ICardGetExtension, FillCommentsVirtualSectionGetExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 1)
                    .WithSingleton())

                .RegisterExtension<ICardNewExtension, KrSetDefaultSettingsValuesNewExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 10)
                    .WithSingleton()
                    .WhenCardTypes(DefaultCardTypes.KrSettingsTypeID, DefaultCardTypes.KrDocTypeTypeID))

                .RegisterExtension<ICardGetExtension, KrSetDefaultSettingsValuesGetExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 10)
                    .WithUnity(this.UnityContainer)
                    .WhenCardTypes(DefaultCardTypes.KrSettingsTypeID))
                ;

            extensionContainer
                // Использует тип документа вместо типа карточки при создании шаблона
                .RegisterExtension<ICardNewExtension, KrSetTemplateDocTypeNewExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 1)
                    .WithUnity(this.UnityContainer)
                    .WhenCardTypes(CardHelper.TemplateTypeID))

                ;

            extensionContainer
                // Должно выполняться строго после KrProcessWorkflowStoreExtension
                .RegisterExtension<ICardStoreExtension, KrAdditionalApprovalCardStoreExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 26)
                    .WithSingleton())

                .RegisterExtension<ICardGetExtension, KrAdditionalApprovalCardGetExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 4)
                    .WithSingleton()
                    .WhenMethod(CardGetMethod.Default, CardGetMethod.Backup, CardGetMethod.Export))

                // Сбрасывает кэш при изменении списка типов карточек в настройках процесса
                .RegisterExtension<ICardStoreExtension, KrSettingsInvalidateTypeCacheStoreExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform)
                    .WithUnity(this.UnityContainer)
                    .WhenCardTypes(DefaultCardTypes.KrSettingsTypeID)
                    .WhenAnyStoreMethod())

                .RegisterExtension<ICardDeleteExtension, KrSettingsInvalidateTypeCacheDeleteExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform)
                    .WithUnity(this.UnityContainer)
                    .WhenCardTypes(DefaultCardTypes.KrSettingsTypeID))
                ;

            extensionContainer
                // Сбрасывает кэш при изменении типов документов
                .RegisterExtension<ICardStoreExtension, KrDocTypeInvalidateSettingsCacheStoreExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform)
                    .WithUnity(this.UnityContainer)
                    .WhenCardTypes(DefaultCardTypes.KrDocTypeTypeID)
                    .WhenAnyStoreMethod())

                // Сброс кэша при удалении карточки типа документа
                .RegisterExtension<ICardDeleteExtension, KrDocTypeInvalidateSettingsCacheDeleteExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform)
                    .WithUnity(this.UnityContainer)
                    .WhenCardTypes(DefaultCardTypes.KrDocTypeTypeID))

                // Прокидывает серверный кэш в запрос от клиента
                .RegisterExtension<ICardRequestExtension, KrGetDocTypesCardExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform)
                    .WithUnity(this.UnityContainer)
                    .WhenRequestTypes(DefaultRequestTypes.KrGetDocTypes))
                ;

            extensionContainer
                // копирование данных при создании на основании другой карточки
                .RegisterExtension<ICardNewExtension, KrCreateBasedOnNewExtension>(x => x
                    .WithOrder(ExtensionStage.BeforePlatform, 1)
                    .WithUnity(this.UnityContainer)
                    .WhenMethod(CardNewMethod.Default, CardNewMethod.Template))
                ;

            extensionContainer
                // отправка сигналу этапу о завершении задачи Workflow
                .RegisterExtension<ICardStoreTaskExtension, KrResolutionStoreExtensions>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform)
                    .WithUnity(this.UnityContainer)
                    .WhenTaskTypes(DefaultTaskTypes.WfResolutionTypeID, DefaultTaskTypes.WfResolutionChildTypeID, DefaultTaskTypes.WfResolutionControlTypeID))
                ;

            extensionContainer
                // смена родительского задания при делегировании для дополнительных согласований
                // должно выполняться строго после KrProcessWorkflowStoreExtension
                .RegisterExtension<ICardStoreExtension, KrReassignAdditionalApprovalStoreExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 25)
                    .WithSingleton()
                    .WhenTaskTypes(DefaultTaskTypes.KrApproveTypeID))
                ;

            extensionContainer
                .RegisterExtension<ICardStoreTaskExtension, KrUniversalTaskStoreExtension>(x => x
                    .WithOrder(ExtensionStage.BeforePlatform)
                    .WithSingleton()
                    .WhenTaskTypes(DefaultTaskTypes.KrUniversalTaskTypeID))
                .RegisterExtension<ICardStoreExtension, KrUniversalTaskSettingsStoreExtension>(x => x
                    .WithOrder(ExtensionStage.BeforePlatform)
                    .WithUnity(this.UnityContainer))
                .RegisterExtension<ICardGetExtension, KrUniversalTaskSettingsGetExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, int.MaxValue)
                    .WithSingleton()
                    .WhenCardTypes(DefaultCardTypes.KrStageTemplateTypeID))
                ;

            extensionContainer
                .RegisterExtension<ICardStoreExtension, KrNotificationSettingsStoreExtension>(x => x
                    .WithOrder(ExtensionStage.BeforePlatform)
                    .WithSingleton())
                ;

            extensionContainer
                .RegisterExtension<ICardStoreExtension, KrAcquaintanceSettingsStoreExtension>(x => x
                    .WithOrder(ExtensionStage.BeforePlatform)
                    .WithUnity(this.UnityContainer))
                ;
            
            // экспорт карточки "Правило доступа", очищаем виртуальные поля, при импорте восстанавливаем
            extensionContainer
                .RegisterExtension<ICardGetExtension, KrStagesExportExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 1)
                    .WithSingleton()
                    .WhenMethod(CardGetMethod.Export)
                    .WhenCardTypes(
                        DefaultCardTypes.KrSecondaryProcessTypeID,
                        DefaultCardTypes.KrStageTemplateTypeID))
                .RegisterExtension<ICardStoreExtension, KrStagesImportExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 1)
                    .WithUnity(this.UnityContainer)
                    .WhenMethod(CardStoreMethod.Import)
                    .WhenCardTypes(
                        DefaultCardTypes.KrSecondaryProcessTypeID,
                        DefaultCardTypes.KrStageTemplateTypeID))
                .RegisterExtension<ICardGetExtension, KrSecondaryProcessExportExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 2)
                    .WithSingleton()
                    .WhenMethod(CardGetMethod.Export)
                    .WhenCardTypes(DefaultCardTypes.KrSecondaryProcessTypeID));

            // StrictSecurity
            extensionContainer
                .RegisterExtension<ICardNewExtension, KrStrictSecurityCardNewGetExtension>(x => x
                    .WithOrder(ExtensionStage.BeforePlatform, 10)
                    .WithUnity(this.UnityContainer)
                    .WhenCardTypes(
                        DefaultCardTypes.KrStageTemplateTypeID,
                        DefaultCardTypes.KrStageGroupTypeID,
                        DefaultCardTypes.KrSecondaryProcessTypeID,
                        DefaultCardTypes.KrStageCommonMethodTypeID))

                .RegisterExtension<ICardGetExtension, KrStrictSecurityCardNewGetExtension>(x => x
                    .WithOrder(ExtensionStage.BeforePlatform, 10)
                    .WithUnity(this.UnityContainer)
                    .WhenCardTypes(
                        DefaultCardTypes.KrStageTemplateTypeID,
                        DefaultCardTypes.KrStageGroupTypeID,
                        DefaultCardTypes.KrSecondaryProcessTypeID,
                        DefaultCardTypes.KrStageCommonMethodTypeID))

                .RegisterExtension<ICardStoreExtension, KrStrictSecurityCardStoreExtension>(x => x
                    .WithOrder(ExtensionStage.BeforePlatform, 10)
                    .WithUnity(this.UnityContainer)
                    .WhenCardTypes(
                        DefaultCardTypes.KrStageTemplateTypeID,
                        DefaultCardTypes.KrStageGroupTypeID,
                        DefaultCardTypes.KrSecondaryProcessTypeID,
                        DefaultCardTypes.KrStageCommonMethodTypeID))

                .RegisterExtension<ICardDeleteExtension, KrStrictSecurityCardDeleteExtension>(x => x
                    .WithOrder(ExtensionStage.BeforePlatform, 10)
                    .WithUnity(this.UnityContainer)
                    .WhenCardTypes(
                        DefaultCardTypes.KrStageTemplateTypeID,
                        DefaultCardTypes.KrStageGroupTypeID,
                        DefaultCardTypes.KrSecondaryProcessTypeID,
                        DefaultCardTypes.KrStageCommonMethodTypeID))
                ;
        }
    }
}
