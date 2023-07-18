using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using Tessa.BusinessCalendar;
using Tessa.Cards;
using Tessa.Cards.Caching;
using Tessa.Extensions;
using Tessa.Platform;
using Tessa.Platform.ConsoleApps;
using Tessa.Platform.Data;
using Tessa.Platform.IO;
using Tessa.Platform.Operations;
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;
using Tessa.Roles;
using Tessa.Test.Default.Shared;
using Tessa.Test.Default.Shared.Kr;
using Tessa.Test.Default.Shared.Roles;
using Tessa.Views;
using Tessa.Views.Json;
using Tessa.Views.Json.Converters;
using Tessa.Views.Parser;
using Tessa.Views.Parser.SyntaxTree.ExchangeFormat;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace Tessa.Test.Default.Client
{
    /// <summary>
    /// Базовый абстрактный класс для клиентских тестов без поддержки пользовательского интерфейса.
    /// </summary>
    public abstract class ClientTestBase :
        TestBase
    {
        #region Protected Properties

        /// <summary>
        /// Тестируемый объект <see cref="ICardStreamClientRepository"/> с расширениями.
        /// </summary>
        protected ICardStreamClientRepository CardStreamRepository { get; private set; }

        /// <summary>
        /// Тестируемый объект <see cref="ICardTypeClientRepository"/>.
        /// </summary>
        protected ICardTypeClientRepository CardTypeRepository { get; private set; }

        /// <summary>
        /// Возвращает значение, показывающее, должна ли выполняться расширенная инициализация при открытии сессии
        /// (с выполняемым стримом инициализации).
        /// </summary>
        protected virtual bool ExtendedInitialization { get; }

        /// <summary>
        /// Возвращает базовый адрес для подключения к серверу TESSA.
        /// Если свойство возвращает <see langword="null"/>, то используется значение из конфигурационного файла.
        /// </summary>
        protected virtual string BaseAddressOverride => null;

        /// <summary>
        /// Возвращает имя пользователя для подключения к серверу TESSA.
        /// Если свойство возвращает <see langword="null"/>, то используется значение из конфигурационного файла.
        /// </summary>
        protected virtual string UserNameOverride => null;

        /// <summary>
        /// Возвращает пароль пользователя для подключения к серверу TESSA.
        /// Если свойство возвращает <see langword="null"/>, то используется значение из конфигурационного файла.
        /// </summary>
        protected virtual string PasswordOverride => null;

        #endregion

        #region AssertThatAreEqual Protected Method

        /// <summary>
        /// Выполняет сравнение объекта карточки <see cref="Card"/> с объектами ролевой модели.
        /// </summary>
        /// <param name="card">Объект карточки.</param>
        /// <param name="sessionUserID">Идентификатор пользователя, выполнявшего сохранение карточки.</param>
        /// <param name="sessionUserName">Имя пользователя, выполнявшего сохранение карточки.</param>
        /// <param name="role">Объект, описывающий персональную роль.</param>
        /// <param name="deputies">Объекты, описывающие заместителей.</param>
        /// <param name="users">Объекты, описывающие состав роли.</param>
        protected static void AssertThatAreEqual(
            Card card,
            Guid sessionUserID,
            string sessionUserName,
            PersonalRole role,
            ICollection<RoleDeputyRecord> deputies,
            ICollection<RoleUserRecord> users)
        {
            PersonalRoleTestHelper.AssertPersonalRole(card, sessionUserID, sessionUserName, role);

            Assert.That(deputies.Count, Is.EqualTo(card.Sections["RoleDeputies"].Rows.Count));
            foreach (var deputy in deputies)
            {
                var deputyRow = card.Sections["RoleDeputies"].Rows.Single(x => x.RowID == deputy.RowID);
                AssertThatAreEqual(deputyRow, deputy);
            }

            Assert.That(users.Count, Is.EqualTo(card.Sections["RoleUsers"].Rows.Count));
            foreach (var user in users)
            {
                var userRow = card.Sections["RoleUsers"].Rows.Single(x => x.RowID == user.RowID);
                AssertThatAreEqual(userRow, user);
            }
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Возвращает объект <see cref="PersonalRole"/>, полученный для текущей сессии.
        /// </summary>
        /// <returns>Объект <see cref="PersonalRole"/>, полученный для текущей сессии.</returns>
        protected PersonalRole CreateSessionPersonalRole() =>
            PersonalRoleTestHelper.Create(this.Session.User.ID, this.Session.User.Name);

        /// <summary>
        /// Сохраняет содержимое встроенного ресурса во временный файл.
        /// </summary>
        /// <param name="resourceDirName">Путь к файлу в ресурсах манифеста относительно сборки <paramref name="assembly"/>.</param>
        /// <param name="fileName">Имя файла.</param>
        /// <param name="tempDir">Объект, представляющий временную папку.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        public Task CopyFileFromResourcesToTempDirAsync(
            string resourceDirName,
            string fileName,
            ITempFolder tempDir,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(tempDir, nameof(tempDir));
            Check.ArgumentNotNull(resourceDirName, nameof(resourceDirName));
            Check.ArgumentNotNull(fileName, nameof(fileName));

            return AssemblyHelper.SaveEmbeddedResourcesToFileAsync(
                this.ResourceAssembly,
                Path.Combine(resourceDirName, fileName),
                tempDir.AcquireFile(fileName).Path,
                cancellationToken);
        }

        #endregion

        #region Base override

        /// <inheritdoc/>
        protected override async ValueTask InitializeContainerAsync(IUnityContainer container)
        {
            await base.InitializeContainerAsync(container);

            await this.InitializeClientContainerAsync(
                container,
                this.DbFactory is null ? null : this.DbFactory.Create,
                this.DbScope,
                await this.GetBaseAddressAsync(),
                this.BeforeRegisterExtensionsOnClient,
                this.BeforeFinalizeClientRegistration);

            container
                .RegisterType<ICardLifecycleCompanionRequestExtender, CardLifecycleCompanionClientRequestExtender>(new ContainerControlledLifetimeManager())
                .RegisterType<IBusinessCalendarService, BusinessCalendarService>(new ContainerControlledLifetimeManager())
                .RegisterType<ICardLifecycleCompanionDependencies, CardLifecycleCompanionDependencies>(
                    nameof(ICardLifecycleCompanionDependencies),
                    new TransientLifetimeManager(),
                    new InjectionConstructor(
                        typeof(ICardRepository),
                        typeof(ICardMetadata),
                        typeof(Func<ICardFileManager>),
                        typeof(Func<ICardStreamClientRepository>),
                        typeof(ICardCache),
                        typeof(IDbScope),
                        typeof(ICardLifecycleCompanionRequestExtender)))
                .RegisterFactory<ICardLifecycleCompanionDependencies>(
                    static _ => new CardLifecycleCompanionDependenciesProxy(
                        static () => KrTestContext.CurrentContext.UnityContainer
                            .Resolve<ICardLifecycleCompanionDependencies>(nameof(ICardLifecycleCompanionDependencies))),
                    new ContainerControlledLifetimeManager())
                .RegisterType<TestConfigurationBuilder>(
                    new ContainerControlledLifetimeManager(),
                    new InjectionConstructor(
                        new InjectionParameter<Assembly>(this.ResourceAssembly),
                        typeof(IDbScope),
                        typeof(ICardManager),
                        typeof(ICardLibraryManager),
                        typeof(ICardRepository),
                        typeof(ICardFileSourceSettings),
                        typeof(ICardLifecycleCompanionDependencies),
                        typeof(IOperationRepository),
                        typeof(ICardTypeClientRepository),
                        typeof(ICardCachedMetadata),
                        typeof(IExchangeFormatInterpreter),
                        typeof(IIndentationStrategy),
                        typeof(ITessaViewService),
                        typeof(IJsonViewModelImporter),
                        typeof(IJsonViewModelConverter)))
                .RegisterType<IMessageProvider, TestMessageProvider>(new ContainerControlledLifetimeManager())
                .RegisterType<ITestSessionManager, TestSessionManager>(new ContainerControlledLifetimeManager());
        }

        /// <inheritdoc/>
        protected override async Task InitializeCoreAsync()
        {
            await base.InitializeCoreAsync();

            await this.OpenSessionCoreAsync();

            this.CardStreamRepository = this.UnityContainer.Resolve<ICardStreamClientRepository>();
            this.CardTypeRepository = this.UnityContainer.Resolve<ICardTypeClientRepository>();
        }

        /// <inheritdoc/>
        protected override async Task InitializeScopeCoreAsync()
        {
            await base.InitializeScopeCoreAsync();

            await this.OpenSessionCoreAsync();
        }

        /// <inheritdoc/>
        protected override async Task OneTimeTearDownCoreAsync()
        {
            // Выполняем явное закрытие сессии.
            await (this.UnityContainer
                ?.TryResolve<ISessionClient>()
                ?.CloseSessionSafeAsync() ?? Task.CompletedTask);

            await base.OneTimeTearDownCoreAsync();
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Выполняет действия перед поиском и выполнением клиентских регистраторов расширений в папке приложения.
        /// </summary>
        /// <param name="unityContainer">Unity-контейнер.</param>
        protected virtual void BeforeRegisterExtensionsOnClient(IUnityContainer unityContainer)
        {
        }

        /// <summary>
        /// Выполняет действия перед завершением регистрации клиента приложений.
        /// </summary>
        /// <param name="unityContainer">Unity-контейнер.</param>
        protected virtual void BeforeFinalizeClientRegistration(IUnityContainer unityContainer)
        {
        }

        /// <summary>
        /// Создаёт и конфигурирует контейнер Unity для выполнения тестов на стороне клиента с расширениями.
        /// Дополнительно инициализирует зависимости платформы.
        /// </summary>
        /// <param name="container">Unity-контейнер.</param>
        /// <param name="createDbManagerFunc">Функция, создающая и возвращающая <see cref="DbManager"/>. Если задано значение по умолчанию для типа, то перерегистрация не выполняется.</param>
        /// <param name="dbScope">Экземпляр объекта осуществляющий взаимодействие с базой данных. Если задано значение по умолчанию для типа, то используется <see cref="DbScope.CreateDefault"/>.</param>
        /// <param name="baseAddress">
        /// Базовый адрес сервера, который переопределяет соответствующую настройку, заданную в конфигурационном файле,
        /// или <c>null</c>, если используется адрес из конфигурационного файла.
        /// </param>
        /// <param name="beforeRegisterExtensionsOnClientAction">Метод выполняющий действия перед поиском и выполнением клиентских регистраторов расширений в папке приложения.</param>
        /// <param name="beforeFinalizeClientRegistrationAction">Метод выполняющий действия перед завершением регистрации клиента приложений.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Созданный контейнер.</returns>
        protected virtual ValueTask<IUnityContainer> InitializeClientContainerAsync(
            IUnityContainer container,
            Func<DbManager> createDbManagerFunc = null,
            IDbScope dbScope = null,
            string baseAddress = null,
            Action<IUnityContainer> beforeRegisterExtensionsOnClientAction = null,
            Action<IUnityContainer> beforeFinalizeClientRegistrationAction = null,
            CancellationToken cancellationToken = default)
        {
            return this.InitializeClientContainerBaseAsync(
                container,
                createDbManagerFunc,
                dbScope,
                baseAddress,
                beforeRegisterExtensionsOnClientAction,
                beforeFinalizeClientRegistrationAction,
                cancellationToken);
        }

        /// <summary>
        /// Создаёт и конфигурирует контейнер Unity для выполнения тестов на стороне клиента с расширениями.
        /// </summary>
        /// <param name="unityContainer">Unity-контейнер.</param>
        /// <param name="createDbManagerFunc">Функция, создающая и возвращающая <see cref="DbManager"/>. Если задано значение по умолчанию для типа, то перерегистрация не выполняется.</param>
        /// <param name="dbScope">Экземпляр объекта осуществляющий взаимодействие с базой данных. Если задано значение по умолчанию для типа, то используется <see cref="DbScope.CreateDefault"/>.</param>
        /// <param name="baseAddress">
        /// Базовый адрес сервера, который переопределяет соответствующую настройку, заданную в конфигурационном файле,
        /// или <c>null</c>, если используется адрес из конфигурационного файла.
        /// </param>
        /// <param name="beforeRegisterExtensionsOnClientAction">Метод выполняющий действия перед поиском и выполнением клиентских регистраторов расширений в папке приложения.</param>
        /// <param name="beforeFinalizeClientRegistrationAction">Метод выполняющий действия перед завершением регистрации клиента приложений.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Созданный контейнер.</returns>
        protected virtual ValueTask<IUnityContainer> InitializeClientContainerBaseAsync(
            IUnityContainer unityContainer,
            Func<DbManager> createDbManagerFunc = null,
            IDbScope dbScope = null,
            string baseAddress = null,
            Action<IUnityContainer> beforeRegisterExtensionsOnClientAction = null,
            Action<IUnityContainer> beforeFinalizeClientRegistrationAction = null,
            CancellationToken cancellationToken = default)
        {
            unityContainer
                .RegisterClientForConsole(baseAddress: baseAddress)
                ;
            return this.RegisterClientContainerBaseAsync(
                unityContainer,
                createDbManagerFunc,
                dbScope,
                beforeRegisterExtensionsOnClientAction,
                beforeFinalizeClientRegistrationAction,
                cancellationToken);
        }

        /// <summary>
        /// Конфигурирует контейнер Unity для выполнения тестов на стороне клиента с расширениями.
        /// </summary>
        /// <param name="unityContainer">Unity-контейнер.</param>
        /// <param name="createDbManagerFunc">Функция, создающая и возвращающая <see cref="DbManager"/>. Если задано значение по умолчанию для типа, то перерегистрация не выполняется.</param>
        /// <param name="dbScope">Экземпляр объекта осуществляющий взаимодействие с базой данных. Если задано значение по умолчанию для типа, то используется <see cref="DbScope.CreateDefault"/>.</param>
        /// <param name="beforeRegisterExtensionsOnClientAction">Метод выполняющий действия перед поиском и выполнением клиентских регистраторов расширений в папке приложения.</param>
        /// <param name="beforeFinalizeClientRegistrationAction">Метод выполняющий действия перед завершением регистрации клиента приложений.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Созданный контейнер.</returns>
        protected virtual async ValueTask<IUnityContainer> RegisterClientContainerBaseAsync(
            IUnityContainer unityContainer,
            Func<DbManager> createDbManagerFunc = null,
            IDbScope dbScope = null,
            Action<IUnityContainer> beforeRegisterExtensionsOnClientAction = null,
            Action<IUnityContainer> beforeFinalizeClientRegistrationAction = null,
            CancellationToken cancellationToken = default)
        {
            if (createDbManagerFunc is not null)
            {
                unityContainer
                    .RegisterDbManager(createDbManagerFunc);
            }

            unityContainer
                .RegisterInstance(dbScope ?? Platform.Data.DbScope.CreateDefault(), new ContainerControlledLifetimeManager())
                .SetCachingSourceForCardComponents();

            beforeRegisterExtensionsOnClientAction?.Invoke(unityContainer);

            var actualFoldersList = await unityContainer.FindAndRegisterExtensionsOnClientAsync(cancellationToken: cancellationToken);

            beforeFinalizeClientRegistrationAction?.Invoke(unityContainer);

            unityContainer
                .FinalizeClientRegistration(actualFoldersList);

            return unityContainer;
        }

        /// <summary>
        /// Возвращает базовый адрес подключения к серверу TESSA.
        /// </summary>
        /// <returns>Адрес сервера.</returns>
        protected virtual ValueTask<string> GetBaseAddressAsync()
        {
            var address = this.BaseAddressOverride ?? ConfigurationManager.Settings.TryGet<string>("BaseAddress");
            return new(address);
        }

        /// <summary>
        /// Открывает клиентскую сессию от имени пользователя с указанным паролем.
        /// </summary>
        /// <param name="login">Имя пользователя.</param>
        /// <param name="password">Пароль. Если имеет значение <see langword="null"/>, то считается, что он равен <paramref name="login"/>.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную операцию.</param>
        /// <returns>Объект, закрывающий текущую сессию по вызову <see cref="IAsyncDisposable.DisposeAsync"/>.</returns>
        protected async Task<IAsyncDisposable> OpenSessionAsync(
            string login,
            string password = null,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNullOrEmpty(login, nameof(login));

            var outerUnityContainer = this.UnityContainer;

            await this.CreateAndInitializeContainerAsync();

            await this.OpenSessionCoreAsync(
                login,
                password ?? login,
                cancellationToken);

            return new TestSessionScope(outerUnityContainer);
        }

        /// <summary>
        /// Инициализирует <see cref="TestSessionScope"/> без фактического открытия сессии.
        /// </summary>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную операцию.</param>
        /// <returns>Объект, закрывающий текущий скоуп без сессии по вызову <see cref="IAsyncDisposable.DisposeAsync"/>, после чего запросы будут выполняться в сессии из вышележащего скоупа или в сессии по умолчанию, если скоупа нет.</returns>
        protected async Task<IAsyncDisposable> WithoutSessionAsync(
            CancellationToken cancellationToken = default)
        {
            var outerUnityContainer = this.UnityContainer;

            await this.CreateAndInitializeContainerAsync();

            cancellationToken.ThrowIfCancellationRequested();

            return new TestSessionScope(outerUnityContainer);
        }

        #endregion

        #region Private Methods

        private Task OpenSessionCoreAsync(
            CancellationToken cancellationToken = default) =>
            this.OpenSessionCoreAsync(
                this.UserNameOverride,
                this.PasswordOverride,
                cancellationToken);

        private async Task OpenSessionCoreAsync(
            string userName,
            string password,
            CancellationToken cancellationToken = default)
        {
            var sessionManager = this.UnityContainer.Resolve<ITestSessionManager>();

            await sessionManager.OpenAsync(
                userName,
                password,
                this.ExtendedInitialization,
                cancellationToken);
        }

        private static void AssertThatAreEqual(CardRow deputyRow, RoleDeputyRecord deputy)
        {
            Assert.That((int) deputy.RoleType, Is.EqualTo(deputyRow["TypeID"]));
            Assert.That(deputy.DeputyID, Is.EqualTo(deputyRow["DeputyID"]));
            Assert.That(deputy.DeputyName, Is.EqualTo(deputyRow["DeputyName"]));
            Assert.That(deputy.MinDate.Date, Is.EqualTo(deputyRow.Get<DateTime>("MinDate").Date));
            Assert.That(deputy.MaxDate.Date, Is.EqualTo(deputyRow.Get<DateTime>("MaxDate").Date));
            Assert.That(deputy.IsActive, Is.EqualTo(deputyRow["IsActive"]));
            Assert.That(deputy.IsEnabled, Is.EqualTo(deputyRow["IsEnabled"]));
        }

        private static void AssertThatAreEqual(CardRow userRow, RoleUserRecord user)
        {
            Assert.That((int) user.RoleType, Is.EqualTo(userRow["TypeID"]));
            Assert.That(user.UserID, Is.EqualTo(userRow["UserID"]));
            Assert.That(user.UserName, Is.EqualTo(userRow["UserName"]));
            Assert.That(user.IsDeputy, Is.EqualTo(userRow["IsDeputy"]));
        }

        #endregion
    }
}
