using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Tessa.Localization;
using Tessa.Platform.CommandLine;
using Tessa.Platform.ConsoleApps;

namespace Tessa.Extensions.Default.Console.Script
{
    public static class Command
    {
        [Verb("Script")]
        [LocalizableDescription("Common_CLI_Script")]
        public static async Task Script(
            [Input] TextReader input,
            [Output] TextWriter stdOut,
            [Error] TextWriter stdErr,
            [Argument] [LocalizableDescription("Common_CLI_ScriptNames")] IEnumerable<string> scriptName = null,
            [Argument("a")] [LocalizableDescription("Common_CLI_Address")] string address = null,
            [Argument("i")] [LocalizableDescription("Common_CLI_Instance")] string instanceName = null,
            [Argument("u")] [LocalizableDescription("Common_CLI_UserName")] string userName = null,
            [Argument("p")] [LocalizableDescription("Common_CLI_Password")] string password = null,
            [Argument("cs"), LocalizableDescription("Common_CLI_ConfigurationString")] string configurationString = null,
            [Argument("db"), LocalizableDescription("Common_CLI_DatabaseName")] string databaseName = null,
            [Argument("pp"), LocalizableDescription("Common_CLI_ParametersScript")] IEnumerable<string> parameters = null,
            [Argument("help"), LocalizableDescription("Common_CLI_ScriptHelp")] bool? help = null,
            [Argument("q"), LocalizableDescription("Common_CLI_Quiet")] bool quiet = false,
            [Argument("nologo")] [LocalizableDescription("CLI_NoLogo")] bool nologo = false)
        {
            if (!nologo && !quiet)
            {
                ConsoleAppHelper.WriteLogo(stdOut);
            }

            string[] scriptsArray = scriptName?.ToArray() ?? Array.Empty<string>();
            if (scriptsArray.Length == 0)
            {
                var (allScriptFuncs, _) = await ConsoleScriptHelper.FindAndGetScriptsAsync(null, null);
                string[] allScripts = allScriptFuncs.Keys.OrderBy(x => x).ToArray();
                if (allScripts.Length == 0)
                {
                    await stdOut.WriteLineAsync("No scripts has been found.");

                    ConsoleAppHelper.EnvironmentExit(-1);
                    return;
                }

                if (!quiet)
                {
                    await stdOut.WriteLineAsync("List of all available scripts:");
                    await stdOut.WriteLineAsync();
                }

                for (int i = 0; i < allScripts.Length; i++)
                {
                    await stdOut.WriteLineAsync(allScripts[i]);
                }

                if (!quiet)
                {
                    await stdOut.WriteLineAsync();
                    await stdOut.WriteLineAsync("Use \"ScriptName --help\" to show its help.");
                }

                ConsoleAppHelper.EnvironmentExit(0);
                return;
            }

            var scripts = new HashSet<string>(scriptsArray, StringComparer.OrdinalIgnoreCase);
            (Dictionary<string, Func<IConsoleScript>> actualScripts, _) = await ConsoleScriptHelper.FindAndGetScriptsAsync(scripts, null);

            for (int i = 0; i < scriptsArray.Length; i++)
            {
                if (!actualScripts.ContainsKey(scriptsArray[i]))
                {
                    throw new InvalidOperationException("Can't find script by name: " + scriptsArray[i]);
                }
            }

            var options = new ConsoleScriptOptions
            {
                Input = input,
                StdOut = stdOut,
                StdErr = stdErr,
                Address = address,
                InstanceName = instanceName,
                UserName = userName,
                Password = password,
                ConfigurationString = configurationString,
                DatabaseName = databaseName,
                Parameters = DefaultConsoleHelper.ParseParameters(parameters, StringComparer.OrdinalIgnoreCase),
                Quiet = quiet,
                Help = help.HasValue, // вызов --help устанавливает help == false, но это всё равно отличается от help == null
            };

            int result = 0;

            IConsoleScript[] scriptInstances = scriptsArray.Select(name => actualScripts[name]()).ToArray();
            for (int i = 0; i < scriptInstances.Length; i++)
            {
                scriptInstances[i].ScriptName = scriptsArray[i];

                result = await scriptInstances[i].ExecuteAsync(options);
                if (result < 0)
                {
                    ConsoleAppHelper.EnvironmentExit(result);
                    return;
                }
            }

            ConsoleAppHelper.EnvironmentExit(result);
        }
    }
}