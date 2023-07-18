using System;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Platform.Validation;

namespace Tessa.Test.Default.Shared.Kr
{
    /// <summary>
    /// Предоставляет методы, выполняющие настройку лицензий (Правая панель -&gt; Настройки -&gt; Лицензия).
    /// </summary>
    public sealed class LicenseConfigurator :
        CardLifecycleCompanion<LicenseConfigurator>,
        IConfiguratorScopeManager<TestConfigurationBuilder>
    {
        #region Fields

        private readonly ConfiguratorScopeManager<TestConfigurationBuilder> configuratorScopeManager;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="LicenseConfigurator"/> и планирует инициализацию карточки которой выполняется управление.
        /// </summary>
        /// <param name="deps">Зависимости, используемые при взаимодействии с карточками.</param>
        public LicenseConfigurator(
            ICardLifecycleCompanionDependencies deps)
            : this(deps, default)
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="LicenseConfigurator"/> и планирует инициализацию карточки которой выполняется управление.
        /// </summary>
        /// <param name="deps">Зависимости, используемые при взаимодействии с карточками.</param>
        /// <param name="scope">Конфигуратор верхнего уровня.</param>
        public LicenseConfigurator(
            ICardLifecycleCompanionDependencies deps,
            TestConfigurationBuilder scope)
            : base(CardHelper.LicenseTypeID, CardHelper.LicenseTypeName, deps)
        {
            this.configuratorScopeManager = new ConfiguratorScopeManager<TestConfigurationBuilder>(scope);
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Добавляет указанного пользователя в список "Мобильное согласование".
        /// </summary>
        /// <param name="userID">Идентификатор пользователя.</param>
        /// <param name="userName">Имя пользователя.</param>
        /// <returns>Объект <see cref="ServerConfigurator"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public LicenseConfigurator WithMobileUser(
            Guid userID,
            string userName)
        {
            this.AddPendingAction(
                new PendingAction(
                    nameof(ServerConfigurator) + "." + nameof(WithMobileUser),
                    (_, ct) => new ValueTask<ValidationResult>(this.AddUserToLicenseSection("MobileLicenses", userID, userName))));
            return this;
        }

        /// <summary>
        /// Добавляет указанного пользователя в список "Персональные лицензии".
        /// </summary>
        /// <param name="userID">Идентификатор пользователя.</param>
        /// <param name="userName">Имя пользователя.</param>
        /// <returns>Объект <see cref="ServerConfigurator"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public LicenseConfigurator WithPersonalUser(
            Guid userID,
            string userName)
        {
            this.AddPendingAction(
                new PendingAction(
                    nameof(ServerConfigurator) + "." + nameof(this.WithPersonalUser),
                    (_, ct) => new ValueTask<ValidationResult>(this.AddUserToLicenseSection("PersonalLicenses", userID, userName))));
            return this;
        }

        #endregion

        #region IConfiguratorScopeManager<TestConfigurationBuilder> Members

        /// <inheritdoc/>
        public TestConfigurationBuilder Complete() => this.configuratorScopeManager.Complete();

        #endregion

        #region Private methods

        private ValidationResult AddUserToLicenseSection(
            string sectionName,
            Guid userID,
            string userName)
        {
            var newRow = this.GetCardOrThrow().Sections[sectionName].Rows.Add();
            newRow.RowID = Guid.NewGuid();
            newRow.State = CardRowState.Inserted;
            newRow.Fields["UserID"] = userID;
            newRow.Fields["UserName"] = userName;
            return ValidationResult.Empty;
        }

        #endregion
    }
}
