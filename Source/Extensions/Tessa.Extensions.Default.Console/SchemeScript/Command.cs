using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using NLog;
using Tessa.Localization;
using Tessa.Platform.CommandLine;
using Tessa.Platform.ConsoleApps;
using Tessa.Platform.Data;

namespace Tessa.Extensions.Default.Console.SchemeScript
{
    public static class Command
    {
        [Verb("SchemeScript")]
        [LocalizableDescription("Common_CLI_Scheme_Script")]
        public static async Task SchemeScript(
            [Output] TextWriter stdOut,
            [Error] TextWriter stdErr,
            [Argument, LocalizableDescription("Common_CLI_Scheme_X_Source")] string source,
            [Argument("out"), LocalizableDescription("Common_CLI_Scheme_Script_Out")] string target = null,
            [Argument("dbms"), LocalizableDescription("Common_CLI_Scheme_Script_DBMS")] Dbms dbms = Dbms.SqlServer,
            [Argument("dbmsv"), LocalizableDescription("Common_CLI_Scheme_Script_DBMSV"), TypeConverter(typeof(VersionConverter))] Version dbmsv = null,
            [Argument("include"), LocalizableDescription("Common_CLI_Scheme_IncludePartition")] IEnumerable<string> includedPartitions = null,
            [Argument("exclude"), LocalizableDescription("Common_CLI_Scheme_ExcludePartition")] IEnumerable<string> excludedPartitions = null,
            [Argument("notran"), LocalizableDescription("Common_CLI_Scheme_Script_NoTran")] bool withoutTransactions = false,
            [Argument("q"), LocalizableDescription("Common_CLI_Quiet")] bool quiet = false,
            [Argument("nologo")] [LocalizableDescription("CLI_NoLogo")] bool nologo = false)
        {
            if (!nologo && !quiet)
            {
                ConsoleAppHelper.WriteLogo(stdOut);
            }

            IConsoleLogger logger = new ConsoleLogger(LogManager.GetLogger(nameof(SchemeScript)), stdOut, stdErr, quiet);

            int result = await Operation.ExecuteAsync(logger, stdOut, source, target, dbms, dbmsv, includedPartitions, excludedPartitions, withoutTransactions);
            ConsoleAppHelper.EnvironmentExit(result);
        }
    }
}