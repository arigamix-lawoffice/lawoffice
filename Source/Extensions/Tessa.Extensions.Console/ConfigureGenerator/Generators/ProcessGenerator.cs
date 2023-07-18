using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared;
using Tessa.Platform.Json;
using Tessa.Platform.Storage;
using Tessa.Workflow.Actions;
using Tessa.Workflow.Helpful;
using Tessa.Workflow.Storage;

namespace Tessa.Extensions.Console.ConfigureGenerator.Generators
{
    public sealed class ProcessGenerator : IGenerator
    {
        public async Task<string> GenerateAsync(string path, CancellationToken cancellationToken = default)
        {
            var builder = new StringBuilder();
            builder.AppendLine("using System;");
            builder.AppendLine();
            builder.AppendLine("namespace Tessa.Extensions.Shared.Info");
            builder.AppendLine("{// ReSharper disable InconsistentNaming");
            
            var processNames = new List<string>();
            foreach (string file in Directory.GetFiles(path, "*.jcard", SearchOption.AllDirectories).OrderBy(Path.GetFileName))
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
                var caption = card.Sections["BusinessProcessInfo"].Fields.Get<string>("Name").Replace("\"", "\\\"", StringComparison.Ordinal);
                var group = card.Sections["BusinessProcessInfo"].Fields.Get<string>("Group");
                var alias = Translate.ToName(Transliteration.Front(caption.Replace("$", "", StringComparison.Ordinal).Replace("(", "", StringComparison.Ordinal).Replace(")", "", StringComparison.Ordinal).Replace("_", " ", StringComparison.Ordinal)).Replace("'s", "", StringComparison.Ordinal));
                processNames.Add(alias);

                builder.AppendLine($"    #region {alias}");
                builder.AppendLine();
                builder.AppendLine("    /// <summary>");
                builder.AppendLine($"    ///     ID: {card.ID:B}");
                builder.AppendLine($"    ///     Alias: {alias}");
                builder.AppendLine($"    ///     Caption: {caption}");
                builder.AppendLine($"    ///     Group: {group}");
                builder.AppendLine("    /// </summary>");
                builder.AppendLine($"    public class {alias}ProcessInfo");
                builder.AppendLine("    {");
                builder.AppendLine("        #region Common");
                builder.AppendLine();
                builder.AppendLine("        /// <summary>");
                builder.AppendLine($"        ///     Process identifier for \"{caption}\": {card.ID:B}.");
                builder.AppendLine("        /// </summary>");
                builder.AppendLine($"        public readonly Guid ID = new Guid({card.ID.ToString("X").Replace("{", "", StringComparison.Ordinal).Replace("}", "", StringComparison.Ordinal)});");
                builder.AppendLine();
                builder.AppendLine("        /// <summary>");
                builder.AppendLine($"        ///     Process caption for \"{caption}\".");
                builder.AppendLine("        /// </summary>");
                builder.AppendLine($"        public readonly string Caption = \"{caption}\";");
                builder.AppendLine();
                builder.AppendLine("        #endregion");
                
                var buttons = card.Sections["BusinessProcessButtons"].Rows;
                if (buttons.Count > 0)
                {
                    builder.AppendLine();
                    builder.AppendLine("        #region Buttons");
                    builder.AppendLine();
                    var buttonAliases = new List<string>();
                    foreach (var button in buttons)
                    {
                        var buttonId = button.RowID;
                        var buttonCaption = button.TryGet<string>("Caption");
                        var buttonAlias = Translate.ToName(Transliteration.Front(buttonCaption.Replace("$", "", StringComparison.Ordinal).Replace("(", "", StringComparison.Ordinal).Replace(")", "", StringComparison.Ordinal).Replace("_", " ", StringComparison.Ordinal)).Replace("'s", "", StringComparison.Ordinal));
                        if (buttonAliases.Contains(buttonAlias))
                        {
                            var cnt = 1;
                            do
                            {
                                cnt++;
                            }
                            while (buttonAliases.Contains(buttonAlias + cnt));

                            buttonAlias += cnt;
                        }

                        buttonAliases.Add(buttonAlias);
                        builder.AppendLine("        /// <summary>");
                        builder.AppendLine($"        ///     Button identifier for \"{buttonCaption}\": {buttonId:B}.");
                        builder.AppendLine("        /// </summary>");
                        builder.AppendLine($"        public readonly Guid {buttonAlias}ID = new Guid({buttonId.ToString("X").Replace("{", "", StringComparison.Ordinal).Replace("}", "", StringComparison.Ordinal)});");
                        builder.AppendLine();
                    }

                    builder.AppendLine("        #endregion");
                }
                var versions = card.Sections["BusinessProcessVersions"].Rows;
                if (versions.Count > 0)
                {
                    builder.AppendLine();
                    builder.AppendLine("        #region Signals");
                    builder.AppendLine();
                    var subscriptions = new List<string>();
                    foreach (var processRow in versions)
                    {
                        var processStorage = processRow.TryGet<Dictionary<string, object>>("ProcessData");
                        WorkflowProcessStorageCompressor.Default.Decompress(processStorage, null);
                        var processTemplate = new WorkflowProcessStorage(processStorage);
                        foreach (var subscription in processTemplate.Subscriptions)
                        {
                            if (subscriptions.Contains(subscription.EventName))
                            {
                                continue;
                            }
                            builder.AppendLine("        /// <summary>");
                            builder.AppendLine($"        ///     Signal identifier for \"{subscription.EventName}\".");
                            builder.AppendLine("        /// </summary>");
                            builder.AppendLine($"        public readonly string {subscription.EventName} = nameof({subscription.EventName});");
                            builder.AppendLine();
                            
                            subscriptions.Add(subscription.EventName);
                        }
                        foreach (var node in processTemplate.Nodes)
                        {
                            foreach (var action in node.Actions)
                            {
                                var command = WorkflowEngineHelper.Get<string>(action.Hash, WorkflowActionTypes.CommandSectionName, WorkflowActionTypes.CommandCommandName, "Name");
                                if (string.IsNullOrWhiteSpace(command) || subscriptions.Contains(command))
                                {
                                    continue;
                                }
                                builder.AppendLine("        /// <summary>");
                                builder.AppendLine($"        ///     Signal identifier for \"{command}\".");
                                builder.AppendLine("        /// </summary>");
                                builder.AppendLine($"        public readonly string {command} = nameof({command});");
                                builder.AppendLine();
                                subscriptions.Add(command);
                            }
                        }
                    }

                    builder.AppendLine("        #endregion");
                }

                builder.AppendLine("    }");
                builder.AppendLine();
                builder.AppendLine("    #endregion");
                builder.AppendLine();
            }

