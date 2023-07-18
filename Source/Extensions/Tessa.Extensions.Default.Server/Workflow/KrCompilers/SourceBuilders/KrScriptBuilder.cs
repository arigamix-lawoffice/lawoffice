using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp;
using Tessa.Compilation;
using Tessa.Extensions.Default.Server.Workflow.KrProcess;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers.SourceBuilders
{
    public abstract class KrScriptBuilder<T>: KrSourceBuilder<T>
    {
        private const string DefaultCondition = "return true;";


        protected string BeforeMethodBody;
        protected string AfterMethodBody;
        protected string ConditionMethodBody;

        protected KrScriptBuilder(
            ICompilationSourceProvider compileSourceProvider,
            IKrPreprocessorProvider preprocessorProvider)
            : base(compileSourceProvider, preprocessorProvider)
        {
        }

        protected abstract string ConditionErrorPrefix { get; }
        protected abstract string BeforeErrorPrefix { get; }
        protected abstract string AfterErrorPrefix { get; }

        protected abstract string ClassPrefix { get; }

        /// <inheritdoc />
        protected override string FormatClassName() => KrCompilersHelper.FormatClassName(this.ClassPrefix, this.ClassAlias, this.ClassID);
        
        public override IList<ICompilationSource> BuildSources()
        {
            if (this.ClassID == default)
            {
                throw new InvalidOperationException("Class id doesn't set");
            }
            var sources = new List<ICompilationSource>();

            if (!string.IsNullOrWhiteSpace(this.BeforeMethodBody))
            {
                sources.Add(this.BuildBefore());
            }

            if (!string.IsNullOrWhiteSpace(this.AfterMethodBody))
            {
                sources.Add(this.BuildAfter());
            }

            if (!string.IsNullOrWhiteSpace(this.ConditionMethodBody)
                || sources.Count == 0)
            {
                sources.Add(this.BuildCondition());
            }
            
            sources.AddRange(this.BuildExtraSources());
            
            var defaultConstructor = this.BuildDefaultConstructor();
            if (defaultConstructor != null)
            {
                sources.Add(defaultConstructor);
            }
            return sources;
        }

        private ICompilationSource BuildCondition()
        {
            var builder = this.CompileSourceProvider.AcquireSyntaxTree();
            var methodBody = !string.IsNullOrWhiteSpace(this.ConditionMethodBody) ?
                this.PreprocessorProvider.AcquireFunctionPreprocessor().Preprocess(this.ConditionMethodBody) :
                DefaultCondition;

            var trace = KrErrorHelper.FormatErrorMessageTrace(this.ConditionErrorPrefix, this.Location);

            var sourceID = Guid.NewGuid();
            this.AnchorsMap?.Add(sourceID, new CompilationAnchor(SourceIdentifiers.ConditionMethod, SyntaxKind.MethodDeclaration));
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
                    SourceIdentifiers.ConditionMethod,
                    null,
                    methodBody,
                    isOverride: true,
                    isAsync: true);

            return builder.Build();
        }

        private ICompilationSource BuildBefore()
        {
            var builder = this.CompileSourceProvider.AcquireSyntaxTree();
            var methodBody = !string.IsNullOrWhiteSpace(this.BeforeMethodBody) ?
                this.PreprocessorProvider.AcquireProcedurePreprocessor().Preprocess(this.BeforeMethodBody) :
                string.Empty;
            var trace = KrErrorHelper.FormatErrorMessageTrace(this.BeforeErrorPrefix, this.Location);

            var sourceID = Guid.NewGuid();
            this.AnchorsMap?.Add(sourceID, new CompilationAnchor(SourceIdentifiers.BeforeMethod, SyntaxKind.MethodDeclaration));
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
                    "Task",
                    SourceIdentifiers.BeforeMethod,
                    null,
                    methodBody,
                    isOverride: true,
                    isAsync: true);

            return builder.Build();
        }

        private ICompilationSource BuildAfter()
        {
            var builder = this.CompileSourceProvider.AcquireSyntaxTree();
            var methodBody = !string.IsNullOrWhiteSpace(this.AfterMethodBody) ?
                this.PreprocessorProvider.AcquireProcedurePreprocessor().Preprocess(this.AfterMethodBody) :
                string.Empty;
            var trace = KrErrorHelper.FormatErrorMessageTrace(this.AfterErrorPrefix, this.Location);

            var sourceID = Guid.NewGuid();
            this.AnchorsMap?.Add(sourceID, new CompilationAnchor(SourceIdentifiers.AfterMethod, SyntaxKind.MethodDeclaration));
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
                    "Task",
                    SourceIdentifiers.AfterMethod,
                    null,
                    methodBody,
                    isOverride: true,
                    isAsync: true);

            return builder.Build();
        }
    }
}