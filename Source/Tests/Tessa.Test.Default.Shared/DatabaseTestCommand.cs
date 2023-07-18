using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using NUnit.Framework.Internal.Commands;
using Tessa.Platform;
using Tessa.Platform.Data;

namespace Tessa.Test.Default.Shared
{
    /// <summary>
    /// Команда выполняющая удаление, создание и инициализацию тестовой базы данных.
    /// </summary>
    public sealed class DatabaseTestCommand :
        DelegatingTestCommand
    {
        #region Nested Types

        private struct CommandScope :
            IDisposable
        {
            #region Fields

            private readonly DbConnection connection;

            private DbCommand command;

            #endregion

            #region Constructors

            public CommandScope(DbProviderFactory factory, string connectionString)
            {
                if (factory is null)
                {
                    throw new ArgumentNullException(nameof(factory));
                }

                this.connection = factory.CreateConnection()
                    ?? throw new ArgumentException("Can't creation connection with " + factory.GetType().FullName, nameof(factory));

                this.connection.ConnectionString = connectionString;
                this.command = null;
            }

            #endregion

            #region IDisposable Overrides

            public void Dispose()
            {
                this.command?.Dispose();
                this.connection?.Dispose();
            }

            #endregion

            #region Properties

            public DbCommand Command
            {
                get
                {
                    if (this.command == null)
                    {
                        this.command = this.connection.CreateCommand();
                        this.connection.Open();
                    }

                    return this.command;
                }
            }

            #endregion
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="DatabaseTestCommand"/>.
        /// </summary>
        /// <param name="innerCommand">Внутренняя команда.</param>
        public DatabaseTestCommand(TestCommand innerCommand)
            : base(innerCommand)
        {
        }

        #endregion

        #region TestCommand Overrides

        /// <inheritdoc/>
        public override TestResult Execute(TestExecutionContext context) =>
            this.ExecuteCoreAsync(NotNullOrThrow(context)).GetAwaiter().GetResult();

        #endregion

        #region Private methods

        private async Task<TestResult> ExecuteCoreAsync(TestExecutionContext context)
        {
            var test = this.Test;
            var properties = test.Properties;

            var connectionString = (string) properties.Get(DatabasePropertyNames.ConnectionString);
            if (connectionString == null)
            {
                return SkipExecution(context, "Connection string is not specified");
            }

            var providerName = (string) properties.Get(DatabasePropertyNames.ProviderName);

            DbProviderFactory factory = ConfigurationManager
                .GetConfigurationDataProviderFromType(providerName)
                .GetDbProviderFactory();

            DbConnectionStringBuilder builder = CreateConnectionStringBuilderChecked(factory);
            builder.ConnectionString = connectionString;

            Dbms dbms = factory.GetDbms();

            string databaseName;
            switch (dbms)
            {
                case Dbms.SqlServer:
                    databaseName = (string) builder["Initial Catalog"];
                    builder["Database"] = "master";
                    break;

                case Dbms.PostgreSql:
                    databaseName = (string) builder["Database"];
                    builder["Database"] = "postgres";
                    break;

                default:
                    throw new NotSupportedException("Unknown database type.");
            }

            var masterConnectionString = builder.ConnectionString;
            var scriptFileNames = properties[DatabasePropertyNames.ScriptFileNamesProvider].Cast<string>();
            try
            {
                using (var scope = new CommandScope(factory, masterConnectionString))
                {
                    await DatabaseHelper.DropDatabaseAsync(scope.Command, dbms, databaseName);
                    await DatabaseHelper.CreateDatabaseAsync(scope.Command, dbms, databaseName);
                }

                using (var scope = new CommandScope(factory, connectionString))
                {
                    await PrepareDatabaseAsync(scope.Command, dbms, scriptFileNames, test.TypeInfo.Assembly);
                }

                context.CurrentResult = this.innerCommand.Execute(context);
            }
            catch (OperationCanceledException)
            {
                // ignored
            }
            catch (Exception ex)
            {
                context.CurrentResult.RecordException(ex);
            }
            finally
            {
                using var scope = new CommandScope(factory, masterConnectionString);
                await DatabaseHelper.DropDatabaseAsync(scope.Command, dbms, databaseName);
            }

            return context.CurrentResult;
        }

        private static DbConnectionStringBuilder CreateConnectionStringBuilderChecked(DbProviderFactory factory) =>
            factory.CreateConnectionStringBuilder()
            ?? throw new InvalidOperationException(
                $"Method {nameof(factory.CreateConnectionStringBuilder)} has returned null for {factory.GetType().FullName}");

        private static TestResult SkipExecution(TestExecutionContext context, string reason)
        {
            var result = context.CurrentResult;
            result.SetResult(ResultState.NotRunnable, reason);
            return result;
        }

        private static async Task PrepareDatabaseAsync(
            DbCommand command,
            Dbms dbms,
            IEnumerable<string> scriptFileNames,
            Assembly assembly,
            CancellationToken cancellationToken = default)
        {
            var sqlTextScripts = TestHelper.GetSqlTextScripts(
                assembly,
                scriptFileNames);

            switch (dbms)
            {
                case Dbms.SqlServer:
                    foreach (var sqlText in sqlTextScripts)
                    {
                        var commands =
                            SqlHelper.SplitGo(sqlText)
                                .Select(x => x.Trim())
                                .Where(x => x.Length > 0);

                        foreach (var scriptCommand in commands)
                        {
                            command.CommandText = scriptCommand;
                            await command.ExecuteNonQueryAsync(cancellationToken);
                        }
                    }

                    break;

                case Dbms.PostgreSql:
                    foreach (var sqlText in sqlTextScripts)
                    {
                        command.CommandText = sqlText;
                        await command.ExecuteNonQueryAsync(cancellationToken);
                    }

                    break;
                default:
                    throw new NotSupportedException($"Dbms {dbms:G} is not supported.");
            }
        }

        #endregion
    }
}
