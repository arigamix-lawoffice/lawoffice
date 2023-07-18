using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tessa.Platform;
using Tessa.Platform.Storage;

namespace Tessa.Test.Default.Shared.Kr
{
    /// <summary>
    /// Выполняет построение трассировочной информации.
    /// </summary>
    public sealed class PendingActionTrace
    {
        #region Constants

        private const int DefaultCapacity = 5;

        private const int DefaultLineLength = 20;

        #endregion

        #region Fields

        private readonly IList<IPendingAction> pendingActions;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр объекта c начальной ёмкостью внутреннего хранилища по умолчанию.
        /// </summary>
        public PendingActionTrace()
            : this(DefaultCapacity)
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр объекта с заданной начальной ёмкостью.
        /// </summary>
        /// <param name="capacity">Начальная ёмкость внутреннего хранилища.</param>
        public PendingActionTrace(int capacity)
        {
            this.pendingActions = new List<IPendingAction>(capacity);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Добавляет новое отложенное действие для включения его в трассировочную информацию.
        /// </summary>
        /// <param name="pendingAction">Добавляемое отложенное действие.</param>
        public void Add(IPendingAction pendingAction)
        {
            Check.ArgumentNotNull(pendingAction, nameof(pendingAction));

            this.pendingActions.Add(pendingAction);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            var pendingActionsCount = this.pendingActions.Count;

            if (pendingActionsCount == 0)
            {
                return string.Empty;
            }

            var sb = StringBuilderHelper.Acquire(pendingActionsCount * DefaultLineLength);

            AppendActions(sb, this.pendingActions, 0);

            return sb.ToString();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Добавляет информацию о действиях.
        /// </summary>
        /// <param name="sb">Объект, выполняющий построение строки с трассировкой.</param>
        /// <param name="pendingActions">Перечисление содержащее отложенные действия информацию о которых требуется добавить.</param>
        /// <param name="indent">Отступ строк содержащих информацию.</param>
        private static void AppendActions(
            StringBuilder sb,
            IEnumerable<IPendingAction> pendingActions,
            int indent)
        {
            int index = default;

            foreach (var pendingAction in pendingActions)
            {
                AppendAction(sb, ++index, pendingAction, indent);
            }
        }

        /// <summary>
        /// Добавляет информацию о действии.
        /// </summary>
        /// <param name="sb">Объект, выполняющий построение строки с трассировкой.</param>
        /// <param name="index">Порядковый индекс обрабатываемого действия.</param>
        /// <param name="pendingAction">Обрабатываемое действие.</param>
        /// <param name="indent">Отступ строк содержащих информацию.</param>
        private static void AppendAction(
            StringBuilder sb,
            int index,
            IPendingAction pendingAction,
            int indent)
        {
            const string formatDisplayName = "Serial number {0}: {1}";

            sb
                .AppendIndent(indent)
                .AppendFormat(formatDisplayName, index.ToString(), pendingAction.Name)
                .AppendLine();

            AppendActionDetails(sb, pendingAction, indent);
        }

        /// <summary>
        /// Добавляет подробную информацию о действии.
        /// </summary>
        /// <param name="sb">Объект, выполняющий построение строки с трассировкой.</param>
        /// <param name="pendingAction">Обрабатываемое действие.</param>
        /// <param name="indent">Отступ строк содержащих информацию.</param>
        private static void AppendActionDetails(
            StringBuilder sb,
            IPendingAction pendingAction,
            int indent)
        {
            const string nameValueSeparator = ":";

            if (pendingAction.Info.Any())
            {
                sb
                    .AppendIndent(indent)
                    .Append(nameof(PendingAction.Info)).AppendLine(nameValueSeparator);

                StorageHelper.Print(sb, pendingAction.Info);

                sb.AppendLine();
            }

            if (pendingAction.PreparationActions.Any())
            {
                sb
                    .AppendIndent(indent)
                    .Append(nameof(PendingAction.PreparationActions)).AppendLine(nameValueSeparator);

                AppendActions(sb, pendingAction.PreparationActions, indent + 1);
            }

            if (pendingAction.AfterActions.Any())
            {
                sb
                    .AppendIndent(indent)
                    .Append(nameof(PendingAction.AfterActions)).AppendLine(nameValueSeparator);

                AppendActions(sb, pendingAction.AfterActions, indent + 1);
            }
        }

        #endregion
    }
}
