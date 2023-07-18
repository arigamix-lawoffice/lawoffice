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
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Console.ConfigureGenerator.Generators
{
    public sealed class RouteGenerator : IGenerator
    {
        public async Task<string> GenerateAsync(string path, CancellationToken cancellationToken = default)
        {
            var builder = new StringBuilder();
            builder.AppendLine("using System;");
            builder.AppendLine();
            builder.AppendLine("namespace Tessa.Extensions.Shared.Info");
            builder.AppendLine("{// ReSharper disable InconsistentNaming");

            var routesPaths = Directory.GetDirectories(path, "*", SearchOption.TopDirectoryOnly)
                .Where(routePath => new DirectoryInfo(routePath).Name.Equals("Secondary processes", StringComparison.Ordinal)
                               || new DirectoryInfo(routePath).Name.Equals("Proceses", StringComparison.Ordinal)
                               || new DirectoryInfo(routePath).Name.Equals("Process groups", StringComparison.Ordinal)
                    )
                .OrderBy(x => x).ToList();

            var cardTypes = new Dictionary<string, List<(Guid, string, string, string)>>();
            foreach (var routePath in routesPaths)
            {
                var cardType = Path.GetFileName(routePath);
                var files = Directory.GetFiles(routePath, "*.jcard", SearchOption.AllDirectories).OrderBy(Path.GetFileName).ToList();
                if (files.Count == 0)
                {
                    continue;
                }

                builder.AppendLine($"    #region {cardType}");
                foreach (string file in files)
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
                    var caption = card.TypeName switch
                    {
                        "KrStageGroup" => card.Sections["KrStageGroups"].Fields.Get<string>("Name"), // Группа этапов
                        "KrSecondaryProcess" => card.Sections["KrSecondaryProcesses"].Fields.Get<string>("Name"), // Вторичный процесс
                        "KrStageTemplate" => card.Sections["KrStageTemplates"].Fields.Get<string>("Name"), // Шаблон этапа
                        _ => string.Empty
                    };
                    var typeInfo = card.TypeName switch
                    {
                        "KrStageGroup" => "GroupInfo", // Группа этапов
                        "KrSecondaryProcess" => "ProcessInfo", // Вторичный процесс
                        "KrStageTemplate" => "TemplateInfo", // Шаблон этапа
                        _ => string.Empty
                    };
                    var stages = new List<(Guid, string)>();
                    var completionOptions = new List<(Guid, string)>();
                    if (card.TypeName == "KrStageTemplate")
                    {
                        stages.AddRange(card.Sections["KrStages"].Rows
                            .OrderBy(x => x.RowID)
                            .Select(row => (row.RowID, row.Fields.Get<string>("Name"))));
                        var items = card.Sections["KrStages"].Rows
                            .Where(x => x.Fields.Get<Dictionary<string, object>>("Settings").TryGet<List<object>>("KrUniversalTaskOptionsSettingsVirtual_Synthetic").Any())
                            .OrderBy(x => x.RowID)
                            .Select(x => x.Fields.Get<Dictionary<string, object>>("Settings").TryGet("KrUniversalTaskOptionsSettingsVirtual_Synthetic", new List<object>()));
                        foreach (var item in items)
                        {
                            completionOptions.AddRange(item
                                .Select(x => x as Dictionary<string, object>)
                                .OrderBy(x => x.Get<Guid>("RowID"))
                                .Select(x => (x.Get<Guid>("OptionID"), x.Get<string>("Caption"))));
                        }
                    }

                    var shortType = typeInfo.Replace("Info", string.Empty, StringComparison.Ordinal);
                    var alias = Translate.ToName(Transliteration.Front(caption.Replace("$", "", StringComparison.Ordinal).Replace("(", "", StringComparison.Ordinal).Replace(")", "", StringComparison.Ordinal).Replace("_", " ", StringComparison.Ordinal)).Replace("'s", "", StringComparison.Ordinal));
                    if (!cardTypes.ContainsKey(shortType))
                    {
                        cardTypes.Add(shortType, new List<(Guid, string, string, string)>());
                    }

                    cardTypes[shortType].Add((card.ID, typeInfo, alias, caption));
                    builder.AppendLine();
                    builder.AppendLine($"    #region {alias}");
                    builder.AppendLine();
                    builder.AppendLine("    /// <summary>");
                    builder.AppendLine($"    ///     ID: {card.ID:B}");
                    builder.AppendLine($"    ///     Alias: {alias}");
                    builder.AppendLine($"    ///     Caption: {caption}");
                    builder.AppendLine("    /// </summary>");
                    builder.AppendLine($"    public sealed class {alias}{typeInfo}");
                    builder.AppendLine("    {");
                    builder.AppendLine("        /// <summary>");
                    builder.AppendLine($"        ///     {cardType} identifier for \"{caption}\": {card.ID:B}.");
                    builder.AppendLine("        /// </summary>");
                    builder.AppendLine($"        public readonly Guid ID = new Guid({card.ID.ToString("X").Replace("{", "", StringComparison.Ordinal).Replace("}", "", StringComparison.Ordinal)});");
                    builder.AppendLine();
                    builder.AppendLine("        /// <summary>");
                    builder.AppendLine($"        ///     {cardType} alias for \"{caption}\".");
                    builder.AppendLine("        /// </summary>");
                    builder.AppendLine($"        public readonly string Alias = \"{alias}\";");
                    builder.AppendLine();
                    builder.AppendLine("        /// <summary>");
                    builder.AppendLine($"        ///     {cardType} caption for \"{caption}\".");
                    builder.AppendLine("        /// </summary>");
                    builder.AppendLine($"        public readonly string Caption = \"{caption}\";");
                    if (stages.Any())
                    {
                        builder.AppendLine();
                        builder.AppendLine("        /// <summary>");
                        builder.AppendLine($"        ///     {cardType} stages for \"{caption}\".");
                        builder.AppendLine("        /// </summary>");
                        builder.AppendLine($"        public readonly {alias}StageInfo Stages = new {alias}StageInfo();");
                    }

                    if (completionOptions.Any())
                    {
                        builder.AppendLine();
                        builder.AppendLine("        /// <summary>");
                        builder.AppendLine($"        ///     {cardType} completition options for \"{caption}\".");
                        builder.AppendLine("        /// </summary>");
                        builder.AppendLine($"        public readonly {alias}CompletitionInfo CompletitionOptions = new {alias}CompletitionInfo(); ");
                    }

                    builder.AppendLine("    }");
                    builder.AppendLine();
                    builder.AppendLine("    #endregion");
                    if (stages.Any())
                    {
                        builder.AppendLine();
                        builder.AppendLine($"    #region {alias}");
                        builder.AppendLine();
                        builder.AppendLine($"    public sealed class {alias}StageInfo");
                        builder.AppendLine("    {");
                        builder.AppendLine("        #region Stages");
                        builder.AppendLine();
                        var aliases = new List<string>();
                        foreach (var (rowId, rowCaption) in stages)
                        {
                            var rowAlias = Translate.ToName(Transliteration.Front(rowCaption.Replace("$", "", StringComparison.Ordinal).Replace("(", "", StringComparison.Ordinal).Replace(")", "", StringComparison.Ordinal).Replace("_", " ", StringComparison.Ordinal)).Replace("'s", "", StringComparison.Ordinal));
                            if (aliases.Contains(rowAlias))
                            {
                                var cnt = 1;
                                do
                                {
                                    cnt++;
                                }
                                while (aliases.Contains(rowAlias + cnt));

                                rowAlias += cnt;
                            }

                            aliases.Add(rowAlias);

                            builder.AppendLine("        /// <summary>");
                            builder.AppendLine($"        ///     ID: {rowId:B}");
                            builder.AppendLine($"        ///     Alias: {rowAlias}");
                            builder.AppendLine($"        ///     Caption: {rowCaption}");
                            builder.AppendLine("        /// </summary>");
                            builder.AppendLine($"        public readonly Guid {rowAlias}ID = new Guid({rowId.ToString("X").Replace("{", "", StringComparison.Ordinal).Replace("}", "", StringComparison.Ordinal)});");
                            builder.AppendLine();
                        }

                        builder.AppendLine("        #endregion");
                        builder.AppendLine("    }");
                        builder.AppendLine();
                        builder.AppendLine("    #endregion");
                    }

                    if (completionOptions.Any())
                    {
                        builder.AppendLine();
                        builder.AppendLine($"    #region {alias}");
                        builder.AppendLine();
                        builder.AppendLine($"    public sealed class {alias}CompletitionInfo");
                        builder.AppendLine("    {");
                        builder.AppendLine("        #region Completition Options");
                        builder.AppendLine();
                        var aliases = new List<string>();
                        foreach (var (rowId, rowCaption) in completionOptions)
                        {
                            var rowAlias = Translate.ToName(Transliteration.Front(rowCaption.Replace("$", "", StringComparison.Ordinal).Replace("(", "", StringComparison.Ordinal).Replace(")", "", StringComparison.Ordinal).Replace("_", " ", StringComparison.Ordinal)).Replace("'s", "", StringComparison.Ordinal));
                            if (aliases.Contains(rowAlias))
                            {
                                var cnt = 1;
                                do
                                {
                                    cnt++;
                                }
                                while (aliases.Contains(rowAlias + cnt));

                                rowAlias += cnt;
                            }

                            aliases.Add(rowAlias);

                            builder.AppendLine("        /// <summary>");
                            builder.AppendLine($"        ///     ID: {rowId:B}");
                            builder.AppendLine($"        ///     Alias: {rowAlias}");
                            builder.AppendLine($"        ///     Caption: {rowCaption}");
                            builder.AppendLine("        /// </summary>");
                            builder.AppendLine($"        public readonly Guid {rowAlias}ID = new Guid({rowId.ToString("X").Replace("{", "", StringComparison.Ordinal).Replace("}", "", StringComparison.Ordinal)});");
                            builder.AppendLine();
                        }

                        builder.AppendLine("        #endregion");
                        builder.AppendLine("    }");
                        builder.AppendLine();
                        builder.AppendLine("    #endregion");
                    }
                }

                builder.AppendLine();
                builder.AppendLine("    #endregion");
                builder.AppendLine();
            }

            foreach (var type in cardTypes)
            {
                builder.AppendLine($"    public sealed class {type.Key}RouteInfo");
                builder.AppendLine("    {");
                builder.AppendLine($"        #region {type.Key}");
                builder.AppendLine();
                foreach (var (id, typeInfo, alias, caption) in cardTypes[type.Key])
                {
                    builder.AppendLine("        /// <summary>");
                    builder.AppendLine($"        ///     ID: {id:B}");
                    builder.AppendLine($"        ///     Alias: {alias}");
                    builder.AppendLine($"        ///     Caption: {caption}");
                    builder.AppendLine("        /// </summary>");
                    builder.AppendLine($"        public readonly {alias}{typeInfo} {alias} = new {alias}{typeInfo}();");
                    builder.AppendLine();
                }

                builder.AppendLine("        #endregion");
                builder.AppendLine("    }");
                builder.AppendLine();
            }

            builder.AppendLine("    public static class RouteInfo");
            builder.AppendLine("    {");
            builder.AppendLine("        #region Routes");
            builder.AppendLine();
            foreach (var type in cardTypes.Keys)
            {
                builder.AppendLine($"        public static readonly {type}RouteInfo {type} = new {type}RouteInfo();");
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

            var routesPaths = Directory.GetDirectories(path, "*", SearchOption.TopDirectoryOnly).OrderBy(x => x, StringComparer.InvariantCultureIgnoreCase).ToList();
            var cardTypes = new Dictionary<string, List<(Guid ID, string TypeInfo, string Alias, string Caption)>>();
            foreach (var routePath in routesPaths)
            {
                var cardType = Path.GetFileName(routePath);
                var files = Directory.GetFiles(routePath, "*.jcard", SearchOption.AllDirectories).OrderBy(Path.GetFileName, StringComparer.InvariantCultureIgnoreCase).ToList();
                if (files.Count == 0)
                {
                    continue;
                }

                builder.AppendLine($"//#region {cardType}");
                foreach (var file in files)
                {
                    using var stream = new StringReader(await File.ReadAllTextAsync(file, cancellationToken));
                    using var jsonReader = new JsonTextReader(stream);
                    var storage = TessaSerializer.JsonTyped.Deserialize<List<object>>(jsonReader);
                    if (storage.Count == 0 || storage[0] is not Dictionary<string, object> requestStorage)
                    {
                        throw new InvalidOperationException($"Can't read card as {CardFileFormat.Json} format from specified stream.");
                    }

                    var storeRequest = new CardStoreRequest(requestStorage);
                    var card = storeRequest.Card;
                    var caption = card.TypeName switch
                    {
                        "KrStageGroup" => card.Sections["KrStageGroups"].Fields.Get<string>("Name"), // Группа этапов
                        "KrSecondaryProcess" => card.Sections["KrSecondaryProcesses"].Fields.Get<string>("Name"), // Вторичный процесс
                        "KrStageTemplate" => card.Sections["KrStageTemplates"].Fields.Get<string>("Name"), // Шаблон этапа
                        _ => string.Empty
                    };
                    var typeInfo = card.TypeName switch
                    {
                        "KrStageGroup" => "GroupInfo", // Группа этапов
                        "KrSecondaryProcess" => "ProcessInfo", // Вторичный процесс
                        "KrStageTemplate" => "TemplateInfo", // Шаблон этапа
                        _ => string.Empty
                    };
                    var stages = new List<(Guid, string)>();
                    var completitionOptions = new List<(Guid, string)>();
                    if (card.TypeName == "KrStageTemplate")
                    {
                        stages.AddRange(card.Sections["KrStages"].Rows
                            .OrderBy(x => x.RowID)
                            .Select(row => (row.RowID, row.Fields.Get<string>("Name"))));
                        var items = card.Sections["KrStages"].Rows
                            .Where(x => x.Fields.Get<Dictionary<string, object>>("Settings").TryGet<List<object>>("KrUniversalTaskOptionsSettingsVirtual_Synthetic").Any())
                            .OrderBy(x => x.RowID)
                            .Select(x => x.Fields.Get<Dictionary<string, object>>("Settings").TryGet("KrUniversalTaskOptionsSettingsVirtual_Synthetic", new List<object>()));
                        foreach (var item in items)
                        {
                            completitionOptions.AddRange(item
                                .Select(x => x as Dictionary<string, object>)
                                .OrderBy(x => x.Get<Guid>("RowID"))
                                .Select(x => (x.Get<Guid>("OptionID"), x.Get<string>("Caption"))));
                        }
                    }

                    var shortType = typeInfo.Replace("Info", string.Empty, StringComparison.Ordinal);
                    var alias = Translate.AliasName(caption);
                    if (!cardTypes.ContainsKey(shortType))
                    {
                        cardTypes.Add(shortType, new List<(Guid, string, string, string)>());
                    }

                    cardTypes[shortType].Add((card.ID, typeInfo, alias, caption));
                    builder.AppendLine();
                    builder.AppendLine($"//#region {alias}");
                    builder.AppendLine();
                    builder.AppendLine("/**");
                    builder.AppendLine($" * ID: {card.ID:B}");
                    builder.AppendLine($" * Alias: {alias}");
                    builder.AppendLine($" * Caption: {caption}");
                    builder.AppendLine(" */");
                    builder.AppendLine($"class {alias}{typeInfo} {{");
                    builder.AppendLine("  /**");
                    builder.AppendLine($"   * {cardType} identifier for \"{caption}\": {card.ID:B}.");
                    builder.AppendLine("   */");
                    builder.AppendLine($"  readonly ID: guid = '{card.ID}';");
                    builder.AppendLine();
                    builder.AppendLine("  /**");
                    builder.AppendLine($"   * {cardType} alias for '{caption}'.");
                    builder.AppendLine("   */");
                    builder.AppendLine($"  readonly Alias: string = '{alias}';");
                    builder.AppendLine();
                    builder.AppendLine("  /**");
                    builder.AppendLine($"   * {cardType} caption for \"{caption}\".");
                    builder.AppendLine("   */");
                    builder.AppendLine($"  readonly Caption: string = '{caption.ToGenString()}';");
                    if (stages.Any())
                    {
                        builder.AppendLine();
                        builder.AppendLine("  /**");
                        builder.AppendLine($"   * {cardType} stages for \"{caption}\".");
                        builder.AppendLine("   */");
                        builder.AppendLine($"  readonly Stages: {alias}StageInfo = new {alias}StageInfo();");
                    }

                    if (completitionOptions.Any())
                    {
                        builder.AppendLine();
                        builder.AppendLine("  /**");
                        builder.AppendLine($"   * {cardType} completition options for \"{caption}\".");
                        builder.AppendLine("   */");
                        builder.AppendLine($"  readonly CompletitionOptions: {alias}CompletitionInfo = new {alias}CompletitionInfo();");
                    }

                    builder.AppendLine("}");
                    builder.AppendLine();
                    builder.AppendLine("//#endregion");
                    if (stages.Any())
                    {
                        builder.AppendLine();
                        builder.AppendLine($"//#region {alias}");
                        builder.AppendLine();
                        builder.AppendLine($"class {alias}StageInfo {{");
                        builder.AppendLine("  //#region Stages");
                        builder.AppendLine();
                        var aliases = new List<string>();
                        foreach (var (rowId, rowCaption) in stages)
                        {
                            var rowAlias = Translate.UniqueAliasName(aliases, Translate.AliasName(rowCaption));
                            aliases.Add(rowAlias);

                            builder.AppendLine("  /**");
                            builder.AppendLine($"   * ID: {rowId:B}");
                            builder.AppendLine($"   * Alias: {rowAlias}");
                            builder.AppendLine($"   * Caption: {rowCaption}");
                            builder.AppendLine("   */");
                            builder.AppendLine($"  readonly {rowAlias}ID: guid = '{rowId}';");
                            builder.AppendLine();
                        }

                        builder.AppendLine("  //#endregion");
                        builder.AppendLine("}");
                        builder.AppendLine();
                        builder.AppendLine("//#endregion");
                    }

                    if (completitionOptions.Any())
                    {
                        builder.AppendLine();
                        builder.AppendLine($"//#region {alias}");
                        builder.AppendLine();
                        builder.AppendLine($"class {alias}CompletitionInfo {{");
                        builder.AppendLine("  //#region Completition Options");
                        builder.AppendLine();
                        var aliases = new List<string>();
                        foreach (var (rowId, rowCaption) in completitionOptions)
                        {
                            var rowAlias = Translate.UniqueAliasName(aliases, Translate.AliasName(rowCaption));
                            aliases.Add(rowAlias);

                            builder.AppendLine("  /**");
                            builder.AppendLine($"   * ID: {rowId:B}");
                            builder.AppendLine($"   * Alias: {rowAlias}");
                            builder.AppendLine($"   * Caption: {rowCaption}");
                            builder.AppendLine("   */");
                            builder.AppendLine($"  readonly {rowAlias}ID: guid = '{rowId}';");
                            builder.AppendLine();
                        }

                        builder.AppendLine("  //#endregion");
                        builder.AppendLine("}");
                        builder.AppendLine();
                        builder.AppendLine("//#endregion");
                    }
                }

                builder.AppendLine();
                builder.AppendLine("//#endregion");
                builder.AppendLine();
            }

            foreach (var type in cardTypes.OrderBy(x => x.Key, StringComparer.InvariantCultureIgnoreCase))
            {
                builder.AppendLine($"class {type.Key}RouteInfo {{");
                builder.AppendLine($"  //#region {type.Key}");
                builder.AppendLine();
                foreach (var (id, typeInfo, alias, caption) in cardTypes[type.Key])
                {
                    builder.AppendLine("  /**");
                    builder.AppendLine($"   * ID: {id:B}");
                    builder.AppendLine($"   * Alias: {alias}");
                    builder.AppendLine($"   * Caption: {caption.Trim()}");
                    builder.AppendLine("   */");
                    builder.AppendLine($"  static get {alias}(): {alias}{typeInfo} {{");
                    builder.AppendLine($"    return {type.Key}RouteInfo.{alias.ToFirstLower()} = {type.Key}RouteInfo.{alias.ToFirstLower()} ?? new {alias}{typeInfo}();");
                    builder.AppendLine("  }");
                    builder.AppendLine();
                    builder.AppendLine($"  private static {alias.ToFirstLower()}: {alias}{typeInfo};");
                    builder.AppendLine();
                }

                builder.AppendLine("  //#endregion");
                builder.AppendLine("}");
                builder.AppendLine();
            }

            builder.AppendLine("export class RouteInfo {");
            builder.AppendLine("  //#region Routes");
            builder.AppendLine();
            foreach (var type in cardTypes.Keys.OrderBy(x => x, StringComparer.InvariantCultureIgnoreCase))
            {
                builder.AppendLine($"  static get {type}(): {type}RouteInfo {{");
                builder.AppendLine($"    return RouteInfo.{type.ToFirstLower()} = RouteInfo.{type.ToFirstLower()} ?? new {type}RouteInfo();");
                builder.AppendLine("  }");
                builder.AppendLine();
                builder.AppendLine($"  private static {type.ToFirstLower()}: {type}RouteInfo;");
                builder.AppendLine();
            }

            builder.AppendLine("  //#endregion");
            builder.Append('}');

            return builder.ToString();
        }
    }
}