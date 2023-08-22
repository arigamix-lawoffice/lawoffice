using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LinqToDB.Data;
using Tessa.Platform.Data;

namespace Tessa.Extensions.Shared.Extensions
{
    /// <summary>
    ///     Extension methods for IDbScope.
    /// </summary>
    public static class DbScopeExtensions
    {
        #region Выполнить запрос

        /// <summary>
        ///     Execute the query.
        /// </summary>
        /// <param name="dbScope">An object for interacting with the database.</param>
        /// <param name="sqlCommand">Query.</param>
        /// <param name="cancellationToken">An object by which you can cancel the execution of an asynchronous task.</param>
        /// <param name="parameters">Request parameters.</param>
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
        ///     Get a list of values.
        /// </summary>
        /// <param name="dbScope">An object for interacting with the database.</param>
        /// <param name="sqlCommand">Query.</param>
        /// <param name="cancellationToken">An object by which you can cancel the execution of an asynchronous task.</param>
        /// <param name="parameters">Request parameters.</param>
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
        ///     Retrieves multiple fields from a section.
        /// </summary>
        /// <typeparam name="T1">Type of the first parameter returned.</typeparam>
        /// <typeparam name="T2">Type of the second returned parameter.</typeparam>
        /// <param name="dbScope">An object for interacting with the database.</param>
        /// <param name="sectionName">Section name.</param>
        /// <param name="fieldName1">Name of the first field.</param>
        /// <param name="fieldName2">Name of the second field.</param>
        /// <param name="value">ID</param>
        /// <param name="type">Name of the ID field.</param>
        /// <param name="cancellationToken">An object by which you can cancel the execution of an asynchronous task.</param>
        /// <returns>Asynchronous task.</returns>
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
        ///     Get the value of one field of the card.
        /// </summary>
        /// <param name="dbScope">An object for interacting with the database.</param>
        /// <param name="value">Column value.</param>
        /// <param name="sectionName">The section containing the field.</param>
        /// <param name="fieldName">Field name.</param>
        /// <param name="column">The column that we are searching for.</param>
        /// <param name="cancellationToken">An object by which you can cancel the execution of an asynchronous task.</param>
        /// <returns>Asynchronous task.</returns>
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