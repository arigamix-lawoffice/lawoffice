using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Platform.IO;
using Tessa.Platform.Json;
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;
using Tessa.Views;
using Tessa.Views.Metadata.Criteria;
using Tessa.Views.Metadata.Types;
using Tessa.Views.Parser;
using Tessa.Views.Parser.SyntaxTree;
using Tessa.Views.Parser.SyntaxTree.ExchangeFormat;
using Tessa.Views.Parser.SyntaxTree.Expressions;
using Tessa.Views.Parser.SyntaxTree.Parameters;
using Tessa.Views.Parser.SyntaxTree.ViewMetadata;
using Tessa.Views.Parser.SyntaxTree.Workplace;
using Tessa.Views.Workplaces;
using Tessa.Views.Workplaces.Json;
using Unity;
using Unity.Lifetime;

namespace Tessa.Extensions.Console.ConfigureGenerator.Generators
{
    public sealed class WorkplaceGenerator : IGenerator
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
                .RegisterType<ITextBuilder, TextBuilder>(new PerResolveLifetimeManager());

            SyntaxNodeRegistration.Register(container);
            CriteriaOperatorRegistration.Register(container);
            ExchangeFormatSyntaxNodeRegistration.Register(container);
            ViewMetadataSyntaxNodeRegistration.Register(container);
            ParametersSyntaxNodeRegistration.Register(container);
            ExpressionsSyntaxNodeRegistration.Register(container);
            WorkplaceSyntaxNodeRegistration.Register(container);

            var interpreter = container.Resolve<IExchangeFormatInterpreter>();
            var indentationStrategy = new IndentationStrategy();
            builder.AppendLine("using System;");
            builder.AppendLine();
            builder.AppendLine("namespace Tessa.Extensions.Shared.Info");
            builder.AppendLine("{// ReSharper disable InconsistentNaming");
            var workplaces = new List<string>();
            foreach (var file in Directory.EnumerateFiles(path).Where(p => Path.GetExtension(p) == ".workplace" || Path.GetExtension(p) == ".jworkplace").OrderBy(Path.GetFileName))
            {
                var name = Translate.ToName(Transliteration.Front(Path.GetFileNameWithoutExtension(file)));
                await using var stream = new FileStream(file, FileMode.Open, FileAccess.Read);
                WorkplaceModel model;
                if (Path.GetExtension(file) == ".workplace")
                {
                    var result = await interpreter.InterpretAsync(stream, indentationStrategy, cancellationToken);
                    model = (WorkplaceModel) result.ResultItems.First();
                }
                else
                {
                    var context = new TessaJsonSerializationContext();
                    await using var scope = TessaJsonSerializationContext.Create(context);

                    var reader = new TextPartReader(stream);
                    var text = await reader.ReadAsync(cancellationToken).ConfigureAwait(false);

                    var jsonWorkplaceModel = text?.FromJsonString<JsonWorkplaceModel>();

                    var jsonWorkplace = jsonWorkplaceModel!.Content;
                    if (jsonWorkplace == null)
                    {
                        continue;
                    }

                    model = new WorkplaceModel
                    {
                        ID = jsonWorkplace.Metadata.CompositionId,
                        Name = jsonWorkplace.Metadata.Alias,
                        Metadata = jsonWorkplace.Metadata.ToJsonString(false),
                        Roles = jsonWorkplace.Roles
                    };
                }

                workplaces.Add(name);
                builder.AppendLine($"    #region {name}");
                builder.AppendLine();
                builder.AppendLine("    /// <summary>");
                builder.AppendLine($"    ///     ID: {model.ID:B}");
                builder.AppendLine($"    ///     Alias: {name}");
                builder.AppendLine($"    ///     Caption: {model.Name}");
                builder.AppendLine("    /// </summary>");
                builder.AppendLine($"    public class {name}WorkplaceInfo");
                builder.AppendLine("    {");
                builder.AppendLine("        #region Common");
                builder.AppendLine();
                builder.AppendLine("        /// <summary>");
                builder.AppendLine($"        ///     Workplace identifier for \"{model.Name}\": {model.ID:B}.");
                builder.AppendLine("        /// </summary>");
                builder.AppendLine($"        public readonly Guid ID = new Guid({model.ID.ToString("X").Replace("{", "", StringComparison.InvariantCulture).Replace("}", "", StringComparison.InvariantCulture)});");
                builder.AppendLine();
                builder.AppendLine("        /// <summary>");
                builder.AppendLine($"        ///     Workplace name for \"{model.Name}\".");
                builder.AppendLine("        /// </summary>");
                builder.AppendLine($"        public readonly string Alias = \"{name}\";");
                builder.AppendLine();
                builder.AppendLine("        /// <summary>");
                builder.AppendLine($"        ///     Workplace Caption for \"{model.Name}\".");
                builder.AppendLine("        /// </summary>");
                builder.AppendLine($"        public readonly string Caption = \"{model.Name}\";");
                builder.AppendLine();
                builder.AppendLine("        #endregion");
                builder.AppendLine();
                builder.AppendLine("        #region ToString");
                builder.AppendLine();
                builder.AppendLine($"        public static implicit operator string({name}WorkplaceInfo obj) => obj.ToString();");
                builder.AppendLine();
                builder.AppendLine("        public override string ToString() => this.Alias;");
                builder.AppendLine();
                builder.AppendLine("        #endregion");
                builder.AppendLine("    }");
                builder.AppendLine();
                builder.AppendLine("    #endregion");
                builder.AppendLine();
            }

