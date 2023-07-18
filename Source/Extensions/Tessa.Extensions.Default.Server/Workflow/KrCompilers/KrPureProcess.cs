using System;
using System.Collections.Generic;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers
{
    /// <summary>
    /// Предоставляет информацию о вторичном процессе работающем в режиме "Простой процесс".
    /// </summary>
    public sealed class KrPureProcess :
        KrSecondaryProcess,
        IKrPureProcess
    {
        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="KrPureProcess"/>.
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
        /// <param name="allowClientSideLaunch">Значение <see langword="true"/>, если запуск процесса разрешен с клиента, иначе - <see langword="false"/>.</param>
        /// <param name="checkRecalcRestrictions">Значение <see langword="true"/>, если необходимо проверять ограничения при запуске процесса, иначе - <see langword="false"/>.</param>
        public KrPureProcess(
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
            bool allowClientSideLaunch,
            bool checkRecalcRestrictions)
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
            this.AllowClientSideLaunch = allowClientSideLaunch;
            this.CheckRecalcRestrictions = checkRecalcRestrictions;
        }

        #endregion

        #region IKrPureProcess Members

        /// <inheritdoc />
        public bool AllowClientSideLaunch { get; }

        /// <inheritdoc />
        public bool CheckRecalcRestrictions { get; }

        #endregion
    }
}