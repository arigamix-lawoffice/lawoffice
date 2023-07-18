using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.ComponentModel;
using Tessa.Platform.ConsoleApps;
using Tessa.Platform.Data;
using Tessa.Platform.Runtime;
using Unity;

namespace Tessa.Extensions.Default.Console.MigrateFiles
{
    public static class Operation
    {
        #region Private Methods

        private static async Task<bool> MigrateFilesAsync(
            IConsoleLogger logger,
            List<FileVersionInfo> versionInfoList,
            CardFileSourceType fromSourceTypeID,
            ICardContentStrategy fromSourceContentStrategy,
            CardFileSourceType toSourceTypeID,
            ICardContentStrategy toSourceContentStrategy,
            IDbScope dbScope,
            int threadCount,
            bool removeFromTargetFileSource,
            bool canCancel,
            bool quiet,
            CancellationToken cancellationToken = default)
        {
            var migrationTasks = new MigrationTask[threadCount];
            var completedCount = new int[threadCount];
            var errorCount = new int[threadCount];

            int totalCount = versionInfoList.Count;
            int itemIndex = 0;
            int itemPerThread = totalCount / threadCount;

            for (int taskIndex = 0; taskIndex < migrationTasks.Length; taskIndex++)
            {
                int itemCount = taskIndex < migrationTasks.Length - 1
                    ? itemPerThread
                    : totalCount - itemIndex;

                List<FileVersionInfo> taskInfoList = versionInfoList.GetRange(itemIndex, itemCount);
                itemIndex += itemCount;

                migrationTasks[taskIndex] = new MigrationTask(
                    taskInfoList,
                    fromSourceTypeID,
                    fromSourceContentStrategy,
                    toSourceTypeID,
                    toSourceContentStrategy,
                    dbScope,
                    completedCount,
                    errorCount,
                    taskIndex,
                    removeFromTargetFileSource,
                    logger.LogResultAsync);
            }

            Task[] tasks = migrationTasks
                .Select(migrationTask => migrationTask.RunAsync(cancellationToken))
                .ToArray();

            if (!quiet)
            {
                if (canCancel)
                {
                    await logger.WriteLineAsync("Press Esc to cancel");
                }

                string prevText = null;
                await WritePercentageAsync(logger, 0, totalCount, ref prevText);

                bool cancelled = false;
                Task allTaskAwaiter = Task.WhenAll(tasks);

                while (true)
                {
                    Task completedTask = await Task.WhenAny(allTaskAwaiter, Task.Delay(1000, cancellationToken));
                    if (completedTask == allTaskAwaiter || cancellationToken.IsCancellationRequested)
                    {
                        break;
                    }

                    int count = 0;
                    for (int i = 0; i < completedCount.Length; i++)
                    {
                        count += completedCount[i];
                    }

                    await WritePercentageAsync(logger, count, totalCount, ref prevText);

                    if (canCancel)
                    {
                        while (System.Console.KeyAvailable)
                        {
                            if (System.Console.ReadKey(intercept: true).Key == ConsoleKey.Escape)
                            {
                                cancelled = true;
                                break;
                            }
                        }
                    }

                    if (cancelled)
                    {
                        // выполняем отмену
                        await logger.InfoAsync("Cancelling is initiated by user");

                        for (int i = 0; i < completedCount.Length; i++)
                        {
                            Interlocked.Exchange(ref completedCount[i], int.MinValue);
                        }

                        await allTaskAwaiter;

                        await logger.InfoAsync("Migration is cancelled");
                        break;
                    }
                }

                if (!cancelled)
                {
                    await WritePercentageAsync(logger, totalCount, totalCount, ref prevText);
                }
            }
            else
            {
                await Task.WhenAll(tasks);
            }

            await logger.WriteLineAsync();

            return errorCount.All(count => count == 0);
        }


