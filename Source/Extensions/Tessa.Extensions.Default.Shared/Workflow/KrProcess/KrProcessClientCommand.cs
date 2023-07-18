using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess
{
    /// <summary>
    /// Команда, формируемая на сервере при работе процесса Kr
    /// и возвращаемая на клиент для дальнейшей интерпретаци.
    /// </summary>
    [StorageObjectGenerator(GenerateDefaultConstructor = false)]
    public sealed partial class KrProcessClientCommand : StorageObject
    {
        /// <summary>
        /// Инициализируе новый экземпляр класса <see cref="KrProcessClientCommand"/>.
        /// </summary>
        /// <param name="commandType">Тип команды.</param>
        /// <param name="parameters">Параметры команды.</param>
        public KrProcessClientCommand(
            string commandType,
            Dictionary<string, object> parameters = null) 
            : base(new Dictionary<string, object>())
        {
            this.Set(nameof(this.CommandType), commandType);
            this.Set(nameof(this.Parameters), parameters ?? new Dictionary<string, object>());
        }

        /// <inheritdoc />
        public KrProcessClientCommand(
            Dictionary<string, object> storage)
            : base(storage)
        {
        }

        /// <summary>
        /// Возвращает тип команды.
        /// </summary>
        public string CommandType => this.Get<string>(nameof(this.CommandType));

        /// <summary>
        /// Возвращает параметры команды.
        /// </summary>
        public Dictionary<string, object> Parameters =>
            this.Get<Dictionary<string, object>>(nameof(this.Parameters), () => new Dictionary<string, object>(StringComparer.Ordinal));
    }
}
