using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Platform.Data;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Cards
{
    /// <summary>
    /// Расширение, проверяющее корректность сохраняемой карточки договора.
    /// </summary>
    public sealed class ContractStoreExtension :
        CardStoreExtension
    {
        #region Private Methods

        private static Task<bool> GetHasAmountAsync(
            Guid cardID,
            DbManager db,
            IQueryBuilderFactory builderFactory,
            CancellationToken cancellationToken = default)
        {
            return db
                .SetCommand(
                    builderFactory
                        .Select().V(true)
                        .From("DocumentCommonInfo").NoLock()
                        .Where().C("ID").Equals().P("ID")
                            .And().C("Amount").IsNotNull()
                        .Build(),
                    db.Parameter("ID", cardID))
                .LogCommand()
                .ExecuteAsync<bool>(cancellationToken);
        }


        private static Task<bool> GetHasCurrencyAsync(
            Guid cardID,
            DbManager db,
            IQueryBuilderFactory builderFactory,
            CancellationToken cancellationToken = default)
        {
            return db
                .SetCommand(
                    builderFactory
                        .Select().V(true)
                        .From("DocumentCommonInfo").NoLock()
                        .Where().C("ID").Equals().P("ID")
                            .And().C("CurrencyID").IsNotNull()
                        .Build(),
                    db.Parameter("ID", cardID))
                .LogCommand()
                .ExecuteAsync<bool>(cancellationToken);
        }

        #endregion

        #region Base Overrides

        public override async Task AfterBeginTransaction(ICardStoreExtensionContext context)
        {
            Card card = context.Request.TryGetCard();
            StringDictionaryStorage<CardSection> sections;
            if (card == null
                || (sections = card.TryGetSections()) == null
                || !sections.TryGetValue("DocumentCommonInfo", out CardSection commonInfo))
            {
                return;
            }

            Dictionary<string, object> fields = commonInfo.RawFields;

            // если оба поля отсутствуют в структуре сохраняемой карточки, то не выполняем проверку - поля не изменялись
            bool newCardMode = card.StoreMode == CardStoreMode.Insert;
            bool hasAmountField = newCardMode || fields.ContainsKey("Amount");
            bool hasCurrencyField = newCardMode || fields.ContainsKey("CurrencyID");
            if (!hasAmountField && !hasCurrencyField)
            {
                return;
            }

            var db = context.DbScope.Db;
            var builderFactory = context.DbScope.BuilderFactory;

            bool hasAmount = hasAmountField
                ? fields.TryGet<object>("Amount") != null
                : await GetHasAmountAsync(card.ID, db, builderFactory, context.CancellationToken);

            bool hasCurrency = hasCurrencyField
                ? fields.TryGet<object>("CurrencyID") != null
                : await GetHasCurrencyAsync(card.ID, db, builderFactory, context.CancellationToken);

            if (hasAmount && !hasCurrency
                || !hasAmount && hasCurrency)
            {
                context.ValidationResult.AddError(this, "$Error_AmountAndCurrencyAreInvalid");
            }
        }

        #endregion
    }
}
