using System;
using System.Threading;
using NUnit.Framework;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow;
using Tessa.Extensions.Default.Shared.Workflow.KrCompilers;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;
using Tessa.Platform.Collections;
using Tessa.Platform.Validation;

namespace Tessa.Test.Default.Shared.Kr.Routes
{
    /// <summary>
    /// Предоставляет методы для создания и модификации карточки шаблонов этапов.
    /// </summary>
    public sealed class KrStageTemplateBuilder :
        CardLifecycleCompanion<KrStageTemplateBuilder>,
        INamedEntry
    {
        #region Constructor

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="KrStageTemplateBuilder"/>.
        /// </summary>
        /// <param name="deps"><inheritdoc cref="ICardLifecycleCompanionDependencies" path="/summary"/></param>
        public KrStageTemplateBuilder(
            ICardLifecycleCompanionDependencies deps)
            : this(Guid.NewGuid(), deps)
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="KrStageTemplateBuilder"/>.
        /// </summary>
        /// <param name="cardID">Идентификатор карточки.</param>
        /// <param name="deps"><inheritdoc cref="ICardLifecycleCompanionDependencies" path="/summary"/></param>
        public KrStageTemplateBuilder(
            Guid cardID,
            ICardLifecycleCompanionDependencies deps)
            : base(cardID, DefaultCardTypes.KrStageTemplateTypeID, DefaultCardTypes.KrStageTemplateTypeName, deps)
        {
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Устанавливает название шаблона этапов.
        /// </summary>
        /// <param name="value">Название шаблона этапов.</param>
        /// <returns>Объект <see cref="KrStageTemplateBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public KrStageTemplateBuilder SetName(string value) =>
            this.SetField(KrConstants.KrStageTemplates.NameField, value);

        /// <summary>
        /// Возвращает название шаблона этапов.
        /// </summary>
        /// <returns>Название шаблона этапов.</returns>
        public string GetName() =>
            this.TryGetField<string>(KrConstants.KrStageTemplates.NameField);

        /// <summary>
        /// Устанавливает порядковый номер шаблона этапов.
        /// </summary>
        /// <param name="value">Порядковый номер шаблона этапов.</param>
        /// <returns>Объект <see cref="KrStageTemplateBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public KrStageTemplateBuilder SetOrder(int value) =>
            this.SetField(KrConstants.KrStageTemplates.Order, Int32Boxes.Box(value));

        /// <summary>
        /// Возвращает порядковый номер шаблона этапов.
        /// </summary>
        /// <returns>Порядковый номер шаблона этапов.</returns>
        public int GetOrder() =>
            this.TryGetField<int>(KrConstants.KrStageTemplates.Order);

        /// <summary>
        /// Устанавливает позицию шаблона этапов относительно этапов, добавленных вручную.
        /// </summary>
        /// <param name="value"><inheritdoc cref="GroupPosition" path="/summary"/></param>
        /// <returns>Объект <see cref="KrStageTemplateBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public KrStageTemplateBuilder SetGroupPosition(GroupPosition value)
        {
            ThrowIfNull(value);

            this.SetField(KrConstants.KrStageTemplates.GroupPositionID, Int32Boxes.Box(value.ID))
                .SetField(KrConstants.KrStageTemplates.GroupPositionName, value.Name);

            return this;
        }

        /// <summary>
        /// Возвращает позицию шаблона этапов относительно этапов, добавленных вручную.
        /// </summary>
        /// <returns><inheritdoc cref="GroupPosition" path="/summary"/></returns>
        public GroupPosition GetGroupPosition() =>
            GroupPosition.GetByID(this.TryGetField<int?>(KrConstants.KrStageTemplates.GroupPositionID));

        /// <summary>
        /// Устанавливает значение, показывающее, разрешено ли изменять порядок этапов.
        /// </summary>
        /// <param name="value">Значение <see langword="true"/>, если разрешено изменять порядок этапов, иначе - <see langword="false"/>.</param>
        /// <returns>Объект <see cref="KrStageTemplateBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public KrStageTemplateBuilder SetCanChangeOrder(bool value) =>
            this.SetField(KrConstants.KrStageTemplates.CanChangeOrder, BooleanBoxes.Box(value));

        /// <summary>
        /// Возвращает значение, показывающее, разрешено ли изменять порядок этапов.
        /// </summary>
        /// <returns>Значение <see langword="true"/>, если разрешено изменять порядок этапов, иначе - <see langword="false"/>.</returns>
        public bool GetCanChangeOrder() =>
            this.TryGetField<bool>(KrConstants.KrStageTemplates.CanChangeOrder);

        /// <summary>
        /// Устанавливает значение, показывающее, можно ли редактировать этапы.
        /// </summary>
        /// <param name="value">Значение <see langword="true"/>, если этапы нередактируемые, иначе - <see langword="false"/>.</param>
        /// <returns>Объект <see cref="KrStageTemplateBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public KrStageTemplateBuilder SetStagesReadonly(bool value) =>
            this.SetField(KrConstants.KrStageTemplates.IsStagesReadonly, BooleanBoxes.Box(value));

        /// <summary>
        /// Возвращает значение, показывающее, можно ли редактировать этапы.
        /// </summary>
        /// <returns>Значение <see langword="true"/>, если этапы нередактируемые, иначе - <see langword="false"/>.</returns>
        public bool GetStagesReadonly() =>
            this.TryGetField<bool>(KrConstants.KrStageTemplates.IsStagesReadonly);

        /// <summary>
        /// Задаёт группу этапов.
        /// </summary>
        /// <param name="id">Идентификатор группы этапов.</param>
        /// <param name="name">Название группы этапов.</param>
        /// <returns>Объект <see cref="KrStageTemplateBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public KrStageTemplateBuilder SetStageGroup(
            Guid id,
            string name)
        {
            ThrowIfNullOrWhiteSpace(name);

            return this
                .SetField(KrConstants.KrStageTemplates.StageGroupID, id)
                .SetField(KrConstants.KrStageTemplates.StageGroupName, name);
        }

        /// <summary>
        /// Возвращает дескриптор группы этапов.
        /// </summary>
        /// <returns>Дескриптор группы этапов или значение по умолчанию для типа, если группа этапов не задана. Свойство <see cref="KrStageGroupDescriptor.Order"/> всегда имеет значение 0, т.к. порядок группы этапов не хранится в карточке шаблона этапов.</returns>
        public KrStageGroupDescriptor GetStageGroup()
        {
            var stageGroupID = this.TryGetField<Guid?>(KrConstants.KrStageTemplates.StageGroupID);

            if (!stageGroupID.HasValue)
            {
                return default;
            }

            var stageGroupName = this.TryGetField<string>(KrConstants.KrStageTemplates.StageGroupName);

            return new KrStageGroupDescriptor(
                stageGroupID.Value,
                stageGroupName,
                0);
        }

        /// <summary>
        /// Задаёт указанный тип документа или карточки в качестве ограничения при пересчёте.
        /// </summary>
        /// <param name="typeID">Идентификатор типа документа или карточки.</param>
        /// <param name="typeName">Имя типа документа или карточки.</param>
        /// <param name="isDocType">Значение <see langword="true"/>, если указанный тип является типом документа, иначе - <see langword="false"/>, типом карточки.</param>
        /// <returns>Объект <see cref="KrStageTemplateBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public KrStageTemplateBuilder ForType(
            Guid typeID,
            string typeName,
            bool isDocType)
        {
            this.ApplyAction(
                (clc, action) =>
                {
                    var row = clc.GetCardOrThrow().Sections[KrConstants.KrStageTypes.Name].Rows.Add();
                    row.RowID = Guid.NewGuid();
                    row["TypeID"] = typeID;
                    row["TypeCaption"] = typeName;
                    row["TypeIsDocType"] = BooleanBoxes.Box(isDocType);
                    row.State = CardRowState.Inserted;
                },
                name: $"{nameof(KrStageGroupBuilder)}.{nameof(ForType)}");

            return this;
        }

