#nullable enable

using System;
using Tessa.Compilation;
using Tessa.Extensions.Default.Shared;

namespace Tessa.Extensions.Default.Server.Files.VirtualFiles.Compilation
{
    /// <summary>
    /// Кэш, содержащий результаты компиляции скриптов виртуальных файлов.
    /// </summary>
    /// <remarks>Категория кэша: <see cref="DefaultCompilationCacheNames.KrVirtualFile"/>.</remarks>
    public interface IKrVirtualFileCompilationCache :
        ITessaCompilationObjectCache<IKrVirtualFileCompilationContext, Guid, IKrVirtualFileScript>
    {
    }
}
