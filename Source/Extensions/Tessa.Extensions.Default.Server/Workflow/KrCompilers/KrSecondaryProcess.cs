using System;
using System.Collections.Generic;
using System.Linq;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Scope;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers
{
    /// <summary>
    /// Базовый абстрактный класс предоставляющий основную информацию о вторичном процессе.
    /// </summary>
    public abstract class KrSecondaryProcess :
        IKrSecondaryProcess
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
        protected KrSecondaryProcess(
            Guid id,
            string name,
            bool isGlobal,
            bool async,
            string executionAccessDeniedMessage,
            bool runOnce,
            bool notMessageHasNoActiveStages,
            IEnumerable<Guid> contextRolesIDs,
            string executionSqlCondition,
            string executionSourceCondition)
        {
            this.ID = id;
            this.Name = name;
            this.IsGlobal = isGlobal;
            this.Async = async;
            this.ExecutionAccessDeniedMessage = executionAccessDeniedMessage;
            this.RunOnce = runOnce;
            this.NotMessageHasNoActiveStages = notMessageHasNoActiveStages;
            this.ExecutionSqlCondition = executionSqlCondition;
            this.ExecutionSourceCondition = executionSourceCondition;
            this.ContextRolesIDs = contextRolesIDs.ToList().AsReadOnly();
        }

        #endregion

        #region IKrSecondaryProcess Members

        /// <inheritdoc />
        public Guid ID { get; }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public bool IsGlobal { get; }

        /// <inheritdoc />
        public bool Async { get; }

        /// <inheritdoc />
        public string ExecutionAccessDeniedMessage { get; }

        /// <inheritdoc />
        public bool RunOnce { get; }

        /// <inheritdoc/>
        public bool NotMessageHasNoActiveStages { get; }

        /// <inheritdoc />
        public IReadOnlyList<Guid> ContextRolesIDs { get; }

        /// <inheritdoc />
        public string ExecutionSqlCondition { get; }

        /// <inheritdoc />
        public string ExecutionSourceCondition { get; }

        #endregion
    }
}