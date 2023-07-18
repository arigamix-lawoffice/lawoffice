using System;
using System.Collections.Generic;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers
{
    /// <summary>
    /// Предоставляет информацию о вторичном процессе работающем в режиме "Действие".
    /// </summary>
    public sealed class KrAction :
        KrSecondaryProcess,
        IKrAction
    {
        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="KrSecondaryProcess"/>.
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
        /// <param name="eventType">Тип события, по которому запускается действие. Может иметь значение по умолчанию для типа.</param>
        public KrAction(
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
            string eventType)
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
            this.EventType = eventType;
        }

        #endregion

        #region IKrAction Members

        /// <inheritdoc />
        public string EventType { get; }

        #endregion
    }
}