using System;
using Tessa.Extensions.Default.Shared.Workflow.KrCompilers;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers
{
    /// <inheritdoc cref="IKrStageTemplate"/>
    public sealed class KrStageTemplate :
        IKrStageTemplate
    {
        #region Constructors

        public KrStageTemplate(
            Guid id,
            string name,
            int order,
            Guid stageGroupID,
            string stageGroupName = null,
            GroupPosition position = null,
            bool canChangeOrder = false,
            bool isStagesReadonly = false,
            string sqlCondition = null,
            string sourceCondition = null,
            string sourceBefore = null,
            string sourceAfter = null)
        {
            this.ID = id;
            this.Name = name;
            this.Order = order;
            this.StageGroupID = stageGroupID;
            this.StageGroupName = stageGroupName;
            this.Position = position ?? GroupPosition.Unspecified;
            this.CanChangeOrder = canChangeOrder;
            this.IsStagesReadonly = isStagesReadonly;
            this.SqlCondition = sqlCondition ?? string.Empty;
            this.SourceCondition = sourceCondition ?? string.Empty;
            this.SourceBefore = sourceBefore ?? string.Empty;
            this.SourceAfter = sourceAfter ?? string.Empty;
        }

        #endregion

        #region IKrStageTemplate Members

        /// <inheritdoc />
        public Guid ID { get; }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public Guid StageGroupID { get; }

        /// <inheritdoc />
        public string StageGroupName { get; }

        /// <inheritdoc />
        public int Order { get; }

        /// <inheritdoc />
        public GroupPosition Position { get; }

        /// <inheritdoc />
        public bool CanChangeOrder { get; }

        /// <inheritdoc />
        public bool IsStagesReadonly { get; }

        /// <inheritdoc />
        public string SqlCondition { get; }

        /// <inheritdoc />
        public string SourceCondition { get; }

        /// <inheritdoc />
        public string SourceBefore { get; }

        /// <inheritdoc />
        public string SourceAfter { get; }

        #endregion
    }
}
