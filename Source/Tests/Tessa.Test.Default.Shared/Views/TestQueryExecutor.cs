using System.Threading;
using System.Threading.Tasks;
using Tessa.Views;

namespace Tessa.Test.Default.Shared.Views
{
    /// <summary>
    ///     Тестовый исполнитель запросов представлений
    /// </summary>
    public sealed class TestQueryExecutor : IViewQueryExecutor
    {
        #region Public Methods and Operators

        /// <summary>
        /// Выполняет запрос <paramref name="queryText"/> в соответствии
        ///     с заданными параметрами запроса <paramref name="request"/>
        /// </summary>
        /// <param name="queryText">
        /// Текст запроса
        /// </param>
        /// <param name="request">
        /// <see cref="ITessaViewRequest"/> Параметры запроса
        /// </param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>
        /// Результат исполнения запроса <see cref="ITessaViewResult"/>
        /// </returns>
        public Task<ITessaViewResult> ExecuteAsync(
            string queryText,
            ITessaViewRequest request,
            CancellationToken cancellationToken = default)
        {
            return Task.FromResult<ITessaViewResult>(new TessaViewResult());
        }

        #endregion
    }
}
