using System;
using System.Threading.Tasks;
using LinqToDB.Data;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Server.Cards.Helpers;
using Tessa.Extensions.Shared.Extensions;
using Tessa.Extensions.Shared.Info;
using Tessa.Platform.Collections;
using Tessa.Platform.Data;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Server.Cards.Law
{
    /// <summary>
    ///     Серверное расширение на создание виртуальной карточки дела.
    /// </summary>
    public sealed class LawCaseNewExtension : CardNewExtension
    {
        private readonly IDbScope dbScope;
        private readonly LawCaseHelper lawCaseHelper;

        public LawCaseNewExtension(IDbScope dbScope,
            LawCaseHelper lawCaseHelper)
        {
            this.dbScope = dbScope;
            this.lawCaseHelper = lawCaseHelper;
        }

        /// <inheritdoc />
        public override async Task AfterRequest(ICardNewExtensionContext context)
        {
            if (!context.RequestIsSuccessful
                || context.Request.Info.ContainsKey(InfoMarks.IsGetRequest))
            {
                return;
            }

            var card = context.Response?.Card;
            if (card is null)
            {
                return;
            }

            // Копирование карточки
            if (context.Request.Info.ContainsKey(InfoMarks.SourceCardID))
            {
                var sourceCardID = context.Request.Info.Get<Guid>(InfoMarks.SourceCardID);
                await this.lawCaseHelper.FillCaseCardFieldsAsync(card, sourceCardID, context.ValidationResult, context.CancellationToken);
                card.Sections[SchemeInfo.LawCase].Fields[SchemeInfo.LawCase.Number] = string.Empty;

                if (card.Sections.TryGetValue(SchemeInfo.LawClients, out var clientsSection))
                {
                    clientsSection.Rows.ForEach(row =>
                    {
                        row.Fields[SchemeInfo.LawClients.ID] = null;
                    });
                }

                if (card.Sections.TryGetValue(SchemeInfo.LawPartners, out var partnersSection))
                {
                    partnersSection.Rows.ForEach(row =>
                    {
                        row.Fields[SchemeInfo.LawPartners.PartnerID] = Guid.NewGuid();
                        row.Fields[SchemeInfo.LawPartners.PartnerAddressID] = Guid.NewGuid();
                    });
                }

                if (card.Sections.TryGetValue(SchemeInfo.LawPartnerRepresentatives, out var partnerRepresentativesSection))
                {
                    partnerRepresentativesSection.Rows.ForEach(row =>
                    {
                        row.Fields[SchemeInfo.LawPartnerRepresentatives.RepresentativeID] = Guid.NewGuid();
                        row.Fields[SchemeInfo.LawPartnerRepresentatives.RepresentativeAddressID] = Guid.NewGuid();
                    });
                }

                return;
            }

            card.Sections[SchemeInfo.LawCase].Fields[SchemeInfo.LawCase.Date] = DateTime.Now;

            // Заполнение поля Users текущим пользователем
            var externalUid = await this.dbScope.GetFieldAsync<Guid?>(context.Session.User.ID,
                SchemeInfo.PersonalRoles,
                SchemeInfo.PersonalRoles.ExternalUid,
                cancellationToken: context.CancellationToken);

            if (externalUid is not null)
            {
                await using (this.dbScope.CreateNew(ExtSchemeInfo.ConnectionString))
                {
                    this.dbScope.Db.SetCommand(this.dbScope.BuilderFactory
                            .Select().C(ExtSchemeInfo.Uporabnik,
                                ExtSchemeInfo.Uporabnik.Uid,
                                ExtSchemeInfo.Uporabnik.Ime)
                            .From(ExtSchemeInfo.Uporabnik)
                            .Where().C(ExtSchemeInfo.Uporabnik.Uid).Equals().P(ExtSchemeInfo.Uporabnik.Uid)
                            .Build(),
                        new DataParameter(ExtSchemeInfo.Uporabnik.Uid, externalUid));

                    await using var reader = await this.dbScope.Db.ExecuteReaderAsync(context.CancellationToken);
                    while (await reader.ReadAsync(context.CancellationToken))
                    {
                        var users = card.Sections[SchemeInfo.LawUsers];
                        var row = users.Rows.Add();
                        row.RowID = Guid.NewGuid();
                        row.Fields[SchemeInfo.LawUsers.UserID] = reader.GetGuid(0);
                        row.Fields[SchemeInfo.LawUsers.UserName] = reader.GetString(1);
                        row.State = CardRowState.Inserted;
                    }
                }
            }
        }
    }
}
