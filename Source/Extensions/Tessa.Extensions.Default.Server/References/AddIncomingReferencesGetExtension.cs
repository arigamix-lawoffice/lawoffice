using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Platform;
using Tessa.Platform.Data;
using Tessa.Platform.Formatting;
using Tessa.Platform.Storage;
using Tessa.Properties.Resharper;

namespace Tessa.Extensions.Default.Server.References
{
    public sealed class AddIncomingReferencesGetExtension : CardGetExtension
    {
        public override async Task AfterRequest(ICardGetExtensionContext context)
        {
            Card card;
            StringDictionaryStorage<CardSection> sections;

            if (!context.RequestIsSuccessful
                || (card = context.Response.TryGetCard()) == null
                || (sections = card.TryGetSections()) == null
                || !sections.TryGetValue("IncomingRefDocs", out CardSection incomingRefDocs))
            {
                return;
            }

            await using (context.DbScope.Create())
            {
                var db = context.DbScope.Db;
                var builderFactory = context.DbScope.BuilderFactory;

                List<Document> docs = await db
                    .SetCommand(
                        builderFactory.Cached(this, "SelectRows", static builderFactory =>
                            builderFactory
                                .Select().C("dci", "ID", "FullNumber", "Subject", "PartnerName", "DocDate")
                                .From("OutgoingRefDocs", "ord").NoLock()
                                .InnerJoin("DocumentCommonInfo", "dci").NoLock()
                                .On().C("dci", "ID").Equals().C("ord", "ID")
                                .Where().C("ord", "DocID").Equals().P("DocID")
                                .OrderBy("dci", "FullNumber")
                                .Build()),
                        db.Parameter("DocID", card.ID))
                    .LogCommand()
                    .ExecuteListAsync<Document>(context.CancellationToken);

                if (docs.Count > 0)
                {
                    ListStorage<CardRow> rows = incomingRefDocs.Rows;

                    foreach (Document doc in docs)
                    {
                        CardRow row = rows.Add();
                        row.RowID = Guid.NewGuid();
                        row["DocID"] = doc.ID;

                        StringBuilder description = StringBuilderHelper.Acquire()
                            .Append(doc.FullNumber)
                            .Append(", ")
                            .Append(doc.Subject);

                        if (!string.IsNullOrWhiteSpace(doc.PartnerName))
                        {
                            description
                                .Append(", ")
                                .Append(doc.PartnerName.Trim());
                        }

                        if (doc.DocDate.HasValue)
                        {
                            description
                                .Append(", ")
                                .Append(FormattingHelper.FormatDate(doc.DocDate.Value, convertToLocal: false));
                        }

                        row["DocDescription"] = description.ToStringAndRelease();
                        row.State = CardRowState.None;
                    }
                }
            }
        }

        [UsedImplicitly]
        private class Document
        {
            public Guid ID { get; [UsedImplicitly] set; }
            public string FullNumber { get; [UsedImplicitly] set; }
            public string Subject { get; [UsedImplicitly] set; }
            public string PartnerName { get; [UsedImplicitly] set; }
            public DateTime? DocDate { get; [UsedImplicitly] set; }
        }
    }
}
