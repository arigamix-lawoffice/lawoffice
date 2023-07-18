using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;
using Tessa.Platform.Data;
using Tessa.Roles.NestedRoles;

namespace Tessa.Extensions.Default.Server.Roles
{
    ///<inheritdoc/>
    public sealed class NestedRoleContextSelector : INestedRoleContextSelector
    {
        #region Fields

        private readonly IDbScope dbScope;
        private readonly IKrTypesCache krTypesCache;

        #endregion

        #region Constructors

        public NestedRoleContextSelector(
            IDbScope dbScope,
            IKrTypesCache krTypesCache)
        {
            Check.ArgumentNotNull(dbScope, nameof(dbScope));
            Check.ArgumentNotNull(krTypesCache, nameof(krTypesCache));

            this.dbScope = dbScope;
            this.krTypesCache = krTypesCache;
        }

        #endregion

        #region INestedRoleContextSelector Implementation

        ///<inheritdoc/>
        public async ValueTask<Guid?> GetContextAsync(Card card, CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(card, nameof(card));

            return await KrProcessSharedHelper.GetDocTypeIDAsync(
                card,
                this.dbScope,
                cancellationToken) ?? card.TypeID;
        }

        ///<inheritdoc/>
        public async ValueTask<Guid?> GetContextAsync(Guid cardID, CancellationToken cancellationToken = default)
        {
            await using (this.dbScope.Create())
            {
                var db = this.dbScope.Db;
                var builder = this.dbScope.BuilderFactory;

                return await db.SetCommand(
                    builder
                        .Select().Coalesce(b => b.C(null, "DocTypeID", "CardTypeID")).From("DocumentCommonInfo").NoLock()
                        .Where().C("ID").Equals().P("CardID")
                        .Build(),
                    db.Parameter("CardID", cardID))
                    .LogCommand()
                    .ExecuteAsync<Guid?>(cancellationToken);
            }
        }

        ///<inheritdoc/>
        public async ValueTask<Dictionary<Guid, IList<Guid>>> GetContextsAsync(
            ICollection<Guid> cardIDs,
            CancellationToken cancellationToken = default)
        {
            await using (this.dbScope.Create())
            {
                var db = this.dbScope.Db;
                var builder = this.dbScope.BuilderFactory;

                db.SetCommand(
                    builder
                        .Select().C("ID").Coalesce(b => b.C(null, "DocTypeID", "CardTypeID")).From("DocumentCommonInfo").NoLock()
                        .Where().C("ID").InArray(cardIDs, nameof(cardIDs), out var dpIDs)
                        .Build(), DataParameters.Get(dpIDs))
                    .LogCommand();

                var result = new Dictionary<Guid, IList<Guid>>();
                await using var reader = await db.ExecuteReaderAsync(cancellationToken);
                while (await reader.ReadAsync(cancellationToken))
                {
                    var ownerID = reader.GetNullableGuid(1);
                    if (ownerID.HasValue)
                    {
                        if (result.TryGetValue(ownerID.Value, out var list))
                        {
                            list.Add(reader.GetGuid(0));
                        }
                        else
                        {
                            result[ownerID.Value] = new List<Guid> { reader.GetGuid(0) };
                        }
                    }
                }

                return result;
            }
        }

        ///<inheritdoc/>
        public async ValueTask<string> GetContextNameAsync(Guid contextID, CancellationToken cancellationToken = default)
        {
            var types = await this.krTypesCache.GetTypesAsync(cancellationToken);
            if (types is not null)
            {
                foreach (var type in types)
                {
                    if (type.ID == contextID)
                    {
                        return type.Name;
                    }
                }
            }

            return null;
        }

        #endregion
    }
}
