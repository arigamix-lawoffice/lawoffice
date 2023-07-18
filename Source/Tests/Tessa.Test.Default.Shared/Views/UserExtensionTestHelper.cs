using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Platform;
using Tessa.Views;
using Tessa.Views.Metadata;
using Tessa.Views.SearchQueries;
using Tessa.Views.Workplaces;

namespace Tessa.Test.Default.Shared.Views
{
    /// <summary>
    ///     Помощник для тестирование пользовательских расширений
    /// </summary>
    public static class UserExtensionTestHelper
    {
        #region Constants

        /// <summary>
        ///     The test view name.
        /// </summary>
        private const string TestViewName = "TestView";

        #endregion

        #region Static Fields

        /// <summary>
        ///     The first test search query id.
        /// </summary>
        private static readonly Guid firstTestSearchQueryId = new Guid(0x0A9769F8, 0xE718, 0x4012, 0xBD, 0x4F, 0x0C, 0x35, 0x2A, 0x6E, 0xCA, 0x53); // 0A9769F8-E718-4012-BD4F-0C352A6ECA53

        /// <summary>
        ///     The second test search query id.
        /// </summary>
        private static readonly Guid secondTestSearchQueryId = new Guid(0x235CE789, 0x6A66, 0x4822, 0xAD, 0xAB, 0xC6, 0x47, 0x8F, 0x4D, 0x8A, 0xA4); // 235CE789-6A66-4822-ADAB-C6478F4D8AA4

        /// <summary>
        /// The test search query service.
        /// </summary>
        private static readonly ISearchQueryService testSearchQueryService = new TestSearchQueryService();

        /// <summary>
        ///     The test user id.
        /// </summary>
        private static readonly Guid testUserId = new Guid(0x915E9279, 0x99C6, 0x4A48, 0xB4, 0xB9, 0xCF, 0x17, 0x08, 0x16, 0x32, 0x30); // 915E9279-99C6-4A48-B4B9-CF1708163230

        /// <summary>
        /// The test view service.
        /// </summary>
        private static readonly IViewService testViewService = new TestViewService();

        /// <summary>
        ///     The test workplace id.
        /// </summary>
        private static readonly Guid testWorkplaceId = new Guid(0xC9EB9065, 0xC061, 0x4A03, 0xBE, 0x3B, 0x1E, 0xD0, 0xFA, 0x20, 0x9F, 0x48); // C9EB9065-C061-4A03-BE3B-1ED0FA209F48

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Возвращает тестовые метаданные пользовательских расширений
        /// </summary>
        /// <returns>
        ///     Метаданные пользовательских расширений
        /// </returns>
        public static async Task<IWorkplaceUserExtensionMetadata> GetTestMetadataAsync()
        {
            var extension = new WorkplaceUserExtensionMetadata
            {
                CompositionId = Guid.NewGuid(),
                OwnerId = GetUserId(),
                Alias = "Тестовый пользователь"
            };
            var folder = new FolderNodeMetadata
            {
                Alias = "Узел добавленный в корень",
                CompositionId = Guid.NewGuid(),
                ParentCompositionId = GetTestWorkplaceId(),
                OrderPos = OrderPosProvider.GetNextOrderPos(),
                ShowMode = ShowMode.Always
            };

            var childFolder = new FolderNodeMetadata
            {
                Alias = "Дочерний узел",
                CompositionId = Guid.NewGuid(),
                ParentCompositionId = folder.CompositionId,
                OrderPos = OrderPosProvider.GetNextOrderPos(),
                ShowMode = ShowMode.Always
            };

            var firstSearchQuery = new WorkplaceSearchQueryMetadata
            {
                Alias = "Поисковый запрос 1",
                CompositionId = Guid.NewGuid(),
                Caption = "Поисковый запрос 1",
                OrderPos = OrderPosProvider.GetNextOrderPos(),
                ParentCompositionId = childFolder.CompositionId,
                OwnerId = GetUserId(),
                SearchQueryId = firstTestSearchQueryId,
                ShowMode = ShowMode.Always,
                Metadata =
                    await GetTestSearchQueryService()
                        .GetByIdAsync(firstTestSearchQueryId)
            };
            childFolder.AddMetadata(firstSearchQuery);

            folder.AddMetadata(childFolder);

            var otherRootFolder = new FolderNodeMetadata
            {
                Alias = "Другой узел добавленный в корень",
                CompositionId = Guid.NewGuid(),
                ParentCompositionId = GetTestWorkplaceId(),
                OrderPos = OrderPosProvider.GetNextOrderPos(),
                ShowMode = ShowMode.Always
            };

            var secondSearchQuery = new WorkplaceSearchQueryMetadata
            {
                Alias = "Поисковый запрос 2",
                CompositionId = Guid.NewGuid(),
                Caption = "Поисковый запрос 2",
                OrderPos = OrderPosProvider.GetNextOrderPos(),
                ParentCompositionId =
                    otherRootFolder.CompositionId,
                OwnerId = GetUserId(),
                SearchQueryId = secondTestSearchQueryId,
                ShowMode = ShowMode.Always,
                Metadata =
                    await GetTestSearchQueryService()
                        .GetByIdAsync(secondTestSearchQueryId)
            };

            otherRootFolder.AddMetadata(secondSearchQuery);
            extension.AddMetadata(folder);
            extension.AddMetadata(otherRootFolder);
            return extension;
        }
        