        private static Task WritePercentageAsync(IConsoleLogger logger, int count, int totalCount, ref string prevText)
        {
            // выводим проценты на той же строке

            double percentage = 100.0 * count / totalCount;
            string percentageText = "\r" + percentage.ToString("F1", CultureInfo.InvariantCulture) + " % ";

            if (!string.Equals(prevText, percentageText, StringComparison.Ordinal))
            {
                prevText = percentageText;
                return logger.WriteAsync(percentageText);
            }

            return Task.CompletedTask;
        }

        #endregion

        #region Methods

        public static async Task<int> ExecuteAsync(
            IConsoleLogger logger,
            string configurationString,
            string databaseName,
            int fromSourceID,
            int toSourceID,
            int threadCount,
            bool removeFromTargetFileSource,
            bool canCancel,
            bool quiet,
            CancellationToken cancellationToken = default)
        {
            await logger.InfoAsync(
                "Migrating files from source ID={0} to ID={1} via connection \"{2}\"",
                fromSourceID,
                toSourceID,
                string.IsNullOrEmpty(configurationString) ? "default" : configurationString);

            if (!string.IsNullOrEmpty(databaseName))
            {
                await logger.InfoAsync("Database is \"{0}\"", databaseName);
            }

            if (fromSourceID == toSourceID)
            {
                await logger.ErrorAsync("File sources are the same ID={0}", fromSourceID);
                return -1;
            }

            await logger.InfoAsync("Initializing Tessa server");

            Func<CancellationToken, Task<DbManager>> createDbManagerFunc =
                await ConsoleAppHelper.CreateAsyncDbManagerFuncAsync(logger, configurationString, databaseName, cancellationToken);

            IUnityContainer container = await new UnityContainer()
                .RegisterServerForConsoleAsync(
                    () => createDbManagerFunc(cancellationToken).GetAwaiter().GetResult(),
                    cancellationToken: cancellationToken);

            IExtensionAssemblyInfo assemblyInfo = container.ResolveAssemblyInfo();
            await ConsoleAppHelper.LogLoadedExtensionsAsync(logger, assemblyInfo, SessionType.Server);

            await logger.InfoAsync("Loading file source information");

            var versionInfoList = new List<FileVersionInfo>();
            var fromSourceTypeID = new CardFileSourceType(fromSourceID);
            var toSourceTypeID = new CardFileSourceType(toSourceID);
            ICardFileSource fromSource;
            ICardFileSource toSource;

            var dbScope = container.Resolve<IDbScope>();
            await using (dbScope.Create())
            {
                // проверяем доступность местоположений и выводим их в лог
                Task LogFileSourceInfoAsync(ICardFileSource fileSource) =>
                    logger.InfoAsync(
                        "File source ID={0}, Name={1}, {2}: {3}",
                        fileSource.Type.ID,
                        fileSource.Name,
                        fileSource.IsDatabase ? "Database" : "Files",
                        fileSource.Path);

                var fileSources = await GetFileSourcesAsync(dbScope, fromSourceTypeID, toSourceTypeID, cancellationToken);
                fromSource = fileSources.FromSource;
                toSource = fileSources.ToSource;

                if (fromSource == null)
                {
                    await logger.ErrorAsync("Can't get information about file source ID={0}", fromSourceTypeID);
                    return -2;
                }

                await LogFileSourceInfoAsync(fromSource);

                if (toSource == null)
                {
                    await logger.ErrorAsync("Can't get information about file source ID={0}", toSourceTypeID);
                    return -3;
                }

                await LogFileSourceInfoAsync(toSource);

                // определяем идентификаторы версий файлов, которые будут перенесены
                await logger.InfoAsync("Loading files list to migrate");

                DbManager db = dbScope.Db;

                db
                    .SetCommand(
                        dbScope.BuilderFactory
                            .Select().C("f", "ID", "RowID").C("fv", "RowID")
                            .From("Files", "f").NoLock()
                            .InnerJoin("FileVersions", "fv").NoLock()
                            .On().C("fv", "ID").Equals().C("f", "RowID")
                            .Where().C("fv", "SourceID").Equals().P("SourceID")
                            .Build(),
                        db.Parameter("SourceID", fromSourceID))
                    .LogCommand();

                await using (DbDataReader reader = await db.ExecuteReaderAsync(cancellationToken))
                {
                    while (await reader.ReadAsync(cancellationToken))
                    {
                        Guid cardID = reader.GetGuid(0);
                        Guid fileID = reader.GetGuid(1);
                        Guid versionRowID = reader.GetGuid(2);

                        versionInfoList.Add(new FileVersionInfo(cardID, fileID, versionRowID));
                    }
                }

                if (versionInfoList.Count == 0)
                {
                    await logger.InfoAsync("There are no files to migrate from file source ID={0}", fromSourceTypeID);
                    return 0;
                }
            }

            // переносим файлы из исходного местоположения в целевое
            if (threadCount > versionInfoList.Count)
            {
                threadCount = versionInfoList.Count;
            }

            await logger.InfoAsync("Migrating files ({0}) using {1} thread(s)", versionInfoList.Count, threadCount);

            var contentStrategy = container.Resolve<CardSourceContentStrategy>();
            ICardContentStrategy fromSourceContentStrategy = contentStrategy.GetContentStrategy(fromSource);
            ICardContentStrategy toSourceContentStrategy = contentStrategy.GetContentStrategy(toSource);

            bool success = await MigrateFilesAsync(
                logger,
                versionInfoList,
                fromSourceTypeID,
                fromSourceContentStrategy,
                toSourceTypeID,
                toSourceContentStrategy,
                dbScope,
                threadCount,
                removeFromTargetFileSource,
                canCancel,
                quiet,
                cancellationToken);

            if (!success)
            {
                await logger.ErrorAsync("Migration has been completed with errors, see log for details");
                return -4;
            }

            await logger.InfoAsync("Migration has been completed");
            return 0;
        }

