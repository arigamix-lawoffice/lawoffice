using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Metadata;
using Tessa.Cards.SmartMerge;
using Tessa.Platform;
using Tessa.Platform.Data;
using Tessa.Platform.Operations;
using Tessa.Platform.Validation;
using Tessa.Test.Default.Shared.Cards;
using Tessa.Test.Default.Shared.Kr;
using Tessa.Test.Default.Shared.Views;
using Tessa.Views;
using Tessa.Views.Json;
using Tessa.Views.Json.Converters;
using Tessa.Views.Parser;
using Tessa.Views.Parser.SyntaxTree.ExchangeFormat;

namespace Tessa.Test.Default.Shared
{
    /// <summary>
    /// Предоставляет методы для настройки тестовой базы данных.
    /// </summary>
    public sealed class TestConfigurationBuilder :
        PendingActionsProvider<IPendingAction, TestConfigurationBuilder>
    {
        #region Fields

        private readonly ICardTypeServerRepository cardTypeServerRepository;

        private readonly ICardTypeClientRepository cardTypeClientRepository;

        private readonly CardMetadataCache cardMetadataCache;

        private readonly ICardCachedMetadata cardCachedMetadata;

        private readonly IExchangeFormatInterpreter exchangeFormatInterpreter;

        private readonly IIndentationStrategy indentationStrategy;

        private readonly IJsonViewModelImporter jsonViewModelImporter;

        private readonly IJsonViewModelConverter jsonViewModelAdapter;

        private readonly ITessaViewService tessaViewService;

        #endregion

        #region Properties

        /// <summary>
        /// Возвращает сборку содержащую встроенные ресурсы.
        /// </summary>
        public Assembly ResourceAssembly { get; }

        /// <summary>
        /// Возвращает объект для взаимодействия с базой данных.
        /// </summary>
        public IDbScope DbScope { get; }

        /// <summary>
        /// Возвращает объект управляющий операциями с карточками.
        /// </summary>
        public ICardManager CardManager { get; }

        /// <summary>
        /// Возвращает объект управляющий операциями с библиотеками и списками карточек.
        /// </summary>
        public ICardLibraryManager CardLibraryManager { get; }

        /// <summary>
        /// Возвращает репозиторий для управления карточками.
        /// </summary>
        public ICardRepository CardRepository { get; }

        /// <summary>
        /// Возвращает зависимости, используемые объектами, управляющими жизненным циклом карточек.
        /// </summary>
        public ICardLifecycleCompanionDependencies Dependencies { get; }

        /// <summary>
        /// Возвращает репозиторий, управляющий операциями.
        /// </summary>
        public IOperationRepository OperationRepository { get; }

        /// <summary>
        /// Возвращает потокобезопасный кэш настроек по всем местоположениям файлов.
        /// </summary>
        public ICardFileSourceSettings CardFileSourceSettings { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр объекта <see cref="TestConfigurationBuilder"/> для использования в серверных тестах.
        /// </summary>
        /// <param name="resourceAssembly">Сборка содержащая встроенные ресурсы.</param>
        /// <param name="dbScope">Объект для взаимодействия с базой данных.</param>
        /// <param name="cardManager">Объект, управляющий операциями с карточками.</param>
        /// <param name="cardLibraryManager">Объект, управляющий операциями с библиотеками и списками карточек.</param>
        /// <param name="cardRepository">Репозиторий для управления карточками.</param>
        /// <param name="cardFileSourceSettings">Потокобезопасный кэш настроек по всем местоположениям файлов.</param>
        /// <param name="deps">Зависимости, используемые объектами, управляющими жизненным циклом карточек.</param>
        /// <param name="operationRepository">Репозиторий, управляющий операциями.</param>
        /// <param name="cardTypeServerRepository">Репозиторий для управления типами карточек на сервере.</param>
        /// <param name="cardMetadataCache">Потокобезопасный кэш типов карточек.</param>
        /// <param name="exchangeFormatInterpreter">Интерпретатор текста формата обмена.</param>
        /// <param name="indentationStrategy">Стратегия выравнивания текста.</param>
        /// <param name="tessaViewService">Сервис представлений.</param>
        /// <param name="jsonViewModelImporter">Объект для импорта представлений.</param>
        /// <param name="jsonViewModelAdapter">Адаптер представлений.</param>
        public TestConfigurationBuilder(
            Assembly resourceAssembly,
            IDbScope dbScope,
            ICardManager cardManager,
            ICardLibraryManager cardLibraryManager,
            ICardRepository cardRepository,
            ICardFileSourceSettings cardFileSourceSettings,
            ICardLifecycleCompanionDependencies deps,
            IOperationRepository operationRepository,
            ICardTypeServerRepository cardTypeServerRepository,
            CardMetadataCache cardMetadataCache,
            IExchangeFormatInterpreter exchangeFormatInterpreter,
            IIndentationStrategy indentationStrategy,
            ITessaViewService tessaViewService,
            IJsonViewModelImporter jsonViewModelImporter,
            IJsonViewModelConverter jsonViewModelAdapter)
        {
            Check.ArgumentNotNull(resourceAssembly, nameof(resourceAssembly));
            Check.ArgumentNotNull(dbScope, nameof(dbScope));
            Check.ArgumentNotNull(cardManager, nameof(cardManager));
            Check.ArgumentNotNull(cardLibraryManager, nameof(cardLibraryManager));
            Check.ArgumentNotNull(cardRepository, nameof(cardRepository));
            Check.ArgumentNotNull(deps, nameof(deps));
            Check.ArgumentNotNull(operationRepository, nameof(operationRepository));
            Check.ArgumentNotNull(cardTypeServerRepository, nameof(cardTypeServerRepository));
            Check.ArgumentNotNull(cardMetadataCache, nameof(cardMetadataCache));
            Check.ArgumentNotNull(exchangeFormatInterpreter, nameof(exchangeFormatInterpreter));
            Check.ArgumentNotNull(indentationStrategy, nameof(indentationStrategy));
            Check.ArgumentNotNull(tessaViewService, nameof(tessaViewService));
            Check.ArgumentNotNull(jsonViewModelImporter, nameof(jsonViewModelImporter));
            Check.ArgumentNotNull(jsonViewModelAdapter, nameof(jsonViewModelAdapter));

            this.ResourceAssembly = resourceAssembly;
            this.DbScope = dbScope;
            this.CardManager = cardManager;
            this.CardLibraryManager = cardLibraryManager;
            this.CardRepository = cardRepository;
            this.CardFileSourceSettings = cardFileSourceSettings;
            this.Dependencies = deps;
            this.OperationRepository = operationRepository;
            this.cardTypeServerRepository = cardTypeServerRepository;
            this.cardTypeClientRepository = default;
            this.cardMetadataCache = cardMetadataCache;
            this.cardCachedMetadata = default;
            this.exchangeFormatInterpreter = exchangeFormatInterpreter;
            this.indentationStrategy = indentationStrategy;
            this.tessaViewService = tessaViewService;
            this.jsonViewModelImporter = jsonViewModelImporter;
            this.jsonViewModelAdapter = jsonViewModelAdapter;
        }

        /// <summary>
        /// Инициализирует новый экземпляр объекта <see cref="TestConfigurationBuilder"/> для использования в клиентских тестах.
        /// </summary>
        /// <param name="resourceAssembly">Сборка содержащая встроенные ресурсы.</param>
        /// <param name="dbScope">Объект для взаимодействия с базой данных.</param>
        /// <param name="cardManager">Объект, управляющий операциями с карточками.</param>
        /// <param name="cardLibraryManager">Объект, управляющий операциями с библиотеками и списками карточек.</param>
        /// <param name="cardRepository">Репозиторий для управления карточками.</param>
        /// <param name="cardFileSourceSettings">Потокобезопасный кэш настроек по всем местоположениям файлов.</param>
        /// <param name="deps">Зависимости, используемые объектами, управляющими жизненным циклом карточек.</param>
        /// <param name="operationRepository">Репозиторий, управляющий операциями.</param>
        /// <param name="cardTypeClientRepository">Репозиторий для управления типами карточек на клиенте.</param>
        /// <param name="cardCachedMetadata">Метаинформация, необходимая для использования типов карточек совместно с пакетом карточек.</param>
        /// <param name="exchangeFormatInterpreter">Интерпретатор текста формата обмена.</param>
        /// <param name="indentationStrategy">Стратегия выравнивания текста.</param>
        /// <param name="tessaViewService">Сервис представлений.</param>
        /// <param name="jsonViewModelImporter">Объект для импорта представлений.</param>
        /// <param name="jsonViewModelAdapter">Адаптер представлений.</param>
        public TestConfigurationBuilder(
            Assembly resourceAssembly,
            IDbScope dbScope,
            ICardManager cardManager,
            ICardLibraryManager cardLibraryManager,
            ICardRepository cardRepository,
            ICardFileSourceSettings cardFileSourceSettings,
            ICardLifecycleCompanionDependencies deps,
            IOperationRepository operationRepository,
            ICardTypeClientRepository cardTypeClientRepository,
            ICardCachedMetadata cardCachedMetadata,
            IExchangeFormatInterpreter exchangeFormatInterpreter,
            IIndentationStrategy indentationStrategy,
            ITessaViewService tessaViewService,
            IJsonViewModelImporter jsonViewModelImporter,
            IJsonViewModelConverter jsonViewModelAdapter)
        {
            Check.ArgumentNotNull(resourceAssembly, nameof(resourceAssembly));
            Check.ArgumentNotNull(dbScope, nameof(dbScope));
            Check.ArgumentNotNull(cardManager, nameof(cardManager));
            Check.ArgumentNotNull(cardLibraryManager, nameof(cardLibraryManager));
            Check.ArgumentNotNull(cardRepository, nameof(cardRepository));
            Check.ArgumentNotNull(deps, nameof(deps));
            Check.ArgumentNotNull(operationRepository, nameof(operationRepository));
            Check.ArgumentNotNull(cardTypeClientRepository, nameof(cardTypeClientRepository));
            Check.ArgumentNotNull(cardCachedMetadata, nameof(cardCachedMetadata));
            Check.ArgumentNotNull(exchangeFormatInterpreter, nameof(exchangeFormatInterpreter));
            Check.ArgumentNotNull(indentationStrategy, nameof(indentationStrategy));
            Check.ArgumentNotNull(tessaViewService, nameof(tessaViewService));
            Check.ArgumentNotNull(jsonViewModelImporter, nameof(jsonViewModelImporter));
            Check.ArgumentNotNull(jsonViewModelAdapter, nameof(jsonViewModelAdapter));

            this.ResourceAssembly = resourceAssembly;
            this.DbScope = dbScope;
            this.CardManager = cardManager;
            this.CardLibraryManager = cardLibraryManager;
            this.CardRepository = cardRepository;
            this.CardFileSourceSettings = cardFileSourceSettings;
            this.Dependencies = deps;
            this.OperationRepository = operationRepository;
            this.cardTypeServerRepository = default;
            this.cardTypeClientRepository = cardTypeClientRepository;
            this.cardMetadataCache = default;
            this.cardCachedMetadata = cardCachedMetadata;
            this.exchangeFormatInterpreter = exchangeFormatInterpreter;
            this.indentationStrategy = indentationStrategy;
            this.tessaViewService = tessaViewService;
            this.jsonViewModelImporter = jsonViewModelImporter;
            this.jsonViewModelAdapter = jsonViewModelAdapter;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Настраивает часовые пояса.
        /// </summary>
        /// <returns>Объект <see cref="TestConfigurationBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public TestConfigurationBuilder ConfigureTimeZones()
        {
            this.AddPendingAction(
                new PendingAction(
                    nameof(TestConfigurationBuilder) + "." + nameof(TestConfigurationBuilder.ConfigureTimeZones),
                    this.ConfigureTimeZonesAsync));
            return this;
        }

        /// <summary>
        /// Выполняет построение календаря.
        /// </summary>
        /// <param name="startDate">Начальная дата, от которой отсчитывается диапазон построения календаря. Если не задана, то используется текущая дата.</param>
        /// <param name="dateEndOffset">Число календарных дней прибавляемых к начальной дате при вычислении правой границы диапазона расчёта календаря.</param>
        /// <param name="calendarID">Идентификатор создаваемого календаря или значение <see langword="null"/>, если используется календарь по умолчанию <see cref="CalendarTestsHelper.DefaultCalendarID"/>.</param>
        /// <param name="calendarIntID">Целочисленный идентификатор календаря.</param>
        /// <returns>Объект <see cref="TestConfigurationBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public TestConfigurationBuilder BuildCalendar(
            DateTime? startDate = default,
            double dateEndOffset = TestHelper.DefaultCalendarDateEndOffset,
            Guid? calendarID = null,
            int calendarIntID = 0)
        {
            this.AddPendingAction(
                new PendingAction(
                    nameof(TestConfigurationBuilder) + "." + nameof(TestConfigurationBuilder.BuildCalendar),
                    async (action, cancellationToken) =>
                    {
                        // Карточка способа расчёта должна была быть импортирована.
                        // Карточка типа календаря должна была быть импортирована.

                        // Если ID календаря не задан - будем работать с тем, что был импортирован.
                        if (!calendarID.HasValue)
                        {
                            calendarID = CalendarTestsHelper.DefaultCalendarID;

                            // Грузим карточку календаря
                            var clcCalendar =
                                new CardLifecycleCompanion(calendarID.Value, CardHelper.CalendarTypeID, CardHelper.CalendarTypeName, this.Dependencies);
                            clcCalendar
                                .Load()
                                .ApplyAction((companion, pendingAction) =>
                                {
                                    var dateStart = startDate ?? DateTime.UtcNow.Date.AddDays(-7.0).StartOfWeek(DayOfWeek.Monday);
                                    var dateEnd =
                                        startDate == null
                                            ? DateTime.UtcNow.Date + TimeSpan.FromDays(dateEndOffset)
                                            : startDate + TimeSpan.FromDays(dateEndOffset);

                                    var settingsSection = companion.Card.Sections["CalendarSettings"];
                                    settingsSection.Fields["CalendarStart"] = dateStart;
                                    settingsSection.Fields["CalendarEnd"] = dateEnd;
                                });
                            await clcCalendar.Save().GoAsync(cancellationToken: cancellationToken);
                        }
                        else
                        {
                            // Создадим карточку календаря
                            await new CardLifecycleCompanion(
                                    calendarID.Value,
                                    CardHelper.CalendarTypeID,
                                    CardHelper.CalendarTypeName,
                                    this.Dependencies)
                                .Create()
                                .ApplyAction((companion, pendingAction) =>
                                {
                                    var dateStart = startDate ?? DateTime.UtcNow.Date.AddDays(-7.0).StartOfWeek(DayOfWeek.Monday);
                                    var dateEnd =
                                        startDate.HasValue
                                            ? startDate + TimeSpan.FromDays(dateEndOffset)
                                            : DateTime.UtcNow.Date + TimeSpan.FromDays(dateEndOffset);

                                    var settingsSection = companion.Card.Sections["CalendarSettings"];
                                    settingsSection.Fields["CalendarID"] = Int32Boxes.Box(calendarIntID);
                                    settingsSection.Fields["Name"] = CalendarTestsHelper.DefaultCalendarName;

                                    settingsSection.Fields["CalendarStart"] = dateStart;
                                    settingsSection.Fields["CalendarEnd"] = dateEnd;

                                    settingsSection.Fields["CalendarTypeID"] =
                                        CalendarTestsHelper.DefaultWorkWeekCalendarTypeID;
                                    settingsSection.Fields["CalendarTypeCaption"] =
                                        CalendarTestsHelper.DefaultWorkWeekCalendarTypeCaption;
                                })
                                .Save()
                                .GoAsync(cancellationToken: cancellationToken);
                        }

                        var validationResultBuilder = new ValidationResultBuilder();

                        await TestHelper.BuildCalendarAsync(
                            this.OperationRepository,
                            this.CardRepository,
                            calendarID.Value,
                            validationResultBuilder,
                            cancellationToken);

                        return validationResultBuilder.Build();
                    }));

            return this;
        }

        /// <summary>
        /// Импортирует все карточки, из ресурсов текущей сборки, расположенные в заданной директории.
        /// </summary>
        /// <param name="getDirectoryFuncAsync">Функция возвращающая директорию, расположенную в <see cref="ResourcesPaths.Cards.Name"/>, из которой должны быть импортированы карточки.</param>
        /// <param name="cardPredicateAsync">Функция определяющая возможность импорта карточки или значение по умолчанию для типа, если фильтрация не выполняется.</param>
        /// <param name="getMergeOptionsFuncAsync">Функция возвращающая параметры объединения для файла с заданным именем или значение по умолчанию для типа, если используются параметры по умолчанию. Параметры: имя файла для которого запрашиваются параметры объединения.</param>
        /// <returns>Объект <see cref="TestConfigurationBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public TestConfigurationBuilder ImportCardsFromDirectory(
            Func<TestConfigurationBuilder, CancellationToken, ValueTask<string>> getDirectoryFuncAsync,
            Func<Card, CancellationToken, ValueTask<bool>> cardPredicateAsync = default,
            Func<string, CancellationToken, ValueTask<ICardMergeOptions>> getMergeOptionsFuncAsync = default)
        {
            Check.ArgumentNotNull(getDirectoryFuncAsync, nameof(getDirectoryFuncAsync));

            this.AddPendingAction(
                new PendingAction(
                    nameof(TestConfigurationBuilder) + "." + nameof(TestConfigurationBuilder.ImportCardsFromDirectory),
                    async (action, cancellationToken) =>
                    {
                        await TestCardHelper.ImportCardsFromDirectoryAsync(
                            this.ResourceAssembly,
                            this.CardManager,
                            this.CardRepository,
                            await getDirectoryFuncAsync(this, cancellationToken),
                            cardPredicateAsync: cardPredicateAsync,
                            getMergeOptionsFuncAsync: getMergeOptionsFuncAsync,
                            cancellationToken: cancellationToken);

                        return ValidationResult.Empty;
                    }));
            return this;
        }

        /// <summary>
        /// Импортирует все карточки из ресурсов указанной сборки, описанные в файле библиотеки карточек (*.cardlib).
        /// </summary>
        /// <param name="getCardLibFuncAsync">Функция возвращающая имя файла с расширением библиотеки карточек в соответствии с которой должен выполняться импорт карточек. Файл библиотеки должен располагаться относительно описываемых им карточек также как если бы он располагался в конфигурации решения.</param>
        /// <param name="cardPredicateAsync">Функция определяющая возможность импорта карточки или значение по умолчанию для типа, если фильтрация не выполняется.</param>
        /// <returns>Объект <see cref="TestConfigurationBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public TestConfigurationBuilder ImportCardsWithCardLib(
            Func<TestConfigurationBuilder, CancellationToken, ValueTask<string>> getCardLibFuncAsync,
            Func<Card, CancellationToken, ValueTask<bool>> cardPredicateAsync = default)
        {
            Check.ArgumentNotNull(getCardLibFuncAsync, nameof(getCardLibFuncAsync));

            this.AddPendingAction(
                new PendingAction(
                    nameof(TestConfigurationBuilder) + "." + nameof(TestConfigurationBuilder.ImportCardsWithCardLib),
                    async (action, cancellationToken) =>
                    {
                        await TestCardHelper.ImportCardsWithCardLibAsync(
                            await this.DbScope.GetDbmsAsync(cancellationToken),
                            this.ResourceAssembly,
                            this.CardLibraryManager,
                            await getCardLibFuncAsync(this, cancellationToken),
                            cardPredicateAsync,
                            cancellationToken);

                        return ValidationResult.Empty;
                    }));
            return this;
        }

