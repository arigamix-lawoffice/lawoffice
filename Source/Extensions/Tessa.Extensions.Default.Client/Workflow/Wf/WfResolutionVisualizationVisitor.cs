using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared;
using Tessa.Platform;
using Tessa.Platform.Collections;
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;
using Tessa.UI.WorkflowViewer.Factories;
using Tessa.UI.WorkflowViewer.Layouts;
using Tessa.UI.WorkflowViewer.Shapes;

namespace Tessa.Extensions.Default.Client.Workflow.Wf
{
    /// <summary>
    /// Объект, посещающий узлы по дереву резолюций для их последующей обработки.
    /// </summary>
    public sealed class WfResolutionVisualizationVisitor
    {
        #region Constructors

        /// <summary>
        /// Создаёт экземпляр класса с указанием его зависимостей.
        /// </summary>
        /// <param name="nodeLayout">Макет визуализатора.</param>
        /// <param name="nodeFactory">Фабрика узлов визуализатора.</param>
        /// <param name="card">Карточка, обход резолюций которой выполняется.</param>
        /// <param name="extensionContainer">
        /// Контейнер расширений на визуализацию
        /// или <c>null</c>, если визуализация выполняется без расширений.
        /// </param>
        public WfResolutionVisualizationVisitor(
            INodeLayout nodeLayout,
            INodeFactory nodeFactory,
            Card card,
            IExtensionContainer extensionContainer = null)
        {
            if (nodeLayout is null)
            {
                throw new ArgumentNullException("nodeLayout");
            }
            if (nodeFactory is null)
            {
                throw new ArgumentNullException("nodeFactory");
            }
            if (card is null)
            {
                throw new ArgumentNullException("card");
            }

            this.nodeLayout = nodeLayout;
            this.nodeFactory = nodeFactory;
            this.card = card;
            this.extensionContainer = extensionContainer;
        }

        #endregion

        #region TreeLevel Private Class

        private sealed class TreeLevel
        {
            #region Properties

            public INode ParentNode { get; private set; }

            public IList<CardTaskHistoryItem> Children { get; private set; }

            public int ChildIndex { get; set; }     // = 0

            #endregion

            #region Methods

            public TreeLevel Set(
                INode parentNode,
                IList<CardTaskHistoryItem> children = null)
            {
                this.ParentNode = parentNode;
                this.Children = children ?? Array.Empty<CardTaskHistoryItem>();
                this.ChildIndex = 0;
                return this;
            }

            public void Return()
            {
                if (treeLevelPool != null)
                {
                    treeLevelPool.Return(this);
                }

                if (this.Children is List<CardTaskHistoryItem> childrenList && historyItemListPool != null)
                {
                    this.Children = null;
                    historyItemListPool.Return(childrenList);
                }
            }

            #endregion

            #region Static Methods

            public static TreeLevel Get()
            {
                treeLevelPool ??= new ObjectPool<TreeLevel>(() => new TreeLevel());
                return treeLevelPool.Get();
            }

            #endregion
        }

        #endregion

        #region Fields

        private readonly INodeLayout nodeLayout;

        private readonly INodeFactory nodeFactory;

        private readonly Card card;

        private readonly IExtensionContainer extensionContainer;

        private CardTaskHistoryItem[] historyItems;

        private Dictionary<Guid, CardTask> tasksByRowID;

        [ThreadStatic]
        private static ObjectPool<TreeLevel> treeLevelPool;

        [ThreadStatic]
        private static ObjectPool<List<CardTaskHistoryItem>> historyItemListPool;

        #endregion

        #region Private Methods

