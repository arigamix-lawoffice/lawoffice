using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Tessa.Extensions.Default.Server.Workflow.KrObjectModel;
using Tessa.Platform.Collections;
using Tessa.Platform.Data;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers.SqlProcessing
{
    public sealed class KrSqlExecutor: IKrSqlExecutor
    {
        #region fields

        private readonly Func<IKrSqlPreprocessor> getSqlPreprocessor;

        private readonly IDbScope dbScope;

        #endregion

        #region constructor

        public KrSqlExecutor(
            Func<IKrSqlPreprocessor> getSqlPreprocessor,
            IDbScope dbScope)
        {
            this.getSqlPreprocessor = getSqlPreprocessor;
            this.dbScope = dbScope;
        }

        #endregion

        #region implementation

        /// <inheritdoc />
        public async Task<bool> ExecuteConditionAsync(IKrSqlExecutorContext context)
        {
            if (string.IsNullOrWhiteSpace(context.Query))
            {
                return true;
            }

            var sqlPreprocessorResult = this.getSqlPreprocessor().Preprocess(context);

            try
            {
                await using (this.dbScope.Create())
                {
                    var db = this.dbScope.Db;
                    db.SetCommand(
                        sqlPreprocessorResult.Query,
                        sqlPreprocessorResult.Parameters.Select(p => db.Parameter(p.Key, p.Value)).ToArray());

                    bool result = false;
                    await using (var reader = await db.ExecuteReaderAsync(context.CancellationToken))
                    {
                        if (await reader.ReadAsync(context.CancellationToken))
                        {
                            if (reader.FieldCount != 1)
                            {
                                ThrowException(
                                    null,
                                    "$KrProcess_ErrorMessage_SqlConditionTooManyColumns",
                                    sqlPreprocessorResult,
                                    context);
                            }

                            var value = reader.GetValue<object>(0);
                            if (value == null || value.Equals(0) || value.Equals(false))
                            {
                                // ReSharper disable once RedundantAssignment
                                result = false;
                            }
                            else if (value.Equals(1) || value.Equals(true))
                            {
                                result = true;
                            }
                            else
                            {
                                ThrowException(
                                    null,
                                    "$KrProcess_ErrorMessage_SqlConditionTooManyRows",
                                    sqlPreprocessorResult,
                                    context);
                            }
                        }
                        else
                        {
                            // ReSharper disable once RedundantAssignment
                            result = false;
                        }

                        if (await reader.ReadAsync(context.CancellationToken))
                        {
                            ThrowException(
                                null,
                                "$KrProcess_ErrorMessage_SqlPerformersError",
                                sqlPreprocessorResult,
                                context);
                        }
                    }

                    return result;
                }
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (QueryExecutionException)
            {
                throw;
            }
            catch (Exception e)
            {
                ThrowException(
                    e,
                    "$KrProcess_ErrorMessage_SqlPerformersError",
                    sqlPreprocessorResult,
                    context);
            }

            return false;
        }

        /// <inheritdoc />
        public async Task<List<Performer>> ExecutePerformersAsync(IKrSqlExecutorContext context)
        {
            if (string.IsNullOrWhiteSpace(context.Query))
            {
                return new List<Performer>();
            }
            var sqlPreprocessorResult = this.getSqlPreprocessor().Preprocess(context);

            var roles = new List<Performer>();
            try
            {
                await using (this.dbScope.Create())
                {
                    var db = this.dbScope.Db;
                    db.SetCommand(
                        sqlPreprocessorResult.Query,
                        sqlPreprocessorResult.Parameters.Select(p => db.Parameter(p.Key, p.Value)).ToArray());
                    await using var reader = await db.ExecuteReaderAsync(context.CancellationToken);
                    roles.AddRange(await ReadPerformersAsync(reader, context.StageRowID, context));
                }
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (QueryExecutionException)
            {
                throw;
            }
            catch (Exception e)
            {
                ThrowException(
                    e,
                    "$KrProcess_ErrorMessage_SqlPerformersError",
                    sqlPreprocessorResult,
                    context);
            }

            return roles;
        }

        #endregion

        #region private

        private static async Task<IEnumerable<Performer>> ReadPerformersAsync(DbDataReader reader, Guid stageRowID, IKrSqlExecutorContext context)
        {
            // на разных СУБД разные типы, поэтому мы не можем их проверить по GetDataTypeName;
            // читаем первую строку и смотрим

            if (!await reader.ReadAsync(context.CancellationToken))
            {
                return Array.Empty<Performer>();
            }

            if (reader.FieldCount != 2
                || !(reader[0] is Guid firstID)
                || !(reader[1] is string firstName))
            {
                var errorText = context.GetErrorTextFunc(
                    context,
                    "$KrProcess_ErrorMessage_IncorrectRoleResultSet",
                    Array.Empty<object>());
                throw new QueryExecutionException(errorText, context.Query);
            }

            var result = new List<Performer> { new MultiPerformer(firstID, firstName, stageRowID, isSql: true) };

            while (await reader.ReadAsync(context.CancellationToken))
            {
                result.Add(new MultiPerformer(reader.GetGuid(0), reader.GetNullableString(1), stageRowID, isSql: true));
            }

            // Проверка есть ли ещё запросы, в т.ч. содержащими ошибки.
            if (await reader.NextResultAsync(context.CancellationToken))
            {
                var errorText = context.GetErrorTextFunc(
                    context,
                    "$KrProcess_ErrorMessage_SeveralQueries",
                    Array.Empty<object>());
                throw new QueryExecutionException(errorText, context.Query);
            }

            return result;
        }

        /// <summary>
        /// Преобразовать список SQL параметров в одну строку.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static string SqlParametersToString(
            IEnumerable<KeyValuePair<string, object>> parameters)
        {
            return string.Join(
                Environment.NewLine,
                parameters.Select(x => $"{x.Key} = {x.Value}"));
        }

        private static void ThrowException(
            Exception e,
            string text,
            IKrSqlPreprocessorResult sqlPreprocessorResult,
            IKrSqlExecutorContext context)
        {
            var query =
                SqlParametersToString(sqlPreprocessorResult.Parameters) + Environment.NewLine +
                sqlPreprocessorResult.Query;

            var errorText = context.GetErrorTextFunc(
                context,
                text,
                Array.Empty<object>());

            throw new QueryExecutionException(errorText, query, e);
        }

        #endregion
    }
}