        /// <summary>
        /// Возвращает тестовый сервис поисковых запросов
        /// </summary>
        /// <returns>
        /// The <see cref="ISearchQueryService"/>.
        /// </returns>
        public static ISearchQueryService GetTestSearchQueryService() => testSearchQueryService;

        /// <summary>
        /// Возвращает тестовый сервис представлений
        /// </summary>
        /// <returns>
        /// The <see cref="IViewService"/>.
        /// </returns>
        public static IViewService GetTestViewService() => testViewService;

        /// <summary>
        ///     Возвращает идентификатор тестового рабочего места
        /// </summary>
        /// <returns>Идентификатор тестового рабочего места</returns>
        public static Guid GetTestWorkplaceId() => testWorkplaceId;

        /// <summary>
        /// Возвращает текстовое представление тестовых метаданных загруженных из файла расположенного в встроенных ресурсах указанной сборки.
        /// </summary>
        /// <param name="assembly">Сборка, содержащая ресурсы.</param>
        /// <returns>Текстовое представление метаданных.</returns>
        public static string GetWorkplaceUserExtensionText(Assembly assembly) => AssemblyHelper.GetResourceTextFile(assembly, @"Resources/Views/TestUserExtensions.txt");

        
        /// <summary>
        ///     Возвращает идентификатор тестового пользователя
        /// </summary>
        /// <returns>Идентификатор тестового пользователя</returns>
        public static Guid GetUserId() => testUserId;

        #endregion

        /// <summary>
        ///     Поставщик порядковых номеров
        /// </summary>
        private static class OrderPosProvider
        {
            #region Static Fields

            /// <summary>
            ///     The order pos.
            /// </summary>
            private static int orderPos;

            #endregion

            #region Public Methods and Operators

            /// <summary>
            ///     Возвращает следующий порядковый номер
            /// </summary>
            /// <returns>Порядковый номер</returns>
            public static int GetNextOrderPos()
            {
                return ++orderPos;
            }

            #endregion
        }

        /// <summary>
        ///     The test search query service.
        /// </summary>
        private sealed class TestSearchQueryService : ISearchQueryService
        {
            #region Fields

            /// <summary>
            ///     The search queries.
            /// </summary>
            private readonly Dictionary<Guid, ISearchQueryMetadata> searchQueries;

            #endregion

            #region Constructors and Destructors

            /// <summary>
            ///     Initializes a new instance of the <see cref="TestSearchQueryService" /> class.
            ///     Инициализирует новый экземпляр класса <see cref="T:System.Object" />.
            /// </summary>
            public TestSearchQueryService()
            {
                this.searchQueries = new Dictionary<Guid, ISearchQueryMetadata>();
                var firstSearchQuery = new SearchQueryMetadata
                {
                    Alias = "Тестовый сохранённый запрос номер 1",
                    ID = firstTestSearchQueryId,
                    IsPublic = true,
                    CreatedByUserID = Guid.Empty,
                    ModificationDateTime = DateTime.Now,
                    ViewAlias = TestViewName
                };
                this.searchQueries[firstSearchQuery.ID] = firstSearchQuery;

                var secondSearchQuery = new SearchQueryMetadata
                {
                    Alias = "Тестовый сохранённый запрос номер 2",
                    ID = secondTestSearchQueryId,
                    IsPublic = false,
                    CreatedByUserID = GetUserId(),
                    ModificationDateTime = DateTime.Now,
                    ViewAlias = TestViewName
                };
                this.searchQueries[firstSearchQuery.ID] = secondSearchQuery;
            }

            #endregion

            #region Public Methods and Operators

            /// <summary>
            ///     Осуществляет сброс внутреннего кэша
            /// </summary>
            public Task ClearCacheAsync(CancellationToken cancellationToken = default) =>
                throw new NotSupportedException();

            /// <summary>
            /// Осуществляет удаление поискового запроса
            /// </summary>
            /// <param name="queries">
            /// Идентификаторы удаляемых поисковых запросов
            /// </param>
            /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
            /// <returns>Асинхронная задача.</returns>
            public Task DeleteAsync(IReadOnlyList<Guid> queries, CancellationToken cancellationToken = default) =>
                throw new NotSupportedException();

            /// <summary>
            /// Возвращает поисковый запрос по его идентификатору
            /// </summary>
            /// <param name="id">
            /// Идентификатор поискового запроса
            /// </param>
            /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
            /// <returns>
            /// Метаданные поискового запроса или null, если запрос не найден или не доступен пользователю
            /// </returns>
            public Task<ISearchQueryMetadata> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
                this.searchQueries.TryGetValue(id, out ISearchQueryMetadata searchQuery)
                    ? Task.FromResult(searchQuery)
                    : Task.FromResult((ISearchQueryMetadata)null);

