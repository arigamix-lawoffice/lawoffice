using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace Tessa.Extensions.Default.Shared.Workflow.WorkflowEngine
{
    /// <summary>
    /// Описывает объект предоставляющий доступ к вариантам завершения действий.
    /// </summary>
    public interface IActionCompletionOptionsProvider
    {
        /// <summary>
        /// Возвращает доступный только для чтения словарь с вариантами завершения действий.
        /// </summary>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить выполнения асинхронной задачи.</param>
        /// <returns>Доступный только для чтения словарь с вариантами завершения действий.</returns>
        ValueTask<ReadOnlyDictionary<Guid, ActionCompletionOption>> GetActionCompletionOptionsAsync(CancellationToken cancellationToken = default);
    }
}
