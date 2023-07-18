#nullable enable

using System;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace Tessa.Test.Default.Shared
{
    /// <summary>
    /// Предоставляет статические методы расширения для <see cref="ITestNameResolver"/>.
    /// </summary>
    public static class TestNameResolverExtensions
    {
        /// <summary>
        /// Возвращает имя ресурса, полученное для указанного типа класса, содержащего тесты.
        /// </summary>
        /// <param name="type">Тип, для которого должно быть получено имя ресурса.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Имя ресурса.</returns>
        public static ValueTask<string> GetFixtureNameAsync(
            this ITestNameResolver testNameResolver,
            Type? type,
            CancellationToken cancellationToken = default)
        {
            return NotNullOrThrow(testNameResolver).GetFixtureNameAsync(
                type?.AssemblyQualifiedName,
                cancellationToken);
        }

        /// <summary>
        /// Возвращает имя ресурса, полученное для указанного класса, содержащего тесты, получаемого из <paramref name="test"/>.
        /// </summary>
        /// <param name="test">Тест, для которого должно быть получено имя ресурса.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Имя ресурса.</returns>
        public static ValueTask<string> GetFixtureNameAsync(
            this ITestNameResolver testNameResolver,
            ITest test,
            CancellationToken cancellationToken = default)
        {
            ThrowIfNull(testNameResolver);
            ThrowIfNull(test);

            var testFixture = test.FirstItemOrDefaultFromTree<TestFixture>();

            if (testFixture is null)
            {
                return testNameResolver.GetFixtureNameAsync(
                    test.TypeInfo?.Type,
                    cancellationToken);
            }

            return testNameResolver.GetFixtureNameAsync(
                testFixture.FullName,
                cancellationToken);
        }
    }
}
