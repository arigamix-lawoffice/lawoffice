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
    public sealed class RoleGenerator : IGenerator
    {
        public async Task<string> GenerateAsync(string path, CancellationToken cancellationToken = default)
        {
            var builder = new StringBuilder();
            builder.AppendLine("using System;");
            builder.AppendLine();
            builder.AppendLine("namespace Tessa.Extensions.Shared.Info");
            builder.AppendLine("{// ReSharper disable InconsistentNaming");
            var paths = path.Split(',').Select(x => x.Trim()).ToArray();
            var rolesPaths = new List<string>(100);
            foreach (var rolePath in paths)
            {
                rolesPaths.AddRange(Directory.GetFiles(rolePath, "*.jcard", SearchOption.AllDirectories).OrderBy(Path.GetFileName));
            }
            var roles = new List<string>();
            foreach (string rolePath in rolesPaths)
            {
                using var stream = new StringReader(await File.ReadAllTextAsync(rolePath, cancellationToken));
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
                var caption = card.TypeName == "RoleGenerator"
                    ? card.Sections["RoleGenerators"].Fields.Get<string>("Name")
                    : card.Sections["Roles"].Fields.Get<string>("Name");
                var alias = Translate.ToName(Transliteration.Front(caption.Replace("$", "", StringComparison.Ordinal).Replace("(", "", StringComparison.Ordinal).Replace(")", "", StringComparison.Ordinal).Replace("_", " ", StringComparison.Ordinal)).Replace("'s", "", StringComparison.Ordinal));
                roles.Add(alias);
                builder.AppendLine();
                builder.AppendLine($"    #region {alias}");
                builder.AppendLine();
                builder.AppendLine("    /// <summary>");
                builder.AppendLine($"    ///     ID: {card.ID:B}");
                builder.AppendLine($"    ///     Alias: {alias}");
                builder.AppendLine($"    ///     Caption: {caption}");
                builder.AppendLine("    /// </summary>");
                builder.AppendLine($"    public sealed class {alias}RoleInfo");
                builder.AppendLine("    {");
                builder.AppendLine("        /// <summary>");
                builder.AppendLine($"        ///     Role identifier for \"{caption}\": {card.ID:B}.");
                builder.AppendLine("        /// </summary>");
                builder.AppendLine($"        public readonly Guid ID = new Guid({card.ID.ToString("X").Replace("{", "", StringComparison.Ordinal).Replace("}", "", StringComparison.Ordinal)});");
                builder.AppendLine();
                builder.AppendLine("        /// <summary>");
                builder.AppendLine($"        ///     Role alias for \"{caption}\".");
                builder.AppendLine("        /// </summary>");
                builder.AppendLine($"        public readonly string Alias = \"{alias}\";");
                builder.AppendLine();
                builder.AppendLine("        /// <summary>");
                builder.AppendLine($"        ///     Role caption for \"{caption}\".");
                builder.AppendLine("        /// </summary>");
                builder.AppendLine($"        public readonly string Caption = \"{caption}\";");
                builder.AppendLine("    }");
                builder.AppendLine();
                builder.AppendLine("    #endregion");
            }

            if (!roles.Contains("Admin") || !roles.Contains("System"))
            {
                builder.AppendLine("    #region System Employees");
                if (!roles.Contains("Admin"))
                {
                    roles.Add("Admin");
                    builder.AppendLine();
                    builder.AppendLine("    #region Admin");
                    builder.AppendLine();
                    builder.AppendLine("    /// <summary>");
                    builder.AppendLine("    ///     ID: {3db19fa0-228a-497f-873a-0250bf0a4ccb}");
                    builder.AppendLine("    ///     Alias: Admin");
                    builder.AppendLine("    ///     Caption: Admin");
                    builder.AppendLine("    /// </summary>");
                    builder.AppendLine("    public sealed class AdminRoleInfo");
                    builder.AppendLine("    {");
                    builder.AppendLine("        /// <summary>");
                    builder.AppendLine("        ///     Role identifier for \"Admin\": {3db19fa0-228a-497f-873a-0250bf0a4ccb}.");
                    builder.AppendLine("        /// </summary>");
                    builder.AppendLine("        public readonly Guid ID = new Guid(0x3db19fa0,0x228a,0x497f,0x87,0x3a,0x02,0x50,0xbf,0x0a,0x4c,0xcb);");
                    builder.AppendLine();
                    builder.AppendLine("        /// <summary>");
                    builder.AppendLine("        ///     Role alias for \"Admin\".");
                    builder.AppendLine("        /// </summary>");
                    builder.AppendLine("        public readonly string Alias = \"Admin\";");
                    builder.AppendLine();
                    builder.AppendLine("        /// <summary>");
                    builder.AppendLine("        ///     Role caption for \"Admin\".");
                    builder.AppendLine("        /// </summary>");
                    builder.AppendLine("        public readonly string Caption = \"Admin\";");
                    builder.AppendLine("    }");
                    builder.AppendLine();
                    builder.AppendLine("    #endregion");
                }

                if (!roles.Contains("System"))
                {
                    roles.Add("System");
                    builder.AppendLine();
                    builder.AppendLine("    #region System");
                    builder.AppendLine();
                    builder.AppendLine("    /// <summary>");
                    builder.AppendLine("    ///     ID: {11111111-1111-1111-1111-111111111111}");
                    builder.AppendLine("    ///     Alias: System");
                    builder.AppendLine("    ///     Caption: System");
                    builder.AppendLine("    /// </summary>");
                    builder.AppendLine("    public sealed class SystemRoleInfo");
                    builder.AppendLine("    {");
                    builder.AppendLine("        /// <summary>");
                    builder.AppendLine("        ///     Role identifier for \"System\": {11111111-1111-1111-1111-111111111111}.");
                    builder.AppendLine("        /// </summary>");
                    builder.AppendLine("        public readonly Guid ID = new Guid(0x11111111,0x1111,0x1111,0x11,0x11,0x11,0x11,0x11,0x11,0x11,0x11);");
                    builder.AppendLine();
                    builder.AppendLine("        /// <summary>");
                    builder.AppendLine("        ///     Role alias for \"System\".");
                    builder.AppendLine("        /// </summary>");
                    builder.AppendLine("        public readonly string Alias = \"System\";");
                    builder.AppendLine();
                    builder.AppendLine("        /// <summary>");
                    builder.AppendLine("        ///     Role caption for \"System\".");
                    builder.AppendLine("        /// </summary>");
                    builder.AppendLine("        public readonly string Caption = \"System\";");
                    builder.AppendLine("    }");
                    builder.AppendLine();
                    builder.AppendLine("    #endregion");
                }
                builder.AppendLine();
                builder.AppendLine("    #endregion");
            }
            builder.AppendLine();
            builder.AppendLine("    public static class RoleInfo");
            builder.AppendLine("    {");
            builder.AppendLine("        #region Roles");
            builder.AppendLine();
            foreach (var role in roles)
            {
                builder.AppendLine($"        public static readonly {role}RoleInfo {role} = new {role}RoleInfo();");
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

            var rolesPaths = Directory.GetDirectories(path, "*", SearchOption.TopDirectoryOnly).OrderBy(x => x, StringComparer.InvariantCultureIgnoreCase).ToList();
            var roles = new Dictionary<string, List<(Guid ID, string TypeInfo, string Alias, string Caption)>>();
            foreach (var rolePath in rolesPaths)
            {
                var roleType = Path.GetFileName(rolePath);
                var files = Directory.GetFiles(rolePath, "*.jcard", SearchOption.AllDirectories).OrderBy(Path.GetFileName, StringComparer.InvariantCultureIgnoreCase).ToList();
                if (files.Count == 0)
                {
                    continue;
                }

                builder.AppendLine($"//#region {roleType}");
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

                    var typeInfo = card.TypeName switch
                    {
                        "PersonalRole" => "PersonalInfo", // Сотрудник
                        "StaticRole" => "StaticInfo", // Статическая роль
                        "ContextRole" => "ContextInfo", // Контекстная роль
                        "DynamicRole" => "DynamicInfo", // Динамическая роль
                        "DepartmentRole" => "DepartmentInfo", // Подразделения
                        "RoleGenerator" => "GeneratorInfo", // Мета роль
                        var _ => string.Empty
                    };
                    var shortType = typeInfo.Replace("Info", string.Empty, StringComparison.Ordinal);
                    var caption = card.TypeName == "RoleGenerator"
                        ? card.Sections["RoleGenerators"].Fields.Get<string>("Name")
                        : card.Sections["Roles"].Fields.Get<string>("Name");
                    var alias = Translate.AliasName(caption);
                    if (!roles.ContainsKey(shortType))
                    {
                        roles.Add(shortType, new List<(Guid, string, string, string)>());
                    }

                    roles[shortType].Add((card.ID, typeInfo, alias, caption));
                    builder.AppendLine();
                    builder.AppendLine($"//#region {alias}");
                    builder.AppendLine();
                    builder.AppendLine("/**");
                    builder.AppendLine($" * ID: {card.ID:B}");
                    builder.AppendLine($" * Alias: {alias}");
                    builder.AppendLine($" * Caption: {caption.Trim()}");
                    builder.AppendLine(" */");
                    builder.AppendLine($"class {alias}{typeInfo} {{");
                    builder.AppendLine("  /**");
                    builder.AppendLine($"   * Role identifier for \"{caption}\": {card.ID:B}.");
                    builder.AppendLine("   */");
                    builder.AppendLine($"  readonly ID: guid = '{card.ID}';");
                    builder.AppendLine();
                    builder.AppendLine("  /**");
                    builder.AppendLine($"   * Role alias for \"{caption}\".");
                    builder.AppendLine("   */");
                    builder.AppendLine($"  readonly Alias: string = '{alias}';");
                    builder.AppendLine();
                    builder.AppendLine("  /**");
                    builder.AppendLine($"   * Role caption for \"{caption}\".");
                    builder.AppendLine("   */");
                    builder.AppendLine($"  readonly Caption: string = '{caption.ToGenString()}';");
                    builder.AppendLine("}");
                    builder.AppendLine();
                    builder.AppendLine("//#endregion");
                }

                builder.AppendLine();
                builder.AppendLine("//#endregion");
                builder.AppendLine();
            }

            if (!roles.ContainsKey("Personal"))
            {
                roles.Add("Personal", new List<(Guid, string, string, string)>());
            }

            if (roles["Personal"].All(x => x.Item2 != "Admin") || roles["Personal"].All(x => x.Item2 != "System"))
            {
                builder.AppendLine("//#region System Employees");
                builder.AppendLine();
                if (roles["Personal"].All(x => x.Item2 != "Admin"))
                {
                    roles["Personal"].Add((new Guid(0x3db19fa0, 0x228a, 0x497f, 0x87, 0x3a, 0x02, 0x50, 0xbf, 0x0a, 0x4c, 0xcb), "PersonalInfo", "Admin", "Admin"));
                    builder.AppendLine("//#region Admin");
                    builder.AppendLine();
                    builder.AppendLine("/**");
                    builder.AppendLine(" * ID: {3db19fa0-228a-497f-873a-0250bf0a4ccb}");
                    builder.AppendLine(" * Alias: Admin");
                    builder.AppendLine(" * Caption: Admin");
                    builder.AppendLine(" */");
                    builder.AppendLine("class AdminPersonalInfo {");
                    builder.AppendLine("  /**");
                    builder.AppendLine("   * Role identifier for \"Admin\": {3db19fa0-228a-497f-873a-0250bf0a4ccb}.");
                    builder.AppendLine("   */");
                    builder.AppendLine("  readonly ID: guid = '3db19fa0-228a-497f-873a-0250bf0a4ccb';");
                    builder.AppendLine();
                    builder.AppendLine("  /**");
                    builder.AppendLine("   * Role alias for \"Admin\".");
                    builder.AppendLine("   */");
                    builder.AppendLine("  readonly Alias: string = 'Admin';");
                    builder.AppendLine();
                    builder.AppendLine("  /**");
                    builder.AppendLine("   * Role caption for \"Admin\".");
                    builder.AppendLine("   */");
                    builder.AppendLine("  readonly Caption: string = 'Admin';");
                    builder.AppendLine("}");
                    builder.AppendLine();
                    builder.AppendLine("//#endregion");
                    builder.AppendLine();
                }

                if (roles["Personal"].All(x => x.Item2 != "System"))
                {
                    roles["Personal"].Add((new Guid(0x11111111, 0x1111, 0x1111, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11), "PersonalInfo", "System", "System"));
                    builder.AppendLine("//#region System");
                    builder.AppendLine();
                    builder.AppendLine("/**");
                    builder.AppendLine(" * ID: {11111111-1111-1111-1111-111111111111}");
                    builder.AppendLine(" * Alias: System");
                    builder.AppendLine(" * Caption: System");
                    builder.AppendLine(" */");
                    builder.AppendLine("class SystemPersonalInfo {");
                    builder.AppendLine("  /**");
                    builder.AppendLine("   * Role identifier for \"System\": {11111111-1111-1111-1111-111111111111}.");
                    builder.AppendLine("   */");
                    builder.AppendLine("  readonly ID: guid = '11111111-1111-1111-1111-111111111111';");
                    builder.AppendLine();
                    builder.AppendLine("  /**");
                    builder.AppendLine("   * Role alias for \"System\".");
                    builder.AppendLine("   */");
                    builder.AppendLine("  readonly Alias: string = 'System';");
                    builder.AppendLine();
                    builder.AppendLine("  /**");
                    builder.AppendLine("   * Role caption for \"System\".");
                    builder.AppendLine("   */");
                    builder.AppendLine("  readonly Caption: string = 'System';");
                    builder.AppendLine("}");
                    builder.AppendLine();
                    builder.AppendLine("//#endregion");
                    builder.AppendLine();
                }

                builder.AppendLine("//#endregion");
                builder.AppendLine();
            }

            foreach (var (type, value) in roles.OrderBy(x => x.Key, StringComparer.InvariantCultureIgnoreCase))
            {
                builder.AppendLine($"class {type}RoleInfo {{");
                builder.AppendLine($"  //#region {type}");
                builder.AppendLine();
                foreach (var (id, typeInfo, alias, caption) in value)
                {
                    builder.AppendLine("  /**");
                    builder.AppendLine($"   * ID: {id:B}");
                    builder.AppendLine($"   * Alias: {alias}");
                    builder.AppendLine($"   * Caption: {caption.Trim()}");
                    builder.AppendLine("   */");
                    builder.AppendLine($"  readonly {alias} = new {alias}{typeInfo}();");
                    builder.AppendLine();
                }

                builder.AppendLine("  //#endregion");
                builder.AppendLine("}");
                builder.AppendLine();
            }

            builder.AppendLine("export class RoleInfo {");
            builder.AppendLine("  //#region Roles");
            builder.AppendLine();
            foreach (var alias in roles.Keys.OrderBy(x => x, StringComparer.InvariantCultureIgnoreCase))
            {
                builder.AppendLine($"  static get {alias}(): {alias}RoleInfo {{");
                builder.AppendLine($"    return RoleInfo.{alias.ToFirstLower()} = RoleInfo.{alias.ToFirstLower()} ?? new {alias}RoleInfo();");
                builder.AppendLine("  }");
                builder.AppendLine();
                builder.AppendLine($"  private static {alias.ToFirstLower()}: {alias}RoleInfo;");
                builder.AppendLine();
            }

            builder.AppendLine("  //#endregion");
            builder.Append('}');

            return builder.ToString();
        }
    }
}