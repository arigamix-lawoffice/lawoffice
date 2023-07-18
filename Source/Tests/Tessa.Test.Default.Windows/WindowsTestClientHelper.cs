using System;
using System.Collections.Generic;
using System.Linq;
using Tessa.Cards.Caching;
using Tessa.Platform;
using Tessa.Platform.Data;
using Tessa.Platform.Placeholders;
using Tessa.Platform.Runtime;
using Tessa.Test.Default.Shared.Views;
using Tessa.UI.Controls;
using Tessa.UI.Views;
using Tessa.UI.Views.Extensions;
using Tessa.UI.Views.Filtering;
using Tessa.UI.Views.MessagingServices;
using Tessa.UI.Views.Workplaces;
using Tessa.UI.Views.Workplaces.Tree;
using Tessa.Views;
using Tessa.Views.Json;
using Tessa.Views.Json.Converters;
using Tessa.Views.Metadata;
using Tessa.Views.Metadata.Types;
using Tessa.Views.Parser.SyntaxTree;
using Tessa.Views.Parser.SyntaxTree.Expressions;
using Tessa.Views.Parser.SyntaxTree.ViewMetadata;
using Tessa.Views.SearchQueries;
using Tessa.Views.Validation;
using Tessa.Views.Workplaces;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using Unity.Resolution;

namespace Tessa.Test.Default.Windows
{
    /// <summary>
    /// Предоставляет вспомогательные методы, используемые в клиентских тестах с поддержкой пользовательского интерфейса.
    /// </summary>
    public static class WindowsTestClientHelper
    {
        #region Static Methods

