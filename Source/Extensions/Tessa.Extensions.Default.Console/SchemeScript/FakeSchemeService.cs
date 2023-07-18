using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Platform.Data;
using Tessa.Platform.Data.Fake;
using Tessa.Platform.Data.Fake.Npgsql;
using Tessa.Platform.Data.Fake.SqlClient;
using Tessa.Platform.Runtime;
using Tessa.Scheme;

namespace Tessa.Extensions.Default.Console.SchemeScript
{
    public sealed class FakeSchemeService : SystemSchemeService
    {
        #region Fields

        private readonly TextWriter output;
        private readonly Version dbmsVersion;
        private readonly DatabaseSchemeService service;
        private DatabaseSchemeService.KnownTables tables;

        #endregion

        #region Constructors

        public FakeSchemeService(TextWriter output, bool outputTransactions, Dbms dbms, Version dbmsVersion)
            : base(SchemeServiceOptions.None)
        {
            this.output = output;
            this.dbmsVersion = dbmsVersion;
            FakeConnection connection = CreateConnection(dbms, output, outputTransactions, executor => executor
                .Query(SqlHelper.GetDatabaseVersionQuery(dbms), this.GetDbmsVersion)
                .Query(DatabaseSchemeService.TableExistsQuery(dbms), this.TableExists)
                .Execute(DatabaseSchemeService.SaveTableQuery(dbms, false), this.SaveTable)
                .Execute(DatabaseSchemeService.RemoveTableQuery(dbms), this.RemoveTable));

            var dbScope = new DbScope(() =>
            {
                return connection.AsDbManager();
            });
            this.service = new DatabaseSchemeService(
                dbScope,
                new FakeConfigurationVersionProvider(),
                currentTime: () => new DateTime(2014, 08, 21, 0, 0, 0, DateTimeKind.Utc),
                skipUpdateModifiedByProperties: true);
        }

        private FakeDataReader GetDbmsVersion(FakeCommand command) =>
            new FakeDataReader(new object[,] { { this.dbmsVersion.ToString() } });

        private FakeDataReader TableExists(FakeCommand command)
        {
            var table = ToTable((string) command.Parameters[0].Value);
            return new FakeDataReader(new object[,] { { (this.tables & table) == table } });
        }

        private int SaveTable(FakeCommand command)
        {
            this.tables |= ToTable((Guid) command.Parameters[0].Value);
            return 1;
        }

        private int RemoveTable(FakeCommand command)
        {
            this.tables &= ~ToTable((Guid) command.Parameters[0].Value);
            return 1;
        }

        #endregion

        #region SystemSchemeService Overrides

        #region Database Members

        protected override ValueTask<SchemeDatabaseProperties> GetDatabasePropertiesOverrideAsync(CancellationToken cancellationToken = default)
        {
            return this.service.GetDatabasePropertiesAsync(cancellationToken);
        }

        protected override Task SaveDatabasePropertiesOverrideAsync(SchemeDatabaseProperties properties, CancellationToken cancellationToken = default)
        {
            WriteComment(this.output, properties.Name);
            return this.service.SaveDatabasePropertiesAsync(properties, cancellationToken);
        }

        #endregion

        #region Partition Members

        protected override ValueTask<SchemePartition> GetPartitionOverrideAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return this.service.GetPartitionAsync(id, cancellationToken);
        }

        protected override ValueTask<SchemePartition> GetPartitionOverrideAsync(string name, CancellationToken cancellationToken = default)
        {
            return this.service.GetPartitionAsync(name, cancellationToken);
        }

        protected override ValueTask<IEnumerable<SchemePartition>> GetPartitionsOverrideAsync(CancellationToken cancellationToken = default)
        {
            return this.service.GetPartitionsAsync(cancellationToken);
        }

        protected override Task SavePartitionOverrideAsync(SchemePartition partition, CancellationToken cancellationToken = default)
        {
            WriteComment(this.output, partition.Name);
            return this.service.SavePartitionAsync(partition, cancellationToken);
        }

        protected override Task<bool> RemovePartitionOverrideAsync(SchemePartition partition, CancellationToken cancellationToken = default)
        {
            WriteComment(this.output, partition.Name);
            return this.service.RemovePartitionAsync(partition, cancellationToken);
        }

        #endregion

        #region Table Members

