using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Tessa.Cards;
using Tessa.Platform;
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;
using Tessa.UI.Cards;
using Tessa.UI.WorkflowViewer.Factories;
using Tessa.UI.WorkflowViewer.Layouts;

namespace Tessa.Extensions.Default.Client.Workflow.Wf
{
    /// <summary>
    /// Вспомогательные поля и методы, которые используются для построения пользовательского
    /// интерфейса для бизнес-процессов Workflow.
    /// </summary>
    public static class WfUIHelper
    {
        #region Constants

        /// <summary>
        /// Имя блока в задании для основной информации.
        /// </summary>
        public const string MainInfoBlockName = "MainInfo";

        /// <summary>
        /// Суффикс для имени такого элемента управления в блоке задания <see cref="MainInfoBlockName"/>,
        /// который должен быть доступен только при выделении галки "с контролем".
        /// </summary>
        public const string WithControlSuffix = "_WithControl";

        /// <summary>
        /// Суффикс для имени такого элемента управления в блоке задания <see cref="MainInfoBlockName"/>,
        /// который должен быть доступен только при выделении галки "дополнительно".
        /// </summary>
        public const string AdditionalSuffix = "_Additional";

        /// <summary>
        /// Суффикс для имени такого элемента управления в блоке задания <see cref="MainInfoBlockName"/>,
        /// который должен быть доступен только при наличии хотя бы одной дочерней резолюции.
        /// </summary>
        public const string ChildResolutionsSuffix = "_ChildResolutions";

        /// <summary>
        /// Имя блока для списка исполнителей.
        /// </summary>
        public const string PerformersBlockName = "Performers";

        /// <summary>
        /// Суффикс для имени такого элемента управления в блоке задания <see cref="PerformersBlockName"/>,
        /// который должен быть доступен только при наличии хотя бы двух ролей в списке исполнителей резолюции.
        /// </summary>
        public const string MultiplePerformersSuffix = "_MultiplePerformers";

        /// <summary>
        /// Суффикс для имени такого элемента управления в блоке задания <see cref="PerformersBlockName"/>,
        /// который должен быть доступен только при наличии хотя бы двух ролей в списке исполнителей резолюции,
        /// а также при установленном флажке "Отдельная задача каждому исполнителю".
        /// </summary>
        public const string MassCreationSuffix = "_MassCreation";

        /// <summary>
        /// Имя блока в задании для дочерних резолюций.
        /// </summary>
        public const string ChildResolutionsBlockName = "ChildResolutions";

        /// <summary>
        /// Имя блока в настройках типа карточки и типа документа для настроек резолюций.
        /// </summary>
        public const string UseResolutionsBlockName = "UseResolutionsBlock";

        /// <summary>
        /// Имя контрола в настройках типа карточки, который содержит строки с настройками типов карточек,
        /// входящих в типовое решение, в т.ч. с настройками резолюций.
        /// </summary>
        public const string CardTypesControlName = "CardTypeControl";

        /// <summary>
        /// Суффикс для имени такого элемента управления в блоке настроек <see cref="UseResolutionsBlockName"/>,
        /// который должен быть доступен только при установке флага "Использовать резолюции".
        /// </summary>
        public const string UseResolutionsSuffix = "_UseResolutions";

        /// <summary>
        /// Имя элемента меню для визуализации ветки резолюций.
        /// </summary>
        public const string VisualizeResolutionBranchMenuAction = "WfResolution_VisualizeBranch";

        /// <summary>
        /// Имя элемента меню для визуализации процесса резолюций.
        /// </summary>
        public const string VisualizeResolutionProcessMenuAction = "WfResolution_VisualizeProcess";

        /// <summary>
        /// Имя сепаратора меню, отделяющего элементы меню, связанные с визуализацией, от других элементов.
        /// </summary>
        public const string VisualizeResolutionSeparatorMenuAction = "WfResolution_Visualize_Separator";

        /// <summary>
        /// Имя элемента меню для перехода к карточке с файлами, приложенными к заданию.
        /// </summary>
        public const string NavigateTaskCardResolutionProcessMenuAction = "WfResolution_NavigateTaskCard";

        #endregion

        #region Static Methods

        /// <summary>
        /// Устанавливает видимость элементов управления для текущего блока.
        /// </summary>
        /// <param name="block">Блок, для элементов управления которого требуется определить видимость.</param>
        /// <param name="suffix">Суффикс в имени элементов управления, для которых надо изменить видимость.</param>
        /// <param name="isVisible">
        /// Признак того, что элемент управления должен быть видим.
        /// Значение <c>false</c> определяет, что элемент управления должен быть скрыт.
        /// </param>
        public static void SetControlVisibility(IBlockViewModel block, string suffix, bool isVisible)
        {
            bool hasChanges = false;

            foreach (IControlViewModel control in block.Controls)
            {
                string controlName = control.Name;
                if (controlName != null && controlName.EndsWith(suffix, StringComparison.Ordinal))
                {
                    bool controlIsVisible = control.ControlVisibility == Visibility.Visible;
                    if (controlIsVisible != isVisible)
                    {
                        control.ControlVisibility = isVisible ? Visibility.Visible : Visibility.Collapsed;
                        hasChanges = true;
                    }
                }
            }

            if (hasChanges)
            {
                // если в блоке есть и другие контролы, которые всегда видимы,
                // то было бы достаточно вызова block.RearrangeSelf();

                // но если это единственный нескрытый блок на форме, в котором скрыты все контролы,
                // то нужно делать полный Rearrange() формы

                block.Form.Rearrange();
            }
        }

