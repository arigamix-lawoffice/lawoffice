using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LinqToDB;
using LinqToDB.Data;
using Tessa.Platform;
using Tessa.Platform.Collections;
using Tessa.Platform.ConsoleApps;
using Tessa.Platform.Data;
using Tessa.Platform.Formatting;

namespace Tessa.Extensions.Default.Console.SqlScript
{
    public static class Operation
    {
        public static async Task<int> ExecuteAsync(
            IConsoleLogger logger,
            IEnumerable<string> source,
            string configurationString,
            string databaseName,
            IEnumerable<string> parameters,
            bool selectMode,
            bool csvResult = false,
            int topRowCount = 0,
            char csvSeparator = ';',
            bool showHeaders = false,
            CancellationToken cancellationToken = default)
        {
            string[] sourceArray = source as string[] ?? source?.ToArray() ?? Array.Empty<string>();
            if (sourceArray.Length > 0)
            {
                await logger.InfoAsync("Executing available scripts from \"{0}\"", string.Join(" ", sourceArray));
            }

            DbManager db = null;
            object outputResult = null;

            try
            {
                List<string> sourceFiles = DefaultConsoleHelper.GetSourceFiles(sourceArray, "*.sql");
                if (sourceFiles.Count > 0)
                {
                    foreach (string filePath in sourceFiles)
                    {
                        await logger.InfoAsync("Found script \"{0}\"", filePath);

                        string sqlText = await File.ReadAllTextAsync(filePath, cancellationToken);
                        db = await ExecuteScriptAsync(db, sqlText, cancellationToken);
                    }
                }
                else
                {
                    await logger.InfoAsync(
                        "Query text is expected from console input or previous chained command.{0}" +
                        "You can separate commands with \"GO\", starting from a new line (Enter, GO, Enter).{0}" +
                        "Use new line with EOF ({1}) to complete input. Ctrl+C to cancel.",
                        Environment.NewLine,
                        OperatingSystem.IsLinux() ? "Enter, Ctrl+D, Ctrl+D" : "Enter, Ctrl+Z, Enter");

                    string sqlText = (await System.Console.In.ReadToEndAsync()).Trim();
                    db = await ExecuteScriptAsync(null, sqlText, cancellationToken);
                }

                async Task<DbManager> ExecuteScriptAsync(DbManager dbParam, string sqlText, CancellationToken ct = default)
                {
                    if (string.IsNullOrWhiteSpace(sqlText))
                    {
                        await logger.InfoAsync("Script is empty");
                        return dbParam;
                    }

                    sqlText = sqlText.NormalizeLineEndingsOnCurrentPlatform();

                    string[] sqlCommands = SqlHelper.SplitGo(sqlText).Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

                    if (dbParam == null)
                    {
                        await logger.InfoAsync(
                            string.IsNullOrEmpty(configurationString)
                                ? "Opening connection to default database"
                                : "Opening connection to database with connection \"{0}\"",
                            configurationString);

                        if (!string.IsNullOrEmpty(databaseName))
                        {
                            await logger.InfoAsync("Overriding connection to database \"{0}\"", databaseName);
                        }

                        dbParam = await ConsoleAppHelper.CreateDbManagerAsync(logger, configurationString, databaseName, ct);
                    }

                    if (sqlCommands.Length > 0)
                    {
                        var p = DefaultConsoleHelper.ParseParameters(parameters);
                        var dbParameters = new DataParameter[p.Count];

                        int index = 0;
                        foreach ((string key, string value) in p)
                        {
                            dbParameters[index++] = dbParam.Parameter(key, value, DataType.NVarChar);
                        }

                        for (int i = 0; i < sqlCommands.Length; i++)
                        {
                            if (sqlCommands.Length == 1 || selectMode)
                            {
                                await logger.InfoAsync("Executing command");
                            }
                            else
                            {
                                await logger.InfoAsync("Executing command {0} / {1}", i + 1, sqlCommands.Length);
                            }

                            if (i == 1)
                            {
                                // расширяем массив, чтобы добавить параметр @Result
                                Array.Resize(ref dbParameters, dbParameters.Length + 1);
                            }

                            string text = sqlCommands[i].Trim();

                            if (i >= 1)
                            {
                                dbParameters[^1] = dbParam.Parameter("Result", outputResult);

                                // эта штука актуальна для Postgres для команды REINDEX DATABASE dbname, где "dbname" не может быть параметром;
                                // если мы находим в тексте запрос не параметр @Result, а спец-строку @Result@, то мы явно её заменяем на текст
                                text = text.Replace("@Result@", FormattingHelper.FormatToString(outputResult) ?? string.Empty, StringComparison.Ordinal);
                            }

                            // пробелы мы уже отрезали и значения null исключили
                            if (text.StartsWith("--NORESULT", StringComparison.OrdinalIgnoreCase))
                            {
                                await logger.InfoAsync("Suppressing \"Result\" parameter with --NORESULT");
                                dbParam.SetCommand(text);
                            }
                            else
                            {
                                dbParam.SetCommand(text, dbParameters);
                            }

                            dbParam
                                .SetCommandTimeout(0)
                                .LogCommand();

                            bool lastCommand = i + 1 == sqlCommands.Length;
                            if (lastCommand && !selectMode)
                            {
                                // последняя команда в режиме EXECUTE не возвращает результат
                                await dbParam
                                    .ExecuteNonQueryAsync(ct);
                            }
                            else if (lastCommand && csvResult)
                            {
                                // в режиме SELECT последняя команда при выводе в CSV читает все строки и колонки
                                outputResult = await DefaultConsoleHelper.ExecuteReaderAndReturnCsvAsync(dbParam, csvSeparator, topRowCount, showHeaders, ct);
                            }
                            else
                            {
                                // в режиме SELECT без вывода в CSV выполняем последнюю команду и возвращаем результат от первой колонки в первой строке;

                                // в режиме EXECUTE и в SELECT для непоследней команды для команды читается результат,
                                // если он есть, для передачи в следующую команду как параметр @Result;
                                // если результата нет, то передаётся количество строк, как в ExecuteNonQuery

                                outputResult = await dbParam
                                    .ExecuteAsync<object>(ct);
                            }
                        }
                    }

                    return dbParam;
                }
            }
            finally
            {
                if (db != null)
                {
                    try
                    {
                        await db.CloseAsync();
                    }
                    finally
                    {
                        await db.DisposeAsync();
                        await logger.InfoAsync("Database connection is closed");
                    }
                }
            }

            await logger.InfoAsync("Scripts have been executed");

            if (selectMode)
            {
                string text = FormattingHelper.FormatToString(outputResult);
                await logger.WriteAsync(text);
            }

            return 0;
        }
    }
}