        /// <summary>
        /// Регистрирует зависимости для работы с представлениями в указанном контейнере.
        /// </summary>
        /// <param name="unityContainer">Контейнер в котором должны быть зарегистрированы зависимости.</param>
        /// <returns>Контейнер, указанный в <paramref name="unityContainer"/> для создания цепочки вызовов.</returns>
        public static IUnityContainer RegisterViews(this IUnityContainer unityContainer)
        {
            Check.ArgumentNotNull(unityContainer, nameof(unityContainer));

            ViewValidationKeys.Register();
            WorkplaceValidationKeys.Register();
            var mediator = new Mediator();
            var implementer = new TestRepositoryImplementer(mediator, Enumerable.Empty<TessaViewModel>);
            var repository = new TessaViewModelRepository(implementer);

            SyntaxTreeRegistration.Register(unityContainer);
            TypeConverterRegistration.Register(unityContainer);

            unityContainer
                .RegisterType<IDbScope, DbScope>(
                    new ContainerControlledLifetimeManager(),
                    new InjectionConstructor(typeof(Func<DbManager>)))
                .RegisterFactory<IEnumerable<ITessaView>>(c => c.ResolveAll<ITessaView>())
                .RegisterFactory<IViewServiceImplementer>(
                    c => new TestViewServiceImplementer(repository,
                        mediator,
                        () => c.Resolve<ISession>(),
                        c.Resolve<IViewMetadataInterpreter>(),
                        c.Resolve<IQueryGeneratorFactory>(),
                        c.Resolve<ViewMetadataEvaluationContextFactory>(),
                        c.Resolve<NormalizeParameterNameResolver>(),
                        c.Resolve<IDbScope>(),
                        c.Resolve<IViewMetadataConverter<IJsonViewMetadata, IViewMetadata>>(),
                        c.Resolve<IErrorManager>(),
                        c.Resolve<ICardCache>()
                    ),
                    new ContainerControlledLifetimeManager())
                .RegisterInstance<IRepository<IGetModelRequest, IStoreTessaViewRequest, IEnumerable<TessaViewModel>>>(implementer)
                .RegisterType<TessaViewModelRepository>(new ContainerControlledLifetimeManager())
                .RegisterType<IViewService, ViewService>(new ContainerControlledLifetimeManager())
                .RegisterType<ISearchQueryService, SearchQueryServiceClient>(new ContainerControlledLifetimeManager())
                ;

            ViewMetadataConverterRegistration.Register(unityContainer);
            TypeConverterRegistration.Register(unityContainer);
            TreeItemLoaderRegistration.Register(unityContainer);
            FilterRegistration.Register(unityContainer);

            unityContainer
                .RegisterType<WorkplaceView>(new PerResolveLifetimeManager())
                .RegisterFactory<Func<WorkplaceMetadata, WorkplaceView>>(
                    c => new Func<WorkplaceMetadata, WorkplaceView>(
                        metadata => c.Resolve<WorkplaceView>(new ParameterOverride("metadata", metadata))),
                    new ContainerControlledLifetimeManager())
                .RegisterType<IWorkplaceViewModel, WorkplaceViewModel>(new PerResolveLifetimeManager())
                .RegisterType<IOpenedCardObserver, OpenedCardObserver>(new ContainerControlledLifetimeManager())
                .RegisterType<IViewParameterMetadata, ViewParameterMetadata>()
                .RegisterType<IViewCardParameters, ViewCardParameters>(new ContainerControlledLifetimeManager())
                .RegisterType<IViewCurrentUserParameters, ViewCurrentUserParameters>()
                .RegisterType<IViewPagingParameters, ViewPagingParameters>()
                .RegisterType<IViewSpecialParameters, ViewSpecialParameters>(new ContainerControlledLifetimeManager())
                .RegisterType<IWorkplaceService, WorkplaceService>(new ContainerControlledLifetimeManager())
                .RegisterType<IWorkplaceCreationContext, WorkplaceCreationContext>(new PerResolveLifetimeManager())
                .RegisterType<ITreeItemFactory, TreeItemFactory>(new ContainerControlledLifetimeManager())
                .RegisterFactory<Func<IWorkplaceMetadata, string, IDoubleClickAction, IEnumerable<RequestParameter>, IWorkplaceCreationContext>>(
                    c => new Func<IWorkplaceMetadata, string, IDoubleClickAction, IEnumerable<RequestParameter>, IWorkplaceCreationContext>(
                        (metadata, refSection, doubleClickAction, extraParameters) =>
                            c.Resolve<IWorkplaceCreationContext>(
                                new ParameterOverride("metadata", metadata),
                                new ParameterOverride("refSection", refSection),
                                new ParameterOverride("doubleClickAction", doubleClickAction),
                                new ParameterOverride("extraParameters", extraParameters ?? Enumerable.Empty<RequestParameter>()))),
                    new ContainerControlledLifetimeManager())
                .RegisterType<IContentProviderStorage, ContentProviderStorage>(new ContainerControlledLifetimeManager())
                .RegisterWorkplaceHandlers()
                .RegisterFactory<WorkplaceViewFactory>(
                    c => new WorkplaceViewFactory(
                        model => c.Resolve<WorkplaceView>(new ParameterOverride("viewModel", model))),
                    new ContainerControlledLifetimeManager())
                .RegisterType<IDoubleClickAction, DefaultViewDoubleClickAction>()
                .RegisterType<ITessaViewRequest, TessaViewRequest>(new PerResolveLifetimeManager())
                .RegisterFactory<RequestFactory>(
                    c => new RequestFactory(
                        metadata => c.Resolve<ITessaViewRequest>(new ParameterOverride("viewMetadata", metadata))),
                    new ContainerControlledLifetimeManager())
                .RegisterType<IDialogService, DialogService>(new ContainerControlledLifetimeManager())
                .RegisterFactory<Func<Action<ISearchQueryViewModel>, ISearchQueriesViewModel>>(
                    c => new Func<Action<ISearchQueryViewModel>, ISearchQueriesViewModel>(
                        doubleClickAction => c.Resolve<SearchQueriesViewModel>(
                            new ParameterOverride("doubleClickAction", doubleClickAction))),
                    new ContainerControlledLifetimeManager())
                .RegisterFactory<Func<Action<ISearchQueryViewModel>, ISearchQueryDialogViewModel>>(
                    c => new Func<Action<ISearchQueryViewModel>, ISearchQueryDialogViewModel>(
                        doubleClickAction => c.Resolve<SearchQueryDialogViewModel>(
                            new ParameterOverride("doubleClickAction", doubleClickAction))),
                    new ContainerControlledLifetimeManager())
                .RegisterType<ISearchQueryManageDialogViewModel, SearchQueryManageDialogViewModel>()
                .RegisterFactory<Func<bool, RequestParameterBuilder>>(
                    c => new Func<bool, RequestParameterBuilder>(
                        readOnly => c.Resolve<RequestParameterBuilder>(new ParameterOverride("asReadOnly", readOnly))),
                    new ContainerControlledLifetimeManager())
                ;

            unityContainer.RegisterWorkplaceExtensions();

            return unityContainer;
        }

        #endregion
    }
}