        #endregion

        #region Private Methods

        private static async Task<(ICardFileSource FromSource, ICardFileSource ToSource)> GetFileSourcesAsync(IDbScope dbScope,
            CardFileSourceType fromSourceType, CardFileSourceType toSourceType, CancellationToken cancellationToken)
        {
            // SELECT ID, Name, [Path], IsDatabase, Description, [Size], MaxSize, FileExtensions
            // FROM tessa_master.dbo.FileSources;
            var db = dbScope.Db;
            db
                .SetCommand(
                    dbScope.BuilderFactory
                        .Select().C(
                            "fs",
                            "ID",
                            "Name",
                            "Path",
                            "IsDatabase",
                            "Size",
                            "MaxSize",
                            "FileExtensions"
                        )
                        .From("FileSources", "fs").NoLock()
                        .Where().C("fs", "ID").Equals().P("FromSourceID")
                        .Or().C("fs", "ID").Equals().P("ToSourceID")
                        .Build(),
                    db.Parameter("FromSourceID", fromSourceType.ID),
                    db.Parameter("ToSourceID", toSourceType.ID))
                .LogCommand();

            IList<ICardFileSource> fileSources = new List<ICardFileSource>();

            await using (DbDataReader reader = await db.ExecuteReaderAsync(cancellationToken))
            {
                while (await reader.ReadAsync(cancellationToken))
                {
                    int id = reader.GetInt16(0);
                    string name = reader.GetString(1);
                    string path = reader.GetString(2);
                    bool isDatabase = reader.GetBoolean(3);
                    int size = reader.GetInt32(4);
                    int maxSize = reader.GetInt32(5);
                    string fileExtensions = reader.GetNullableString(6);

                    fileSources.Add(new CardFileSource(
                        new CardFileSourceType(id),
                        name,
                        path,
                        isDatabase,
                        fileExtensions,
                        size,
                        maxSize));
                }
            }

            ICardFileSource fromSource = fileSources.FirstOrDefault(x => x.Type == fromSourceType);
            ICardFileSource toSource = fileSources.FirstOrDefault(x => x.Type == toSourceType);

            return (fromSource, toSource);
        }

        #endregion
    }
}
