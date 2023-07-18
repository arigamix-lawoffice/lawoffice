using System;
using Tessa.Extensions.Default.Client.Workplaces;
using Tessa.Extensions.Default.Client.Workplaces.Manager;
using Tessa.Extensions.Default.Client.Workplaces.WebChart;
using Tessa.Platform;
using Tessa.UI.Views.Charting;
using Tessa.UI.Views.Extensions;
using Tessa.Views;
using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Default.Client.Views
{
    [Registrator]
    public sealed class Registrator : RegistratorBase
    {
        public override void RegisterUnity()
        {
            // Регистрация клиентских представлений в контейнере приложения должна осуществляется с уникальным именем
            // желательно совпадающим с алиасом представления в метаданных. В случае не уникальности имен
            // в контейнере и IViewService будет зарегистрировано представление последним осуществившее
            // регистрацию в контейнере. Регистрация в IViewService будет осуществлена по алиасу из метаданных представления

            this.UnityContainer
                .RegisterType<ITessaView, ClientProgramView>(nameof(ClientProgramView), new ContainerControlledLifetimeManager())
                .RegisterType<IAdvancedFilterViewDialogManager, AdvancedFilterViewDialogManager>(new ContainerControlledLifetimeManager())
                .RegisterType<IFilterViewDialogDescriptorRegistry, FilterViewDialogDescriptorRegistry>(new ContainerControlledLifetimeManager())
                ;
        }


        public override void FinalizeRegistration()
        {
            // типы могут быть не зарегистрированы в тестах или плагинах Chronos

            this.UnityContainer
                .RegisterType<ImageCache>(new ContainerControlledLifetimeManager())
                .TryResolve<IWorkplaceExtensionRegistry>()
                ?
                .Register(typeof(CreateCardExtension))
                .RegisterConfiguratorType(
                    typeof(CreateCardExtension),
                    type => this.UnityContainer.Resolve<CreateCardExtensionConfigurator>())

                .Register(typeof(CustomButtonWorkplaceComponentExtension))
                .RegisterConfiguratorType(
                    typeof(CustomButtonWorkplaceComponentExtension),
                    type => this.UnityContainer.Resolve<CustomButtonWorkplaceComponentExtensionConfigurator>())

                .Register(typeof(RecordViewExtension))
                .RegisterConfiguratorType(
                    typeof(RecordViewExtension),
                    type => this.UnityContainer.Resolve<RecordViewExtensionConfigurator>())

                .Register(typeof(GetDataWithDelayExtension))
                .RegisterConfiguratorType(
                    typeof(GetDataWithDelayExtension),
                    type => this.UnityContainer.Resolve<GetDataWithDelayExtensionConfigurator>())

                .Register(typeof(TreeViewItemTestExtension))
                .RegisterConfiguratorType(
                    typeof(TreeViewItemTestExtension),
                    type => this.UnityContainer.Resolve<TreeViewItemTestExtensionConfigurator>())

                .Register(typeof(CustomFolderViewExtension))
                .RegisterConfiguratorType(
                    typeof(CustomFolderViewExtension),
                    type => this.UnityContainer.Resolve<CustomFolderViewExtensionConfigurator>())

                .Register(typeof(CustomNavigationViewExtension))
                .RegisterConfiguratorType(
                    typeof(CustomNavigationViewExtension),
                    type => this.UnityContainer.Resolve<CustomNavigationViewExtensionConfigurator>())

                .Register(typeof(ViewsContextMenuExtension))
                .RegisterConfiguratorType(
                    typeof(ViewsContextMenuExtension),
                    type => this.UnityContainer.Resolve<ViewsContextMenuExtensionConfigurator>())

                .Register(typeof(ChartViewExtension))
                .RegisterConfiguratorType(
                    typeof(ChartViewExtension),
                    type => this.UnityContainer.Resolve<ChartViewExtensionConfigurator>())

                .Register(typeof(AutomaticNodeRefreshExtension))
                .RegisterConfiguratorType(
                    typeof(AutomaticNodeRefreshExtension),
                    type => this.UnityContainer.Resolve<AutomaticNodeRefreshExtensionConfigurator>())

                .Register(typeof(ManagerWorkplaceExtension))
                .RegisterConfiguratorType(
                    typeof(ManagerWorkplaceExtension),
                    type => this.UnityContainer.Resolve<ManagerWorkplaceExtensionConfigurator>())

                .Register(typeof(WebChartWorkplaceExtension))
                .RegisterConfiguratorType(
                    typeof(WebChartWorkplaceExtension),
                    type => this.UnityContainer.Resolve<WebChartWorkplaceExtensionConfigurator>())

                .Register(typeof(PreviewExtension))
                .RegisterConfiguratorType(
                    typeof(PreviewExtension),
                    type => this.UnityContainer.Resolve<PreviewExtensionConfigurator>())

                .Register(typeof(RefSectionExtension))
                .RegisterConfiguratorType(
                    typeof(RefSectionExtension),
                    type => this.UnityContainer.Resolve<RefSectionExtensionConfigurator>())

                .Register(typeof(FilterViewDialogOverrideWorkplaceComponentExtension))
                .RegisterConfiguratorType(
                    typeof(FilterViewDialogOverrideWorkplaceComponentExtension),
                    type => this.UnityContainer.Resolve<FilterViewDialogOverrideWorkplaceComponentExtensionConfigurator>())

                .Register(typeof(TagsWorkplaceViewDemoActionExtension))
                .RegisterConfiguratorType(
                    typeof(TagsWorkplaceViewDemoActionExtension),
                    type => this.UnityContainer.Resolve<TagsWorkplaceViewDemoActionExtensionConfigurator>())

                .Register(typeof(TagCardsViewExtension))
                .RegisterConfiguratorType(
                    typeof(TagCardsViewExtension),
                    type => this.UnityContainer.Resolve<TagCardsViewExtensionConfigurator>());
                ;

            this.UnityContainer
                .TryResolve<IFilterViewDialogDescriptorRegistry>()
                ?
                .Register(
                    new Guid(0x23d03a10, 0xe610, 0x442d, 0x9e, 0x8d, 0x71, 0x4f, 0xf0, 0x58, 0x29, 0xf4),
                    FilterViewDialogDescriptors.Cars)
                ;
        }
    }
}
