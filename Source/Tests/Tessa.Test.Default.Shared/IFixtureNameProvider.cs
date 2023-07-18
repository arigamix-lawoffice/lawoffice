using System;
using System.Threading;
using System.Threading.Tasks;

namespace Tessa.Test.Default.Shared
{
    /// <summary>
    /// Объект, предоставляющий имена для временных ресурсов.
    /// </summary>
    /// <remarks>Возвращаемые значения не изменяются между вызовами, если не указано иного.</remarks>
    public interface IFixtureNameProvider
    {
        /// <summary>
        /// Возвращает имя ресурса, полученное для класса, содержащего выполняемый тест. При последующих вызовах, в рамках этого же экземпляра класса или области выполнения, будет возвращено постоянное значение.
        /// </summary>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Имя ресурса.</returns>
        ValueTask<string> GetFixtureNameAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Возвращает имя ресурса, полученное для класса, содержащего выполняемый тест. При последующих вызовах будет возвращено новое значение.
        /// </summary>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Имя ресурса.</returns>
        ValueTask<string> GetNextRandomFixtureNameAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Возвращает значение параметра FixtureDate или текущую дату и время, если параметр не задан в конфигурационном файле. При последующих вызовах, в рамках этого же экземпляра класса или области выполнения, будет возвращено постоянное значение.
        /// </summary>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Значение параметра FixtureDate или текущая дата и время, если параметр не задан в конфигурационном файле.</returns>
        ValueTask<DateTime> GetFixtureDateTimeAsync(CancellationToken cancellationToken = default);
    }
}
