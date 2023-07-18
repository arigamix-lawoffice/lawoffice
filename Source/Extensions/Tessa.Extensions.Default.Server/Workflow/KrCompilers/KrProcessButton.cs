using System;
using System.Collections.Generic;
using Tessa.Platform;
using Tessa.Platform.Conditions;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers
{
    /// <summary>
    /// Предоставляет информацию о вторичном процессе работающем в режиме "Кнопка".
    /// </summary>
    public sealed class KrProcessButton :
        KrSecondaryProcess,
        IKrProcessButton
    {
        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="KrProcessButton"/>.
        /// </summary>
        /// <param name="id">Идентификатор вторичного процесса.</param>
        /// <param name="name">Название вторичного процесса.</param>
        /// <param name="isGlobal">Значение <see langword="true"/>, если процесс является глобальным, иначе - <see langword="false"/>.</param>
        /// <param name="async">Значение <see langword="true"/>, если процесс допускает асинхронное выполнение, иначе - <see langword="false"/>.</param>
        /// <param name="executionAccessDeniedMessage">Собщение о недоступности процесса для выполнения.</param>
        /// <param name="runOnce">Значение <see langword="true"/>, если процесс может быть запущен только один раз в пределах одной и той же области выполнения процесса (<see cref="KrScope"/>), иначе - <see langword="false"/>.</param>
        /// <param name="notMessageHasNoActiveStages">Значение <see langword="true"/>, если при отсутствии этапов, доступных для выполнения, не должно отображаться сообщение, иначе - <see langword="false"/>.</param>
        /// <param name="contextRolesIDs">Список контекстных ролей, проверяемых перед выполнением процесса.</param>
        /// <param name="executionSqlCondition">Текст SQL запроса с условием пределяющий доступность выполнения.</param>
        /// <param name="executionSourceCondition">C# код, определяющий доступность выполнения.</param>
        /// <param name="caption">Отображаемое название кнопки.</param>
        /// <param name="icon">Значок.</param>
        /// <param name="tileSize">Размер кнопки.</param>
        /// <param name="tooltip">Подсказка.</param>
        /// <param name="tileGroup">Группа кнопки.</param>
        /// <param name="refreshAndNotify">Значение <see langword="true"/>, если необходимо ли проверить наличие новых заданий после выполнения, иначе - <see langword="false"/>.</param>
        /// <param name="askConfirmation">Значение <see langword="true"/>, если необходимо спрашивать подтверждение перед выполнением, иначе - <see langword="false"/>.</param>
        /// <param name="confirmationMessage">Текст подтверждения перед выполнением.</param>
        /// <param name="actionGrouping">Значение <see langword="true"/>, если необходимо ли группировать тайл в группу "Действия", иначе - <see langword="false"/>.</param>
        /// <param name="buttonHotkey">Сочетание клавиш.</param>
        /// <param name="order">Порядок кнопки.</param>
        /// <param name="visibilitySqlCondition">Текст SQL запроса с условием пределяющим видимость.</param>
        /// <param name="visibilitySourceCondition">C# код, определяющий видимость.</param>
        /// <param name="conditions">Перечисление параметров условий.</param>
        public KrProcessButton(
            Guid id,
            string name,
            bool isGlobal,
            bool async,
            string executionAccessDeniedMessage,
            bool runOnce,
            bool notMessageHasNoActiveStages,
            IEnumerable<Guid> contextRolesIDs,
            string executionSqlCondition,
            string executionSourceCondition,
            string caption,
            string icon,
            TileSize tileSize,
            string tooltip,
            string tileGroup,
            bool refreshAndNotify,
            bool askConfirmation,
            string confirmationMessage,
            bool actionGrouping,
            string buttonHotkey,
            int order,
            string visibilitySqlCondition,
            string visibilitySourceCondition,
            IEnumerable<ConditionSettings> conditions)
        : base(
              id,
              name,
              isGlobal,
              async,
              executionAccessDeniedMessage,
              runOnce,
              notMessageHasNoActiveStages,
              contextRolesIDs,
              executionSqlCondition,
              executionSourceCondition)
        {
            this.Caption = caption;
            this.Icon = icon;
            this.Tooltip = tooltip;
            this.TileSize = tileSize;
            this.TileGroup = tileGroup;
            this.RefreshAndNotify = refreshAndNotify;
            this.AskConfirmation = askConfirmation;
            this.ConfirmationMessage = confirmationMessage;
            this.ActionGrouping = actionGrouping;
            this.ButtonHotkey = buttonHotkey;
            this.Order = order;
            this.VisibilitySqlCondition = visibilitySqlCondition;
            this.VisibilitySourceCondition = visibilitySourceCondition;
            this.Conditions = conditions;
        }

        #endregion

        #region IKrProcessButton Members

        /// <inheritdoc />
        public string Caption { get; }

        /// <inheritdoc />
        public string Icon { get; }

        /// <inheritdoc />
        public TileSize TileSize { get; }

        /// <inheritdoc />
        public string Tooltip { get; }

        /// <inheritdoc />
        public string TileGroup { get; }

        /// <inheritdoc />
        public bool RefreshAndNotify { get; }

        /// <inheritdoc />
        public bool AskConfirmation { get; }

        /// <inheritdoc />
        public string ConfirmationMessage { get; }

        /// <inheritdoc />
        public bool ActionGrouping { get; }

        /// <inheritdoc />
        public string ButtonHotkey { get; }

        /// <inheritdoc />
        public int Order { get; }

        /// <inheritdoc />
        public string VisibilitySqlCondition { get; }

        /// <inheritdoc />
        public string VisibilitySourceCondition { get; }

        /// <inheritdoc />
        public IEnumerable<ConditionSettings> Conditions { get; }

        #endregion
    }
}