        /// <summary>
        /// Задаёт указанный карточки в качестве ограничения при пересчёте.
        /// </summary>
        /// <param name="typeID">Идентификатор типа карточки.</param>
        /// <param name="typeName">Имя типа карточки.</param>
        /// <returns>Объект <see cref="KrStageTemplateBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public KrStageTemplateBuilder ForCardType(
            Guid typeID,
            string typeName) => this.ForType(typeID, typeName, false);

        /// <summary>
        /// Задаёт указанный тип документа в качестве ограничения при пересчёте.
        /// </summary>
        /// <param name="typeID">Идентификатор типа документа.</param>
        /// <param name="typeName">Имя типа документа.</param>
        ///<returns>Объект <see cref="KrStageTemplateBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public KrStageTemplateBuilder ForDocType(
            Guid typeID,
            string typeName) => this.ForType(typeID, typeName, true);

        /// <summary>
        /// Задаёт указанную роль в качестве ограничения при пересчёте.
        /// </summary>
        /// <param name="roleID">Идентификатор роли.</param>
        /// <param name="roleName">Имя роли.</param>
        /// <returns>Объект <see cref="KrStageTemplateBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public KrStageTemplateBuilder ForRole(
            Guid roleID,
            string roleName)
        {
            this.ApplyAction(
                (clc, action) =>
                {
                    var row = clc.GetCardOrThrow().Sections[KrConstants.KrStageRoles.Name].Rows.Add();
                    row.RowID = Guid.NewGuid();
                    row[KrConstants.KrStageRoles.RoleID] = roleID;
                    row[KrConstants.KrStageRoles.RoleName] = roleName;
                    row.State = CardRowState.Inserted;
                },
                name: $"{nameof(KrStageGroupBuilder)}.{nameof(ForType)}");

            return this;
        }

        /// <summary>
        /// Задаёт сценарий с условием включения шаблона этапов в маршрут.
        /// </summary>
        /// <param name="source">Сценарий с условием включения шаблона этапов в маршрут.</param>
        /// <returns>Объект <see cref="KrStageTemplateBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public KrStageTemplateBuilder SetCondition(
            string source) => this.SetField(KrConstants.KrStageTemplates.SourceCondition, source);

        /// <summary>
        /// Задаёт SQL условие включения шаблона этапов в маршрут.
        /// </summary>
        /// <param name="sql">SQL условие включения шаблона этапов в маршрут.</param>
        /// <returns>Объект <see cref="KrStageTemplateBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public KrStageTemplateBuilder SetSqlCondition(
            string sql) => this.SetField(KrConstants.KrStageTemplates.SqlCondition, sql);

        /// <summary>
        /// Задаёт сценарий инициализации шаблона этапов.
        /// </summary>
        /// <param name="source">Сценарий инициализации шаблона этапов.</param>
        /// <returns>Объект <see cref="KrStageTemplateBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public KrStageTemplateBuilder SetBefore(
            string source) => this.SetField(KrConstants.KrStageTemplates.SourceBefore, source);

        /// <summary>
        /// Задаёт сценарий постобработки шаблона этапов.
        /// </summary>
        /// <param name="source">Сценарий постобработки шаблона этапов.</param>
        /// <returns>Объект <see cref="KrStageTemplateBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public KrStageTemplateBuilder SetAfter(
            string source) => this.SetField(KrConstants.KrStageTemplates.SourceAfter, source);

        /// <summary>
        /// Добавляет этап имеющий указанный тип и параметры.
        /// </summary>
        /// <param name="name">Имя этапа.</param>
        /// <param name="desc">Дескриптор этапа.</param>
        /// <param name="timeLimit">Срок выполнения задания отправляемого этапом в рабочих днях. Может быть задано только или это значение, или <paramref name="planned"/>.</param>
        /// <param name="planned">Дата выполнения задания отправляемого этапом. Может быть задано только или это значение, или <paramref name="timeLimit"/>. Имеет приоритет над <paramref name="timeLimit"/>.</param>
        /// <param name="hidden">Значение <see langword="true"/>, если этап является скрытым, иначе - <see langword="false"/>.</param>
        /// <param name="modifyAction">Действие выполняемое над строкой, содержащей информацию об этапе, после её инициализации.</param>
        /// <param name="skip">Значение <see langword="true"/>, если этап является пропущенным, иначе - <see langword="false"/>.</param>
        /// <param name="canBeSkipped">Значение <see langword="true"/>, если разрешено пропускать этап, иначе - <see langword="false"/>.</param>
        /// <param name="performerIDs">Массив идентификаторов ролей исполнителей этапа. Может иметь значение <see langword="null"/>.</param>
        /// <returns>Объект <see cref="KrStageTemplateBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public KrStageTemplateBuilder AddStage(
            string name,
            StageTypeDescriptor desc,
            double? timeLimit = default,
            DateTime? planned = default,
            bool hidden = default,
            Action<CardRow, Card> modifyAction = default,
            bool canBeSkipped = default,
            params Guid[] performerIDs)
        {
            this.ApplyAction(
                async (clc, action, ct) =>
                {
                    var rb = new RouteBuilder(clc, this.Dependencies.CardMetadata);
                    rb.AddStage(
                        name,
                        desc,
                        modifyAction: (r, c) =>
                        {
                            if (timeLimit.HasValue && !planned.HasValue)
                            {
                                r.Fields[KrConstants.KrStages.TimeLimit] = DoubleBoxes.Box(timeLimit);
                            }

                            if (planned.HasValue)
                            {
                                r.Fields[KrConstants.KrStages.Planned] = planned;
                            }

                            r.Fields[KrConstants.KrStages.Hidden] = BooleanBoxes.Box(hidden);
                            r.Fields[KrConstants.KrStages.CanBeSkipped] = BooleanBoxes.Box(canBeSkipped);
                            modifyAction?.Invoke(r, c);
                        });

                    var usageMode = desc.PerformerUsageMode;
                    if (performerIDs?.Length > 0
                        && usageMode != PerformerUsageMode.None)
                    {
                        var perf = rb
                            .ModifyPerformers();

                        switch (usageMode)
                        {
                            case PerformerUsageMode.Single:
                                {
                                    if (performerIDs.Length > 1)
                                    {
                                        TestContext.WriteLine(
                                            nameof(KrStageTemplateBuilder) + "." + nameof(KrStageTemplateBuilder.AddStage)
                                            + ": Only one performer can be assigned to a stage. The first performer will be used."
                                            + $"{Environment.NewLine}Stage name: \"{name}\"."
                                            + $"{Environment.NewLine}Stage descriptor: {desc}.");
                                    }

                                    var perfID = performerIDs[0];
                                    perf.SetSinglePerformer(perfID, perfID.ToString());

                                    break;
                                }

                            case PerformerUsageMode.Multiple:
                                {
                                    foreach (var perfID in performerIDs)
                                    {
                                        perf.AddPerformer(perfID, perfID.ToString());
                                    }

                                    break;
                                }
                        }
                    }

                    await rb.GoAsync(cancellationToken: ct);

                    return ValidationResult.Empty;
                },
                name: $"{nameof(KrStageTemplateBuilder)}.{nameof(AddStage)}");

            return this;
        }

        #endregion

        #region INamedEntry Members

        /// <summary>
        /// Возвращает идентификатор объекта.
        /// </summary>
        /// <exception cref="NotSupportedException">Установка значения не поддерживается.</exception>
        Guid INamedEntry.ID
        {
            get => this.CardID;
            set => throw new NotSupportedException("Setting the value is not supported.");
        }

        /// <summary>
        /// Возвращает название объекта.
        /// </summary>
        /// <exception cref="NotSupportedException">Установка значения не поддерживается.</exception>
        string INamedEntry.Name
        {
            get => ((INamedItem) this).Name;
            set => throw new NotSupportedException("Setting the value is not supported.");
        }

        #endregion

        #region INamedItem Members

        /// <inheritdoc/>
        string INamedItem.Name => this.GetName();

        #endregion

        #region Private methods

        /// <summary>
        /// Задаёт значение указанного поля секции <see cref="KrConstants.KrStageTemplates.Name"/>.
        /// </summary>
        /// <param name="field">Имя поля.</param>
        /// <param name="value">Значение.</param>
        /// <returns>Объект <see cref="KrStageTemplateBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        private KrStageTemplateBuilder SetField(
            string field,
            object value) => this.SetValue(KrConstants.KrStageTemplates.Name, field, value);

        /// <summary>
        /// Возвращает значение указанного поля секции <see cref="KrConstants.KrStageTemplates.Name"/>.
        /// </summary>
        /// <typeparam name="T">Тип возвращаемого значения.</typeparam>
        /// <param name="field">Имя поля.</param>
        /// <returns>Возвращаемое значение.</returns>
        private T TryGetField<T>(
            string field) => this.TryGetValue<T>(KrConstants.KrStageTemplates.Name, field);

        #endregion
    }
}
