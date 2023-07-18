using System;
using System.Collections.Concurrent;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Platform.Data;

namespace Tessa.Extensions.Default.Chronos.Notices
{
    public sealed class OutboxManager : IOutboxManager
    {
        #region Fields

        private readonly IDbScope dbScope;

        #endregion

        #region Constructors

        public OutboxManager(IDbScope dbScope) =>
            this.dbScope = dbScope ?? throw new ArgumentNullException(nameof(dbScope));

        #endregion

        #region IOutboxManager Members

        public async Task<ConcurrentQueue<OutboxMessage>> GetTopMessagesAsync(
            int topCount,
            int retryIntervalMinutes,
            CancellationToken cancellationToken = default)
        {
            await using (this.dbScope.Create())
            {
                var db = this.dbScope.Db;
                var builderFactory = this.dbScope.BuilderFactory;

                DateTime maxLastErrorDateToProcess = DateTime.UtcNow - TimeSpan.FromMinutes(retryIntervalMinutes);

                var result = new ConcurrentQueue<OutboxMessage>();
                await using (DbDataReader reader = await db
                    .SetCommand(
                        builderFactory
                            .Select().Top(topCount).C(null, "ID", "Email", "Subject", "Body", "Attempts", "Info")
                            .From("Outbox").NoLock()
                            .Where().C("LastErrorDate").IsNull()
                            .Or().C("LastErrorDate").LessOrEquals().P("MaxLastErrorDateToProcess")
                            .OrderBy("Created")
                            .Limit(topCount)
                            .Build(),
                        db.Parameter("MaxLastErrorDateToProcess", maxLastErrorDateToProcess))
                    .LogCommand()
                    .ExecuteReaderAsync(CommandBehavior.SequentialAccess, cancellationToken))
                {
                    while (await reader.ReadAsync(cancellationToken))
                    {
                        result.Enqueue(
                            new OutboxMessage
                            {
                                ID = reader.GetGuid(0),
                                Email = reader.GetValue<string>(1),
                                Subject = reader.GetValue<string>(2),
                                Body = reader.GetValue<string>(3),
                                Attempts = reader.GetInt32(4),
                                Info = await reader.GetSequentialNullableStringAsync(5, cancellationToken: cancellationToken)
                            });
                    }
                }

                return result;
            }
        }


        public async Task MarkAsBadMessageAsync(
            Guid id,
            long attemptNum,
            string exceptionMessage,
            CancellationToken cancellationToken = default)
        {
            await using (this.dbScope.Create())
            {
                var db = this.dbScope.Db;
                var builderFactory = this.dbScope.BuilderFactory;

                await db
                    .SetCommand(
                        builderFactory
                            .Update("Outbox")
                            .C("Attempts").Assign().P("Attempts")
                            .C("LastErrorDate").Assign().P("LastErrorDate")
                            .C("LastErrorText").Assign().P("LastErrorText")
                            .Where().C("ID").Equals().P("ID")
                            .Build(),
                        db.Parameter("Attempts", attemptNum),
                        db.Parameter("LastErrorDate", DateTime.UtcNow),
                        db.Parameter("LastErrorText", SqlHelper.LimitString(exceptionMessage, 256)),
                        db.Parameter("ID", id))
                    .LogCommand()
                    .ExecuteNonQueryAsync(cancellationToken);
            }
        }


        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            await using (this.dbScope.Create())
            {
                var db = this.dbScope.Db;
                var builderFactory = this.dbScope.BuilderFactory;

                await db
                    .SetCommand(
                        builderFactory
                            .DeleteFrom("Outbox")
                            .Where().C("ID").Equals().P("ID")
                            .Build(),
                        db.Parameter("ID", id))
                    .LogCommand()
                    .ExecuteNonQueryAsync(cancellationToken);
            }
        }

        #endregion
    }
}