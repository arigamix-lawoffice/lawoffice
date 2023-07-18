﻿using System.IO;
using Tessa.Platform.CommandLine;
using Tessa.Platform.ConsoleApps;

namespace Tessa.Extensions.Default.Console.GetKey
{
    [ConsoleRegistrator]
    public sealed class CommandRegistrator :
        ConsoleRegistratorBase
    {
        public override void RegisterCommands()
        {
            this.CommandContext
                .AddCommand<TextWriter, KeyType>(Command.GetKey)
                ;
        }
    }
}