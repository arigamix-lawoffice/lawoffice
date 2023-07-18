using System;
using System.IO;
using System.Threading.Tasks;
using NLog;
using Tessa.Localization;
using Tessa.Platform.CommandLine;
using Tessa.Platform.ConsoleApps;

namespace Tessa.Extensions.Default.Console.MigrateFiles
{
    public static class Command
    {
        [Verb("MigrateFiles")]
        [LocalizableDescription("Common_CLI_MigrateFiles")]
        public static async Task MigrateFiles(
            [Output] TextWriter stdOut,
            [Error] TextWriter stdErr,
            [Argument("from"), LocalizableDescription("Common_CLI_FileSourceFrom")] int fromSourceID,
            [Argument("to"), LocalizableDescription("Common_CLI_FileSourceTo")] int toSourceID,
            [Argument("cs"), LocalizableDescription("Common_CLI_ConfigurationString")] string configurationString = null,
            [Argument("db"), LocalizableDescription("Common_CLI_DatabaseName")] string databaseName = null,
            [Argument("p"), LocalizableDescription("Common_CLI_ConcurrentConnectionCount")] int threadCount = 0,
            [Argument("c"), LocalizableDescription("Common_CLI_RemoveFromTargetFileSource")] bool removeFromTargetFileSource = false,
            [Argument("nocancel"), LocalizableDescription("Common_CLI_NoCancel")] bool noCancel = false,
            [Argument("q"), LocalizableDescription("Common_CLI_Quiet")] bool quiet = false,
            [Argument("nologo")] [LocalizableDescription("CLI_NoLogo")] bool nologo = false)
        {
            if (!nologo && !quiet)
            {
                ConsoleAppHelper.WriteLogo(stdOut);
            }

            if (threadCount <= 0)
            {
                threadCount = Environment.ProcessorCount;
            }

            bool canCancel = !noCancel;

            IConsoleLogger logger = new ConsoleLogger(LogManager.GetLogger(nameof(MigrateFiles)), stdOut, stdErr, quiet, synchronized: true);

            int result = await Operation.ExecuteAsync(
                logger, configurationString, databaseName, fromSourceID, toSourceID,
                threadCount, removeFromTargetFileSource, canCancel, quiet);

            ConsoleAppHelper.EnvironmentExit(result);
        }
    }
}