        /// <summary>
        /// Возвращает признак того, что в заданном блоке <paramref name="block"/> есть хотя бы один элемент управления,
        /// имя которого заканчивается на суффикс <paramref name="suffix"/>.
        /// </summary>
        /// <param name="block">
        /// Блок, в котором требуется проверить наличие элементов управления с заданным суффиксом.
        /// </param>
        /// <param name="suffix">Суффикс, в наличии которого требуется убедиться хотя бы для одного контрола в блоке.</param>
        /// <returns>
        /// <c>true</c>, если в заданном блоке <paramref name="block"/> есть хотя бы один элемент управления,
        /// имя которого заканчивается на суффикс <paramref name="suffix"/>.
        /// </returns>
        public static bool HasControlsWithSuffix(IBlockViewModel block, string suffix)
        {
            foreach (IControlViewModel control in block.Controls)
            {
                string controlName = control.Name;
                if (controlName != null && controlName.EndsWith(suffix, StringComparison.Ordinal))
                {
                    return true;
                }
            }

            return false;
        }

        #endregion

        #region VisualizeResolutionsAsync Static Methods

        /// <summary>
        /// Выполняет визуализацию резолюций, начиная от заданной резолюции
        /// <paramref name="rootResolution"/>.
        /// </summary>
        /// <param name="generator">
        /// Объект, посредством которого выполняется генерация узлов визуализации.
        /// Не может быть равен <c>null</c>.
        /// </param>
        /// <param name="nodeLayout">Макет визуализатора. Не может быть равен <c>null</c>.</param>
        /// <param name="nodeFactory">Фабрика узлов визуализатора. Не может быть равна <c>null</c>.</param>
        /// <param name="card">
        /// Карточка, в которой визуализируются резолюции. Не может быть равен <c>null</c>.
        /// </param>
        /// <param name="rootResolution">
        /// Задание резолюции, начиная от которого выполняется визуализация.
        /// Не может быть равен <c>null</c>.
        /// </param>
        /// <param name="session">Сессия, для которой происходит визуализация.
        /// Не может быть равен <c>null</c>.</param>
        /// <param name="extensionContainer">
        /// Контейнер расширений на визуализацию
        /// или <c>null</c>, если визуализация выполняется без расширений.
        /// </param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>
        /// <c>true</c>, если визуализация успешно выполнена;
        /// <c>false</c>, если визуализация не была выполнена, т.к. возникли ошибки.
        /// </returns>
        public static async Task<bool> VisualizeResolutionsAsync(
            IWfResolutionVisualizationGenerator generator,
            INodeLayout nodeLayout,
            INodeFactory nodeFactory,
            Card card,
            CardTask rootResolution,
            ISession session,
            IExtensionContainer extensionContainer = null,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(generator, nameof(generator));
            Check.ArgumentNotNull(nodeLayout, nameof(nodeLayout));
            Check.ArgumentNotNull(nodeFactory, nameof(nodeFactory));
            Check.ArgumentNotNull(card, nameof(card));
            Check.ArgumentNotNull(rootResolution, nameof(rootResolution));
            Check.ArgumentNotNull(session, nameof(session));

            ListStorage<CardTaskHistoryItem> taskHistory = card.TryGetTaskHistory();
            if (taskHistory == null || taskHistory.Count == 0)
            {
                return false;
            }

            Guid rootResolutionRowID = rootResolution.RowID;
            CardTaskHistoryItem rootHistoryItem = null;
            foreach (CardTaskHistoryItem historyItem in taskHistory)
            {
                if (historyItem.RowID == rootResolutionRowID)
                {
                    rootHistoryItem = historyItem;
                    break;
                }
            }

            if (rootHistoryItem == null)
            {
                return false;
            }

            await VisualizeResolutionsCoreAsync(generator, nodeLayout, nodeFactory, card, rootHistoryItem, rootResolution, extensionContainer, session, cancellationToken);
            return true;
        }


