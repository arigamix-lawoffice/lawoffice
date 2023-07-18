using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LinqToDB.Data;
using Tessa.Platform;
using Tessa.Platform.Collections;
using Tessa.Platform.ConsoleApps;
using Tessa.Platform.Data;
using Tessa.Platform.Runtime;
using Tessa.Scheme;
using Unity;

namespace Tessa.Extensions.Default.Console.MigrateDatabase
{
    public static class Operation
    {
        #region Fields

        private static readonly HashSet<string> tablesToExclude =
            new HashSet<string>(new[]
            {
                Names.Configuration,
                Names.Scheme,
                Names.Functions,
                Names.Migrations,
                Names.Partitions,
                Names.Procedures,
                Names.Tables,
                "CompiledViews",
            });

        #endregion

        #region Constants

        // В запрос добавлено явное указание COLLATE "C" при сравнении имен таблиц из таблицы "Tables" и из системной таблицы pg_class
        // Начиная с версии Postgres 12 все системные объекты имеют COLLATION "C"
        // В Tessa для системных объектов используется COLLATION "POSIX"
        // Из-за разницы COLLATION в Postgres 12 нужно явно указывать COLLATION при сравнении значений.
        // В Postgres 9.6 нельзя указывать для системных объектов COLLATE "POSIX", поэтому используется COLLATE "C" для таблицы Tessa "Tables"."Name"
        private const string EnableIndicesPostgreSql =
            @"DO $$
DECLARE query_text TEXT;
BEGIN
    SELECT string_agg('REINDEX TABLE ""' || ""Tables"".""Name"" || '"";', chr(10))
    INTO query_text
    FROM ""Tables""
    INNER JOIN pg_class ON pg_class.relname = ""Tables"".""Name"" COLLATE ""C"";

    EXECUTE query_text;

    SELECT string_agg('CLUSTER ""' || table_name || '"" USING ""' || index_name[1] || '"";', chr(10))
    INTO query_text
    FROM (
        SELECT ""Tables"".""Name"", xpath('/SchemeTable/*[@IsClustered=""true""]/@Name', ""Tables"".""Definition"")
        FROM ""Tables""
        INNER JOIN pg_class ON pg_class.relname = ""Tables"".""Name"" COLLATE ""C"") AS t (table_name, index_name)
    WHERE array_length(index_name, 1) > 0;

    EXECUTE query_text;

    SELECT string_agg('ALTER TABLE ""' || ""Tables"".""Name"" || '"" ENABLE TRIGGER ALL;', chr(10))
    INTO query_text
    FROM ""Tables""
    INNER JOIN pg_class ON pg_class.relname = ""Tables"".""Name"" COLLATE ""C"";

    EXECUTE query_text;
END $$;";

        // В запрос добавлено явное указание COLLATE "C" при сравнении имен таблиц из таблицы "Tables" и имен из системной таблицы pg_class
        // Начиная с версии Postgres 12 все системные объекты имеют COLLATION "C"
        // В Tessa для системных объектов используется COLLATION "POSIX"
        // Из-за разницы COLLATION в Postgres 12 нужно явно указывать COLLATION при сравнении значений.
        // В Postgres 9.6 нельзя указывать для системных объйктов COLLATE "POSIX", поэтому используется COLLATE "C" для таблицы Tessa "Tables"."Name"
        private const string DisableIndicesPostgreSql =
            @"DO $$
DECLARE query_text TEXT;
BEGIN
    UPDATE pg_index
    SET indisready = false
    WHERE indrelid IN (
        SELECT pg_class.oid
        FROM pg_class
        INNER JOIN ""Tables"" ON ""Tables"".""Name"" COLLATE ""C"" = pg_class.relname
    );

    SELECT string_agg('ALTER TABLE ""' || ""Tables"".""Name"" || '"" DISABLE TRIGGER ALL;', chr(10))
    INTO query_text
    FROM ""Tables""
    INNER JOIN pg_class ON pg_class.relname = ""Tables"".""Name"" COLLATE ""C"";

    EXECUTE query_text;
END $$;";

