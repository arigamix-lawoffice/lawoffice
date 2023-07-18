#nullable enable

using System;

namespace Tessa.Extensions.Default.Server.Files.VirtualFiles.Compilation
{
    /// <summary>
    /// Контекст компиляции для компилятора <see cref="IKrVirtualFileCompiler"/>
    /// </summary>
    public interface IKrVirtualFileCompilationContext
    {
        /// <summary>
        /// Идентификатор виртуального файла.
        /// </summary>
        Guid ID { get; set; }

        /// <summary>
        /// Название виртуального файла.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// C# текст сценария инициализации виртуального файла.
        /// </summary>
        string? InitializationScenario { get; set; }
    }
}
