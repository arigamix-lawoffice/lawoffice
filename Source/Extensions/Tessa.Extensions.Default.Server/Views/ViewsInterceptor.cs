using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Platform.Collections;
using Tessa.Platform.Data;
using Tessa.Scheme;
using Tessa.Views;
using Tessa.Views.Metadata;
using Tessa.Views.Metadata.Criteria;
using Tessa.Views.Metadata.Types;
using Tessa.Views.Parser;

namespace Tessa.Extensions.Default.Server.Views
{
    /// <summary>
    ///     The views interceptor.
    /// </summary>
    public sealed class ViewsInterceptor
        : ViewInterceptorBase
    {
        #region Constants

        private const string ViewFiles = "ViewFiles";

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewsInterceptor"/> class.
        /// Инициализирует новый экземпляр класса <see cref="ViewsInterceptor"/>.
        /// </summary>
        /// <param name="schemeTypeConverter">
        /// Конвертер типов данных
        /// </param>
        /// <param name="dbScope">
        /// Объект для взаимодействия с базой данных
        /// </param>
        /// <param name="viewServiceFactory">
        /// Фабрика создания сервиса представлений
        /// </param>
        public ViewsInterceptor(
            ISchemeTypeConverter schemeTypeConverter,
            IDbScope dbScope,
            Func<IViewService> viewServiceFactory)
            : base(new[] { ViewFiles })
        {
            this.schemeTypeConverter = schemeTypeConverter ?? throw new ArgumentNullException(nameof(schemeTypeConverter));
            this.dbScope = dbScope ?? throw new ArgumentNullException(nameof(dbScope));
            this.viewServiceFactory = viewServiceFactory ?? throw new ArgumentNullException(nameof(viewServiceFactory));
        }

        #endregion

        #region Private Fields

        private ViewFilesInterceptor viewFiles;

        private readonly ISchemeTypeConverter schemeTypeConverter;

        private readonly IDbScope dbScope;

        private readonly Func<IViewService> viewServiceFactory;

        #endregion

        #region Private Methods

        private async Task<ViewFilesInterceptor> CreateViewAsync(CancellationToken cancellationToken = default)
        {
            ITessaView view = await this.viewServiceFactory().GetByNameAsync(ViewFiles, cancellationToken);
            IViewMetadata metadata = view is null ? null : await view.GetMetadataAsync(cancellationToken);

            return new ViewFilesInterceptor(this.schemeTypeConverter, this.dbScope, metadata);
        }

        #endregion

        #region Public Methods

        /// <inheritdoc />
        public override async Task<ITessaViewResult> GetDataAsync(ITessaViewRequest request, CancellationToken cancellationToken = default)
        {
            if (!ParserNames.IsEquals(request.View?.Alias, ViewFiles))
            {
                throw new InvalidOperationException("Unknown view");
            }

            this.viewFiles ??= await this.CreateViewAsync(cancellationToken);
            return await this.viewFiles.GetDataAsync(request, cancellationToken);
        }

        #endregion

        #region Nested Types

        /// <summary>
        ///     The view files interceptor.
        /// </summary>
        private sealed class ViewFilesInterceptor : ITessaView
        {
            #region Constants

            /// <summary>
            ///     The by folders.
            /// </summary>
            private const string ByFolders = "ByFolder";

            /// <summary>
            ///     The default directory.
            /// </summary>
            private const string DefaultDirectory = @"c:\temp\1\";

            /// <summary>
            ///     The default filter.
            /// </summary>
            private const string DefaultFilter = "*.*";

            /// <summary>
            ///     The folder.
            /// </summary>
            private const string Folder = "Folder";

            /// <summary>
            ///     The name parameter.
            /// </summary>
            private const string NameParameter = "NAME";

            /// <summary>
            ///     The parent folder.
            /// </summary>
            private const string ParentFolder = "ParentFolder";

            #endregion

            #region Fields

            private readonly ISchemeTypeConverter schemeTypeConverter;

            private readonly IDbScope dbScope;

            private readonly IViewMetadata metadata;

            #endregion

            #region Constructors and Destructors

            /// <summary>
            /// Initializes a new instance of the <see cref="ViewFilesInterceptor"/> class.
            ///     Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
            /// </summary>
            /// <param name="schemeTypeConverter">
            /// Конвертер типов данных
            /// </param>
            /// <param name="dbScope">
            /// Объект для взаимодействия с базой данных
            /// </param>
            /// <param name="metadata">
            /// Метаинформация представления
            /// </param>
            public ViewFilesInterceptor(
                ISchemeTypeConverter schemeTypeConverter,
                IDbScope dbScope,
                IViewMetadata metadata)
            {
                this.schemeTypeConverter = schemeTypeConverter;
                this.dbScope = dbScope;
                this.metadata = metadata;
            }

            #endregion

            #region Public Methods and Operators

            /// <inheritdoc />
            public ValueTask<IViewMetadata> GetMetadataAsync(CancellationToken cancellationToken = default) =>
                new ValueTask<IViewMetadata>(this.metadata);

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
            public async Task<ITessaViewResult> GetDataAsync(ITessaViewRequest request, CancellationToken cancellationToken = default)
            {
                var result = await this.GetParentFolderItemsAsync(request, cancellationToken);
                if (result != null)
                {
                    return result;
                }

                result = await this.GetFolderItemsAsync(request, cancellationToken);
                return result ?? await this.GetDefaultFolderResultAsync(request, cancellationToken);
            }

            #endregion

            #region Methods

            /// <summary>
            /// Преобразует список файлов в результат выполнения представления
            /// </summary>
            /// <param name="files">
            /// Список файлов
            /// </param>
            /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
            /// <returns>
            /// Результат выполнения представления
            /// </returns>
            private async ValueTask<ITessaViewResult> GetResultAsync(IEnumerable<string> files, CancellationToken cancellationToken = default)
            {
                Dbms dbms = await this.dbScope.GetDbmsAsync(cancellationToken);
                string sqlStringTypeName = this.schemeTypeConverter.TryGetSqlTypeName(SchemeType.String, dbms);

                return new TessaViewResult
                {
                    Columns = new List<object> { "FileName", "FullFileName" },
                    DataTypes = new List<object> { sqlStringTypeName, sqlStringTypeName },
                    SchemeTypes = new[] { SchemeType.String, SchemeType.String },
                    Rows = (from file in files
                            select (object) new List<object> { Path.GetFileName(file), file })
                        .ToList()
                };
            }

            /// <summary>
            /// Возвращает список файлов в папке по умолчанию в соответствии с заданным фильтром
            ///     в параметре <see cref="NameParameter"/>
            /// </summary>
            /// <param name="request">
            /// The request.
            /// </param>
            /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
            /// <returns>
            /// Результат выполнения запроса
            /// </returns>
            private ValueTask<ITessaViewResult> GetDefaultFolderResultAsync(ITessaViewRequest request, CancellationToken cancellationToken = default)
            {
                var filter = GetDirectoryFilter(request);
                var files = GetItems(DefaultDirectory, filter, false);
                return this.GetResultAsync(files, cancellationToken);
            }

            /// <summary>
            /// Возвращает шаблон отбора имен файлов.
            ///     Если в запросе задано одно значение в параметре с именем 'Name'
            ///     не зависимо от заданного условия параметра будет возвращен фильтр
            ///     указанный в данном параметре.
            ///     Если параметр не задан то возвращает фильтр по умолчанию *.*
            /// </summary>
            /// <param name="request">
            /// Запрос к представлению
            /// </param>
            /// <returns>
            /// Шаблон отбора имен файлов
            /// </returns>
            private static string GetDirectoryFilter(ITessaViewRequest request)
            {
                var requestValues = request?.Values;
                if (requestValues is null)
                {
                    return DefaultFilter;
                }

                var nameParameter = requestValues.FirstOrDefault(p => ParserNames.IsEquals(NameParameter, p.Name));
                if (nameParameter == null || !nameParameter.CriteriaValues.Any())
                {
                    return DefaultFilter;
                }

                var criteria = nameParameter.CriteriaValues.FirstOrDefault();
                if (criteria == null || criteria.Values == null || !criteria.Values.Any())
                {
                    return DefaultFilter;
                }

                var value = criteria.Values.FirstOrDefault();
                var isContains = ParserNames.IsEquals(criteria.CriteriaName, CriteriaOperatorConst.Contains);
                return value.IsNull() ? DefaultFilter : isContains ? value + DefaultFilter : value.ToString();
            }

            /// <summary>
            /// Обрабатывает наличие параметра <see cref="Folder"/> на получение списка файлов
            ///     указанной папки
            /// </summary>
            /// <param name="request">
            /// Запрос к представлению
            /// </param>
            /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
            /// <returns>
            /// Результат выполнения запроса
            /// </returns>
            private async ValueTask<ITessaViewResult> GetFolderItemsAsync(ITessaViewRequest request, CancellationToken cancellationToken = default)
            {
                ITessaViewResult result = null;
                if (request.IsDefinedParameter(Folder))
                {
                    await request.IterateParameterCriteriasAsync(
                        Folder,
                        async (criteria, ct) =>
                        {
                            if (criteria.CriteriaName == new EqualsCriteriaOperator().Name)
                            {
                                var files = GetItems(
                                    criteria.Values.FirstOrDefault().Value.ToString(),
                                    GetDirectoryFilter(request),
                                    false);
                                result = await this.GetResultAsync(files, ct);
                            }
                        },
                        cancellationToken);
                }

                return result;
            }

            /// <summary>
            /// Возвращает список файлов из каталога <paramref name="path"/>
            ///     в соответствии с фильтром <paramref name="filter"/>.
            ///     Если каталог не существует будет возвращен пустой список
            /// </summary>
            /// <param name="path">
            /// Путь к папке с файлами
            /// </param>
            /// <param name="filter">
            /// Шаблон отбора имен файлов
            /// </param>
            /// <param name="getFolders">
            /// The get Folders.
            /// </param>
            /// <returns>
            /// Список файлов
            /// </returns>
            private static IEnumerable<string> GetItems(string path, string filter, bool getFolders)
            {
                if (!Directory.Exists(path))
                {
                    // в .NET Core 3+ штука, возвращаемая методом Enumerable.Empty, не помечена, как [Serializable]; поэтому возвращаем здесь массив
                    return Array.Empty<string>();
                }

                return getFolders ? Directory.GetDirectories(path, filter) : Directory.GetFiles(path, filter);
            }

            /// <summary>
            /// Обрабатывает наличие параметра <see cref="ParentFolder"/> на получение списка папок
            ///     указанной папки
            /// </summary>
            /// <param name="request">
            /// Запрос к представлению
            /// </param>
            /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
            /// <returns>
            /// Результат выполнения запроса
            /// </returns>
            private async ValueTask<ITessaViewResult> GetParentFolderItemsAsync(ITessaViewRequest request, CancellationToken cancellationToken = default)
            {
                ITessaViewResult result = null;
                if (request.IsSubsetDefined(ByFolders))
                {
                    if (request.IsDefinedParameter(ParentFolder))
                    {
                        await request.IterateParameterCriteriasAsync(
                            ParentFolder,
                            async (criteria, ct) =>
                            {
                                if (criteria.CriteriaName == new IsNullCriteriaOperator().Name)
                                {
                                    Dbms dbms = await this.dbScope.GetDbmsAsync(ct);
                                    string sqlStringTypeName = this.schemeTypeConverter.TryGetSqlTypeName(SchemeType.String, dbms);

                                    var columns = new List<object>
                                    {
                                        "FileName",
                                        "FullFileName"
                                    };
                                    var dataTypes = new List<object> { sqlStringTypeName, sqlStringTypeName };
                                    var schemeTypes = new[] { SchemeType.String, SchemeType.String };
                                    var rows = new List<object>
                                    {
                                        new List<object>
                                        {
                                            "Root",
                                            DefaultDirectory
                                        }
                                    };
                                    result = new TessaViewResult
                                    {
                                        Columns = columns,
                                        DataTypes = dataTypes,
                                        SchemeTypes = schemeTypes,
                                        Rows = rows
                                    };
                                }

                                if (criteria.CriteriaName == new EqualsCriteriaOperator().Name)
                                {
                                    var folders = GetItems(
                                        criteria.Values.FirstOrDefault().Value.ToString(),
                                        GetDirectoryFilter(request),
                                        true);

                                    result = await this.GetResultAsync(folders, ct);
                                }
                            },
                            cancellationToken);
                    }
                }

                return result;
            }

            #endregion
        }

        #endregion
    }
}