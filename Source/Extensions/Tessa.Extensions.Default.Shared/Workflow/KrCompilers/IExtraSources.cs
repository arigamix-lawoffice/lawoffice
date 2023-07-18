using System.Collections.Generic;

namespace Tessa.Extensions.Default.Shared.Workflow.KrCompilers
{
    /// <summary>
    /// Описывает объект, предоставляющий дополнительные методы.
    /// </summary>
    public interface IExtraSources
    {
        /// <summary>
        /// Возвращает список дополнительных методов.
        /// </summary>
        IReadOnlyList<IExtraSource> ExtraSources { get; }
    }
}