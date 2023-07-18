using System;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Platform.Data;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Cards
{
    public sealed class PartnerContractStoreExtension : CardStoreExtension
    {
        #region Constants

        private const string NameField = "Name";

        private const string DepartmentField = "Department";

        #endregion

        #region Private Members

        private static Task<string> GetContactNameAsync(
            DbManager db,
            IQueryBuilderFactory builderFactory,
            Guid rowID,
            CancellationToken cancellationToken = default)
        {
            return db
                .SetCommand(
                    builderFactory
                        .Select().C("Name")
                        .From("PartnersContacts").NoLock()
                        .Where().C("RowID").Equals().P("RowID")
                        .Build(),
                    db.Parameter("RowID", rowID))
                .LogCommand()
                .ExecuteAsync<string>(cancellationToken);
        }

        private static Task<string> GetContactDepartmentAsync(
            DbManager db,
            IQueryBuilderFactory builderFactory,
            Guid rowID,
            CancellationToken cancellationToken = default)
        {
            return db
                .SetCommand(
                    builderFactory
                        .Select().C("Department")
                        .From("PartnersContacts").NoLock()
                        .Where().C("RowID").Equals().P("RowID")
                        .Build(),
                    db.Parameter("RowID", rowID))
                .LogCommand()
                .ExecuteAsync<string>(cancellationToken);
        }

        #endregion

        #region Base Overrides

        public override async Task BeforeRequest(ICardStoreExtensionContext context)
        {
            Card card;
            StringDictionaryStorage<CardSection> sections;
            ListStorage<CardRow> rows;
            if (context.CardType == null
                || (card = context.Request.TryGetCard()) == null
                || (sections = card.TryGetSections()) == null
                || !sections.TryGetValue("PartnersContacts", out CardSection section)
                || (rows = section.TryGetRows()) == null
                || rows.Count == 0)
            {
                return;
            }

            IDbScope dbScope = context.DbScope;
            await using (dbScope.Create())
            {
                foreach (CardRow row in rows)
                {
                    CardRowState state = row.State;
                    if (state != CardRowState.Inserted && state != CardRowState.Modified)
                    {
                        continue;
                    }

                    // если в строке нет ни имени, ни департамента, и строка изменяется, то интересующие нас поля просто не меняются;
                    // тогда мы подразумеваем, что их существующие значения в БД корректные, поэтому не выполняем проверок;
                    // если строка добавляется, но нужного поля в структуре нет, то при вставке поле получит значение по умолчанию, т.е. null
                    bool hasName = row.ContainsKey(NameField) || state == CardRowState.Inserted;
                    bool hasDepartment = row.ContainsKey(DepartmentField) || state == CardRowState.Inserted;
                    if (!hasName && !hasDepartment)
                    {
                        continue;
                    }

                    string name = hasName
                        ? row.TryGet<string>(NameField)
                        : await GetContactNameAsync(dbScope.Db, dbScope.BuilderFactory, row.RowID, context.CancellationToken);

                    // если имя не пустое, то независимо от департамента ошибки не будет
                    if (!string.IsNullOrWhiteSpace(name))
                    {
                        continue;
                    }

                    string department = hasDepartment
                        ? row.TryGet<string>(DepartmentField)
                        : await GetContactDepartmentAsync(dbScope.Db, dbScope.BuilderFactory, row.RowID, context.CancellationToken);

                    if (string.IsNullOrWhiteSpace(department))
                    {
                        context.ValidationResult.AddError(this, "$CardTypes_PartnersContacts_ValidationError");
                        break;
                    }
                }
            }
        }

        #endregion
    }
}