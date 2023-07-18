using LinqToDB.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards.Metadata;
using Tessa.Scheme;

namespace Tessa.Extensions.Console.ConfigureGenerator.Generators
{
    public sealed class SchemeGenerator : IGenerator
    {
        public async Task<string> GenerateAsync(string path, CancellationToken cancellationToken = default)
        {
            var builder = new StringBuilder();
            builder.AppendLine("using System;");
            builder.AppendLine();
            builder.AppendLine("namespace Tessa.Extensions.Shared.Info");
            builder.AppendLine("{// ReSharper disable InconsistentNaming");

            var tsdFilePath = Directory.EnumerateFiles(path, "*.tsd").OrderBy(x => x).FirstOrDefault();
            if (string.IsNullOrWhiteSpace(tsdFilePath))
            {
                throw new Exception("*.tsd file not found at path " + path);
            }

            var partitions = FileSchemeService.GetPartitionPaths(tsdFilePath);
            var service = new FileSchemeService(tsdFilePath, partitions, SchemeServiceOptions.ReadOnly);
            var tablesUnordered = await service.GetTablesAsync(cancellationToken);
            var tables = tablesUnordered.OrderBy(x => x.Name).ToList();
            foreach (var table in tables)
            {
                builder.AppendLine($"    #region {table.Name}");
                builder.AppendLine();
                builder.AppendLine("    /// <summary>");
                builder.AppendLine($"    ///     ID: {table.ID:B}");
                builder.AppendLine($"    ///     Alias: {table.Name}");
                builder.AppendLine($"    ///     Group: {table.Group}{(!string.IsNullOrWhiteSpace(table.Description) ? "\r\n    ///     Description: " + table.Description.Replace("\"", "\\\"", StringComparison.InvariantCultureIgnoreCase).Replace("\r", string.Empty, StringComparison.InvariantCultureIgnoreCase).Replace("\n", "\n    ///                  ", StringComparison.InvariantCultureIgnoreCase) : string.Empty)}");
                builder.AppendLine("    /// </summary>");
                builder.AppendLine($"    public sealed class {table.Name}SchemeInfo");
                builder.AppendLine("    {");
                builder.AppendLine($"        private const string name = \"{table.Name}\";");
                if (table.Columns.Count > 0)
                {
                    builder.AppendLine();
                    builder.AppendLine("        #region Columns");
                    builder.AppendLine();
                    foreach (var column in table.Columns)
                    {
                        if (column is SchemeComplexColumn complexColumn)
                        {
                            foreach (var simpleColumn in complexColumn.Columns)
                            {
                                builder.AppendLine($"        public readonly string {simpleColumn.Name} = nameof({simpleColumn.Name});");
                            }
                        }
                        else
                        {
                            builder.AppendLine($"        public readonly string {column.Name} = nameof({column.Name});");
                        }
                    }

                    builder.AppendLine();
                    builder.AppendLine("        #endregion");
                }

                if (table.Records.Count > 0)
                {
                    builder.AppendLine();
                    builder.AppendLine("        #region Enumeration");
                    builder.AppendLine();

                    var enumRecordNames = new List<string>();
                    var nameColumn = table
                        .Columns
                        .Where(x => x.Type != SchemeType.Guid && Translate.IsNotNull(x.Name, table.Records))
                        .OrderByDescending(x => x.Type, new SchemeTypeComparer())
                        .FirstOrDefault();
                    var cnt = 1;
                    builder.AppendLine($"        public static class Records");
                    builder.AppendLine("        {");
                    
                    foreach (var record in table.Records)
                    {
                        var objects = new Dictionary<string, object>();
                        foreach (var column in record.Columns)
                        {
                            var tableValue = record[column];
                            var storageValue = CardMetadataHelper.CoerceAfterLoading(tableValue, column.Type.DbType);
                            if (storageValue == null)
                            {
                                storageValue = "null";
                            }
                            else if (storageValue is string)
                            {
                                storageValue = "\"" + storageValue.ToString().Replace("\"", "\\\"", StringComparison.InvariantCultureIgnoreCase) + "\"";
                            }
                            else if (storageValue is Guid guidValue)
                            {
                                if (guidValue == Guid.Empty)
                                {
                                    storageValue = "Guid.Empty";
                                }
                                else
                                {
                                    storageValue = "new Guid(" + guidValue.ToString("X").Replace("{", "", StringComparison.InvariantCultureIgnoreCase).Replace("}", "", StringComparison.InvariantCultureIgnoreCase) + ")";
                                }
                            }
                            else if (storageValue is bool boolValue)
                            {
                                storageValue = boolValue.ToString().ToLower();
                            }

                            objects.Add(column.Name, storageValue);
                        }

                        string enumName;
                        if (nameColumn == null)
                        {
                            enumName = "Enum" + cnt++;
                        }
                        else
                        {
                            enumName = objects[nameColumn.Name].ToString().Replace("\"", "", StringComparison.InvariantCultureIgnoreCase);
                            if (Regex.Matches(enumName, @"\p{IsCyrillic}").Count > 0)
                            {
                                enumName = Transliteration.Front(enumName);
                            }

                            var index = enumName.LastIndexOf("_", StringComparison.CurrentCultureIgnoreCase);
                            if (enumName.StartsWith("$") && index != -1 && enumName.Length - 1 > index)
                            {
                                enumName = enumName.Substring(index + 1);
                            }

                            enumName = Translate.ToName(enumName);
                            if (SchemeTypeComparer.IsIntegerType(nameColumn.Type.DbType) || int.TryParse(enumName, out _) || int.TryParse(enumName[0].ToString(), out _))
                            {
                                enumName = "Enum" + enumName;
                            }

                            if (enumRecordNames.Contains(enumName))
                            {
                                enumName += cnt++;
                            }
                        }

                        enumRecordNames.Add(enumName);

                        builder.AppendLine($"            public static class {enumName}");
                        builder.AppendLine("            {");
                        int k = 0;
                        foreach (var column in table.Columns)
                        {
                            if (column.Type.ClrType == typeof(Guid) || column.Type.ClrType.IsNullable())
                            {
                                builder.AppendLine($"                    public static {Translate.TypeNameOrAlias(CardMetadataHelper.GetRuntimeType(CardMetadataHelper.GetMetadataRuntimeType(column.Type.ClrType))) + Translate.GetNullable(column.Type.ClrType)} {column.Name} = {objects.Values.ElementAt(k)};");
                            }
                            else
                            {
                                builder.AppendLine($"                    public const {Translate.TypeNameOrAlias(CardMetadataHelper.GetRuntimeType(CardMetadataHelper.GetMetadataRuntimeType(column.Type.ClrType))) + Translate.GetNullable(column.Type.ClrType)} {column.Name} = {objects.Values.ElementAt(k)};");
                            }
                           
                            k++;
                        }
                        builder.AppendLine("            }");

                    }
                    builder.AppendLine("        }");
                    builder.AppendLine();
                    builder.AppendLine("        #endregion");
                }
                else if (table.Name == "Functions")
                {
                    builder.AppendLine();
                    builder.AppendLine("        #region Functions");
                    builder.AppendLine();
                    var functionsUnordered = await service.GetFunctionsAsync(cancellationToken);
                    var functions = functionsUnordered.OrderBy(x => x.Name).ToList();
                    foreach (var function in functions)
                    {
                        builder.AppendLine($"        public const string {function.Name} = nameof({function.Name});");
                    }

                    builder.AppendLine();
                    builder.AppendLine("        #endregion");
                }
                else if (table.Name == "Procedures")
                {
                    builder.AppendLine();
                    builder.AppendLine("        #region Procedures");
                    builder.AppendLine();
                    var proceduresUnordered = await service.GetProceduresAsync(cancellationToken);
                    var procedures = proceduresUnordered.OrderBy(x => x.Name).ToList();
                    foreach (var procedure in procedures)
                    {
                        builder.AppendLine($"        public const string {procedure.Name} = nameof({procedure.Name});");
                    }

                    builder.AppendLine();
                    builder.AppendLine("        #endregion");
                }

                builder.AppendLine();
                builder.AppendLine("        #region ToString");
                builder.AppendLine();
                builder.AppendLine($"        public static implicit operator string({table.Name}SchemeInfo obj) => obj.ToString();");
                builder.AppendLine();
                builder.AppendLine("        public override string ToString() => name;");
                builder.AppendLine();
                builder.AppendLine("        #endregion");
                builder.AppendLine("    }");
                builder.AppendLine();
                if (table.Records.Count > 0)
                {
                    builder.AppendLine($"    #region {table.Name} Enumeration");
                    builder.AppendLine();
                    builder.AppendLine($"    public sealed class {table.Name}");
                    builder.AppendLine("    {");
                    foreach (var column in table.Columns)
                    {
                        builder.AppendLine($"        public readonly {Translate.TypeNameOrAlias(CardMetadataHelper.GetRuntimeType(CardMetadataHelper.GetMetadataRuntimeType(column.Type.ClrType))) + Translate.GetNullable(column.Type.ClrType)} {column.Name};");
                    }

                    builder.AppendLine();
                    builder.AppendLine($"        public {table.Name}({string.Join(", ", table.Columns.Select(x => Translate.TypeNameOrAlias(CardMetadataHelper.GetRuntimeType(CardMetadataHelper.GetMetadataRuntimeType(x.Type.ClrType))) + Translate.GetNullable(x.Type.ClrType) + " " + x.Name))})");
                    builder.AppendLine("        {");
                    foreach (var column in table.Columns)
                    {
                        builder.AppendLine($"            this.{column.Name} = {column.Name};");
                    }

                    builder.AppendLine("        }");
                    builder.AppendLine("    }");
                    builder.AppendLine();
                    builder.AppendLine("    #endregion");
                    builder.AppendLine();
                }

                builder.AppendLine("    #endregion");
                builder.AppendLine();
            }

            builder.AppendLine("    public static class SchemeInfo");
            builder.AppendLine("    {");
            builder.AppendLine("        #region Tables");
            builder.AppendLine();
            foreach (var table in tables)
            {
                builder.AppendLine($"        public static readonly {table.Name}SchemeInfo {table.Name} = new {table.Name}SchemeInfo();");
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

            var tsdFilePath = Directory.EnumerateFiles(path, "*.tsd").OrderBy(x => x, StringComparer.InvariantCultureIgnoreCase).FirstOrDefault();
            if (string.IsNullOrWhiteSpace(tsdFilePath))
            {
                throw new Exception("*.tsd file not found at path " + path);
            }

            var partitions = FileSchemeService.GetPartitionPaths(tsdFilePath);
            var service = new FileSchemeService(tsdFilePath, partitions, SchemeServiceOptions.ReadOnly);
            var tablesUnordered = await service.GetTablesAsync(cancellationToken);
            var tables = tablesUnordered.OrderBy(x => x.Name, StringComparer.InvariantCultureIgnoreCase).ToList();

            foreach (var table in tables)
            {
                builder.AppendLine($"//#region {table.Name}");
                builder.AppendLine();
                builder.AppendLine("/**");
                builder.AppendLine($" * ID: {table.ID:B}");
                builder.AppendLine($" * Alias: {table.Name}");
                builder.AppendLine($" * Group: {table.Group}{(!string.IsNullOrWhiteSpace(table.Description) ? "\r\n * Description: " + Translate.ToMultiLine(table.Description, forWeb: true) : string.Empty)}");
                builder.AppendLine(" */");
                builder.AppendLine($"class {table.Name}SchemeInfo {{");
                builder.AppendLine($"  private readonly name: string = '{table.Name}';");
                if (table.Columns.Count > 0)
                {
                    builder.AppendLine();
                    builder.AppendLine("  //#region Columns");
                    builder.AppendLine();
                    foreach (var column in table.Columns)
                    {
                        if (column is SchemeComplexColumn complexColumn)
                        {
                            foreach (var simpleColumn in complexColumn.Columns)
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

                if (table.Records.Count > 0)
                {
                    builder.AppendLine();
                    builder.AppendLine("  //#region Enumeration");
                    builder.AppendLine();

                    var enumRecordNames = new List<string>();
                    var nameColumn = table
                        .Columns
                        .Where(x => x.Type != SchemeType.Guid && Translate.IsNotNull(x.Name, table.Records))
                        .OrderByDescending(x => x.Type, new SchemeTypeComparer())
                        .FirstOrDefault();
                    foreach (var record in table.Records)
                    {
                        var objects = new Dictionary<string, object>();
                        foreach (var column in record.Columns)
                        {
                            var tableValue = record[column];
                            var storageValue = CardMetadataHelper.CoerceAfterLoading(tableValue, column.Type.DbType);
                            switch (storageValue)
                            {
                                case null:
                                    storageValue = "null";
                                    break;
                                case string:
                                    storageValue = $"'{storageValue.ToString().ToGenString()}'";
                                    break;
                                case Guid guidValue:
                                    storageValue = $"'{guidValue.ToString()}'";
                                    break;
                                case bool boolValue:
                                    storageValue = boolValue.ToString().ToLower();
                                    break;
                            }

                            objects.Add(column.Name, storageValue);
                        }

                        var enumName = GetEnumName(nameColumn, objects, enumRecordNames);
                        enumRecordNames.Add(enumName);

                        builder.AppendLine($"  readonly {enumName}: {table.Name} = new {table.Name}({string.Join(", ", objects.Values)});");
                    }

                    builder.AppendLine();
                    builder.AppendLine("  //#endregion");
                }
                else
                {
                    switch (table.Name)
                    {
                        case "Functions":
                        {
                            builder.AppendLine();
                            builder.AppendLine("  //#region Functions");
                            builder.AppendLine();
                            var functionsUnordered = await service.GetFunctionsAsync(cancellationToken);
                            var functions = functionsUnordered.OrderBy(x => x.Name, StringComparer.InvariantCultureIgnoreCase).ToList();
                            foreach (var function in functions)
                            {
                                builder.AppendLine($"  readonly {function.Name} = '{function.Name}';");
                                if (Regex.Matches(function.Name, @"\p{IsCyrillic}").Count > 0)
                                {
                                    throw new Exception($"Function {function.Name} has cyrillic symbols!");
                                }
                            }

                            builder.AppendLine();
                            builder.AppendLine("  //#endregion");
                            break;
                        }
                        case "Procedures":
                        {
                            builder.AppendLine();
                            builder.AppendLine("  //#region Procedures");
                            builder.AppendLine();
                            var proceduresUnordered = await service.GetProceduresAsync(cancellationToken);
                            var procedures = proceduresUnordered.OrderBy(x => x.Name, StringComparer.InvariantCultureIgnoreCase).ToList();
                            foreach (var procedure in procedures)
                            {
                                builder.AppendLine($"  readonly {procedure.Name} = '{procedure.Name}';");
                                if (Regex.Matches(procedure.Name, @"\p{IsCyrillic}").Count > 0)
                                {
                                    throw new Exception($"Procedure {procedure.Name} has cyrillic symbols!");
                                }
                            }

                            builder.AppendLine();
                            builder.AppendLine("  //#endregion");
                            break;
                        }
                    }
                }

                builder.AppendLine();
                builder.AppendLine("  //#region toString");
                builder.AppendLine();
                builder.AppendLine("  get getName(): string {");
                builder.AppendLine("    return this.name;");
                builder.AppendLine("  }");
                builder.AppendLine();
                builder.AppendLine("  //#endregion");
                builder.AppendLine("}");
                builder.AppendLine();
                if (table.Records.Count > 0)
                {
                    builder.AppendLine($"//#region {table.Name} Enumeration");
                    builder.AppendLine();
                    builder.AppendLine($"class {table.Name} {{");
                    foreach (var column in table.Columns)
                    {
                        builder.AppendLine($"  readonly {column.Name}: {Translate.TypeNameOrAlias(CardMetadataHelper.GetRuntimeType(CardMetadataHelper.GetMetadataRuntimeType(column.Type.ClrType)), true) + Translate.GetNullable(column.Type.ClrType, true)};");
                    }

                    builder.AppendLine();
                    builder.AppendLine($"  constructor ({string.Join(", ", table.Columns.Select(x => x.Name + ": " + Translate.TypeNameOrAlias(CardMetadataHelper.GetRuntimeType(CardMetadataHelper.GetMetadataRuntimeType(x.Type.ClrType)), true) + Translate.GetNullable(x.Type.ClrType, true)))}) {{");
                    foreach (var column in table.Columns)
                    {
                        builder.AppendLine($"    this.{column.Name} = {column.Name};");
                    }

                    builder.AppendLine("  }");
                    builder.AppendLine("}");
                    builder.AppendLine();
                    builder.AppendLine("//#endregion");
                    builder.AppendLine();
                }

                builder.AppendLine("//#endregion");
                builder.AppendLine();
            }

            builder.AppendLine();
            builder.AppendLine(@"//#endregion");
            builder.AppendLine();

            builder.AppendLine("export class SchemeInfo {");
            builder.AppendLine("  //#region Tables");
            builder.AppendLine();
            foreach (var table in tables)
            {
                builder.AppendLine("  /**");
                builder.AppendLine($"   * Table identifier for \"{table.Name}\": {table.ID:B}.");
                builder.AppendLine("   */");
                builder.AppendLine($"  static get {table.Name}(): {table.Name}SchemeInfo {{");
                builder.AppendLine($"    return SchemeInfo.{table.Name.ToFirstLower()} = SchemeInfo.{table.Name.ToFirstLower()} ?? new {table.Name}SchemeInfo();");
                builder.AppendLine("  }");
                builder.AppendLine();
                builder.AppendLine($"  private static {table.Name.ToFirstLower()}: {table.Name}SchemeInfo;");
                builder.AppendLine();
            }

            builder.AppendLine("  //#endregion");
            builder.Append('}');

            return builder.ToString();
        }

        private static string GetEnumName(SchemeColumn nameColumn, Dictionary<string, object> objects, List<string> enumRecordNames)
        {
            var enumName = GetEnumNameCore(nameColumn, objects);
            return enumRecordNames.Contains(enumName)
                ? Translate.UniqueAliasName(enumRecordNames, enumName)
                : enumName;
        }

        private static string GetEnumNameCore(SchemeColumn nameColumn, Dictionary<string, object> objects)
        {
            if (nameColumn == null)
            {
                return "Enum";
            }

            var enumName = Translate.AliasName(objects[nameColumn.Name].ToString());
            return SchemeTypeComparer.IsIntegerType(nameColumn.Type.DbType) || int.TryParse(enumName, out _) || int.TryParse(enumName[0].ToString(), out _)
                ? "Enum" + enumName
                : enumName;
        }
    }
}