        /// <summary>
        /// Выполняет визуализацию резолюций, начиная от заданной записи в истории резолюций
        /// <paramref name="rootHistoryItem"/>.
        /// </summary>
        /// <param name="generator">
        /// Объект, посредством которого выполняется генерация узлов визуализации.
        /// Не может быть равен <c>null</c>.
        /// </param>
        /// <param name="nodeLayout">Макет визуализатора. Не может быть равен <c>null</c>.</param>
        /// <param name="nodeFactory">Фабрика узлов визуализатора. Не может быть равна <c>null</c>.</param>
        /// <param name="card">
        /// Карточка, в которой визуализируются резолюции. Не может быть равен <c>null</c>.
        /// </param>
        /// <param name="rootHistoryItem">
        /// Запись в истории резолюций, начиная от которой выполняется визуализация.
        /// Не может быть равна <c>null</c>.
        /// </param>
        /// <param name="session">Сессия, для которой происходит визуализация.
        /// Не может быть равен <c>null</c>.</param>
        /// <param name="extensionContainer">
        /// Контейнер расширений на визуализацию
        /// или <c>null</c>, если визуализация выполняется без расширений.
        /// </param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>
        /// <c>true</c>, если визуализация успешно выполнена;
        /// <c>false</c>, если визуализация не была выполнена, т.к. возникли ошибки.
        /// </returns>
        public static async Task<bool> VisualizeResolutionsAsync(
            IWfResolutionVisualizationGenerator generator,
            INodeLayout nodeLayout,
            INodeFactory nodeFactory,
            Card card,
            CardTaskHistoryItem rootHistoryItem,
            ISession session,
            IExtensionContainer extensionContainer = null,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(generator, nameof(generator));
            Check.ArgumentNotNull(nodeLayout, nameof(nodeLayout));
            Check.ArgumentNotNull(nodeFactory, nameof(nodeFactory));
            Check.ArgumentNotNull(card, nameof(card));
            Check.ArgumentNotNull(rootHistoryItem, nameof(rootHistoryItem));
            Check.ArgumentNotNull(session, nameof(session));

            ListStorage<CardTask> tasks = card.TryGetTasks();
            if (tasks == null || tasks.Count == 0)
            {
                return false;
            }

            Guid rootResolutionRowID = rootHistoryItem.RowID;
            CardTask rootResolution = null;
            foreach (CardTask task in tasks)
            {
                if (task.RowID == rootResolutionRowID)
                {
                    rootResolution = task;
                    break;
                }
            }

            await VisualizeResolutionsCoreAsync(generator, nodeLayout, nodeFactory, card, rootHistoryItem, rootResolution, extensionContainer, session, cancellationToken);
            return true;
        }

        /// <summary>
        /// Выполняет визуализацию резолюций, начиная от заданных записи в истории резолюций
        /// <paramref name="rootHistoryItem"/> и резолюции <paramref name="rootResolution"/>.
        /// </summary>
        /// <param name="generator">
        /// Объект, посредством которого выполняется генерация узлов визуализации.
        /// Не может быть равен <c>null</c>.
        /// </param>
        /// <param name="nodeLayout">Макет визуализатора. Не может быть равен <c>null</c>.</param>
        /// <param name="nodeFactory">Фабрика узлов визуализатора. Не может быть равна <c>null</c>.</param>
        /// <param name="card">
        /// Карточка, в которой визуализируются резолюции. Не может быть равен <c>null</c>.
        /// </param>
        /// <param name="rootHistoryItem">
        /// Запись в истории резолюций, начиная от которой выполняется визуализация.
        /// Не может быть равна <c>null</c>.
        /// </param>
        /// <param name="rootResolution">
        /// Задание резолюции, начиная от которого выполняется визуализация.
        /// Может быть равен <c>null</c>.
        /// </param>
        /// <param name="session">Сессия, для которой происходит визуализация.
        /// Не может быть равен <c>null</c>.</param>
        /// <param name="extensionContainer">
        /// Контейнер расширений на визуализацию
        /// или <c>null</c>, если визуализация выполняется без расширений.
        /// </param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        public static Task VisualizeResolutionsAsync(
            IWfResolutionVisualizationGenerator generator,
            INodeLayout nodeLayout,
            INodeFactory nodeFactory,
            Card card,
            CardTaskHistoryItem rootHistoryItem,
            CardTask rootResolution,
            ISession session,
            IExtensionContainer extensionContainer = null,
            CancellationToken cancellationToken = default) =>
            VisualizeResolutionsCoreAsync(
                generator ?? throw new ArgumentNullException(nameof(generator)),
                nodeLayout ?? throw new ArgumentNullException(nameof(nodeLayout)),
                nodeFactory ?? throw new ArgumentNullException(nameof(nodeFactory)),
                card ?? throw new ArgumentNullException(nameof(card)),
                rootHistoryItem ?? throw new ArgumentNullException(nameof(rootHistoryItem)),
                rootResolution,
                extensionContainer,
                session ?? throw new ArgumentNullException(nameof(session)),
                cancellationToken);


        private static Task VisualizeResolutionsCoreAsync(
            IWfResolutionVisualizationGenerator generator,
            INodeLayout nodeLayout,
            INodeFactory nodeFactory,
            Card card,
            CardTaskHistoryItem rootHistoryItem,
            CardTask rootResolution,
            IExtensionContainer extensionContainer,
            ISession session,
            CancellationToken cancellationToken = default)
        {
            var visitor = new WfResolutionVisualizationVisitor(nodeLayout, nodeFactory, card, extensionContainer);
            var utcNow = DateTime.UtcNow;
            
            return visitor.VisitAsync(generator, rootHistoryItem, rootResolution, session, utcNow, cancellationToken);
        }

        #endregion
    }
}
