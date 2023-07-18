using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Tessa.Cards;
using Tessa.Platform;
using Tessa.Platform.Json;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Console.ConfigureGenerator.Generators
{
    public sealed class CustomGenerator : IGenerator
    {
        private class CustomObject
        {
            public string Caption;
            public string Name;
            public string Path;
            public CustomMapping Digest;
            public List<CustomField> Fields;
        }

        private class CustomField
        {
            public string Caption;
            public string Name;
            public CustomMapping Mapping;
        }

        private class CustomMapping
        {
            public string Section;
            public string Field;
            public string Type;
        }

        private readonly string configPath;

        public CustomGenerator(string configPath) => this.configPath = configPath;

        public async Task<string> GenerateAsync(string path, CancellationToken cancellationToken = default)
        {
            var builder = new StringBuilder();
            builder.AppendLine("using System;");
            builder.AppendLine();
            builder.AppendLine("namespace Tessa.Extensions.Shared.Info");
            builder.AppendLine("{// ReSharper disable InconsistentNaming");

            var configManager = new ConfigurationManager(fileName: this.configPath);
            await configManager.InitializeAsync(cancellationToken);

            var infos = LoadFromConfiguration(configManager, "TemplateSource");

            var infoCnt = 0;
            foreach (var info in infos)
            {
                infoCnt++;
                builder.AppendLine($"    #region {info.Caption}");
                builder.AppendLine();
                var types = new List<(string Alias, Guid ID, string Caption)>();
                foreach (var file in Directory.GetFiles(info.Path, "*.jcard", SearchOption.TopDirectoryOnly).OrderBy(Path.GetFileName, StringComparer.InvariantCultureIgnoreCase))
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
                    var caption = GetFieldValue(card, info.Digest)?.ToString();
                    var alias = Translate.AliasName(caption);
                    types.Add((alias, card.ID, caption));

                    builder.AppendLine($"    #region {caption}");
                    builder.AppendLine();
                    builder.AppendLine("    /// <summary>");
                    builder.AppendLine($"    ///     ID: {card.ID:B}");

                    foreach (var field in info.Fields)
                    {
                        builder.AppendLine($"    ///     {field.Name}: {GetFieldValue(card, field.Mapping)?.ToString()?.ToGenString()}");
                    }

                    builder.AppendLine("    /// </summary>");
                    builder.AppendLine($"    public class {alias}{info.Name}Info");
                    builder.AppendLine("    {");
                    builder.AppendLine("        #region Common");
                    builder.AppendLine();
                    builder.AppendLine("        /// <summary>");
                    builder.AppendLine($"        ///     {info.Caption} identifier for \"{caption}\": {card.ID:B}.");
                    builder.AppendLine("        /// </summary>");
                    builder.AppendLine($"        public readonly Guid ID = new({card.ID.ToGenString()});");
                    builder.AppendLine();

                    foreach (var field in info.Fields)
                    {
                        builder.AppendLine("        /// <summary>");
                        builder.AppendLine($"        ///     {field.Caption} for \"{caption}\".");
                        builder.AppendLine("        /// </summary>");
                        builder.AppendLine($"        public readonly string {field.Name} = \"{GetFieldValue(card, field.Mapping)?.ToString()?.ToGenString()}\";");
                        builder.AppendLine();
                    }

                    builder.AppendLine("        #endregion");
                    builder.AppendLine("    }");
                    builder.AppendLine();
                    builder.AppendLine("    #endregion");
                    builder.AppendLine();
                }

                builder.AppendLine($"    public static class {info.Name}Info");
                builder.AppendLine("    {");
                builder.AppendLine($"        #region {info.Name}Info");
                foreach (var (type, id, name) in types.OrderBy(x => x.Alias, StringComparer.InvariantCultureIgnoreCase))
                {
                    builder.AppendLine();
                    builder.AppendLine("        /// <summary>");
                    builder.AppendLine($"        ///     {info.Caption} identifier for \"{name}\": {id:B}.");
                    builder.AppendLine("        /// </summary>");
                    builder.AppendLine($"        public static readonly {type}{info.Name}Info {type} = new();");
                }

                builder.AppendLine();
                builder.AppendLine("        #endregion");
                builder.AppendLine("    }");
                builder.AppendLine();
                builder.AppendLine("    #endregion");

                if (infoCnt != infos.Count)
                {
                    builder.AppendLine();
                }
            }

            builder.Append('}');

            return builder.ToString();
        }

        public async Task<string> GenerateWebAsync(string path, CancellationToken cancellationToken = default)
        {
            var builder = new StringBuilder();
            builder.AppendLine("// noinspection JSUnusedGlobalSymbols,SpellCheckingInspection");
            builder.AppendLine();

            var configManager = new ConfigurationManager(fileName: this.configPath);
            await configManager.InitializeAsync(cancellationToken);

            var infos = LoadFromConfiguration(configManager, "TemplateSource");

            var infoCnt = 0;
            foreach (var info in infos)
            {
                infoCnt++;
                builder.AppendLine($"//#region {info.Caption.Trim()}");
                builder.AppendLine();
                var types = new List<(string Alias, Guid ID, string Caption)>();
                foreach (var file in Directory.GetFiles(info.Path, "*.jcard", SearchOption.TopDirectoryOnly).OrderBy(Path.GetFileName, StringComparer.InvariantCultureIgnoreCase))
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
                    var caption = GetFieldValue(card, info.Digest)?.ToString();
                    var alias = Translate.AliasName(caption);
                    types.Add((alias, card.ID, caption));

                    builder.AppendLine($"//#region {caption?.Trim()}");
                    builder.AppendLine();
                    builder.AppendLine("/**");
                    builder.AppendLine($" * ID: {card.ID:B}");

                    foreach (var field in info.Fields)
                    {
                        builder.AppendLine($" * {field.Name}: {GetFieldValue(card, field.Mapping)?.ToString()?.ToGenString()}.");
                    }

                    builder.AppendLine(" */");
                    builder.AppendLine($"class {alias}{info.Name}Info {{");
                    builder.AppendLine("  //#region Common");
                    builder.AppendLine();
                    builder.AppendLine("  /**");
                    builder.AppendLine($"   * {info.Caption} identifier for \"{caption}\": {card.ID:B}.");
                    builder.AppendLine("   */");
                    builder.AppendLine($"  readonly ID: guid = '{card.ID}';");

                    foreach (var field in info.Fields)
                    {
                        builder.AppendLine("  /**");
                        builder.AppendLine($"   * {field.Caption} for \"{caption}\".");
                        builder.AppendLine("   */");
                        builder.AppendLine($"  readonly {field.Name}: string = '{GetFieldValue(card, field.Mapping)?.ToString()?.ToGenString()}';");
                        builder.AppendLine();
                    }

                    builder.AppendLine("  //#endregion");
                    builder.AppendLine("}");
                    builder.AppendLine();
                    builder.AppendLine("//#endregion");
                    builder.AppendLine();
                }

                builder.AppendLine($"export class {info.Name}Info {{");
                builder.AppendLine($"  //#region {info.Name}Info");
                builder.AppendLine();
                foreach (var (type, id, name) in types.OrderBy(x => x.Alias, StringComparer.InvariantCultureIgnoreCase))
                {
                    builder.AppendLine("  /**");
                    builder.AppendLine($"   * {info.Caption} identifier for \"{name}\": {id:B}.");
                    builder.AppendLine("   */");
                    builder.AppendLine($"  static get {type}(): {type}{info.Name}Info {{");
                    builder.AppendLine($"    return {info.Name}Info.{type.ToFirstLower()} = {info.Name}Info.{type.ToFirstLower()} ?? new {type}{info.Name}Info();");
                    builder.AppendLine("  }");
                    builder.AppendLine();
                    builder.AppendLine($"  private static {type.ToFirstLower()}: {type}{info.Name}Info;");
                    builder.AppendLine();
                }
                builder.AppendLine("  //#endregion");
                builder.AppendLine("}");
                builder.AppendLine();
                builder.Append("//#endregion");

                if (infoCnt != infos.Count)
                {
                    builder.AppendLine();
                    builder.AppendLine();
                }
            }

            return builder.ToString();
        }

        private static List<CustomObject> LoadFromConfiguration(IConfigurationManager configManager, string key)
        {
            var items = new List<CustomObject>();
            if (configManager.Configuration.Settings.TryGetValue(key, out var value) && value is List<object> list)
            {
                foreach (Dictionary<string, object> l in list)
                {
                    var digest = l.Get<Dictionary<string, object>>("Digest");
                    var obj = new CustomObject
                    {
                        Caption = l.Get<string>("Caption"),
                        Name = l.Get<string>("Name"),
                        Path = l.Get<string>("Path"),
                        Digest = new CustomMapping
                        {
                            Section = digest.Get<string>("Section"),
                            Field = digest.Get<string>("Field"),
                            Type = digest.Get<string>("Type")
                        },
                        Fields = new List<CustomField>()
                    };
                    var fields = l.Get<List<object>>("Fields");
                    foreach (Dictionary<string, object> field in fields)
                    {
                        var ff = field.Get<Dictionary<string, object>>("Field");
                        var f = new CustomField
                        {
                            Caption = field.Get<string>("Caption"),
                            Name = field.Get<string>("Name"),
                            Mapping = new CustomMapping
                            {
                                Section = ff.Get<string>("Section"),
                                Field = ff.Get<string>("Field"),
                                Type = ff.Get<string>("Type")
                            }
                        };
                        obj.Fields.Add(f);
                    }

                    items.Add(obj);
                }
            }

            return items;
        }

        private static object GetFieldValue(Card card, CustomMapping mapping)
        {
            var value = card.Sections[mapping.Section].Fields[mapping.Field];
            return mapping.Type.ToLowerInvariant() switch
            {
                "string" => value,
                "guid" => $"{((Guid?)value).GetValueOrDefault().ToGenString()}",
                "int" => value,
                _ => value
            };
        }
    }
}