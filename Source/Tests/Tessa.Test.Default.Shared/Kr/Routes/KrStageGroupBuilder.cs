using System;
using System.Threading;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;
using Tessa.Platform.Collections;
using Tessa.Platform.Validation;

namespace Tessa.Test.Default.Shared.Kr.Routes
{
    /// <summary>
    /// Предоставляет методы для создания и модификации карточки группы этапов.
    /// </summary>
    public sealed class KrStageGroupBuilder :
        CardLifecycleCompanion<KrStageGroupBuilder>,
        INamedEntry
    {
        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="KrStageGroupBuilder"/>.
        /// </summary>
        /// <param name="deps">Зависимости, используемые при взаимодействии с карточками.</param>
        public KrStageGroupBuilder(
            ICardLifecycleCompanionDependencies deps)
            : this(Guid.NewGuid(), deps)
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="KrStageGroupBuilder"/>.
        /// </summary>
        /// <param name="cardID">Идентификатор карточки группы этапов.</param>
        /// <param name="deps">Зависимости, используемые при взаимодействии с карточками.</param>
        public KrStageGroupBuilder(
            Guid cardID,
            ICardLifecycleCompanionDependencies deps)
            : base(cardID, DefaultCardTypes.KrStageGroupTypeID, DefaultCardTypes.KrStageGroupTypeName, deps)
        {
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Устанавливает название группы этапов.
        /// </summary>
        /// <param name="value">Название группы этапов.</param>
        /// <returns>Объект <see cref="KrStageGroupBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public KrStageGroupBuilder SetName(string value) =>
            this.SetField(KrConstants.KrStageGroups.NameField, value);

        /// <summary>
        /// Возвращает название группы этапов.
        /// </summary>
        /// <returns>Название группы этапов.</returns>
        public string GetName() =>
            this.TryGetField<string>(KrConstants.KrStageGroups.NameField);

        /// <summary>
        /// Устанавливает порядковый номер группы этапов.
        /// </summary>
        /// <param name="value">Порядковый номер группы этапов.</param>
        /// <returns>Объект <see cref="KrStageGroupBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public KrStageGroupBuilder SetOrder(int value) =>
            this.SetField(KrConstants.KrStageGroups.Order, Int32Boxes.Box(value));

        /// <summary>
        /// Возвращает порядковый номер группы этапов.
        /// </summary>
        /// <returns>Порядковый номер группы этапов.</returns>
        public int GetOrder() =>
            this.TryGetField<int>(KrConstants.KrStageGroups.Order);

        /// <summary>
        /// Задаёт информацию о вторичном процессе с которым связана данная группа этапов.
        /// </summary>
        /// <param name="id">Идентификатор вторичного процесса.</param>
        /// <param name="name">Название вторичного процесса.</param>
        /// <returns>Объект <see cref="KrStageGroupBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public KrStageGroupBuilder SetSecondaryProcess(
            Guid id,
            string name)
        {
            this.SetField(KrConstants.KrStageGroups.KrSecondaryProcessID, id)
                .SetField(KrConstants.KrStageGroups.KrSecondaryProcessName, name);
            return this;
        }

        /// <summary>
        /// Возвращает информацию о вторичном процессе с которым связана данная группа этапов.
        /// </summary>
        /// <returns>Кортеж: &lt;Идентификатор вторичного процесса; Название вторичного процесса.&gt;.</returns>
        public (Guid? ID, string Name) GetSecondaryProcess()
        {
            return (
                this.TryGetField<Guid?>(KrConstants.KrStageGroups.KrSecondaryProcessID),
                this.TryGetField<string>(KrConstants.KrStageGroups.KrSecondaryProcessName));
        }

        /// <summary>
        /// Устанавливает значение, показывающее, что все этапы группы этапов являются нередактируемыми.
        /// </summary>
        /// <param name="value">Значение <see langword="true"/>, если все этапы группы нередактируемые, иначе - <see langword="false"/>.</param>
        /// <returns>Объект <see cref="KrStageGroupBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public KrStageGroupBuilder SetGroupReadonly(bool value) =>
            this.SetField(KrConstants.KrStageGroups.IsGroupReadonly, BooleanBoxes.Box(value));

        /// <summary>
        /// Возвращает значение, показывающее, что все этапы группы этапов являются нередактируемыми.
        /// </summary>
        /// <returns>Значение <see langword="true"/>, если все этапы группы нередактируемые, иначе - <see langword="false"/>.</returns>
        public bool GetGroupReadonly() =>
            this.TryGetField<bool>(KrConstants.KrStageGroups.IsGroupReadonly);

        /// <summary>
        /// Устанавливает значение, показывающее, что данная группа этапов должна игнорироваться при пересчёте.
        /// </summary>
        /// <param name="value">Значение <see langword="true"/>, если группа должна игнорироваться при пересчёте, иначе - <see langword="false"/>.</param>
        /// <returns>Объект <see cref="KrStageGroupBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public KrStageGroupBuilder SetIgnore(bool value) =>
            this.SetField(KrConstants.KrStageGroups.Ignore, BooleanBoxes.Box(value));

        /// <summary>
        /// Возвращает значение, показывающее, что данная группа этапов должна игнорироваться при пересчёте.
        /// </summary>
        /// <returns>Значение <see langword="true"/>, если группа должна игнорироваться при пересчёте, иначе - <see langword="false"/>.</returns>
        public bool GetIgnore() =>
            this.TryGetField<bool>(KrConstants.KrStageGroups.Ignore);

        /// <summary>
        /// Задаёт указанный тип документа или карточки в качестве ограничения при пересчёте.
        /// </summary>
        /// <param name="typeID">Идентификатор типа документа или карточки.</param>
        /// <param name="typeName">Имя типа документа или карточки.</param>
        /// <param name="isDocType">Значение <see langword="true"/>, если указанный тип является типом документа, иначе - <see langword="false"/>, типом карточки.</param>
        /// <returns>Объект <see cref="KrStageGroupBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public KrStageGroupBuilder ForType(
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
                name: nameof(KrStageGroupBuilder) + "." + nameof(ForType));