        protected override ValueTask<SchemeTable> GetTableOverrideAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return this.service.GetTableAsync(id, cancellationToken);
        }

        protected override ValueTask<SchemeTable> GetTableOverrideAsync(string name, CancellationToken cancellationToken = default)
        {
            return this.service.GetTableAsync(name, cancellationToken);
        }

        protected override ValueTask<IEnumerable<SchemeTable>> GetTablesOverrideAsync(CancellationToken cancellationToken = default)
        {
            return this.service.GetTablesAsync(cancellationToken);
        }

        protected override Task SaveTableOverrideAsync(SchemeTable table, CancellationToken cancellationToken = default)
        {
            WriteComment(this.output, table.Name);
            this.tables |= ToTable(table.ID);
            return this.service.SaveTableAsync(table, cancellationToken);
        }

        protected override Task<bool> RemoveTableOverrideAsync(SchemeTable table, CancellationToken cancellationToken = default)
        {
            WriteComment(this.output, table.Name);
            this.tables &= ~ToTable(table.ID);
            return this.service.RemoveTableAsync(table, cancellationToken);
        }

        #endregion

        #region Procedure Members

        protected override ValueTask<SchemeProcedure> GetProcedureOverrideAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return this.service.GetProcedureAsync(id, cancellationToken);
        }

        protected override ValueTask<SchemeProcedure> GetProcedureOverrideAsync(string name, CancellationToken cancellationToken = default)
        {
            return this.service.GetProcedureAsync(name, cancellationToken);
        }

        protected override ValueTask<IEnumerable<SchemeProcedure>> GetProceduresOverrideAsync(CancellationToken cancellationToken = default)
        {
            return this.service.GetProceduresAsync(cancellationToken);
        }

        protected override Task SaveProcedureOverrideAsync(SchemeProcedure procedure, CancellationToken cancellationToken = default)
        {
            WriteComment(this.output, procedure.Name);
            return this.service.SaveProcedureAsync(procedure, cancellationToken);
        }

        protected override Task<bool> RemoveProcedureOverrideAsync(SchemeProcedure procedure, CancellationToken cancellationToken = default)
        {
            WriteComment(this.output, procedure.Name);
            return this.service.RemoveProcedureAsync(procedure, cancellationToken);
        }

        #endregion

        #region Function Members

        protected override ValueTask<SchemeFunction> GetFunctionOverrideAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return this.service.GetFunctionAsync(id, cancellationToken);
        }

        protected override ValueTask<SchemeFunction> GetFunctionOverrideAsync(string name, CancellationToken cancellationToken = default)
        {
            return this.service.GetFunctionAsync(name, cancellationToken);
        }

        protected override ValueTask<IEnumerable<SchemeFunction>> GetFunctionsOverrideAsync(CancellationToken cancellationToken = default)
        {
            return this.service.GetFunctionsAsync(cancellationToken);
        }

        protected override Task SaveFunctionOverrideAsync(SchemeFunction function, CancellationToken cancellationToken = default)
        {
            WriteComment(this.output, function.Name);
            return this.service.SaveFunctionAsync(function, cancellationToken);
        }

        protected override Task<bool> RemoveFunctionOverrideAsync(SchemeFunction function, CancellationToken cancellationToken = default)
        {
            WriteComment(this.output, function.Name);
            return this.service.RemoveFunctionAsync(function, cancellationToken);
        }

        #endregion

        #region Migration Members

        protected override ValueTask<SchemeMigration> GetMigrationOverrideAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return this.service.GetMigrationAsync(id, cancellationToken);
        }

        protected override ValueTask<SchemeMigration> GetMigrationOverrideAsync(string name, CancellationToken cancellationToken = default)
        {
            return this.service.GetMigrationAsync(name, cancellationToken);
        }

        protected override ValueTask<IEnumerable<SchemeMigration>> GetMigrationsOverrideAsync(CancellationToken cancellationToken = default)
        {
            return this.service.GetMigrationsAsync(cancellationToken);
        }

        protected override Task SaveMigrationOverrideAsync(SchemeMigration migration, CancellationToken cancellationToken = default)
        {
            WriteComment(this.output, migration.Name);
            return this.service.SaveMigrationAsync(migration, cancellationToken);
        }

        protected override Task<bool> RemoveMigrationOverrideAsync(SchemeMigration migration, CancellationToken cancellationToken = default)
        {
            WriteComment(this.output, migration.Name);
            return this.service.RemoveMigrationAsync(migration, cancellationToken);
        }

        #endregion

        #region Storage Members

        public override ValueTask<bool> IsStorageExistsAsync(CancellationToken cancellationToken = default) =>
            this.service.IsStorageExistsAsync(cancellationToken);

        protected override Task CreateStorageOverrideAsync(CancellationToken cancellationToken = default) =>
            this.service.CreateStorageAsync(cancellationToken);

        public override ValueTask<Version> GetStorageVersionAsync(CancellationToken cancellationToken = default) =>
            this.service.GetStorageVersionAsync(cancellationToken);

        protected override Task UpdateStorageOverrideAsync(CancellationToken cancellationToken = default) =>
            this.service.UpdateStorageAsync(cancellationToken);

        protected override ValueTask InitializeOverrideAsync(CancellationToken cancellationToken = default) =>
            new ValueTask();

        protected override SchemeOperationScope CreateOperationScopeOverride(string operation, SchemeObject obj) => null;

        #endregion

        #region Cache Members

        /// <inheritdoc/>
        public override Task InvalidateCacheAsync(CancellationToken cancellationToken = default) =>
            this.service.InvalidateCacheAsync(cancellationToken);

        /// <inheritdoc/>
        public override ValueTask<bool> InvalidateCacheIfChangedAsync(CancellationToken cancellationToken = default) =>
            this.service.InvalidateCacheIfChangedAsync(cancellationToken);

        /// <inheritdoc/>
        public override Task EnsureInvalidateCacheSubscribedAsync(CancellationToken cancellationToken = default) =>
            Task.CompletedTask;

        #endregion

        #endregion

        #region Methods

        private static FakeConnection CreateConnection(Dbms dbms, TextWriter output, bool outputTransactions, Action<FakeCommandExecutor> defineCommands)
        {
            switch (dbms)
            {
                case Dbms.SqlServer:
                    return new FakeSqlConnection(defineCommands, output) { OutputTransactions = outputTransactions };

                case Dbms.PostgreSql:
                    return new FakeNpgsqlConnection(defineCommands, output) { OutputTransactions = outputTransactions };

                default:
                    throw new NotSupportedException();
            }
        }

        private static DatabaseSchemeService.KnownTables ToTable(string name)
        {
            switch (name)
            {
                case Names.Configuration: return DatabaseSchemeService.KnownTables.Configuration;
                case Names.Scheme: return DatabaseSchemeService.KnownTables.Scheme;
                case Names.Partitions: return DatabaseSchemeService.KnownTables.Partitions;
                case Names.Tables: return DatabaseSchemeService.KnownTables.Tables;
                case Names.Procedures: return DatabaseSchemeService.KnownTables.Procedures;
                case Names.Functions: return DatabaseSchemeService.KnownTables.Functions;
                case Names.Migrations: return DatabaseSchemeService.KnownTables.Migrations;
                default: return DatabaseSchemeService.KnownTables.None;
            }
        }

        private static DatabaseSchemeService.KnownTables ToTable(Guid id)
        {
            if (id == SchemeGuids.Configuration)
            {
                return DatabaseSchemeService.KnownTables.Configuration;
            }

            if (id == SchemeGuids.Scheme)
            {
                return DatabaseSchemeService.KnownTables.Scheme;
            }

            if (id == SchemeGuids.Partitions)
            {
                return DatabaseSchemeService.KnownTables.Partitions;
            }

            if (id == SchemeGuids.Tables)
            {
                return DatabaseSchemeService.KnownTables.Tables;
            }

            if (id == SchemeGuids.Procedures)
            {
                return DatabaseSchemeService.KnownTables.Procedures;
            }

            if (id == SchemeGuids.Functions)
            {
                return DatabaseSchemeService.KnownTables.Functions;
            }

            if (id == SchemeGuids.Migrations)
            {
                return DatabaseSchemeService.KnownTables.Migrations;
            }

            return DatabaseSchemeService.KnownTables.None;
        }

        private static void WriteComment(TextWriter textWriter, string comment)
        {
            for (int i = 0; i < 80; i++)
            {
                textWriter.Write('-');
            }

            textWriter.WriteLine();
            textWriter.Write('-');
            textWriter.Write('-');
            textWriter.Write(' ');
            textWriter.WriteLine(comment);

            for (int i = 0; i < 80; i++)
            {
                textWriter.Write('-');
            }

            textWriter.WriteLine();
        }

        #endregion
    }
}