            /// <summary>
            ///     Возвращает метаданные только общедоступных поисковых запросов
            /// </summary>
            /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
            /// <returns>
            ///     Список запросов
            /// </returns>
            public Task<IReadOnlyList<ISearchQueryMetadata>> GetPublicAsync(CancellationToken cancellationToken = default) =>
                Task.FromResult((IReadOnlyList<ISearchQueryMetadata>) this.searchQueries.Values.Where(x => x.IsPublic).ToArray());

            /// <summary>
            ///     Возвращает список доступных пользователю поисковых запросов
            /// </summary>
            /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
            /// <returns>Список запросов</returns>
            public Task<IReadOnlyList<ISearchQueryMetadata>> GetUserAvailableAsync(CancellationToken cancellationToken = default) =>
                Task.FromResult((IReadOnlyList<ISearchQueryMetadata>) this.searchQueries.Values.ToArray());

            /// <summary>
            /// Осуществляет импорт поисковых запросов
            /// </summary>
            /// <param name="searchQueries">
            /// Список импортируемых поисковых запросов
            /// </param>
            /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
            /// <returns>Асинхронная задача.</returns>
            public Task ImportAsync(IReadOnlyList<ISearchQueryMetadata> searchQueries, CancellationToken cancellationToken = default) =>
                throw new NotSupportedException();

            /// <summary>
            /// Осуществляет сохранение поискового запроса метаданные поискового запроса
            /// </summary>
            /// <param name="metadata">
            /// Метаданные
            /// </param>
            /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
            /// <returns>Асинхронная задача.</returns>
            public Task SaveAsync(ISearchQueryMetadata metadata, CancellationToken cancellationToken = default) =>
                throw new NotSupportedException();

            #endregion
        }

        /// <summary>
        ///     The test view service.
        /// </summary>
        private sealed class TestViewService : IViewService
        {
            #region Fields

            /// <summary>
            ///     The views.
            /// </summary>
            private readonly Dictionary<string, ITessaView> views;

            #endregion

            #region Constructors and Destructors

            /// <summary>
            ///     Initializes a new instance of the <see cref="TestViewService" /> class.
            ///     Инициализирует новый экземпляр класса <see cref="T:System.Object" />.
            /// </summary>
            public TestViewService()
            {
                this.views = new Dictionary<string, ITessaView>(StringComparer.OrdinalIgnoreCase);
                var view = new TestView();
                this.views[view.Metadata.Alias] = view;
            }

            #endregion

            #region Public Methods and Operators

            /// <summary>
            /// Возвращает список представлений <see cref="ITessaView" />.
            /// </summary>
            /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
            /// <returns>Асинхронная задача.</returns>
            public ValueTask<IReadOnlyList<ITessaView>> GetAllViewsAsync(CancellationToken cancellationToken = default) =>
                new ValueTask<IReadOnlyList<ITessaView>>(this.views.Values.ToArray());

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
            public ValueTask<ITessaView> GetByNameAsync(string viewName, CancellationToken cancellationToken = default) =>
                new ValueTask<ITessaView>(this.views.TryGetValue(viewName, out ITessaView view) ? view : null);

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
            public ValueTask<IReadOnlyList<ITessaView>> GetByNamesAsync(IEnumerable<string> viewsNames, CancellationToken cancellationToken = default)
            {
                var result = new List<ITessaView>();
                foreach (string name in viewsNames)
                {
                    if (this.views.TryGetValue(name, out ITessaView view))
                    {
                        result.Add(view);
                    }
                }

                return new ValueTask<IReadOnlyList<ITessaView>>(result);
            }

            /// <inheritdoc/>
            public ValueTask<IReadOnlyList<ITessaView>> GetByReferencesAsync(IEnumerable<string> refSection, CancellationToken cancellationToken = default) =>
                throw new NotSupportedException();

            #endregion

            /// <summary>
            ///     The test view.
            /// </summary>
            private sealed class TestView : ITessaView
            {
                #region Constructors and Destructors

                /// <summary>
                ///     Initializes a new instance of the <see cref="TestView" /> class.
                ///     Инициализирует новый экземпляр класса <see cref="T:System.Object" />.
                /// </summary>
                public TestView() =>
                    this.Metadata = new ViewMetadata { Alias = TestViewName, Caption = "Test view" };

                #endregion

                #region Properties

                public IViewMetadata Metadata { get; }

                #endregion

                #region Public Methods and Operators

                /// <inheritdoc />
                public ValueTask<IViewMetadata> GetMetadataAsync(CancellationToken cancellationToken = default) =>
                    new ValueTask<IViewMetadata>(this.Metadata);

                /// <summary>
                /// Выполняет получение данных из представления
                ///     на основании полученного <see cref="ITessaViewRequest">запроса</see>
                /// </summary>
                /// <param name="request">
                /// Запрос к представлению
                /// </param>
                /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
                /// <returns>
                /// <see cref="ITessaViewResult">Результат</see> выполнения запроса
                /// </returns>
                public Task<ITessaViewResult> GetDataAsync(ITessaViewRequest request, CancellationToken cancellationToken = default)
                {
                    return Task.FromResult((ITessaViewResult)null);
                }

                #endregion
            }
        }
    }
}