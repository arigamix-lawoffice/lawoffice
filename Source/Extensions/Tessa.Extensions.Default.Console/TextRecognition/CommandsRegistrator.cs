#nullable enable

using Tessa.Platform.CommandLine;
using Tessa.Platform.ConsoleApps;

namespace Tessa.Extensions.Default.Console.TextRecognition
{
    [ConsoleRegistrator]
    public sealed class CommandsRegistrator : ConsoleRegistratorBase
    {
        public override void RegisterCommands()
        {
            this.CommandContext?.Commands.Add(new DelegateCommand(Synchronous.Command.TextRecognitionSynchronous));
            this.CommandContext?.Commands.Add(new DelegateCommand(Asynchronous.Command.TextRecognitionAsynchronous));
        }
    }
}
