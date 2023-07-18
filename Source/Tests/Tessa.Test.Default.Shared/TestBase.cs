using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using NLog;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Tessa.Cards;
using Tessa.Cards.Caching;
using Tessa.Cards.ComponentModel;
using Tessa.Platform;
using Tessa.Platform.Data;
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using Tessa.Test.Default.Shared.GC;
using Tessa.Test.Default.Shared.GC.Handlers;
using Tessa.Test.Default.Shared.Kr;
using Unity;
using Unity.Lifetime;

namespace Tessa.Test.Default.Shared
{
    /// <summary>
    /// Абстрактный базовый класс для тестов.
    /// </summary>
    [DefaultTestScope]
    public abstract class TestBase :
        ResourceAssemblyManager,
        ITestBase
    {
        #region Constants And Static Fields

        private static readonly NLog.Logger logger = LogManager.GetCurrentClassLogger();

        #endregion

        #region Fields

        private readonly TestActionsContainer testActionsContainer = new TestActionsContainer();

        private readonly object testCardManagerLock = new object();

        private readonly TestFixtureLifecycleController testFixtureLifecycleController;

        private bool isDisposed;

        private DateTime fixtureDate;

        private string fixtureName;

        private ITestNameResolver nameResolver;

        private IDbScope dbScope;

        private bool isInitialized;

        #endregion

        #region Private Properties

        private ITestNameResolver NameResolver => this.nameResolver ??= this.UnityContainer.Resolve<ITestNameResolver>();

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="TestBase"/>.
        /// </summary>
        protected TestBase()
        {
            this.GetTestActions(ActionStage.BeforeInitializeScope).Add(
                new TestAction(
                    this,
                    GCActionsAsync));

            this.GetTestActions(ActionStage.AfterInitializeContainer).Add(
                new TestAction(
                    this,
                    async static sender =>
                    {
                        var senderT = (TestBase) sender;

                        // Вычисление и фиксация значений для исключения возможности использования методов в которых недоступен KrTestContext.
                        await senderT.GetFixtureNameAsync();
                        await senderT.GetFixtureDateTimeAsync();

                        senderT.UnityContainer.RegisterExternalObjectHandlersInRegistry();
                    }));

            this.testFixtureLifecycleController = new TestFixtureLifecycleController(this);
        }

        #endregion

        #region ITestBase Members

        /// <inheritdoc/>
        public ISession Session => this.UnityContainer.TryResolve<ISession>();

        /// <inheritdoc/>
        public ICardRepository CardRepository => this.UnityContainer.TryResolve<ICardRepository>();

        /// <inheritdoc/>
        public ICardRepository DefaultCardRepository => this.UnityContainer.TryResolve<ICardRepository>(CardRepositoryNames.Default);

        /// <inheritdoc/>
        public ICardManager CardManager => this.UnityContainer.TryResolve<ICardManager>();

        /// <inheritdoc/>
        public ICardLibraryManager CardLibraryManager => this.UnityContainer.TryResolve<ICardLibraryManager>();

        /// <inheritdoc/>
        public ICardMetadata CardMetadata => this.UnityContainer.TryResolve<ICardMetadata>();

        /// <inheritdoc/>
        public ICardCache CardCache => this.UnityContainer.TryResolve<ICardCache>();

        /// <inheritdoc/>
        public ICardLifecycleCompanionDependencies CardLifecycleDependencies => this.UnityContainer.TryResolve<ICardLifecycleCompanionDependencies>();

        /// <inheritdoc/>
        public ITestCardManager TestCardManager
        {
            get
            {
                var testCardManager = KrTestContext.CurrentContext.TestCardManager;

                if (testCardManager is not null)
                {
                    return testCardManager;
                }

                lock (this.testCardManagerLock)
                {
                    testCardManager = KrTestContext.CurrentContext.TestCardManager;

                    if (testCardManager is not null)
                    {
                        return testCardManager;
                    }

                    testCardManager = this.UnityContainer.TryResolve<ITestCardManager>(TestCardManagerNames.Every);
                    KrTestContext.CurrentContext.TestCardManager = testCardManager;

                    return testCardManager;
                }
            }
        }

        /// <inheritdoc/>
        public ITestCardManager TestCardManagerOnce => this.UnityContainer.TryResolve<ITestCardManager>(TestCardManagerNames.Once);

        /// <inheritdoc/>
        public bool IsInitialized
        {
            get => this.isInitialized;
            set
            {
                ThrowIfSealed(this);

                this.isInitialized = value;
            }
        }

        /// <inheritdoc/>
        public TestConfigurationBuilder TestConfigurationBuilder => this.UnityContainer.TryResolve<TestConfigurationBuilder>();

        /// <inheritdoc/>
        public RemoveFileStorageMode RemoveFileStorageMode { get; set; } = RemoveFileStorageMode.Auto;

        /// <inheritdoc/>
        public IDbFactory DbFactory { get; set; }

        /// <inheritdoc/>
        public IDbScope DbScope
        {
            get
            {
                this.dbScope ??= this.UnityContainer.TryResolve<IDbScope>();

                return this.dbScope;
            }
            set => this.dbScope = value;
        }

        /// <inheritdoc/>
        public virtual IUnityContainer UnityContainer
        {
            get => KrTestContext.CurrentContext.UnityContainer;
            set => KrTestContext.CurrentContext.UnityContainer = value;
        }

        /// <inheritdoc/>
        public bool IsSealed { get; private set; }

        /// <inheritdoc/>
        public async ValueTask<string> GetFixtureNameAsync(
            CancellationToken cancellationToken = default)
        {
            const string scopeKey = StorageHelper.SystemKeyPrefix + nameof(this.fixtureName);

            if (this.fixtureName is null)
            {
                var scopeContext = KrTestContext.CurrentContext.ScopeContext;

                this.fixtureName = scopeContext is null
                    ? await this.GetNextRandomFixtureNameAsync(cancellationToken)
                    : (string) await scopeContext.Info.GetOrAddAsync(
                        scopeKey,
                        async ct => (object) await this.GetNextRandomFixtureNameAsync(ct),
                        cancellationToken);
            }

            return this.fixtureName;
        }

        /// <inheritdoc/>
        public ValueTask<string> GetNextRandomFixtureNameAsync(
            CancellationToken cancellationToken = default)
        {
            return this.NameResolver.GetFixtureNameAsync(
                TestHelper.TestExecutionContext.CurrentTest,
                cancellationToken);
        }

        /// <inheritdoc/>
        public async ValueTask<DateTime> GetFixtureDateTimeAsync(
            CancellationToken cancellationToken = default)
        {
            const string scopeKey = StorageHelper.SystemKeyPrefix + nameof(this.fixtureDate);

            if (this.fixtureDate == DateTime.MinValue)
            {
                var scopeContext = KrTestContext.CurrentContext.ScopeContext;

                this.fixtureDate = scopeContext is null
                    ? await this.NameResolver.GetFixtureDateTimeAsync(cancellationToken)
                    : (DateTime) await scopeContext.Info.GetOrAddAsync(
                        scopeKey,
                        async ct => (object) await this.NameResolver.GetFixtureDateTimeAsync(ct),
                        cancellationToken);
            }

            return this.fixtureDate;
        }

        /// <inheritdoc/>
        public async ValueTask<ICardContentStrategy> CreateContentStrategyAsync(
            IDbScope dbScope,
            bool randomizeFileBasePath = true,
            CancellationToken cancellationToken = default) =>
            CardSourceContentStrategy.CreateDefault(
                await this.CreateDefaultFileSourceSettingsAsync(
                    randomizeFileBasePath: randomizeFileBasePath,
                    cancellationToken: cancellationToken),
                dbScope);

        /// <inheritdoc/>
        public async ValueTask<string> GetFileStoragePathAsync(
            bool randomize = true,
            CancellationToken cancellationToken = default)
        {
            return randomize
                ? Path.Combine(
                    TestSettings.FileStoragePath,
                    (await this.GetFixtureDateTimeAsync(cancellationToken)).FormatDateTimeCode(),
                    await this.GetFixtureNameAsync(cancellationToken))
                : TestSettings.FileStoragePath;
        }

        /// <inheritdoc/>
        public async ValueTask<ICardFileSourceSettings> CreateDefaultFileSourceSettingsAsync(
            bool useDatabaseAsDefault = false,
            bool randomizeFileBasePath = true,
            CancellationToken cancellationToken = default) =>
            CardFileSourceSettings
                .CreateDefault(
                    await this.GetFileStoragePathAsync(
                        randomizeFileBasePath,
                        cancellationToken),
                    useDatabaseAsDefault: useDatabaseAsDefault);

        /// <inheritdoc/>
        [SetUp]
        public Task SetUpAsync() => this.testFixtureLifecycleController.SetUpAsync();

        /// <inheritdoc/>
        [TearDown]
        public Task TearDownAsync() => this.testFixtureLifecycleController.TearDownAsync();

        /// <inheritdoc/>
        [OneTimeTearDown]
        public async Task OneTimeTearDownAsync() => await this.testFixtureLifecycleController.OneTimeTearDownAsync();

        /// <inheritdoc/>
        public void Seal() => this.IsSealed = true;

        /// <inheritdoc/>
        public IList<ITestAction> GetTestActions(ActionStage stage) =>
            this.testActionsContainer.GetTestActions(stage);

        /// <inheritdoc/>
        Task ITestBase.InitializeCoreAsync() => this.InitializeCoreAsync();

        /// <inheritdoc/>
        Task ITestBase.InitializeScopeCoreAsync() => this.InitializeScopeCoreAsync();

        /// <inheritdoc/>
        Task ITestBase.SetUpCoreAsync() => this.SetUpCoreAsync();

        /// <inheritdoc/>
        Task ITestBase.NeedInitializeCoreAsync() => this.NeedInitializeCoreAsync();

        /// <inheritdoc/>
        Task ITestBase.TearDownCoreAsync() => this.TearDownCoreAsync();

        /// <inheritdoc/>
        Task ITestBase.OneTimeTearDownCoreAsync() => this.OneTimeTearDownCoreAsync();

        /// <inheritdoc/>
        Task ITestBase.OneTimeTearDownScopeCoreAsync() => this.OneTimeTearDownScopeCoreAsync();

        /// <inheritdoc/>
        Task ITestBase.RemoveCardAfterTestAsync() => this.RemoveCardAfterTestAsync();

        /// <inheritdoc/>
        Task ITestBase.RemoveCardOnceAfterTestAsync() => this.RemoveCardOnceAfterTestAsync();

        /// <inheritdoc/>
        ValueTask ITestBase.CreateAndInitializeContainerAsync() => this.CreateAndInitializeContainerAsync();

        /// <inheritdoc/>
        ValueTask<IUnityContainer> ITestBase.CreateContainerAsync() => this.CreateContainerAsync();

        /// <inheritdoc/>
        ValueTask ITestBase.InitializeContainerAsync(IUnityContainer container) => this.InitializeContainerAsync(container);

        /// <inheritdoc/>
        void ITestBase.SetServerUtcNow(DateTime? utcNow) => this.SetServerUtcNow(utcNow);

        #endregion

        #region Protected methods

        /// <inheritdoc cref="ITestBase.InitializeCoreAsync"/>
        protected virtual Task InitializeCoreAsync() => Task.CompletedTask;

        /// <inheritdoc cref="ITestBase.InitializeScopeCoreAsync"/>
        protected virtual Task InitializeScopeCoreAsync() => Task.CompletedTask;

        /// <inheritdoc cref="ITestBase.SetUpCoreAsync"/>
        protected virtual Task SetUpCoreAsync()
        {
            this.SetServerUtcNow(null);
            return Task.CompletedTask;
        }

        /// <inheritdoc cref="ITestBase.NeedInitializeCoreAsync"/>
        protected virtual Task NeedInitializeCoreAsync() => Task.CompletedTask;

        /// <inheritdoc cref="ITestBase.TearDownCoreAsync"/>
        protected virtual Task TearDownCoreAsync() => Task.CompletedTask;

        /// <inheritdoc cref="ITestBase.OneTimeTearDownCoreAsync"/>
        protected virtual Task OneTimeTearDownCoreAsync() => Task.CompletedTask;

        /// <inheritdoc cref="ITestBase.OneTimeTearDownScopeCoreAsync"/>
        protected virtual Task OneTimeTearDownScopeCoreAsync() => Task.CompletedTask;

        /// <inheritdoc cref="ITestBase.RemoveCardAfterTestAsync"/>
        protected virtual async Task RemoveCardAfterTestAsync()
        {
            if (this.DbScope is not null
                && this.TestCardManager is not null)
            {
                await this.TestCardManager.AfterTestAsync();
            }
        }

        /// <inheritdoc cref="ITestBase.RemoveCardOnceAfterTestAsync"/>
        protected virtual async Task RemoveCardOnceAfterTestAsync()
        {
            if (this.DbScope is not null
                && this.TestCardManagerOnce is not null)
            {
                await this.TestCardManagerOnce.AfterTestAsync();
            }
        }

        /// <inheritdoc cref="ITestBase.CreateAndInitializeContainerAsync()"/>
        protected virtual async ValueTask CreateAndInitializeContainerAsync()
        {
            this.UnityContainer = await this.CreateContainerAsync();

            await TestHelper.SafeExecuteAllActionsAsync(this.GetTestActions(ActionStage.BeforeInitializeContainer));
            await this.InitializeContainerAsync(this.UnityContainer);
            await TestHelper.SafeExecuteAllActionsAsync(this.GetTestActions(ActionStage.AfterInitializeContainer));
        }

        /// <inheritdoc cref="ITestBase.CreateContainerAsync()"/>
        protected virtual ValueTask<IUnityContainer> CreateContainerAsync() =>
            // есть много серверных тестов, которые не используют контейнер Unity, но выполняют инициализацию в InitializeSequenceAsync
            new(new UnityContainer());

        /// <inheritdoc cref="ITestBase.InitializeContainerAsync(IUnityContainer)"/>
        protected virtual ValueTask InitializeContainerAsync(IUnityContainer container)
        {
            RegisterTestsBasicDependencies(container);
            return ValueTask.CompletedTask;
        }

        /// <inheritdoc cref="ITestBase.SetServerUtcNow(DateTime?)"/>
        protected virtual void SetServerUtcNow(DateTime? utcNow) { }

        /// <summary>
        /// Регистрирует базовые зависимости для API тестов. Вызовите при создании основного контейнера для тестов
        /// без вызова базового метода <see cref="InitializeContainerAsync"/>.
        /// </summary>
        /// <param name="container">Контейнер, в котором выполняется регистрация.</param>
        protected static void RegisterTestsBasicDependencies(IUnityContainer container)
        {
            container
                .RegisterSingleton<ITestNameResolver, TestNameResolver>()
                .RegisterType<ITestCardManager, TestCardManager>(TestCardManagerNames.Every, new TransientLifetimeManager())
                .RegisterSingleton<ITestCardManager, TestCardManager>(TestCardManagerNames.Once)
                .RegisterExternalObjects();
        }

        #endregion

        #region IDispose Members

        /// <inheritdoc/>
        public void Dispose() => this.Dispose(true);

        /// <summary>
        /// Освобождает ресурсы, занимаемые объектом.
        /// </summary>
        /// <param name="disposing">Значение <see langword="true"/>, если метод вызван до финализации, иначе - <see langword="false"/> - метод вызван из финализатора.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.isDisposed)
            {
                if (disposing)
                {
                    this.testFixtureLifecycleController.Dispose();
                    this.UnityContainer?.Dispose();
                }

                this.isDisposed = true;
            }
        }

