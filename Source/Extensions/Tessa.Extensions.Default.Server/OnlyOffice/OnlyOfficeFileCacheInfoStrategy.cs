#nullable enable

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LinqToDB;
using NLog;
using Tessa.Cards;
using Tessa.Platform;
using Tessa.Platform.Data;
using Tessa.Platform.Formatting;

namespace Tessa.Extensions.Default.Server.OnlyOffice
{
    /// <inheritdoc />
    public sealed class OnlyOfficeFileCacheInfoStrategy :
        IOnlyOfficeFileCacheInfoStrategy
    {
        #region Constructors

        /// <summary>
        /// Создаёт экземпляр класса с указанием его зависимостей.
        /// </summary>
        /// <param name="dbScope">Объект, посредством которого выполняется взаимодействие с базой данных.</param>
        public OnlyOfficeFileCacheInfoStrategy(IDbScope dbScope) =>
            this.dbScope = dbScope ?? throw new ArgumentNullException(nameof(dbScope));

        #endregion

        #region Fields

        private readonly IDbScope dbScope;

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        #endregion

        #region IOnlyOfficeFileCacheInfoStrategy Members

        /// <inheritdoc />
        public async Task InsertAsync(IOnlyOfficeFileCacheInfo info, CancellationToken cancellationToken = default)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            await using var _ = this.dbScope.Create();

            IQueryExecutor executor = this.dbScope.Executor;

            await executor.ExecuteNonQueryAsync(
                this.dbScope.BuilderFactory
                    .InsertInto("OnlyOfficeFileCache",
                        "ID",
                        "SourceFileVersionID",
                        "CreatedByID",
                        "SourceFileName",
                        "ModifiedFileUrl",
                        "LastModifiedFileUrlTime",
                        "LastAccessTime",
                        "HasChangesAfterClose",
                        "CoeditKey")
                    .Values(b => b.P(
                        "ID",
                        "SourceFileVersionID",
                        "CreatedByID",
                        "SourceFileName",
                        "ModifiedFileUrl",
                        "LastModifiedFileUrlTime",
                        "LastAccessTime",
                        "HasChangesAfterClose",
                        "CoeditKey"))
                    .Build(),
                cancellationToken,
                executor.Parameter("ID", info.ID, DataType.Guid),
                executor.Parameter("SourceFileVersionID", info.SourceFileVersionID, DataType.Guid),
                executor.Parameter("CreatedByID", info.CreatedByID, DataType.Guid),
                executor.Parameter("SourceFileName", SqlHelper.LimitString(info.SourceFileName, CardHelper.FileNameMaxLength), DataType.NVarChar),
                executor.Parameter("ModifiedFileUrl", info.ModifiedFileUrl, DataType.NVarChar),
                executor.Parameter("LastModifiedFileUrlTime", info.LastModifiedFileUrlTime, DataType.DateTime),
                executor.Parameter("LastAccessTime", info.LastAccessTime, DataType.DateTime),
                executor.Parameter("HasChangesAfterClose", info.HasChangesAfterClose, DataType.Boolean),
                executor.Parameter("CoeditKey", info.CoeditKey, DataType.NVarChar));
        }


