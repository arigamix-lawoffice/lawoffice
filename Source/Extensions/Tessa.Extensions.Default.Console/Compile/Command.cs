#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Threading.Tasks;
using Tessa.Localization;
using Tessa.Platform.CommandLine;
using Tessa.Platform.ConsoleApps;
using Unity;

namespace Tessa.Extensions.Default.Console.Compile
{
    public static class Command
    {
        #region Public Methods

        [Verb("Compile")]
        [LocalizableDescription("Common_CLI_Compile")]
        public static async Task Compile(
            [Output] TextWriter stdOut,
            [Error] TextWriter stdErr,
            [Argument][LocalizableDescription("Common_CLI_CompileCategories")] IEnumerable<string>? categories = null,
            [Argument("id")][LocalizableDescription("Common_CLI_CompileObject")] IEnumerable<Guid>? identifiers = null,
            [Argument("showCategories")][LocalizableDescription("Common_CLI_ShowCategories")] bool showCategories = false,
            [Argument("a")][LocalizableDescription("Common_CLI_Address")] string? address = null,
            [Argument("i")][LocalizableDescription("Common_CLI_Instance")] string? instanceName = null,
            [Argument("u")][LocalizableDescription("Common_CLI_UserName")] string? userName = null,
            [Argument("p")][LocalizableDescription("Common_CLI_Password")] string? password = null,
            [Argument("q")][LocalizableDescription("Common_CLI_Quiet")] bool quiet = false,
            [Argument("nologo")][LocalizableDescription("CLI_NoLogo")] bool nologo = false)
        {
            if (!nologo && !quiet)
            {
                ConsoleAppHelper.WriteLogo(stdOut);
            }

            IReadOnlySet<string>? categorySet = null;
            IReadOnlySet<Guid>? identifierSet = null;

            if (!showCategories)
            {
                categorySet = GetReadOnlySetOrNullIfEmpty(categories);
                identifierSet = GetReadOnlySetOrNullIfEmpty(identifiers);

                if (categorySet is null
                    && identifierSet?.Count > 0)
                {
                    throw new ArgumentException(
                        "Category(s) is required.",
                        nameof(categories));
                }
            }

            var container = await new UnityContainer()
                .ConfigureConsoleForClientAsync(
                    stdOut,
                    stdErr,
                    quiet,
                    instanceName,
                    address);

            int result;
            await using (var operation = container.Resolve<Operation>())
            {
                var context = new OperationContext
                {
                    Categories = categorySet,
                    Identifiers = identifierSet,
                    ShowCategories = showCategories,
                };

                if (!await operation.LoginAsync(userName, password))
                {
                    ConsoleAppHelper.EnvironmentExit(ConsoleAppHelper.FailedLoginExitCode);
                    return;
                }

                result = await operation.ExecuteAsync(context);
                await operation.CloseAsync();
            }

            ConsoleAppHelper.EnvironmentExit(result);
        }

        #endregion

        #region Private Methods

        private static IReadOnlySet<T>? GetReadOnlySetOrNullIfEmpty<T>(
            IEnumerable<T>? items)
        {
            var result = items?.ToImmutableHashSet();

            if (result?.Count == 0)
            {
                result = null;
            }

            return result;
        }

        #endregion
    }
}
