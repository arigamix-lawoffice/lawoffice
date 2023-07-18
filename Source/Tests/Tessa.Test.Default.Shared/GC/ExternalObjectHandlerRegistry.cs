using System;
using Tessa.Platform;
using Tessa.Test.Default.Shared.GC.Handlers;

namespace Tessa.Test.Default.Shared.GC
{
    /// <inheritdoc cref="IExternalObjectHandlerRegistry"/>
    public sealed class ExternalObjectHandlerRegistry :
        Registry<Guid, IExternalObjectHandler>,
        IExternalObjectHandlerRegistry
    {
    }
}