        /// <summary>
        /// Импортирует все типы карточек из ресурсов указанной сборки, расположенные в заданной директории.
        /// </summary>
        /// <param name="directory">Директория, из которой должны быть импортированы типы карточек расположенная в <see cref="ResourcesPaths.Types.Name"/>.</param>
        /// <param name="callbackAsync">Метод, вызываемый перед импортом каждого типа.</param>
        /// <returns>Объект <see cref="TestConfigurationBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public TestConfigurationBuilder ImportTypesFromDirectory(
            string directory,
            Func<CardType, CancellationToken, ValueTask> callbackAsync = default)
        {
            var pendingAction = new PendingAction(
                nameof(TestConfigurationBuilder) + "." + nameof(TestConfigurationBuilder.ImportTypesFromDirectory),
                async (action, ct) =>
                {
                    if (this.Dependencies.ServerSide)
                    {
                        await TestCardHelper.ImportTypesFromDirectoryAsync(
                            this.ResourceAssembly,
                            this.cardTypeServerRepository,
                            directory: directory,
                            callbackAsync: callbackAsync,
                            cancellationToken: ct);
                    }
                    else
                    {
                        await TestCardHelper.ImportTypesFromDirectoryAsync(
                            this.ResourceAssembly,
                            this.cardTypeClientRepository,
                            directory: directory,
                            callbackAsync: callbackAsync,
                            cancellationToken: ct);
                    }

                    return ValidationResult.Empty;
                });

            this.AddPendingAction(pendingAction);
            return this;
        }

        /// <summary>
        /// Импортирует все представления из ресурсов указанной сборки, расположенные в заданной директории.
        /// </summary>
        /// <param name="directory">Путь, относительный к <see cref="ResourcesPaths.Views"/>, по которому выполнятся импорт представлений. Если задано значение <see langword="null"/> или <see cref="string.Empty"/>, тогда импорт будет выполнен из <see cref="ResourcesPaths.Views"/>.</param>
        /// <param name="importRoles">Значение <see langword="true"/>, если при импорте должны быть заменены разрешения в базе данных, иначе - <see langword="false"/>.</param>
        /// <param name="clearViews">Значение <see langword="true"/>, если перед импортом все существующие представления должны быть удалены, иначе - <see langword="false"/>.</param>
        /// <returns>Объект <see cref="TestConfigurationBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public TestConfigurationBuilder ImportViewsFromDirectory(
            string directory = default,
            bool importRoles = default,
            bool clearViews = default)
        {
            var pendingAction = new PendingAction(
                nameof(TestConfigurationBuilder) + "." + nameof(TestConfigurationBuilder.ImportViewsFromDirectory),
                async (action, ct) =>
                {
                    var viewModels = await TestViewHelper.ReadViewsAsync(
                        this.ResourceAssembly,
                        this.exchangeFormatInterpreter,
                        this.indentationStrategy,
                        this.jsonViewModelImporter,
                        this.jsonViewModelAdapter,
                        directory,
                        ct);

                    var request = new ImportTessaViewRequest
                    {
                        Models = viewModels,
                        ImportRoles = importRoles,
                        NeedClear = clearViews
                    };

                    await this.tessaViewService.ImportViewsAsync(request, ct);

                    return ValidationResult.Empty;
                });

            this.AddPendingAction(pendingAction);
            return this;
        }

        /// <summary>
        /// Выполняет указанную коллекцию SQL-скриптов расположенных в встроенных ресурсах сборки по пути <see cref="ResourcesPaths.Resources"/>\<see cref="ResourcesPaths.Sql"/>.
        /// </summary>
        /// <param name="scriptFileNames">Имена файлов SQL-скриптов.</param>
        /// <param name="timeoutSeconds">Таймаут в секундах или значение <see langword="null"/>, если используется таймаут по умолчанию. Укажите <c>0</c>, чтобы таймаут был неограниченным.</param>
        /// <returns>Объект <see cref="TestConfigurationBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public TestConfigurationBuilder ExecuteSqlScripts(
            ICollection<string> scriptFileNames,
            int? timeoutSeconds = null)
        {
            Check.ArgumentNotNull(scriptFileNames, nameof(scriptFileNames));

            if (scriptFileNames.Count > 0)
            {
                var pendingAction = new PendingAction(
                    nameof(TestConfigurationBuilder) + "."
                    + nameof(TestConfigurationBuilder.ExecuteSqlScripts)
                    + "(" + string.Join(", ", scriptFileNames) + ")",
                    async (action, ct) =>
                    {
                        await TestHelper.ExecuteSqlScriptsFromEmbeddedResourcesAsync(
                            this.DbScope,
                            this.ResourceAssembly,
                            scriptFileNames,
                            timeoutSeconds,
                            ct);

                        return ValidationResult.Empty;
                    });

                this.AddPendingAction(pendingAction);
            }

            return this;
        }

        /// <summary>
        /// Сбрасывает кэш с метаинформацией по карточкам.
        /// </summary>
        /// <returns>Объект <see cref="TestConfigurationBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public TestConfigurationBuilder CardMetadataCacheInvalidate()
        {
            this.AddPendingAction(
                new PendingAction(
                    nameof(TestConfigurationBuilder) + "." + nameof(TestConfigurationBuilder.CardMetadataCacheInvalidate),
                    this.CardMetadataCacheInvalidateAsync));
            return this;
        }

        #endregion

        #region Private methods

        private async ValueTask<ValidationResult> ConfigureTimeZonesAsync(
            IPendingAction action,
            CancellationToken cancellationToken = default)
        {
            await new DefaultTimeZonesBuilder(this.Dependencies)
                .Create()
                .WithDefaultZone()
                .WithTimeZones(TestHelper.TestTimeZonesWithoutDefaultTimeZone)
                .Save()
                .GoAsync(cancellationToken: cancellationToken);

            return ValidationResult.Empty;
        }

        private async ValueTask<ValidationResult> CardMetadataCacheInvalidateAsync(
            IPendingAction action,
            CancellationToken cancellationToken = default)
        {
            if (this.Dependencies.ServerSide)
            {
                await this.cardMetadataCache.InvalidateGlobalAsync(cancellationToken);
            }
            else
            {
                await this.cardCachedMetadata.InvalidateAsync(cancellationToken);
            }

            return ValidationResult.Empty;
        }

        [DebuggerStepThrough]
        private void CheckServerDependencies()
        {
            if (!this.Dependencies.ServerSide)
            {
                throw new InvalidOperationException("Method is available for server tests only.");
            }
        }

        #endregion
    }
}
