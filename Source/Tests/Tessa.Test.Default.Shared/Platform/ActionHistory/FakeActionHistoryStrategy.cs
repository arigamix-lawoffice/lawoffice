#nullable enable
using System;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Platform.Runtime;

namespace Tessa.Test.Default.Shared.Platform.ActionHistory
{
    /// <summary>
    /// Стратегия работы с историей действий, которая ничего не выполняет.
    /// За счет неё снижается нагрузка на базу данных при запуске тестов.
    /// </summary>
    public sealed class FakeActionHistoryStrategy :
        IActionHistoryStrategy
    {
        public ValueTask DeleteAsync(Guid cardID, CancellationToken cancellationToken = default) =>
            ValueTask.CompletedTask;

        public ValueTask InsertAsync(ActionHistoryRecord actionHistoryRecord, CancellationToken cancellationToken = default)
        {
            actionHistoryRecord.RowID = Guid.NewGuid();
            return ValueTask.CompletedTask;
        }

        public ValueTask<ActionHistoryRecord?> TryGetAsync(Guid rowID, CancellationToken cancellationToken = default) =>
            ValueTask.FromResult(default(ActionHistoryRecord?));
    }
}
