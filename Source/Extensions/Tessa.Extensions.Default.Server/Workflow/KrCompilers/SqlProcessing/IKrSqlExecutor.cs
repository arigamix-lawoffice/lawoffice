using System.Collections.Generic;
using System.Threading.Tasks;
using Tessa.Extensions.Default.Server.Workflow.KrObjectModel;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers.SqlProcessing
{
    public interface IKrSqlExecutor
    {
        /// <summary>
        /// Вычисление условного SQL-выражения.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task<bool> ExecuteConditionAsync(IKrSqlExecutorContext context);

        /// <summary>
        /// Вычисление списка SQL-исполнителей.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task<List<Performer>> ExecutePerformersAsync(IKrSqlExecutorContext context);

    }
}