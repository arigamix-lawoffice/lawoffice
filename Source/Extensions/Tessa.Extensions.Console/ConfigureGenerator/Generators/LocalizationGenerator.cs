using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Tessa.Extensions.Console.ConfigureGenerator.Generators
{
    public sealed class LocalizationGenerator : IGenerator
    {
        public async Task<string> GenerateAsync(string path, CancellationToken cancellationToken = default)
        {
            var builder = new StringBuilder();
            builder.AppendLine("using System;");
            builder.AppendLine();
            builder.AppendLine("namespace Tessa.Extensions.Shared.Info");
            builder.AppendLine("{// ReSharper disable InconsistentNaming");
            builder.AppendLine("    public class LocalizationInfo");
            builder.AppendLine("    {");
            builder.AppendLine("        #region Localization");
            builder.AppendLine();

            var knownAliases = new HashSet<string>();
            foreach (var file in Directory.GetFiles(path, "*.jlocalization", SearchOption.TopDirectoryOnly).OrderBy(Path.GetFileName, StringComparer.InvariantCultureIgnoreCase))
            {
                var isFirstEntry = true;
                var libraryText = await File.ReadAllTextAsync(file, cancellationToken);
                var library = JObject.Parse(libraryText);
                var libraryName = Path.GetFileNameWithoutExtension(file);

                builder.AppendLine($"        #region {libraryName}");
                foreach (var array in library["Entries"]!.Value<JArray>()!)
                {
                    if (array is not JObject entry)
                    {
                        continue;
                    }

                    var comment = entry["Comment"]!.Value<string>();
                    var fieldName = entry["Name"]!.Value<string>()!.Replace("_", string.Empty, StringComparison.InvariantCultureIgnoreCase);
                    if (knownAliases.Contains(fieldName))
                    {
                        continue;
                    }

                    knownAliases.Add(fieldName);
                    if (!string.IsNullOrWhiteSpace(comment))
                    {
                        isFirstEntry = false;
                        builder.AppendLine();
                        builder.AppendLine("        /// <summary>");
                        builder.AppendLine($"        /// {Translate.ToSingleLine(comment)}");
                        builder.AppendLine("        /// </summary>");
                    }
                    else if (isFirstEntry)
                    {
                        isFirstEntry = false;
                        builder.AppendLine();
                    }
                    builder.AppendLine($@"        public static readonly string {fieldName} = ""${entry["Name"].Value<string>()}"";");
                }

                builder.AppendLine();
                builder.AppendLine("        #endregion");
                builder.AppendLine();
            }

            builder.AppendLine("        #endregion");
            builder.AppendLine("    }");
            builder.AppendLine("}");

            return builder.ToString();
        }

        public async Task<string> GenerateWebAsync(string path, CancellationToken cancellationToken = default)
        {
            var builder = new StringBuilder();
            builder.AppendLine("// noinspection JSUnusedGlobalSymbols,SpellCheckingInspection");
            builder.AppendLine();
            builder.AppendLine("export class LocalizationInfo {");
            builder.AppendLine("  //#region Localization");
            builder.AppendLine();

            var knownAliases = new HashSet<string>();
            foreach (var file in Directory.GetFiles(path, "*.jlocalization", SearchOption.TopDirectoryOnly).OrderBy(Path.GetFileName, StringComparer.InvariantCultureIgnoreCase))
            {
                var isFirstEntry = true;
                var libraryText = await File.ReadAllTextAsync(file, cancellationToken);
                var library = JObject.Parse(libraryText);
                var libraryName = Path.GetFileNameWithoutExtension(file);

                builder.AppendLine($"  //#region {libraryName}");
                foreach (var array in library["Entries"]!.Value<JArray>()!)
                {
                    if (array is not JObject entry)
                    {
                        continue;
                    }

                    var comment = entry["Comment"]!.Value<string>();
                    var fieldName = entry["Name"]!.Value<string>()!.Replace("_", string.Empty, StringComparison.InvariantCultureIgnoreCase);
                    if (knownAliases.Contains(fieldName))
                    {
                        continue;
                    }

                    knownAliases.Add(fieldName);
                    if (!string.IsNullOrWhiteSpace(comment))
                    {
                        isFirstEntry = false;
                        builder.AppendLine();
                        builder.AppendLine("  /**");
                        builder.AppendLine($"   * {Translate.ToSingleLine(comment)}");
                        builder.AppendLine("   */");
                    }
                    else if (isFirstEntry)
                    {
                        isFirstEntry = false;
                        builder.AppendLine();
                    }

                    builder.AppendLine($"  static readonly {fieldName}: string = '${entry["Name"].Value<string>()}';");
                }

                builder.AppendLine();
                builder.AppendLine("  //#endregion");
                builder.AppendLine();
            }

            builder.AppendLine("  //#endregion");
            builder.Append('}');

            return builder.ToString();
        }
    }
}