using Tessa.Compilation;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers.SourceBuilders
{
    public sealed class KrSourceBuilderFactory: IKrSourceBuilderFactory
    {
        private readonly ICompilationSourceProvider compilationSourceProvider;

        private readonly IKrPreprocessorProvider preprocessorProvider;

        public KrSourceBuilderFactory(
            ICompilationSourceProvider compilationSourceProvider,
            IKrPreprocessorProvider preprocessorProvider)
        {
            this.compilationSourceProvider = compilationSourceProvider;
            this.preprocessorProvider = preprocessorProvider;
        }

        /// <inheritdoc />
        public KrCommonMethodBuilder GetKrCommonMethodBuilder() =>
            new KrCommonMethodBuilder(this.compilationSourceProvider, this.preprocessorProvider);

        /// <inheritdoc />
        public KrDesignScriptBuilder GetKrDesignScriptBuilder() =>
            new KrDesignScriptBuilder(this.compilationSourceProvider, this.preprocessorProvider);

        /// <inheritdoc />
        public KrRuntimeScriptBuilder GetKrRuntimeScriptBuilder() =>
            new KrRuntimeScriptBuilder(this.compilationSourceProvider, this.preprocessorProvider);

        /// <inheritdoc />
        public KrVisibilityScriptBuilder GetKrVisibilityScriptBuilder() =>
            new KrVisibilityScriptBuilder(this.compilationSourceProvider, this.preprocessorProvider);

        /// <inheritdoc />
        public KrExecutionScriptBuilder GetKrExecutionScriptBuilder() =>
            new KrExecutionScriptBuilder(this.compilationSourceProvider, this.preprocessorProvider);
    }
}