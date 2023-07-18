using System;
using Tessa.Extensions.Default.Shared.Workflow.KrCompilers;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers
{
    /// <summary>
    /// Объект, предоставляющий информацию об шаблоне этапов,
    /// необходимую для его компиляции и выполнения.
    /// </summary>
    public interface IKrStageTemplate :
        IDesignTimeSources
    {
        /// <summary>
        /// Возвращает идентификатор шаблона этапов.
        /// </summary>
        Guid ID { get; }

        /// <summary>
        /// Возвращает название шаблона этапов.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Возвращает идентификатор группы этапов, к которой относится шаблон.
        /// </summary>
        Guid StageGroupID { get; }

        /// <summary>
        /// Возвращает название группы этапов, к которой относится шаблон.
        /// </summary>
        string StageGroupName { get; }

        /// <summary>
        /// Возвращает порядок шаблона.
        /// </summary>
        int Order { get; }

        /// <summary>
        /// Возвращает положение относительно этапов, добавленных вручную.
        /// </summary>
        GroupPosition Position { get; }

        /// <summary>
        /// Возвращает значение, показывающее, можно ли перемещать этапы.
        /// </summary>
        bool CanChangeOrder { get; }

        /// <summary>
        /// Возвращает значение, показывающее, являются ли этапы нередактируемыми.
        /// </summary>
        bool IsStagesReadonly { get; }
    }
}