        private IList<CardTaskHistoryItem> GetChildrenHistoryItems(Guid parentRowID)
        {
            List<CardTaskHistoryItem> children = null;
            CardTaskHistoryItem[] items = this.GetHistoryItems();

            // ReSharper disable once ForCanBeConvertedToForeach
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].ParentRowID == parentRowID)
                {
                    if (children is null)
                    {
                        historyItemListPool ??= new ObjectPool<List<CardTaskHistoryItem>>(() => new List<CardTaskHistoryItem>());

                        children = historyItemListPool.Get();
                        children.Clear();
                    }

                    children.Add(items[i]);
                }
            }

            return (IList<CardTaskHistoryItem>)children
                ?? Array.Empty<CardTaskHistoryItem>();
        }

        private CardTaskHistoryItem[] GetHistoryItems()
        {
            if (this.historyItems != null)
            {
                return this.historyItems;
            }

            ListStorage<CardTaskHistoryItem> taskHistory = this.card.TryGetTaskHistory();

            return this.historyItems =
                (taskHistory is { Count: > 0 }
                    ? taskHistory.ToArray()
                    : Array.Empty<CardTaskHistoryItem>());
        }

        private CardTask TryGetTaskByHistoryItem(Guid historyItemRowID)
        {
            Dictionary<Guid, CardTask> tasksByRowID = this.GetTasksByRowID();

            return tasksByRowID.TryGetValue(historyItemRowID, out CardTask result)
                ? result
                : null;
        }

        private Dictionary<Guid, CardTask> GetTasksByRowID()
        {
            if (this.tasksByRowID != null)
            {
                return this.tasksByRowID;
            }

            ListStorage<CardTask> tasks = this.card.TryGetTasks();
            if (tasks is { Count: > 0 })
            {
                this.tasksByRowID = new Dictionary<Guid, CardTask>(tasks.Count);
                foreach (CardTask task in tasks)
                {
                    this.tasksByRowID[task.RowID] = task;
                }
            }
            else
            {
                this.tasksByRowID = new Dictionary<Guid, CardTask>();
            }

            return this.tasksByRowID;
        }

        private static WfResolutionParentAction GetParentAction(CardTaskHistoryItem historyItem)
        {
            Guid typeID = historyItem.TypeID;
            if (typeID == DefaultTaskTypes.WfResolutionChildTypeID)
            {
                return WfResolutionParentAction.CreateChild;
            }
            if (typeID == DefaultTaskTypes.WfResolutionTypeID)
            {
                return WfResolutionParentAction.SendToPerformer;
            }
            if (typeID == DefaultTaskTypes.WfResolutionControlTypeID)
            {
                return WfResolutionParentAction.SendControl;
            }

            return WfResolutionParentAction.None;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Выполняет посещение резолюций по дереву в глубину.
        /// </summary>
        /// <param name="generator">
        /// Объект, создающий узлы визуализации резолюций по истории заданий.
        /// </param>
        /// <param name="rootHistoryItem">
        /// Корневая запись в истории заданий среди посещаемых резолюций. Не может быть равна <c>null</c>.
        /// </param>
        /// <param name="rootTask">
        /// Корневое задание среди посещаемых резолюций
        /// или <c>null</c>, если для записи в истории заданий отсутствует задание.
        /// </param>
        /// <param name="session">Сессия, для которой происходит визуализация.</param>
        /// <param name="utcNow">Текущий момент времени, в который был вызван Visitor. Неободим для точных расчётов "Астрономических сроков".</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        public async Task VisitAsync(
            IWfResolutionVisualizationGenerator generator,
            CardTaskHistoryItem rootHistoryItem,
            CardTask rootTask,
            ISession session,
            DateTime utcNow,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(generator, nameof(generator));
            Check.ArgumentNotNull(card, nameof(card));
            Check.ArgumentNotNull(rootHistoryItem, nameof(rootHistoryItem));
            Check.ArgumentNotNull(session, nameof(session));

            var context = new WfResolutionVisualizationContext(
                this.nodeLayout,
                this.nodeFactory,
                this.card,
                rootHistoryItem,
                rootTask,
                session,
                utcNow,
                cancellationToken);

            IExtensionExecutor executor =
                this.extensionContainer != null
                    ? await this.extensionContainer.ResolveExecutorAsync<IWfResolutionVisualizationExtension>(cancellationToken)
                    : null;

            try
            {
                if (executor != null)
                {
                    await executor.ExecuteAsync(nameof(IWfResolutionVisualizationExtension.OnVisualizationStarted), context, continueOnCapturedContext: true);
                }

                CardTaskHistoryItem historyItem = rootHistoryItem;
                CardTask task = rootTask;
                WfResolutionParentAction parentAction = WfResolutionParentAction.None;

                var levels = new Stack<TreeLevel>();
                levels.Push(TreeLevel.Get().Set(null));

                while (levels.Count > 0)
                {
                    TreeLevel level = levels.Peek();
                    context.HistoryItem = historyItem;
                    context.Task = task;
                    context.ParentAction = parentAction;
                    context.ParentNode = level.ParentNode;
                    context.Node = null;

                    if (executor != null)
                    {
                        await executor.ExecuteAsync(nameof(IWfResolutionVisualizationExtension.OnNodeGenerating), context, continueOnCapturedContext: true);
                    }

                    INode currentNode = context.Node;
                    if (currentNode is null)
                    {
                        currentNode = await generator.GenerateAsync(context);
                        context.Node = currentNode;
                    }

                    if (executor != null)
                    {
                        await executor.ExecuteAsync(nameof(IWfResolutionVisualizationExtension.OnNodeGenerated), context, continueOnCapturedContext: true);
                        currentNode = context.Node;
                    }

                    IList<CardTaskHistoryItem> children = this.GetChildrenHistoryItems(historyItem.RowID);

                    if (children.Count == 0)
                    {
                        // листовой узел, пытаемся обойти следующего дочернего на том же уровне или на предыдущем

                        while (true)
                        {
                            level.ChildIndex++;

                            if (level.ChildIndex < level.Children.Count)
                            {
                                // есть ещё дочерний узел на том же уровне, который пока не обошли
                                historyItem = level.Children[level.ChildIndex];
                                task = this.TryGetTaskByHistoryItem(historyItem.RowID);
                                parentAction = GetParentAction(historyItem);
                                break;
                            }

                            // уходим на уровень выше и берём следующий узел
                            level.Return();
                            levels.Pop();

                            if (levels.Count == 0)
                            {
                                // дошли до самого верха, завершаем визуализацию
                                break;
                            }

                            level = levels.Peek();
                        }
                    }
                    else
                    {
                        // в узле есть дочерние элементы, сначала обходим их
                        levels.Push(TreeLevel.Get().Set(currentNode, children));

                        historyItem = children[0];
                        task = this.TryGetTaskByHistoryItem(historyItem.RowID);
                        parentAction = GetParentAction(historyItem);
                    }
                }

                if (executor != null)
                {
                    await executor.ExecuteAsync(nameof(IWfResolutionVisualizationExtension.OnVisualizationCompleted), context, continueOnCapturedContext: true);
                }
            }
            finally
            {
                if (executor != null)
                {
                    await executor.DisposeAsync();
                }
            }
        }

        #endregion
    }
}
