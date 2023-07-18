using System;
using Tessa.Extensions.Default.Server.Workflow.KrCompilers.UserAPI;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers
{
    /// <inheritdoc />
    public class KrExecutionUnit: IKrExecutionUnit
    {
        #region constructor

        public KrExecutionUnit(
            IKrStageTemplate stageInfo,
            IKrScript instance)
        {
            this.StageTemplateInfo = stageInfo;
            this.Instance = instance;

            this.ID = this.StageTemplateInfo.ID;
            this.Name = this.StageTemplateInfo.Name;
            this.DesignTimeSources = this.StageTemplateInfo;
        }

        public KrExecutionUnit(
            IKrRuntimeStage runtimeStage,
            IKrScript instance)
        {
            this.RuntimeStage = runtimeStage;
            this.Instance = instance;

            this.ID = runtimeStage.StageID;
            this.Name = runtimeStage.StageName;
            this.RuntimeSources = this.RuntimeStage;
        }

        public KrExecutionUnit(
            IKrStageGroup stageGroupInfo,
            IKrScript instance)
        {
            this.StageTemplateInfo = null;
            this.StageGroupInfo = stageGroupInfo;
            this.Instance = instance;

            this.ID = this.StageGroupInfo.ID;
            this.Name = this.StageGroupInfo.Name;
            this.RuntimeSources = this.StageGroupInfo;
            this.DesignTimeSources = this.StageGroupInfo;
        }

        #endregion

        #region implementation

        /// <inheritdoc />
        public Guid ID { get; }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public IKrStageTemplate StageTemplateInfo { get; }

        /// <inheritdoc />
        public IKrStageGroup StageGroupInfo { get; }

        /// <inheritdoc />
        public IKrRuntimeStage RuntimeStage { get; }

        /// <inheritdoc />
        public IRuntimeSources RuntimeSources { get; }

        /// <inheritdoc />
        public IDesignTimeSources DesignTimeSources { get; }

        /// <inheritdoc />
        public IKrScript Instance { get; }

        #endregion
    }
}
