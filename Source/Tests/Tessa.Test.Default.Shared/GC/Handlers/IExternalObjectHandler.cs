using System;
using System.Threading.Tasks;
using Tessa.Platform;

namespace Tessa.Test.Default.Shared.GC.Handlers
{
    /// <summary>
    /// Обработчик внешних объектов.
    /// </summary>
    public interface IExternalObjectHandler :
        IRegistryItem<Guid>
    {
        /// <summary>
        /// Обрабатывает объект, заданный в контексте.
        /// </summary>
        /// <param name="context">Контекст обработчика.</param>
        /// <returns>Асинхронная задача.</returns>
        ValueTask HandleAsync(IExternalObjectHandlerContext context);
    }
}
