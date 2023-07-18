using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared;
using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers.Requests
{
    [Registrator]
    public class Registrator : RegistratorBase
    {

        public override void RegisterUnity()
        {
            this.UnityContainer

                .RegisterType<KrSourceStageCommonMethodGetExtension>(new ContainerControlledLifetimeManager())
                .RegisterType<KrCompileCommonMethodStoreExtension>(new ContainerControlledLifetimeManager())
                .RegisterType<KrCommonMethodDeleteExtension>(new ContainerControlledLifetimeManager())

                .RegisterType<KrStageTemplateNewGetExtension>(new ContainerControlledLifetimeManager())
                .RegisterType<KrSourceStageTemplateGetExtension>(new ContainerControlledLifetimeManager())
                .RegisterType<KrStageTemplateStoreExtension>(new ContainerControlledLifetimeManager())
                .RegisterType<KrCompileStageTemplateStoreExtension>(new ContainerControlledLifetimeManager())
                .RegisterType<KrStageTemplateDeleteExtension>(new ContainerControlledLifetimeManager())

                .RegisterType<KrSourceStageGroupGetExtension>(new ContainerControlledLifetimeManager())
                .RegisterType<KrCompileStageGroupStoreExtension>(new ContainerControlledLifetimeManager())
                .RegisterType<KrStageGroupDeleteExtension>(new ContainerControlledLifetimeManager())

                .RegisterType<KrSecondaryProcessNewGetExtension>(new ContainerControlledLifetimeManager())
                .RegisterType<KrSourceSecondaryProcessGetExtension>(new ContainerControlledLifetimeManager())
                .RegisterType<KrSecondaryProcessStoreExtension>(new ContainerControlledLifetimeManager())
                .RegisterType<KrCompileSecondaryProcessStoreExtension>(new ContainerControlledLifetimeManager())
                .RegisterType<KrSecondaryProcessDeleteExtension>(new ContainerControlledLifetimeManager())

                .RegisterType<KrRecalcStagesStoreExtension>(new PerResolveLifetimeManager())
                ;

        }

        public override void RegisterExtensions(IExtensionContainer extensionContainer)
        {
            extensionContainer
                .RegisterExtension<ICardGetExtension, KrSourceStageCommonMethodGetExtension>(x => x
                    .WithOrder(ExtensionStage.BeforePlatform, 1)
                    .WithUnity(this.UnityContainer)
                    .WhenCardTypes(DefaultCardTypes.KrStageCommonMethodTypeID))
                .RegisterExtension<ICardStoreExtension, KrCompileCommonMethodStoreExtension>(x => x
                    .WithOrder(ExtensionStage.BeforePlatform, 1)
                    .WithUnity(this.UnityContainer)
                    .WhenAnyStoreMethod()
                    .WhenCardTypes(DefaultCardTypes.KrStageCommonMethodTypeID))
                .RegisterExtension<ICardDeleteExtension, KrCommonMethodDeleteExtension>(x => x
                    .WithOrder(ExtensionStage.BeforePlatform, 1)
                    .WithUnity(this.UnityContainer)
                    .WhenCardTypes(DefaultCardTypes.KrStageCommonMethodTypeID))

                .RegisterExtension<ICardNewExtension, KrStageTemplateNewGetExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 1)
                    .WithUnity(this.UnityContainer)
                    .WhenCardTypes(DefaultCardTypes.KrStageTemplateTypeID)
                    .WhenMethod(CardNewMethod.Default, CardNewMethod.Template))
                .RegisterExtension<ICardGetExtension, KrStageTemplateNewGetExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 1)
                    .WithUnity(this.UnityContainer)
                    .WhenCardTypes(DefaultCardTypes.KrStageTemplateTypeID)
                    .WhenMethod(CardGetMethod.Default))
                .RegisterExtension<ICardGetExtension, KrSourceStageTemplateGetExtension>(x => x
                    .WithOrder(ExtensionStage.BeforePlatform, 2)
                    .WithUnity(this.UnityContainer)
                    .WhenCardTypes(DefaultCardTypes.KrStageTemplateTypeID))
                .RegisterExtension<ICardStoreExtension, KrStageTemplateStoreExtension>(x => x
                    .WithOrder(ExtensionStage.BeforePlatform, 1)
                    .WithUnity(this.UnityContainer)
                    .WhenCardTypes(DefaultCardTypes.KrStageTemplateTypeID))
                // Должно выполнятся после KrStageTemplateStoreExtension
                .RegisterExtension<ICardStoreExtension, KrCompileStageTemplateStoreExtension>(x => x
                    .WithOrder(ExtensionStage.BeforePlatform, 2)
                    .WithUnity(this.UnityContainer)
                    .WhenAnyStoreMethod()
                    .WhenCardTypes(DefaultCardTypes.KrStageTemplateTypeID))
                .RegisterExtension<ICardDeleteExtension, KrStageTemplateDeleteExtension>(x => x
                    .WithOrder(ExtensionStage.BeforePlatform, 1)
                    .WithUnity(this.UnityContainer)
                    .WhenCardTypes(DefaultCardTypes.KrStageTemplateTypeID, DefaultCardTypes.KrSecondaryProcessTypeID))

                .RegisterExtension<ICardNewExtension, KrStageGroupNewGetExtension>(x => x
                    .WithOrder(ExtensionStage.BeforePlatform, 1)
                    .WithSingleton()
                    .WhenCardTypes(DefaultCardTypes.KrStageGroupTypeID)
                    .WhenMethod(CardNewMethod.Default, CardNewMethod.Template))
                .RegisterExtension<ICardGetExtension, KrStageGroupNewGetExtension>(x => x
                    .WithOrder(ExtensionStage.BeforePlatform, 1)
                    .WithSingleton()
                    .WhenCardTypes(DefaultCardTypes.KrStageGroupTypeID)
                    .WhenMethod(CardGetMethod.Default))
                .RegisterExtension<ICardGetExtension, KrSourceStageGroupGetExtension>(x => x
                    .WithOrder(ExtensionStage.BeforePlatform, 2)
                    .WithUnity(this.UnityContainer)
                    .WhenCardTypes(DefaultCardTypes.KrStageGroupTypeID))
                .RegisterExtension<ICardStoreExtension, KrCompileStageGroupStoreExtension>(x => x
                    .WithOrder(ExtensionStage.BeforePlatform, 1)
                    .WithUnity(this.UnityContainer)
                    .WhenAnyStoreMethod()
                    .WhenCardTypes(DefaultCardTypes.KrStageGroupTypeID))

                .RegisterExtension<ICardDeleteExtension, KrStageGroupDeleteExtension>(x => x
                    .WithOrder(ExtensionStage.BeforePlatform, 1)
                    .WithUnity(this.UnityContainer)
                    .WhenCardTypes(DefaultCardTypes.KrStageGroupTypeID, DefaultCardTypes.KrSecondaryProcessTypeID))

                .RegisterExtension<ICardNewExtension, KrSecondaryProcessNewGetExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 1)
                    .WithUnity(this.UnityContainer)
                    .WhenCardTypes(DefaultCardTypes.KrSecondaryProcessTypeID)
                    .WhenMethod(CardNewMethod.Default, CardNewMethod.Template))
                .RegisterExtension<ICardGetExtension, KrSecondaryProcessNewGetExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 1)
                    .WithUnity(this.UnityContainer)
                    .WhenCardTypes(DefaultCardTypes.KrSecondaryProcessTypeID)
                    .WhenMethod(CardGetMethod.Default))
                .RegisterExtension<ICardGetExtension, KrSourceSecondaryProcessGetExtension>(x => x
                    .WithOrder(ExtensionStage.BeforePlatform, 2)
                    .WithUnity(this.UnityContainer)
                    .WhenCardTypes(DefaultCardTypes.KrSecondaryProcessTypeID))
                .RegisterExtension<ICardStoreExtension, KrSecondaryProcessStoreExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 1)
                    .WithUnity(this.UnityContainer)
                    .WhenCardTypes(DefaultCardTypes.KrSecondaryProcessTypeID)
                    .WhenMethod(CardStoreMethod.Default, CardStoreMethod.Import))
                .RegisterExtension<ICardStoreExtension, KrCompileSecondaryProcessStoreExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 2)
                    .WithUnity(this.UnityContainer)
                    .WhenAnyStoreMethod()
                    .WhenCardTypes(DefaultCardTypes.KrSecondaryProcessTypeID))
                .RegisterExtension<ICardDeleteExtension, KrSecondaryProcessDeleteExtension>(x => x
                    .WithOrder(ExtensionStage.BeforePlatform, 1)
                    .WithUnity(this.UnityContainer)
                    .WhenCardTypes(DefaultCardTypes.KrSecondaryProcessTypeID))

                .RegisterExtension<ICardStoreExtension, KrRecalcStagesStoreExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 10)
                    .WithUnity(this.UnityContainer))

                ;
        }
    }
}
