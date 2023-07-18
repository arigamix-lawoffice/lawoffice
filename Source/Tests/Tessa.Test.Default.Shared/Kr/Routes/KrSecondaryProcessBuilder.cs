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
    /// Предоставляет методы для создания и модификации карточки вторичного процесса.
    /// </summary>
    public sealed class KrSecondaryProcessBuilder :
        CardLifecycleCompanion<KrSecondaryProcessBuilder>,
        INamedEntry
    {
        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="KrSecondaryProcessBuilder"/>.
        /// </summary>
        /// <param name="deps">Зависимости, используемые при взаимодействии с карточками.</param>
        public KrSecondaryProcessBuilder(
            ICardLifecycleCompanionDependencies deps)
            : this(Guid.NewGuid(), deps)
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="KrSecondaryProcessBuilder"/>.
        /// </summary>
        /// <param name="cardID">Идентификатор карточки вторичного процесса.</param>
        /// <param name="deps">Зависимости, используемые при взаимодействии с карточками.</param>
        public KrSecondaryProcessBuilder(
            Guid cardID,
            ICardLifecycleCompanionDependencies deps)
            : base(cardID, DefaultCardTypes.KrSecondaryProcessTypeID, DefaultCardTypes.KrSecondaryProcessTypeName, deps)
        {
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Устанавливает название вторичного процесса.
        /// </summary>
        /// <param name="value">Название вторичного процесса.</param>
        /// <returns>Объект <see cref="KrSecondaryProcessBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public KrSecondaryProcessBuilder SetName(string value) =>
            this.SetField(KrConstants.KrSecondaryProcesses.NameField, value);

        /// <summary>
        /// Возвращает название вторичного процесса.
        /// </summary>
        /// <returns>Название вторичного процесса.</returns>
        public string GetName() =>
            this.TryGetField<string>(KrConstants.KrSecondaryProcesses.NameField);

        /// <summary>
        /// Устанавливает признак, показывающий, что процесс является глобальным.
        /// </summary>
        /// <param name="value">Значение <see langword="true"/>, если вторичный процесс является глобальным, иначе - <see langword="false"/>.</param>
        /// <returns>Объект <see cref="KrSecondaryProcessBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public KrSecondaryProcessBuilder SetIsGlobal(bool value) =>
            this.SetField(KrConstants.KrSecondaryProcesses.IsGlobal, BooleanBoxes.Box(value));

        /// <summary>
        /// Возвращает признак, показывающий, что процесс является глобальным.
        /// </summary>
        /// <returns>Значение <see langword="true"/>, если вторичный процесс является глобальным, иначе - <see langword="false"/>.</returns>
        public bool GetIsGlobal() =>
            this.TryGetField<bool>(KrConstants.KrSecondaryProcesses.IsGlobal);

        /// <summary>
        /// Устанавливает признак, показывающий, что процесс является асинхронным.
        /// </summary>
        /// <param name="value">Значение <see langword="true"/>, если вторичный процесс является асинхронным, иначе - <see langword="false"/>.</param>
        /// <returns>Объект <see cref="KrSecondaryProcessBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public KrSecondaryProcessBuilder SetAsync(bool value) =>
            this.SetField(KrConstants.KrSecondaryProcesses.Async, BooleanBoxes.Box(value));

        /// <summary>
        /// Возвращает признак, показывающий, что процесс является асинхронным.
        /// </summary>
        /// <returns>Значение <see langword="true"/>, если вторичный процесс является асинхронным, иначе - <see langword="false"/>.</returns>
        public bool GetAsync() =>
            this.TryGetField<bool>(KrConstants.KrSecondaryProcesses.Async);

        /// <summary>
        /// Устанавливает сообщение при недоступности для выполнения.
        /// </summary>
        /// <param name="value">Сообщение при недоступности для выполнения.</param>
        /// <returns>Объект <see cref="KrSecondaryProcessBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public KrSecondaryProcessBuilder SetExecutionAccessDeniedMessage(string value) =>
            this.SetField(KrConstants.KrSecondaryProcesses.ExecutionAccessDeniedMessage, value);

        /// <summary>
        /// Возвращает сообщение при недоступности для выполнения.
        /// </summary>
        /// <returns>Сообщение при недоступности для выполнения.</returns>
        public string GetExecutionAccessDeniedMessage() =>
            this.TryGetField<string>(KrConstants.KrSecondaryProcesses.ExecutionAccessDeniedMessage);

        /// <summary>
        /// Устанавливает признак, показывающий, что процесс должен быть запущен один раз за запрос.
        /// </summary>
        /// <param name="value">Значение <see langword="true"/>, если вторичный процесс должен быть запущен один раз за запрос, иначе - <see langword="false"/>.</param>
        /// <returns>Объект <see cref="KrSecondaryProcessBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public KrSecondaryProcessBuilder SetRunOnce(bool value) =>
            this.SetField(KrConstants.KrSecondaryProcesses.RunOnce, BooleanBoxes.Box(value));

        /// <summary>
        /// Возвращает признак, показывающий, что процесс должен быть запущен один раз за запрос.
        /// </summary>
        /// <returns>Значение <see langword="true"/>, если вторичный процесс должен быть запущен один раз за запрос, иначе - <see langword="false"/>.</returns>
        public bool GetRunOnce() =>
            this.TryGetField<bool>(KrConstants.KrSecondaryProcesses.RunOnce);

        /// <summary>
        /// Устанавливает признак, показывающий, что не должно отображаться сообщение при отсутствии этапов доступных для выполнения.
        /// </summary>
        /// <param name="value">Значение <see langword="true"/>, если не должно отображаться сообщение при отсутствии этапов доступных для выполнения, иначе - <see langword="false"/>.</param>
        /// <returns>Объект <see cref="KrSecondaryProcessBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public KrSecondaryProcessBuilder SetNotMessageHasNoActiveStages(bool value) =>
            this.SetField(KrConstants.KrSecondaryProcesses.NotMessageHasNoActiveStages, BooleanBoxes.Box(value));

        /// <summary>
        /// Возвращает признак, показывающий, что не должно отображаться сообщение при отсутствии этапов доступных для выполнения.
        /// </summary>
        /// <returns>Значение <see langword="true"/>, если не должно отображаться сообщение при отсутствии этапов доступных для выполнения, иначе - <see langword="false"/>.</returns>
        public bool GetNotMessageHasNoActiveStages() =>
            this.TryGetField<bool>(KrConstants.KrSecondaryProcesses.NotMessageHasNoActiveStages);

        /// <summary>
        /// Устанавливает режим работы "Простой процесс" (<see cref="KrConstants.KrSecondaryProcessModes.PureProcess"/>) и его параметры.
        /// </summary>
        /// <param name="allowClientSideLaunch">Значение <see langword="true"/>, если разрешён запуск на клиенте, иначе - <see langword="false"/>.</param>
        /// <param name="checkRecalcRestrictions">Значение <see langword="true"/>, если следует проверять ограничения при пересчёте, иначе - <see langword="false"/>.</param>
        /// <returns>Объект <see cref="KrSecondaryProcessBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public KrSecondaryProcessBuilder SetPureProcess(
            bool allowClientSideLaunch = default,
            bool checkRecalcRestrictions = default)
        {
            var mode = KrConstants.KrSecondaryProcessModes.PureProcess;
            this.SetField(KrConstants.KrSecondaryProcesses.ModeID, mode.ID)
                .SetField(KrConstants.KrSecondaryProcesses.ModeName, mode.Name)
                .SetField(KrConstants.KrSecondaryProcesses.AllowClientSideLaunch, BooleanBoxes.Box(allowClientSideLaunch))
                .SetField(KrConstants.KrSecondaryProcesses.CheckRecalcRestrictions, BooleanBoxes.Box(checkRecalcRestrictions));
            return this;
        }

        /// <summary>
        /// Устанавливает режим работы "Кнопка" (<see cref="KrConstants.KrSecondaryProcessModes.Button"/>) и его параметры.
        /// </summary>
        /// <param name="caption">Текст плитки.</param>
        /// <param name="icon">Иконка плитки.</param>
        /// <param name="tileSize">Размер плитки.</param>
        /// <param name="tooltip">Всплывающая подсказка у тайла.</param>
        /// <param name="tileGroup">Группа в которую должна быть включена плитка.</param>
        /// <param name="refreshAndNotify">Значение <see langword="true"/>, если необходимо проверить наличие новых заданий после выполнения вторичного процесса, иначе - <see langword="false"/>.</param>
        /// <param name="askConfirmation">Значение <see langword="true"/>, если необходимо запрашивать подтверждение при запуске вторичного процесса, иначе - <see langword="false"/>.</param>
        /// <param name="confirmationMessage">Текст подтверждения.</param>
        /// <param name="actionGrouping">Значение <see langword="true"/>, если необходимо тайл этого вторичного процесса разместить в группе "Действия", иначе - <see langword="false"/>.</param>
        /// <param name="order">Порядок.</param>
        /// <param name="buttonHotkey">Сочетание клавиш.</param>
        /// <returns>Объект <see cref="KrSecondaryProcessBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public KrSecondaryProcessBuilder SetProcessButton(
            string caption,
            string icon = default,
            TileSize tileSize = TileSize.Full,
            string tooltip = default,
            string tileGroup = default,
            bool refreshAndNotify = default,
            bool askConfirmation = default,
            string confirmationMessage = default,
            bool actionGrouping = default,
            int order = default,
            string buttonHotkey = default)
        {
            var mode = KrConstants.KrSecondaryProcessModes.Button;
            this.SetField(KrConstants.KrSecondaryProcesses.ModeID, Int32Boxes.Box(mode.ID))
                .SetField(KrConstants.KrSecondaryProcesses.ModeName, mode.Name)

                .SetField(KrConstants.KrSecondaryProcesses.Icon, icon)
                .SetField(KrConstants.KrSecondaryProcesses.TileSizeID, Int32Boxes.Box((int) tileSize))
                .SetField(KrConstants.KrSecondaryProcesses.Caption, caption)
                .SetField(KrConstants.KrSecondaryProcesses.Tooltip, tooltip)
                .SetField(KrConstants.KrSecondaryProcesses.TileGroup, tileGroup)
                .SetField(KrConstants.KrSecondaryProcesses.RefreshAndNotify, BooleanBoxes.Box(refreshAndNotify))
                .SetField(KrConstants.KrSecondaryProcesses.AskConfirmation, BooleanBoxes.Box(askConfirmation))
                .SetField(KrConstants.KrSecondaryProcesses.ConfirmationMessage, confirmationMessage)
                .SetField(KrConstants.KrSecondaryProcesses.ActionGrouping, BooleanBoxes.Box(actionGrouping))
                .SetField(KrConstants.KrSecondaryProcesses.Order, Int32Boxes.Box(order))
                .SetField(KrConstants.KrSecondaryProcesses.ButtonHotkey, buttonHotkey);

            return this;
        }

        /// <summary>
        /// Устанавливает режим работы "Действие" (<see cref="KrConstants.KrSecondaryProcessModes.Action"/>) и тип указанного события.
        /// </summary>
        /// <param name="eventType">Тип события. Значение предоставляемое классом <see cref="T:Tessa.Extensions.Default.Server.Workflow.KrProcess.Events.DefaultEventTypes"/>.</param>
        /// <returns>Объект <see cref="KrSecondaryProcessBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public KrSecondaryProcessBuilder SetAction(
            string eventType)
        {
            var mode = KrConstants.KrSecondaryProcessModes.Action;
            this.SetField(KrConstants.KrSecondaryProcesses.ModeID, Int32Boxes.Box(mode.ID))
                .SetField(KrConstants.KrSecondaryProcesses.ModeName, mode.Name)
                .SetField(KrConstants.KrSecondaryProcesses.ActionEventType, eventType);

            return this;
        }

        /// <summary>
        /// Задаёт указанный тип документа или карточки в качестве ограничения при пересчёте.
        /// </summary>
        /// <param name="typeID">Идентификатор типа документа или карточки.</param>
        /// <param name="typeName">Имя типа документа или карточки.</param>
        /// <param name="isDocType">Значение <see langword="true"/>, если указанный тип является типом документа, иначе - <see langword="false"/>, типом карточки.</param>
        /// <returns>Объект <see cref="KrSecondaryProcessBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public KrSecondaryProcessBuilder ForType(
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
                name: nameof(KrSecondaryProcessBuilder) + "." + nameof(ForType));

            return this;
        }

        /// <summary>
        /// Задаёт указанный тип карточки в качестве ограничения при пересчёте.
        /// </summary>
        /// <param name="typeID">Идентификатор типа карточки.</param>
        /// <param name="typeName">Имя типа карточки.</param>
        /// <returns>Объект <see cref="KrSecondaryProcessBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public KrSecondaryProcessBuilder ForCardType(
            Guid typeID,
            string typeName) => this.ForType(typeID, typeName, false);

        /// <summary>
        /// Задаёт указанный тип документа в качестве ограничения при пересчёте.
        /// </summary>
        /// <param name="typeID">Идентификатор типа документа.</param>
        /// <param name="typeName">Имя типа документа.</param>
        /// <returns>Объект <see cref="KrSecondaryProcessBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public KrSecondaryProcessBuilder ForDocType(
            Guid typeID,
            string typeName) => this.ForType(typeID, typeName, true);

        /// <summary>
        /// Задаёт указанную роль в качестве ограничения при пересчёте.
        /// </summary>
        /// <param name="roleID">Идентификатор роли.</param>
        /// <param name="roleName">Имя роли.</param>
        /// <returns>Объект <see cref="KrSecondaryProcessBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public KrSecondaryProcessBuilder ForRole(
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
        /// Задаёт условное выражение, определяющее видимость тайла процесса.
        /// </summary>
        /// <param name="source">Условное выражение, определяющее видимость тайла процесса.</param>
        /// <returns>Объект <see cref="KrSecondaryProcessBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public KrSecondaryProcessBuilder SetVisibilityCondition(
            string source) => this.SetField(KrConstants.KrSecondaryProcesses.VisibilitySourceCondition, source);

        /// <summary>
        /// Задаёт условное выражение, дополнительной настройки выполнения.
        /// </summary>
        /// <param name="source">Условное выражение, дополнительной настройки выполнения.</param>
        /// <returns>Объект <see cref="KrSecondaryProcessBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public KrSecondaryProcessBuilder SetExecutionCondition(
            string source) => this.SetField(KrConstants.KrSecondaryProcesses.ExecutionSourceCondition, source);

        /// <summary>
        /// Задаёт SQL условие, дополнительной настройки выполнения.
        /// </summary>
        /// <param name="source">SQL условие, дополнительной настройки выполнения.</param>
        /// <returns>Объект <see cref="KrSecondaryProcessBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public KrSecondaryProcessBuilder SetExecutionSqlCondition(
            string source) => this.SetField(KrConstants.KrSecondaryProcesses.ExecutionSqlCondition, source);

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
        /// Задаёт значение указанного поля секции <see cref="KrConstants.KrSecondaryProcesses.Name"/>.
        /// </summary>
        /// <param name="field">Имя поля.</param>
        /// <param name="value">Значение.</param>
        /// <returns>Объект <see cref="KrSecondaryProcessBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        private KrSecondaryProcessBuilder SetField(
            string field,
            object value) => this.SetValue(KrConstants.KrSecondaryProcesses.Name, field, value);

        /// <summary>
        /// Возвращает значение указанного поля секции <see cref="KrConstants.KrSecondaryProcesses.Name"/>.
        /// </summary>
        /// <typeparam name="T">Тип возвращаемого значения.</typeparam>
        /// <param name="field">Имя поля.</param>
        /// <returns>Возвращаемое значение.</returns>
        private T TryGetField<T>(
            string field) => this.TryGetValue<T>(KrConstants.KrSecondaryProcesses.Name, field);

        #endregion
    }
}
