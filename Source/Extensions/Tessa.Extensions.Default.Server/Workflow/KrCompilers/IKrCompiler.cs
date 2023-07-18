#nullable enable

using Tessa.Compilation;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers
{
    /// <summary>
    /// Объект, выполняющий компиляцию объектов подсистемы маршрутов.
    /// </summary>
    public interface IKrCompiler :
        ITessaCompiler<IKrCompilationContext>
    {
    }
}
