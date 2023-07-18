using System;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using Tessa.Extensions.Console.ConfigureGenerator.Generators;
using Tessa.Localization;
using Tessa.Platform.CommandLine;
using Tessa.Platform.ConsoleApps;

namespace Tessa.Extensions.Console.ConfigureGenerator
{
    public static class Command
    {
        [Verb("ConfigureGenerator")]
        [Description("Generate usefull classes from configuration")]
        public static async Task ConfigureGeneratorAsync(
            [Output] TextWriter stdOut,
            [Error] TextWriter stdErr,
            [Argument][Description("Source")] string source,
            [Argument("mode"), Description("Generation mode")] GenerationMode conversionMode = GenerationMode.Scheme,
            [Argument("output")] [Description("Generation result file")] string outputPath = null,
            [Argument("q"), LocalizableDescription("Common_CLI_Quiet")] bool quiet = true,
            [Argument("web")] [Description("For web client")] bool forWeb = false,
            [Argument("configPath")] [Description("Configuration file for custom generator")] string configPath = null,
            [Argument("nologo")][LocalizableDescription("CLI_NoLogo")] bool nologo = true)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (!nologo && !quiet)
            {
                ConsoleAppHelper.WriteLogo(stdOut);
            }

            var generator = GetGenerator(conversionMode, configPath);
            if (string.IsNullOrWhiteSpace(outputPath))
            {
                await stdOut.WriteAsync(forWeb ? await generator.GenerateWebAsync(source) : await generator.GenerateAsync(source));
            }
            else
            {
                Directory.CreateDirectory(Path.GetDirectoryName(outputPath)!);
                await File.WriteAllTextAsync(outputPath, forWeb ? await generator.GenerateWebAsync(source) : await generator.GenerateAsync(source));
            }

            ConsoleAppHelper.EnvironmentExit(0);
        }

        private static IGenerator GetGenerator(GenerationMode mode, string configPath = null) => mode switch
        {
            GenerationMode.Scheme => new SchemeGenerator(),
            GenerationMode.Types => new TypesGenerator(),
            GenerationMode.Views => new ViewGenerator(),
            GenerationMode.Workplaces => new WorkplaceGenerator(),
            GenerationMode.Files => new FilesGenerator(),
            GenerationMode.Notifications => new NotificationGenerator(),
            GenerationMode.Processes => new ProcessGenerator(),
            GenerationMode.Roles => new RoleGenerator(),
            GenerationMode.Routes => new RouteGenerator(),
            GenerationMode.Settings => new SettingsGenerator(),
            GenerationMode.Custom => configPath is not null
                ? new CustomGenerator(configPath)
                : throw new ArgumentNullException(nameof(configPath)),
            GenerationMode.Localization => new LocalizationGenerator(),
            _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
        };
    }
}