        /// <summary>
        /// Важно сначала включить индексы, и только потом FK.
        /// </summary>
        private const string EnableIndicesSqlServer =
            @"DECLARE @query NVARCHAR(MAX);

DECLARE query_cursor CURSOR FOR
SELECT 'ALTER INDEX ' + QUOTENAME(I.name) + ' ON ' +  QUOTENAME(SCHEMA_NAME(T.schema_id))+'.'+ QUOTENAME(T.name) + ' REBUILD'
FROM sys.indexes I
INNER JOIN sys.tables T ON I.object_id = T.object_id
WHERE I.type_desc = 'NONCLUSTERED'
AND I.name IS NOT NULL
AND I.is_disabled = 1;

OPEN query_cursor;
FETCH NEXT FROM query_cursor INTO @query;

WHILE @@FETCH_STATUS = 0
BEGIN
	EXECUTE sp_executesql @query;
	FETCH NEXT FROM query_cursor INTO @query;
END;

CLOSE query_cursor;
DEALLOCATE query_cursor;

EXEC sp_msforeachtable ""ALTER TABLE ? WITH CHECK CHECK CONSTRAINT ALL"";";

        private const string DisableIndicesSqlServer =
            @"EXEC sp_msforeachtable ""ALTER TABLE ? NOCHECK CONSTRAINT ALL"";

DECLARE @query NVARCHAR(MAX);

DECLARE query_cursor CURSOR FOR
SELECT 'ALTER INDEX ' + QUOTENAME(I.name) + ' ON ' +  QUOTENAME(SCHEMA_NAME(T.schema_id))+'.'+ QUOTENAME(T.name) + ' DISABLE'
FROM sys.indexes I
INNER JOIN sys.tables T ON I.object_id = T.object_id
WHERE I.type_desc = 'NONCLUSTERED'
AND I.name IS NOT NULL
AND I.is_disabled = 0;

OPEN query_cursor;
FETCH NEXT FROM query_cursor INTO @query;

WHILE @@FETCH_STATUS = 0
BEGIN
	EXECUTE sp_executesql @query;
	FETCH NEXT FROM query_cursor INTO @query;
END;

CLOSE query_cursor;
DEALLOCATE query_cursor;";

        #endregion

        #region Private Methods

        private static async ValueTask<(
                string ConnectionString,
                DbProviderFactory DbProviderFactory,
                ConfigurationDataProvider ConfigurationDataProvider)>
            ResolveConnectionParametersAsync(
                string sourceConfigurationString,
                string sourceDatabaseName)
        {
            (ConfigurationDataProvider dataProvider, ConfigurationConnection configurationConnection) =
                (await ConfigurationManager.GetDefaultAsync()).Configuration
                .GetConfigurationDataProvider(sourceConfigurationString);

            ConfigurationDataProvider configurationDataProvider = dataProvider;

            DbProviderFactory dbProviderFactory = ConfigurationManager
                .GetConfigurationDataProviderFromType(configurationConnection.DataProvider)
                .GetDbProviderFactory();

            string connectionString = configurationConnection.ConnectionString;

            if (!string.IsNullOrEmpty(sourceDatabaseName))
            {
                DbConnectionStringBuilder connectionBuilder = dbProviderFactory.CreateConnectionStringBuilder();
                if (connectionBuilder is null)
                {
                    throw new InvalidOperationException($"Connection builder is null in method {nameof(Operation)}.{nameof(ResolveConnectionParametersAsync)}");
                }

                connectionBuilder.ConnectionString = connectionString;
                connectionBuilder["Database"] = sourceDatabaseName;
                connectionString = connectionBuilder.ToString();
            }

            return (connectionString, dbProviderFactory, configurationDataProvider);
        }


        private static DbConnection CreateDbConnection(
            DbProviderFactory sourceDbProviderFactory,
            string sourceConnectionString)
        {
            DbConnection connection = sourceDbProviderFactory.CreateConnection();
            if (connection is null)
            {
                throw new InvalidOperationException($"Can't create connection: {sourceConnectionString}");
            }

            connection.ConnectionString = sourceConnectionString;
            return connection;
        }


