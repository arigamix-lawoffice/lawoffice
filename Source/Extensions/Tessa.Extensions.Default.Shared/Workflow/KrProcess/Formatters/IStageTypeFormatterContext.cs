using System.Collections.Generic;
using Tessa.Cards;
using Tessa.Platform.Runtime;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess.Formatters
{
    /// <summary>
    /// Описывает контекст форматтера этапа.
    /// </summary>
    public interface IStageTypeFormatterContext
        : IExtensionContext
    {
        /// <summary>
        /// Возвращает сессию пользователя.
        /// </summary>
        ISession Session { get; }

        /// <summary>
        /// Возвращает дополнительную информацию.
        /// </summary>
        Dictionary<string, object> Info { get; }

        /// <summary>
        /// Возвращает карточку содержащую этап.
        /// </summary>
        Card Card { get; }

        /// <summary>
        /// Возвращает строку содержащую этап.
        /// </summary>
        CardRow StageRow { get; }

        /// <summary>
        /// Возвращает словарь содержащий настройки этапа.
        /// </summary>
        IDictionary<string, object> Settings { get; }

        /// <summary>
        /// Возвращает или задаёт отображаемый срок исполнения.
        /// </summary>
        string DisplayTimeLimit { get; set; }

        /// <summary>
        /// Возвращает или задаёт отображаемый список участников.
        /// </summary>
        string DisplayParticipants { get; set; }

        /// <summary>
        /// Возвращает или задаёт отображаемые настройки.
        /// </summary>
        string DisplaySettings { get; set; }
    }
}