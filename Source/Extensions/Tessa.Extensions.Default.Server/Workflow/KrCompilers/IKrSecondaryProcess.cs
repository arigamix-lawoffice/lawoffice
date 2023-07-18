using System;
using System.Collections.Generic;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Scope;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers
{
    /// <summary>
    /// Описывает объект предоставляющий основную информацию о вторичном процессе.
    /// </summary>
    public interface IKrSecondaryProcess :
        IExecutionSources
    {
        /// <summary>
        /// Возвращает идентификатор вторичного процесса.
        /// </summary>
        Guid ID { get; }

        /// <summary>
        /// Возвращает название вторичного процесса.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Возвращает значение, показывающее, что процесс является глобальным.
        /// </summary>
        bool IsGlobal { get; }

        /// <summary>
        /// Возвращает значение, показывающее, что процесс допускает асинхронное выполнение.
        /// </summary>
        bool Async { get; }

        /// <summary>
        /// Возвращает сообщение о недоступности процесса для выполнения.
        /// </summary>
        string ExecutionAccessDeniedMessage { get; }

        /// <summary>
        /// Возвращает значение, показывающее, что процесс может быть запущен только один раз в пределах одной и той же области выполнения процесса (<see cref="KrScope"/>).
        /// </summary>
        bool RunOnce { get; }

        /// <summary>
        /// Возвращает значение, показывающее, что при отсутствии этапов, доступных для выполнения, не должно отображаться сообщение.
        /// </summary>
        bool NotMessageHasNoActiveStages { get; }

        /// <summary>
        /// Возвращает список контекстных ролей, проверяемых перед выполнением процесса.
        /// </summary>
        IReadOnlyList<Guid> ContextRolesIDs { get; }
    }
}
