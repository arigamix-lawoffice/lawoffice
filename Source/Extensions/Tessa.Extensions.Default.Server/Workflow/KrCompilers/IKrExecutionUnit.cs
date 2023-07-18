using System;
using Tessa.Extensions.Default.Server.Workflow.KrCompilers.UserAPI;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers
{
    /// <summary>
    /// Агрегация метаинформации об шаблоне этапа или группе этапов.(название, позиция, sql) 
    /// и объекта, сгенерированного на основе карточки, скомпилированного и инстанцированного.
    /// </summary>
    public interface IKrExecutionUnit
    {
        /// <summary>
        /// ID карточки - элемента выполнения.
        /// </summary>
        Guid ID { get; }

        /// <summary>
        /// Название карточки - элемента выполнения.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Информация о шаблоне этапов.
        /// </summary>
        IKrStageTemplate StageTemplateInfo { get; }

        /// <summary>
        /// Информация о группе этапов.
        /// </summary>
        IKrStageGroup StageGroupInfo { get; }

        /// <summary>
        /// Информация об этапе в процессе выполнения.
        /// </summary>
        IKrRuntimeStage RuntimeStage { get; }

        /// <summary>
        /// Исходные коды на момент выполнения процесса.
        /// </summary>
        IRuntimeSources RuntimeSources { get; }

        /// <summary>
        /// Исходные коды для этапа перерасчета.
        /// </summary>
        IDesignTimeSources DesignTimeSources { get; }

        /// <summary>
        /// Сгенерированный на основе карточки, скомпилированный и созданный объект.
        /// </summary>
        IKrScript Instance { get; }
    }
}
