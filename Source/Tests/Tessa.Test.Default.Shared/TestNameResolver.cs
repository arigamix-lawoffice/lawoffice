#nullable enable

using System;
using System.Buffers;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Platform;

namespace Tessa.Test.Default.Shared
{
    /// <inheritdoc cref="ITestNameResolver"/>
    public sealed class TestNameResolver :
        ITestNameResolver
    {
        #region Constants

        private const int SaltLength = 100;

        #endregion

        #region Fields

        private readonly Lazy<Random> randomLazy = new(() =>
        {
            long? seed = TestSettings.FixtureSeed;

            return seed > 0
                ? new Random((int) seed)
                : new Random();
        }, LazyThreadSafetyMode.PublicationOnly);

        #endregion

        #region ITestNameResolver Members

        /// <inheritdoc />
        public ValueTask<string> GetFixtureNameAsync(
            string? testFixtureID,
            CancellationToken cancellationToken = default)
        {
            byte[] bytes = ArrayPool<byte>.Shared.Rent(SaltLength);
            Span<byte> limitedBytes = bytes.AsSpan(0, SaltLength);
            this.randomLazy.Value.NextBytes(limitedBytes);

            string result = HashCode
                .Combine(
                    (testFixtureID ?? string.Empty).GetConstantHashCode(),
                    limitedBytes.GetConstantHashCode())
                .ToString("x4");

            ArrayPool<byte>.Shared.Return(bytes);
            return new(result);
        }

        /// <inheritdoc />
        public ValueTask<DateTime> GetFixtureDateTimeAsync(
            CancellationToken cancellationToken = default) =>
            ValueTask.FromResult(TestSettings.FixtureDate ?? DateTime.UtcNow);

        #endregion
    }
}
