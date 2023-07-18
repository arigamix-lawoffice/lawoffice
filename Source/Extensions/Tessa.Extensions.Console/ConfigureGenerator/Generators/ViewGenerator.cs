using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Platform.Data;
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;
using Tessa.Views;
using Tessa.Views.Json;
using Tessa.Views.Json.Converters;
using Tessa.Views.Metadata.Criteria;
using Tessa.Views.Metadata.Types;
using Tessa.Views.Parser;
using Tessa.Views.Parser.SyntaxTree;
using Tessa.Views.Parser.SyntaxTree.ExchangeFormat;
using Tessa.Views.Parser.SyntaxTree.Expressions;
using Tessa.Views.Parser.SyntaxTree.Parameters;
using Tessa.Views.Parser.SyntaxTree.ViewMetadata;
using Tessa.Views.Parser.SyntaxTree.Workplace;
using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Console.ConfigureGenerator.Generators
{
    public sealed class ViewGenerator : IGenerator
    {
        public async Task<string> GenerateAsync(string path, CancellationToken cancellationToken = default)
        {
            var builder = new StringBuilder();
            var container = new UnityContainer();
            container
                .RegisterType<ISchemeTypeConverter, NullSchemeTypeConverter>()
                .RegisterType<IViewService, NullViewService>()
                .RegisterType<ISession, NullSession>()
                .RegisterType<IIndentationStrategy, IndentationStrategy>(new PerResolveLifetimeManager())
                .RegisterType<ITextBuilder, TextBuilder>(new PerResolveLifetimeManager())
                .RegisterType<IJsonViewModelImporter, JsonViewModelImporter>()
                .RegisterType<IJsonViewModelConverter, JsonViewModelConverter>();

            SyntaxNodeRegistration.Register(container);
            CriteriaOperatorRegistration.Register(container);
            ExchangeFormatSyntaxNodeRegistration.Register(container);
            ViewMetadataSyntaxNodeRegistration.Register(container);
            ParametersSyntaxNodeRegistration.Register(container);
            ExpressionsSyntaxNodeRegistration.Register(container);
            WorkplaceSyntaxNodeRegistration.Register(container);

            var interpreter = container.Resolve<IExchangeFormatInterpreter>();
            var metadataInterpreter = container.Resolve<IViewMetadataInterpreter>();
            var jsonViewModelImporter = container.Resolve<IJsonViewModelImporter>();
            var jsonViewModelAdapter = container.Resolve<IJsonViewModelConverter>();
            var evaluationContextFactory = container.Resolve<ViewMetadataEvaluationContextFactory>();
            var indentationStrategy = new IndentationStrategy();
            builder.AppendLine("using System;");
            builder.AppendLine();
            builder.AppendLine("namespace Tessa.Extensions.Shared.Info");
            builder.AppendLine("{// ReSharper disable InconsistentNaming");
            builder.AppendLine("    #region ViewObject");
            builder.AppendLine();
            builder.AppendLine("    public struct ViewObject");
            builder.AppendLine("    {");
            builder.AppendLine("        public ViewObject(int id, string alias, string caption = null)");
            builder.AppendLine("        {");
            builder.AppendLine("            this.Id = id;");
            builder.AppendLine("            this.Alias = alias;");
            builder.AppendLine("            this.Caption = caption ?? string.Empty;");
            builder.AppendLine("        }");
            builder.AppendLine();
            builder.AppendLine("        public int Id { get; }");
            builder.AppendLine("        public string Alias { get; }");
            builder.AppendLine("        public string Caption { get; }");
            builder.AppendLine();
            builder.AppendLine("        public static implicit operator string(ViewObject obj) => obj.ToString();");
            builder.AppendLine();
            builder.AppendLine("        public override string ToString() => this.Alias;");
            builder.AppendLine("    }");
            builder.AppendLine();
            builder.AppendLine("    #endregion");
            builder.AppendLine();

            var refSections = new List<string>();
            var viewNames = new List<string>();
            foreach (var file in Directory.EnumerateFiles(path, "*.*view", SearchOption.AllDirectories).Where(p => Path.GetExtension(p) == ".view" || Path.GetExtension(p) == ".jview").OrderBy(Path.GetFileName))
            {
                await using var stream = new FileStream(file, FileMode.Open, FileAccess.Read);
                TessaViewModel model;
                if (Path.GetExtension(file) == ".view")
                {
                    var result = await interpreter.InterpretAsync(stream, indentationStrategy, cancellationToken);
                    model = (TessaViewModel)result.ResultItems.First();
                }
                else
                {
                    var jsonViewModel = await jsonViewModelImporter.ImportAsync(stream, cancellationToken).ConfigureAwait(false);
                    model = jsonViewModelAdapter.ConvertToTessaViewModel(jsonViewModel);
                }
                viewNames.Add(model.Alias);
                var metadata = string.IsNullOrWhiteSpace(model.JsonMetadataSource)
                    ? await metadataInterpreter.EvaluateAsync(model.MetadataSource, evaluationContextFactory(Dbms.SqlServer, model.Alias, model.Caption), cancellationToken)
                    : model.JsonMetadataSource?.FromJsonString<JsonViewMetadata>();
                foreach (var reference in metadata.References)
                {
                    if (reference.RefSection == null)
                    {
                        continue;
                    }

                    foreach (var refSection in reference.RefSection)
                    {
                        if (!refSections.Contains(refSection))
                        {
                            refSections.Add(refSection);
                        }
                    }
                }

                var group = model.GroupName ?? "(Без группы)";

                builder.AppendLine($"    #region {model.Alias}");
                builder.AppendLine();
                builder.AppendLine("    /// <summary>");
                builder.AppendLine($"    ///     ID: {model.Id:B}");
                builder.AppendLine($"    ///     Alias: {model.Alias}");
                builder.AppendLine($"    ///     Caption: {model.Caption}");
                builder.AppendLine($"    ///     Group: {group}");
                builder.AppendLine("    /// </summary>");
                builder.AppendLine($"    public class {model.Alias}ViewInfo");
                builder.AppendLine("    {");
                builder.AppendLine("        #region Common");
                builder.AppendLine();
                builder.AppendLine("        /// <summary>");
                builder.AppendLine($"        ///     View identifier for \"{model.Alias}\": {model.Id:B}.");
                builder.AppendLine("        /// </summary>");
                builder.AppendLine($"        public readonly Guid ID = new Guid({model.Id.ToString("X").Replace("{", "", StringComparison.InvariantCultureIgnoreCase).Replace("}", "", StringComparison.InvariantCultureIgnoreCase)});");
                builder.AppendLine();
                builder.AppendLine("        /// <summary>");
                builder.AppendLine($"        ///     View name for \"{model.Alias}\".");
                builder.AppendLine("        /// </summary>");
                builder.AppendLine($"        public readonly string Alias = \"{model.Alias}\";");
                builder.AppendLine();
                builder.AppendLine("        /// <summary>");
                builder.AppendLine($"        ///     View caption for \"{model.Alias}\".");
                builder.AppendLine("        /// </summary>");
                builder.AppendLine($"        public readonly string Caption = \"{model.Caption?.Replace("\"", "\\\"", StringComparison.InvariantCultureIgnoreCase)}\";");
                builder.AppendLine();
                builder.AppendLine("        /// <summary>");
                builder.AppendLine($"        ///     View group for \"{model.Alias}\".");
                builder.AppendLine("        /// </summary>");
                builder.AppendLine($"        public readonly string Group = \"{group}\";");
                builder.AppendLine();
                builder.AppendLine("        #endregion");
                if (metadata.Columns.Count > 0)
                {
                    builder.AppendLine();
                    builder.AppendLine("        #region Columns");
                    builder.AppendLine();
                    var i = 0;
                    foreach (var column in metadata.Columns)
                    {
                        builder.AppendLine("        /// <summary>");
                        builder.AppendLine($"        ///     ID:{i} ");
                        builder.AppendLine($"        ///     Alias: {column.Alias}{(!string.IsNullOrWhiteSpace(column.Caption) ? "\r\n        ///     Caption: " + column.Caption.Replace("\"", "\\\"", StringComparison.InvariantCultureIgnoreCase): string.Empty)}.");
                        builder.AppendLine("        /// </summary>");
                        builder.AppendLine($"        public readonly ViewObject Column{column.Alias} = new ViewObject({i++}, \"{column.Alias}\"{(!string.IsNullOrWhiteSpace(column.Caption) ? ", \"" + column.Caption.Replace("\"", "\\\"", StringComparison.InvariantCultureIgnoreCase) + "\"": string.Empty)});");
                        builder.AppendLine();
                    }

                    builder.AppendLine("        #endregion");
                }

                if (metadata.Parameters.Count > 0)
                {
                    builder.AppendLine();
                    builder.AppendLine("        #region Parameters");
                    builder.AppendLine();
                    var i = 0;
                    foreach (var param in metadata.Parameters)
                    {
                        builder.AppendLine("        /// <summary>");
                        builder.AppendLine($"        ///     ID:{i} ");
                        builder.AppendLine($"        ///     Alias: {param.Alias}{(!string.IsNullOrWhiteSpace(param.Caption) ? "\r\n        ///     Caption: " + param.Caption.Replace("\"", "\\\"", StringComparison.InvariantCultureIgnoreCase) : string.Empty)}.");
                        builder.AppendLine("        /// </summary>");
                        builder.AppendLine($"        public readonly ViewObject Param{param.Alias} = new ViewObject({i++}, \"{param.Alias}\"{(!string.IsNullOrWhiteSpace(param.Caption) ? ", \"" + param.Caption.Replace("\"", "\\\"", StringComparison.InvariantCultureIgnoreCase) + "\"" : string.Empty)});");
                        builder.AppendLine();
                    }

                    builder.AppendLine("        #endregion");
                }

                builder.AppendLine();
                builder.AppendLine("        #region ToString");
                builder.AppendLine();
                builder.AppendLine($"        public static implicit operator string({model.Alias}ViewInfo obj) => obj.ToString();");
                builder.AppendLine();
                builder.AppendLine("        public override string ToString() => this.Alias;");
                builder.AppendLine();
                builder.AppendLine("        #endregion");
                builder.AppendLine("    }");
                builder.AppendLine();
                builder.AppendLine("    #endregion");
                builder.AppendLine();
            }

            builder.AppendLine("    #region RefSections");
            builder.AppendLine();
            builder.AppendLine("    public sealed class RefSectionsInfo");
            builder.AppendLine("    {");
            foreach (var refSection in refSections)
            {
                builder.AppendLine($"        public readonly string {refSection} = nameof({refSection});");
            }

            builder.AppendLine("    }");
            builder.AppendLine();
            builder.AppendLine("    #endregion");
            builder.AppendLine();
            builder.AppendLine("    public static class ViewInfo");
            builder.AppendLine("    {");
            builder.AppendLine("        #region Workplaces");
            builder.AppendLine();
            foreach (var view in viewNames)
            {
                builder.AppendLine($"        public static readonly {view}ViewInfo {view} = new {view}ViewInfo();");
            }

            builder.AppendLine();
            builder.AppendLine("        #endregion");
            builder.AppendLine();
            builder.AppendLine("        #region RefSections");
            builder.AppendLine();
            builder.AppendLine("        public static readonly RefSectionsInfo RefSections = new RefSectionsInfo();");
            builder.AppendLine();
            builder.AppendLine("        #endregion");
            builder.AppendLine("    }");
            builder.Append('}');
            
            return builder.ToString();
        }

        public async Task<string> GenerateWebAsync(string path, CancellationToken cancellationToken = default)
        {
            var builder = new StringBuilder();
            var container = new UnityContainer();
            container
                .RegisterType<ISchemeTypeConverter, NullSchemeTypeConverter>()
                .RegisterType<IViewService, NullViewService>()
                .RegisterType<ISession, NullSession>()
                .RegisterType<IIndentationStrategy, IndentationStrategy>(new PerResolveLifetimeManager())
                .RegisterType<ITextBuilder, TextBuilder>(new PerResolveLifetimeManager())
                .RegisterType<IJsonViewModelImporter, JsonViewModelImporter>()
                .RegisterType<IJsonViewModelConverter, JsonViewModelConverter>();

            SyntaxNodeRegistration.Register(container);
            CriteriaOperatorRegistration.Register(container);
            ExchangeFormatSyntaxNodeRegistration.Register(container);
            ViewMetadataSyntaxNodeRegistration.Register(container);
            ParametersSyntaxNodeRegistration.Register(container);
            ExpressionsSyntaxNodeRegistration.Register(container);
            WorkplaceSyntaxNodeRegistration.Register(container);

            var interpreter = container.Resolve<IExchangeFormatInterpreter>();
            var metadataInterpreter = container.Resolve<IViewMetadataInterpreter>();
            var jsonViewModelAdapter = container.Resolve<IJsonViewModelConverter>();
            var jsonViewModelImporter = container.Resolve<IJsonViewModelImporter>();
            var evaluationContextFactory = container.Resolve<ViewMetadataEvaluationContextFactory>();
            var indentationStrategy = new IndentationStrategy();
            builder.AppendLine("// noinspection JSUnusedGlobalSymbols,SpellCheckingInspection");
            builder.AppendLine();
            builder.AppendLine("//#region ViewObject");
            builder.AppendLine();
            builder.AppendLine("class ViewObject {");
            builder.AppendLine("  constructor (id: number, alias: string, caption: string | null = null) {");
            builder.AppendLine("    this.id = id;");
            builder.AppendLine("    this.alias = alias;");
            builder.AppendLine("    this.caption = caption ?? null;");
            builder.AppendLine("  }");
            builder.AppendLine();
            builder.AppendLine("  private id: number;");
            builder.AppendLine("  private alias: string;");
            builder.AppendLine("  private caption: string | null;");
            builder.AppendLine();
            builder.AppendLine("  public get Id(): number { return this.id; }");
            builder.AppendLine("  public get Alias(): string { return this.alias; }");
            builder.AppendLine("  public get Caption(): string | null { return this.caption; }");
            builder.AppendLine("}");
            builder.AppendLine();
            builder.AppendLine("//#endregion");
            builder.AppendLine();

            var refSections = new Dictionary<string, List<string>>();
            var viewNames = new List<(string, Guid, string)>();
            foreach (var file in Directory.EnumerateFiles(path, "*.jview", SearchOption.AllDirectories).Where(p => Path.GetExtension(p) == ".view" || Path.GetExtension(p) == ".jview").OrderBy(Path.GetFileName, StringComparer.InvariantCultureIgnoreCase))
            {
                await using var stream = new FileStream(file, FileMode.Open, FileAccess.Read);
                var model = await GetModelAsync(file, interpreter, stream, indentationStrategy, jsonViewModelAdapter, jsonViewModelImporter, cancellationToken);
                if (model is null)
                {
                    continue;
                }

                viewNames.Add((model.Alias, model.Id, model.Caption?.ToGenString()));
                var metadata = string.IsNullOrWhiteSpace(model.JsonMetadataSource)
                    ? await metadataInterpreter.EvaluateAsync(model.MetadataSource, evaluationContextFactory(Dbms.SqlServer, model.Alias, model.Caption), cancellationToken)
                    : model.JsonMetadataSource?.FromJsonString<JsonViewMetadata>();
                foreach (var reference in metadata.References)
                {
                    if (reference.RefSection == null)
                    {
                        continue;
                    }

                    foreach (var refSection in reference.RefSection)
                    {
                        if (!refSections.ContainsKey(refSection))
                        {
                            refSections.Add(refSection, new List<string>());
                        }

                        refSections[refSection].Add(model.Alias);
                    }
                }

                var group = model.GroupName?.ToGenString() ?? "(Без группы)";

                builder.AppendLine($"//#region {model.Alias}");
                builder.AppendLine();
                builder.AppendLine("/**");
                builder.AppendLine($" * ID: {model.Id:B}");
                builder.AppendLine($" * Alias: {model.Alias}");
                builder.AppendLine($" * Caption: {model.Caption?.ToGenString()}");
                builder.AppendLine($" * Group: {group}");
                builder.AppendLine(" */");
                builder.AppendLine($"class {model.Alias}ViewInfo {{");
                builder.AppendLine("  //#region Common");
                builder.AppendLine();
                builder.AppendLine("  /**");
                builder.AppendLine($"   * View identifier for \"{model.Alias}\": {model.Id:B}.");
                builder.AppendLine("   */");
                builder.AppendLine($"   readonly ID: guid = '{model.Id}';");
                builder.AppendLine();
                builder.AppendLine("  /**");
                builder.AppendLine($"   * View name for \"{model.Alias}\".");
                builder.AppendLine("   */");
                builder.AppendLine($"   readonly Alias: string = '{model.Alias}';");
                builder.AppendLine();
                builder.AppendLine("  /**");
                builder.AppendLine($"   * View caption for \"{model.Alias}\".");
                builder.AppendLine("   */");
                builder.AppendLine($"   readonly Caption: string = '{model.Caption?.ToGenString()}';");
                builder.AppendLine();
                builder.AppendLine("  /**");
                builder.AppendLine($"   * View group for \"{model.Alias}\".");
                builder.AppendLine("   */");
                builder.AppendLine($"   readonly Group: string = '{group}';");
                builder.AppendLine();
                builder.AppendLine("  //#endregion");
                if (metadata.Columns.Count > 0)
                {
                    builder.AppendLine();
                    builder.AppendLine("  //#region Columns");
                    builder.AppendLine();
                    var i = 0;
                    foreach (var column in metadata.Columns)
                    {
                        builder.AppendLine("  /**");
                        builder.AppendLine($"   * ID:{i}");
                        builder.AppendLine($"   * Alias: {column.Alias}{(!string.IsNullOrWhiteSpace(column.Caption) ? "\r\n   * Caption: " + column.Caption.ToGenString() : string.Empty)}.");
                        builder.AppendLine("   */");
                        builder.AppendLine($"   readonly Column{column.Alias}: ViewObject = new ViewObject({i++}, '{column.Alias}'{(!string.IsNullOrWhiteSpace(column.Caption) ? ", '" + column.Caption.ToGenString() + "'" : string.Empty)});");
                        builder.AppendLine();
                    }

                    builder.AppendLine("  //#endregion");
                }

                if (metadata.Parameters.Count > 0)
                {
                    builder.AppendLine();
                    builder.AppendLine("  //#region Parameters");
                    builder.AppendLine();
                    var i = 0;
                    foreach (var param in metadata.Parameters)
                    {
                        builder.AppendLine("  /**");
                        builder.AppendLine($"   * ID:{i}");
                        builder.AppendLine($"   * Alias: {param.Alias}{(!string.IsNullOrWhiteSpace(param.Caption) ? "\r\n   * Caption: " + param.Caption.ToGenString() : string.Empty)}.");
                        builder.AppendLine("   */");
                        builder.AppendLine($"   readonly Param{param.Alias}: ViewObject = new ViewObject({i++}, '{param.Alias}'{(!string.IsNullOrWhiteSpace(param.Caption) ? ", '" + param.Caption.ToGenString() + "'" : string.Empty)});");
                        builder.AppendLine();
                    }

                    builder.AppendLine("  //#endregion");
                }
                builder.AppendLine("}");
                builder.AppendLine();
                builder.AppendLine("//#endregion");
                builder.AppendLine();
            }

            builder.AppendLine("//#region RefSections");
            builder.AppendLine();
            builder.AppendLine("class RefSectionsInfo {");
            builder.AppendLine("  //#region RefSections");
            foreach (var (refSection, views) in refSections.OrderBy(x => x.Key, StringComparer.InvariantCultureIgnoreCase))
            {
                builder.AppendLine();
                builder.AppendLine("  /**");
                builder.AppendLine("   * Views:");
                foreach (var view in views)
                {
                    builder.AppendLine($"   * {view}");
                }

                builder.AppendLine("   */");
                builder.AppendLine($"   readonly {refSection}: string = '{refSection}';");
            }

            builder.AppendLine();
            builder.AppendLine("  //#endregion");

            builder.AppendLine("}");
            builder.AppendLine();
            builder.AppendLine("//#endregion");
            builder.AppendLine();
            builder.AppendLine("export class ViewInfo {");
            builder.AppendLine("  //#region Views");
            builder.AppendLine();
            foreach (var (alias, id, caption) in viewNames.OrderBy(x => x.Item1, StringComparer.InvariantCultureIgnoreCase))
            {
                builder.AppendLine("  /**");
                builder.AppendLine($"   * View identifier for \"{caption}\": {id:B}.");
                builder.AppendLine("   */");
                builder.AppendLine($"  static get {alias}(): {alias}ViewInfo {{");
                builder.AppendLine($"    return ViewInfo.{alias.ToFirstLower()} = ViewInfo.{alias.ToFirstLower()} ?? new {alias}ViewInfo();");
                builder.AppendLine("  }");
                builder.AppendLine();
                builder.AppendLine($"  private static {alias.ToFirstLower()}: {alias}ViewInfo;");
                builder.AppendLine();
            }

            builder.AppendLine("  //#endregion");
            builder.AppendLine();
            builder.AppendLine("  //#region RefSections");
            builder.AppendLine();
            builder.AppendLine("  static get RefSections(): RefSectionsInfo {");
            builder.AppendLine("    return ViewInfo.refSections = ViewInfo.refSections ?? new RefSectionsInfo();");
            builder.AppendLine("  }");
            builder.AppendLine();
            builder.AppendLine("  private static refSections: RefSectionsInfo;");
            builder.AppendLine();
            builder.AppendLine("  //#endregion");
            builder.Append('}');

            return builder.ToString();
        }

        private static async Task<TessaViewModel> GetModelAsync(
            string file,
            IExchangeFormatInterpreter interpreter,
            Stream stream,
            IIndentationStrategy indentationStrategy,
            IJsonViewModelConverter jsonViewModelAdapter,
            IJsonViewModelImporter jsonViewModelImporter,
            CancellationToken cancellationToken = default)
        {
            if (string.Equals(Path.GetExtension(file), ".view", StringComparison.OrdinalIgnoreCase))
            {
                var result = await interpreter.InterpretAsync(stream, indentationStrategy, cancellationToken);
                return (TessaViewModel)result.ResultItems.First();
            }

            var jsonViewModel = await jsonViewModelImporter.ImportAsync(stream, cancellationToken).ConfigureAwait(false);
            return jsonViewModelAdapter.ConvertToTessaViewModel(jsonViewModel);
        }
    }
}