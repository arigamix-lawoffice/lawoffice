using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.ComponentModel;
using Tessa.Cards.Metadata;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using Tessa.Test.Default.Shared;
using Tessa.Test.Default.Shared.Cards;

namespace Tessa.Test.Default.Shared.Kr.Routes
{
    /// <summary>
    /// Предоставляет методы для создания и настройки этапов подсистемы маршрутов.
    /// </summary>
    public sealed class RouteBuilder :
        PendingActionsProvider<IPendingAction, RouteBuilder>
    {
        #region Fields

        private readonly ICardMetadata cardMetadata;

        private readonly Func<Card> getCardFunc;

        private readonly bool isClientSide;

        private StringDictionaryStorage<CardRow> sectionRows;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр объекта <see cref="RouteBuilder"/>.
        /// </summary>
        /// <param name="clc"><inheritdoc cref="ICardLifecycleCompanionDependencies" path="/summary"/></param>
        public RouteBuilder(
            ICardLifecycleCompanion clc)
        {
            ThrowIfNull(clc);

            this.getCardFunc = clc.GetCardOrThrow;
            this.cardMetadata = clc.Dependencies.CardMetadata;
            this.isClientSide = !clc.Dependencies.ServerSide;
        }

        /// <summary>
        /// Инициализирует новый экземпляр объекта <see cref="RouteBuilder"/>.
        /// </summary>
        /// <param name="clc"><inheritdoc cref="ICardLifecycleCompanionDependencies" path="/summary"/></param>
        /// <param name="cardMetadata"><inheritdoc cref="ICardMetadata" path="/summary"/></param>
        public RouteBuilder(
            ICardLifecycleCompanion clc,
            ICardMetadata cardMetadata)
        {
            ThrowIfNull(clc);
            ThrowIfNull(cardMetadata);

            this.getCardFunc = clc.GetCardOrThrow;
            this.cardMetadata = cardMetadata;
            this.isClientSide = !clc.Dependencies.ServerSide;
        }

        /// <summary>
        /// Инициализирует новый экземпляр объекта <see cref="RouteBuilder"/>.
        /// </summary>
        /// <param name="card">Карточка в которой необходимо выполнить создание маршрута.</param>
        /// <param name="cardMetadata"><inheritdoc cref="ICardMetadata" path="/summary"/></param>
        /// <param name="isClientSide">Значение <see langword="true"/>, если объект используется в клиентских тестах или <see langword="false"/>, если в серверных.</param>
        public RouteBuilder(
            Card card,
            ICardMetadata cardMetadata,
            bool isClientSide)
        {
            ThrowIfNull(card);
            ThrowIfNull(cardMetadata);

            this.getCardFunc = () => card;
            this.cardMetadata = cardMetadata;
            this.isClientSide = isClientSide;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Возвращает карточку в которой выполняется настройка маршрута.
        /// </summary>
        public Card Card => this.getCardFunc();

        #endregion

        #region Public methods

        /// <summary>
        /// Добавляет новый этап.
        /// </summary>
        /// <param name="name">Название этапа.</param>
        /// <param name="descriptor">Дескриптор типа этапа.</param>
        /// <param name="group">Дескриптор описывающий группу этапов. Должен быть задан, если карточка относится к типу содержащему выполняющийся маршрут.</param>
        /// <param name="order">Порядковый номер этапа. Используется, если карточка относится к типу содержащему шаблоны этапов.</param>
        /// <param name="modifyAction">Предикат выполняющий изменение строки, содержащей информацию о новом этапе.</param>
        /// <exception cref="InvalidOperationException">Stage with name "<paramref name="name"/>" already exists.</exception>
        /// <returns>Объект <see cref="RouteBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public RouteBuilder AddStage(
            string name,
            StageTypeDescriptor descriptor,
            KrStageGroupDescriptor group = default,
            int order = int.MaxValue,
            Action<CardRow, Card> modifyAction = default)
        {
            Check.ArgumentNotNull(name, nameof(name));
            Check.ArgumentNotNull(descriptor, nameof(descriptor));

            if (!KrProcessSharedHelper.DesignTimeCard(this.Card.TypeID))
            {
                Check.ArgumentNotNull(group, nameof(group));
            }

            return this.ApplyAction(async (builder, action, ct) =>
            {
                var rows = builder.GetStages();
                int effectiveOrder;
                int? originalOrder;
                if (KrProcessSharedHelper.DesignTimeCard(builder.Card.TypeID))
                {
                    effectiveOrder = order != int.MaxValue
                        ? order
                        : rows.Count;
                    originalOrder = null;
                }
                else
                {
                    effectiveOrder = KrProcessSharedHelper.ComputeStageOrder(
                        group.ID,
                        group.Order,
                        rows);
                    originalOrder = effectiveOrder;
                }

                if (rows.Any(p => StageRowPredicate(p, name)))
                {
                    return ValidationResult.FromText(
                        $"Stage with name \"{name}\" already exists.",
                        ValidationResultType.Error);
                }

                var row = rows.Add();
                var emptyRow = (await builder.GetSectionRows(ct))[KrConstants.KrStages.Virtual].Clone();
                row.Set(emptyRow);

                row.RowID = Guid.NewGuid();
                row.State = CardRowState.Inserted;
                row.Fields[KrConstants.KrStages.NameField] = name;

                var state = KrStageState.Inactive;
                row.Fields[KrConstants.KrStages.StateID] = Int32Boxes.Box(state.ID);
                row.Fields[KrConstants.KrStages.StateName] = await builder.cardMetadata.GetStageStateNameAsync(
                    state,
                    ct);

                row.Fields[KrConstants.KrStages.StageTypeID] = descriptor.ID;
                row.Fields[KrConstants.KrStages.StageTypeCaption] = descriptor.Caption;

                if (group is not null)
                {
                    SetStageGroupInternal(row, group);
                }

                row.Fields[KrConstants.KrStages.Order] = Int32Boxes.Box(effectiveOrder);

                if (originalOrder.HasValue)
                {
                    row[KrConstants.KrStages.OriginalOrder] = Int32Boxes.Box(originalOrder);
                }

                IncrementBottomStagesOrder(row, rows);
                modifyAction?.Invoke(row, builder.Card);

                return ValidationResult.Empty;
            },
            nameof(RouteBuilder) + "." + nameof(AddStage));
        }

        /// <summary>
        /// Возвращает дескриптор группы этапов сформированный по данным хранящимся в строке этапа.
        /// </summary>
        /// <param name="row">Строка содержащая информацию по этапу.</param>
        /// <returns>Дескриптор группы этапов.</returns>
        public static KrStageGroupDescriptor GetStageGroup(
            CardRow row)
        {
            Check.ArgumentNotNull(row, nameof(row));

            var fields = row.Fields;
            var stageGroupID = fields.Get<Guid>(KrConstants.KrStages.StageGroupID);
            var stageGroupName = fields.Get<string>(KrConstants.KrStages.StageGroupName);
            var stageGroupOrder = fields.Get<int>(KrConstants.KrStages.StageGroupOrder);

            return new KrStageGroupDescriptor(stageGroupID, stageGroupName, stageGroupOrder);
        }

        /// <summary>
        /// Задаёт информацию по группе этапов в строку содержащую информацию по этапу.
        /// </summary>
        /// <param name="row">Строка содержащая информацию по этапу.</param>
        /// <param name="groupDescriptor">Дескриптор группы этапов.</param>
        public static void SetStageGroup(
            CardRow row,
            KrStageGroupDescriptor groupDescriptor)
        {
            Check.ArgumentNotNull(row, nameof(row));
            Check.ArgumentNotNull(groupDescriptor, nameof(groupDescriptor));

            SetStageGroupInternal(row, groupDescriptor);
        }

        /// <summary>
        /// Изменяет значение поля в последней строке, содержащей информацию о этапе.
        /// </summary>
        /// <param name="field">Название поля.</param>
        /// <param name="value">Значение.</param>
        /// <returns>Объект <see cref="RouteBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public RouteBuilder ModifyStageField(
            string field,
            object value)
        {
            return this.ApplyAction(
                (builder, _, ct) =>
                {
                    var lastRow = builder.GetStages().LastOrDefault();

                    if (lastRow is null)
                    {
                        return ValueTask.FromResult(ValidationResult.FromText(
                            builder,
                            "Card doesn't contain stages.",
                            ValidationResultType.Error));
                    }

                    lastRow.Fields[field] = value;

                    return ValueTask.FromResult(ValidationResult.Empty);
                },
                nameof(RouteBuilder) + "." + nameof(ModifyStages));
        }

