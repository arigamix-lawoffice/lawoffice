using System;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Platform;
using Tessa.Platform.Data;

namespace Tessa.Test.Default.Shared.Cards.Numbers
{
    /// <summary>
    /// Предоставляет вспомогательные статические методы для работы с номерами и последовательностями.
    /// </summary>
    public static class TestNumberHelper
    {
        #region Numbers

        /// <summary>
        /// Возвращает значение, показывающее, что указанный номер зарезервирован.
        /// </summary>
        /// <param name="dbScope">Объект для взаимодействия с базой данных.</param>
        /// <param name="number">Проверяемый номер.</param>
        /// <param name="sequenceName">Имя последовательности, содержащей проверяемый номер.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Значение <see langword="true"/>, если указанный номер зарезервирован, иначе - <see langword="false"/>.</returns>
        public static async Task<bool> IsNumberReservedAsync(
            IDbScope dbScope,
            long number,
            string sequenceName,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(dbScope, nameof(dbScope));

            await using (dbScope.Create())
            {
                DbManager db = dbScope.Db;
                var hasRow = await db
                    .SetCommand(dbScope.BuilderFactory
                            .Select().Top(1).V(true)
                            .From("SequencesInfo", "inf").NoLock()
                            .InnerJoin("SequencesIntervals", "vals").NoLock().On().C("inf", "ID").Equals().C("vals", "ID")
                            .Where().LowerC("inf", "Name").Equals().LowerP("Sequence")
                            .And().C("First").LessOrEquals().P("Number").And().C("Last").GreaterOrEquals().P("Number")
                            .Limit(1)
                            .Build(),
                        db.Parameter("Number", number),
                        db.Parameter("Sequence", sequenceName))
                    .ExecuteAsync<bool>(cancellationToken);
                return !hasRow;
            }
        }

        #endregion
    }
}
