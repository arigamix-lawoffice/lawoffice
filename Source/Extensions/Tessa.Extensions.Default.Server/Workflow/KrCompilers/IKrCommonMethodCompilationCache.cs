#nullable enable

using Tessa.Extensions.Default.Shared;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers
{
    /// <summary>
    /// Кэш, содержащий результаты компиляции методов расширений подсистемы маршрутов.
    /// </summary>
    /// <remarks>Категория кэша: <see cref="DefaultCompilationCacheNames.KrCommonMethod"/>.</remarks>
    public interface IKrCommonMethodCompilationCache :
        IKrCompilationCacheBase
    {
    }
}
