using System.Collections.Generic;
using System.IO;
using Tessa.Platform.CommandLine;
using Tessa.Platform.ConsoleApps;

namespace Tessa.Extensions.Default.Console.Script
{
    [ConsoleRegistrator]
    public sealed class CommandRegistrator :
        ConsoleRegistratorBase
    {
        public override void RegisterCommands()
        {
            this.CommandContext
                .AddCommand<TextReader, TextWriter, TextWriter, IEnumerable<string>, string, string, string, string, string, string, IEnumerable<string>, bool?, bool, bool>(
                    Command.Script)
                ;
        }
    }
}