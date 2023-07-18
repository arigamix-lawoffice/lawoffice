#nullable enable

using System.Threading.Tasks;

namespace Tessa.Extensions.Default.Server.Files.VirtualFiles.Compilation
{
    /// <summary>
    /// Объект со скриптами виртуального файла.
    /// </summary>
    public interface IKrVirtualFileScript
    {
        /// <summary>
        /// Скрипт инициализации виртуального файла.
        /// </summary>
        /// <param name="context">Контекст выполнения скрипта.</param>
        Task InitializationScenarioAsync(IKrVirtualFileScriptContext context);
    }
}
