#nullable enable
using System;
using Tessa.Platform.Data;

namespace Tessa.Test.Default.Shared.Platform.Data
{
    /// <inheritdoc cref="ISeparateDbConnectionWorker"/>
    public sealed class FakeSeparateDbConnectionWorker : ISeparateDbConnectionWorker
    {
        private readonly IDbScope dbScope;

        public FakeSeparateDbConnectionWorker(IDbScope dbScope) => this.dbScope = NotNullOrThrow(dbScope);

        /// <inheritdoc/>
        public IAsyncDisposable CreateScope(bool throwIfDefault = false, bool forceNewConnection = false) =>
            this.dbScope.CreateNew();
    }
}
