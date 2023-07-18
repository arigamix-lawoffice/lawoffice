using Tessa.Compilation;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers.SourceBuilders
{
    public sealed class KrRuntimeScriptBuilder: KrScriptBuilder<IRuntimeSources>
    {
        /// <inheritdoc />
        public KrRuntimeScriptBuilder(
            ICompilationSourceProvider compileSourceProvider,
            IKrPreprocessorProvider preprocessorProvider)
            : base(compileSourceProvider, preprocessorProvider)
        {
        }

        /// <inheritdoc />
        protected override string ConditionErrorPrefix { get; } = "$KrProcess_ErrorMessage_RuntimeConditionTrace";

        /// <inheritdoc />
        protected override string BeforeErrorPrefix { get; } = "$KrProcess_ErrorMessage_RuntimeBeforeTrace";

        /// <inheritdoc />
        protected override string AfterErrorPrefix { get; } = "$KrProcess_ErrorMessage_RuntimeAfterTrace";

        /// <inheritdoc />
        protected override string ClassPrefix { get; } = SourceIdentifiers.KrRuntimeClass;

        /// <inheritdoc />
        public override IKrSourceBuilder<IRuntimeSources> SetSources(
            IRuntimeSources source)
        {
            this.ConditionMethodBody = source.RuntimeSourceCondition;
            this.BeforeMethodBody = source.RuntimeSourceBefore;
            this.AfterMethodBody = source.RuntimeSourceAfter;
            return this;
        }
    }
}