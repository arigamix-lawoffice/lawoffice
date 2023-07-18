using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp;
using Tessa.Compilation;
using Tessa.Extensions.Default.Server.Workflow.KrProcess;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers.SourceBuilders
{
    public abstract class KrSingleScriptBuilder<T> : KrSourceBuilder<T>
    {
        private const string DefaultCondition = "return true;";

        protected string SourceText;

        protected KrSingleScriptBuilder(
            ICompilationSourceProvider compileSourceProvider,
            IKrPreprocessorProvider preprocessorProvider)
            : base(compileSourceProvider, preprocessorProvider)
        {
        }

        protected abstract string ErrorPrefix { get; }

        protected abstract string ClassPrefix { get; }

        protected abstract string MethodName { get; }

        /// <inheritdoc />
        protected override string FormatClassName() => KrCompilersHelper.FormatClassName(this.ClassPrefix, this.ClassAlias, this.ClassID);

        /// <inheritdoc />
        public override IList<ICompilationSource> BuildSources()
        {
            var sources = new List<ICompilationSource> { this.Build() };
            sources.AddRange(this.BuildExtraSources());
            var defaultConstructor = this.BuildDefaultConstructor();
            if (defaultConstructor != null)
            {
                sources.Add(defaultConstructor);
            }
            return sources;
        }

        private ICompilationSource Build()
        {
            var builder = this.CompileSourceProvider.AcquireSyntaxTree();
            var methodBody = !string.IsNullOrWhiteSpace(this.SourceText) ?
                this.PreprocessorProvider.AcquireFunctionPreprocessor().Preprocess(this.SourceText) :
                DefaultCondition;

            var trace = KrErrorHelper.FormatErrorMessageTrace(this.ErrorPrefix,this.Location);

            var sourceID = Guid.NewGuid();
            this.AnchorsMap?.Add(sourceID, new CompilationAnchor(this.MethodName, SyntaxKind.MethodDeclaration));
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
                    "ValueTask<bool>",
                    this.MethodName,
                    null,
                    methodBody,
                    isOverride: true,
                    isAsync: true);

            return builder.Build();
        }

    }
}