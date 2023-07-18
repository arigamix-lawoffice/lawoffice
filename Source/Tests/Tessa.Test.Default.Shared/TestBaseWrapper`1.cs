using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using Tessa.Cards;
using Tessa.Cards.Caching;
using Tessa.Cards.ComponentModel;
using Tessa.Platform.Data;
using Tessa.Platform.Runtime;
using Tessa.Roles;
using Tessa.Test.Default.Shared.Kr;
using Tessa.Test.Default.Shared.Roles;
using Unity;

namespace Tessa.Test.Default.Shared
{
    /// <inheritdoc cref="ITestBaseWrapper{T}"/>
    [DefaultTestScope]
    public abstract class TestBaseWrapper<T> :
        ResourceAssemblyManager,
        ITestBaseWrapper<T>
        where T : class, ITestBase
    {
        #region Fields

        private readonly TestFixtureLifecycleController testFixtureLifecycleController;

        private bool isDisposed;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="TestBaseWrapper{T}"/>.
        /// </summary>
        /// <param name="internalTestBase">Объект для которого создаётся обёртка.</param>
        protected TestBaseWrapper(
            T internalTestBase)
        {
            this.InternalTestBase = NotNullOrThrow(internalTestBase);

            this.testFixtureLifecycleController = new TestFixtureLifecycleController(this);
        }

        #endregion

        #region ITestBaseWrapper<T> Members

        /// <inheritdoc/>
        public T InternalTestBase { get; }

        /// <inheritdoc/>
        public ISession Session => this.InternalTestBase.Session;

        /// <inheritdoc/>
        public ICardRepository CardRepository => this.InternalTestBase.CardRepository;

        /// <inheritdoc/>
        public ICardRepository DefaultCardRepository => this.InternalTestBase.DefaultCardRepository;

        /// <inheritdoc/>
        public ICardManager CardManager => this.InternalTestBase.CardManager;

        /// <inheritdoc/>
        public ICardLibraryManager CardLibraryManager => this.InternalTestBase.CardLibraryManager;

        /// <inheritdoc/>
        public ICardMetadata CardMetadata => this.InternalTestBase.CardMetadata;

        /// <inheritdoc/>
        public ICardCache CardCache => this.InternalTestBase.CardCache;

        /// <inheritdoc/>
        public ICardLifecycleCompanionDependencies CardLifecycleDependencies => this.InternalTestBase.CardLifecycleDependencies;

        /// <inheritdoc/>
        public ITestCardManager TestCardManager => this.InternalTestBase.TestCardManager;

        /// <inheritdoc/>
        public ITestCardManager TestCardManagerOnce => this.InternalTestBase.TestCardManagerOnce;

        /// <inheritdoc/>
        public bool IsInitialized
        {
            get => this.InternalTestBase.IsInitialized;
            set => this.InternalTestBase.IsInitialized = value;
        }

        /// <inheritdoc/>
        public TestConfigurationBuilder TestConfigurationBuilder => this.InternalTestBase.TestConfigurationBuilder;

        /// <inheritdoc/>
        public RemoveFileStorageMode RemoveFileStorageMode
        {
            get => this.InternalTestBase.RemoveFileStorageMode;
            set => this.InternalTestBase.RemoveFileStorageMode = value;
        }

        /// <inheritdoc/>
        public virtual IUnityContainer UnityContainer
        {
            get => this.InternalTestBase.UnityContainer;
            set => this.InternalTestBase.UnityContainer = value;
        }

        /// <inheritdoc/>
        public IDbFactory DbFactory
        {
            get => this.InternalTestBase.DbFactory;
            set => this.InternalTestBase.DbFactory = value;
        }

        /// <inheritdoc/>
        public IDbScope DbScope
        {
            get => this.InternalTestBase.DbScope;
            set => this.InternalTestBase.DbScope = value;
        }

        /// <inheritdoc/>
        public bool IsSealed => this.InternalTestBase.IsSealed;

        /// <inheritdoc/>
        public ValueTask<ICardContentStrategy> CreateContentStrategyAsync(
            IDbScope dbScope,
            bool randomizeFileBasePath = true,
            CancellationToken cancellationToken = default) =>
            this.InternalTestBase.CreateContentStrategyAsync(dbScope, randomizeFileBasePath, cancellationToken);

        /// <inheritdoc/>
        public ValueTask<ICardFileSourceSettings> CreateDefaultFileSourceSettingsAsync(
            bool useDatabaseAsDefault = false,
            bool randomizeFileBasePath = true,
            CancellationToken cancellationToken = default) =>
            this.InternalTestBase.CreateDefaultFileSourceSettingsAsync(useDatabaseAsDefault, randomizeFileBasePath, cancellationToken);

        /// <inheritdoc/>
        public ValueTask<string> GetFileStoragePathAsync(
            bool randomize = true,
            CancellationToken cancellationToken = default) =>
            this.InternalTestBase.GetFileStoragePathAsync(randomize, cancellationToken);

        /// <inheritdoc/>
        public ValueTask<DateTime> GetFixtureDateTimeAsync(
            CancellationToken cancellationToken = default) =>
            this.InternalTestBase.GetFixtureDateTimeAsync(cancellationToken);

        /// <inheritdoc/>
        public ValueTask<string> GetFixtureNameAsync(
            CancellationToken cancellationToken = default) =>
            this.InternalTestBase.GetFixtureNameAsync(cancellationToken);

        /// <inheritdoc/>
        public ValueTask<string> GetNextRandomFixtureNameAsync(
            CancellationToken cancellationToken = default) =>
            this.InternalTestBase.GetNextRandomFixtureNameAsync(cancellationToken);

        /// <inheritdoc/>
        public IList<ITestAction> GetTestActions(ActionStage stage) =>
            this.InternalTestBase.GetTestActions(stage);

        /// <inheritdoc/>
        public virtual Task RemoveCardAfterTestAsync() =>
            this.InternalTestBase.RemoveCardAfterTestAsync();

        /// <inheritdoc/>
        public virtual Task RemoveCardOnceAfterTestAsync() =>
            this.InternalTestBase.RemoveCardOnceAfterTestAsync();

        /// <inheritdoc/>
        [SetUp]
        public Task SetUpAsync() => this.testFixtureLifecycleController.SetUpAsync();

        /// <inheritdoc/>
        [TearDown]
        public Task TearDownAsync() => this.testFixtureLifecycleController.TearDownAsync();

        /// <inheritdoc/>
        [OneTimeTearDown]
        public Task OneTimeTearDownAsync() => this.testFixtureLifecycleController.OneTimeTearDownAsync();

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
        ValueTask ITestBase.CreateAndInitializeContainerAsync() => this.CreateAndInitializeContainerAsync();

        /// <inheritdoc/>
        ValueTask<IUnityContainer> ITestBase.CreateContainerAsync() => this.CreateContainerAsync();

        /// <inheritdoc/>
        ValueTask ITestBase.InitializeContainerAsync(IUnityContainer container) => this.InitializeContainerAsync(container);

        /// <inheritdoc/>
        void ITestBase.SetServerUtcNow(DateTime? utcNow) => this.SetServerUtcNow(utcNow);

        /// <inheritdoc/>
        public void Seal() => this.InternalTestBase.Seal();

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
                    this.InternalTestBase.Dispose();
                }

                this.isDisposed = true;
            }
        }

        #endregion

        #region Protected Methods

        /// <inheritdoc cref="ITestBase.CreateAndInitializeContainerAsync"/>
        protected virtual ValueTask CreateAndInitializeContainerAsync() =>
            this.InternalTestBase.CreateAndInitializeContainerAsync();

        /// <inheritdoc cref="ITestBase.CreateContainerAsync"/>
        protected virtual ValueTask<IUnityContainer> CreateContainerAsync() =>
            this.InternalTestBase.CreateContainerAsync();

        /// <inheritdoc cref="ITestBase.InitializeContainerAsync(IUnityContainer)"/>
        protected virtual ValueTask InitializeContainerAsync(IUnityContainer container) =>
            this.InternalTestBase.InitializeContainerAsync(container);

        /// <inheritdoc cref="ITestBase.InitializeCoreAsync"/>
        protected virtual Task InitializeCoreAsync() =>
            this.InternalTestBase.InitializeCoreAsync();

        /// <inheritdoc cref="ITestBase.InitializeScopeCoreAsync"/>
        protected virtual Task InitializeScopeCoreAsync() =>
            this.InternalTestBase.InitializeScopeCoreAsync();

        /// <inheritdoc cref="ITestBase.NeedInitializeCoreAsync"/>
        protected virtual Task NeedInitializeCoreAsync() =>
            this.InternalTestBase.NeedInitializeCoreAsync();

        /// <inheritdoc cref="ITestBase.OneTimeTearDownCoreAsync"/>
        protected virtual Task OneTimeTearDownCoreAsync() =>
            this.InternalTestBase.OneTimeTearDownCoreAsync();

        /// <inheritdoc cref="ITestBase.OneTimeTearDownScopeCoreAsync"/>
        protected virtual Task OneTimeTearDownScopeCoreAsync() =>
            this.InternalTestBase.OneTimeTearDownScopeCoreAsync();

        /// <inheritdoc cref="ITestBase.SetUpCoreAsync"/>
        protected virtual Task SetUpCoreAsync() =>
            this.InternalTestBase.SetUpCoreAsync();

        /// <inheritdoc cref="ITestBase.TearDownCoreAsync"/>
        protected virtual Task TearDownCoreAsync() =>
            this.InternalTestBase.TearDownCoreAsync();

        /// <inheritdoc cref="ITestBase.SetServerUtcNow"/>
        protected virtual void SetServerUtcNow(DateTime? utcNow) =>
            this.InternalTestBase.SetServerUtcNow(utcNow);

        #endregion
    }
}
