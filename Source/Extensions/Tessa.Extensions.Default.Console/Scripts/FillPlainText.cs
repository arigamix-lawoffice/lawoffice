using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using LinqToDB;
using LinqToDB.Data;
using NLog;
using Tessa.Forums;
using Tessa.Forums.Models;
using Tessa.Platform.ConsoleApps;
using Tessa.Platform.Data;
using Unity;

namespace Tessa.Extensions.Default.Console.Scripts
{
    [ConsoleScript(nameof(FillPlainText))]
    public class FillPlainText :
        ServerConsoleScriptBase
    {
        #region Fields

        private IPlainMessageFiller plainMessageFiller;

        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        #endregion

        #region Private Methods

        private async Task FillPlainTextMessages(
            List<(Guid rowID, MessageType type, string body)> messages,
            string toTable,
            bool noWarn,
            CancellationToken cancellationToken)
        {
            // открытие соединений для обновления данных
            await using DbManager toDb = await this.CreateDbManagerAsync(cancellationToken);
            Dbms dbms = toDb.GetDbms();
            IQueryBuilderFactory builderFactory = new QueryBuilderFactory(dbms);

            string updateSqlText = builderFactory
                .Update(toTable)
                .C("PlainText").Assign().P("PlainText")
                .Where().C("RowID").Equals().P("RowID")
                .Build();

            var parameters = new DataParameter[2];
            parameters[0] = toDb.Parameter("RowID", DataType.Guid);
            parameters[1] = toDb.Parameter("PlainText", DataType.NVarChar);

            foreach ((Guid rowID, MessageType type, string body) in messages)
            {
                if (type == MessageType.Default &&
                    !string.IsNullOrEmpty(body))
                {
                    parameters[0].Value = rowID;
                    parameters[1].Value = this.plainMessageFiller.GetPlainMessage(body);

                    toDb
                        .SetCommand(updateSqlText, parameters)
                        .LogCommand();

                    int count = await toDb.ExecuteNonQueryAsync(cancellationToken);
                    if (count != 1 && !noWarn)
                    {
                        await this.Logger.InfoAsync(
                            $"Query expected to update one row but updated {count} rows.{Environment.NewLine}{toDb.GetCommandTextWithParameters()}{Environment.NewLine}");
                    }
                }
            }
        }

        private async Task<List<(Guid rowID, MessageType type, string body)>> GetMessages(string fromTable, CancellationToken cancellationToken)
        {
            // открытие соединений для чтения и записи данных
            await using DbManager fromDb = await this.CreateDbManagerAsync(cancellationToken);
            Dbms dbms = fromDb.GetDbms();
            IQueryBuilderFactory builderFactory = new QueryBuilderFactory(dbms);

            // подготовка запроса для чтения данных, не используем грязное чтение для одновременной записи
            // в ту же таблицу на MSSQL, чтобы не было артефактов при чтении из-за параллельных транзакций
            fromDb
                .SetCommand(
                    builderFactory
                        .Select().C(null, "RowID", "TypeID", "Body")
                        .From(fromTable)
                        .Build())
                .LogCommand()
                .WithoutTimeout();

            await using DbDataReader reader = await fromDb.ExecuteReaderAsync(CommandBehavior.SequentialAccess, cancellationToken);

            var result = new List<(Guid rowID, MessageType type, string body)>();
            while (await reader.ReadAsync(cancellationToken))
            {
                var rowID = reader.GetGuid(0);
                var type = (MessageType) reader.GetInt32(1);
                var body = await reader.GetSequentialStringAsync(2, cancellationToken);

                logger.Trace("Filling plain text RowID={0:D}, Type={1}", rowID, type);

                try
                {
                    MessageBody messageBody = ForumSerializationHelper.DeserializeMessageBody(body);
                    result.Add((rowID, type, messageBody.Text));
                }
                catch (Exception)
                {
                    await this.Logger.ErrorAsync($"Error while filling plain text RowID={rowID:D}, Type={type}. Body:{Environment.NewLine}{body}");
                    throw;
                }
            }

            return result;
        }

        #endregion

        #region Base Overrides

        protected override async ValueTask ExecuteCoreAsync(CancellationToken cancellationToken)
        {
            this.plainMessageFiller = this.Container.Resolve<IPlainMessageFiller>();

            string from = this.TryGetParameter("from");
            if (string.IsNullOrEmpty(from))
            {
                await this.Logger.ErrorAsync(
                    "Pass \"from\" parameter specifying table to get html text from" +
                    ", i.e.: -pp:from=FmMessages");
                this.Result = -1;
                return;
            }

            string to = this.TryGetParameter("to");
            if (string.IsNullOrEmpty(to))
            {
                await this.Logger.ErrorAsync(
                    "Pass \"to\" parameter specifying table to fill plain text to to" +
                    ", i.e.: -pp:to=FmMessagesTo");
                this.Result = -2;
                return;
            }

            // признак того, что блокируется вывод в консоль предупреждений о том, что в целевой таблице не найдена строка из исходной таблицы
            bool noWarn = this.ParameterIsNullOrEmpty("nowarn");

            List<(Guid rowID, MessageType type, string body)> messages = await this.GetMessages(from, cancellationToken);
            await this.FillPlainTextMessages(messages, to, noWarn, cancellationToken);
        }


        protected override async ValueTask ShowHelpCoreAsync(CancellationToken cancellationToken)
        {
            await this.Logger.WriteLineAsync("Fills plain text from html message body for messages in forums.");
            await this.Logger.WriteLineAsync();
            await this.Logger.WriteLineAsync("-pp:from=FmMessages - Specifies table with html message body columns.");
            await this.Logger.WriteLineAsync("-pp:to=FmMessagesTo - Specifies table to write converted plain text to.");
            await this.Logger.WriteLineAsync("[-pp:nowarn] - Disables output of warnings when \"from\" table contains rows that are absent in \"to\" table.");
            await this.Logger.WriteLineAsync();
            await this.Logger.WriteLineAsync("Example:");
            await this.Logger.WriteLineAsync(
                $"{Assembly.GetEntryAssembly()?.GetName().Name} Script {nameof(FillPlainText)}" +
                " -pp:from=FmMessages -pp:to=FmMessages -pp:nowarn");
        }

        #endregion
    }
}