            builder.AppendLine("    public static class ProcessInfo");
            builder.AppendLine("    {");
            builder.AppendLine("        #region Processes");
            builder.AppendLine();
            foreach (string process in processNames)
            {
                builder.AppendLine($"        public static readonly {process}ProcessInfo {process} = new {process}ProcessInfo();");
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

            var processNames = new List<(string Alias, Guid ID, string Caption)>();

            foreach (var file in Directory.GetFiles(path, "*.jcard", SearchOption.AllDirectories).OrderBy(Path.GetFileName, StringComparer.InvariantCultureIgnoreCase))
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
                var caption = card.Sections["BusinessProcessInfo"].Fields.Get<string>("Name");
                var group = card.Sections["BusinessProcessInfo"].Fields.Get<string>("Group");
                var alias = Translate.AliasName(caption);
                processNames.Add((alias, card.ID, caption));

                builder.AppendLine($"//#region {alias}");
                builder.AppendLine();
                builder.AppendLine("/**");
                builder.AppendLine($" * ID: {card.ID:B}");
                builder.AppendLine($" * Alias: {alias}");
                builder.AppendLine($" * Caption: {caption}");
                builder.AppendLine($" * Group: {group}");
                builder.AppendLine(" */");
                builder.AppendLine($"class {alias}ProcessInfo {{");
                builder.AppendLine("  //#region Common");
                builder.AppendLine();
                builder.AppendLine("  /**");
                builder.AppendLine($"   * Process identifier for \"{caption}\": {card.ID:B}.");
                builder.AppendLine("   */");
                builder.AppendLine($"  readonly ID: guid = '{card.ID}';");
                builder.AppendLine();
                builder.AppendLine("  /**");
                builder.AppendLine($"   * Process caption for \"{caption}\".");
                builder.AppendLine("   */");
                builder.AppendLine($"  readonly Caption: string = '{caption.ToGenString()}';");
                builder.AppendLine();
                builder.AppendLine("  //#endregion");

                var buttonsSection = card.Sections.ContainsKey("BusinessProcessButtonsVirtual") ? "BusinessProcessButtonsVirtual" : "BusinessProcessButtons";
                var buttons = card.Sections[buttonsSection].Rows;
                if (buttons.Count > 0)
                {
                    builder.AppendLine();
                    builder.AppendLine("  //#region Buttons");
                    builder.AppendLine();
                    var buttonAliases = new List<string>();
                    foreach (var button in buttons)
                    {
                        var buttonId = button.RowID;
                        var buttonCaption = button.TryGet<string>("Caption");
                        var buttonAlias = Translate.UniqueAliasName(buttonAliases, Translate.AliasName(buttonCaption));
                        buttonAliases.Add(buttonAlias);
                        builder.AppendLine("  /**");
                        builder.AppendLine($"   * Button identifier for \"{buttonCaption}\": {buttonId:B}.");
                        builder.AppendLine("   */");
                        builder.AppendLine($"  readonly {buttonAlias}ButtonID: guid = '{buttonId}';");
                        builder.AppendLine();
                    }

                    builder.AppendLine("  //#endregion");
                }

                var versions = card.Sections["BusinessProcessVersions"].Rows;
                if (versions.Count > 0)
                {
                    var subscriptions = new List<string>();
                    var nodes = new Dictionary<Guid, (string Alias, string Caption)>();
                    var completionOptions = new List<(Guid, string, string)>();
                    foreach (var processRow in versions)
                    {
                        var processStorage = processRow.TryGet<Dictionary<string, object>>("ProcessData");
                        WorkflowProcessStorageCompressor.Default.Decompress(processStorage, null);
                        var processTemplate = new WorkflowProcessStorage(processStorage);
                        foreach (var subscription in processTemplate.Subscriptions)
                        {
                            if (subscriptions.Contains(subscription.EventName))
                            {
                                continue;
                            }

                            subscriptions.Add(subscription.EventName);
                        }

                        foreach (var node in processTemplate.Nodes)
                        {
                            if (!nodes.ContainsKey(node.ID))
                            {
                                var nodeAlias = Translate.UniqueAliasName(nodes.Select(x => x.Value.Alias).ToList(), Translate.AliasName(string.IsNullOrWhiteSpace(node.Caption) ? node.Name : node.Caption));
                                nodes.Add(node.ID, (nodeAlias, string.IsNullOrWhiteSpace(node.Caption) ? node.Name : node.Caption));
                            }

                            foreach (var action in node.Actions)
                            {
                                if (action.ActionTypeID == DefaultCardTypes.KrUniversalTaskActionTypeID)
                                {
                                    var options = WorkflowEngineHelper.Get<List<object>>(action.Hash, "KrUniversalTaskActionButtonsVirtual");
                                    if (options != null)
                                    {
                                        var nodeAlias = nodes[node.ID].Alias;
                                        foreach (var option in options.Cast<Dictionary<string, object>>())
                                        {
                                            var optionId = option.Get<Guid>("OptionID");
                                            var optionCaption = option.Get<string>("Caption");
                                            var optionAlias = Translate.UniqueAliasName(completionOptions.Select(x => x.Item2).ToList(), Translate.AliasName(nodeAlias + optionCaption));
                                            completionOptions.Add((optionId, optionAlias, optionCaption));
                                        }
                                    }
                                }

                                var command = WorkflowEngineHelper.Get<string>(action.Hash, WorkflowActionTypes.CommandSectionName, WorkflowActionTypes.CommandCommandName, "Name");
                                if (string.IsNullOrWhiteSpace(command) || subscriptions.Contains(command))
                                {
                                    continue;
                                }

                                subscriptions.Add(command);
                            }
                        }
                    }

                    if (completionOptions.Any())
                    {
                        builder.AppendLine();
                        builder.AppendLine("  //#region Completition Options");
                        builder.AppendLine();
                        foreach (var (optionId, optionAlias, optionCaption) in completionOptions)
                        {
                            builder.AppendLine("  /**");
                            builder.AppendLine($"   * Completition option identifier for \"{Translate.ToSingleLine(optionCaption)}\": {optionId:B}.");
                            builder.AppendLine("   */");
                            builder.AppendLine($"  readonly {optionAlias}OptionID: guid = '{optionId}';");
                            builder.AppendLine();
                        }

                        builder.AppendLine("  //#endregion");
                    }

                    if (nodes.Any())
                    {
                        builder.AppendLine();
                        builder.AppendLine("  //#region Nodes");
                        builder.AppendLine();
                        foreach (var (key, (nodeAlias, nodeCaption)) in nodes)
                        {
                            builder.AppendLine("  /**");
                            builder.AppendLine($"   * Node identifier for \"{Translate.ToSingleLine(nodeCaption)}\": {key:B}.");
                            builder.AppendLine("   */");
                            builder.AppendLine($"  readonly {nodeAlias}NodeID: guid = '{key}';");
                            builder.AppendLine();
                        }

                        builder.AppendLine("  //#endregion");
                    }

                    if (subscriptions.Any())
                    {
                        builder.AppendLine();
                        builder.AppendLine("  //#region Signals");
                        builder.AppendLine();

                        foreach (var subscription in subscriptions)
                        {
                            builder.AppendLine("  /**");
                            builder.AppendLine($"   * Signal identifier for \"{subscription}\".");
                            builder.AppendLine("   */");
                            builder.AppendLine($"  readonly {subscription}: string = '{subscription}';");
                            builder.AppendLine();
                        }

                        builder.AppendLine("  //#endregion");
                    }
                }

                builder.AppendLine("}");
                builder.AppendLine();
                builder.AppendLine("//#endregion");
                builder.AppendLine();
            }

            builder.AppendLine("export class ProcessInfo {");
            builder.AppendLine("  //#region Processes");
            builder.AppendLine();
            foreach (var (alias, id, caption) in processNames.OrderBy(x => x.Alias, StringComparer.InvariantCultureIgnoreCase))
            {
                builder.AppendLine("  /**");
                builder.AppendLine($"   * Process identifier for \"{caption}\": {id:B}.");
                builder.AppendLine("   */");
                builder.AppendLine($"  static get {alias}(): {alias}ProcessInfo {{");
                builder.AppendLine($"    return ProcessInfo.{alias.ToFirstLower()} = ProcessInfo.{alias.ToFirstLower()} ?? new {alias}ProcessInfo();");
                builder.AppendLine("  }");
                builder.AppendLine();
                builder.AppendLine($"  private static {alias.ToFirstLower()}: {alias}ProcessInfo;");
                builder.AppendLine();
            }

            builder.AppendLine("  //#endregion");
            builder.Append('}');

            return builder.ToString();
        }
    }
}