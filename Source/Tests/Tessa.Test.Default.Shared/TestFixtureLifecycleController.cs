#nullable enable

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using NLog;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using Tessa.Localization;
using Tessa.Platform;
using Tessa.Platform.Validation;
using Tessa.Test.Default.Shared.GC;
using Tessa.Test.Default.Shared.Kr;
using AssemblyHelper = Tessa.Platform.AssemblyHelper;

namespace Tessa.Test.Default.Shared
{
    /// <summary>
    /// Объект, управляющий жизненным циклом класса с тестами (test fixture), реализующего <see cref="ITestBase"/>.
    /// </summary>
    public sealed class TestFixtureLifecycleController :
        IDisposable
    {
        #region Constants And Static Fields

        /// <summary>
        /// Действие, выполняемое при исключении в методе с атрибутом <see cref="OneTimeTearDownAttribute"/>.
        /// </summary>
        private static readonly Action<Exception> recordTearDownExceptionAction =
            TestHelper.TestExecutionContext.CurrentResult.RecordTearDownException;

        private static readonly NLog.Logger logger = LogManager.GetCurrentClassLogger();

        private static readonly AsyncSynchronizedOneTimeRegistrator globalTestLocalizationRegistrator =
            new AsyncSynchronizedOneTimeRegistrator(InitializeDefaultLocalizationAsync);

        #endregion

        #region Fields

        private readonly AsyncLock asyncLock = new AsyncLock();

        private readonly ITestBase testFixture;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="TestFixtureLifecycleController"/>.
        /// </summary>
        /// <param name="testFixture"><inheritdoc cref="ITestBase" path="/summary"/></param>
        public TestFixtureLifecycleController(
            ITestBase testFixture) =>
            this.testFixture = NotNullOrThrow(testFixture);

        #endregion

        #region Public Methods

        /// <summary>
        /// Выполняет действия выполняющиеся при вызове <see cref="ITestBase.SetUpAsync"/>.
        /// </summary>
        /// <returns>Асинхронная задача.</returns>
        public Task SetUpAsync()
        {
            LocalizationManager.SetEnglishLocalization();

            return this.testFixture.IsInitialized
                ? this.SetUpSequenceAsync(false)
                : this.InitializeSequenceAsync();
        }

        /// <summary>
        /// Выполняет действия выполняющиеся при вызове <see cref="ITestBase.TearDownAsync"/>.
        /// </summary>
        /// <returns>Асинхронная задача.</returns>
        public async Task TearDownAsync()
        {
            const FailureSite site = FailureSite.TearDown;
            static void SafeRecordAction(Exception e)
            {
                TestHelper.SetAssertionResult(site, e);
            }

            await TestHelper.SafeExecuteAllActionsAsync(
                this.testFixture.GetTestActions(ActionStage.BeforeTearDown),
                SafeRecordAction);

            await TestHelper.SafeExecuteAsync(
                this.testFixture.RemoveCardAfterTestAsync,
                SafeRecordAction);

            await TestHelper.SafeExecuteAsync(
                this.testFixture.TearDownCoreAsync,
                SafeRecordAction);

            await TestHelper.SafeExecuteAllActionsAsync(
                this.testFixture.GetTestActions(ActionStage.AfterTearDown),
                SafeRecordAction,
                isReverse: true);
        }

        /// <summary>
        /// Выполняет действия выполняющиеся при вызове <see cref="ITestBase.OneTimeTearDownAsync"/>.
        /// </summary>
        /// <returns>Асинхронная задача.</returns>
        public async Task OneTimeTearDownAsync()
        {
            var scopeContext = KrTestContext.CurrentContext.ScopeContext;

            if (scopeContext is null)
            {
                await this.NeedOneTimeTearDownScopeAsync();
                await this.OneTimeTearDownScopeAsync();

                await TestHelper.SafeExecuteAsync(
                    () => this.CollectAllAsync().AsTask(),
                    recordTearDownExceptionAction);
            }
            else
            {
                await scopeContext.ReleaseScopeAsync(
                    async ct =>
                    {
                        await this.NeedOneTimeTearDownScopeAsync(ct);
                        await this.OneTimeTearDownScopeAsync();

                        await TestHelper.SafeExecuteAsync(
                            () => this.CollectAllAsync().AsTask(),
                            recordTearDownExceptionAction);
                    },
                    this.NeedOneTimeTearDownScopeAsync);

                if (!scopeContext.IsInitialized
                    || scopeContext.Instances <= 0)
                {
                    await scopeContext.DisposeAsync();
                }
            }

            if (this.testFixture.UnityContainer is not null)
            {
                await this.testFixture.UnityContainer.DisposeAllRegistrationsAsync();
            }
        }

        #endregion

        #region IDisposable Members

        /// <inheritdoc/>
        public void Dispose() => this.asyncLock.Dispose();

        #endregion

        #region Private Methods

        private async Task InitializeSequenceAsync()
        {
            using (await this.asyncLock.EnterAsync())
            {
                if (this.testFixture.IsInitialized)
                {
                    await this.SetUpSequenceAsync(false);
                    return;
                }

                try
                {
                    await TestHelper.InitializeTestPlatformAsync();
                    await globalTestLocalizationRegistrator.RegisterAsync();

                    var scopeContext = KrTestContext.CurrentContext.ScopeContext;

                    if (scopeContext is null)
                    {
                        await this.InitializeSequenceCoreAsync(true);
                    }
                    else
                    {
                        await scopeContext.InitializeScopeAsync(
                            async _ => await this.InitializeSequenceCoreAsync(true),
                            async _ => await this.InitializeSequenceCoreAsync(false));
                    }

                    this.testFixture.IsInitialized = true;
                    this.testFixture.Seal();
                }
                catch
                {
                    // Задание статуса выполнения для остановки выполнения параллельно запущенных тестов из этого же TestFixture.
                    TestHelper.TestExecutionContext.ExecutionStatus = TestExecutionStatus.StopRequested;

                    // Повторное создание оригинального исключения для остановки выполнения текущего теста.
                    throw;
                }
            }

            await this.SetUpSequenceAsync(true);
        }

        private async Task InitializeSequenceCoreAsync(bool isInitializeScope)
        {
            await TestHelper.SafeExecuteAllActionsAsync(
                this.testFixture.GetTestActions(ActionStage.BeforeInitialize));

            await this.testFixture.CreateAndInitializeContainerAsync();

            if (isInitializeScope)
            {
                await this.InitializeScopeAsync();
            }

            await this.InitializeLocalAsync();

            await TestHelper.SafeExecuteAllActionsAsync(
                this.testFixture.GetTestActions(ActionStage.AfterInitialize),
                isReverse: true);
        }

        private async Task SetUpSequenceAsync(bool initialization)
        {
            await TestHelper.SafeExecuteAllActionsAsync(
                this.testFixture.GetTestActions(ActionStage.BeforeSetUp));

            await this.testFixture.SetUpCoreAsync();

            await TestHelper.SafeExecuteAllActionsAsync(
                this.testFixture.GetTestActions(ActionStage.AfterSetUp),
                isReverse: true);

            if (!initialization)
            {
                await this.testFixture.NeedInitializeCoreAsync();
            }
        }

        private async Task InitializeScopeAsync()
        {
            await TestHelper.SafeExecuteAllActionsAsync(
                this.testFixture.GetTestActions(ActionStage.BeforeInitializeScope));

            if (this.testFixture.DbScope is null)
            {
                await this.testFixture.InitializeScopeCoreAsync();
            }
            else
            {
                await using (this.testFixture.DbScope.Create())
                {
                    if (this.testFixture.DbFactory is null)
                    {
                        var dataConnection = this.testFixture.DbScope.Db.DataConnection;
                        this.testFixture.DbFactory = new TestDbFactory(
                            dataConnection.DataProvider,
                            dataConnection.ConnectionString);
                    }

                    await this.testFixture.InitializeScopeCoreAsync();
                }
            }

            if (this.testFixture.TestConfigurationBuilder is not null)
            {
                await this.testFixture.TestConfigurationBuilder.GoAsync();
            }

            await TestHelper.SafeExecuteAllActionsAsync(
                this.testFixture.GetTestActions(ActionStage.AfterInitializeScope));
        }

        private async Task InitializeLocalAsync()
        {
            if (this.testFixture.DbScope is null)
            {
                await this.testFixture.InitializeCoreAsync();
            }
            else
            {
                await using (this.testFixture.DbScope.Create())
                {
                    if (this.testFixture.DbFactory is null)
                    {
                        var dataConnection = this.testFixture.DbScope.Db.DataConnection;
                        this.testFixture.DbFactory = new TestDbFactory(
                            dataConnection.DataProvider,
                            dataConnection.ConnectionString);
                    }

                    await this.testFixture.InitializeCoreAsync();
                }
            }

            if (this.testFixture.TestConfigurationBuilder is not null)
            {
                await this.testFixture.TestConfigurationBuilder.GoAsync();
            }
        }

        private async Task NeedOneTimeTearDownScopeAsync(
            CancellationToken cancellationToken = default)
        {
            await TestHelper.SafeExecuteAllActionsAsync(
                this.testFixture.GetTestActions(ActionStage.BeforeOneTimeTearDown),
                recordTearDownExceptionAction);

            await TestHelper.SafeExecuteAsync(
                this.testFixture.RemoveCardOnceAfterTestAsync,
                recordTearDownExceptionAction);

            await TestHelper.SafeExecuteAsync(
                this.testFixture.OneTimeTearDownCoreAsync,
                recordTearDownExceptionAction);

            await TestHelper.SafeExecuteAllActionsAsync(
                this.testFixture.GetTestActions(ActionStage.AfterOneTimeTearDown),
                recordTearDownExceptionAction,
                isReverse: true);
        }

        private async Task OneTimeTearDownScopeAsync()
        {
            await TestHelper.SafeExecuteAllActionsAsync(
                this.testFixture.GetTestActions(ActionStage.BeforeOneTimeTearDownScope),
                recordTearDownExceptionAction,
                isReverse: true);

            await TestHelper.SafeExecuteAsync(
                this.testFixture.OneTimeTearDownScopeCoreAsync,
                recordTearDownExceptionAction);

            await TestHelper.SafeExecuteAllActionsAsync(
                this.testFixture.GetTestActions(ActionStage.AfterOneTimeTearDownScope),
                recordTearDownExceptionAction,
                isReverse: true);
        }

        private async ValueTask CollectAllAsync()
        {
            // Контейнера и объекта может не быть из-за ошибок при инициализации.
            var externalObjectManager = this.testFixture.UnityContainer?.TryResolve<IExternalObjectManager>();

            if (externalObjectManager is null)
            {
                return;
            }

            var validationResult = new ValidationResultBuilder();
            await externalObjectManager.CollectAsync(this.GetHashCode(), validationResult);
            logger.LogResult(validationResult);
        }

        /// <summary>
        /// Инициализирует локализацию по умолчанию.
        /// </summary>
        /// <returns>Асинхронная задача.</returns>
        /// <remarks>Язык локализации по умолчанию: английский.</remarks>
        private static ValueTask InitializeDefaultLocalizationAsync()
        {
            LocalizationManager.SetEnglishLocalization();

            var path = Path.Combine(ResourcesPaths.Resources, ResourcesPaths.Localization);

            // Здесь всегда есть TestExecutionContext.CurrentContext в момент вызова.
            var assembly = ((IResourceAssemblyManager) TestHelper.TestExecutionContext.CurrentTest.Fixture!).ResourceAssembly;
            var resourceNames = AssemblyHelper.GetFileNameEnumerableFromEmbeddedResources(
                assembly,
                path);

            var filteredResourceNames = new List<string>();
            var detachedResourceNames = new List<string>();

            foreach (var resourceName in resourceNames)
            {
                var resourceFullName = resourceName.FullName;

                if (resourceFullName.EndsWith(JsonFileLocalizationService.FileNameExtension, StringComparison.Ordinal))
                {
                    filteredResourceNames.Add(resourceFullName);
                }
                else if (resourceFullName.EndsWith(JsonFileLocalizationService.DetachedFileNameExtension, StringComparison.Ordinal))
                {
                    detachedResourceNames.Add(resourceFullName);
                }
            }

            if (filteredResourceNames.Count == 0)
            {
                TestContext.Progress.WriteLine($"Assembly \"{assembly.FullName}\" (location: {assembly.Location}) does not contain localization files in \"{path}\".");
            }

            // Тесты из разных сборок выполняются в разных процессах.

            return LocalizationManager.InitializeDefaultLocalizationAsync(
                detectLanguage: false,
                fallbackToCoreLibrary: false,
                localizationServices: new ILocalizationService[]
                {
                    new JsonResourceFileLocalizationService(
                        assembly,
                        filteredResourceNames,
                        detachedResourceNames)
                });
        }

        #endregion
    }
}
