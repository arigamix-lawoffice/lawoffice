using System.IO;
using Tessa.Extensions.Default.Console.GetKey;
using Tessa.Platform.CommandLine;
using Tessa.Platform.ConsoleApps;

namespace Tessa.Extensions.Default.Console.SetKey
{
    [ConsoleRegistrator]
    public sealed class CommandRegistrator :
        ConsoleRegistratorBase
    {
        public override void RegisterCommands()
        {
            this.CommandContext
                .AddCommand<TextReader, TextWriter, TextWriter, KeyType, string, string, bool, bool>(Command.SetKey)
                ;
        }
    }
}