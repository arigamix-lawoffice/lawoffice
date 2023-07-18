#nullable enable

using Tessa.Extensions.Default.Shared;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers
{
    /// <summary>
    /// Кэш, содержащий результаты компиляции вторичных процессов подсистемы маршрутов и этапов в них.
    /// </summary>
    /// <remarks>Категория кэша: <see cref="DefaultCompilationCacheNames.KrSecondaryProcess"/>.</remarks>
    public interface IKrSecondaryProcessCompilationCache :
        IKrCompilationCacheBase
    {
    }
}
