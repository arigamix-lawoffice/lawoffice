using System;
using Tessa.Platform;
using Tessa.Test.Default.Shared.GC.Handlers;

namespace Tessa.Test.Default.Shared.GC
{
    /// <summary>
    /// Реестр обработчиков внешних объектов <see cref="IExternalObjectHandler"/>.
    /// </summary>
    public interface IExternalObjectHandlerRegistry :
        IRegistry<Guid, IExternalObjectHandler>
    {
    }
}
