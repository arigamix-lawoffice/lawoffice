#nullable enable

using Tessa.Compilation;

namespace Tessa.Extensions.Default.Server.Files.VirtualFiles.Compilation
{
    /// <summary>
    /// Объект, выполняющий компиляцию скриптов виртуальных файлов.
    /// </summary>
    public interface IKrVirtualFileCompiler :
        ITessaCompiler<IKrVirtualFileCompilationContext>
    {
    }
}
