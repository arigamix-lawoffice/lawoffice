#nullable enable

using System;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework.Internal;

namespace Tessa.Test.Default.Shared
{
    /// <summary>
    /// Объект, предоставляющий имена для временных ресурсов.
    /// </summary>
    public interface ITestNameResolver
    {
        /// <summary>
        /// Возвращает имя ресурса, полученное для указанного идентификатора класса с тестами.
        /// </summary>
        /// <param name="testFixtureID">Идентификатор класса с тестами. Это может быть полное имя класса с тестами или полное название <see cref="TestFixture"/>.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Имя ресурса.</returns>
        /// <remarks>Для однозначного формирования, используйте более специфичные методы из класса расширения <see cref="TestNameResolverExtensions"/>.</remarks>
        ValueTask<string> GetFixtureNameAsync(
            string? testFixtureID,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Возвращает значение параметра FixtureDate или текущую дату и время, если параметр не задан в конфигурационном файле.
        /// </summary>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Значение параметра FixtureDate или текущая дата и время, если параметр не задан в конфигурационном файле.</returns>
        ValueTask<DateTime> GetFixtureDateTimeAsync(
            CancellationToken cancellationToken = default);
    }
}
