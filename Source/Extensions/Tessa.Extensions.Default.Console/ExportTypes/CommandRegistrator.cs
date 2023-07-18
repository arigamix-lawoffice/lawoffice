using System.Collections.Generic;
using System.IO;
using Tessa.Cards;
using Tessa.Platform.CommandLine;
using Tessa.Platform.ConsoleApps;

namespace Tessa.Extensions.Default.Console.ExportTypes
{
    [ConsoleRegistrator]
    public sealed class CommandRegistrator :
        ConsoleRegistratorBase
    {
        public override void RegisterCommands()
        {
            this.CommandContext
                .AddCommand<TextWriter, TextWriter, IEnumerable<string>, string, string, string, string, string, bool, bool, CardInstanceType?, bool, bool>(Command.ExportTypes)
                ;
        }
    }
}