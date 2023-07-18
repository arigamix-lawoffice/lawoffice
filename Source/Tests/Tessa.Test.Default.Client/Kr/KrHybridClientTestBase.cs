using System;
using System.Threading;
using System.Threading.Tasks;
using Tessa.BusinessCalendar;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared;
using Tessa.Platform.Data;
using Tessa.Test.Default.Shared;
using Tessa.Test.Default.Shared.Kr;
using Unity;

namespace Tessa.Test.Default.Client.Kr
{
    /// <summary>
    /// Базовый абстрактный класс для клиентских тестов с поддержкой типового решения, 
    /// маршрутов и без поддержки пользовательского интерфейса, выполняющихся на специально подготовленном сервере приложений.
    /// </summary>
    public abstract class KrHybridClientTestBase :
        HybridClientTestBase,
        IKrTest,
        IImportObjects
    {
        #region Properties

        /// <summary>
        /// Возвращает число календарных дней прибавляемых к текущей дате при вычислении правой границы диапазона расчёта календаря.
        /// </summary>
        public virtual double CalendarDateEndOffset { get; } = TestHelper.DefaultCalendarDateEndOffset;

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        protected override async Task InitializeScopeCoreAsync()
        {
            await this.InitializeAsync();

            await base.InitializeScopeCoreAsync();
        }

        #endregion

        #region IKrTest Members

        /// <inheritdoc/>
        public virtual Guid TestCardTypeID => DefaultCardTypes.ContractTypeID;

        /// <inheritdoc/>
        public virtual string TestCardTypeName => DefaultCardTypes.ContractTypeName;

        /// <inheritdoc/>
        public virtual Guid TestDocTypeID { get; } = new Guid(0x93A392E7, 0x097C, 0x4420, 0x85, 0xC4, 0xDB, 0x10, 0xB2, 0xDF, 0x3C, 0x1D);

        /// <inheritdoc/>
        public virtual string TestDocTypeName => "Contract";

        /// <inheritdoc/>
        public CardLifecycleCompanion CreateCardLifecycleCompanion(
            Guid? id = null) =>
            new CardLifecycleCompanion(
                id ?? Guid.NewGuid(),
                this.TestCardTypeID,
                this.TestCardTypeName,
                this.CardLifecycleDependencies);

        #endregion

        #region IImportObjects Members

        /// <inheritdoc/>
        public virtual bool IsImportCards => true;

        /// <inheritdoc/>
        public virtual bool IsImportFileTemplateCards => true;

        /// <inheritdoc/>
        public virtual bool IsImportViews => true;

        /// <inheritdoc/>
        public virtual async Task ImportCardsAsync()
        {
            await this.TestConfigurationBuilder
                .GetServerConfigurator()
                .Create()
                .InitializeServerInstance(await this.GetFileStoragePathAsync())
                .Save()
                .GoAsync();

            await this.TestConfigurationBuilder
                .If(this.IsImportCards, c => c.ImportCardsWithTessaCardLib(this.ImportCardPredicateAsync))
                .If(this.IsImportFileTemplateCards, c => c.ImportCardsWithFileTemplatesCardLib(this.ImportFileTemplateCardPredicateAsync))
                .GoAsync()
                ;
        }

        /// <inheritdoc/>
        public virtual ValueTask<bool> ImportCardPredicateAsync(Card card, CancellationToken cancellationToken = default) =>
            new ValueTask<bool>(card.TypeID != CardHelper.ServerInstanceTypeID);

        /// <inheritdoc/>
        public virtual ValueTask<bool> ImportFileTemplateCardPredicateAsync(Card card, CancellationToken cancellationToken = default) =>
            new ValueTask<bool>(true);

        #endregion

        #region Private Methods

        private async Task InitializeAsync()
        {
            // Выполняем импорт карточек перед инициализацией клиентских тестов.
            var sessionManager = this.UnityContainer.Resolve<ITestSessionManager>();

            await sessionManager.OpenAsync(this.UserNameOverride, this.PasswordOverride);

            await this.TestConfigurationBuilder
                .ImportAllTypes()
                .CardMetadataCacheInvalidate()
                .GoAsync();

            await this.ImportCardsAsync();

            await this.TestConfigurationBuilder
                .If(this.IsImportViews, c => c.ImportViewsFromDirectory(importRoles: true))
                .GoAsync();

            if (!this.IsImportCards
                || !await TestHelper.ExistsValuesAsync(this.DbScope, TimeZonesHelper.DefaultTimeZoneSection)
                && !await TestHelper.ExistsValuesAsync(this.DbScope, TimeZonesHelper.TimeZonesEnumSection))
            {
                await this.TestConfigurationBuilder
                    .ConfigureTimeZones()
                    .GoAsync();
            }

            await this.TestConfigurationBuilder
                .BuildCalendar(dateEndOffset: this.CalendarDateEndOffset)
                .GoAsync();

            if (this.IsImportCards
                && await TestHelper.ExistsValuesAsync(this.DbScope, "KrDocType"))
            {
                await this.CardRepository.InvalidateCacheAsync(new[] { DefaultCacheNames.KrTypes });
            }
            else
            {
                await this.TestConfigurationBuilder
                    .GetKrDocTypesConfigurator()
                    .GetDocTypeCard(
                        this.TestDocTypeID,
                        cardTypeID: this.TestCardTypeID)
                    .UseApproving()
                    .UseRegistration()
                    .UseResolutions()
                    .Complete()
                    .GoAsync();
            }

            if (!this.IsImportCards)
            {
                await this.TestConfigurationBuilder
                    .GetKrSettingsConfigurator()
                    .CreateOrLoadSingleton()
                    .GetCardTypeConfigurator(this.TestCardTypeID)
                    .UseDocTypes()
                    .UseApproving()
                    .UseRegistration()
                    .UseResolutions()
                    .Complete()
                    .Complete()
                    .GoAsync();
            }

            await sessionManager.CloseAsync();
        }

        #endregion
    }
}
