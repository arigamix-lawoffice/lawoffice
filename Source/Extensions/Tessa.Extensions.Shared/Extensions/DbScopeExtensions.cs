using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LinqToDB.Data;
using Tessa.Platform.Data;

namespace Tessa.Extensions.Shared.Extensions
{
    /// <summary>
    ///     Методы расширений для IDbScope
    /// </summary>
    public static class DbScopeExtensions
    {
        #region Выполнить запрос

        /// <summary>
        ///     Выполнить запрос.
        /// </summary>
        /// <param name="dbScope"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить выполнения асинхронной задачи.</param>
        /// <param name="parameters">Параметры запроса</param>
        public static async Task<int> ExecuteNonQueryAsync(
            this IDbScope dbScope,
            string sqlCommand,
            CancellationToken cancellationToken = default,
            params DataParameter[] parameters)
        {
            await using (dbScope.Create())
            {
                var db = dbScope.Db;
                return await db
                    .SetCommand(sqlCommand, parameters)
                    .LogCommand()
                    .ExecuteNonQueryAsync(cancellationToken);
            }
        }

        #endregion

        #region Получить список значений

        /// <summary>
        ///     Получить список значений.
        /// </summary>
        /// <param name="dbScope"></param>
        /// <param name="sqlCommand">Текст запроса</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить выполнения асинхронной задачи.</param>
        /// <param name="parameters">Параметры запроса</param>
        public static async Task<List<T>> GetListAsync<T>(
            this IDbScope dbScope,
            string sqlCommand,
            CancellationToken cancellationToken = default,
            params DataParameter[] parameters)
        {
            await using (dbScope.Create())
            {
                var db = dbScope.Db;
                return await db
                    .SetCommand(sqlCommand, parameters)
                    .LogCommand()
                    .ExecuteListAsync<T>(cancellationToken);
            }
        }

        #endregion

        /// <summary>
        ///     Получает несколько полей из секции
        /// </summary>
        /// <typeparam name="T1">Тип первого возвращаемого параметра</typeparam>
        /// <typeparam name="T2">Тип второго возвращаемого параметра</typeparam>
        /// <param name="dbScope"></param>
        /// <param name="sectionName">Название секции</param>
        /// <param name="fieldName1">Название первого поля</param>
        /// <param name="fieldName2">Название второго поля</param>
        /// <param name="value">ID</param>
        /// <param name="type">Название поля ID</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить выполнения асинхронной задачи.</param>
        /// <returns>Асинхронная задача.</returns>
        public static async Task<(T1, T2)> GetFieldsAsync<T1, T2>(
            this IDbScope dbScope,
            string sectionName,
            string fieldName1,
            string fieldName2,
            object value,
            string type = "ID",
            CancellationToken cancellationToken = default)
        {
            await using (dbScope.Create())
            {
                var db = dbScope.Db;

                db.SetCommand(dbScope.BuilderFactory
                            .Select()
                            .C(sectionName, fieldName1, fieldName2)
                            .From(sectionName).NoLock()
                            .Where().C(type).Equals().P(nameof(type))
                            .Build(),
                        new DataParameter(nameof(type), value))
                    .LogCommand();

                await using (var reader = await db.ExecuteReaderAsync(cancellationToken))
                {
                    if (await reader.ReadAsync(cancellationToken))
                    {
                        var field1 = reader.GetValue<T1>(0);
                        var field2 = reader.GetValue<T2>(1);
                        return (field1, field2);
                    }
                }

                return (default, default);
            }
        }

        /// <summary>
        ///     Получить значение одного поля карточки.
        /// </summary>
        /// <param name="dbScope"></param>
        /// <param name="value">Значение колонки</param>
        /// <param name="sectionName">Содержащая поле секция</param>
        /// <param name="fieldName">Название поля</param>
        /// <param name="column">Колонка, по которой выполняем поиск</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить выполнения асинхронной задачи.</param>
        /// <returns>Результат выполнения запроса</returns>
        public static async Task<T?> GetFieldAsync<T>(
            this IDbScope dbScope,
            object value,
            string sectionName,
            string fieldName,
            string column = "ID",
            CancellationToken cancellationToken = default)
        {
            await using (dbScope.Create())
            {
                var db = dbScope.Db;
                return await db
                    .SetCommand(dbScope.BuilderFactory
                        .Select().Top(1)
                        .C(fieldName)
                        .From(sectionName).NoLock()
                        .Where().C(column).Equals().P(nameof(column))
                        .Build(),
                        new DataParameter(nameof(column), value))
                    .LogCommand()
                    .ExecuteAsync<T>(cancellationToken);
            }
        }
    }
}