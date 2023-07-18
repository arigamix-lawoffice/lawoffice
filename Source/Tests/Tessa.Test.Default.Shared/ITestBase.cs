using System;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Caching;
using Tessa.Cards.ComponentModel;
using Tessa.Platform;
using Tessa.Platform.Data;
using Tessa.Platform.Runtime;
using Tessa.Test.Default.Shared.Kr;
using Unity;

namespace Tessa.Test.Default.Shared
{
    /// <summary>
    /// Базовый класс для тестов.
    /// </summary>
    public interface ITestBase :
        IResourceAssemblyManager,
        IUnityContainerHolder,
        IDbScopeContainer,
        ITestActionsContainer,
        IFixtureNameProvider,
        IDisposable,
        ISealable
    {
        #region Properties

        /// <summary>
        /// Возвращает текущую сессию или значение по умолчанию для типа, если объект не зарегистрирован в <see cref="IUnityContainerHolder.UnityContainer"/>.
        /// </summary>
        ISession Session { get; }

        /// <summary>
        /// Возвращает репозиторий для управления карточками или значение по умолчанию для типа, если объект не зарегистрирован в <see cref="IUnityContainerHolder.UnityContainer"/>.
        /// </summary>
        ICardRepository CardRepository { get; }

        /// <summary>
        /// Возвращает репозиторий для управления карточками с конфигурацией по умолчанию (<see cref="CardRepositoryNames.Default"/>) или значение по умолчанию для типа, если объект не зарегистрирован в <see cref="IUnityContainerHolder.UnityContainer"/>.
        /// </summary>
        ICardRepository DefaultCardRepository { get; }

        /// <summary>
        /// Возвращает объект, управляющий операциями с карточками или значение по умолчанию для типа, если объект не зарегистрирован в <see cref="IUnityContainerHolder.UnityContainer"/>.
        /// </summary>
        ICardManager CardManager { get; }

        /// <summary>
        /// Возвращает объект, управляющий операциями с библиотеками карточек.
        /// </summary>
        ICardLibraryManager CardLibraryManager { get; }

        /// <summary>
        /// Возвращает метаинформацию, необходимую для использования типов карточек совместно с пакетом карточек или значение по умолчанию для типа, если объект не зарегистрирован в <see cref="IUnityContainerHolder.UnityContainer"/>.
        /// </summary>
        ICardMetadata CardMetadata { get; }

        /// <summary>
        /// Возвращает кэш карточек или значение по умолчанию для типа, если объект не зарегистрирован в <see cref="IUnityContainerHolder.UnityContainer"/>.
        /// </summary>
        ICardCache CardCache { get; }

        /// <summary>
        /// Возвращает зависимости, используемые объектами, управляющими жизненным циклом карточек или значение по умолчанию для типа, если объект не зарегистрирован в <see cref="IUnityContainerHolder.UnityContainer"/>.
        /// </summary>
        ICardLifecycleCompanionDependencies CardLifecycleDependencies { get; }

        /// <summary>
        /// Возвращает объект, управляющий удалением карточек после завершения каждого теста или значение по умолчанию для типа, если объект <see cref="ICardLifecycleCompanionDependencies"/> не зарегистрирован в <see cref="IUnityContainerHolder.UnityContainer"/>.
        /// </summary>
        /// <remarks>
        /// Используемая реализация может быть изменена, регистрацией другого типа в Unity-контейнере:
        /// <code language="cs">
        /// <![CDATA[
        /// protected override async ValueTask InitializeContainerAsync(IUnityContainer container)
        /// {
        ///     await base.InitializeContainerAsync(container);
        /// 
        ///     container.RegisterType<ITestCardManager, NewTestCardManager>(TestCardManagerNames.Every, new TransientLifetimeManager());
        /// }]]>
        /// </code>
        /// </remarks>
        ITestCardManager TestCardManager { get; }

        /// <summary>
        /// Возвращает объект, управляющий удалением карточек после завершения всех тестов, включая дочерние или значение по умолчанию для типа, если объект <see cref="ICardLifecycleCompanionDependencies"/> не зарегистрирован в <see cref="IUnityContainerHolder.UnityContainer"/>.
        /// </summary>
        /// <remarks>
        /// Используемая реализация может быть изменена, регистрацией другого типа в Unity-контейнере:
        /// <code language="cs">
        /// <![CDATA[
        /// protected override async ValueTask InitializeContainerAsync(IUnityContainer container)
        /// {
        ///     await base.InitializeContainerAsync(container);
        /// 
        ///     container.RegisterSingleton<ITestCardManager, NewTestCardManager>(TestCardManagerNames.Once);
        /// }]]>
        /// </code>
        /// </remarks>
        ITestCardManager TestCardManagerOnce { get; }

        /// <summary>
        /// Возвращает значение, показывающее, что инициализация зависимостей была выполнена.
        /// </summary>
        bool IsInitialized { get; set; }

        /// <summary>
        /// Возвращает конфигуратор тестовой базы данных или значение по умолчанию для типа, если объект не зарегистрирован в <see cref="IUnityContainerHolder.UnityContainer"/>.
        /// </summary>
        /// <remarks>
        /// Все запланированные действия будут выполнены автоматически после завершения выполнения метода. Безопасно вызывать исполнение запланированных действий явным образом.
        /// </remarks>
        TestConfigurationBuilder TestConfigurationBuilder { get; }

        /// <summary>
        /// Возвращает или задаёт режим удаления файлового хранилища.
        /// </summary>
        RemoveFileStorageMode RemoveFileStorageMode { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Создаёт стратегию управления контентов файлов для тестов.
        /// </summary>
        /// <param name="dbScope">Объект, обеспечивающий соединение с базой данных.</param>
        /// <param name="randomizeFileBasePath">Значение <see langword="true"/>, если необходимо вернуть рандомизированный путь к файловому хранилищу, иначе - <see langword="false"/>.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Стратегия управления контентом файлов.</returns>
        ValueTask<ICardContentStrategy> CreateContentStrategyAsync(
            IDbScope dbScope,
            bool randomizeFileBasePath = true,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Возвращает путь к файловому хранилищу.
        /// </summary>
        /// <param name="randomize">Значение <see langword="true"/>, если необходимо вернуть рандомизированный путь, иначе - <see langword="false"/>.</param>
        /// <returns>Путь к файловому хранилищу.</returns>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <remarks>Рекомендуется использовать рандомизированный путь для возможности параллельного выполнения тестов.</remarks>
        ValueTask<string> GetFileStoragePathAsync(
            bool randomize = true,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Создаёт кэш настроек с местоположением файлов для тестов.
        /// </summary>
        /// <param name="useDatabaseAsDefault">Признак того, что в качестве источника файлов по умолчанию используется база данных.</param>
        /// <param name="randomizeFileBasePath">Значение <see langword="true"/>, если необходимо вернуть рандомизированный путь к файловому хранилищу, иначе - <see langword="false"/>.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Кэш настроек с местоположением файлов для тестов.</returns>
        ValueTask<ICardFileSourceSettings> CreateDefaultFileSourceSettingsAsync(
            bool useDatabaseAsDefault = false,
            bool randomizeFileBasePath = true,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Выполняет действия перед выполнением тестов. Выполняет инициализацию зависимостей строго один раз для всех тестов.
        /// </summary>
        /// <returns>Асинхронная задача.</returns>
        Task SetUpAsync();

        /// <summary>
        /// Выполняет инициализацию тестов. Данный метод выполняется один раз для всех тестов.
        /// </summary>
        /// <returns>Асинхронная задача.</returns>
        /// <remarks>
        /// Для настройки тестовой базы данных используйте объект предоставляемый свойством <see cref="TestConfigurationBuilder"/>. Все запланированные действия в <see cref="TestConfigurationBuilder"/> будут автоматически выполнены после завершения этого метода. Безопасно вызывать исполнение запланированных действий явным образом.<para/>
        /// Если в <see cref="IUnityContainerHolder.UnityContainer"/> зарегистрирован объект типа <see cref="IDbScope"/>, то данный метод выполняется в контексте области видимости соединения с базой данных.
        /// </remarks>
        Task InitializeCoreAsync();

        /// <summary>
        /// Выполняет инициализацию области выполнения тестов. Данный метод выполняется один раз при инициализации области выполнения тестов.
        /// </summary>
        /// <returns>Асинхронная задача.</returns>
        /// <remarks>
        /// Для настройки тестовой базы данных используйте объект предоставляемый свойством <see cref="TestConfigurationBuilder"/>. Все запланированные действия в <see cref="TestConfigurationBuilder"/> будут автоматически выполнены после завершения этого метода. Безопасно вызывать исполнение запланированных действий явным образом.<para/>
        /// Если в <see cref="IUnityContainerHolder.UnityContainer"/> зарегистрирован объект типа <see cref="IDbScope"/>, то данный метод выполняется в контексте области видимости соединения с базой данных.
        /// </remarks>
        Task InitializeScopeCoreAsync();

        /// <summary>
        /// Выполняется для каждого теста.
        /// </summary>
        /// <returns>Асинхронная задача.</returns>
        Task SetUpCoreAsync();

        /// <summary>
        /// Выполняет действия при инициализации каждого теста, только если инициализацию зависимостей не требовалось выполнять.
        /// </summary>
        /// <returns>Асинхронная задача.</returns>
        Task NeedInitializeCoreAsync();

        /// <summary>
        /// Выполняет действия при завершении каждого теста. Метод гарантированно будет вызван, даже если возникнет исключение.
        /// </summary>
        /// <returns>Асинхронная задача.</returns>
        Task TearDownAsync();

        /// <summary>
        /// Выполняет действия при завершении каждого теста.
        /// </summary>
        /// <returns>Асинхронная задача.</returns>
        Task TearDownCoreAsync();

        /// <summary>
        /// Выполняет действия один раз после выполнения всех дочерних тестов. Метод гарантированно будет вызван, даже если возникнет исключение.
        /// </summary>
        /// <returns>Асинхронная задача.</returns>
        Task OneTimeTearDownAsync();

        /// <summary>
        /// Выполняет действия один раз после выполнения всех дочерних тестов.
        /// </summary>
        /// <returns>Асинхронная задача.</returns>
        Task OneTimeTearDownCoreAsync();

        /// <summary>
        /// Выполняет действия один раз при освобождении области выполнения.
        /// </summary>
        /// <returns>Асинхронная задача.</returns>
        Task OneTimeTearDownScopeCoreAsync();

        /// <summary>
        /// Удаляет карточки запланированные к удалению после завершения каждого теста.
        /// </summary>
        /// <returns>Асинхронная задача.</returns>
        Task RemoveCardAfterTestAsync();

        /// <summary>
        /// Удаляет карточки запланированные к удалению после завершения всех дочерних тестов.
        /// </summary>
        /// <returns>Асинхронная задача.</returns>
        Task RemoveCardOnceAfterTestAsync();

        /// <summary>
        /// Создаёт и инициализирует Unity контейнер.
        /// </summary>
        /// <returns>Созданный Unity контейнер.</returns>
        ValueTask CreateAndInitializeContainerAsync();

        /// <summary>
        /// Создаёт Unity контейнер.
        /// </summary>
        /// <returns>Созданный Unity контейнер.</returns>
        ValueTask<IUnityContainer> CreateContainerAsync();

        /// <summary>
        /// Инициализирует Unity контейнер.
        /// </summary>
        /// <param name="container">Инициализируемый Unity контейнер.</param>
        /// <returns>Асинхронная задача.</returns>
        ValueTask InitializeContainerAsync(IUnityContainer container);

        /// <summary>
        /// Изменяет серверное текущее время.
        /// </summary>
        /// <param name="utcNow">Устанавливаемое текущее время.</param>
        void SetServerUtcNow(DateTime? utcNow);

        #endregion
    }
}
