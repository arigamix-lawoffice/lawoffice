using Tessa.Compilation;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers.SourceBuilders
{
    public sealed class KrExecutionScriptBuilder : KrSingleScriptBuilder<IExecutionSources>
    {
        /// <inheritdoc />
        public KrExecutionScriptBuilder(
            ICompilationSourceProvider compileSourceProvider,
            IKrPreprocessorProvider preprocessorProvider)
            : base(compileSourceProvider, preprocessorProvider)
        {
        }

        /// <inheritdoc />
        public override IKrSourceBuilder<IExecutionSources> SetSources(
            IExecutionSources source)
        {
            this.SourceText = source.ExecutionSourceCondition;
            return this;
        }

        /// <inheritdoc />
        protected override string ErrorPrefix { get; } = "$KrProcess_ErrorMessage_ExecutionTrace";

        /// <inheritdoc />
        protected override string ClassPrefix { get; } = SourceIdentifiers.KrExecutionClass;

        /// <inheritdoc />
        protected override string MethodName { get; } = SourceIdentifiers.ExecutionMethod;
    }
}