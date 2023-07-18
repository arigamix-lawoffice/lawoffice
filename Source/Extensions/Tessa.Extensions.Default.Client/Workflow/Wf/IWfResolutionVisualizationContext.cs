using System;
using Tessa.Cards;
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;
using Tessa.UI.WorkflowViewer.Factories;
using Tessa.UI.WorkflowViewer.Layouts;
using Tessa.UI.WorkflowViewer.Shapes;

namespace Tessa.Extensions.Default.Client.Workflow.Wf
{
    /// <summary>
    /// Контекст расширений на посещение записей в истории резолюций
    /// для визуализации посредством <see cref="IWfResolutionVisualizationGenerator"/>.
    /// </summary>
    public interface IWfResolutionVisualizationContext :
        IExtensionContext
    {
        /// <summary>
        /// Макет визуализатора.
        /// </summary>
        INodeLayout NodeLayout { get; }

        /// <summary>
        /// Фабрика узлов визуализатора.
        /// </summary>
        INodeFactory NodeFactory { get; }

        /// <summary>
        /// Карточка, обход резолюций которой выполняется.
        /// </summary>
        Card Card { get; }

        /// <summary>
        /// Корневая запись в истории заданий среди посещаемых резолюций.
        /// Не может быть равна <c>null</c>.
        /// </summary>
        CardTaskHistoryItem RootHistoryItem { get; }

        /// <summary>
        /// Корневое задание среди посещаемых резолюций
        /// или <c>null</c>, если для записи в истории заданий отсутствует задание.
        /// </summary>
        CardTask RootTask { get; }

        /// <summary>
        /// Дополнительная информация, которая может пригодиться между посещениями узлов.
        /// </summary>
        ISerializableObject Info { get; }

        /// <summary>
        /// Посещаемая запись в истории заданий.
        /// </summary>
        CardTaskHistoryItem HistoryItem { get; }

        /// <summary>
        /// Задание, которое соответствует записи <see cref="HistoryItem"/>,
        /// или <c>null</c>, если задание уже завершено или не было загружено.
        /// Даже если задание указано, его секции могут быть не загружены.
        /// </summary>
        CardTask Task { get; }

        /// <summary>
        /// Узел, соответствующий текущей резолюции,
        /// или <c>null</c>, если узел для текущей резолюции ещё не был создан.
        ///
        /// При установке узла в методе расширений <see cref="IWfResolutionVisualizationExtension.OnNodeGenerating"/>
        /// генерация узла стандартными средствами не будет выполнена.
        ///
        /// В методе расширений <see cref="IWfResolutionVisualizationExtension.OnNodeGenerated"/>
        /// узел должен быть установлен как текущий сгенерированный узел.
        /// </summary>
        INode Node { get; set; }

        /// <summary>
        /// Узел, соответствующий родительской резолюции,
        /// или <c>null</c>, если текущая резолюция не имеет родительской.
        /// </summary>
        INode ParentNode { get; }

        /// <summary>
        /// Действие родительской резолюции по отношению к текущей,
        /// в результате которого текущая резолюция была создана.
        /// </summary>
        WfResolutionParentAction ParentAction { get; }

        /// <summary>
        /// Момент времени для которого происходит визуализация.
        /// </summary>
        DateTime UtcNow { get; }

        /// <summary>
        /// Сессия, для которой происходит визуализация.
        /// </summary>
        ISession Session { get; }
    }
}
