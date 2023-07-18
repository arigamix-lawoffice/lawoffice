using System.Collections.Generic;
using System.IO;
using Tessa.Platform.CommandLine;
using Tessa.Platform.ConsoleApps;

namespace Tessa.Extensions.Default.Console.SqlScript
{
    [ConsoleRegistrator]
    public sealed class CommandRegistrator :
        ConsoleRegistratorBase
    {
        public override void RegisterCommands()
        {
            this.CommandContext
                .AddCommand<TextWriter, TextWriter, IEnumerable<string>, string, string, int, string, bool, bool, IEnumerable<string>, bool, bool>(Command.SelectScript)
                .AddCommand<TextWriter, TextWriter, IEnumerable<string>, string, string, IEnumerable<string>, bool, bool>(Command.SqlScript)
                ;
        }
    }
}