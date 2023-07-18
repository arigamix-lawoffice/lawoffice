using System;
using System.Collections.Generic;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers.UserAPI
{
    /// <summary>
    /// Интерфейс, поддерживающий переключение контекста выполнения.
    /// </summary>
    public interface IContextChangeableScript
    {
        /// <summary>
        /// Идентификатор контекста, в котором необходимо выполнить этап вместо текущего.
        /// </summary>
        Guid? DifferentContextCardID { get; set; }

        /// <summary>
        /// Признак того, что необходимо выполнить в контексте карточки <see cref="DifferentContextCardID" /> всю группу.
        /// </summary>
        bool DifferentContextWholeCurrentGroup { get; set; }

        /// <summary>
        /// Место, где был установлен признак выполнения в другом контексте.
        /// </summary>
        KrScriptType? DifferentContextSetupScriptType { get; set; }

        /// <summary>
        /// Дополнительная информация о процессе, выполняемом в другом контексте.
        /// </summary>
        IDictionary<string, object> DifferentContextProcessInfo { get; set; }

    }
}