            return this;
        }

        /// <summary>
        /// Задаёт указанный карточки в качестве ограничения при пересчёте.
        /// </summary>
        /// <param name="typeID">Идентификатор типа карточки.</param>
        /// <param name="typeName">Имя типа карточки.</param>
        /// <returns>Объект <see cref="KrStageGroupBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public KrStageGroupBuilder ForCardType(
            Guid typeID,
            string typeName) => this.ForType(typeID, typeName, false);

        /// <summary>
        /// Задаёт указанный тип документа в качестве ограничения при пересчёте.
        /// </summary>
        /// <param name="typeID">Идентификатор типа документа.</param>
        /// <param name="typeName">Имя типа документа.</param>
        /// <returns>Объект <see cref="KrStageGroupBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public KrStageGroupBuilder ForDocType(
            Guid typeID,
            string typeName) => this.ForType(typeID, typeName, true);

        /// <summary>
        /// Задаёт указанную роль в качестве ограничения при пересчёте.
        /// </summary>
        /// <param name="roleID">Идентификатор роли.</param>
        /// <param name="roleName">Имя роли.</param>
        /// <returns>Объект <see cref="KrStageGroupBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public KrStageGroupBuilder ForRole(
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
                name: nameof(KrStageGroupBuilder) + "." + nameof(ForType));

            return this;
        }

        /// <summary>
        /// Задаёт условное выражение используемое при построении маршрута.
        /// </summary>
        /// <param name="source">Условное выражение.</param>
        /// <returns>Объект <see cref="KrStageGroupBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public KrStageGroupBuilder SetCondition(
            string source) => this.SetField(KrConstants.KrStageGroups.SourceCondition, source);

        /// <summary>
        /// Задаёт SQL условие используемое при построении маршрута.
        /// </summary>
        /// <param name="sql">SQL условие.</param>
        /// <returns>Объект <see cref="KrStageGroupBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public KrStageGroupBuilder SetSqlCondition(
            string sql) => this.SetField(KrConstants.KrStageGroups.SqlCondition, sql);

        /// <summary>
        /// Задаёт сценарий инициализации выполняющийся при построении маршрута.
        /// </summary>
        /// <param name="source">Сценарий инициализации выполняющийся при построении маршрута.</param>
        /// <returns>Объект <see cref="KrStageGroupBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public KrStageGroupBuilder SetBefore(
            string source) => this.SetField(KrConstants.KrStageGroups.SourceBefore, source);

        /// <summary>
        /// Задаёт сценарий постобработки выполняющийся при построении маршрута.
        /// </summary>
        /// <param name="source">Сценарий постобработки выполняющийся при построении маршрута.</param>
        /// <returns>Объект <see cref="KrStageGroupBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public KrStageGroupBuilder SetAfter(
            string source) => this.SetField(KrConstants.KrStageGroups.SourceAfter, source);

        /// <summary>
        /// Задаёт условное выражение используемое при выполнении маршрута.
        /// </summary>
        /// <param name="source">Условное выражение используемое при выполнении маршрута.</param>
        /// <returns>Объект <see cref="KrStageGroupBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public KrStageGroupBuilder SetRuntimeCondition(
            string source) => this.SetField(KrConstants.KrStageGroups.RuntimeSourceCondition, source);

        /// <summary>
        /// Задаёт SQL условие используемое при выполнении маршрута.
        /// </summary>
        /// <param name="sql">SQL условие используемое при выполнении маршрута.</param>
        /// <returns>Объект <see cref="KrStageGroupBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public KrStageGroupBuilder SetRuntimeSqlCondition(
            string sql) => this.SetField(KrConstants.KrStageGroups.RuntimeSqlCondition, sql);

        /// <summary>
        /// Задаёт сценарий инициализации выполняющийся при выполнении маршрута.
        /// </summary>
        /// <param name="source">Сценарий инициализации выполняющийся при выполнении маршрута.</param>
        /// <returns>Объект <see cref="KrStageGroupBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public KrStageGroupBuilder SetRuntimeBefore(
            string source) => this.SetField(KrConstants.KrStageGroups.RuntimeSourceBefore, source);

        /// <summary>
        /// Задаёт сценарий постобработки выполняющийся при выполнении маршрута.
        /// </summary>
        /// <param name="source">Сценарий постобработки выполняющийся при выполнении маршрута.</param>
        /// <returns>Объект <see cref="KrStageGroupBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public KrStageGroupBuilder SetRuntimeAfter(
            string source) => this.SetField(KrConstants.KrStageGroups.RuntimeSourceAfter, source);

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
        /// Задаёт значение указанного поля секции <see cref="KrConstants.KrStageGroups.Name"/>.
        /// </summary>
        /// <param name="field">Имя поля.</param>
        /// <param name="value">Значение.</param>
        /// <returns>Объект <see cref="KrStageGroupBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        private KrStageGroupBuilder SetField(
            string field,
            object value) => this.SetValue(KrConstants.KrStageGroups.Name, field, value);

        /// <summary>
        /// Возвращает значение указанного поля секции <see cref="KrConstants.KrStageGroups.Name"/>.
        /// </summary>
        /// <typeparam name="T">Тип возвращаемого значения.</typeparam>
        /// <param name="field">Имя поля.</param>
        /// <returns>Возвращаемое значение.</returns>
        private T TryGetField<T>(
            string field) => this.TryGetValue<T>(KrConstants.KrStageGroups.Name, field);

        #endregion
    }
}