        /// <inheritdoc />
        public async Task<IOnlyOfficeFileCacheInfo?> TryGetInfoAsync(Guid id, CancellationToken cancellationToken = default)
        {
            await using var _ = this.dbScope.Create();

            OnlyOfficeFileCacheInfo? result = null;
            var lastAccessTime = DateTime.UtcNow;

            DbManager db = this.dbScope.Db;

            await using (DbDataReader reader = await db
                .SetCommand(
                    this.dbScope.BuilderFactory
                        .Select().C(null,
                            "SourceFileVersionID",
                            "CreatedByID",
                            "SourceFileName",
                            "ModifiedFileUrl",
                            "LastModifiedFileUrlTime",
                            "HasChangesAfterClose",
                            "EditorWasOpen")
                        .From("OnlyOfficeFileCache").NoLock()
                        .Where().C("ID").Equals().P("ID")
                        .Build(),
                    db.Parameter("ID", id, DataType.Guid))
                .LogCommand()
                .ExecuteReaderAsync(cancellationToken))
            {
                if (await reader.ReadAsync(cancellationToken))
                {
                    result = new OnlyOfficeFileCacheInfo
                    {
                        ID = id,
                        SourceFileVersionID = reader.GetGuid(0),
                        CreatedByID = reader.GetGuid(1),
                        SourceFileName = reader.GetString(2),
                        ModifiedFileUrl = reader.GetNullableString(3),
                        LastModifiedFileUrlTime = reader.GetNullableDateTime(4),
                        HasChangesAfterClose = reader.GetNullableBoolean(5),
                        EditorWasOpen = reader.GetBoolean(6),
                    };
                }
            }

            if (result is null)
            {
                return null;
            }

            await db
                .SetCommand(
                    this.dbScope.BuilderFactory
                        .Update("OnlyOfficeFileCache")
                        .C("LastAccessTime").Assign().P("LastAccessTime")
                        .Where().C("ID").Equals().P("ID")
                        .Build(),
                    db.Parameter("ID", id, DataType.Guid),
                    db.Parameter("LastAccessTime", lastAccessTime, DataType.DateTime))
                .LogCommand()
                .ExecuteNonQueryAsync(cancellationToken);

            result.LastAccessTime = lastAccessTime;
            return result;
        }


        /// <inheritdoc />
        public async Task UpdateInfoAsync(
            Guid id,
            string? newModifiedFileUrl,
            bool? hasChangesAfterClose,
            CancellationToken cancellationToken = default)
        {
            await using var _ = this.dbScope.Create();

            DbManager db = this.dbScope.Db;

            var builder = this.dbScope.BuilderFactory
                .Update("OnlyOfficeFileCache")
                .C("HasChangesAfterClose").Assign().P("HasChangesAfterClose")
                .C("LastAccessTime").Assign().P("LastAccessTime")
                .C("ModifiedFileUrl").Assign().P("ModifiedFileUrl");

            if (newModifiedFileUrl is not null)
            {
                builder = builder
                    .C("LastModifiedFileUrlTime").Assign().P("LastModifiedFileUrlTime");
            }

            builder = builder.Where().C("ID").Equals().P("ID");

            int updated = await db
                .SetCommand(
                    builder.Build(),
                    db.Parameter("ID", id, DataType.Guid),
                    db.Parameter("HasChangesAfterClose", hasChangesAfterClose, DataType.Boolean),
                    db.Parameter("LastAccessTime", DateTime.UtcNow, DataType.DateTime),
                    db.Parameter("ModifiedFileUrl", newModifiedFileUrl, DataType.NVarChar),
                    db.Parameter("LastModifiedFileUrlTime", DateTime.UtcNow, DataType.DateTime))
                .LogCommand()
                .ExecuteNonQueryAsync(cancellationToken);

            if (updated == 0)
            {
                throw new InvalidOperationException($"Can't find file with ID={id:B}");
            }
        }


        /// <inheritdoc />
        public async Task UpdateInfoOnEditorOpenedAsync(Guid id, CancellationToken cancellationToken = default)
        {
            await using var _ = this.dbScope.Create();

            DbManager db = this.dbScope.Db;

            int updated = await db
                .SetCommand(
                    this.dbScope.BuilderFactory
                        .Update("OnlyOfficeFileCache")
                        .C("EditorWasOpen").Assign().P("EditorWasOpen")
                        .C("LastAccessTime").Assign().P("LastAccessTime")
                        .Where().C("ID").Equals().P("ID")
                        .Build(),
                    db.Parameter("ID", id, DataType.Guid),
                    db.Parameter("EditorWasOpen", BooleanBoxes.True, DataType.Boolean),
                    db.Parameter("LastAccessTime", DateTime.UtcNow, DataType.DateTime))
                .LogCommand()
                .ExecuteNonQueryAsync(cancellationToken);

            if (updated == 0)
            {
                throw new InvalidOperationException($"Can't find file with ID={id:B}");
            }
        }


        /// <inheritdoc />
        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            await using var _ = this.dbScope.Create();

            IQueryExecutor executor = this.dbScope.Executor;

