using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards.Caching;
using Tessa.Platform;
using Tessa.Platform.Data;
using Tessa.Platform.Runtime;
using Tessa.Platform.Validation;
using Tessa.Properties.Resharper;
using Tessa.Test.Default.Shared.Roles;
using Tessa.Views;
using Tessa.Views.Json;
using Tessa.Views.Json.Converters;
using Tessa.Views.Metadata;
using Tessa.Views.Parser;
using Tessa.Views.Parser.SyntaxTree.Expressions;
using Tessa.Views.Parser.SyntaxTree.ViewMetadata;

namespace Tessa.Test.Default.Shared.Views
{
    /// <summary>
    ///     Тестовая имплементация сервиса представлений
    /// </summary>
    public sealed class TestViewServiceImplementer : IViewServiceImplementer
    {
        /// <summary>
        /// The context factory.
        /// </summary>
        private readonly ViewMetadataEvaluationContextFactory contextFactory;

        [NotNull]
        private readonly NormalizeParameterNameResolver normalizeParameterNameResolver;

        private readonly IDbScope dbScope;

        /// <summary>
        ///     The view metadata factory.
        /// </summary>
        [NotNull]
        private readonly IViewMetadataInterpreter metadataInterpreter;

        /// <summary>
        /// The query generator factory.
        /// </summary>
        [NotNull]
        private readonly IQueryGeneratorFactory queryGeneratorFactory;

        /// <summary>
        ///     The repository.
        /// </summary>
        private readonly TessaViewModelRepository repository;

        /// <summary>
        ///     The session accessor.
        /// </summary>
        private readonly Func<ISession> sessionAccessor;

        private volatile ReadOnlyCollection<ITessaView> allViews;

        private readonly IViewMetadataConverter<IJsonViewMetadata, IViewMetadata> jsonMetadataConverter;

        private readonly IErrorManager errorManager;

        private readonly ICardCache cardCache;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestViewServiceImplementer"/> class.
        /// </summary>
        /// <param name="repository">
        /// Репозиторий представлений
        /// </param>
        /// <param name="mediator">
        /// Посредник для получения уведомления о изменении репозитория
        /// </param>
        /// <param name="sessionAccessor">
        /// Поставщик сессии
        /// </param>
        /// <param name="metadataInterpreter">
        /// The view Metadata Factory.
        /// </param>
        /// <param name="queryGeneratorFactory">
        /// The query Generator Factory.
        /// </param>
        /// <param name="contextFactory">
        /// Фабрика создания контекста преобразования
        /// </param>
        /// <param name="normalizeParameterNameResolver"></param>
        /// <param name="dbScope"></param>
        public TestViewServiceImplementer(
            [NotNull] TessaViewModelRepository repository,
            [NotNull] IOneWayMediatorClient mediator,
            [NotNull] Func<ISession> sessionAccessor,
            [NotNull] IViewMetadataInterpreter metadataInterpreter,
            [NotNull] IQueryGeneratorFactory queryGeneratorFactory,
            [NotNull] ViewMetadataEvaluationContextFactory contextFactory,
            [NotNull] NormalizeParameterNameResolver normalizeParameterNameResolver,
            [NotNull] IDbScope dbScope,
            [NotNull] IViewMetadataConverter<IJsonViewMetadata, IViewMetadata> jsonMetadataConverter,
            [NotNull] IErrorManager errorManager,
            [NotNull] ICardCache cardCache
            )
        {
            Check.ArgumentNotNull(repository, nameof(repository));
            Check.ArgumentNotNull(mediator, nameof(mediator));
            Check.ArgumentNotNull(sessionAccessor, nameof(sessionAccessor));
            Check.ArgumentNotNull(metadataInterpreter, nameof(metadataInterpreter));
            Check.ArgumentNotNull(queryGeneratorFactory, nameof(queryGeneratorFactory));
            Check.ArgumentNotNull(contextFactory, nameof(contextFactory));
            Check.ArgumentNotNull(normalizeParameterNameResolver, nameof(normalizeParameterNameResolver));
            Check.ArgumentNotNull(dbScope, nameof(dbScope));
            Check.ArgumentNotNull(jsonMetadataConverter, nameof(jsonMetadataConverter));
            Check.ArgumentNotNull(errorManager, nameof(errorManager));
            Check.ArgumentNotNull(cardCache, nameof(cardCache));

            this.repository = repository;
            this.sessionAccessor = sessionAccessor;
            this.metadataInterpreter = metadataInterpreter;
            this.queryGeneratorFactory = queryGeneratorFactory;
            this.contextFactory = contextFactory;
            this.normalizeParameterNameResolver = normalizeParameterNameResolver;
            this.dbScope = dbScope;
            this.jsonMetadataConverter = jsonMetadataConverter;
            this.errorManager = errorManager;
            this.cardCache = cardCache;

            mediator.RegisterCallback(this.Reset);
        }

