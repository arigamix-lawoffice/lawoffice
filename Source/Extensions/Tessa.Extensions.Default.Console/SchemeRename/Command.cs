using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using NLog;
using Tessa.Localization;
using Tessa.Platform.CommandLine;
using Tessa.Platform.ConsoleApps;
using Tessa.Platform.Data;

namespace Tessa.Extensions.Default.Console.SchemeRename
{
    public static class Command
    {
        [Verb("SchemeRename")]
        [LocalizableDescription("Common_CLI_Scheme_Rename")]
        public static async Task SchemeRename(
            [Output] TextWriter stdOut,
            [Error] TextWriter stdErr,
            [Argument, LocalizableDescription("Common_CLI_Scheme_X_Source")] string source,
            [Argument("t"), LocalizableDescription("Common_CLI_Scheme_Rename_TableName")] string tableName,
            [Argument("c"), LocalizableDescription("Common_CLI_Scheme_Rename_ColumnName")] string columnName,
            [Argument("out"), LocalizableDescription("Common_CLI_Scheme_Rename_Out")] string target = null,
            [Argument("dbms"), LocalizableDescription("Common_CLI_Scheme_Script_DBMS")] Dbms dbms = Dbms.SqlServer,
            [Argument("include"), LocalizableDescription("Common_CLI_Scheme_IncludePartition")] IEnumerable<string> includedPartitions = null,
            [Argument("exclude"), LocalizableDescription("Common_CLI_Scheme_ExcludePartition")] IEnumerable<string> excludedPartitions = null,
            [Argument("q"), LocalizableDescription("Common_CLI_Quiet")] bool quiet = false,
            [Argument("nologo")] [LocalizableDescription("CLI_NoLogo")] bool nologo = false)
        {
            if (!nologo && !quiet)
            {
                ConsoleAppHelper.WriteLogo(stdOut);
            }

            IConsoleLogger logger = new ConsoleLogger(LogManager.GetLogger(nameof(SchemeRename)), stdOut, stdErr, quiet);

            int result = await Operation.ExecuteAsync(logger, stdOut, source, tableName, columnName, target, dbms, includedPartitions, excludedPartitions);
            ConsoleAppHelper.EnvironmentExit(result);
        }
    }
}