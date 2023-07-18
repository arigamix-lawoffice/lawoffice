using System;
using System.Threading;
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
    public sealed class WfResolutionVisualizationContext :
        IWfResolutionVisualizationContext
    {
        #region Constructors

        /// <summary>
        /// Создаёт экземпляр класса с указанием значений его свойств.
        /// </summary>
        /// <param name="nodeLayout">Макет визуализатора.</param>
        /// <param name="nodeFactory">Фабрика узлов визуализатора.</param>
        /// <param name="card">Карточка, обход резолюций которой выполняется.</param>
        /// <param name="rootHistoryItem">
        /// Корневая запись в истории заданий среди посещаемых резолюций.
        /// Не может быть равна <c>null</c>.
        /// </param>
        /// <param name="rootTask">
        /// Корневое задание среди посещаемых резолюций
        /// или <c>null</c>, если для записи в истории заданий отсутствует задание.
        /// </param>
        /// <param name="session">Сессия, для которой происходит визуализация.</param>
        /// <param name="utcNow">Момент времени для которого происходит визуализация.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        public WfResolutionVisualizationContext(
            INodeLayout nodeLayout,
            INodeFactory nodeFactory,
            Card card,
            CardTaskHistoryItem rootHistoryItem,
            CardTask rootTask,
            ISession session,
            DateTime utcNow,
            CancellationToken cancellationToken = default)
        {
            this.NodeLayout = nodeLayout ?? throw new ArgumentNullException(nameof(nodeLayout));
            this.NodeFactory = nodeFactory ?? throw new ArgumentNullException(nameof(nodeFactory));
            this.Card = card ?? throw new ArgumentNullException(nameof(card));
            this.RootHistoryItem = rootHistoryItem ?? throw new ArgumentNullException(nameof(rootHistoryItem));
            this.RootTask = rootTask;
            this.Session = session;
            this.UtcNow = utcNow;
            this.CancellationToken = cancellationToken;
        }

        #endregion

        #region IExtensionContext Members

        /// <doc path='info[@type="IExtensionContext" and @item="CancellationToken"]'/>
        public CancellationToken CancellationToken { get; set; }

        #endregion

        #region IWfResolutionVisualizationContext Members

        /// <inheritdoc/>
        public INodeLayout NodeLayout { get; }

        /// <inheritdoc/>
        public INodeFactory NodeFactory { get; }

        /// <inheritdoc/>
        public Card Card { get; }

        /// <inheritdoc/>
        public CardTaskHistoryItem RootHistoryItem { get; }

        /// <inheritdoc/>
        public CardTask RootTask { get; }

        /// <inheritdoc/>
        public ISerializableObject Info { get; } = new SerializableObject();

        /// <inheritdoc/>
        /// <remarks>
        /// Значение изменяется при посещении очередного узла.
        /// </remarks>
        public CardTaskHistoryItem HistoryItem { get; set; }

        /// <inheritdoc/>
        /// <remarks>
        /// Значение изменяется при посещении очередного узла.
        /// </remarks>
        public CardTask Task { get; set; }

        /// <inheritdoc/>
        /// <remarks>
        /// Значение изменяется при посещении очередного узла.
        /// </remarks>
        public INode Node { get; set; }

        /// <inheritdoc/>
        /// <remarks>
        /// Значение изменяется при посещении очередного узла.
        /// </remarks>
        public INode ParentNode { get; set; }

        /// <inheritdoc/>
        /// <remarks>
        /// Значение изменяется при посещении очередного узла.
        /// </remarks>
        public WfResolutionParentAction ParentAction { get; set; }

        /// <inheritdoc/>
        public DateTime UtcNow { get; }

        /// <inheritdoc/>
        public ISession Session { get; }

        #endregion
    }
}
