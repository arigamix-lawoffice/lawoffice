#nullable enable

using Tessa.Extensions.Default.Shared;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers
{
    /// <summary>
    /// Кэш, содержащий результаты компиляции групп этапов процессов подсистемы маршрутов.
    /// </summary>
    /// <remarks>Категория кэша: <see cref="DefaultCompilationCacheNames.KrStageGroup"/>.</remarks>
    public interface IKrStageGroupCompilationCache :
        IKrCompilationCacheBase
    {
    }
}
