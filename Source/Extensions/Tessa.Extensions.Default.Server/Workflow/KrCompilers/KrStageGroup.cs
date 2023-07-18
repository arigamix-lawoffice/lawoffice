using System;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers
{
    /// <inheritdoc cref="IKrStageGroup"/>
    public class KrStageGroup :
        IKrStageGroup
    {
        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="KrStageGroup"/>.
        /// </summary>
        /// <param name="id">Идентификатор группы этапов.</param>
        /// <param name="name">Название группы этапов.</param>
        /// <param name="order">Порядок группы этапов.</param>
        /// <param name="isGroupReadonly">Значение, показывающее, являются ли все этапы нередактируемыми.</param>
        /// <param name="secondaryProcessID">Идентификатор вторичного процесса, к которому привязана группа этапов.</param>
        /// <param name="sqlCondition">Текст SQL запроса условия времени построения.</param>
        /// <param name="runtimeSqlCondition">Текст SQL запроса условия времени выполнения.</param>
        /// <param name="sourceCondition">C# код условия времени построения.</param>
        /// <param name="sourceBefore">C# код сценария инициализации времени построения.</param>
        /// <param name="sourceAfter">C# код сценария постобработки времени построения.</param>
        /// <param name="runtimeSourceCondition">C# код условия времени выполнения.</param>
        /// <param name="runtimeSourceBefore">C# код сценария инициализации времени выполнения.</param>
        /// <param name="runtimeSourceAfter">C# код сценария постобработки времени выполнения.</param>
        public KrStageGroup(
            Guid id,
            string name,
            int order,
            bool isGroupReadonly,
            Guid? secondaryProcessID,
            string sqlCondition,
            string runtimeSqlCondition,
            string sourceCondition,
            string sourceBefore,
            string sourceAfter,
            string runtimeSourceCondition,
            string runtimeSourceBefore,
            string runtimeSourceAfter)
        {
            this.ID = id;
            this.Name = name;
            this.Order = order;
            this.IsGroupReadonly = isGroupReadonly;
            this.SecondaryProcessID = secondaryProcessID;
            this.SqlCondition = sqlCondition;
            this.RuntimeSqlCondition = runtimeSqlCondition;
            this.SourceCondition = sourceCondition;
            this.SourceBefore = sourceBefore;
            this.SourceAfter = sourceAfter;
            this.RuntimeSourceCondition = runtimeSourceCondition;
            this.RuntimeSourceBefore = runtimeSourceBefore;
            this.RuntimeSourceAfter = runtimeSourceAfter;
        }

        #endregion

        #region IKrStageGroup Members

        /// <inheritdoc />
        public Guid ID { get; }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public int Order { get; }

        /// <inheritdoc />
        public bool IsGroupReadonly { get; }

        /// <inheritdoc />
        public Guid? SecondaryProcessID { get; }

        /// <inheritdoc />
        public string SqlCondition { get; }

        /// <inheritdoc />
        public string RuntimeSqlCondition { get; }

        /// <inheritdoc />
        public string SourceCondition { get; }

        /// <inheritdoc />
        public string SourceBefore { get; }

        /// <inheritdoc />
        public string SourceAfter { get; }

        /// <inheritdoc />
        public string RuntimeSourceCondition { get; }

        /// <inheritdoc />
        public string RuntimeSourceBefore { get; }

        /// <inheritdoc />
        public string RuntimeSourceAfter { get; }

        #endregion
    }
}
