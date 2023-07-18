using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Tessa.Compilation;
using Tessa.Localization;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers.SourceBuilders
{
    public sealed class KrCommonMethodBuilder : KrSourceBuilder<IEnumerable<IKrCommonMethod>>
    {
        private struct SourceContext
        {
            public string MethodName { get; set; }
            public string MethodBody { get; set; }
        }

        private readonly List<SourceContext> sources = new List<SourceContext>();

        public KrCommonMethodBuilder(
            ICompilationSourceProvider compileSourceProvider,
            IKrPreprocessorProvider preprocessorProvider)
            : base(compileSourceProvider, preprocessorProvider)
        {
        }

        /// <inheritdoc />
        protected override string FormatClassName() => SourceIdentifiers.KrStageCommonClass;

        /// <inheritdoc />
        public override IKrSourceBuilder<IEnumerable<IKrCommonMethod>> SetSources(
            IEnumerable<IKrCommonMethod> source)
        {
            foreach (var method in source)
            {
                this.sources.Add(new SourceContext
                {
                    MethodName = method.Name,
                    MethodBody = method.Source,
                });
            }

            return this;
        }

        /// <inheritdoc />
        public override IList<ICompilationSource> BuildSources()
        {
            var src = this.sources.Select(this.BuildPartialClass).ToList();
            src.AddRange(this.BuildExtraSources());

            var defaultConstructor = this.BuildDefaultConstructor();
            if (defaultConstructor != null)
            {
                src.Add(defaultConstructor);
            }

            return src;
        }

        private ICompilationSource BuildPartialClass(SourceContext method)
        {
            var builder = this.CompileSourceProvider.AcquireSource();
            var methodBody = this.PreprocessorProvider.AcquireProcedurePreprocessor().Preprocess(method.MethodBody);
            var sourceID = Guid.NewGuid();
            this.AnchorsMap?.Add(sourceID, new CompilationAnchor(SourceIdentifiers.KrStageCommonClass, SyntaxKind.ClassDeclaration));
            builder
                .SetID(sourceID)
                .SetName(LocalizationManager.GetString("CardTypes_TypesNames_KrCommonMethod") + ": " + LocalizationManager.Localize(method.MethodName))
                .AppendLine(
                    "",
                    "namespace ",
                    SourceIdentifiers.Namespace,
                    "{",
                    "public abstract partial class ",
                    this.FormatClassName(),
                    " : global::",
                    SourceIdentifiers.BaseClass,
                    "{");

            using (var stream = new StringReader(methodBody))
            {
                string line;
                while ((line = stream.ReadLine()) != null)
                {
                    builder.AppendLine(line);
                }
            }
            builder
                .AppendLine(
                    "}",
                    "}");
            return builder.Build();
        }
    }
}
