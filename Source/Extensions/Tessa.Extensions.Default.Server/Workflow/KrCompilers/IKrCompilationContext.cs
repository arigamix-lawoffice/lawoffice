#nullable enable

using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Tessa.Extensions.Default.Server.Workflow.KrCompilers.SourceBuilders;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers
{
    /// <summary>
    /// Контекст компиляции объектов подсистемы маршрутов.
    /// </summary>
    public interface IKrCompilationContext
    {
        /// <summary>
        /// Список базовых методов, подлежащих компиляции.
        /// </summary>
        IList<IKrCommonMethod> CommonMethods { get; }

        /// <summary>
        /// Список данных о скриптах, выполняемых в процессе прохождения маршрута.
        /// </summary>
        IList<IKrRuntimeStage> Stages { get; }

        /// <summary>
        /// Список шаблонов этапов, подлежащих компиляции
        /// </summary>
        IList<IKrStageTemplate> StageTemplates { get; }

        /// <summary>
        /// Список групп этапов, подлежащих компиляции.
        /// </summary>
        IList<IKrStageGroup> StageGroups { get; }

        /// <summary>
        /// Список вторичных процессов, подлежащих компиляции.
        /// </summary>
        IList<IKrSecondaryProcess> SecondaryProcesses { get; }

        /// <summary>
        /// Коллекция ключ-значение, где ключ - идентификатор исходного кода; значение - объект, идентифицирующий элемент компиляции. Обеспечивает связь между идентификатором исходного кода и объектом, идентифицирующим элемент компиляции.
        /// </summary>
        Dictionary<Guid, CompilationAnchor> AnchorsMap { get; }

        /// <summary>
        /// Простое имя сборки.
        /// </summary>
        string SimpleAssemblyName { get; set; }

        /// <summary>
        /// Список ссылок на образы внешних зависимостей.
        /// </summary>
        IList<MetadataReference> MetadataReferences { get; }
    }
}
