using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Tessa.Cards;
using Tessa.Platform.Json;

namespace Tessa.Extensions.Console.ConfigureGenerator.Generators
{
    public sealed class SettingsGenerator : IGenerator
    {
        public async Task<string> GenerateAsync(string path, CancellationToken cancellationToken = default)
        {
            var builder = new StringBuilder();
            builder.AppendLine("using System;");
            builder.AppendLine();
            builder.AppendLine("namespace Tessa.Extensions.Shared.Info");
            builder.AppendLine("{// ReSharper disable InconsistentNaming");

            var settings = new List<string>();
            foreach (var file in Directory.GetFiles(path, "*.jcard", SearchOption.TopDirectoryOnly).OrderBy(Path.GetFileName))
            {
                using var stream = new StringReader(await File.ReadAllTextAsync(file, cancellationToken));
                using var jsonReader = new JsonTextReader(stream);
                var storage = TessaSerializer.JsonTyped.Deserialize<List<object>>(jsonReader);
                if (storage.Count == 0
                    || !(storage[0] is Dictionary<string, object> requestStorage))
                {
                    // это такая же критичная ошибка, как невозможность прочитать из стрима, поэтому кидаем исключение
                    throw new InvalidOperationException($"Can't read card as {CardFileFormat.Json} format from specified stream.");
                }

                var storeRequest = new CardStoreRequest(requestStorage);
                var card = storeRequest.Card;
                var caption = card.TypeCaption;
                var alias = card.TypeName.Replace("$", "", StringComparison.Ordinal).Replace("(", "", StringComparison.Ordinal).Replace(")", "", StringComparison.Ordinal).Replace("-", " ", StringComparison.Ordinal);
                settings.Add(alias);
                builder.AppendLine($"    #region {alias}");
                builder.AppendLine();
                builder.AppendLine("    /// <summary>");
                builder.AppendLine($"    ///     ID: {card.ID:B}");
                builder.AppendLine($"    ///     Alias: {alias}");
                builder.AppendLine($"    ///     Caption: {caption}");
                builder.AppendLine("    /// </summary>");
                builder.AppendLine($"    public sealed class {alias}SettingsInfo");
                builder.AppendLine("    {");
                builder.AppendLine("        /// <summary>");
                builder.AppendLine($"        ///     Setting identifier for \"{alias}\": {card.ID:B}.");
                builder.AppendLine("        /// </summary>");
                builder.AppendLine($"        public readonly Guid ID = new Guid({card.ID.ToString("X").Replace("{", "", StringComparison.Ordinal).Replace("}", "", StringComparison.Ordinal)});");
                builder.AppendLine();
                builder.AppendLine("        /// <summary>");
                builder.AppendLine($"        ///     Setting alias for \"{alias}\".");
                builder.AppendLine("        /// </summary>");
                builder.AppendLine($"        public readonly string Alias = \"{alias}\";");
                builder.AppendLine();
                builder.AppendLine("        /// <summary>");
                builder.AppendLine($"        ///     Setting caption for \"{alias}\".");
                builder.AppendLine("        /// </summary>");
                builder.AppendLine($"        public readonly string Caption = \"{caption}\";");
                builder.AppendLine("    }");
                builder.AppendLine();
                builder.AppendLine("    #endregion");
                builder.AppendLine();
            }

            builder.AppendLine("    public static class SettingsInfo");
            builder.AppendLine("    {");
            builder.AppendLine("        #region Settings");
            builder.AppendLine();
            foreach (var setting in settings)
            {
                builder.AppendLine($"        public static readonly {setting}SettingsInfo {setting} = new {setting}SettingsInfo();");
            }

            builder.AppendLine();
            builder.AppendLine("        #endregion");
            builder.AppendLine("    }");
            builder.Append('}');

            return builder.ToString();
        }

