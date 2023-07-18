using System;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers
{
    /// <summary>
    /// Объект, предоставляющий информацию о группе этапов.
    /// </summary>
    public interface IKrStageGroup :
        IRuntimeSources,
        IDesignTimeSources
    {
        /// <summary>
        /// Идентификатор группы этапов.
        /// </summary>
        Guid ID { get; }

        /// <summary>
        /// Название группы этапов.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Порядок группы этапов.
        /// </summary>
        int Order { get; }

        /// <summary>
        /// Значение, показывающее, являются ли все этапы нередактируемыми.
        /// </summary>
        bool IsGroupReadonly { get; }

        /// <summary>
        /// Идентификатор вторичного процесса, к которому привязана группа этапов.
        /// </summary>
        Guid? SecondaryProcessID { get; }
    }
}
