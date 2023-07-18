using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Caching;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;
using Tessa.Platform.Data;
using Tessa.Platform.Formatting;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Acquaintance
{
    /// <summary>
    /// Расширение, которое помечает документ, как тот, с которым ознакомились, если нужно.
    /// </summary>
    public sealed class AcquaintanceGetExtension : CardGetExtension
    {
        #region AcquaintanceRow Private Class

        private sealed class AcquaintanceRow
        {
            public Guid? CommentID { get; set; }

            public string SenderName { get; set; }

            public DateTime? Sent { get; set; }
        }

        #endregion

        #region Fields

        private readonly ICardCache cardCache;

        #endregion

        #region Constructors

        public AcquaintanceGetExtension(ICardCache cardCache)
        {
            this.cardCache = cardCache;
        }

        #endregion

        #region Base Overrides

        public override async Task AfterRequest(ICardGetExtensionContext context)
        {
            Guid? cardID;

            if (context.Request.ServiceType == CardServiceType.Default
                || !context.RequestIsSuccessful
                || context.CardType == null
                || context.CardType.InstanceType != CardInstanceType.Card
                || !(cardID = context.Request.CardID).HasValue)
            {
                return;
            }

            KrComponents used = await KrComponentsHelper.GetKrComponentsAsync(context.CardType.ID, this.cardCache, context.CancellationToken);
            if (used.HasNot(KrComponents.Base))
            {
                return;
            }

            await using (context.DbScope.Create())
            {
                var db = context.DbScope.Db;
                var query = context.DbScope.BuilderFactory;

                db
                    .SetCommand(
                        query.Cached(this, "SelectRows", static builderFactory => 
                                builderFactory
                                    .Select().C(null, "CommentID", "SenderName", "Sent")
                                    .From("AcquaintanceRows").NoLock()
                                    .Where().C("IsReceived").Equals().V(false)
                                    .And().C("UserID").Equals().P("UserID")
                                    .And().C("CardID").Equals().P("CardID")
                                    .OrderBy("Sent")
                                    .Build()),
                        db.Parameter("CardID", context.Request.CardID),
                        db.Parameter("UserID", context.Session.User.ID))
                    .LogCommand();

                var rows = new List<AcquaintanceRow>();
                await using (DbDataReader reader = await db.ExecuteReaderAsync(context.CancellationToken))
                {
                    while (await reader.ReadAsync(context.CancellationToken))
                    {
                        rows.Add(
                            new AcquaintanceRow
                            {
                                CommentID = reader.GetNullableGuid(0),
                                SenderName = reader.GetString(1),
                                Sent = reader.GetDateTimeUtc(2),
                            });
                    }
                }

                if (rows.Count > 0)
                {
                    var acquaintanceResult = new ValidationResultBuilder();

                    foreach (AcquaintanceRow row in rows)
                    {
                        string sendDateText = FormattingHelper.FormatDateTimeWithoutSeconds(row.Sent);

                        string comment = row.CommentID.HasValue
                            ? (await db
                                .SetCommand(
                                    query
                                        .Select().C("Comment")
                                        .From("AcquaintanceComments").NoLock()
                                        .Where().C("ID").Equals().P("ID")
                                        .Build(),
                                    db.Parameter("ID", row.CommentID.Value))
                                .LogCommand()
                                .ExecuteAsync<string>(context.CancellationToken))
                                ?.Trim()
                            : null;

                        if (string.IsNullOrEmpty(comment))
                        {
                            acquaintanceResult.AddInfo(this, "$KrMessages_Acquaintance_Received", row.SenderName, sendDateText);
                        }
                        else
                        {
                            acquaintanceResult.AddInfo(this, "$KrMessages_Acquaintance_ReceivedAndCommented", row.SenderName, sendDateText, comment);
                        }
                    }

                    await db
                        .SetCommand(
                            query
                                .Update("AcquaintanceRows")
                                .C("IsReceived").Assign().V(true)
                                .C("Received").Assign().P("Received")
                                .Where().C("IsReceived").Equals().V(false)
                                .And().C("UserID").Equals().P("UserID")
                                .And().C("CardID").Equals().P("CardID")
                                .Build(),
                            db.Parameter("Received", DateTime.UtcNow),
                            db.Parameter("CardID", cardID.Value),
                            db.Parameter("UserID", context.Session.User.ID))
                        .LogCommand()
                        .ExecuteNonQueryAsync(context.CancellationToken);

                    context.ValidationResult.Add(acquaintanceResult);
                }
            }
        }

        #endregion
    }
}