            await executor.ExecuteNonQueryAsync(
                this.dbScope.BuilderFactory
                    .DeleteFrom("OnlyOfficeFileCache")
                    .Where().C("ID").Equals().P("ID")
                    .Build(),
                cancellationToken,
                executor.Parameter("ID", id, DataType.Guid));
        }


        /// <inheritdoc />
        public async Task CleanCacheInfoAsync(
            DateTime? oldestPreviewRequestTime = null,
            CancellationToken cancellationToken = default)
        {
            oldestPreviewRequestTime = oldestPreviewRequestTime?.ToUniversalTime();

            if (logger.IsTraceEnabled)
            {
                if (oldestPreviewRequestTime.HasValue)
                {
                    logger.Trace(
                        "Cleaning OnlyOffice cache, oldest allowed date/time: {0} UTC",
                        FormattingHelper.FormatDateTime(oldestPreviewRequestTime.Value, convertToLocal: false));
                }
                else
                {
                    logger.Trace("Cleaning OnlyOffice cache, removing all files info");
                }
            }

            await using var _ = this.dbScope.Create();

            var db = this.dbScope.Db;
            var builderFactory = this.dbScope.BuilderFactory;

            int deleted = oldestPreviewRequestTime.HasValue
                ? await db
                    .SetCommand(
                        builderFactory
                            .DeleteFrom("OnlyOfficeFileCache")
                            .Where().C("LastAccessTime").Less().P("OldestPreviewRequestTime")
                            .Build(),
                        db.Parameter("OldestPreviewRequestTime", oldestPreviewRequestTime.Value, DataType.DateTime))
                    .LogCommand()
                    .ExecuteNonQueryAsync(cancellationToken)
                : await db
                    .SetCommand(
                        builderFactory
                            .DeleteFrom("OnlyOfficeFileCache")
                            .Build())
                    .LogCommand()
                    .ExecuteNonQueryAsync(cancellationToken);

            logger.Trace(
                deleted == 0
                    ? "There are no files info to remove from OnlyOffice cache."
                    : "Files are successfully removed from OnlyOffice cache.");
        }

        /// <inheritdoc />
        public async Task<(IOnlyOfficeFileCacheInfo, bool)> CreateCoeditInfoAsync(Guid versionID, string name, Guid userID, CancellationToken cancellationToken = default)
        {
            // check existing coedit session
            string? coeditKey = null;
            bool isNew = false;

            await using var _ = this.dbScope.Create();

            DbManager db = this.dbScope.Db;

            await using (DbDataReader reader = await db
                .SetCommand(
                    this.dbScope.BuilderFactory
                        .Select().Top(1).C(null,
                            "CoeditKey")
                        .From("OnlyOfficeFileCache").NoLock()
                        .Where()
                        .C("CoeditKey").IsNotNull()
                        .And()
                        .C("HasChangesAfterClose").IsNull()
                        .And()
                        .C("EditorWasOpen").Equals().V(true)
                        .And()
                        .C("SourceFileVersionID").Equals().P("versionID")
                        .OrderBy("LastAccessTime", SortOrder.Descending)
                        .Limit(1)
                        .Build(),
                    db.Parameter("versionID", versionID, DataType.Guid))
                .LogCommand()
                .ExecuteReaderAsync(cancellationToken))
            {
                if (await reader.ReadAsync(cancellationToken))
                {
                    coeditKey = reader.GetNullableString(0);
                }
            }

            if (coeditKey is null)
            {
                isNew = true;
                coeditKey = Guid.NewGuid().ToString();
            }

            // create with existing or new key

            var info = new OnlyOfficeFileCacheInfo
            {
                ID = Guid.NewGuid(),
                SourceFileVersionID = versionID,
                SourceFileName = name,
                CreatedByID = userID,
                LastAccessTime = DateTime.UtcNow,
                CoeditKey = coeditKey
            };

            await this.InsertAsync(
                info,
                cancellationToken);

            return (info, isNew);
        }

        /// <inheritdoc />
        public async Task<Guid?> TryGetInfoAsync(string? coeditKey, Guid? userId, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(coeditKey) || !userId.HasValue)
            {
                return null;
            }

            await using var _ = this.dbScope.Create();

            DbManager db = this.dbScope.Db;

            return await db
               .SetCommand(
                   this.dbScope.BuilderFactory
                       .Select().C(null,
                            "ID")
                        .From("OnlyOfficeFileCache").NoLock()
                        .Where()
                        .C("CoeditKey").Equals().P("CoeditKey")
                        .And()
                        .C("CreatedByID").Equals().P("CreatedByID")
                        .Build(),
                   db.Parameter("CoeditKey", coeditKey, DataType.NVarChar),
                   db.Parameter("CreatedByID", userId, DataType.Guid))
               .LogCommand()
               .ExecuteAsync<Guid?>(cancellationToken);
        }

        // <inheritdoc />
        public async Task<(Guid cardID, Guid fileID)?> TryGetCardIDFileIDByFileVersionIDAsync(Guid fileVersionID, CancellationToken cancellationToken = default)
        {
            await using var _ = this.dbScope.Create();

            DbManager db = this.dbScope.Db;

            await using (var reader = await db
               .SetCommand(
                   this.dbScope.BuilderFactory
                       .Select().C("f",
                            "ID",
                            "RowID")
                        .From("Files", "f").NoLock()
                        .InnerJoin("FileVersions", "fv").NoLock()
                            .On().C("fv", "ID").Equals().C("f", "RowID")
                        .Where()
                        .C("fv", "RowID").Equals().P("FileVersionID")
                        .Build(),
                   db.Parameter("FileVersionID", fileVersionID, DataType.Guid))
               .LogCommand()
               .ExecuteReaderAsync(cancellationToken))
            {
                if (await reader.ReadAsync(cancellationToken))
                {
                    return (reader.GetGuid(0), reader.GetGuid(1));
                }
                else
                {
                    return null;
                }
            }
        }

        // <inheritdoc />
        public async Task<List<OnlyOfficeFileVersion>> GetFileVersionsAsync(Guid fileVersionID, CancellationToken cancellationToken = default)
        {
            await using var _ = this.dbScope.Create();

            DbManager db = this.dbScope.Db;

            return await db
                .SetCommand(
                   this.dbScope.BuilderFactory
                       .Select()
                        .C("fv2", "RowID")
                        .C("fv2", "Number")
                        .From("FileVersions", "fv").NoLock()
                        .InnerJoin("FileVersions", "fv2").NoLock()
                            .On().C("fv", "ID").Equals().C("fv2", "ID")
                        .Where()
                            .C("fv", "RowID").Equals().P("FileVersionID")
                        .Build(),
                   db.Parameter("FileVersionID", fileVersionID, DataType.Guid))
               .LogCommand()
               .ExecuteListAsync<OnlyOfficeFileVersion>(cancellationToken);
        }

        // <inheritdoc />
        public async Task<List<OnlyOfficeCurrentCoedit>> GetCurrentCoeditInfosAsync(IEnumerable<Guid> fileVersionIDs, CancellationToken cancellationToken = default)
        {
            if (fileVersionIDs == null || fileVersionIDs.Count() == 0)
            {
                return new List<OnlyOfficeCurrentCoedit>();
            }

            await using var _ = this.dbScope.Create();

            DbManager db = this.dbScope.Db;

            return await db
                .SetCommand(
                    this.dbScope.BuilderFactory
                        .Select()
                         .C("oo", "SourceFileVersionID")
                         .C("oo", "CoeditKey")
                         .C("oo", "LastAccessTime")
                         .C("pr", "Name")
                         .From("OnlyOfficeFileCache", "oo").NoLock()
                         .LeftJoin("PersonalRoles", "pr").NoLock()
                             .On().C("pr", "ID").Equals().C("oo", "CreatedByID")
                         .Where()
                             .C("oo", "CoeditKey").IsNotNull()
                             .And()
                             .C("oo", "HasChangesAfterClose").IsNull()
                             .And()
                             .C("oo", "EditorWasOpen").Equals().V(true)
                             .And()
                             .C("oo", "SourceFileVersionID")
                             .InArray(fileVersionIDs, nameof(fileVersionIDs), out var dpVersionIDs)
                         .Build(), DataParameters.Get(dpVersionIDs))
                .LogCommand()
                .ExecuteListAsync<OnlyOfficeCurrentCoedit>(cancellationToken);
        }

        #endregion
    }
}