        /// <summary>
        /// Изменяет значение поля в строке, содержащей информацию о этапе имеющем указанное название.
        /// </summary>
        /// <param name="name">Название этапа, в строке которого требуется изменить значение поля.</param>
        /// <param name="field">Название поля.</param>
        /// <param name="value">Значение.</param>
        /// <returns>Объект <see cref="RouteBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public RouteBuilder ModifyStageField(
            string name,
            string field,
            object value)
        {
            return this.ModifyStage(name, (r, _) =>
            {
                r.Fields[field] = value;

                return ValueTask.CompletedTask;
            });
        }

        /// <summary>
        /// Применяет указанное действие к строке, содержащей информацию о этапе имеющем указанное название.
        /// </summary>
        /// <param name="name">Название этапа.</param>
        /// <param name="modifyActionAsync">Действие выполняющее модификацию строки, содержащей информацию об этапе.</param>
        /// <returns>Объект <see cref="RouteBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public RouteBuilder ModifyStage(
            string name,
            Func<CardRow, CancellationToken, ValueTask> modifyActionAsync)
        {
            this.ModifyStages(r => StageRowPredicate(r, name), modifyActionAsync);
            return this;
        }

        /// <summary>
        /// Применяет указанное действие к строке, содержащей информацию о этапе, удовлетворяющей заданному условию.
        /// </summary>
        /// <param name="filterPredicate">Поисковое условие.</param>
        /// <param name="modifyActionAsync">Действие выполняющее модификацию строки, содержащей информацию об этапе.</param>
        /// <returns>Объект <see cref="RouteBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public RouteBuilder ModifyStages(
            Func<CardRow, bool> filterPredicate,
            Func<CardRow, CancellationToken, ValueTask> modifyActionAsync)
        {
            Check.ArgumentNotNull(filterPredicate, nameof(filterPredicate));
            Check.ArgumentNotNull(modifyActionAsync, nameof(modifyActionAsync));

            this.ApplyAction(
                async (builder, _, ct) =>
                {
                    foreach (var row in builder.GetStages())
                    {
                        if (filterPredicate(row))
                        {
                            await modifyActionAsync(row, ct);
                        }
                    }

                    return ValidationResult.Empty;
                },
                nameof(RouteBuilder) + "." + nameof(ModifyStages));

            return this;
        }

        /// <summary>
        /// Для последнего этапа в списке этапов создаёт объект позволяющий модифицировать его исполнителей.
        /// </summary>
        /// <returns>Объект <see cref="PerformerBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Для возврата к текущему конфигуратору используйте метод <see cref="IConfiguratorScopeManager{T}.Complete"/>.<para/>
        /// Все запланированные действия будут выполнены при выполнении запланированных действий текущего конфигуратора.
        /// </remarks>
        public PerformerBuilder ModifyPerformers()
        {
            return this.RegisterPendingActionsProducer(
                new PerformerBuilder(
                    this,
                    static card => card.GetStagesSection(true).Rows.LastOrDefault()));
        }

        /// <summary>
        /// Для этапа с указанным именем создаёт объект позволяющий модифицировать его исполнителей.
        /// </summary>
        /// <param name="stageName">Название этапа.</param>
        /// <returns>Объект <see cref="PerformerBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Для возврата к текущему конфигуратору используйте метод <see cref="IConfiguratorScopeManager{T}.Complete"/>.<para/>
        /// Все запланированные действия будут выполнены при выполнении запланированных действий текущего конфигуратора.
        /// </remarks>
        public PerformerBuilder ModifyPerformers(string stageName) =>
            this.ModifyPerformers(p => StageRowPredicate(p, stageName));

        /// <summary>
        /// Для первого этапа, удовлетворяющего указанному предикату, создаёт объект позволяющий модифицировать его исполнителей.
        /// </summary>
        /// <param name="predicate">Предикат определяющий строку этапа для которого требуется создать <see cref="PerformerBuilder"/>.</param>
        /// <returns>Объект <see cref="PerformerBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Для возврата к текущему конфигуратору используйте метод <see cref="IConfiguratorScopeManager{T}.Complete"/>.<para/>
        /// Все запланированные действия будут выполнены при выполнении запланированных действий текущего конфигуратора.
        /// </remarks>
        public PerformerBuilder ModifyPerformers(Func<CardRow, bool> predicate)
        {
            return this.RegisterPendingActionsProducer(
                new PerformerBuilder(
                    this,
                    card => card.GetStagesSection(true).Rows.FirstOrDefault(predicate)));
        }

        /// <summary>
        /// Удаляет этап с заданным названием.
        /// </summary>
        /// <param name="name">Название удаляемого этапа.</param>
        /// <returns>Объект <see cref="RouteBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Метод выполняет пропуск этапа вместо удаления, если это возможно. Поведение аналогично удалению на клиентах.<para/>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public RouteBuilder RemoveStage(
            string name)
        {
            return this.RemoveStages(
                p => StageRowPredicate(p, name));
        }

        /// <summary>
        /// Удаляет этап удовлетворяющий указанному условию.
        /// </summary>
        /// <param name="filterPredicate">Условие в соответствии с котором выполняется удаление этапов.</param>
        /// <returns>Объект <see cref="RouteBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Метод выполняет пропуск этапа вместо удаления, если это возможно. Поведение аналогично удалению на клиентах.<para/>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public RouteBuilder RemoveStages(
            Func<CardRow, bool> filterPredicate)
        {
            ThrowIfNull(filterPredicate);

            this.ApplyAction(
                async (builder, _, ct) =>
                {
                    var section = builder.Card.GetStagesSection(true);
                    var sectionName = section.Name;
                    ICardMetadataBinder cardMetadataBinder = null;

                    var rows = section.Rows;

                    for (var i = 0; i < rows.Count; i++)
                    {
                        var row = rows[i];

                        if (filterPredicate(row))
                        {
                            // Обработка пропуска этапов должна выполняться до применения к карточке метода Card.RemoveAllButChanged,
                            // иначе для строк в состоянии CardRowState.Deleted будет удалена информацию о всех изменения.
                            // На клиенте аналогичная логика выполняется в KrStageUIExtension.RowInvokedSkipStage.
                            if (!KrProcessSharedHelper.SkipStage(row))
                            {
                                cardMetadataBinder ??= new CardMetadataBinder(builder.Card, builder.cardMetadata);
                                await cardMetadataBinder.RemoveRowAsync(
                                    sectionName,
                                    row,
                                    ct);
                            }
                        }
                    }

                    TestCardHelper.RepairCardRowOrders<int>(rows);

                    return ValidationResult.Empty;
                },
                nameof(RouteBuilder) + "." + nameof(RemoveStages));

            return this;
        }

        /// <summary>
        /// Возвращает строку, содержащую информацию о этапе.
        /// </summary>
        /// <param name="stageName">Название этапа.</param>
        /// <returns>Строка, содержащая информацию о этапе или значение <see langword="null"/>, если она не найдена.</returns>
        public CardRow GetStage(
            string stageName) =>
            this.GetStages()
                .FirstOrDefault(i => StageRowPredicate(i, stageName));

        /// <summary>
        /// Возвращает строки, содержащие информацию о этапах.
        /// </summary>
        /// <returns>Строки, содержащие информацию о этапах.</returns>
        public ListStorage<CardRow> GetStages() =>
            this.Card.GetStagesSection(true).Rows;

        #endregion

        #region Private methods

        private static void IncrementBottomStagesOrder(
            CardRow row,
            ListStorage<CardRow> rows)
        {
            var order = row.Fields.Get<int>(KrConstants.KrStages.Order);

            foreach (var r in rows)
            {
                if (!ReferenceEquals(row, r)
                    && order <= r.Get<int>(KrConstants.KrStages.Order))
                {
                    r.Fields[KrConstants.KrStages.Order] = Int32Boxes.Box(r.Fields.Get<int>(KrConstants.KrStages.Order) + 1);
                }
            }
        }

        private static bool StageRowPredicate(CardRow stageRow, string stageName) =>
            string.Equals(stageRow.Get<string>(KrConstants.KrStages.NameField), stageName, StringComparison.Ordinal);

        private static void SetStageGroupInternal(
            CardRow row,
            KrStageGroupDescriptor groupDescriptor)
        {
            row.Fields[KrConstants.KrStages.StageGroupID] = groupDescriptor.ID;
            row.Fields[KrConstants.KrStages.StageGroupName] = groupDescriptor.Name;
            row.Fields[KrConstants.KrStages.StageGroupOrder] = Int32Boxes.Box(groupDescriptor.Order);
        }

        private async ValueTask<StringDictionaryStorage<CardRow>> GetSectionRows(CancellationToken cancellationToken = default)
        {
            if (this.sectionRows is not null)
            {
                return this.sectionRows;
            }

            var cardNewContext = new CardNewContext(this.Card.TypeID, CardNewMode.Default, this.cardMetadata);
            return this.sectionRows = await CardNewStrategy.Default.CreateSectionRowsAsync(cardNewContext, cancellationToken);
        }

        #endregion
    }
}
