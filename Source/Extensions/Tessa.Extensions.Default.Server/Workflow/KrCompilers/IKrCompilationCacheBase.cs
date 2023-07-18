#nullable enable

using Tessa.Compilation;
using Tessa.Extensions.Default.Server.Workflow.KrCompilers.UserAPI;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers
{
    /// <summary>
    /// Кэш, содержащий результаты компиляции объектов подсистемы маршрутов.
    /// </summary>
    public interface IKrCompilationCacheBase :
        ITessaCompilationObjectCache<IKrCompilationContext, string, IKrScript>
    {
    }
}
