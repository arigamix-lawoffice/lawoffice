using System;
using System.Collections.Generic;
using Tessa.Compilation;
using Tessa.Extensions.Default.Shared.Workflow.KrCompilers;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers.SourceBuilders
{
    /// <summary>
    /// Описывает построитель генерируемых классов.
    /// </summary>
    /// <typeparam name="T">Тип объекта являющегося источником исходных кодов.</typeparam>
    public interface IKrSourceBuilder<in T>
    {
        /// <summary>
        /// Устанавливает идентификатор генерируемого класса.
        /// </summary>
        /// <param name="id">ИД класса.</param>
        /// <returns>Объект <see cref="IKrSourceBuilder{T}"/> для создания цепочки.</returns>
        IKrSourceBuilder<T> SetClassID(
            Guid id);

        /// <summary>
        /// Устанавливает алиас генерируемого класса.
        /// </summary>
        /// <param name="classAlias">Алиас класса.</param>
        /// <returns>Объект <see cref="IKrSourceBuilder{T}"/> для создания цепочки.</returns>
        IKrSourceBuilder<T> SetClassAlias(
            string classAlias);

        /// <summary>
        /// Задаёт информацию о месте расположения текущего элемента относительно корневого.
        /// </summary>
        /// <param name="stageName">Название этапа. Может быть не задано.</param>
        /// <param name="stageTemplateName">Название шаблона этапов. Может быть не задано.</param>
        /// <param name="stageGroupName">Название группы этапов. Может быть не задано.</param>
        /// <param name="secondaryProcessName">Название вторичного процесса. Может быть не задано.</param>
        /// <returns>Объект <see cref="IKrSourceBuilder{T}"/> для создания цепочки.</returns>
        IKrSourceBuilder<T> SetLocation(
            string stageName = null,
            string stageTemplateName = null,
            string stageGroupName = null,
            string secondaryProcessName = null);

        /// <summary>
        /// Устанавливает источник исходных кодов.
        /// </summary>
        /// <param name="source">Источник исходных кодов.</param>
        /// <returns>Объект <see cref="IKrSourceBuilder{T}"/> для создания цепочки.</returns>
        IKrSourceBuilder<T> SetSources(
            T source);

        /// <summary>
        /// Устанавливает дополнительные исходные коды.
        /// </summary>
        /// <param name="extraSources">Дополнительные исходные коды.</param>
        /// <returns>Объект <see cref="IKrSourceBuilder{T}"/> для создания цепочки.</returns>
        IKrSourceBuilder<T> SetExtraSources(
            IExtraSources extraSources);

        /// <summary>
        /// Устанавливает связь между идентификатором исходного кода и объектом идентифицирующим элемент компиляции.
        /// </summary>
        /// <param name="anchorsMap">Коллекция ключ-значение, где ключ - идентификатор исходного кода; значение - объект, идентифицирующий элемент компиляции.</param>
        /// <returns>Объект <see cref="IKrSourceBuilder{T}"/> для создания цепочки.</returns>
        IKrSourceBuilder<T> FillAnchorsMap(
            Dictionary<Guid, CompilationAnchor> anchorsMap);

        /// <summary>
        /// Выполняет компиляцию исходных кодов.
        /// </summary>
        /// <returns>Список результатов компиляции.</returns>
        IList<ICompilationSource> BuildSources();
    }
}