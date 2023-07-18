using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp;
using Tessa.Compilation;
using Tessa.Extensions.Default.Server.Workflow.KrCompilers.UserAPI;
using Tessa.Extensions.Default.Server.Workflow.KrProcess;
using Tessa.Extensions.Default.Shared.Workflow.KrCompilers;
using Tessa.Platform;
using Tessa.Platform.Collections;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers.SourceBuilders
{
    public abstract class KrSourceBuilder<T> : IKrSourceBuilder<T>
    {
        // ReSharper disable once StaticMemberInGenericType
        private static readonly string[] voids = { SourceIdentifiers.Void, "void", "Void", "System.Void", "global::System.Void" };

        protected Guid ClassID { get; private set; }

        protected string ClassAlias { get; private set; } = string.Empty;

        protected string Location { get; private set; }

        protected Dictionary<Guid, CompilationAnchor> AnchorsMap { get; private set; }

        protected List<IExtraSource> ExtraSources { get; } = new List<IExtraSource>();

        protected List<string> DefaultConstructorParts { get; } = new List<string>();

        protected ICompilationSourceProvider CompileSourceProvider { get; }

        protected IKrPreprocessorProvider PreprocessorProvider { get; }

        protected KrSourceBuilder(
            ICompilationSourceProvider compileSourceProvider,
            IKrPreprocessorProvider preprocessorProvider)
        {
            this.CompileSourceProvider = compileSourceProvider;
            this.PreprocessorProvider = preprocessorProvider;
        }

        protected abstract string FormatClassName();

        /// <inheritdoc />
        public IKrSourceBuilder<T> SetClassID(
            Guid id)
        {
            this.ClassID = id;
            return this;
        }

        /// <inheritdoc />
        public IKrSourceBuilder<T> SetClassAlias(
            string classAlias)
        {
            this.ClassAlias = classAlias;
            return this;
        }

        /// <inheritdoc />
        public IKrSourceBuilder<T> SetLocation(
            string stageName = null,
            string stageTemplateName = null,
            string stageGroupName = null,
            string secondaryProcessName = null)
        {
            this.Location = KrErrorHelper.FormatErrorMessageTrace(
                stageName,
                stageTemplateName,
                stageGroupName,
                secondaryProcessName);
            return this;
        }

        /// <inheritdoc />
        public abstract IKrSourceBuilder<T> SetSources(
            T source);

        /// <inheritdoc />
        public IKrSourceBuilder<T> FillAnchorsMap(
            Dictionary<Guid, CompilationAnchor> anchorsMap)
        {
            this.AnchorsMap = anchorsMap;
            return this;
        }

        /// <inheritdoc />
        public IKrSourceBuilder<T> SetExtraSources(
            IExtraSources extraSources)
        {
            foreach (var source in extraSources.ExtraSources)
            {
                this.ExtraSources.Add(source);
            }

            return this;
        }

        /// <inheritdoc />
        public abstract IList<ICompilationSource> BuildSources();

        protected ICompilationSource BuildDefaultConstructor()
        {
            if (this.DefaultConstructorParts.Count == 0)
            {
                return null;
            }

            var builder = this.CompileSourceProvider.AcquireSyntaxTree();
            var sb = StringBuilderHelper.Acquire(256);
            foreach (var part in this.DefaultConstructorParts)
            {
                sb.AppendLine(part);
            }

            var methodBody = this.PreprocessorProvider
                .AcquireProcedurePreprocessor()
                .Preprocess(sb.ToStringAndRelease());

            var trace = KrErrorHelper.FormatErrorMessageTrace("$KrProcess_Constructor", this.Location);

            var sourceID = Guid.NewGuid();
            var className = this.FormatClassName();
            this.AnchorsMap?.Add(sourceID, new CompilationAnchor(className, SyntaxKind.ConstructorDeclaration));

            return builder
                .SetID(sourceID)
                .SetName(trace)
                .Namespace(SourceIdentifiers.Namespace)
                .Class(
                    className,
                    AccessModifier.Public,
                    new[] { SourceIdentifiers.KrStageCommonClass },
                    isPartial: true)
                .AddConstructor(methodBody)
                .Build();
        }

        protected IList<ICompilationSource> BuildExtraSources()
        {
            if (this.ExtraSources.Count == 0)
            {
                return EmptyHolder<ICompilationSource>.Collection;
            }

            var extraCompilationSources = new List<ICompilationSource>(this.ExtraSources.Count);
            foreach (var source in this.ExtraSources)
            {
                var builder = this.CompileSourceProvider.AcquireSyntaxTree();
                var methodBody = !string.IsNullOrWhiteSpace(source.Source)
                    ? this.PreprocessorProvider.AcquireProcedurePreprocessor().Preprocess(source.Source)
                    : string.Empty;
                var trace = KrErrorHelper.FormatErrorMessageTrace(source.DisplayName, this.Location);

                var sourceID = Guid.NewGuid();
                this.AnchorsMap?.Add(sourceID, new CompilationAnchor(source.Name, SyntaxKind.MethodDeclaration));
                var isReturnTypeVoid = voids.Contains(source.ReturnType, StringComparer.Ordinal);
                builder
                    .SetID(sourceID)
                    .SetName(trace)
                    .Namespace(SourceIdentifiers.Namespace)
                    .Class(
                        this.FormatClassName(),
                        AccessModifier.Public,
                        new[] { SourceIdentifiers.KrStageCommonClass },
                        isPartial: true)
                    .AddMethod(
                        isReturnTypeVoid
                        ? nameof(Task)
                        : "ValueTask<" + source.ReturnType + ">",
                        source.Name,
                        new[]
                        {
                            new Tuple<string, string>(source.ParameterType, source.ParameterName),
                        },
                        methodBody,
                        isAsync: true);

                extraCompilationSources.Add(builder.Build());

                var invocationBody = isReturnTypeVoid
                    ? $"{{ await this.{source.Name}(({source.ParameterType})ctx); return null; }};"
                    : $"await this.{source.Name}(({source.ParameterType})ctx);";
                this.DefaultConstructorParts.Add(
                    $"this.{KrScript.ExtraMethodsName}[\"{source.Name}\"] = async ctx => {invocationBody}");
            }

            return extraCompilationSources;
        }
    }
}