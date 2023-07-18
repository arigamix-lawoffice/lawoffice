using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Platform;
using Tessa.Platform.ConsoleApps;
using Tessa.Platform.Data;
using Tessa.Scheme;
using Tessa.Views.Metadata.Types;
using Unity;

namespace Tessa.Extensions.Default.Console.Scripts
{
    [ConsoleScript(nameof(ViewTypesFix))]
    public sealed class ViewTypesFix :
        ServerConsoleScriptBase
    {
        #region Private Fields

        private const string TypeFieldPattern = @"""Type""\s*" + KeyValuePairSeparator + @"\s*""" + ValuePattern + "\"";
        private const string KeyValuePairSeparator = ":";
        private const string ValuePattern = @"[\w\s\(\)]+";
        private const string KeyValuePairFormat = @"""Type"": ""{0}""";
        private const string NullValue = "null";

        #endregion

        #region Private Members

        private async ValueTask ExecuteAsync(
            string fullPath,
            CancellationToken cancellationToken)
        {
            var filesToFix = Directory.GetFiles(fullPath, "*.jview");
            if (filesToFix.Length == 0)
            {
                await this.Logger.InfoAsync($"Views not found by path {fullPath}");
                return;
            }

            await this.Logger.InfoAsync($"{filesToFix.Length} views found");

            var sqlConverter = this.Container.Resolve<IDbmsSchemeTypeConverter>(Dbms.SqlServer.ToString());
            var pgConverter = this.Container.Resolve<IDbmsSchemeTypeConverter>(Dbms.PostgreSql.ToString());

            var regex = new Regex(TypeFieldPattern);

            var count = 0;
            foreach (var file in filesToFix)
            {
                var content = await File.ReadAllTextAsync(file, cancellationToken);

                var result = await ReplaceAsync(regex, content, (m, ct) => GetNewKeyPairAsync(sqlConverter, pgConverter, m, ct), cancellationToken);
                if (!result.HasChanges)
                {
                    continue;
                }

                await File.WriteAllTextAsync(file, result.Result, cancellationToken);

                ++count;
            }

            await this.Logger.InfoAsync($"The script is complete. {count} views fixed.");
        }

        private static async Task<(string Result, bool HasChanges)> ReplaceAsync(
            Regex regex,
            string input,
            Func<Match, CancellationToken, Task<(string Result, bool HasChanges)>> replaceAsync,
            CancellationToken cancellationToken = default)
        {
            var result = StringBuilderHelper.Acquire();
            var lastIndex = 0;
            var hasChanges = false;

            foreach (Match match in regex.Matches(input))
            {
                cancellationToken.ThrowIfCancellationRequested();

                var replaced = await replaceAsync(match, cancellationToken);

                if (!hasChanges && replaced.HasChanges)
                {
                    hasChanges = true;
                }

                result
                    .Append(input, lastIndex, match.Index - lastIndex)
                    .Append(replaced.Result);

                lastIndex = match.Index + match.Length;
            }

            result.Append(input, lastIndex, input.Length - lastIndex);

            return (result.ToStringAndRelease(), hasChanges);
        }

        private static async Task<(string Result, bool HasChanges)> GetNewKeyPairAsync(
            IDbmsSchemeTypeConverter sqlConverter,
            IDbmsSchemeTypeConverter pgConverter,
            Match m,
            CancellationToken cancellationToken = default)
        {
            if (m.Value.Contains(NullValue, StringComparison.OrdinalIgnoreCase))
            {
                return (m.Value, false);
            }

            var splits = m.Value.Split(KeyValuePairSeparator);
            var match = Regex.Match(splits.Last().Trim(), ValuePattern);
            if (!match.Success)
            {
                return (m.Value, false);
            }

            var value = match.Value;
            if (!SchemeType.TryParse(value, out var schemeType))
            {
                schemeType =
                    await pgConverter.TryGetAsync(value, cancellationToken)
                    ?? await sqlConverter.TryGetAsync(value, cancellationToken);
            }

            return schemeType is null 
                ? (m.Value, false) 
                : (string.Format(KeyValuePairFormat, schemeType.ToString()), true);
        }

        #endregion

        #region Base Overrides

        protected override async ValueTask ExecuteCoreAsync(CancellationToken cancellationToken)
        {
            var dir = this.TryGetParameter("dir");
            if (string.IsNullOrEmpty(dir))
            {
                await this.Logger.ErrorAsync(
                    "Pass \"dir\" parameter to specify the folder with views in .jview format" +
                    ", i.e.: -pp:dir=C:\\Repository\\Configuration\\Views");
                this.Result = -1;
                return;
            }

            string fullPath = Path.GetFullPath(dir);
            if (!Directory.Exists(fullPath))
            {
                throw new DirectoryNotFoundException(fullPath);
            }

            await this.ExecuteAsync(fullPath, cancellationToken);
        }

        protected override async ValueTask ShowHelpCoreAsync(CancellationToken cancellationToken)
        {
            await this.Logger.WriteLineAsync("Fixes the data types of columns and parameters for the views at the specified path.");
            await this.Logger.WriteLineAsync("-pp:dir=C:\\Repository\\Configuration\\Views - specify the folder with views in .jview format.");
            await this.Logger.WriteLineAsync();
            await this.Logger.WriteLineAsync("Example:");
            await this.Logger.WriteLineAsync(
                $"{Assembly.GetEntryAssembly()?.GetName().Name} Script {nameof(ViewTypesFix)}" +
                " -pp:dir=C:\\Repository\\Configuration\\Views");
        }

        #endregion
    }
}
