using System;
using System.Threading;
using System.Threading.Tasks;
using Tessa.BusinessCalendar;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared;

namespace Tessa.Test.Default.Shared.Kr
{
    /// <summary>
    /// Базовый абстрактный класс для серверных тестов, выполняющий настройку тестовой базы данных для типового решения и маршрутов.
    /// </summary>
    public abstract class KrServerTestBase :
        ServerTestBase,
        IKrTest,
        IImportObjects
    {
        #region Properties

        /// <summary>
        /// Возвращает число календарных дней прибавляемых к текущей дате при вычислении правой границы диапазона расчёта календаря.
        /// </summary>
        public virtual double CalendarDateEndOffset { get; } = TestHelper.DefaultCalendarDateEndOffset;

        #endregion

        #region Base override

        /// <inheritdoc/>
        protected override async Task InitializeScopeCoreAsync()
        {
            await base.InitializeScopeCoreAsync();

            await this.InitializeAsync();
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
                .CreateOrLoadSingleton()
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
            await this.TestConfigurationBuilder
                .ImportAllTypes()
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

            if (!this.IsImportCards
                || !await TestHelper.ExistsValuesAsync(this.DbScope, "KrDocType"))
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
        }

        #endregion
    }
}