        #endregion

        #region Private Methods

        private static async ValueTask GCActionsAsync(object sender)
        {
            if (KrTestContext.CurrentContext.ScopeContext?.IsInitialized == true)
            {
                return;
            }

            var senderT = (TestBase) sender;

            // Объекта может не быть из-за частично инициализированного контейнера.
            var externalObjectManager = senderT.UnityContainer?.TryResolve<IExternalObjectManager>();

            if (externalObjectManager is null)
            {
                return;
            }

            // Удаление объектов оставшихся после предыдущих запусков.
            var validationResult = new ValidationResultBuilder();
            await externalObjectManager.CollectAsync(TestSettings.GCKeepAliveInterval, validationResult);
            logger.LogResult(validationResult);

            // Планирование удаления временного файлового хранилища.
            var isRemoveFileStorage = senderT.RemoveFileStorageMode switch
            {
                RemoveFileStorageMode.None => false,
                RemoveFileStorageMode.Always => true,
                RemoveFileStorageMode.Auto => senderT.GetType().GetCustomAttribute<SetupTempDbAttribute>()?.RemoveDatabase != false,
                _ => throw new ArgumentOutOfRangeException(
                    nameof(RemoveFileStorageMode),
                    senderT.RemoveFileStorageMode,
                    "Unknown mode."),
            };

            if (isRemoveFileStorage)
            {
                var fileStoragePath = await senderT.GetFileStoragePathAsync();
                var obj = FolderExternalObjectHandler.CreateObjectInfo(
                    fileStoragePath,
                    senderT.GetHashCode());
                externalObjectManager.RegisterForFinalize(obj);
            }
        }

        #endregion
    }
}