        public async Task<string> GenerateWebAsync(string path, CancellationToken cancellationToken = default)
        {
            var builder = new StringBuilder();
            builder.AppendLine("// noinspection JSUnusedGlobalSymbols,SpellCheckingInspection");
            builder.AppendLine();

            var settings = new List<(string Alias, Guid ID, string Caption)>();
            foreach (var file in Directory.GetFiles(path, "*.jcard", SearchOption.TopDirectoryOnly).OrderBy(Path.GetFileName, StringComparer.InvariantCultureIgnoreCase))
            {
                using var stream = new StringReader(await File.ReadAllTextAsync(file, cancellationToken));
                using var jsonReader = new JsonTextReader(stream);
                var storage = TessaSerializer.JsonTyped.Deserialize<List<object>>(jsonReader);
                if (storage.Count == 0 || storage[0] is not Dictionary<string, object> requestStorage)
                {
                    // это такая же критичная ошибка, как невозможность прочитать из стрима, поэтому кидаем исключение
                    throw new InvalidOperationException($"Can't read card as {CardFileFormat.Json} format from specified stream.");
                }

                var storeRequest = new CardStoreRequest(requestStorage);
                var card = storeRequest.Card;
                var caption = card.TypeCaption;
                var alias = card.TypeName.Replace("$", "", StringComparison.Ordinal).Replace("(", "", StringComparison.Ordinal).Replace(")", "", StringComparison.Ordinal).Replace("-", " ", StringComparison.Ordinal);
                settings.Add((alias, card.ID, caption));
                builder.AppendLine($"//#region {alias}");
                builder.AppendLine();
                builder.AppendLine("/**");
                builder.AppendLine($" * ID: {card.ID:B}");
                builder.AppendLine($" * Alias: {alias}");
                builder.AppendLine($" * Caption: {caption}");
                builder.AppendLine(" */");
                builder.AppendLine($"class {alias}SettingsInfo {{");
                builder.AppendLine("  //#region Common");
                builder.AppendLine();
                builder.AppendLine("  /**");
                builder.AppendLine($"   * Setting identifier for \"{alias}\": {card.ID:B}.");
                builder.AppendLine("   */");
                builder.AppendLine($"   readonly ID: guid = '{card.ID}';");
                builder.AppendLine();
                builder.AppendLine("  /**");
                builder.AppendLine($"   * Setting alias for \"{alias}\".");
                builder.AppendLine("   */");
                builder.AppendLine($"   readonly Alias: string = '{alias}';");
                builder.AppendLine();
                builder.AppendLine("  /**");
                builder.AppendLine($"   * Setting caption for \"{alias}\".");
                builder.AppendLine("   */");
                builder.AppendLine($"   readonly Caption: string = '{caption.ToGenString()}';");
                builder.AppendLine();
                builder.AppendLine("  //#endregion");
                builder.AppendLine("}");
                builder.AppendLine();
                builder.AppendLine("//#endregion");
                builder.AppendLine();
            }

            builder.AppendLine("export class SettingsInfo {");
            builder.AppendLine("  //#region SettingsInfo");
            foreach (var (setting, id, caption) in settings.OrderBy(x => x.Alias, StringComparer.InvariantCultureIgnoreCase))
            {
                builder.AppendLine();
                builder.AppendLine("  /**");
                builder.AppendLine($"   * Setting identifier for \"{caption}\": {id:B}.");
                builder.AppendLine("   */");
                builder.AppendLine($"  static get {setting}(): {setting}SettingsInfo {{");
                builder.AppendLine($"    return SettingsInfo.{setting.ToFirstLower()} = SettingsInfo.{setting.ToFirstLower()} ?? new {setting}SettingsInfo();");
                builder.AppendLine("  }");
                builder.AppendLine();
                builder.AppendLine($"  private static {setting.ToFirstLower()}: {setting}SettingsInfo;");
            }

            builder.AppendLine();
            builder.AppendLine("  //#endregion");
            builder.Append('}');

            return builder.ToString();
        }
    }
}