        /// <summary>
        /// Возвращает список представлений <see cref="ITessaView" />.
        /// </summary>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        public async ValueTask<IReadOnlyList<ITessaView>> GetAllViewsAsync(CancellationToken cancellationToken = default) =>
            await this.GetAllViewsCoreAsync(cancellationToken);

        /// <summary>
        /// Возвращает представление заданное по имени <paramref name="viewName"/>
        /// </summary>
        /// <param name="viewName">
        /// Имя представления
        /// </param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>
        /// Найденное представление или null
        /// </returns>
        public async ValueTask<ITessaView> GetByNameAsync(string viewName, CancellationToken cancellationToken = default)
        {
            foreach (ITessaView view in await this.GetAllViewsCoreAsync(cancellationToken))
            {
                IViewMetadata metadata = await view.GetMetadataAsync(cancellationToken);
                if (ParserNames.IsEquals(metadata.Alias, viewName))
                {
                    return view;
                }
            }

            return null;
        }

        /// <summary>
        /// Возвращает список представлений заданных именами в списке <paramref name="viewsNames"/>
        /// </summary>
        /// <param name="viewsNames">
        /// Список имен
        /// </param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>
        /// Список представлений
        /// </returns>
        public async ValueTask<IReadOnlyList<ITessaView>> GetByNamesAsync(IEnumerable<string> viewsNames, CancellationToken cancellationToken = default)
        {
            string[] viewsNamesArray = viewsNames.ToArray();
            if (viewsNamesArray.Length == 0)
            {
                return Array.Empty<ITessaView>();
            }

            var result = new List<ITessaView>();
            foreach (ITessaView view in await this.GetAllViewsCoreAsync(cancellationToken))
            {
                if (view is null)
                {
                    continue;
                }

                IViewMetadata metadata = await view.GetMetadataAsync(cancellationToken);
                string viewName = metadata.Alias;

                for (int i = 0; i < viewsNamesArray.Length; i++)
                {
                    if (ParserNames.IsEquals(viewName, viewsNamesArray[i]))
                    {
                        result.Add(view);
                        break;
                    }
                }
            }

            return result;
        }

        private async ValueTask EnsureAllViewsAsync(CancellationToken cancellationToken = default)
        {
            if (this.allViews is not null)
            {
                return;
            }

            List<ITessaView> allViews = await this.InitializeAsync(cancellationToken);
            if (this.allViews is not null)
            {
                return;
            }

            this.allViews = new ReadOnlyCollection<ITessaView>(allViews);
        }

        private async ValueTask<ReadOnlyCollection<ITessaView>> GetAllViewsCoreAsync(
            CancellationToken cancellationToken = default)
        {
            while (this.allViews is null)
            {
                await this.EnsureAllViewsAsync(cancellationToken);
            }

            return this.allViews;
        }

        /// <summary>
        ///     Инициализирует список представлений
        /// </summary>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>
        ///     Список представлений
        /// </returns>
        private async Task<List<ITessaView>> InitializeAsync(CancellationToken cancellationToken = default) =>
            (await this.repository.GetItemsAsync(new GetModelRequest(), cancellationToken))
            .Select(model =>
                new TessaViewModelAdapter(
                    Dbms.SqlServer,
                    model,
                    () => new TestQueryExecutor(),
                    this.sessionAccessor,
                    this.metadataInterpreter,
                    this.queryGeneratorFactory,
                    () => new ValidationResultBuilder(),
                    this.contextFactory,
                    this.dbScope,
                    this.normalizeParameterNameResolver, 
                    new DefaultViewGetDataExecutor(),
                    this.jsonMetadataConverter,
                    new TestDeputiesManagementSettingsProvider(),
                    this.errorManager,
                    this.cardCache
                    )
            )
            .Cast<ITessaView>()
            .ToList();

        /// <summary>
        ///     The reset.
        /// </summary>
        private void Reset() => this.allViews = null;
    }
}