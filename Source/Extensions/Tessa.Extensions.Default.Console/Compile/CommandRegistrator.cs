#nullable enable

using System;
using System.Collections.Generic;
using System.IO;
using Tessa.Platform.CommandLine;
using Tessa.Platform.ConsoleApps;

namespace Tessa.Extensions.Default.Console.Compile
{
    [ConsoleRegistrator]
    public sealed class CommandRegistrator :
        ConsoleRegistratorBase
    {
        public override void RegisterCommands()
        {
            this.CommandContext
                !.AddCommand<TextWriter, TextWriter, IEnumerable<string>?, IEnumerable<Guid>?, bool, string, string, string, string, bool, bool>(Command.Compile)
                ;
        }
    }
}