        private static Task MigrateDataAsync(
            IConsoleLogger logger,
            IReadOnlyList<TaskDbContext> contextArray,
            IReadOnlyCollection<SchemeTable> tables,
            CancellationToken cancellationToken = default)
        {
            if (contextArray.Count == 0 || tables.Count == 0)
            {
                return Task.CompletedTask;
            }

            var queue = new ConcurrentQueue<SchemeTable>(tables);

            async Task DoWorkAsync(int threadIndex)
            {
                TaskDbContext context = contextArray[threadIndex];

                while (queue.TryDequeue(out SchemeTable table))
                {
                    await MigrateDataTableAsync(logger, context, table, threadIndex, cancellationToken);
                }
            }

            return Task.WhenAll(Enumerable
                .Range(0, contextArray.Count)
                .Select(DoWorkAsync)
                .ToArray());
        }


        private static async Task MigrateDataTableAsync(
            IConsoleLogger logger,
            TaskDbContext context,
            SchemeTable table,
            int threadIndex,
            CancellationToken cancellationToken = default)
        {
            SchemePhysicalColumn[] columns = table.Columns.GetPhysicalColumns().ToArray();
            if (columns.Length == 0)
            {
                return;
            }

            await logger.InfoAsync("Migrating table \"{0}\" on connection {1}", table.Name, threadIndex + 1);

            try
            {
                string[] columnNames = columns.Select(x => x.Name).ToArray();

                context.SourceDb
                    .SetCommand(
                        context.SourceBuilderFactory
                            .Select().C(null, columnNames)
                            .From(table.Name).NoLock()
                            .Build())
                    .WithoutTimeout()
                    .LogCommand();

                if (context.BulkSize > 1)
                {
                    SchemeDbType[] dataTypes = columns
                        .Select(x => x.Type.DbType)
                        .ToArray();

                    BulkInsertParameters bulkInsertParameters = context.InsertExecutor.PrepareBulkInsert(
                        context.TargetDb,
                        context.TargetBuilderFactory,
                        table.Name,
                        columnNames,
                        dataTypes);

                    // этом массивы значений, наполняемые через SELECT на исходной базе
                    var parameterValueArrays = new object[columnNames.Length][];
                    for (int i = 0; i < parameterValueArrays.Length; i++)
                    {
                        parameterValueArrays[i] = context.ValueArrayPool.Get();
                    }

                    // это массивы значений, на которых будет выполняться асинхронная вставка INSERT,
                    // пока parameterValueArrays будет параллельно наполняться из SELECT
                    var taskParameterValueArrays = new object[columnNames.Length][];
                    for (int i = 0; i < taskParameterValueArrays.Length; i++)
                    {
                        taskParameterValueArrays[i] = context.ValueArrayPool.Get();
                    }

                    Task insertTask = Task.CompletedTask;
                    int row = 0;

                    await using (DbDataReader reader = await context.SourceDb.ExecuteReaderAsync(cancellationToken))
                    {
                        while (await reader.ReadAsync(cancellationToken))
                        {
                            // читаем строку
                            for (int i = 0; i < parameterValueArrays.Length; i++)
                            {
                                parameterValueArrays[i][row] = reader.GetValue(i);
                            }

                            row++;

                            // если мы вышли за пределы Bulk-а, то пора его вставлять
                            if (row == context.BulkSize)
                            {
                                row = 0;

                                // если предыдущая вставка не закончилась - ждём её
                                await insertTask;

                                // прочитанные значения из parameterValueArrays копируем в taskParameterValueArrays для вставки,
                                // а уже "отработавшие" значения из taskParameterValueArrays переносим в parameterValueArrays для наполнения
                                object[][] currentTaskParameterValueArrays = parameterValueArrays;
                                parameterValueArrays = taskParameterValueArrays;
                                taskParameterValueArrays = currentTaskParameterValueArrays;

                                insertTask = context.InsertExecutor.BulkInsertAsync(
                                    bulkInsertParameters,
                                    context.BulkSize,
                                    currentTaskParameterValueArrays,
                                    cancellationToken);
                            }
                        }
                    }

                    if (row > 0)
                    {
                        // есть несколько строк, ожидающих вставки (индексы от 0 до row-1), а таблица уже закончилась
                        for (int i = 0; i < parameterValueArrays.Length; i++)
                        {
                            object[] valueArray = parameterValueArrays[i];

                            object[] resizedArray = new object[row];
                            Array.Copy(valueArray, resizedArray, row);

                            parameterValueArrays[i] = resizedArray;
                            context.ValueArrayPool.Return(valueArray);
                        }

                        // необходимо дождаться окончания предыдущей вставки
                        await insertTask;

                        // последнюю "пачку" вставляем синхронно без пула потоков, т.к. всё равно надо сразу ждать завершения
                        await context.InsertExecutor.BulkInsertAsync(
                            bulkInsertParameters,
                            row,
                            parameterValueArrays,
                            cancellationToken);
                    }
                    else
                    {
                        // все строки вставлены, возвращаем массивы в пул для использования в других таблицах
                        for (int i = 0; i < parameterValueArrays.Length; i++)
                        {
                            context.ValueArrayPool.Return(parameterValueArrays[i]);
                        }

                        // ждём завершения вставки, чтобы вернуть массивы в пул
                        await insertTask;
                    }

                    // освобождаем массивы из taskParameterValueArrays только после того, как insertTask завершился
                    for (int i = 0; i < taskParameterValueArrays.Length; i++)
                    {
                        context.ValueArrayPool.Return(taskParameterValueArrays[i]);
                    }
                }
                else
                {
                    // режим копирования-вставки по одной строке (без bulk insert), оно быстрее всего выполняется если читать строку и тут же её вставить

                    // имена параметров в INSERT совпадают с именами колонок
                    DataParameter[] targetCommandParameters =
                        columns
                            .Select(x => context.TargetDb.Parameter(x.Name, x.Type.DbType.ToDataType()))
                            .ToArray();

                    context.TargetDb
                        .SetCommand(context.TargetBuilderFactory
                                .InsertInto(table.Name, columnNames)
                                .Values(b => b.P(columnNames))
                                .Build(),
                            targetCommandParameters)
                        .WithoutTimeout();

                    await using DbDataReader reader = await context.SourceDb.ExecuteReaderAsync(cancellationToken);
                    while (await reader.ReadAsync(cancellationToken))
                    {
                        for (int i = 0; i < targetCommandParameters.Length; i++)
                        {
                            targetCommandParameters[i].Value = reader.GetValue(i);
                        }

                        await context.TargetDb.ExecuteNonQueryAsync(cancellationToken);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                await logger.LogExceptionAsync($"Error while migrating table \"{table.Name}\".", ex);
            }
            finally
            {
                context.TargetDb.CancelCommand();
            }
        }


        private static async Task SetupIndicesForeignKeysAndTableLoggingAsync(
            bool enable,
            IConsoleLogger logger,
            Dbms dbms,
            DbManager db,
            IQueryBuilderFactory builderFactory,
            List<SchemeTable> tables,
            CancellationToken cancellationToken = default)
        {
            await logger.InfoAsync("{0} indices, foreign keys and logging", enable ? "Enabling" : "Disabling");

            switch (dbms)
            {
                case Dbms.SqlServer:
                    if (enable)
                    {
                        await db
                            .SetCommand(EnableIndicesSqlServer)
                            .WithoutTimeout()
                            .LogCommand()
                            .ExecuteNonQueryAsync(cancellationToken);
                    }
                    else
                    {
                        await db
                            .SetCommand(DisableIndicesSqlServer)
                            .WithoutTimeout()
                            .LogCommand()
                            .ExecuteNonQueryAsync(cancellationToken);
                    }

                    break;

                case Dbms.PostgreSql:
                    // сортировка должна быть быстрая и стабильная, т.к. нам приходят таблицы
                    // только с системными неотключаемыми FK (остальные мы уже прибили);
                    // сама сортировка нужна для установки LOGGED/UNLOGGED для Postgres

                    List<SchemeTable> orderedTables = tables
                        .OrderByDependencies(
                            t => t.Constraints
                                .OfType<SchemeForeignKey>()
                                .Select(f => f.ReferencedTable)
                                .Where(rt => rt is not null),
                            (t, r) => t)
                        .ToList();

                    if (enable)
                    {
                        await db
                            .SetCommand(EnableIndicesPostgreSql)
                            .WithoutTimeout()
                            .LogCommand()
                            .ExecuteNonQueryAsync(cancellationToken);

                        foreach (SchemeTable table in orderedTables)
                        {
                            await db
                                .SetCommand(
                                    builderFactory
                                        .Q("ALTER TABLE ").T(table.Name).Q(" SET LOGGED")
                                        .Build())
                                .WithoutTimeout()
                                .LogCommand()
                                .ExecuteNonQueryAsync(cancellationToken);
                        }
                    }
                    else
                    {
                        await db
                            .SetCommand(DisableIndicesPostgreSql)
                            .WithoutTimeout()
                            .LogCommand()
                            .ExecuteNonQueryAsync(cancellationToken);

                        orderedTables.Reverse();

                        foreach (SchemeTable table in orderedTables)
                        {
                            await db
                                .SetCommand(
                                    builderFactory
                                        .Q("ALTER TABLE ").T(table.Name).Q(" SET UNLOGGED")
                                        .Build())
                                .WithoutTimeout()
                                .LogCommand()
                                .ExecuteNonQueryAsync(cancellationToken);
                        }
                    }

                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(dbms), dbms, null);
            }
        }

        #endregion

        #region Methods

        public static async Task<int> ExecuteAsync(
            IConsoleLogger logger,
            string sourceConfigurationString,
            string sourceDatabaseName,
            string targetConfigurationString,
            string targetDatabaseName,
            int threadCount,
            int bulkSize,
            bool quiet,
            CancellationToken cancellationToken = default)
        {
            await logger.InfoAsync(
                "Migrating database from connection \"{0}\" to connection \"{1}\", using {2} thread(s), bulk size is {3}",
                string.IsNullOrEmpty(sourceConfigurationString) ? "default" : sourceConfigurationString,
                string.IsNullOrEmpty(targetConfigurationString) ? "default" : targetConfigurationString,
                threadCount,
                bulkSize);

            if (!string.IsNullOrEmpty(sourceDatabaseName))
            {
                await logger.InfoAsync("Source database is \"{0}\"", sourceDatabaseName);
            }

            if (!string.IsNullOrEmpty(targetDatabaseName))
            {
                await logger.InfoAsync("Target database is \"{0}\"", targetDatabaseName);
            }

            // нужен контейнер с расширениями для IBulkInsertExecutor-а, который у Postgres и других СУБД может быть особый
            IUnityContainer container = await new UnityContainer().RegisterDatabaseForConsoleAsync(cancellationToken: cancellationToken);

            IExtensionAssemblyInfo assemblyInfo = container.ResolveAssemblyInfo();
            await ConsoleAppHelper.LogLoadedExtensionsAsync(logger, assemblyInfo, SessionType.Server);

            await logger.InfoAsync("Connecting to the source database");

            (string sourceConnectionString, DbProviderFactory sourceDbProviderFactory, ConfigurationDataProvider sourceConfigurationDataProvider) =
                await ResolveConnectionParametersAsync(sourceConfigurationString, sourceDatabaseName);

            await logger.InfoAsync(sourceConnectionString);

            var sourceDbScope = new DbScope(() =>
                new DbManager(sourceConfigurationDataProvider.GetDataProvider(sourceConnectionString), sourceConnectionString));

            var sourceDatabaseSchemeService = new DatabaseSchemeService(
                sourceDbScope,
                new ServerConfigurationVersionProvider(sourceDbScope));

            if (!await sourceDatabaseSchemeService.IsStorageExistsAsync(cancellationToken))
            {
                await logger.ErrorAsync("Scheme doesn't exists in the source database");
                return -1;
            }

            if (!await sourceDatabaseSchemeService.IsStorageUpToDateAsync(cancellationToken))
            {
                await logger.ErrorAsync("Scheme isn't up-to-date in the source database");
                return -2;
            }

            await logger.InfoAsync("Loading scheme information");

            // накатываем схему, в которой будут все таблицы без констрейнтов и индексов,
            // а также функции и процедуры (миграций не будет)

            SchemeDatabase database = new SchemeDatabase(DatabaseNames.Original);
            await database.RefreshAsync(sourceDatabaseSchemeService, cancellationToken);

            SchemeDatabase databaseFinal = new SchemeDatabase(DatabaseNames.Original);
            await databaseFinal.RefreshAsync(database, cancellationToken);
            // должна была работать вот такая команда, но она падает: databaseFinal.Copy(database, fullCopy: true);

            // игнорируем:
            // - виртуальные таблицы
            // - таблицы-перечисления, в которых в схеме присутствует хотя бы одна запись
            // - системные таблицы схемы и прочие игнорируемые tablesToExclude

            List<SchemeTable> tables = database.Tables.OrderBy(x => x.Name).ToList();
            tables.RemoveAll(x => x.IsVirtual || x.ContentType == SchemeTableContentType.Enumeration && x.Records.Any() || tablesToExclude.Contains(x.Name));

            foreach (SchemeTable table in tables)
            {
                if (!table.IsSealed)
                {
                    // отключаем внешние ключи во всех комлексных колонках, в которых можем;
                    // их нельзя отключить в таблицах-карточках ID -> Instances, аналогично в файлах/заданиях, в локализации и в др. системных таблицах;
                    // после отключения ключей не будет циклических зависимостей по FK между таблицами, и мы сможем их отсортировать для LOGGED/UNLOGGED в Postgres
                    foreach (SchemeColumn column in table.Columns)
                    {
                        if (!column.IsSealed
                            && column is SchemeComplexColumn { WithForeignKey: true } complexColumn)
                        {
                            complexColumn.WithForeignKey = false;
                        }
                    }
                }
            }

            database.Migrations.RemoveAll(x => !x.IsSystem);

            await logger.InfoAsync("Connecting to the target database");

            (string targetConnectionString, DbProviderFactory targetDbProviderFactory, ConfigurationDataProvider targetConfigurationDataProvider) =
                await ResolveConnectionParametersAsync(targetConfigurationString, targetDatabaseName);

            await logger.InfoAsync(targetConnectionString);

            var targetDbScope = new DbScope(() =>
                new DbManager(targetConfigurationDataProvider.GetDataProvider(targetConnectionString), targetConnectionString));

            var targetDatabaseSchemeService = new DatabaseSchemeService(
                targetDbScope,
                new ServerConfigurationVersionProvider(targetDbScope));

            if (!await targetDatabaseSchemeService.IsStorageExistsAsync(cancellationToken))
            {
                await logger.InfoAsync("Scheme doesn't exists in the target database, creating it");
                await targetDatabaseSchemeService.CreateStorageAsync(cancellationToken);
            }

            if (!await targetDatabaseSchemeService.IsStorageUpToDateAsync(cancellationToken))
            {
                await logger.InfoAsync("Scheme isn't up-to-date in the target database, upgrading it");
                await targetDatabaseSchemeService.UpdateStorageAsync(cancellationToken);
            }

            await logger.InfoAsync("Importing initial scheme to the target database");
            await database.SubmitChangesAsync(targetDatabaseSchemeService, cancellationToken);

            // переносим данные таблиц из исходной базы в целевую

            // это временные переменные, в которых будут записываться disposable-объекты до того, как они попадут в contextArray
            DbConnection sourceConnection = null;
            DbManager sourceDb = null;
            Dbms sourceDbms = Dbms.Unknown;

            DbConnection targetConnection = null;
            DbManager targetDb = null;
            Dbms targetDbms = Dbms.Unknown;

            // здесь содержатся объекты для подключения к базам данных из разных потоков
            var contextArray = new TaskDbContext[threadCount];

            try
            {
                for (int threadIndex = 0; threadIndex < contextArray.Length; threadIndex++)
                {
                    await logger.InfoAsync("Preparing connections to the source database: {0}", threadIndex + 1);

                    sourceConnection = CreateDbConnection(sourceDbProviderFactory, sourceConnectionString);
                    sourceDb = new DbManager(sourceConfigurationDataProvider.GetDataProvider(sourceConnectionString), sourceConnection);

                    if (threadIndex == 0)
                    {
                        sourceDbms = sourceDb.GetDbms();

                        if (sourceDbms == Dbms.Unknown)
                        {
                            await logger.ErrorAsync(
                                "Unknown database management system for the source connection:{0}{1}",
                                Environment.NewLine,
                                sourceConnectionString);

                            return -3;
                        }
                    }

                    IQueryBuilderFactory sourceBuilderFactory = new QueryBuilderFactory(sourceDbms);

                    await logger.InfoAsync("Preparing connections to the target database: {0}", threadIndex + 1);

                    targetConnection = CreateDbConnection(targetDbProviderFactory, targetConnectionString);
                    targetDb = new DbManager(targetConfigurationDataProvider.GetDataProvider(targetConnectionString), targetConnection);

                    IQueryBuilderFactory targetBuilderFactory;

                    if (threadIndex == 0)
                    {
                        targetDbms = targetDb.GetDbms();

                        if (targetDbms == Dbms.Unknown)
                        {
                            await logger.ErrorAsync(
                                "Unknown database management system for the target connection:{0}{1}",
                                Environment.NewLine,
                                targetConnectionString);

                            return -3;
                        }

                        // в базе Postgres нельзя хранить файлы, поэтому не переносим их содержимое из исходной базы
                        if (targetDbms == Dbms.PostgreSql)
                        {
                            tables.RemoveAll(x => x.Name == Names.FileContent);
                        }

                        // отключаем внешние ключи на таблицах, т.к. не все из них можно отключить через схему
                        targetBuilderFactory = new QueryBuilderFactory(targetDbms);
                        await SetupIndicesForeignKeysAndTableLoggingAsync(false, logger, targetDbms, targetDb, targetBuilderFactory, tables, cancellationToken);
                    }
                    else
                    {
                        targetBuilderFactory = new QueryBuilderFactory(targetDbms);
                    }

                    IBulkInsertExecutor insertExecutor = container.Resolve<IBulkInsertExecutor>();

                    var context = new TaskDbContext(sourceDb, sourceBuilderFactory, sourceDbms, targetDb, targetBuilderFactory, targetDbms, insertExecutor, bulkSize);
                    contextArray[threadIndex] = context;

                    sourceDb = null;
                    sourceConnection = null;
                    targetDb = null;
                    targetConnection = null;
                }

                await logger.InfoAsync("Migrating table data");
                await MigrateDataAsync(logger, contextArray, tables, cancellationToken);

                if (contextArray.Length > 0)
                {
                    // включаем внешние ключи на таблицах, которые были отключены выше перед миграцией
                    TaskDbContext context = contextArray[0];
                    await SetupIndicesForeignKeysAndTableLoggingAsync(true, logger, targetDbms, context.TargetDb, context.TargetBuilderFactory, tables, cancellationToken);

                    // асинхронно закрываем соединения
                    foreach (TaskDbContext dbContext in contextArray)
                    {
                        await dbContext.SourceDb.CloseAsync();
                        await dbContext.TargetDb.CloseAsync();
                    }
                }
            }
            finally
            {
                await logger.InfoAsync("Closing connections");

                if (targetDb is not null)
                {
                    await targetDb.DisposeAsync();
                }

                targetConnection?.Dispose();

                if (sourceDb is not null)
                {
                    await sourceDb.DisposeAsync();
                }

                sourceConnection?.Dispose();

                foreach (TaskDbContext context in contextArray)
                {
                    if (context is not null)
                    {
                        await context.TargetDb.DisposeAsync();
                        await context.SourceDb.DisposeAsync();
                    }
                }
            }

            // импортируем исходную схему, в которой есть все внешние ключи, индексы, миграции и др.
            // при этом схема должна сгенерировать ALTER TABLE-ы для таблиц, и создать/выполнить всё остальное

            await logger.InfoAsync("Importing actual scheme to the target database");
            await databaseFinal.SubmitChangesAsync(targetDatabaseSchemeService, cancellationToken);

            await logger.InfoAsync("Migration has been completed");
            return 0;
        }

        #endregion
    }
}
