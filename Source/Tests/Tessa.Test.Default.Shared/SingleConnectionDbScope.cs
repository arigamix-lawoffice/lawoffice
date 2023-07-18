#nullable enable
using System;
using Tessa.Platform.Data;

namespace Tessa.Test.Default.Shared
{
    /// <summary>
    /// Объект для взаимодействия с базой данные.
    /// Всегда использует только одно подключение. Метод <see cref="CreateNew()"/> и его перегрузки не создают новое подключение.
    /// Определяет область видимости объекта <see cref="DbManager"/>.
    /// </summary>
    /// <remarks>
    /// Все открытые методы и свойства класса являются потокобезопасными.
    /// </remarks>
    public sealed class SingleConnectionDbScope : DbScope
    {
        public SingleConnectionDbScope(Func<DbManager> dbFactory)
            : base(dbFactory)
        {
        }

        /// <inheritdoc/>
        public override IDbScopeInstance CreateNew() =>
            this.Create();

        /// <inheritdoc/>
        public override IDbScopeInstance CreateNew(
            string? configurationString,
            string? description = null) =>
            this.Create();

        /// <inheritdoc/>
        public override IDbScopeInstance CreateNew(
            Func<DbManager>? dbFactory,
            Func<DbManager, IQueryExecutor>? executorFactory = null,
            string? description = null) =>
            this.Create();
    }
}
