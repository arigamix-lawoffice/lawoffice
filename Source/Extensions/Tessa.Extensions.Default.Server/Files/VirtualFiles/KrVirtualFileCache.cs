using System;
using System.Linq;
using System.Collections.Generic;
using Tessa.Cards;
using Tessa.Cards.Caching;
using Tessa.Platform.Data;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Platform;
using Tessa.Files;
using Tessa.Platform.Storage;
using Tessa.Platform.Conditions;
using Unity;

namespace Tessa.Extensions.Default.Server.Files.VirtualFiles
{
    public sealed class KrVirtualFileCache :
        IKrVirtualFileCache,
        IDisposable
    {
        #region Nested Types

        private class Cache
        {
            private readonly Dictionary<Guid, IKrVirtualFile> itemsByKey;

            public Cache(IKrVirtualFile[] items)
            {
                this.Items = items;
                this.itemsByKey = items.ToDictionary(x => x.ID, x => x);
            }

            public IKrVirtualFile[] Items { get; }

            public IKrVirtualFile TryGet(Guid id) => this.itemsByKey.TryGetValue(id, out IKrVirtualFile result) ? result : null;
        }

        #endregion

        #region Fields

        private readonly ICardCache cache;
        private readonly IDbScope dbScope;
        private readonly ISeparateDbConnectionWorker worker;
        private readonly AsyncLock asyncLock = new AsyncLock();

        private const string CacheKey = CardHelper.SystemKeyPrefix + nameof(KrVirtualFileCache);
        private const string TypesCackeKey = CacheKey + "Types";

        #endregion

        #region Constructors

        public KrVirtualFileCache(
            ICardCache cache,
            IDbScope dbScope,
            ISeparateDbConnectionWorker worker,
            [OptionalDependency] IUnityDisposableContainer container = null)
        {
            this.cache = cache ?? throw new ArgumentNullException(nameof(cache));
            this.dbScope = dbScope ?? throw new ArgumentNullException(nameof(dbScope));
            this.worker = worker ?? throw new ArgumentNullException(nameof(worker));

            container?.Register(this);
        }

        #endregion

        #region IKrVirtualFileCache Implementation

        public async ValueTask<IKrVirtualFile> TryGetAsync(Guid virtualFileID, CancellationToken cancellationToken = default) =>
            (await cache.Settings.GetAsync(CacheKey, InitCacheAsync, cancellationToken))
            .TryGet(virtualFileID);

        public async ValueTask<IKrVirtualFile[]> GetAllAsync(CancellationToken cancellationToken = default) =>
            (await cache.Settings.GetAsync(CacheKey, InitCacheAsync, cancellationToken))
            .Items;

        public ValueTask<Guid[]> GetAllowedTypesAsync(CancellationToken cancellationToken = default) =>
            cache.Settings.GetAsync(TypesCackeKey, InitTypesAsync, cancellationToken);

        public async Task InvalidateAsync(CancellationToken cancellationToken = default)
        {
            using (await this.asyncLock.EnterAsync(cancellationToken))
            {
                await cache.Settings.InvalidateAsync(CacheKey, cancellationToken);
                await cache.Settings.InvalidateAsync(TypesCackeKey, cancellationToken);
            }
        }

        #endregion

        #region Private Methods

        private async Task<Guid[]> InitTypesAsync(string arg, CancellationToken cancellationToken = default)
        {
            using (await this.asyncLock.EnterAsync(cancellationToken))
            await using (this.worker.CreateScope())
            {
                var db = dbScope.Db;
                var builder = dbScope.BuilderFactory;

                return (await db.SetCommand(
                        builder
                            .SelectDistinct()
                            .C("vft", "TypeID") // 0
                            .From("KrVirtualFileCardTypes", "vft").NoLock()
                            .Build())
                    .LogCommand()
                    .ExecuteListAsync<Guid>(cancellationToken)).ToArray();
            }
        }

        private async Task<Cache> InitCacheAsync(string arg, CancellationToken cancellationToken = default)
        {
            using (await this.asyncLock.EnterAsync(cancellationToken))
            await using (worker.CreateScope())
            {
                var db = dbScope.Db;
                var builder = dbScope.BuilderFactory;

                db.SetCommand(
                        builder
                            .Select()
                            .C("vf", "ID", "FileVersionID", "FileTemplateID", "FileName", "FileCategoryID", "FileCategoryName", "Conditions", "InitializationScenario") // 0 - 7
                            .C("f", "Name") // 8
                            .C("vfv", "RowID", "FileTemplateID", "FileName") // 9 - 11
                            .C("ff", "Name") // 12
                            .C("ft", "ConvertToPDF") // 13
                            .From("KrVirtualFiles", "vf").NoLock()
                            .LeftJoin("KrVirtualFileVersions", "vfv").NoLock()
                            .On().C("vf", "ID").Equals().C("vfv", "ID")
                            .InnerJoin("Files", "f").NoLock()
                            .On().C("f", "ID").Equals().C("vf", "FileTemplateID")
                            .InnerJoin("FileTemplates", "ft").NoLock()
                            .On().C("ft", "ID").Equals().C("vf", "FileTemplateID")
                            .LeftJoin("Files", "ff").NoLock()
                            .On().C("ff", "ID").Equals().C("vfv", "FileTemplateID")
                            .OrderBy("vf", "ID").By("vfv", "Order", SortOrder.Ascending)
                            .Build())
                    .LogCommand();

                var items = new List<IKrVirtualFile>();
                await using (var reader = await db.ExecuteReaderAsync(cancellationToken))
                {
                    var prevFileID = Guid.Empty;
                    IKrVirtualFile prevFile = null;
                    IKrVirtualFileVersion mainVersion = null;
                    while (await reader.ReadAsync(cancellationToken))
                    {
                        var currentFileID = reader.GetGuid(0);
                        var currentVersionID = reader.GetGuid(1);
                        var fileTemplateID = reader.GetGuid(2);
                        var fileName = reader.GetNullableString(3);
                        var fileCategoryID = reader.GetNullableGuid(4);
                        var fileCategoryName = reader.GetNullableString(5);
                        var conditions = reader.GetNullableString(6);
                        var initializationScenario = reader.GetNullableString(7);
                        var templateName = reader.GetNullableString(8);
                        var additionalVersionID = reader.GetNullableGuid(9);
                        var additionalVersionTemplateID = reader.GetNullableGuid(10);
                        var additionalFileName = reader.GetNullableString(11);
                        var additionalTemplateName = reader.GetNullableString(12);
                        var convertToPdf = reader.GetBoolean(13);

                        if (currentFileID != prevFileID)
                        {
                            prevFile?.Versions.Add(mainVersion);
                            prevFileID = currentFileID;

                            if (string.IsNullOrEmpty(fileName))
                            {
                                fileName = templateName;
                            }
                            else
                            {
                                fileName += Tessa.Platform.IO.FileHelper.GetExtension(templateName);
                            }

                            if (convertToPdf)
                            {
                                fileName = Tessa.Platform.IO.FileHelper.GetFileNameWithoutExtension(fileName) + ".pdf";
                            }

                            prevFile =
                                new KrVirtualFile
                                {
                                    ID = currentFileID,
                                    Name = fileName,
                                    FileCategory = fileCategoryID.HasValue ? new FileCategory(fileCategoryID, fileCategoryName) : null,
                                    Conditions = string.IsNullOrEmpty(conditions)
                                        ? null
                                        : ConditionSettings.GetFromList(StorageHelper.DeserializeListFromTypedJson(conditions)),
                                    InitializationScenario = initializationScenario,
                                };

                            mainVersion =
                                new KrVirtualFileVersion
                                {
                                    ID = currentVersionID,
                                    FileTemplateID = fileTemplateID,
                                    Name = fileName,
                                };

                            items.Add(prevFile);
                        }

                        if (additionalVersionID.HasValue
                            && additionalVersionTemplateID.HasValue)
                        {
                            if (string.IsNullOrEmpty(additionalFileName))
                            {
                                additionalFileName = additionalTemplateName;
                            }
                            else
                            {
                                additionalFileName += Tessa.Platform.IO.FileHelper.GetExtension(additionalTemplateName);
                            }

                            if (prevFile != null)
                            {
                                prevFile.Versions.Add(
                                    new KrVirtualFileVersion
                                    {
                                        ID = additionalVersionID.Value,
                                        FileTemplateID = additionalVersionTemplateID.Value,
                                        Name = additionalFileName,
                                    });
                            }
                        }
                    }

                    prevFile?.Versions.Add(mainVersion);
                }

                return new Cache(items.ToArray());
            }
        }

        #endregion

        #region IDisposable Members

        public void Dispose() => this.asyncLock.Dispose();

        #endregion
    }
}
