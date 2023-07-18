using Tessa.Compilation;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers.SourceBuilders
{
    public sealed class KrDesignScriptBuilder: KrScriptBuilder<IDesignTimeSources>
    {
        /// <inheritdoc />
        public KrDesignScriptBuilder(
            ICompilationSourceProvider compileSourceProvider,
            IKrPreprocessorProvider preprocessorProvider)
            : base(compileSourceProvider, preprocessorProvider)
        {
        }

        /// <inheritdoc />
        protected override string ConditionErrorPrefix { get; } = "$KrProcess_ErrorMessage_DesignConditionTrace";

        /// <inheritdoc />
        protected override string BeforeErrorPrefix { get; } = "$KrProcess_ErrorMessage_DesignBeforeTrace";

        /// <inheritdoc />
        protected override string AfterErrorPrefix { get; } = "$KrProcess_ErrorMessage_DesignAfterTrace";

        /// <inheritdoc />
        protected override string ClassPrefix { get; } = SourceIdentifiers.KrDesignTimeClass;

        /// <inheritdoc />
        public override IKrSourceBuilder<IDesignTimeSources> SetSources(
            IDesignTimeSources source)
        {
            this.ConditionMethodBody = source.SourceCondition;
            this.BeforeMethodBody = source.SourceBefore;
            this.AfterMethodBody = source.SourceAfter;
            return this;
        }

    }
}