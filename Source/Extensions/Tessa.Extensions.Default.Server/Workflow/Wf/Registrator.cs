using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Server.Workflow.KrPermissions;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.Wf;
using Tessa.Extensions.Platform.Server.Cards.Satellites;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Server.Workflow.Wf
{
    [Registrator]
    public sealed class Registrator : RegistratorBase
    {
        public override void RegisterUnity()
        {
            this.UnityContainer
                .RegisterType<WfTasksServerGetExtension>(new ContainerControlledLifetimeManager())
                .RegisterType<WfGetResolutionVisualizationDataRequestExtension>(new ContainerControlledLifetimeManager())
                .RegisterType<WfCardMetadataExtension>(new ContainerControlledLifetimeManager(), new InjectionConstructor())
                .RegisterType<WfWorkflowStoreExtension>(new PerResolveLifetimeManager())

                .RegisterType<WfTaskSatelliteHandler>(new ContainerControlledLifetimeManager())
                .RegisterType<WfSatelliteHandler>(new ContainerControlledLifetimeManager())
                ;
        }


        public override void RegisterExtensions(IExtensionContainer extensionContainer)
        {
            // Metadata
            extensionContainer
                .RegisterExtension<ICardMetadataExtension, WfCardMetadataExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 1)
                    .WithUnity(this.UnityContainer))
                ;

            // Get
            extensionContainer

                .RegisterExtension<ICardGetExtension, WfTasksServerGetExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 4)
                    .WithUnity(this.UnityContainer)
                    .WhenMethod(CardGetMethod.Default, CardGetMethod.Backup, CardGetMethod.Export))
                ;

            // Store
            extensionContainer
                .RegisterExtension<ICardStoreExtension, WfWorkflowStoreExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 1)
                    .WithUnity(this.UnityContainer))
                ;

            // Store Task
            extensionContainer
                .RegisterExtension<ICardStoreTaskExtension, WfResolutionStoreTaskExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 1)
                    .WithSingleton()
                    .WhenTaskTypes(WfHelper.ResolutionTaskTypeIDList))
                .RegisterExtension<ICardStoreTaskExtension, WfResolutionCheckSafeLimitStoreTaskExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 2)
                    .WithSingleton()
                    .WhenTaskTypes(WfHelper.ResolutionTaskTypeIDList))
                ;

            // Request
            extensionContainer
               .RegisterExtension<ICardRequestExtension, WfGetResolutionVisualizationDataRequestExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 1)
                    .WithUnity(this.UnityContainer)
                    .WhenRequestTypes(DefaultRequestTypes.GetResolutionVisualizationData))
                ;

            extensionContainer
                //Расширение на выдачу прав по резолюциям Wf
                .RegisterExtension<ITaskPermissionsExtension, WfTasksPermissionsExtension>(x => x
                    .WithOrder(ExtensionStage.AfterPlatform, 2)
                    .WithSingleton()
                    .WhenTaskTypes(WfHelper.ResolutionTaskTypeIDList))
                ;        
        }

        public override void FinalizeRegistration()
        {
            var registry = this.UnityContainer.Resolve<ISatelliteTypeRegistry>();
            registry.Register(new SatelliteTypeDescriptor(DefaultCardTypes.WfTaskCardTypeID)
            {
                HandlerType = typeof(WfTaskSatelliteHandler),
                IsDeferredStore = true,
                IsSingleton = true,
                IsTaskSatellite = true,
                LoadMainCardFiles = true,
                AllowGetFromClient = true,
                AllowStoreFromClient = true,
            });

            registry.Register(new SatelliteTypeDescriptor(DefaultCardTypes.WfSatelliteTypeID)
            {
                HandlerType = typeof(WfSatelliteHandler),
                IsSingleton = true,
                IgnoreStoreExtensions = true,
            });
        }
    }
}