            builder.AppendLine("    public static class WorkplaceInfo");
            builder.AppendLine("    {");
            builder.AppendLine("        #region Workplaces");
            builder.AppendLine();
            foreach (var workplace in workplaces)
            {
                builder.AppendLine($"        public static readonly {workplace}WorkplaceInfo {workplace} = new {workplace}WorkplaceInfo();");
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
            var container = new UnityContainer();
            container
                .RegisterType<ISchemeTypeConverter, NullSchemeTypeConverter>()
                .RegisterType<IViewService, NullViewService>()
                .RegisterType<ISession, NullSession>()
                .RegisterType<IIndentationStrategy, IndentationStrategy>(new PerResolveLifetimeManager())
                .RegisterType<ITextBuilder, TextBuilder>(new PerResolveLifetimeManager());

            SyntaxNodeRegistration.Register(container);
            CriteriaOperatorRegistration.Register(container);
            ExchangeFormatSyntaxNodeRegistration.Register(container);
            ViewMetadataSyntaxNodeRegistration.Register(container);
            ParametersSyntaxNodeRegistration.Register(container);
            ExpressionsSyntaxNodeRegistration.Register(container);
            WorkplaceSyntaxNodeRegistration.Register(container);

            var interpreter = container.Resolve<IExchangeFormatInterpreter>();
            var indentationStrategy = new IndentationStrategy();
            builder.AppendLine("// noinspection JSUnusedGlobalSymbols,SpellCheckingInspection");
            builder.AppendLine();
            var workplaces = new List<(string Alias, Guid ID, string Name)>();
            foreach (var file in Directory.EnumerateFiles(path).Where(p => Path.GetExtension(p) == ".workplace" || Path.GetExtension(p) == ".jworkplace").OrderBy(Path.GetFileName, StringComparer.InvariantCultureIgnoreCase))
            {
                await using var stream = new FileStream(file, FileMode.Open, FileAccess.Read);
                var model = await GetModelAsync(file, interpreter, stream, indentationStrategy, cancellationToken);
                if (model == null)
                {
                    continue;
                }

                var alias = Translate.AliasName(model.Name);
                workplaces.Add((alias, model.ID, model.Name));
                builder.AppendLine($"//#region {alias?.Trim()}");
                builder.AppendLine();
                builder.AppendLine("/**");
                builder.AppendLine($" * ID: {model.ID:B}");
                builder.AppendLine($" * Alias: {alias}");
                builder.AppendLine($" * Caption: {model.Name}");
                builder.AppendLine(" */");
                builder.AppendLine($"class {alias}WorkplaceInfo {{");
                builder.AppendLine("  //#region Common");
                builder.AppendLine();
                builder.AppendLine("  /**");
                builder.AppendLine($"   * Workplace identifier for \"{model.Name}\": {model.ID:B}.");
                builder.AppendLine("   */");
                builder.AppendLine($"   readonly ID: guid = '{model.ID}';");
                builder.AppendLine();
                builder.AppendLine("  /**");
                builder.AppendLine($"   * Workplace name for \"{model.Name}\".");
                builder.AppendLine("   */");
                builder.AppendLine($"   readonly Alias: string = '{alias}';");
                builder.AppendLine();
                builder.AppendLine("  /**");
                builder.AppendLine($"   * Workplace Caption for \"{model.Name}\".");
                builder.AppendLine("   */");
                builder.AppendLine($"   readonly Caption: string = '{model.Name.ToGenString()}';");
                builder.AppendLine();
                builder.AppendLine("  //#endregion");
                builder.AppendLine("}");
                builder.AppendLine();
                builder.AppendLine("//#endregion");
                builder.AppendLine();
            }

            builder.AppendLine("export class WorkplaceInfo {");
            builder.AppendLine("  //#region Workplaces");
            builder.AppendLine();
            foreach (var (alias, id, caption) in workplaces.OrderBy(x => x.Alias, StringComparer.InvariantCultureIgnoreCase))
            {
                builder.AppendLine("  /**");
                builder.AppendLine($"   * Workplace identifier for \"{caption}\": {id:B}.");
                builder.AppendLine("   */");
                builder.AppendLine($"  static get {alias}(): {alias}WorkplaceInfo {{");
                builder.AppendLine($"    return WorkplaceInfo.{alias.ToFirstLower()} = WorkplaceInfo.{alias.ToFirstLower()} ?? new {alias}WorkplaceInfo();");
                builder.AppendLine("  }");
                builder.AppendLine();
                builder.AppendLine($"  private static {alias.ToFirstLower()}: {alias}WorkplaceInfo;");
                builder.AppendLine();
            }

            builder.AppendLine("  //#endregion");
            builder.Append('}');

            return builder.ToString();
        }

        private static async Task<WorkplaceModel> GetModelAsync(
            string file,
            IExchangeFormatInterpreter interpreter,
            Stream stream,
            IIndentationStrategy indentationStrategy,
            CancellationToken cancellationToken = default)
        {
            if (string.Equals(Path.GetExtension(file), ".workplace", StringComparison.OrdinalIgnoreCase))
            {
                var result = await interpreter.InterpretAsync(stream, indentationStrategy, cancellationToken);
                return (WorkplaceModel)result.ResultItems.First();
            }

            var context = new TessaJsonSerializationContext();
            await using var scope = TessaJsonSerializationContext.Create(context);

            var reader = new TextPartReader(stream);
            var text = await reader.ReadAsync(cancellationToken).ConfigureAwait(false);

            var jsonWorkplaceModel = text?.FromJsonString<JsonWorkplaceModel>();

            var jsonWorkplace = jsonWorkplaceModel!.Content;
            if (jsonWorkplace == null)
            {
                return null;
            }

            return new WorkplaceModel
            {
                ID = jsonWorkplace.Metadata.CompositionId,
                Name = jsonWorkplace.Metadata.Alias,
                Metadata = jsonWorkplace.Metadata.ToJsonString(false),
                Roles = jsonWorkplace.Roles
            };
        }
    }
}