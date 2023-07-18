#nullable enable

using Tessa.Extensions.Default.Shared;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers
{
    /// <summary>
    /// Кэш, содержащий результаты компиляции шаблонов этапов процессов подсистемы маршрутов и этапов в них.
    /// </summary>
    /// <remarks>Категория кэша: <see cref="DefaultCompilationCacheNames.KrStageTemplate"/>.</remarks>
    public interface IKrStageTemplateCompilationCache :
        IKrCompilationCacheBase
    {
    }
}
