using System;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;
using Tessa.Platform.Validation;
using Tessa.Test.Default.Shared.Cards;

namespace Tessa.Test.Default.Shared.Kr.Routes
{
    /// <summary>
    /// Предоставляет методы для управления исполнителями в этапе подсистемы маршрутов.
    /// </summary>
    public sealed class PerformerBuilder :
        PendingActionsProvider<IPendingAction, PerformerBuilder>,
        IConfiguratorScopeManager<RouteBuilder>
    {
        #region Fields

        private readonly ConfiguratorScopeManager<RouteBuilder> configuratorScopeManager;

        private readonly Func<Card, CardRow> getStageRowFunc;

        private readonly Func<Card, CardSection> getPerformersSectionFunc;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="PerformerBuilder"/>.
        /// </summary>
        /// <param name="stageBuidler">Объект, выполняющий создание и настройку этапа для которого требуется выполнить конфигурирование исполнителей.</param>
        /// <param name="getStageRowFunc">Метод возвращающий строку, содержащую информацию о конфигурируемом этапе.</param>
        /// <param name="getPerformersSectionFunc">Метод возвращающий секцию, содержащую информацию об исполнителях. Если не задан, то используется секция <see cref="KrConstants.KrPerformersVirtual.Synthetic"/>.</param>
        public PerformerBuilder(
            RouteBuilder stageBuidler,
            Func<Card, CardRow> getStageRowFunc,
            Func<Card, CardSection> getPerformersSectionFunc = null)
        {
            Check.ArgumentNotNull(getStageRowFunc, nameof(getStageRowFunc));

            this.configuratorScopeManager = new ConfiguratorScopeManager<RouteBuilder>(stageBuidler);

            this.getStageRowFunc = getStageRowFunc;
            this.getPerformersSectionFunc = getPerformersSectionFunc ?? new Func<Card, CardSection>(static card => card.Sections[KrConstants.KrPerformersVirtual.Synthetic]);
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Добавляет указанную роль в качестве исполнителя.
        /// </summary>
        /// <param name="roleID">Идентификатор роли.</param>
        /// <param name="roleName">Имя роли.</param>
        /// <returns>Объект <see cref="PerformerBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public PerformerBuilder SetSinglePerformer(Guid roleID, string roleName)
        {
            this.AddPendingAction(
                new PendingAction(
                    $"{nameof(PerformerBuilder)}.{nameof(SetSinglePerformer)}({roleID}, {roleName})",
                    (_, _) =>
                    {
                        var stageRow = this.GetStageRow();

                        stageRow[KrConstants.KrSinglePerformerVirtual.PerformerID] = roleID;
                        stageRow[KrConstants.KrSinglePerformerVirtual.PerformerName] = roleName;
                        return ValueTask.FromResult(ValidationResult.Empty);
                    }));

            return this;
        }

        /// <summary>
        /// Добавляет указанную роль в качестве исполнителя.
        /// </summary>
        /// <param name="roleID">Идентификатор роли.</param>
        /// <param name="roleName">Имя роли.</param>
        /// <param name="order">Порядковый номер строки. Используйте значение <see cref="int.MaxValue"/> для добавления строки в конец списка.</param>
        /// <returns>Объект <see cref="PerformerBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Порядковые номера строк автоматически исправляются. Исходный порядок строк, имеющих одинаковые порядковые номера, сохраняется.<para/>
        /// 
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public PerformerBuilder AddPerformer(Guid roleID, string roleName, int order = int.MaxValue)
        {
            this.AddPendingAction(
                new PendingAction(
                    $"{nameof(PerformerBuilder)}.{nameof(AddPerformer)}({roleID}, {roleName}, {order})",
                    (builder, _) =>
                    {
                        var rows = this.GetPerformersSection().Rows;
                        var count = rows.Count;

                        var newRow = rows.Add();
                        newRow.RowID = Guid.NewGuid();
                        newRow.State = CardRowState.Inserted;
                        newRow.Fields[KrConstants.KrPerformersVirtual.PerformerID] = roleID;
                        newRow.Fields[KrConstants.KrPerformersVirtual.PerformerName] = roleName;
                        newRow.Fields[KrConstants.KrPerformersVirtual.StageRowID] = this.GetStageRow().RowID;
                        newRow.Fields[KrConstants.KrPerformersVirtual.Order] = Int32Boxes.Box(order == int.MaxValue ? count : order);

                        TestCardHelper.RepairCardRowOrders<int>(rows);

                        return ValueTask.FromResult(ValidationResult.Empty);
                    }));

            return this;
        }

        /// <summary>
        /// Удаляет всех исполнителей отвечающих указанному предикату.
        /// </summary>
        /// <param name="predicate">Предикат в соответствии с которым выполняется удаление исполнителей. Параметры: строка, содержащая информацию об исполнителе.</param>
        /// <returns>Объект <see cref="PerformerBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public PerformerBuilder RemovePerformers(Func<CardRow, bool> predicate)
        {
            Check.ArgumentNotNull(predicate, nameof(predicate));

            this.AddPendingAction(
                new PendingAction(
                    $"{nameof(PerformerBuilder)}.{nameof(RemovePerformers)}",
                    (builder, _) =>
                    {
                        var rows = this.GetPerformersSection().Rows;

                        TestCardHelper.RemoveRows(rows, predicate);
                        TestCardHelper.RepairCardRowOrders<int>(rows);

                        return ValueTask.FromResult(ValidationResult.Empty);
                    }));

            return this;
        }

        #endregion

        #region IConfiguratorScopeManager<T> Members

        /// <inheritdoc/>
        public RouteBuilder Complete() =>
            this.configuratorScopeManager.Complete();

        #endregion

        #region Private Methods

        private CardRow GetStageRow()
        {
            return this.getStageRowFunc(this.configuratorScopeManager.Scope.Card)
                ?? throw new InvalidOperationException(nameof(this.getStageRowFunc) + " don't return stage row.");
        }

        private CardSection GetPerformersSection()
        {
            return this.getPerformersSectionFunc(this.configuratorScopeManager.Scope.Card)
                ?? throw new InvalidOperationException(nameof(this.getPerformersSectionFunc) + " don't return performers section.");
        }

        #endregion
    }
}
