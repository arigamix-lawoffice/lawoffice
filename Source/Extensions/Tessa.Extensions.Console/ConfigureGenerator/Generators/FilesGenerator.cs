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
    public sealed class FilesGenerator : IGenerator
    {
        public async Task<string> GenerateAsync(string cardPath, CancellationToken cancellationToken = default)
        {
            var builder = new StringBuilder();
            builder.AppendLine("using System;");
            builder.AppendLine("using Tessa.Files;");
            builder.AppendLine();
            builder.AppendLine("namespace Tessa.Extensions.Shared.Info");
            builder.AppendLine("{// ReSharper disable InconsistentNaming");
            var folders = new List<string> { "OfficeSolution\\FileCategories", "File templates", "VirtualFiles"};
            var cardTypes = new Dictionary<string, List<(string, Card)>>();
            foreach (var folder in folders)
            {
                var dirPath = Path.Combine(cardPath, folder);
                if (!Directory.Exists(dirPath))
                {
                    continue;
                }

                foreach (var file in Directory.GetFiles(dirPath, "*.jcard", SearchOption.TopDirectoryOnly).OrderBy(Path.GetFileName))
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
                    var typeInfo = card.TypeName switch
                    {
                        "FileCategory" => "CategoryInfo", // Категория файла
                        "FileTemplate" => "TemplateInfo", // Файловый шаблон
                        "KrVirtualFile" => "VirtualInfo", // Виртуальный файл
                        _ => string.Empty
                    };
                    var shortType = typeInfo.Replace("Info", string.Empty, StringComparison.Ordinal);
                    if (!cardTypes.ContainsKey(shortType))
                    {
                        cardTypes.Add(shortType, new List<(string, Card)>());
                    }
                    cardTypes[shortType].Add((folder, card));
                }
            }

            foreach (var type in cardTypes)
            {
                if (type.Key == "Template")
                {
                    builder.AppendLine("    #region TemplateInfo");
                    builder.AppendLine();
                    builder.AppendLine("    public sealed class TemplateInfo");
                    builder.AppendLine("    {");
                    builder.AppendLine("        public readonly Guid ID;");
                    builder.AppendLine("        public readonly string Name;");
                    builder.AppendLine();
                    builder.AppendLine("        public TemplateInfo(Guid ID, string Name)");
                    builder.AppendLine("        {");
                    builder.AppendLine("            this.ID = ID;");
                    builder.AppendLine("            this.Name = Name;");
                    builder.AppendLine("        }");
                    builder.AppendLine("    }");
                    builder.AppendLine();
                    builder.AppendLine("    #endregion");
                    builder.AppendLine();
                }

                if (type.Key == "Virtual")
                {
                    builder.AppendLine("    #region VirtualInfo");
                    builder.AppendLine();
                    builder.AppendLine("    public sealed class VirtualInfo");
                    builder.AppendLine("    {");
                    builder.AppendLine("        public readonly Guid ID;");
                    builder.AppendLine("        public readonly string Name;");
                    builder.AppendLine();
                    builder.AppendLine("        public VirtualInfo(Guid ID, string Name)");
                    builder.AppendLine("        {");
                    builder.AppendLine("            this.ID = ID;");
                    builder.AppendLine("            this.Name = Name;");
                    builder.AppendLine("        }");
                    builder.AppendLine("    }");
                    builder.AppendLine();
                    builder.AppendLine("    #endregion");
                    builder.AppendLine();
                }
                builder.AppendLine($"    #region {type.Key}");
                builder.AppendLine();
                builder.AppendLine($"    public sealed class {type.Key}FileInfo");
                builder.AppendLine("    {");
                foreach (var (path, card) in cardTypes[type.Key])
                {
                    var caption = card.TypeName switch
                    {
                        "FileCategory" => card.Sections["FileCategories"].Fields.Get<string>("Name"), // Категория файла
                        "FileTemplate" => card.Sections["FileTemplates"].Fields.Get<string>("Name"), // Файловый шаблон
                        "KrVirtualFile" => card.Sections["KrVirtualFiles"].Fields.Get<string>("Name"), // Виртуальный файл
                        var _ => string.Empty
                    };
                    var alias = Translate.ToName(Transliteration.Front(caption.Replace("$", "", StringComparison.Ordinal).Replace("(", "", StringComparison.Ordinal).Replace(")", "", StringComparison.Ordinal).Replace("_", " ", StringComparison.Ordinal)).Replace("'s", "", StringComparison.Ordinal));
                    if (card.TypeName == "FileCategory")
                    {
                        builder.AppendLine("         /// <summary>");
                        builder.AppendLine($"         ///     {path} for \"{caption}\": {card.ID:B}.");
                        builder.AppendLine("         /// </summary>");
                        builder.AppendLine($"         public readonly IFileCategory {alias} = new FileCategory(new Guid({card.ID.ToString("X").Replace("{", "", StringComparison.Ordinal).Replace("}", "", StringComparison.Ordinal)}), \"{caption}\");");
                        builder.AppendLine();
                    }
                    else if (card.TypeName == "FileTemplate")
                    {
                        builder.AppendLine("         /// <summary>");
                        builder.AppendLine($"         ///     {path} for \"{caption}\": {card.ID:B}.");
                        builder.AppendLine("         /// </summary>");
                        builder.AppendLine($"         public readonly TemplateInfo {alias} = new TemplateInfo(new Guid({card.ID.ToString("X").Replace("{", "", StringComparison.Ordinal).Replace("}", "", StringComparison.Ordinal)}), \"{caption}\");");
                        builder.AppendLine();
                    }
                    else if (card.TypeName == "KrVirtualFile")
                    {
                        builder.AppendLine("         /// <summary>");
                        builder.AppendLine($"         ///     {path} for \"{caption}\": {card.ID:B}.");
                        builder.AppendLine("         /// </summary>");
                        builder.AppendLine($"         public readonly VirtualInfo {alias} = new VirtualInfo(new Guid({card.ID.ToString("X").Replace("{", "", StringComparison.Ordinal).Replace("}", "", StringComparison.Ordinal)}), \"{caption}\");");
                        builder.AppendLine();
                    }
                }
                
                builder.AppendLine("    }");
                builder.AppendLine();
                builder.AppendLine("    #endregion");
                builder.AppendLine();
            }
            builder.AppendLine("    public static class FilesInfo");
            builder.AppendLine("    {");
            builder.AppendLine("        #region FileInfo");
            builder.AppendLine();
            foreach (var type in cardTypes.Keys)
            {
                builder.AppendLine($"        public static readonly {type}FileInfo {type} = new {type}FileInfo();");
            }
            builder.AppendLine();
            builder.AppendLine("        #endregion");
            builder.AppendLine("    }");
            builder.Append('}');

            return builder.ToString();
        }

        public async Task<string> GenerateWebAsync(string cardPath, CancellationToken cancellationToken = default)
        {
            var builder = new StringBuilder();
            builder.AppendLine("// noinspection JSUnusedGlobalSymbols,SpellCheckingInspection");
            builder.AppendLine();
            builder.AppendLine("import { FileCategory } from 'tessa/files';");
            builder.AppendLine();

            var folders = new List<string> { "File categories", "File templates", "Virtual files", "Категория файлов" };
            var cardTypes = new Dictionary<string, List<(string Path, Card Card)>>();
            foreach (var folder in folders)
            {
                var dirPath = Path.Combine(cardPath, folder);
                if (!Directory.Exists(dirPath))
                {
                    continue;
                }

                foreach (var file in Directory.GetFiles(dirPath, "*.jcard", SearchOption.TopDirectoryOnly).OrderBy(Path.GetFileName, StringComparer.InvariantCultureIgnoreCase))
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
                    var typeInfo = card.TypeName switch
                    {
                        "ApFileCategories" => "CategoryInfo", // Категория файла
                        "FileCategory" => "CategoryInfo", // Категория файла
                        "FileTemplate" => "TemplateInfo", // Файловый шаблон
                        "KrVirtualFile" => "VirtualInfo", // Виртуальный файл
                        var _ => string.Empty
                    };
                    var shortType = typeInfo.Replace("Info", string.Empty, StringComparison.Ordinal);
                    if (!cardTypes.ContainsKey(shortType))
                    {
                        cardTypes.Add(shortType, new List<(string, Card)>());
                    }

                    cardTypes[shortType].Add((folder, card));
                }
            }

            foreach (var (type, value) in cardTypes.OrderBy(x => x.Key, StringComparer.InvariantCultureIgnoreCase))
            {
                builder.AppendLine($"//#region {type}");
                builder.AppendLine();
                builder.AppendLine($"class {type}FileInfo {{");
                builder.AppendLine($"  //#region {type}");
                builder.AppendLine();
                foreach (var (path, card) in value.OrderBy(x => x.Path, StringComparer.InvariantCultureIgnoreCase))
                {
                    var caption = card.TypeName switch
                    {
                        "ApFileCategories" => card.Sections["ApFileCategory"].Fields.Get<string>("Name"), // Категория файла
                        "FileCategory" => card.Sections["FileCategories"].Fields.Get<string>("Name"), // Категория файла
                        "FileTemplate" => card.Sections["FileTemplates"].Fields.Get<string>("Name"), // Файловый шаблон
                        "KrVirtualFile" => card.Sections["KrVirtualFiles"].Fields.Get<string>("Name"), // Виртуальный файл
                        var _ => string.Empty
                    };
                    var alias = Translate.AliasName(caption);
                    switch (card.TypeName)
                    {
                        case "FileCategory" or "ApFileCategories":
                            builder.AppendLine("  /**");
                            builder.AppendLine($"   * {path} for \"{caption}\": {card.ID:B}.");
                            builder.AppendLine("   */");
                            builder.AppendLine($"  readonly {alias}: FileCategory = new FileCategory('{card.ID}', '{caption.ToGenString()}');");
                            builder.AppendLine();
                            break;
                        case "FileTemplate":
                            builder.AppendLine("  /**");
                            builder.AppendLine($"   * {path} for \"{caption}\": {card.ID:B}.");
                            builder.AppendLine("   */");
                            builder.AppendLine($"  readonly {alias}: {{ ID: string; Name: string }} = {{ ID: '{card.ID}', Name: '{caption.ToGenString()}' }};");
                            builder.AppendLine();
                            break;
                        case "KrVirtualFile":
                            builder.AppendLine("  /**");
                            builder.AppendLine($"   * {path} for \"{caption}\": {card.ID:B}.");
                            builder.AppendLine("   */");
                            builder.AppendLine($"  readonly {alias}: {{ ID: string; Name: string }} = {{ ID: '{card.ID}', Name: '{caption.ToGenString()}' }};");
                            builder.AppendLine();
                            break;
                    }
                }

                builder.AppendLine("  //#endregion");
                builder.AppendLine("}");
                builder.AppendLine();
                builder.AppendLine("//#endregion");
                builder.AppendLine();
            }

            builder.AppendLine("export class FilesInfo {");
            builder.AppendLine("  //#region FileInfo");
            builder.AppendLine();
            foreach (var type in cardTypes.Keys.OrderBy(x => x, StringComparer.InvariantCultureIgnoreCase))
            {
                builder.AppendLine($"  static get {type}(): {type}FileInfo {{");
                builder.AppendLine($"    return FilesInfo.{type.ToFirstLower()} = FilesInfo.{type.ToFirstLower()} ?? new {type}FileInfo();");
                builder.AppendLine("  }");
                builder.AppendLine();
                builder.AppendLine($"  private static {type.ToFirstLower()}: {type}FileInfo;");
                builder.AppendLine();
            }
            builder.AppendLine("  //#endregion");
            builder.Append('}');

            return builder.ToString();
        }
    }
}