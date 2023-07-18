using System.IO;
using Tessa.Extensions.Default.Console.ConvertConfiguration;
using Tessa.Platform.CommandLine;
using Tessa.Platform.ConsoleApps;

namespace Tessa.Extensions.Default.Console.ConvertCards
{
    [ConsoleRegistrator]
    public sealed class CommandRegistrator :
        ConsoleRegistratorBase
    {
        public override void RegisterCommands()
        {
            this.CommandContext
                .AddCommand<TextWriter, TextWriter, string, string, string, ConversionMode, string, string, bool, bool, bool>(Command.ConvertCards)
                ;
        }
    }
}