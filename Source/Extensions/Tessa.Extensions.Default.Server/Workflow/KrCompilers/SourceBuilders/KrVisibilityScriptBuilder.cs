using Tessa.Compilation;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers.SourceBuilders
{
    public sealed class KrVisibilityScriptBuilder: KrSingleScriptBuilder<IVisibilitySources>
    {
        /// <inheritdoc />
        public KrVisibilityScriptBuilder(
            ICompilationSourceProvider compileSourceProvider,
            IKrPreprocessorProvider preprocessorProvider)
            : base(compileSourceProvider, preprocessorProvider)
        {
        }

        /// <inheritdoc />
        public override IKrSourceBuilder<IVisibilitySources> SetSources(
            IVisibilitySources source)
        {
            this.SourceText = source.VisibilitySourceCondition;
            return this;
        }

        /// <inheritdoc />
        protected override string ErrorPrefix { get; } = "$KrProcess_ErrorMessage_VisibilityTrace";

        /// <inheritdoc />
        protected override string ClassPrefix { get; } = SourceIdentifiers.KrVisibilityClass;

        /// <inheritdoc />
        protected override string MethodName { get; } = SourceIdentifiers.VisibilityMethod;
    }
}