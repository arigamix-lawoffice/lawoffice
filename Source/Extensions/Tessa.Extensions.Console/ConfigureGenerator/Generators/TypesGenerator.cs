using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Tessa.Cards;
using Tessa.Cards.Metadata;
using Tessa.Platform.Collections;
using Tessa.Platform.Json;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Console.ConfigureGenerator.Generators
{
    public sealed class TypesGenerator : IGenerator
    {
        public async Task<string> GenerateAsync(string path, CancellationToken cancellationToken = default)
        {
            var builder = new StringBuilder();
            builder.AppendLine("using System;");
            builder.AppendLine();
            builder.AppendLine("namespace Tessa.Extensions.Shared.Info");
            builder.AppendLine("{// ReSharper disable InconsistentNaming");

            var docTypesPath = Path.Combine(path, @"..\Cards\Document types");
            var docTypes = new Dictionary<Guid, List<(Guid, string, string)>>();
            if (Directory.Exists(docTypesPath))
            {
                foreach (var file in Directory.GetFiles(docTypesPath, "*.jcard", SearchOption.TopDirectoryOnly).OrderBy(Path.GetFileName))
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
                    var documentId = card.Sections["KrDocType"].Fields.Get<Guid>("CardTypeID");
                    if (!docTypes.ContainsKey(documentId))
                    {
                        docTypes.Add(documentId, new List<(Guid, string, string)>());
                    }

                    var documentName = card.Sections["KrDocType"].Fields.Get<string>("Title");
                    var caption = Translate.ToName(Transliteration.Front(documentName));
                    docTypes[documentId].Add((card.ID, documentName, caption));
                }
            }

            var typeNames = new List<(string, string)>();
            foreach (var file in Directory.GetFiles(path, "*.jtype", SearchOption.AllDirectories).OrderBy(Path.GetFileName))
            {
                var text = await File.ReadAllTextAsync(file, cancellationToken);
                var cardType = new CardType();
                await cardType.DeserializeFromJsonAsync(text);
                var vs = new ControlTypesVisitor(new ValidationResultBuilder());
                await cardType.VisitAsync(vs, cancellationToken);
                var viewTypes = cardType.Extensions.Where(x => x.Type == CardTypeExtensionTypes.MakeViewTableControl);
                foreach (var viewType in viewTypes)
                {
                    var serializedTableSettings = viewType.ExtensionSettings.Get<Dictionary<string, object>>(CardTypeExtensionSettings.TableSettings);
                    var table = new CardTypeTableControl();
                    await table.DeserializeFromStorageAsync(serializedTableSettings);
                    var form = new CardTypeNamedForm();
                    form.Blocks.AddRange(table.Form.Blocks);
                    var viewCardType = new CardType();
                    viewCardType.Forms.Add(form);
                    await viewCardType.VisitAsync(vs, cancellationToken);
                }

                typeNames.Add((cardType.Name, cardType.Caption));
                var group = cardType.Group ?? "(Без группы)";
                builder.AppendLine($"    #region {cardType.Name}");
                builder.AppendLine();
                builder.AppendLine("    /// <summary>");
                builder.AppendLine($"    ///     ID: {cardType.ID:B}");
                builder.AppendLine($"    ///     Alias: {cardType.Name}");
                builder.AppendLine($"    ///     Caption: {cardType.Caption}");
                builder.AppendLine($"    ///     Group: {group}");
                builder.AppendLine("    /// </summary>");
                builder.AppendLine($"    public class {cardType.Name}TypeInfo");
                builder.AppendLine("    {");
                builder.AppendLine("        #region Common");
                builder.AppendLine();
                builder.AppendLine("        /// <summary>");
                builder.AppendLine($"        ///     Card type identifier for \"{cardType.Name}\": {cardType.ID:B}.");
                builder.AppendLine("        /// </summary>");
                builder.AppendLine($"        public readonly Guid ID = new Guid({cardType.ID.ToString("X").Replace("{", "", StringComparison.InvariantCultureIgnoreCase).Replace("}", "", StringComparison.InvariantCultureIgnoreCase)});");
                builder.AppendLine();
                builder.AppendLine("        /// <summary>");
                builder.AppendLine($"        ///     Card type name for \"{cardType.Name}\".");
                builder.AppendLine("        /// </summary>");
                builder.AppendLine($"        public readonly string Alias = \"{cardType.Name}\";");
                builder.AppendLine();
                builder.AppendLine("        /// <summary>");
                builder.AppendLine($"        ///     Card type caption for \"{cardType.Name}\".");
                builder.AppendLine("        /// </summary>");
                builder.AppendLine($"        public readonly string Caption = \"{cardType.Caption.Replace("\"", "\\\"", StringComparison.InvariantCultureIgnoreCase)}\";");
                builder.AppendLine();
                builder.AppendLine("        /// <summary>");
                builder.AppendLine($"        ///     Card type group for \"{cardType.Name}\".");
                builder.AppendLine("        /// </summary>");
                builder.AppendLine($"        public readonly string Group = \"{group}\";");
                builder.AppendLine();
                builder.AppendLine("        #endregion");
                builder.AppendLine();
                if (docTypes.TryGetValue(cardType.ID, out var types))
                {
                    builder.AppendLine("        #region Document Types");
                    builder.AppendLine();
                    foreach (var (id, name, alias) in types)
                    {
                        builder.AppendLine("        /// <summary>");
                        builder.AppendLine($"        ///     Document type identifier for \"{name}\": {id:B}.");
                        builder.AppendLine("        /// </summary>");
                        builder.AppendLine($"        public readonly Guid {alias}ID = new({id.ToGenString()});");
                        builder.AppendLine();
                        builder.AppendLine("        /// <summary>");
                        builder.AppendLine($"        ///     Document type caption for \"{name}\".");
                        builder.AppendLine("        /// </summary>");
                        builder.AppendLine($"        public readonly string {alias}Caption = \"{name.ToGenString()}\";");
                        builder.AppendLine();
                    }

                    builder.AppendLine("        #endregion");
                    builder.AppendLine();
                }

                if (vs.FormsData.Count > 0)
                {
                    builder.AppendLine("        #region Forms");
                    var isFirstForm = true;
                    foreach (var form in vs.FormsData)
                    {
                        var name = Regex.Matches(form.Key, @"\p{IsCyrillic}").Count > 0
                            ? Transliteration.Front(form.Key)
                            : form.Key;
                        name = Translate.ToName(name);
                        if (form.Value != null && !form.Value.StartsWith("$"))
                        {
                            builder.AppendLine();
                            builder.AppendLine("        /// <summary>");
                            builder.AppendLine($"        ///     Form caption \"{form.Value}\" for \"{form.Key}\".");
                            builder.AppendLine("        /// </summary>");
                        }
                        else if (isFirstForm)
                        {
                            isFirstForm = false;
                            builder.AppendLine();
                        }

                        builder.AppendLine($"        public readonly string Form{name} = \"{form.Key}\";");
                    }

                    builder.AppendLine();
                    builder.AppendLine("        #endregion");
                    builder.AppendLine();
                }

                if (vs.BlocksData.Count > 0)
                {
                    builder.AppendLine("        #region Blocks");
                    var isFirstBlock = true;
                    foreach (var block in vs.BlocksData)
                    {
                        if (block.Value != null && !block.Value.StartsWith("$"))
                        {
                            builder.AppendLine();
                            builder.AppendLine("        /// <summary>");
                            builder.AppendLine($"        ///     Block caption \"{block.Value}\" for \"{block.Key}\".");
                            builder.AppendLine("        /// </summary>");
                        }
                        else if (isFirstBlock)
                        {
                            isFirstBlock = false;
                            builder.AppendLine();
                        }

                        builder.AppendLine($"        public readonly string Block{Translate.ToName(block.Key)} = \"{block.Key}\";");
                    }

                    builder.AppendLine();
                    builder.AppendLine("        #endregion");
                    builder.AppendLine();
                }

                if (vs.ControlsData.Count > 0)
                {
                    builder.AppendLine("        #region Controls");
                    var isFirstControl = true;
                    foreach (var control in vs.ControlsData)
                    {
                        if (control.Value != null && !control.Value.StartsWith("$"))
                        {
                            builder.AppendLine();
                            builder.AppendLine("        /// <summary>");
                            builder.AppendLine($"        ///     Control caption \"{control.Value}\" for \"{control.Key}\".");
                            builder.AppendLine("        /// </summary>");
                        }
                        else if (isFirstControl)
                        {
                            isFirstControl = false;
                            builder.AppendLine();
                        }

                        builder.AppendLine($"        public readonly string {control.Key} = nameof({control.Key});");
                    }

                    builder.AppendLine();
                    builder.AppendLine("        #endregion");
                    builder.AppendLine();
                }

                builder.AppendLine("        #region ToString");
                builder.AppendLine();
                builder.AppendLine($"        public static implicit operator string({cardType.Name}TypeInfo obj) => obj.ToString();");
                builder.AppendLine();
                builder.AppendLine("        public override string ToString() => this.Alias;");
                builder.AppendLine();
                builder.AppendLine("        #endregion");
                builder.AppendLine("    }");
                builder.AppendLine();
                builder.AppendLine("    #endregion");
                builder.AppendLine();
            }

            builder.AppendLine("    public static class TypeInfo");
            builder.AppendLine("    {");
            builder.AppendLine("        #region Const");
            builder.AppendLine();
            builder.AppendLine("        public const string CurrentRow = nameof(CurrentRow);");
            builder.AppendLine("        public const string SelectedRow = nameof(SelectedRow);");
            builder.AppendLine("        public const string SelectedItem = nameof(SelectedItem);");
            builder.AppendLine("        public const string SelectedDate = nameof(SelectedDate);");
            builder.AppendLine("        public const string IsActive = nameof(IsActive);");
            builder.AppendLine("        public const string IsChecked = nameof(IsChecked);");
            builder.AppendLine("        public const string IsExpanded = nameof(IsExpanded);");
            builder.AppendLine("        public const string LastUpdateTime = nameof(LastUpdateTime);");
            builder.AppendLine("        public const string Parent = nameof(Parent);");
            builder.AppendLine();
            builder.AppendLine("        #endregion");
            builder.AppendLine();
            builder.AppendLine("        #region Types");
            foreach (var (type, caption) in typeNames)
            {
                builder.AppendLine();
                builder.AppendLine("        /// <summary>");
                builder.AppendLine($"        ///     Card type caption \"{caption}\" for \"{type}\".");
                builder.AppendLine("        /// </summary>");
                builder.AppendLine($"        public static readonly {type}TypeInfo {type} = new {type}TypeInfo();");
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

            var docTypesPath = Path.Combine(path, @"..\Cards\Document types");
            var docTypes = new Dictionary<Guid, List<(Guid, string, string)>>();

            if (Directory.Exists(docTypesPath))
            {
                foreach (var file in Directory.GetFiles(docTypesPath, "*.jcard", SearchOption.TopDirectoryOnly).OrderBy(Path.GetFileName, StringComparer.InvariantCultureIgnoreCase))
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
                    var documentId = card.Sections["KrDocType"].Fields.Get<Guid>("CardTypeID");
                    if (!docTypes.ContainsKey(documentId))
                    {
                        docTypes.Add(documentId, new List<(Guid, string, string)>());
                    }

                    var documentName = card.Sections["KrDocType"].Fields.Get<string>("Title");
                    var alias = Translate.AliasName(documentName);
                    docTypes[documentId].Add((card.ID, documentName, alias));
                }
            }

            var typeNames = new List<(string Alias, Guid ID, string Caption)>();

            foreach (var file in Directory.GetFiles(path, "*.jtype", SearchOption.AllDirectories).OrderBy(Path.GetFileName, StringComparer.InvariantCultureIgnoreCase))
            {
                var text = await File.ReadAllTextAsync(file, cancellationToken);
                var cardType = new CardType();
                await cardType.DeserializeFromJsonAsync(text);
                var vs = new ControlTypesVisitor(new ValidationResultBuilder());
                await cardType.VisitAsync(vs, cancellationToken);
                var viewTypes = cardType.Extensions.Where(x => x.Type == CardTypeExtensionTypes.MakeViewTableControl);
                foreach (var viewType in viewTypes)
                {
                    var serializedTableSettings = viewType.ExtensionSettings.Get<Dictionary<string, object>>(CardTypeExtensionSettings.TableSettings);
                    var table = new CardTypeTableControl();
                    await table.DeserializeFromStorageAsync(serializedTableSettings);
                    var form = new CardTypeNamedForm();
                    form.Blocks.AddRange(table.Form.Blocks);
                    var viewCardType = new CardType();
                    viewCardType.Forms.Add(form);
                    await viewCardType.VisitAsync(vs, cancellationToken);
                }
                typeNames.Add((cardType.Name, cardType.ID, cardType.Caption?.ToGenString()));
                var group = cardType.Group ?? "(Без группы)";
                builder.AppendLine($"//#region {cardType.Name}");
                builder.AppendLine();
                builder.AppendLine("/**");
                builder.AppendLine($" * ID: {cardType.ID:B}");
                builder.AppendLine($" * Alias: {cardType.Name}");
                builder.AppendLine($" * Caption: {cardType.Caption?.ToGenString()}");
                builder.AppendLine($" * Group: {group}");
                builder.AppendLine(" */");
                builder.AppendLine($"class {cardType.Name}TypeInfo {{");
                builder.AppendLine("  //#region Common");
                builder.AppendLine();
                builder.AppendLine("  /**");
                builder.AppendLine($"   * Card type identifier for \"{cardType.Name}\": {cardType.ID:B}.");
                builder.AppendLine("   */");
                builder.AppendLine($"  readonly ID: guid = '{cardType.ID}';");
                builder.AppendLine();
                builder.AppendLine("  /**");
                builder.AppendLine($"   * Card type name for \"{cardType.Name}\".");
                builder.AppendLine("   */");
                builder.AppendLine($"  readonly Alias: string = '{cardType.Name.ToGenString()}';");
                builder.AppendLine();
                builder.AppendLine("  /**");
                builder.AppendLine($"   * Card type caption for \"{cardType.Name}\".");
                builder.AppendLine("   */");
                builder.AppendLine($"  readonly Caption: string = '{cardType.Caption.ToGenString()}';");
                builder.AppendLine();
                builder.AppendLine("  /**");
                builder.AppendLine($"   * Card type group for \"{cardType.Name}\".");
                builder.AppendLine("   */");
                builder.AppendLine($"  readonly Group: string = '{group.ToGenString()}';");
                builder.AppendLine();
                builder.AppendLine("  //#endregion");
                builder.AppendLine();
                if (docTypes.TryGetValue(cardType.ID, out var type))
                {
                    builder.AppendLine("  //#region Document Types");
                    builder.AppendLine();
                    foreach (var (id, name, alias) in type)
                    {
                        builder.AppendLine("  /**");
                        builder.AppendLine($"   * Document type identifier for \"{name}\": {id:B}.");
                        builder.AppendLine("   */");
                        builder.AppendLine($"  readonly {alias}ID: guid = '{id}';");
                        builder.AppendLine();
                        builder.AppendLine("  /**");
                        builder.AppendLine($"   * Document type caption for \"{name}\".");
                        builder.AppendLine("   */");
                        builder.AppendLine($"  readonly {alias}Caption: string = '{name.ToGenString()}';");
                        builder.AppendLine();
                    }

                    builder.AppendLine("  //#endregion");
                    builder.AppendLine();
                }

                if (cardType.InstanceType == CardInstanceType.Dialog)
                {
                    builder.AppendLine("  //#region Scheme Items");
                    builder.AppendLine();
                    builder.AppendLine($"  readonly Scheme: {cardType.Name}SchemeInfoVirtual = new {cardType.Name}SchemeInfoVirtual();");
                    builder.AppendLine();
                    builder.AppendLine("  //#endregion");
                    builder.AppendLine();
                }

                if (vs.TabsData.Count > 0)
                {
                    builder.AppendLine("  //#region Tabs");
                    var isFirstForm = true;
                    foreach (var (tabName, tabCaption) in vs.TabsData)
                    {
                        var name = Translate.AliasName(tabName);
                        if (tabCaption != null && !tabCaption.StartsWith("$"))
                        {
                            isFirstForm = false;
                            builder.AppendLine();
                            builder.AppendLine("  /**");
                            builder.AppendLine($"   * Tab caption \"{tabCaption}\" for \"{tabName.ToGenString()}\".");
                            builder.AppendLine("   */");
                        }
                        else if (isFirstForm)
                        {
                            isFirstForm = false;
                            builder.AppendLine();
                        }

                        builder.AppendLine($"  readonly Tab{name}: string = '{tabName.ToGenString()}';");
                    }

                    builder.AppendLine();
                    builder.AppendLine("  //#endregion");
                    builder.AppendLine();
                }

                if (vs.FormsData.Count > 0)
                {
                    builder.AppendLine("  //#region Forms");
                    var isFirstForm = true;
                    foreach (var (formName, formCaption) in vs.FormsData)
                    {
                        var name = Translate.AliasName(formName);
                        if (formCaption != null && !formCaption.StartsWith("$"))
                        {
                            isFirstForm = false;
                            builder.AppendLine();
                            builder.AppendLine("  /**");
                            builder.AppendLine($"   * Form caption \"{formCaption}\" for \"{formName.ToGenString()}\".");
                            builder.AppendLine("   */");
                        }
                        else if (isFirstForm)
                        {
                            isFirstForm = false;
                            builder.AppendLine();
                        }

                        builder.AppendLine($"  readonly Form{name}: string = '{formName.ToGenString()}';");
                    }

                    builder.AppendLine();
                    builder.AppendLine("  //#endregion");
                    builder.AppendLine();
                }

                if (vs.BlocksData.Count > 0)
                {
                    builder.AppendLine("  //#region Blocks");
                    var isFirstBlock = true;
                    foreach (var (blockName, blockCaption) in vs.BlocksData)
                    {
                        if (blockCaption != null && !blockCaption.StartsWith("$"))
                        {
                            isFirstBlock = false;
                            builder.AppendLine();
                            builder.AppendLine("  /**");
                            builder.AppendLine($"   * Block caption \"{blockCaption}\" for \"{blockName}\".");
                            builder.AppendLine("   */");
                        }
                        else if (isFirstBlock)
                        {
                            isFirstBlock = false;
                            builder.AppendLine();
                        }

                        builder.AppendLine($"  readonly Block{Translate.AliasName(blockName)}: string = '{blockName.ToGenString()}';");
                    }

                    builder.AppendLine();
                    builder.AppendLine("  //#endregion");
                    builder.AppendLine();
                }

                if (vs.ControlsData.Count > 0)
                {
                    builder.AppendLine("  //#region Controls");
                    var isFirstControl = true;
                    foreach (var (typeName, typeCaption) in vs.ControlsData)
                    {
                        if (typeCaption != null && !typeCaption.StartsWith("$"))
                        {
                            builder.AppendLine();
                            builder.AppendLine("  /**");
                            builder.AppendLine($"   * Control caption \"{typeCaption}\" for \"{typeName.ToGenString()}\".");
                            builder.AppendLine("   */");
                        }
                        else if (isFirstControl)
                        {
                            isFirstControl = false;
                            builder.AppendLine();
                        }

                        builder.AppendLine(
                            typeName.Any(x => Translate.InvalidNameChars.Contains(x)) || Regex.Matches(typeName, @"\p{IsCyrillic}").Count > 0
                                ? $"  readonly {Translate.AliasName(typeName)}: string = '{typeName.ToGenString()}';"
                                : $"  readonly {typeName}: string = '{typeName}';");
                    }

                    builder.AppendLine();
                    builder.AppendLine("  //#endregion");
                    builder.AppendLine();
                }

                builder.AppendLine("  //#region ToString");
                builder.AppendLine();
                builder.AppendLine("  get ToString(): string {");
                builder.AppendLine("    return this.Alias;");
                builder.AppendLine("  }");
                builder.AppendLine();
                builder.AppendLine("  //#endregion");
                builder.AppendLine("}");
                builder.AppendLine();
                builder.AppendLine("//#endregion");
                builder.AppendLine();

                if (cardType.InstanceType == CardInstanceType.Dialog)
                {
                    builder.AppendLine($"//#region {cardType.Name} Scheme Items");
                    builder.AppendLine();
                    builder.AppendLine("//#region SchemeInfo");
                    builder.AppendLine();
                    builder.AppendLine($"class {cardType.Name}SchemeInfoVirtual {{");

                    foreach (var table in cardType.CardTypeSections)
                    {
                        builder.AppendLine($"  readonly {table.Name}: {table.Name}{cardType.Name}SchemeInfoVirtual = new {table.Name}{cardType.Name}SchemeInfoVirtual();");
                    }

                    builder.AppendLine("}");
                    builder.AppendLine();
                    builder.AppendLine("//#endregion");
                    builder.AppendLine();

                    builder.AppendLine("//#region Tables");
                    builder.AppendLine();

                    foreach (var table in cardType.CardTypeSections)
                    {
                        builder.AppendLine($"//#region {table.Name}");
                        builder.AppendLine();
                        builder.AppendLine("/**");
                        builder.AppendLine($" * ID: {table.ID:B}");
                        builder.AppendLine($" * Alias: {table.Name}{(!string.IsNullOrWhiteSpace(table.Description) ? "\r\n * Description: " + Translate.ToMultiLine(table.Description, forWeb: true) : string.Empty)}");
                        builder.AppendLine(" */");

                        if (table.Name.Contains("_", StringComparison.OrdinalIgnoreCase) || cardType.Name.Contains("_", StringComparison.OrdinalIgnoreCase))
                        {
                            builder.AppendLine("// tslint:disable-next-line:class-name");
                        }

                        builder.AppendLine($"class {table.Name}{cardType.Name}SchemeInfoVirtual {{");
                        builder.AppendLine($"  private readonly name: string = \"{table.Name}\";");
                        if (table.Columns.Count > 0)
                        {
                            builder.AppendLine();
                            builder.AppendLine("  //#region Columns");
                            builder.AppendLine();
                            foreach (var column in table.Columns)
                            {
                                if (column.ColumnType == CardMetadataColumnType.Complex)
                                {
                                    foreach (var simpleColumn in column.ReferencedColumns)
                                    {
                                        builder.AppendLine($"  readonly {simpleColumn.Name}: string = '{simpleColumn.Name}';");
                                        if (Regex.Matches(simpleColumn.Name, @"\p{IsCyrillic}").Count > 0)
                                        {
                                            throw new Exception($"Column {simpleColumn.Name} (table - {table.Name}) has cyrillic symbols!");
                                        }
                                    }
                                }
                                else
                                {
                                    builder.AppendLine($"  readonly {column.Name}: string = '{column.Name}';");
                                    if (Regex.Matches(column.Name, @"\p{IsCyrillic}").Count > 0)
                                    {
                                        throw new Exception($"Column {column.Name} (table - {table.Name}) has cyrillic symbols!");
                                    }
                                }
                            }

                            builder.AppendLine();
                            builder.AppendLine("  //#endregion");
                        }

                        builder.AppendLine();
                        builder.AppendLine("  //#region ToString");
                        builder.AppendLine();
                        builder.AppendLine("  get ToString(): string {");
                        builder.AppendLine("    return this.name;");
                        builder.AppendLine("  }");
                        builder.AppendLine();
                        builder.AppendLine("  //#endregion");
                        builder.AppendLine("}");
                        builder.AppendLine();
                        builder.AppendLine("//#endregion");
                        builder.AppendLine();
                    }

                    builder.AppendLine("//#endregion");
                    builder.AppendLine();
                    builder.AppendLine("//#endregion");
                    builder.AppendLine();
                }
            }

            builder.AppendLine("export class TypeInfo {");
            builder.AppendLine("  //#region Const");
            builder.AppendLine();
            builder.AppendLine("   readonly CurrentRow: string = 'CurrentRow';");
            builder.AppendLine("   readonly SelectedRow: string = 'SelectedRow';");
            builder.AppendLine("   readonly SelectedItem: string = 'SelectedItem';");
            builder.AppendLine("   readonly SelectedItemCount: string = 'SelectedItemCount';");
            builder.AppendLine("   readonly SelectedDate: string = 'SelectedDate';");
            builder.AppendLine("   readonly IsActive: string = 'IsActive';");
            builder.AppendLine("   readonly IsChecked: string = 'IsChecked';");
            builder.AppendLine("   readonly IsExpanded: string = 'IsExpanded';");
            builder.AppendLine("   readonly IsDataLoading: string = 'IsDataLoading';");
            builder.AppendLine("   readonly LastUpdateTime: string = 'LastUpdateTime';");
            builder.AppendLine("   readonly Parent: string = 'Parent';");
            builder.AppendLine("   readonly Count: string = 'Count';");
            builder.AppendLine();
            builder.AppendLine("  //#endregion");
            builder.AppendLine();
            builder.AppendLine("  //#region Types");
            builder.AppendLine();
            foreach (var (alias, id, caption) in typeNames.OrderBy(x => x.Alias, StringComparer.InvariantCultureIgnoreCase))
            {
                builder.AppendLine("  /**");
                builder.AppendLine($"   * Card type identifier for \"{caption}\": {id:B}.");
                builder.AppendLine("   */");
                builder.AppendLine($"  static get {alias}(): {alias}TypeInfo {{");
                builder.AppendLine($"    return TypeInfo.{alias.ToFirstLower()} = TypeInfo.{alias.ToFirstLower()} ?? new {alias}TypeInfo();");
                builder.AppendLine("  }");
                builder.AppendLine();
                builder.AppendLine($"  private static {alias.ToFirstLower()}: {alias}TypeInfo;");
                builder.AppendLine();
            }

            builder.AppendLine("  //#endregion");
            builder.Append('}');

            return builder.ToString();
        }
    }
}