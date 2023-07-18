#nullable enable

using System;
using System.Collections.Generic;
using Tessa.Cards;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess
{
    /// <summary>
    /// Результат запуска процесса.
    /// </summary>
    public interface IKrProcessLaunchResult
    {
        /// <inheritdoc cref="KrProcessLaunchStatus"/>
        KrProcessLaunchStatus LaunchStatus { get; }

        /// <summary>
        /// Идентификатор запущенного асинхронного процесса или значение <see langword="null"/>, если при запуске процесса произошла ошибка или запускался синхронный процесс.
        /// </summary>
        Guid? ProcessID { get; }

        /// <summary>
        /// Объект, используемый для построения результата валидации.
        /// </summary>
        ValidationStorageResultBuilder ValidationResult { get; }

        /// <summary>
        /// Дополнительная информация процесса после его завершения.
        /// </summary>
        IDictionary<string, object?>? ProcessInfo { get; }

        /// <summary>
        /// Ответ на запрос на сохранение, при котором был запущен процесс.
        /// </summary>
        CardStoreResponse? StoreResponse { get; }

        /// <summary>
        /// Ответ на универсальный запрос, при котором был запущен процесс.
        /// </summary>
        CardResponse? CardResponse { get; }
    }
}
